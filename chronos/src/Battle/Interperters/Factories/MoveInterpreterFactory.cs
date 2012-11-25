using DesignPatterns.Attributes;

namespace Chronos.Battle {
	
	[FactoryKey("move")]
	public class MoveInterpreterFactory : InterpreterFactory  {
		
		protected override object CreateInterpreter( string info, BattleInfo battleInfo ) {
			return new MoveInterpreter( info, battleInfo );
		}

	}
}