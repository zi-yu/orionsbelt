
using System.Collections;
using DesignPatterns;

namespace Chronos.Battle {
	
	public abstract class InterpreterFactory : IFactory {
		
		public object create( object args ) {
			Hashtable arg = (Hashtable)args;

			string info = arg["info"].ToString( );
			BattleInfo battleInfo = (BattleInfo)arg["battleInfo"];
			return CreateInterpreter( info, battleInfo );
		}

		protected abstract object CreateInterpreter( string info, BattleInfo battleInfo );
	}
}
