using System;
using Chronos.Info.Results;

namespace Chronos.Battle {
	
	public class HologramInterpreter : InterpreterBase {
		
		#region Constructor

		public HologramInterpreter( string info, BattleInfo battleInfo ) : base( info, battleInfo ) {

		}

		#endregion

		public override ResultItem CheckMove( ) {
			throw new NotImplementedException( );
		}

		public override void Interpretate( ) {
			throw new NotImplementedException( );
		}
	}
}