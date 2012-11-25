
//#define DEBUG_TRAVEL_TIME

using System;
using Chronos.Core;
using Chronos.Exceptions;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class FleetTester {
		
		private Fleet fleet;
		Planet pyroPlanet;

	//	private Resource res = null;

		[SetUp]
		public void init() {
			//verificar de o universe  singleton
			Universe universe = Universe.instance;
			Assert.IsNotNull(universe,"Universe not set to an instance of an object");
			Assert.AreEqual(Universe.instance,universe,"Instance of the universe is not equal");
			
			universe.init();
			
			//verificar as cenas l dentro
			Assert.IsNotNull(Universe.factories,"factories shouldn't be null @ Universe");
			Assert.IsNotNull(universe.alliances,"alliances shouldn't be null @ Universe");
			Assert.AreEqual(1,universe.alliances.Count,"alliances should have one alliance" );

			//Pyro stuff
			Ruler pyro = new Ruler(Universe.factories,"Pyro");
			Coordinate c = new Coordinate(1,1,1,2);

			Universe.instance.conquerPlanet( new Coordinate(1,1,1,2), "Dominio", Globals.CreateFleetToConquer(pyro) );

			pyroPlanet = Universe.instance.getPlanet( c );

			pyroPlanet.addResource("Building", "StarPort", 1);
			Globals.FleetMovementAndExploration(pyro);
			
			fleet = pyroPlanet.getDefenseFleet();


		}

		[Test]
		public void removeAllShips() {
			fleet.addShip("Crusader",10);//,GetShipResource("Crusader"));

			//Assert.AreEqual(1, fleet.ShipsResources.Count, "Fleet Devia ter um Ship Resource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );

			fleet.removeAllShips();
			Assert.AreEqual(0, fleet.TotalUnits, "Fleet no devia ter naves" );
			//Assert.AreEqual(0, fleet.ShipsResources.Count, "Fleet no devia ter um ShipResource" );
		}

		[Test]
		public void RemoveShipType() {
			fleet.addShip("Crusader",10);//,GetShipResource("Crusader"));

			//Assert.AreEqual(1, fleet.ShipsResources.Count, "Fleet Devia ter um Ship Resource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );

			fleet.addShip("Rain",10);//,GetShipResource("Rain"));

			//Assert.AreEqual(2, fleet.ShipsResources.Count, "Fleet Devia ter 2 Ship Resource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );

			fleet.removeShip("Crusader");

			Assert.AreEqual(10, fleet.TotalUnits, "Fleet devia ter 10 naves" );
			Assert.AreEqual(1, fleet.Ships.Count, "Fleet devia ter um conjunto naves" );
			//Assert.AreEqual(1, fleet.ShipsResources.Count, "Fleet devia ter um ShipResource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );
		}

		[Test]
		public void RemoveSomeShips() {
			fleet.addShip("Crusader",10);//,GetShipResource("Crusader"));

			//Assert.AreEqual(1, fleet.ShipsResources.Count, "Fleet Devia ter um Ship Resource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );

			fleet.addShip("Rain",10);//,GetShipResource("Rain"));

			//Assert.AreEqual(2, fleet.ShipsResources.Count, "Fleet Devia ter 2 Ship Resource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );

			fleet.removeShip("Crusader",5);
			fleet.removeShip("Rain",5);

			Assert.AreEqual(10, fleet.TotalUnits, "Fleet devia ter 15 naves" );
			Assert.AreEqual(2, fleet.Ships.Count, "Fleet devia ter 2 conjuntos naves" );
			//Assert.AreEqual(2, fleet.ShipsResources.Count, "Fleet devia ter 2 ShipResource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );
		}

		[Test]
		public void AddShip() {
			//Assert.AreEqual(0, fleet.ShipsResources.Count, "Fleet no devia ter o ShipResource da Crusader" );			
			
			fleet.addShip("Crusader",10);//,GetShipResource("Crusader"));

			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );
			//Assert.AreEqual( 1, fleet.ShipsResources.Count, "Fleet devia ter 1 ShipResource" );

			Assert.AreEqual( 1, fleet.Ships.Count, "Devia ter 1 conjunto de naves Crusader" );
			Assert.AreEqual( 10, fleet.TotalUnits, "O nmero total de naves  invlido" );
			Assert.AreEqual( 10, (int)fleet.Ships["Crusader"] , "O nmero total de Crusaders devia ser 10" );
		}

		[Test]
		public void SwapShips() {
			Ruler owner = new Ruler( Universe.factories, "Nunos" );
			Coordinate c = Coordinate.First;
			Fleet fleet2 = new Fleet("NunoS's Fleet",c, owner);

			fleet.addShip("Crusader",10);//,GetShipResource("Crusader"));
			fleet.addShip("Rain",10);//,GetShipResource("Rain"));

			//Assert.AreEqual(2, fleet.ShipsResources.Count, "Fleet Devia ter um Ship Resource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );
			//Assert.AreEqual(2, fleet.ShipsResources.Count, "Fleet Devia ter 2 Ship Resource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );

			fleet2.addShip("Crusader",15);//,GetShipResource("Crusader"));
			fleet2.addShip("Rain",5);//,GetShipResource("Rain"));

			//Assert.AreEqual(2, fleet2.ShipsResources.Count, "Fleet Devia ter um Ship Resource" );
			//Assert.AreEqual(true, fleet2.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );
			//Assert.AreEqual(2, fleet2.ShipsResources.Count, "Fleet Devia ter 2 Ship Resource" );
			//Assert.AreEqual(true, fleet2.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );

			// ---------- FALHA ----------------
			Assert.AreEqual( false, fleet.swapShips( fleet2,"Crusader",200 ),"Swap Ships nao devia ser feito");
            Assert.AreEqual(20, fleet.TotalUnits, "Fleet devia ter 20 naves" );
			Assert.AreEqual(20, fleet2.TotalUnits, "Fleet devia ter 20 naves" );

			Assert.AreEqual( false, fleet.swapShips( fleet2,"Krill",200 ),"Swap Ships nao devia ser feito");
            Assert.AreEqual(20, fleet.TotalUnits, "Fleet devia ter 20 naves" );
			Assert.AreEqual(20, fleet2.TotalUnits, "Fleet devia ter 20 naves" );

			// ---------- Sucesso ----------------
			Assert.AreEqual( true, fleet.swapShips( fleet2,"Crusader",5 ),"Swap Ships devia ser feito");
            Assert.AreEqual(25, fleet.TotalUnits, "Fleet devia ter 25 naves" );
			Assert.AreEqual(15, fleet2.TotalUnits, "Fleet devia ter 15 naves" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );
			//Assert.AreEqual(true, fleet2.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );
			//Assert.AreEqual(true, fleet2.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );

			Assert.AreEqual( true, fleet.swapShips( fleet2,"Crusader",10 ),"Swap Ships devia ser feito");
            Assert.AreEqual(35, fleet.TotalUnits, "Fleet devia ter 35 naves" );
			Assert.AreEqual(5, fleet2.TotalUnits, "Fleet devia ter 5 naves" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );

			//Assert.AreEqual(1, fleet2.ShipsResources.Count, "Fleet devia ter 1 ShipResource" );
			//Assert.AreEqual(true, fleet2.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );
			//Assert.AreEqual(false, fleet2.ShipsResources.ContainsKey( "Crusader" ), "Fleet no devia ter o ShipResource da Crusader" );


			//Assert.AreEqual(10, fleet.TotalUnits, "Fleet devia ter 15 naves" );
			//Assert.AreEqual(2, fleet.Ships.Count, "Fleet devia ter 2 conjuntos naves" );
			//Assert.AreEqual(2, fleet.ShipsResources.Count, "Fleet devia ter 2 ShipResource" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Rain" ), "Fleet devia ter o ShipResource da Rain" );
			//Assert.AreEqual(true, fleet.ShipsResources.ContainsKey( "Crusader" ), "Fleet devia ter o ShipResource da Crusader" );
		}

		[Test]
		[ExpectedException(typeof(NoShipsToMoveException))]
		public void MoveFleetFail() {
			Assert.IsFalse( fleet.IsMoving, "Fleet devia estar parada");
			fleet.startMoving( new Coordinate(3,20,20,2) );
		}

		[Test]
		public void moveFleetSucess() {
			Assert.IsFalse( fleet.IsMoving, "Fleet should be stopped!" );
			pyroPlanet.addFleet("Buu");

			Fleet f = pyroPlanet.getFleet("Buu");

			f.addShip("crusader",10);//,res);
			
			f.startMoving( new Coordinate(3,20,20,2) );

			Assert.IsTrue( f.IsMoving, "A fleet devia estar em movimento!" );
			Assert.IsFalse( f.CanBeRemoved, "A fleet não devia poder ser removida");

			int i1 = f.HoursToArrive;
			int i = 0;
			while( f.IsMoving ) {
				f.moveFleet();
				++i;
			}
			Assert.IsTrue( i == i1, "O numero de horas para chegar ao destino nao esta correcto" );
			f.IsInBattle = true;
			f.Ships.Clear();
			Assert.IsFalse( f.CanBeRemoved, "A fleet não devia poder ser removida");

		}


		private int time( Coordinate source, Coordinate destination) {
			int gCount = Math.Abs( destination.Galaxy - source.Galaxy );
			int syCount = Math.Abs( destination.System - source.System );
			int sCount = Math.Abs( destination.Sector - source.Sector );
			int pCount = Math.Abs( destination.Planet - source.Planet );

			int time = gCount*800 + syCount*40 + sCount*2 + (pCount==0?1:pCount);

			return time;
		}
		
		
		[Test]
		public void travelTimeTest() {
#if DEBUG_TRAVEL_TIME
			Coordinate source = new Coordinate(1,1,1,1);
			
			int tAnt = 0;
			for( int g = 1 ; g <= Coordinate.MaximumGalaxies ; ++g ) {
				for( int sy = 1 ; sy <= Coordinate.MaximumSystems ; ++sy ) {
					for( int s = 1 ; s <= Coordinate.MaximumSectors ; ++s ) {
						for( int p = 2 ; p <= Coordinate.MaximumPlanets ; ++p ) {
							Coordinate destiny = new Coordinate(g,sy,s,p);
							//int t = Fleet.TravelTime(source,destiny);
							int t = time(source,destiny);
#if WRITE
							Console.WriteLine("Coordenada:" + destiny.ToString() +" ; Time: " + t);
#endif

							Assert.IsTrue( t > tAnt ,string.Format("T anterior:{0} ; T corrente:{1}",tAnt,t) );
							tAnt = t;
						}
					}
				}
			}
#endif
		}

		private void FleetToCoordinate( Fleet f, Coordinate c ) {
			f.startMoving( c );

			Assert.IsFalse(f.CanBeRemoved,"A fleet não devia poder ser removida");

			while( f.IsMoving ) {
				Universe.instance.turn();
			}
		}

		[Test]
		public void DeleteFleet() {
			fleet.addShip("Rain",3000);//,GetShipResource("Rain"));
			fleet.addShip("Krill",3000);//,GetShipResource("Krill"));
			fleet.addShip("Crusader",3000);//,GetShipResource("Crusader"));

			Ruler pre = new Ruler(Universe.factories,"Pre");
			Universe.instance.addRulerToUniverse( pre, "Florzinha" );

			//Pre Stuff
			Planet prePlanet = pre.Planets[0];
			prePlanet.addResource("Building", "StarPort", 1);
			Globals.FleetMovementAndExploration(pre);

			Fleet preFleet = new Fleet("preFleet",prePlanet.Coordinate,prePlanet);
			prePlanet.addFleet( preFleet );

			preFleet.addShip("Rain",3000);//,GetShipResource("Rain"));
			preFleet.addShip("Krill",3000);//,GetShipResource("Krill"));
			preFleet.addShip("Crusader",3000);//,GetShipResource("Crusader"));
			
			FleetToCoordinate( preFleet, pyroPlanet.Coordinate );

			Assert.IsFalse(preFleet.CanBeRemoved,"A fleet não devia poder ser removida visto estar parada no universe");

			FleetToCoordinate( preFleet, prePlanet.Coordinate );

			Assert.IsTrue(preFleet.CanBeRemoved,"A fleet devia poder ser removida visto estar no planeta");
            
			preFleet.IsInBattle = true;

			Assert.IsFalse(preFleet.CanBeRemoved,"A fleet não devia poder ser removida visto estar em batalha");
		}
	}
}
