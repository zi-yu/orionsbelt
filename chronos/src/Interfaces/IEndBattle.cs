using Chronos.Core;
using Chronos.Battle;

namespace Chronos.Interfaces {
	
	public interface IEndBattle {
		void BattleEnd();
		void TournamentEnd();
		void FriendlyEnd();
	}
}
