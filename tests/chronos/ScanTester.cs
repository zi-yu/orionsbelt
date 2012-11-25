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
using Chronos.Info;
using Chronos.Messaging;
using Chronos.Messaging.Messages;
using NUnit.Framework;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chronos.Tests {
	
	[TestFixture]
	public class ScanTester {
		
		#region Setup
		
		private Planet planet;
		private Ruler dumb;
		private Ruler ruler;
		private Ruler ruler2;
		
		private Coordinate SameSector = Coordinate.translateCoordinate("1:1:1:2");
		private Coordinate AnotherSector = Coordinate.translateCoordinate("1:1:2:2");
		private Coordinate AnotherSystem = Coordinate.translateCoordinate("1:2:1:2");
		private Coordinate AnotherGalaxy = Coordinate.translateCoordinate("3:1:1:2");
		private Coordinate AnotherPrivate = Coordinate.translateCoordinate("1:1:2:1");
		
		[SetUp]
		public void init()
		{
			ruler = new Ruler("PRE");
			planet = new Planet(ruler, Globals.factories, "Planet", Coordinate.First);
			ruler2 = new Ruler("yos");
			Planet p2 = new Planet(ruler2, Globals.factories, "Planet", Coordinate.First);
			
			Fleet f = new Fleet("Fleet",Coordinate.First,ruler);
			f.addShip("ColonyShip", 1);//, null);
			
			Globals.ScanAvailable(ruler, planet);
			Globals.ScanAvailable(ruler2, p2);
			
			planet.checkDependencies();
			planet.addResource("Building", "CommsSatellite", 1);
			planet.addResource("energy", 1500);
			
			p2.checkDependencies();
			p2.addResource("Building", "CommsSatellite", 1);
			p2.addResource("energy", 1500);
			
			dumb = new Ruler("PRE");
			new Planet(dumb, Globals.factories, "Planet", Coordinate.First);
			
			ruler.addResource("Research", "ScanLevelII", 1);
			ruler.checkDependencies();
			
			Universe.instance.conquerPlanet(SameSector, "buu", Globals.CreateFleetToConquer(ruler));

			Planet sameSectorPlanet = Universe.instance.getPlanet(SameSector);
			
			Globals.ScanAvailable(ruler, sameSectorPlanet);
			sameSectorPlanet.addResource("Building", "CommsSatellite", 1);
		}
		
		#endregion
		
		#region Auxiliar Methods Tests
		
		[Test]
		public void GetLevelTester()
		{
			Assert.AreEqual(Resource.GetResearchLevel(ruler, "asdasdasdasd"), 0, "Para factories no existentes, espera-se zero");
			
			Assert.AreEqual(Resource.GetResearchLevel(ruler2, "ScanLevel"), 1, "Level 1");
			
			ruler.addResource("Research", "ScanLevelII", 1); ruler.checkDependencies();
			Assert.AreEqual(Resource.GetResearchLevel(ruler, "ScanLevel"), 2, "Level 2");
			
			ruler.addResource("Research", "ScanLevelIII", 1); ruler.checkDependencies();
			Assert.AreEqual(Resource.GetResearchLevel(ruler, "ScanLevel"), 3, "Level 3");
		}
		
		#endregion
		
		#region PerformScan Tests
		
		[Test]
		public void CanPerformScan()
		{
			Assert.AreEqual(false, dumb.Planets[0].canScan(SameSector).Ok, "No CommsSatellite");

			Result result = planet.canScan(SameSector);
			Assert.AreEqual(true, result.Ok, result.log());
			
			planet.addResource("Intrinsic", "energy", -planet.Energy);
			result = planet.canScan(SameSector);
			Assert.AreEqual(false, result.Ok, result.log());
		}
		
		[Test]
		public void PerformScan_CheckLevel()
		{
			Assert.AreEqual(2, ruler.Planets.Length, "Where is the planet?");
			
			Scan scan = ruler2.HomePlanet.performScan(SameSector);
			Assert.AreEqual(true, scan.Success, "Should succeded");
			Assert.AreEqual(false, scan.Intercepted, "Should not be intercepted");
		}
		
		[Test]
		public void PerformScan_CheckNotification()
		{
			ruler.addResource("Research", "ScanNotificationI", 1);
			ruler.checkDependencies();
			ruler.addResource("Research", "ScanNotificationII", 1);
			ruler.checkDependencies();
			
			Scan scan = ruler2.HomePlanet.performScan(SameSector);
			Assert.AreEqual(true, scan.Success, "Should succeded");
			Assert.AreEqual(true, scan.Intercepted, "Should not be intercepted " + scan.HasCommsSatellite);
		}
		
		[Test]
		public void PerformScan_CheckShield()
		{
			ruler.addResource("Research", "ScanShieldLevelI", 1);
			ruler.checkDependencies();
			ruler.addResource("Research", "ScanShieldLevelII", 1);
			ruler.checkDependencies();
			
			Scan scan = ruler2.HomePlanet.performScan(SameSector);
			Assert.AreEqual(false, scan.Success, "Should fail");
			Assert.AreEqual(false, scan.Intercepted, "Should be intercepted");
		}
		
		[Test]
		public void PerformScan_CheckNotification_Shield()
		{
			ruler.addResource("Research", "ScanShieldLevelI", 1);
			ruler.checkDependencies();
			ruler.addResource("Research", "ScanShieldLevelII", 1);
			ruler.checkDependencies();
			ruler.addResource("Research", "ScanNotificationI", 1);
			ruler.checkDependencies();
			ruler.addResource("Research", "ScanNotificationII", 1);
			ruler.checkDependencies();
			
			Scan scan = ruler2.HomePlanet.performScan(SameSector);
			Assert.AreEqual(false, scan.Success, "Should fail");
			Assert.AreEqual(true, scan.Intercepted, "Should be intercepted");
		}
		
		[Test]
		public void Test_UnhabitedPlanet()
		{
			Coordinate c = Coordinate.translateCoordinate("1:1:2:3");
			ruler.addResource("Research", "SectorExploration", 1);
			ruler.checkDependencies();
			
			Result result = ruler.HomePlanet.canScan(c);
			Assert.AreEqual( result.Ok, true, result.log() );
			
			Scan scan = ruler.HomePlanet.performScan(c);
			
			Assert.AreEqual(scan.Target, c );
			Assert.IsFalse(scan.TargetPlanet.InitMade);
			Assert.AreEqual(scan.Intercepted, false );
			Assert.AreEqual(scan.ScanLevel, 2 );
			Assert.AreEqual(scan.Success, true );
		}
		
		[Test]
		public void Test_NotificationMessage()
		{
			ruler.addResource("Research", "ScanShieldLevelI", 1);
			ruler.checkDependencies();
			ruler.addResource("Research", "ScanShieldLevelII", 1);
			ruler.checkDependencies();
			ruler.addResource("Research", "ScanNotificationI", 1);
			ruler.checkDependencies();
			ruler.addResource("Research", "ScanNotificationII", 1);
			ruler.checkDependencies();
			
			ruler2.HomePlanet.performScan(SameSector);
			Assert.AreEqual(typeof(ScanDetected), ruler.GetMessages(Messenger.IntelMessages,20)[0].Info.GetType() , "No Scan detected");
		}
		
		[Test]
		public void Test_ScanCost()
		{
			int before = ruler2.HomePlanet.Energy;
			ruler2.HomePlanet.performScan(SameSector);
			int after = ruler2.HomePlanet.Energy;
			Assert.AreEqual(before - ruler2.HomePlanet.ScanCost, after);
		}
		
		#endregion
		
		#region IsAccessible Tests
		
		[Test]
		public void IsAccessibleTester_0()
		{
			Assert.IsFalse( Coordinate.IsAccessible(dumb.Planets[0], SameSector), "No tech" );
		}
		
		[Test]
		public void IsAccessibleTester_1()
		{
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, SameSector), "tech to same sector" );
			Assert.AreEqual(false, Coordinate.IsAccessible(planet, AnotherSector), "tech to another sector" );
			Assert.AreEqual(false, Coordinate.IsAccessible(planet, AnotherSystem), "tech to another system" );
			Assert.AreEqual(false, Coordinate.IsAccessible(planet, AnotherGalaxy), "tech to another galaxy" );
		}
		
		[Test]
		public void IsAccessibleTester_2()
		{
			ruler.addResource("Research", "SectorExploration", 1); ruler.checkDependencies();
			
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, SameSector), "tech to same sector" );
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, AnotherSector), "tech to another sector" );
			Assert.AreEqual(false, Coordinate.IsAccessible(planet, AnotherSystem), "tech to another system" );
			Assert.AreEqual(false, Coordinate.IsAccessible(planet, AnotherGalaxy), "tech to another galaxy" );
			
			Assert.AreEqual(false, Coordinate.IsAccessible(planet, AnotherPrivate), "tech to another private" );
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, Coordinate.First), "tech to home" );
		}
		
		[Test]
		public void IsAccessibleTester_3()
		{
			ruler.Victories = 500;
			ruler.addResource("Research", "SectorExploration", 1); ruler.checkDependencies();
			ruler.addResource("Research", "SystemExploration", 1); ruler.checkDependencies();
			
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, SameSector), "tech to same sector" );
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, AnotherSector), "tech to another sector" );
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, AnotherSystem), "tech to another system" );
			Assert.AreEqual(false, Coordinate.IsAccessible(planet, AnotherGalaxy), "tech to another galaxy" );
		}
		
		[Test]
		public void IsAccessibleTester_4()
		{
			ruler.Victories = 500;
			ruler.addResource("Research", "SectorExploration", 1); ruler.checkDependencies();
			ruler.addResource("Research", "SystemExploration", 1); ruler.checkDependencies();
			ruler.addResource("Research", "GalaxyExploration", 1); ruler.checkDependencies();
			
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, SameSector), "tech to same sector" );
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, AnotherSector), "tech to another sector" );
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, AnotherSystem), "tech to another system" );
			Assert.AreEqual(true, Coordinate.IsAccessible(planet, AnotherGalaxy), "tech to another galaxy" );
		}
		
		#endregion
	};
}

