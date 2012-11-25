using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Resources;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class TeletransportationTester {
		
		#region Setup
		
		private Planet planet;
		private Planet planet2;
		private Planet planet3;
		private Ruler ruler;
		
		private void setPlanet(Planet planet)
		{
			planet.addResource("Building", "Gate", 1);
			planet.addResource("Building", "StarGate", 1);
		}
		
		[SetUp]
		public void init()
		{
			ruler = new Ruler("PRE");
			planet = new Planet(ruler, Globals.factories, "Planet", Coordinate.First);

			Fleet f = new Fleet("Fleet",Coordinate.First,ruler);
			f.addShip("ColonyShip", 1);//, null);
			
			Globals.ToStarGate(ruler, planet);
			Globals.ToStarGate(ruler, ruler.HomePlanet);
			
			planet.checkDependencies();
			
			Coordinate first = Coordinate.translateCoordinate("1:1:1:2");
			Coordinate second = Coordinate.translateCoordinate("1:1:1:3");
			
			Universe.instance.conquerPlanet(first, "buu", Globals.CreateFleetToConquer(ruler));
			Universe.instance.conquerPlanet(second, "buu2", Globals.CreateFleetToConquer(ruler));
			
			planet2 = (Planet) Universe.instance.planets[first];
			planet3 = (Planet) Universe.instance.planets[second];
			
			Globals.ToStarGate(ruler, planet2);
			Globals.ToStarGate(ruler, planet3);
			
			setPlanet(planet);
			setPlanet(planet2);
		}
		
		#endregion
		
		#region Teletransportation Tests
		
		[Test]
		public void IsTeletransportableTest()
		{
			Assert.AreEqual(true, Resource.IsTeletransportable("food"), "food");
			Assert.AreEqual(true, Resource.IsTeletransportable("mp"), "mp");
			Assert.AreEqual(true, Resource.IsTeletransportable("gold"), "gold");
			Assert.AreEqual(true, Resource.IsTeletransportable("labor"), "labor");
			Assert.AreEqual(true, Resource.IsTeletransportable("spy"), "spy");
			Assert.AreEqual(true, Resource.IsTeletransportable("marine"), "marine");
															   
			Assert.AreEqual(false, Resource.IsTeletransportable("energy"), "energy");
			Assert.AreEqual(false, Resource.IsTeletransportable("groundSpace"), "groundSpace");
			Assert.AreEqual(false, Resource.IsTeletransportable("waterSpace"), "waterSpace");
			Assert.AreEqual(false, Resource.IsTeletransportable("orbitSpace"), "orbitSpace");
			Assert.AreEqual(false, Resource.IsTeletransportable("culture"), "culture");
			Assert.AreEqual(false, Resource.IsTeletransportable("housing"), "housing");
			
			foreach( ResourceFactory factory in Universe.getFactories("planet", "Rare" ).Values ) {
				Assert.AreEqual(true, Resource.IsTeletransportable(factory.create()), factory.Name);	
			}
		}
		
		[Test]
		public void TeletransportationCostTest()
		{
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "food", 1) > 0);
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "mp", 1) > 0);
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "gold", 1) > 0);
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "labor", 1) > 0);
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "energy", 1) > 0);
			
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "groundSpace", 1) == 0);
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "waterSpace", 1) == 0);
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "orbitSpace", 1) == 0);
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "culture", 1) == 0);
			Assert.IsTrue( Resource.TeletransportationCost("Intrinsic", "housing", 1) == 0);
		}
		
		[Test]
		public void TestAvailability()
		{
			Assert.AreEqual(true, ruler.HomePlanet.CanTeletransportIntrinsic, "#1");
			Assert.AreEqual(true, ruler.HomePlanet.CanTeletransportFleets, "#2");
			
			Assert.AreEqual(true, planet2.CanTeletransportIntrinsic, "#3");
			Assert.AreEqual(true, planet2.CanTeletransportFleets, "#4");
			
			Assert.AreEqual(false, planet3.CanTeletransportIntrinsic, "#5");
			Assert.AreEqual(false, planet3.CanTeletransportFleets, "#6");
		}
		
		[Test]
		public void TestTargetPlanetsFromPlanets()
		{
			Assert.AreEqual(2, ruler.IntrinsicTeletransportationPlanets.Length, "#1");
			Assert.AreEqual(2, ruler.FleetTeletransportationPlanets.Length, "#2");
			
			Assert.AreEqual(1, ruler.HomePlanet.IntrinsicTeletransportationPlanets.Length, "#3");
			Assert.AreEqual(1, ruler.HomePlanet.FleetTeletransportationPlanets.Length, "#4");
			Assert.AreEqual(planet2.Id, ruler.HomePlanet.IntrinsicTeletransportationPlanets[0].Id, "#5");
			Assert.AreEqual(planet2.Id, ruler.HomePlanet.FleetTeletransportationPlanets[0].Id, "#6");
		}
		
		[Test]
		public void TestCanMove()
		{
			Result res = null;
			
			ruler.HomePlanet.addResource("energy",50000);
			
			res = ruler.HomePlanet.CanMoveIntrinsic(planet2, "mp", 10);
			Assert.IsTrue( res.Ok, res.log() );
			
			res = ruler.HomePlanet.CanMoveIntrinsic(planet3, "mp", 10);
			Assert.IsFalse( res.Ok, res.log() );
			
			res = ruler.HomePlanet.CanMoveIntrinsic(planet2, "culture", 10);
			Assert.IsFalse( res.Ok, res.log() );
		}
		
		[Test]
		public void TestCanMoveLabor()
		{
			Result res = null;
			
			ruler.HomePlanet.addResource("energy",50000);
			int freeSpace = planet2.Housing - planet2.Population;
			ruler.HomePlanet.addResource("labor",freeSpace+100);
			
			res = ruler.HomePlanet.CanMoveIntrinsic(planet2, "labor", freeSpace + 1);
			Assert.IsFalse( res.Ok, res.log() );
		}
		
		[Test]
		public void TestMove_Intrinsic()
		{
			int startEnergy = ruler.HomePlanet.Energy;
			int startMP = ruler.HomePlanet.MP;
			int destinyMP = planet2.MP;
			int toMove = 10;
			
			Result res = ruler.HomePlanet.CanMoveIntrinsic(planet2, "mp", toMove);
			Assert.IsTrue( res.Ok, res.log() );
			
			ruler.HomePlanet.MoveIntrinsic(planet2, "mp", toMove);
			
			Assert.AreEqual( startEnergy - Resource.TeletransportationCost("Intrinsic", "mp", toMove), ruler.HomePlanet.Energy);
			Assert.AreEqual( startMP - toMove, ruler.HomePlanet.MP );
			Assert.AreEqual( destinyMP + toMove, planet2.MP );
		}
		
		[Test]
		public void TestMove_Fleet()
		{
			ruler.HomePlanet.addFleet("fleetToMove");
			Fleet fleet = ruler.HomePlanet.getFleet("fleetToMove");
			
			//Resource resource = Universe.getFactory("planet", "Unit", "Rain").create();
			fleet.addShip("Rain", 10);//, resource );
			
			int cost = planet.FleetMoveCost(fleet);
			Assert.AreEqual( 10, cost, "Bad Cost");
			fleet.addShip("Kamikaze", 1);//, Universe.getFactory("planet", "Unit", "Kamikaze").create());
			cost = planet.FleetMoveCost(fleet);
			Assert.AreEqual( 13, cost, "Bad Cost");
			ruler.HomePlanet.addResource("energy", cost);
			int energy = ruler.HomePlanet.Energy;
			
			Result result = ruler.HomePlanet.CanMoveFleet(planet2, "fleetToMove");
			Assert.AreEqual( true, result.Ok, result.log() );
			Assert.AreEqual( 3, ruler.HomePlanet.Fleets.Count, "where is fleetToMove?");
			
			ruler.HomePlanet.MoveFleet(planet2, "fleetToMove");
			Assert.AreEqual( energy - cost, ruler.HomePlanet.Energy );
			Assert.AreEqual( 2, ruler.HomePlanet.Fleets.Count, "Fleet still in planet");
			Assert.AreEqual( 3, planet2.Fleets.Count, "Fleet not moved");
		}
		
		[Test]
		public void TestTargetPlanetsFromRuler()
		{
			Assert.AreEqual(2, ruler.IntrinsicTeletransportationPlanets.Length, "#1");
			Assert.AreEqual(2, ruler.FleetTeletransportationPlanets.Length, "#2");
			
			Assert.AreEqual(false, planet3.CanTeletransportIntrinsic, "#3");
			planet3.addResource("Building", "Gate", 1);
			Assert.AreEqual(true, planet3.CanTeletransportIntrinsic, "#4");
			Assert.AreEqual(3, ruler.IntrinsicTeletransportationPlanets.Length, "#5");
			Assert.AreEqual(2, ruler.FleetTeletransportationPlanets.Length, "#6");
			
			Assert.AreEqual(false, planet3.CanTeletransportFleets, "#7");
			planet3.addResource("Building", "StarGate", 1);
			Assert.AreEqual(true, planet3.CanTeletransportFleets, "#8");
			Assert.AreEqual(3, ruler.IntrinsicTeletransportationPlanets.Length, "#9");
			Assert.AreEqual(3, ruler.FleetTeletransportationPlanets.Length, "#10");
		}
		
		#endregion
		
		#region Bugs
		
		[Test]
		public void Bug_1190390()
		{
			ruler.HomePlanet.addFleet("buu_______");
			Fleet fleet = ruler.HomePlanet.getFleet("buu_______");
			
			Resource resource = Universe.getFactory("planet", "Unit", "Rain").create();
			fleet.addShip("Krill", 10);//, resource );
			fleet.addShip("Rain", 10);//, resource );
			fleet.removeShip("Rain", 10 );
			
			Assert.IsTrue(ruler.HomePlanet.FleetMoveCost(fleet) > 0);
		}
		
		#endregion
	
	};
}

