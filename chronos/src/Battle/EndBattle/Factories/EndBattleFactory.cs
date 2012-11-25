using DesignPatterns;

namespace Chronos.Battle {
	
	public abstract class EndBattleFactory : IFactory {
		
		public object create( object args ) {
			return CreateEndBattle( (BattleInfo)args );
		}

		protected abstract object CreateEndBattle( BattleInfo battleInfo );
	}
}
