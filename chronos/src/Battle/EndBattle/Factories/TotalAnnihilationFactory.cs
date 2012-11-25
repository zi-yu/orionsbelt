using DesignPatterns.Attributes;

namespace Chronos.Battle {
	[FactoryKey("totalannihilation")]
	public class TotalAnnihilationFactory : EndBattleFactory {
		protected override object CreateEndBattle( BattleInfo battleInfo ) {
			return new TotalAnnihilation( battleInfo );
		}
	}
}
