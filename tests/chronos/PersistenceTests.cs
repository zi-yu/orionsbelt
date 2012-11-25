// created on 7/28/2004 at 8:39 AM

using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Alliances;
using Chronos;
using Chronos.Queue;
using Chronos.Persistence;
using Chronos.Messaging;
using NUnit.Framework;

namespace Chronos.Tests {

	public class PersistenceTests {
	
		#region Instance Fields

		protected Universe original;
		protected Universe loaded;
		protected IPersistence persistenceObj;
		protected DateTime saveStart;
		protected DateTime saveEnd;
		protected DateTime loadStart;
		protected DateTime loadEnd;
		protected DateTime turnStart;
		protected DateTime turnEnd;
		protected long size;
	
		#endregion
		
		#region Initialization
		
		public void init( IPersistence persistence )
		{
			try {
				persistenceObj = persistence;
			
				original = new Universe();
				original.init();

				original.Persistence = persistence;

				string allianceName = null;
				
				for( int i = 0; i < 10; ++i ) {
				
					Ruler ruler = new Ruler("PRE" +i+i);
					original.addRulerToUniverse(ruler, "PRE Planet ######");
					ruler.acceptMessage(new Message());
					for( int j = 0; j < 35; ++j ) {
						Messenger.Send(ruler, "BuildingCompleted");
					}
					Planet planet = ruler.Planets[0];
					planet.addResource("Intrinsic","gold",100000);
					planet.addResource("Intrinsic","labor",100000);
					planet.addResource("Intrinsic","groundSpace",100000);
					planet.addResource("Intrinsic","mp",100000);
					Result result = planet.canQueue("Building", "Farm", 1);
					if( result.Ok ) {
						planet.queue("Building", "Farm", 1);
					} else {
						Console.WriteLine(result.log());
						throw new Exception(result.log());
					}
					planet.queue("Building", "Farm", 1);
					
					if( i % 20 == 0 ) {
						allianceName = "Alliance-" + i;
						original.createAlliance(allianceName, ruler);
					} else {
						original.changeRulerAlliance(allianceName, ruler);
					}
				}
				original.turn();

				turnStart = DateTime.Now;
				original.turn();
				turnEnd = DateTime.Now;

				saveStart = DateTime.Now;
				persistence.save(original, Universe.Parameters);
				saveEnd = DateTime.Now;
				if( persistence is UniverseSerializer ) {
					size = ((UniverseSerializer)persistence).StreamSize;
				}
				
				loaded = new Universe();
				loaded.init();
				
				loadStart = DateTime.Now;
				loaded = persistence.load(Universe.Parameters);
				loadEnd = DateTime.Now;
				
			} catch( Exception e ) {
				Log.log(e);
			}
		}
		
		#endregion
	
		#region Universe Persistence Tests

		public void doUniverseTest( Universe original, Universe loaded )
		{
			doCoordinateTest(original.CurrentCoordinate, loaded.CurrentCoordinate, "Universe Current");
			Assert.AreEqual( original.TurnCount, loaded.TurnCount, "TurnCount mismatch");
			
			Assert.AreEqual( original.allianceCount, loaded.allianceCount, "Alliance count mismatch");
			Assert.AreEqual( original.rulerCount, loaded.rulerCount, "Ruler count mismatch");
			Assert.AreEqual( original.planetCount, loaded.planetCount, "Planet count mismatch");
			
			Assert.AreEqual( original.AllianceId, loaded.AllianceId, "AllianceId mismatch");
			Assert.AreEqual( original.RulerId, loaded.RulerId, "RulerId mismatch");
			Assert.AreEqual( original.PlanetId, loaded.PlanetId, "PlanetId mismatch");
		}
		
		#endregion
		
		#region Alliance Persistence Tests
		
		public void doAllianceTest( Universe original, Universe loaded )
		{
			Assert.AreEqual( original.allianceCount, loaded.allianceCount, "[1] Alliance count don't match" );
			
			IDictionaryEnumerator originalIt = original.alliances.GetEnumerator();
			
			while( originalIt.MoveNext() ) {
				Alliance originalAlliance = (Alliance) originalIt.Value;
				Alliance loadedAlliance = (Alliance) loaded.alliances[originalAlliance.Name];
				Assert.IsNotNull(loadedAlliance, "No '" + originalAlliance.Name + "' found in loaded Universe");
				
				Assert.AreEqual( originalAlliance.Name, loadedAlliance.Name, "[4] Diferent alliance names" );
				Assert.AreEqual( originalAlliance.Id, loadedAlliance.Id, "[3] Diferent alliance id's for alliance " + originalAlliance.Name );
				Assert.AreEqual( originalAlliance.Members.Length, loadedAlliance.Members.Length, "[5] Member check failed for alliance " + originalAlliance.Name );
				Assert.AreEqual( originalAlliance.IsSharing, loadedAlliance.IsSharing, "[9] Sharing check failed for alliance " + originalAlliance.Name );
				
				for( int i = 0; i < originalAlliance.Members.Length; ++i ) {
					Assert.AreEqual( originalAlliance.Members[i].Ruler.Id, loadedAlliance.Members[i].Ruler.Id, "[6] Alliance member id check failed for " + originalAlliance.Members[i].Ruler.Name);
					Assert.AreEqual( originalAlliance.Members[i].Ruler.Name, loadedAlliance.Members[i].Ruler.Name, "[7] Alliance member id check failed for " + originalAlliance.Members[i].Ruler.Id);
					Assert.AreEqual( originalAlliance.Members[i].RulerRole, loadedAlliance.Members[i].RulerRole, "[8] Alliance member role check failed for ruler #" +originalAlliance.Members[i].Ruler.Id );
				}
			}
		}
		
		#endregion
		
		#region Ruler Persistence Tests
		
		public void doRulerTest( Universe original, Universe loaded )
		{
			Assert.AreEqual( original.rulerCount, loaded.rulerCount, "[1] Ruler count don't match" );
			
			IDictionaryEnumerator originalIt = original.rulers.GetEnumerator();
			
			while( originalIt.MoveNext()  ) {
				
				Ruler originalRuler = (Ruler) originalIt.Value;
				Ruler loadedRuler = (Ruler) loaded.rulers[originalIt.Key];
				
				Assert.AreEqual( originalRuler.Id, loadedRuler.Id, "[3] Ruler ID check failed" );
				int id = originalRuler.Id;
				
				doResourceManagerTest(originalRuler, loadedRuler);
				
				Assert.AreEqual( originalRuler.IsSharing, loadedRuler.IsSharing, "[5] Ruler sharing check failed for ruler " + id);
				Assert.AreEqual( originalRuler.MaxPlanets, loadedRuler.MaxPlanets, "[6] Ruler MaxPlanets check failed for ruler " + id);
				Assert.AreEqual( originalRuler.Name, loadedRuler.Name, "[7] Ruler name check failed for ruler " + id);
				Assert.AreEqual( originalRuler.Owner.Id, loadedRuler.Owner.Id, "[8] Ruler Owner ID check failed for ruler " + id);
				Assert.AreEqual( originalRuler.Planets.Length, loadedRuler.Planets.Length, "[9] Ruler Planet Count check failed for ruler " + id);
				Assert.AreEqual( originalRuler.QueueCapacity, loadedRuler.QueueCapacity, "[10] Ruler Queue Capacity check failed for ruler " + id);
			}
		}
		
		#endregion
		
		#region Planet Persistence Tests
		
		public void doPlanetTest( Universe original, Universe loaded )
		{
			Assert.AreEqual( original.planetCount, loaded.planetCount, "Planet Count check failed" );
			
			IDictionaryEnumerator originalIt = original.planets.GetEnumerator();
			
			while( originalIt.MoveNext() ) {
                				
				Planet originalPlanet = (Planet) originalIt.Value;
				if( originalPlanet.Owner == null )
					continue;

				Planet loadedPlanet = (Planet) loaded.planets[originalIt.Key];
				Assert.IsNotNull(loadedPlanet, "Planet " + originalIt.Key.ToString() + " not found in loaded Universe" );
				
				Assert.AreEqual( originalPlanet.Id, loadedPlanet.Id, "[3] Planet ID check failed" );
				int id = originalPlanet.Id;
				
				doResourceManagerTest(originalPlanet, loadedPlanet);
				
				Assert.AreEqual( originalPlanet.IsSharing, loadedPlanet.IsSharing, "[5] Planet sharing check failed for ruler " + id);
				Assert.AreEqual( originalPlanet.Name, loadedPlanet.Name, "[7] Ruler name check failed for ruler " + id);
				Assert.AreEqual( originalPlanet.Owner.Id, loadedPlanet.Owner.Id, "[8] Planet Owner ID check failed for ruler " + id);
				doCoordinateTest(originalPlanet.Coordinate, loadedPlanet.Coordinate, id + "planet");
			}
		}
		
		#endregion
		
		#region ResourceManager Persistencee Tests
		
		public void doResourceManagerTest( ResourceManager original, ResourceManager loaded )
		{
			string id = original.Identifier + "-" + original.Id;
		
			Assert.AreEqual( original.Identifier, loaded.Identifier, "[1] Ruler identifier check failed for ruler " + id);
			Assert.AreEqual( original.Resources.Count, loaded.Resources.Count, "[2] Ruler Resource Count check failed for ruler " + id);
			
			IDictionaryEnumerator originalResources = original.Resources.GetEnumerator();
			
			while( originalResources.MoveNext() ) {
				ResourceInfo loadedInfo = (ResourceInfo) loaded.Resources[originalResources.Key];
				Assert.IsNotNull(loadedInfo,"No ResourceInfo key '" + originalResources.Key.ToString() + "' in " + id);
				doResourceInfoTest( (ResourceInfo) originalResources.Value, loadedInfo, id );
			}
		}
		
		public void doResourceInfoTest( ResourceInfo original, ResourceInfo loaded, string id )
		{
			id = id + "-" + original.Category;
			
			Assert.AreEqual(original.Category, loaded.Category, "Category check failed for " + id );
			Assert.AreEqual(original.AllFactories.Count, loaded.AllFactories.Count, "All Factories Count check failed for " + id );
			Assert.AreEqual(original.AvailableFactories.Count, loaded.AvailableFactories.Count, "Available Factories Count check failed for " + id );
			
			doQueueItemTest(original.Current, loaded.Current, id);
			
			Assert.AreEqual(original.FullQueue, loaded.FullQueue, "Full Queue check failed for " + id );
			Assert.AreEqual(original.Owner.Id, loaded.Owner.Id, "Owner ID check failed for " + id );
			Assert.AreEqual(original.QueueCapacity, loaded.QueueCapacity, "Queue Capacity check failed for " + id );
			Assert.AreEqual(original.QueueCount, loaded.QueueCount, "Queue Count check failed for " + id );
			Assert.AreEqual(original.Resources.Count, loaded.Resources.Count, "Resource Count check failed for " + id );
			
			IDictionaryEnumerator originalResources = original.Resources.GetEnumerator();
			while( originalResources.MoveNext() ) {
				int loadedResource = (int) loaded.Resources[originalResources.Key];
				Assert.AreEqual( originalResources.Value, loadedResource, "Resource Value check failed for " + id + ", key: " + originalResources.Key.ToString());
			}
			
			IDictionaryEnumerator allfactoriesOriginal = original.AllFactories.GetEnumerator();
			while( allfactoriesOriginal.MoveNext() ) {
				string loadedFactory = ((ResourceFactory)loaded.AllFactories[allfactoriesOriginal.Key]).Name;
				Assert.AreEqual( ((ResourceFactory)allfactoriesOriginal.Value).Name, loadedFactory, "Resource factory check failed for " + id + ", key: " + allfactoriesOriginal.Key.ToString());
			}
			
			IDictionaryEnumerator availablefactoriesOriginal = original.AvailableFactories.GetEnumerator();
			while( availablefactoriesOriginal.MoveNext() ) {
				string loadedFactory = ((ResourceFactory)loaded.AvailableFactories[availablefactoriesOriginal.Key]).Name;
				Assert.AreEqual( ((ResourceFactory)availablefactoriesOriginal.Value).Name, loadedFactory, "Resource factory check failed for " + id + ", key: " + availablefactoriesOriginal.Key.ToString());
			}
		}
		
		public void doQueueItemTest( QueueItem original, QueueItem loaded, string id )
		{
			if( original == null || loaded == null ) {
				Assert.IsNull(original, "Current null check failed for " + id );
				Assert.IsNull(loaded, "Current null check failed for " + id );
			} else {
				Assert.AreEqual( original.Duration, loaded.Duration, "Duration check failed for " + id );
				Assert.AreEqual( original.FactoryName, loaded.FactoryName, "FactoryName check failed for " + id );
				Assert.AreEqual( original.Factory.Name, loaded.Factory.Name, "Factory check failed for " + id );
				Assert.AreEqual( original.IsFinished, loaded.IsFinished, "isFiniched check failed for " + id );
				Assert.AreEqual( original.Owner.Id, loaded.Owner.Id, "Owner ID check failed for " + id );
				Assert.AreEqual( original.Quantity, loaded.Quantity, "Quantity check failed for " + id );
				Assert.AreEqual( original.RemainingTurns, loaded.RemainingTurns, "remaining turns check failed for " + id );
			}
		}
		
		#endregion
		
		#region Coordinate Persistece Tests
		
		/// <summayr>Verifica se duas coordenadas so iguais</summary>
		public void doCoordinateTest( Coordinate original, Coordinate loaded, string message )
		{
			Assert.AreEqual( original.Galaxy, loaded.Galaxy, "[1] " + message + " galaxy don't match");
			Assert.AreEqual( original.Sector, loaded.Sector, "[2] " + message + " Current sector don't match");
			Assert.AreEqual( original.Planet, loaded.Planet, "[3] " + message + " Current planet don't match");
		}
		
		#endregion
		
		#region Speed Test
		
		public void doTestSpeed( IPersistence persistence )
		{
			Log.log("");
			Log.log("--- " + GetType().Name + " ---");
			Log.log("Alliances: " + original.allianceCount + " Rulers: " + original.rulerCount + " Planets: " + original.planetCount);
			Log.log("Save time: " + (saveEnd-saveStart) );
			Log.log("Load time: " + (loadEnd-loadStart) );
			Log.log("Turn time: " + (turnEnd-turnStart) );
			Log.log(string.Format("Stream Size: {0} Kb", size/1024));
		}
		
		#endregion

		#region NUnit Tests
		
		[Test]
		public void testUniverse()
		{
			doUniverseTest(original, loaded);
		}
		
		[Test]
		public void testAlliance()
		{
			doAllianceTest(original, loaded);
		}
		
		[Test]
		public void testRulers()
		{
			doRulerTest(original, loaded);
		}
		
		[Test]
		public void testPlanets()
		{
			doPlanetTest(original, loaded);
		}
		
		[Test]
		public void doSpeedTest()
		{
			doTestSpeed(persistenceObj);
		}
		
		#endregion
		
	};
}
