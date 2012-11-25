using System.Collections;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Resources;
using DesignPatterns;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class RulerBattleInfoTest {

		private Ruler pyro;
		private Ruler pre;

		private Fleet pyroFleet;
		private Fleet preFleet;

		private BattleInfo info;

		//private static FactoryContainer interpreterFactory = new FactoryContainer( typeof( InterpreterFactory ) );

		[SetUp]
		public void Init() {
			
			pyro = new Ruler( Globals.factories, "pyro" );
			pre = new Ruler( Globals.factories, "pre" );
			
			Universe.instance.addRulerToUniverse( pyro, "Pyro" );
			Universe.instance.addRulerToUniverse( pre, "Pre" );
			
			pyroFleet = new Fleet("Pyro",Coordinate.First,pyro);
			preFleet = new Fleet("Pyro",Coordinate.First,pre);
			
			pyroFleet.addShip("Crusader",10);//,GetShipResource("Crusader"));
			pyroFleet.addShip("Krill",10);//,GetShipResource("Krill"));
			pyroFleet.addShip("Pretorian",10);//,GetShipResource("Pretorian"));

			preFleet.addShip("Crusader",10);//,GetShipResource("Crusader"));
			preFleet.addShip("Krill",10);//,GetShipResource("Krill"));
			preFleet.addShip("Pretorian",10);//,GetShipResource("Pretorian"));

			int id = Universe.instance.CreateBattle( pyro, pyroFleet, pre, preFleet, BattleType.FRIENDLY, "totalannihilation");
			info = Universe.instance.GetBattle( id );
			
		}

		[Test]
		public void ForcePositioningTest() {
            RulerBattleInfo r1 = info.GetRulerBattleInfo( pyro );
			RulerBattleInfo r2 = info.GetRulerBattleInfo( pre );
			r1.ForcePositioning();
			r2.ForcePositioning();
			
			Assert.IsTrue( r1.SectorHasElements( "8_8" ), "O sector 8_1 devia ter elementos" );
			Assert.IsTrue( r1.SectorHasElements( "8_7" ), "O sector 8_2 devia ter elementos" );
			Assert.IsTrue( r1.SectorHasElements( "8_6" ), "O sector 8_3 devia ter elementos" );

			Assert.AreEqual( r1.InitialContainer.Count, 0, "O initialContainer devia estar vazio" );

			Assert.AreEqual( r1.BattleField.Keys.Count, 3, "O BattleField no tem a qauntidade correcta" );
		}

		[Test]
		public void MoveFromSrc() {
			RulerBattleInfo r1 = info.GetRulerBattleInfo( pyro );

			Element e = (Element)r1.InitialContainer[0];
			
			r1.SectorSrcMove( e, "8_2" , 5 );

			Assert.AreEqual( r1.InitialContainer.Count, 3, "O initialContainer devia ter 3 elementos" );

			Assert.IsTrue( r1.SectorHasElements( "8_2" ), "O sector 8_2 devia ter elementos" );
			
			Assert.AreEqual( r1.SectorGetElement( "8_2" ).Quantity, 5, "O sector 8_2 devia ter 5 elementos" );

			r1.SectorSrcMove( e, "8_2" , 5 );

			Assert.AreEqual( r1.SectorGetElement( "8_2" ).Quantity, 10, "O sector 8_2 devia ter 10 elementos" );

			Assert.AreEqual( r1.InitialContainer.Count, 2, "O initialContainer devia ter 2 elementos" );
			Assert.AreEqual( r1.BattleField.Keys.Count, 1, "O BattleField no tem a quantidade correcta" );

			e = (Element)r1.InitialContainer[0];
			r1.SectorSrcMove( e, "8_1" , 10 );

			Assert.AreEqual( r1.InitialContainer.Count, 1, "O initialContainer devia ter 1 elementos" );
			Assert.AreEqual( r1.BattleField.Keys.Count, 2, "O BattleField no tem a quantidade correcta" );

			e = (Element)r1.InitialContainer[0];
			r1.SectorSrcMove( e, "8_3" , 10 );

			Assert.AreEqual( r1.InitialContainer.Count, 0, "O initialContainer devia ter 0 elementos" );
			Assert.AreEqual( r1.BattleField.Keys.Count, 3, "O BattleField no tem a quantidade correcta" );
		}

		[Test]
		public void NormalMove() {
			RulerBattleInfo r1 = info.GetRulerBattleInfo( pyro );
			RulerBattleInfo r2 = info.GetRulerBattleInfo( pre );
			r1.ForcePositioning();
			r2.ForcePositioning();

			Assert.AreEqual( r1.InitialContainer.Count, 0, "O initialContainer devia ter 0 elementos" );

			Element e = r1.SectorGetElement( "8_1" );

			Assert.AreEqual( r1.BattleField.Keys.Count, 3, "O BattleField no tem a quantidade correcta" );
			
			r1.SectorMove( "8_8", "7_8", 5 );
			r1.SectorMove( "8_7", "7_7", 5 );
			r1.SectorMove( "8_6", "7_6", 5 );

			Assert.AreEqual( r1.BattleField.Keys.Count, 6, "O BattleField no tem a quantidade correcta" );

			r1.SectorMove( "8_8", "7_8", 5 );
			r1.SectorMove( "8_7", "7_7", 5 );
			r1.SectorMove( "8_6", "7_6", 5 );

			Assert.AreEqual( r1.BattleField.Keys.Count, 3, "O BattleField no tem a quantidade correcta" );

			Assert.IsTrue( !r1.SectorHasElements( "8_1" ), "O sector 8_1 no devia ter elementos" );
			Assert.IsTrue( !r1.SectorHasElements( "8_2" ), "O sector 8_2 no devia ter elementos" );
			Assert.IsTrue( !r1.SectorHasElements( "8_3" ), "O sector 8_3 no devia ter elementos" );
		}

		[Test]
		public void NextCoordinate() {
			string s = RulerBattleInfo.NextSector( "2_2", "n" );
			Assert.AreEqual( "3_2", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.NextSector( "2_2", "s" );
			Assert.AreEqual( "1_2", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.NextSector( "2_2", "w" );
			Assert.AreEqual( "2_3", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.NextSector( "2_2", "e" );
			Assert.AreEqual( "2_1", s, "A Coordenada não está correcta" );
		}

		[Test]
		public void LeftCoordinate() {
			string s = RulerBattleInfo.LeftSector( "2_2", "n" );
			Assert.AreEqual( "2_3", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.LeftSector( "2_2", "s" );
			Assert.AreEqual( "2_1", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.LeftSector( "2_2", "w" );
			Assert.AreEqual( "1_2", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.LeftSector( "2_2", "e" );
			Assert.AreEqual( "3_2", s, "A Coordenada não está correcta" );
		}

		[Test]
		public void RightCoordinate() {
			string s = RulerBattleInfo.RightSector( "2_2", "n" );
			Assert.AreEqual( "2_1", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.RightSector( "2_2", "s" );
			Assert.AreEqual( "2_3", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.RightSector( "2_2", "w" );
			Assert.AreEqual( "3_2", s, "A Coordenada não está correcta" );

			s = RulerBattleInfo.RightSector( "2_2", "e" );
			Assert.AreEqual( "1_2", s, "A Coordenada não está correcta" );
		}

   	};

}
