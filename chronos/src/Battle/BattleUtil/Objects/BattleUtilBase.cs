namespace Chronos.Battle {
	
	public abstract class BattleUtilBase {

		#region Abstract

		public abstract void Init( RulerBattleInfo rbi );
		public abstract bool HasWon( RulerBattleInfo rbi );

		#endregion
	
	}
}
