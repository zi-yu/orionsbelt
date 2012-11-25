using System.Collections;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Resources;
using DesignPatterns;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class InterpreterTest {

		private Ruler pyro;
		private Ruler pre;

		private Fleet pyroFleet;
		private Fleet preFleet;

		private static FactoryContainer interpreterFactory = new FactoryContainer( typeof( InterpreterFactory ) );

		private Resource GetShipResource( string ship ) {
			return Universe.getFactory("planet", "Unit", ship).create( );
		}

		[SetUp]
		public void Init() {
			
			pyro = new Ruler( Globals.factories, "pyro" );
			pre = new Ruler( Globals.factories, "pre" );
			
			Universe.instance.addRulerToUniverse( pyro, "Pyro" );
			Universe.instance.addRulerToUniverse( pre, "Pre" );
			
			pyroFleet = new Fleet("Pyro",Coordinate.First,pyro);
			preFleet = new Fleet("Pyro",Coordinate.First,pre);
			
			pyroFleet.addShip("Crusader",10);//,GetShipResource("Crusader"));
			pyroFleet.addShip("Crusader",10);//,GetShipResource("Krill"));
			pyroFleet.addShip("Crusader",10);//,GetShipResource("Pretorian"));

			preFleet.addShip("Crusader",10);//,GetShipResource("Crusader"));
			preFleet.addShip("Crusader",10);//,GetShipResource("Krill"));
			preFleet.addShip("Crusader",10);//,GetShipResource("Pretorian"));
		}

		
		[Test]
		public void GridCoordValid() {
			InterpreterBase interpreter = new MoveInterpreter("",null);
			
			for( int i = 1; i <= 8 ; ++i ) {
				for( int j = 1; j <= 8 ; ++j ) {
					Assert.IsTrue( interpreter.GridCoordValid( i + "_" + j ), "Coordenada no  vlida" ); 		
				}	
			}

			Assert.IsFalse( interpreter.GridCoordValid( 9 + "_" + 1 ), "Coordenada  vlida" );
			Assert.IsFalse( interpreter.GridCoordValid( 10 + "_" + 1 ), "Coordenada  vlida" );
			Assert.IsFalse( interpreter.GridCoordValid( 7 + "_" + 9 ), "Coordenada  vlida" );
			Assert.IsFalse( interpreter.GridCoordValid( 9 + "_" + 9 ), "Coordenada  vlida" );
		}

		[Test]
		public void MoveInterpreterCheck() {
            int id = Universe.instance.CreateBattle( pyro, pyroFleet, pre, preFleet, BattleType.FRIENDLY, "totalannihilation");
			BattleInfo info = Universe.instance.GetBattle( id );

			string move = "Crusader-8_5-20";

			Hashtable parameters = new Hashtable();
			parameters.Add( "info", move );
			parameters.Add( "battleInfo", info );

			InterpreterBase interpreter = (InterpreterBase )interpreterFactory.create( "move", parameters );
            Assert.IsNull(interpreter.CheckMove( ),"O movimento deveria ter sucessedido");

			interpreter.Move = "Crusader-5_10-20";
			ResultItem r = interpreter.CheckMove( );
			Assert.IsNotNull(r,"O movimento deveria ter falhado");

			interpreter.Move = "Crusader-5_5-200";
			r = interpreter.CheckMove( );
			Assert.IsNotNull(r,"O movimento deveria ter falhado");
		}

   	};

}
