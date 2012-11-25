using DesignPatterns.Attributes;

namespace Chronos.Battle {

	[FactoryKey("regicide")]
	public class RegicideUtilFactory : BattleUtilFactory {

		private static RegicideUtil startregicide = new RegicideUtil();
		
		protected override object StartBattle() {
			return startregicide;
		}

	}
}
