using DesignPatterns.Attributes;

namespace Chronos.Battle {
	
	[FactoryKey("battle")]
	public class BattleInterpreterFactory : InterpreterFactory  {
		
		protected override object CreateInterpreter( string info, BattleInfo battleInfo ) {
			return new BattleInterpreter( info, battleInfo );
		}

	}
}