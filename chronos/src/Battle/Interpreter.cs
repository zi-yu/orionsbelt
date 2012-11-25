using System;
using System.Collections;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Messaging;
using DesignPatterns;

namespace Chronos.Battle {
	public class Interpreter {
		#region Constructor

		private static FactoryContainer interpreterFactory = new FactoryContainer( typeof ( InterpreterFactory ) );
		private BattleInfo _battleInfo;
		private Ruler _ruler;

		#endregion

		#region Constructor

		public Interpreter( Ruler ruler, BattleInfo battleInfo ) {
			_ruler = ruler;
			_battleInfo = battleInfo;
		}

		#endregion

		#region Properties

		public BattleInfo BattleInfo {
			get { return _battleInfo; }
			set { _battleInfo = value; }
		}

		public Ruler CurrentRuler {
			get { return _ruler; }
			set { _ruler = value; }
		}

		#endregion

		#region Private

		private Result MakeMoves( string[] splittedMoves ) {
			Result result = new Result( );
			
			int moveCost = 0;
			foreach ( string move in splittedMoves ) {
				if ( move == string.Empty ) {
					continue;
				}

				string[] factory = move.Split( ':' );

				Hashtable parameters = new Hashtable( );
				parameters.Add( "info", factory[ 1 ] );
				parameters.Add( "battleInfo", BattleInfo );

				InterpreterBase interpreter = (InterpreterBase) interpreterFactory.create( factory[0], parameters );
				moveCost += interpreter.MoveCost();
				ResultItem resultItem = interpreter.CheckMove( );
				
				if( null == resultItem) {
					interpreter.Interpretate( );
				}else {
					result.failed( resultItem );
				}
			}
			
			SimpleBattleInfo sInfo = CurrentRuler.GetBattle( BattleInfo.BattleId, BattleInfo.BattleType );
			if ( !sInfo.IsPositionTime && CurrentRuler.NumberOfMoves < moveCost ) {
				result.failed( new InvalidNumberOfMoves( ) );
			}

			if ( result.Ok ) {
				result.passed( new OperationSucceded( ) );
			}

			return result;
		}

		#endregion

		#region Public

		public Result Interpretate( string moves ) {
			RulerBattleInfo r1 = (RulerBattleInfo) BattleInfo.RBI1.Clone( );
			RulerBattleInfo r2 = (RulerBattleInfo) BattleInfo.RBI2.Clone( );

			string[] splittedMoves = moves.Split( ';' );

			Result result = null;

			try {
				result = MakeMoves( splittedMoves );
			}catch(Exception e) {
				result = new Result();
				result.failed( new InvalidMove() );
			}

			if( BattleInfo.GetRulerBattleInfo(CurrentRuler).InitialContainerHasUnits ) {
				result.failed( new InvalidOperation() );
			}

			if ( !result.Ok ) {
				BattleInfo.RBI1 = r1;
				BattleInfo.RBI2 = r2;
			}else {
				Messenger.Send( BattleInfo.RBI1,"BattleEndTurn", CurrentRuler.Name);
				Messenger.Send( BattleInfo.RBI2,"BattleEndTurn", CurrentRuler.Name);
			}

			return result;
		}

		#endregion
	}
}
