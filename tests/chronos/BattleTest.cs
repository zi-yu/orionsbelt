using System.Collections;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Interfaces;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class BattleTest {
    
		private Ruler pyro;
		private Ruler pre;
		private Fleet preFleet;
		private Fleet pyroFleet;

		private Planet pyroPlanet;

		[SetUp]
    	public void startTest() {

			pyro = new Ruler(Universe.factories,"Pyro");
			pre = new Ruler(Universe.factories,"Pre");

			Universe.instance.addRulerToUniverse( pyro, "Fire" );
			Universe.instance.addRulerToUniverse( pre, "Florzinha" );

			//Pyro stuff
			Coordinate c = new Coordinate(1,1,1,2);

			Universe.instance.conquerPlanet( new Coordinate(1,1,1,2), "Dominio", Globals.CreateFleetToConquer(pyro) );

			pyroPlanet = Universe.instance.getPlanet( c );

			pyroPlanet.addResource("Building", "StarPort", 1);
			Globals.FleetMovementAndExploration(pyro);
			
			Fleet pyroPlanetFleet = pyroPlanet.getDefenseFleet();

			pyroPlanetFleet.addShip("Rain",3000);//,GetShipResource("Rain"));
			pyroPlanetFleet.addShip("Krill",3000);//,GetShipResource("Krill"));
			pyroPlanetFleet.addShip("Crusader",3000);//,GetShipResource("Crusader"));

			pyroFleet = new Fleet("pyroFleet",pyroPlanet.Coordinate,pyro);
			pyroFleet.addShip("Rain",3000);
			pyroFleet.addShip("Krill",3000);
			pyroFleet.addShip("Crusader",3000);

			//Pre Stuff
			Planet prePlanet = pre.Planets[0];
			prePlanet.addResource("Building", "StarPort", 1);
			Globals.FleetMovementAndExploration(pre);
						
			preFleet = new Fleet("preFleet",prePlanet.Coordinate,prePlanet);
			prePlanet.addFleet( preFleet );

			preFleet.addShip("Rain",3000);//,GetShipResource("Rain"));
			preFleet.addShip("Krill",3000);//,GetShipResource("Krill"));
			preFleet.addShip("Crusader",3000);//,GetShipResource("Crusader"));

			preFleet.startMoving( pyroPlanet.Coordinate );

			while( preFleet.IsMoving ) {
				Universe.instance.turn();
			}
    	}

		[Test]
		public void CreateBattle() {
			Universe.instance.CreateBattle( pre, preFleet, pyro, pyroPlanet, BattleType.BATTLE, "totalannihilation" );
			
			ICollection battle = pre.GetAllBattles( BattleType.BATTLE );
			Assert.AreEqual( battle.Count, 1, "O Ruler pre devia ter uma batalha" );

			battle = pyro.GetAllBattles( BattleType.BATTLE );
			Assert.AreEqual( battle.Count, 1, "O Ruler pyro devia ter uma batalha" );

			SimpleBattleInfo[] array = new SimpleBattleInfo[battle.Count];
			battle.CopyTo( array, 0 );

			SimpleBattleInfo battleInfo =  array[0];

			Assert.IsNotNull( battleInfo, "O SimpleBattleInfo no devia ser null" );

			IBattle ibattle= Universe.instance.GetIBattle( pyro.Id, battleInfo.BattleId, BattleType.BATTLE );
			Assert.AreEqual( ibattle, pyroPlanet, "Os objectos deviam ser o mesmo" );

			ibattle= Universe.instance.GetIBattle( pre.Id, battleInfo.BattleId, BattleType.BATTLE );
			Assert.AreEqual( ibattle, preFleet, "Os objectos deviam ser o mesmo" );
		}

		private int GetPositionSpan() {
			int daySpan = 86400000;
			int days = 1;
			return (int) ( ( days * daySpan ) / Universe.instance.TurnTime );
		}
    	
		[Test]
		public void CreateTournament() {
			int battleId = Universe.instance.CreateBattle( pre, preFleet, pyro, pyroPlanet, BattleType.TOURNAMENT, "totalannihilation" );

			SimpleBattleInfo battleInfo;
			
			ICollection battle = pre.GetAllBattles( BattleType.TOURNAMENT );
			Assert.AreEqual( battle.Count, 1, "O Ruler pre devia ter uma batalha" );
			
			battleInfo = pre.GetBattle(battleId,BattleType.TOURNAMENT);
			Assert.IsNotNull( battleInfo, "O SimpleBattleInfo nao devia ser null" );
			Assert.IsTrue( battleInfo.IsTurn, "Devia ser a vez do ruler pyro" );

			battle = pyro.GetAllBattles( BattleType.TOURNAMENT );
			Assert.AreEqual( battle.Count, 1, "O Ruler pyro devia ter uma batalha" );
			
			battleInfo = pyro.GetBattle(battleId,BattleType.TOURNAMENT);
			Assert.IsNotNull( battleInfo, "O SimpleBattleInfo nao devia ser null" );
			Assert.IsFalse( battleInfo.IsTurn, "Não devia ser a vez do ruler pyro" );

			IBattle ibattle= Universe.instance.GetIBattle( pyro.Id, battleInfo.BattleId, BattleType.TOURNAMENT );
			Assert.AreEqual( ibattle, pyroPlanet, "Os objectos deviam ser o mesmo" );

			ibattle= Universe.instance.GetIBattle( pre.Id, battleInfo.BattleId, BattleType.TOURNAMENT );
			Assert.AreEqual( ibattle, preFleet, "Os objectos deviam ser o mesmo" );
		}

		[Test]
		public void Positioning() {
			int battleId = Universe.instance.CreateBattle( pre, preFleet, pyro, pyroPlanet, BattleType.BATTLE, "totalannihilation" );
			
			SimpleBattleInfo battleInfo = pre.GetBattle(battleId,BattleType.BATTLE);
			SimpleBattleInfo battleInfo2 = pyro.GetBattle(battleId,BattleType.BATTLE);

			int count = 0;
			while( battleInfo.IsPositionTime || battleInfo2.IsPositionTime ) {
				++count;	
				if( battleInfo.IsPositionTime ) {
					battleInfo.Turn( );
				}
				if( battleInfo2.IsPositionTime ) {
					battleInfo2.Turn( );
				}
			}

			BattleInfo info = Universe.instance.GetBattle(battleId);
			
			//Assert.AreEqual( count, GetPositionSpan(), "Numero de turnos de posicao errados" );
			Assert.AreEqual( false, battleInfo.IsPositionTime, "Numero de turnos de posicao errados" );
			Assert.AreEqual( false, battleInfo2.IsPositionTime, "Numero de turnos de posicao errados" );

			Assert.AreEqual( true, info.RBI1.HasUnits, "O Tabuleiro devia ter naves!" );
			Assert.AreEqual( true, info.RBI2.HasUnits, "O Tabuleiro devia ter naves!" );
		}


		[Test]
		public void MovesCheck() {
			int id = Universe.instance.CreateBattle( pre, preFleet, pyro, pyroPlanet, BattleType.BATTLE, "totalannihilation" );

			BattleInfo b = Universe.instance.GetBattle( id );

			string moves = "move:Rain-8_1-3000;move:Krill-8_2-3000;move:Crusader-8_3-3000;";

			Result r = b.MakePositioning( moves, pyro);
			
			Assert.IsTrue( r.Ok, "Não devia haver erros" );
		}

		[Test]
		public void TestFirstPlayerToPlayAutomatic() {
			int battleId = Universe.instance.CreateBattle( pre, preFleet, pyro, pyroPlanet, BattleType.TOURNAMENT, "totalannihilation" );
			
			SimpleBattleInfo battleInfo = pre.GetBattle(battleId,BattleType.TOURNAMENT);
			SimpleBattleInfo battleInfo2 = pyro.GetBattle(battleId,BattleType.TOURNAMENT);

			int count = 0;
			while( battleInfo.IsPositionTime || battleInfo2.IsPositionTime ) {
				++count;	
				if( battleInfo.IsPositionTime ) {
					battleInfo.Turn( );
				}
				if( battleInfo2.IsPositionTime ) {
					battleInfo2.Turn( );
				}
			}

			BattleInfo info = Universe.instance.GetBattle(battleId);


			Assert.IsFalse( battleInfo.IsPositionTime, "O Posicionamento ja devia ter sido feito" );
			Assert.IsFalse( battleInfo2.IsPositionTime, "O Posicionamento ja devia ter sido feito" );

			Assert.IsTrue( info.RBI1.HasUnits, "O Tabuleiro devia ter naves!" );
			Assert.IsTrue( info.RBI2.HasUnits, "O Tabuleiro devia ter naves!" );

			Assert.IsTrue( battleInfo.IsTurn, "Devia ser a vez do ruler Pre jogar" );
			Assert.IsFalse( battleInfo2.IsTurn , "Não devia ser a vez do ruler Pyro jogar" );
		}

		private void ManualTest( Ruler ruler1, Ruler ruler2, Fleet ruler1Fleet, Fleet ruler2Fleet) {
			int battleId = Universe.instance.CreateBattle( ruler1, ruler1Fleet, ruler2, ruler2Fleet, BattleType.TOURNAMENT, "totalannihilation" );
			
			SimpleBattleInfo battleInfo = ruler1.GetBattle(battleId,BattleType.TOURNAMENT);
			SimpleBattleInfo battleInfo2 = ruler2.GetBattle(battleId,BattleType.TOURNAMENT);

			BattleInfo info = Universe.instance.GetBattle(battleId);

			Result r = info.MakePositioning("move:Crusader-8_1-3000;move:Rain-8_2-3000;move:Krill-8_3-3000",ruler2);
			Assert.IsTrue( r.Ok, "No devia haver erros" );

			r = info.MakePositioning("move:Crusader-8_1-3000;move:Rain-8_2-3000;move:Krill-8_3-3000",ruler1);
			Assert.IsTrue( r.Ok, "No devia haver erros" );

			Assert.IsFalse( battleInfo.IsPositionTime, "O Posicionamento ja devia ter sido feito" );
			Assert.IsFalse( battleInfo2.IsPositionTime, "O Posicionamento ja devia ter sido feito" );

			Assert.IsTrue( info.RBI1.HasUnits, "O Tabuleiro devia ter naves!" );
			Assert.IsTrue( info.RBI2.HasUnits, "O Tabuleiro devia ter naves!" );

			Assert.IsTrue( battleInfo.IsTurn, "Devia ser a vez do ruler ruler1 jogar" );
			Assert.IsFalse( battleInfo2.IsTurn , "Não devia ser a vez do ruler ruler2 jogar" );
		}

		[Test]
		public void TestFirstPlayerToPlayManual() {
			ManualTest(pre,pyro,preFleet,pyroFleet);
		}

		[Test]
		public void TestFirstPlayerToPlayManual2() {
			ManualTest(pyro,pre,pyroFleet,preFleet);
		}

   	};

}
