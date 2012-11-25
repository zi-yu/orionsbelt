// created on 5/22/04 at 1:40 a

using System;
using System.Collections;
using Chronos;
using Chronos.Actions;
using Chronos.Resources;
using Chronos.Core;
using Chronos.Info;
using Chronos.Utils;
using Chronos.Info.Results;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class ResourcesTester {
		
		#region Non Tests
		
		private Planet planet;
		private Ruler ruler;

		[SetUp]
		public void init()
		{
			ruler = new Ruler(null, Globals.factories, "Ruler" );
			planet = new Planet(ruler, Globals.factories, "Planet", Coordinate.First);
			planet.addResource("labor", 10000);
			planet.addResource("mp", 10000);
			planet.addResource("gold", 10000);
			planet.addResource("energy", 10000);
		}
		
		#endregion
		
		#region Unique Building Tests
		
		[Test]
		public void test_StarPort()
		{
			UniqueBuilding("StarPort");
		}

		[Test]
		public void test_CommsSatellite()
		{
			Globals.ScanAvailable(ruler, planet);
			UniqueBuilding("CommsSatellite");
		}
		
		public void test_StockMarkets()
		{
			Globals.ToStockMarckets(ruler, planet);
			UniqueBuilding("StockMarkets");
		}
		
		public void test_MineralExtractor()
		{
			Globals.ToMineralExtractor(ruler, planet);
			UniqueBuilding("MineralExtractor");
		}
		
		public void test_LandReclamation()
		{
			Globals.ToLandReclamation(ruler, planet);
			UniqueBuilding("LandReclamation");
		}
		
		public void test_StarGate()
		{
			Globals.ToStarGate(ruler, planet);
			UniqueBuilding("StarGate");
		}
		
		public void test_Gate()
		{
			Globals.ToStarGate(ruler, planet);
			UniqueBuilding("Gate");
		}
		
		public void test_Spa()
		{
			Globals.ToSpa(ruler, planet);
			UniqueBuilding("Spa");
		}
		
		public void test_WaterReclamation()
		{
			Globals.ToWaterReclamation(ruler, planet);
			UniqueBuilding("WaterReclamation");
		}
		
		public void test_Hospital()
		{
			Globals.ToHospital(ruler, planet);
			UniqueBuilding("Hospital");
		}
		 
		#endregion
		
		#region Sanity Check
		
		private static string StaticAction = "-----STATIC ACTION-----";
		
		private static string[] SpecialResources =  new string[] {
			"groundSpace", 
			"waterSpace", 
			"orbitSpace", "labor", "housing"
		};
		
		private bool IsSpecialResource( string name ) 
		{
			foreach( string res in SpecialResources ) {
				if( res == name ) {
					return true;
				}
			}
			return false;
		}
		
		[Test]
		public void TestSpecialResourceConsistense()
		{
			ResourceBuilder factories = Universe.getFactories("planet", "Building");
			foreach( ResourceFactory factory in factories.Values ) {
				CheckResourceSanity( factory );
			}
		}
		
		[Test]
		public void TestSpecialResourceOnCompleteOnRemoveConsistence()
		{
			ResourceBuilder factories = Universe.getFactories("planet", "Building");
			foreach( ResourceFactory factory in factories.Values ) {
				CheckResourceOnCompleteOnRemoveSanity( factory );
			}
		}
		
		private void CheckResourceSanity( ResourceFactory factory )
		{
			Hashtable needed = GetSpecialResourcesNeeded(factory);
			Hashtable toRemove = GetAdd(factory.OnRemoveActions, true);
			Hashtable onCancelDuringBuild = GetAdd(factory.OnCancelDuringBuild, true);
			
			IDictionaryEnumerator it = needed.GetEnumerator();
			while( it.MoveNext() ) {
			
				if( toRemove[StaticAction] == null ) { 
					Assert.IsNotNull( toRemove[it.Key], "Factory " + factory.Name + " does not compensate " + it.Key + " on `onremove' actions" );
					Assert.AreEqual( it.Value, toRemove[it.Key], "Factory " + factory.Name + " does not compensate `" + it.Key + "' on `onremove' actions" );
				}
				
				Assert.IsNotNull( onCancelDuringBuild[it.Key], "Factory " + factory.Name + " does not compensate `" + it.Key + "' on `onCancelDuringBuild' actions" );
				Assert.AreEqual( it.Value, onCancelDuringBuild[it.Key], "Factory " + factory.Name + " does not compensate `" + it.Key + "' on `onCancelDuringBuild' actions" );
			}
		}
		
		private void CheckResourceOnCompleteOnRemoveSanity( ResourceFactory factory )
		{
			Hashtable needed = GetAdd(factory.OnCompleteActions, false);
			Hashtable toRemove = GetAdd(factory.OnRemoveActions, false);
			
			IDictionaryEnumerator it = needed.GetEnumerator();
			while( it.MoveNext() ) {
			
				if( toRemove[StaticAction] == null ) { 
					Assert.IsNotNull( toRemove[it.Key], "Factory " + factory.Name + " does not compensate " + it.Key + " on `onremove' actions" );
					Assert.AreEqual( "-"+it.Value, toRemove[it.Key], "Factory " + factory.Name + " does not compensate `" + it.Key + "' on `onremove' actions" );
				}

			}
		}
		
		private Hashtable GetSpecialResourcesNeeded( ResourceFactory factory )
		{
			Hashtable hash = new Hashtable();
			if( factory.CostActions == null ) {
				return hash;
			}
			
			foreach( Action action in factory.CostActions ) {
				ResourceNeeded needed = action as ResourceNeeded;
				if( needed == null ) {
					continue;
				}
				if( IsSpecialResource(needed.Key ) ) {
					hash.Add( needed.Key, needed.Value.ToString() );
				}
			}
			
			return hash;
		}
		
		private Hashtable GetAdd( Action[] actions, bool specialOnly )
		{
			Hashtable hash = new Hashtable();
			if( actions == null ) {
				return hash;
			}
			
			foreach( Action action in actions ) {
				Add addAction = action as Add;
				if( addAction == null ) {
					continue;
				}
	
				if( !specialOnly || IsSpecialResource(addAction.Value ) ) {
					hash.Add( addAction.Value, addAction.Quantity.ToString() );
				}
			}
			
			foreach( Action action in actions ) {
				Static staticAction = action as Static;
				
				if( staticAction != null && !staticAction.Action ) {
					hash[StaticAction] = true;
				}

			}
			
			return hash;
		}
		
		[Test]
		public void TestUnitDamage()
		{
			ResourceBuilder factories = Universe.getFactories("planet", "Unit");
			foreach( ResourceFactory factory in factories.Values ) {
				Assert.IsTrue( factory.Unit.MinimumDamage < factory.Unit.MaximumDamage, " Unit " + factory.Name + " has MinimumDamage greater than MaximumDamage"); 
			}
		}
		
		#endregion
		
		#region Utilities
		
		private void UniqueBuilding(string building)
		{
			planet.addResource("Intrinsic", "housing", 5000);
			planet.addResource("Intrinsic", "labor", 5000);
		
			Result result = planet.canQueue("Building", building, 1);
			Assert.AreEqual(result.Ok, true, "#1 " + result.log());
			
			planet.addResource("Building", building);
			
			result = planet.canQueue("Building", building, 1);
			Assert.AreEqual(result.Ok, false, "#2 " + result.log());
			
			bool removed = planet.take("Building", building, 1);
			Assert.AreEqual(removed, false, "#3 " + result.log());
		}
		
		#endregion
		
	};
	
}
