using DesignPatterns;

namespace Chronos.Battle {
	
	public abstract class BattleUtilFactory : IFactory {
		
		public object create( object args ) {
			return StartBattle();
		}

		protected abstract object StartBattle();
	}
}
