using DesignPatterns.Attributes;

namespace Chronos.Battle {
	
	[FactoryKey("hologram")]
	public class HologramInterpreterFactory : InterpreterFactory  {
		
		protected override object CreateInterpreter( string info, BattleInfo battleInfo ) {
			return new HologramInterpreter( info, battleInfo );
		}

	}
}