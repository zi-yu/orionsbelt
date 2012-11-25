using System.Collections;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Interfaces;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class EndBattle {

		private Ruler pyro;
		private Ruler pre;
		private Fleet preFleet;

		Planet pyroPlanet;

		/*private Resource GetShipResource( string ship ) {
			return Universe.getFactory("planet", "Unit", ship).create( );
		}*/

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
			pyro.Victories = 500;
			pyro.addResource("Research", "AdvancedFlightI", 1);pyro.turn();
			pyro.addResource("Research", "AdvancedFlightII", 1);pyro.turn();
			pyro.addResource("Research", "AdvancedFlightIII", 1);pyro.turn();
			
			Fleet pyroPlanetFleet = pyroPlanet.getDefenseFleet();

			if( pyroPlanetFleet.HasShips ) {
				pyroPlanetFleet.Ships.Clear();
			}
			
			pyroPlanetFleet.addShip("Rain",3000);//,GetShipResource("Rain"));
			pyroPlanetFleet.addShip("Krill",3000);//,GetShipResource("Krill"));
			pyroPlanetFleet.addShip("Crusader",3000);//,GetShipResource("Crusader"));

			//Pre Stuff
			Planet prePlanet = pre.Planets[0];
			prePlanet.addResource("Building", "StarPort", 1);
			pre.Victories = 500;
			pre.addResource("Research", "AdvancedFlightI", 1);pre.turn();
			pre.addResource("Research", "AdvancedFlightII", 1);pre.turn();
			pre.addResource("Research", "AdvancedFlightIII", 1);pre.turn();
			pre.addResource("Research", "PlanetExploration", 1);pre.turn();
			pre.addResource("Research", "SectorExploration", 1);pre.turn();
						
			preFleet = new Fleet("preFleet",prePlanet.Coordinate,prePlanet);
			prePlanet.addFleet( preFleet );

			preFleet.addShip("Rain",3000);//,GetShipResource("Rain"));
			preFleet.addShip("Krill",3000);//,GetShipResource("Krill"));
			preFleet.addShip("Crusader",3000);//,GetShipResource("Crusader"));
			preFleet.addShip("ColonyShip",1);//,GetShipResource("Crusader"));

			preFleet.startMoving( pyroPlanet.Coordinate );

			while( preFleet.IsMoving ) {
				Universe.instance.turn();
			}
		}

		private int CreateBattle( BattleType type, string battleMode ) {
			BattleInfo info;
			SimpleBattleInfo battleInfo;
			
			int id = InitialBattleConstructor(type, battleMode, out battleInfo, out info);

			Result r = info.MakePositioning("move:Crusader-8_1-3000;move:Rain-8_2-3000;move:Krill-8_3-3000;move:ColonyShip-8_4-1",pre);
			Assert.IsTrue( r.Ok, "No devia haver erros" );

			r = info.MakePositioning("move:Crusader-8_1-3000;move:Rain-8_2-3000;move:Krill-8_3-3000;",pyro);
			Assert.IsTrue( r.Ok, "No devia haver erros" );
			
			Assert.IsFalse( battleInfo.EnemyIsPositionTime, "EnemyIsPositionTime (pre) deveria ser false" );
			Assert.IsFalse( battleInfo.IsPositionTime, "PositionTime (pyro) deveria ser false" );
		
			return id;
		}

		private int CreateRegicide( BattleType type, string battleMode ) {
			BattleInfo info;
			SimpleBattleInfo battleInfo;
			
			int id = InitialBattleConstructor(type, battleMode, out battleInfo, out info);

			Result r = info.MakePositioning("move:Crusader-8_1-3000;move:Rain-8_2-3000;move:Krill-8_3-3000;move:FlagShip-8_4-1;move:ColonyShip-8_5-1",pre);
			Assert.IsTrue( r.Ok, "No devia haver erros" );

			r = info.MakePositioning("move:Crusader-8_1-3000;move:Rain-8_2-3000;move:Krill-8_3-3000;move:FlagShip-8_4-1",pyro);
			Assert.IsTrue( r.Ok, "No devia haver erros" );
			
			Assert.IsFalse( battleInfo.EnemyIsPositionTime, "EnemyIsPositionTime (pre) deveria ser false" );
			Assert.IsFalse( battleInfo.IsPositionTime, "PositionTime (pyro) deveria ser false" );
			
			return id;
		}

		private int InitialBattleConstructor(BattleType type, string battleMode, out SimpleBattleInfo battleInfo, out BattleInfo info) {
			int id = Universe.instance.CreateBattle( pre, preFleet, pyro, pyroPlanet, type, battleMode );
	
			Assert.IsTrue(preFleet.IsInBattle,"preFleet devia estar em batalha");
			Assert.IsTrue(pyroPlanet.IsInBattle,"pyroPlanet devia estar em batalha");
	
			ICollection battle = pre.GetAllBattles( type );
			Assert.AreEqual( battle.Count, 1, "O Ruler pre devia ter uma batalha" );
	
			battle = pyro.GetAllBattles( type );
			Assert.AreEqual( battle.Count, 1, "O Ruler pyro devia ter uma batalha" );
	
			SimpleBattleInfo[] array = new SimpleBattleInfo[battle.Count];
			battle.CopyTo( array, 0 );

			battleInfo = array[0];

			Assert.IsNotNull( battleInfo, "O SimpleBattleInfo no devia ser null" );
	
			IBattle ibattle= Universe.instance.GetIBattle( pyro.Id, battleInfo.BattleId, BattleType.BATTLE );
			Assert.AreEqual( ibattle, pyroPlanet, "Os objectos deviam ser o mesmo" );
	
			ibattle= Universe.instance.GetIBattle( pre.Id, battleInfo.BattleId, BattleType.BATTLE );
			Assert.AreEqual( ibattle, preFleet, "Os objectos deviam ser o mesmo" );

			info = Universe.instance.GetBattle( battleInfo.BattleId );
			return id;
		}

		[Test]
		public void BattleTotalAnnihilationFleetWins() {
			int id = CreateBattle( BattleType.BATTLE,"totalannihilation");

			BattleInfo info = Universe.instance.GetBattle(id);
			RulerBattleInfo rbpyro = info.GetRulerBattleInfo(pyro);

			rbpyro.BattleField.Clear();
			
			Assert.IsFalse(rbpyro.HasUnits,"O planeta PyroPlanet no devia ter naves");
            Assert.IsTrue(info.EndBattleBase.HasEnded(),"A batalha j devia ter terminado");
			
			info.EndBattleBase.EndBattle();

			Assert.IsNotNull(pyroPlanet.WonABattle,"A propriedade WonABattle no devia estar a null");
			Assert.IsNull(pyroPlanet.Owner,"A propriedade Owner devia estar a null");
			Assert.IsNotNull(pyroPlanet.OldOwner,"A propriedade OldOwner no devia estar a null");

			Assert.IsFalse(preFleet.IsInBattle,"A fleet ja nao devia estar em batalha");
			Assert.IsFalse(pyroPlanet.IsInBattle,"A fleet ja nao devia estar em batalha");

			Assert.AreEqual(pre.Victories,501,"Pre devia ter mais uma vitria");
			Assert.AreEqual(pyro.Defeats,1,"Pyro devia ter uma Derrota");

			Assert.AreEqual(pyroPlanet.Fleets.Count,2,"PyroPlanet devia ter 2 Fleets");


		}

		[Test]
		public void BattleTotalAnnihilationPlanetWins() {
			int id = CreateBattle( BattleType.BATTLE,"totalannihilation");

			BattleInfo info = Universe.instance.GetBattle(id);
			RulerBattleInfo rbpre = info.GetRulerBattleInfo(pre);

			rbpre.BattleField.Clear();
			
			Assert.IsFalse(rbpre.HasUnits,"A Fleet PreFleet no devia ter naves");
			Assert.IsTrue(info.EndBattleBase.HasEnded(),"A batalha j devia ter terminado");
			
			info.EndBattleBase.EndBattle();

			Assert.IsNull(pyroPlanet.WonABattle,"A propriedade WonABattle devia estar a null");
			Assert.IsNotNull(pyroPlanet.Owner,"A propriedade Owner no devia estar a null");
			Assert.IsNull(pyroPlanet.OldOwner,"A propriedade OldOwner devia estar a null");

			Assert.IsFalse(preFleet.IsInBattle,"A fleet j no devia estar em batalha");
			Assert.IsFalse(pyroPlanet.IsInBattle,"A fleet j no devia estar em batalha");

			Assert.AreEqual(pyro.Victories,501,"Pyro devia ter mais uma vitria");
			Assert.AreEqual(pre.Defeats,1,"Pre devia ter uma Derrota");
		}

		[Test]
		public void BattleTotalAnnihilationForcePlanetWin() {
			int id = CreateBattle( BattleType.BATTLE,"totalannihilation");

			BattleInfo info = Universe.instance.GetBattle(id);
			RulerBattleInfo rbpre = info.GetRulerBattleInfo(pre);
			RulerBattleInfo rbpyro = info.GetRulerBattleInfo(pyro);

			info.ForceEndBattle(pre);

			Assert.AreEqual(0,rbpyro.UnitsDestroyed["animal"],"Nao devia haver animais");
			Assert.AreEqual(3000,rbpyro.UnitsDestroyed["light"],"Devia haver light destruidas");
			Assert.AreEqual(3000,rbpyro.UnitsDestroyed["heavy"],"Devia haver heavy destruidas");
			Assert.AreEqual(3000,rbpyro.UnitsDestroyed["medium"],"Devia haver medium destruidas");

			Assert.AreEqual(0,rbpre.UnitsDestroyed["animal"],"Nao devia haver animais");
			Assert.AreEqual(0,rbpre.UnitsDestroyed["light"],"Nao Devia haver light destruidas");
			Assert.AreEqual(0,rbpre.UnitsDestroyed["heavy"],"Nao Devia haver heavy destruidas");
			Assert.AreEqual(0,rbpre.UnitsDestroyed["medium"],"Nao Devia haver medium destruidas");
			Assert.AreEqual(0,rbpre.UnitsDestroyed["special"],"Nao Devia haver special destruidas");

			Assert.IsFalse(rbpre.HasUnits,"A Fleet PreFleet no devia ter naves");
			Assert.IsTrue(info.EndBattleBase.HasEnded(),"A batalha j devia ter terminado");
			
			Assert.IsNull(pyroPlanet.WonABattle,"A propriedade WonABattle devia estar a null");
			Assert.IsNotNull(pyroPlanet.Owner,"A propriedade Owner no devia estar a null");
			Assert.IsNull(pyroPlanet.OldOwner,"A propriedade OldOwner devia estar a null");

			Assert.IsFalse(preFleet.IsInBattle,"A fleet j no devia estar em batalha");
			Assert.IsFalse(pyroPlanet.IsInBattle,"A fleet j no devia estar em batalha");

			Assert.AreEqual(pyro.Victories,501,"Pyro devia ter mais uma vitria");
			Assert.AreEqual(pre.Defeats,1,"Pre devia ter uma Derrota");
		}

		[Test]
		public void BattleRegicideForcePlanetWin() {
			int id = CreateRegicide( BattleType.FRIENDLY,"regicide");

			BattleInfo info = Universe.instance.GetBattle(id);
			RulerBattleInfo rbpre = info.GetRulerBattleInfo(pre);
			RulerBattleInfo rbpyro = info.GetRulerBattleInfo(pyro);

			info.ForceEndBattle(pre);

			Assert.AreEqual(0,rbpyro.UnitsDestroyed["animal"],"Nao devia haver animais");
			Assert.AreEqual(3000,rbpyro.UnitsDestroyed["light"],"Devia haver light destruidas");
			Assert.AreEqual(3000,rbpyro.UnitsDestroyed["heavy"],"Devia haver heavy destruidas");
			Assert.AreEqual(3000,rbpyro.UnitsDestroyed["medium"],"Devia haver medium destruidas");
			Assert.AreEqual(2,rbpyro.UnitsDestroyed["special"],"Devia haver special destruidas");

			Assert.AreEqual(0,rbpre.UnitsDestroyed["animal"],"Nao devia haver animais");
			Assert.AreEqual(0,rbpre.UnitsDestroyed["light"],"Nao Devia haver light destruidas");
			Assert.AreEqual(0,rbpre.UnitsDestroyed["heavy"],"Nao Devia haver heavy destruidas");
			Assert.AreEqual(0,rbpre.UnitsDestroyed["medium"],"Nao Devia haver medium destruidas");
			Assert.AreEqual(0,rbpre.UnitsDestroyed["special"],"Nao Devia haver special destruidas");

			Assert.IsFalse(rbpre.HasUnits,"A Fleet PreFleet no devia ter naves");
			Assert.IsTrue(info.EndBattleBase.HasEnded(),"A batalha ja devia ter terminado");
		}

		[Test]
		public void ScoreTest() {
			int id = CreateBattle( BattleType.BATTLE,"totalannihilation");
			
			BattleInfo info = Universe.instance.GetBattle(id);
			
			RulerBattleInfo rbpre = info.GetRulerBattleInfo(pre);
			RulerBattleInfo rbpyro = info.GetRulerBattleInfo(pyro);

			pre.Rank = 1;
			pyro.Rank = 20;

			rbpyro.AddUnitsDestroyed(1000,"heavy");
			rbpyro.AddUnitsDestroyed(1000,"medium");
			rbpyro.AddUnitsDestroyed(1000,"light");

			rbpre.AddUnitsDestroyed(20,"heavy");
			rbpre.AddUnitsDestroyed(10,"medium");
			rbpre.AddUnitsDestroyed(11,"light");

			Assert.IsTrue( BattleInfo.GetWonScore(rbpyro,rbpre,pre) <= 5000, "O WonScore devia ser menor que 5000");
			
			Assert.IsTrue( BattleInfo.GetLostScore(rbpyro,pre,pyro) >= -5000, "O LostScore devia ser maior que -5000");
			
			Assert.IsTrue( pre.Score + BattleInfo.GetLostScore(rbpyro,pre,pyro) >= 0 , "O LostScore no deve trazer o score para valores menores que 0");
		}
	};

}
