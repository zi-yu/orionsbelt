using DesignPatterns.Attributes;

namespace Chronos.Battle {
	[FactoryKey("regicide")]
	public class RegicideFactory : EndBattleFactory {
		protected override object CreateEndBattle( BattleInfo battleInfo ) {
			return new Regicide(battleInfo);
		}
	}
}
