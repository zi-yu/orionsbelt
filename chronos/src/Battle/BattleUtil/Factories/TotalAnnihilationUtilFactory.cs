using System;
using DesignPatterns.Attributes;

namespace Chronos.Battle {

	[FactoryKey("totalannihilation")]
	public class TotalAnnihilationUtilFactory : BattleUtilFactory {
		
		private static TotalAnnihilationUtil TotalAnnihilationUtil = new TotalAnnihilationUtil();

		protected override object StartBattle() {
			return TotalAnnihilationUtil;
		}
	}
}
