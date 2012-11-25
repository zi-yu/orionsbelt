using DesignPatterns.Attributes;

namespace Chronos.Battle {
	
	[FactoryKey("rotation")]
	public class RotationInterpreterFactory : InterpreterFactory  {
		
		protected override object CreateInterpreter( string info, BattleInfo battleInfo ) {
			return new RotationInterpreter( info, battleInfo );
		}

	}
}