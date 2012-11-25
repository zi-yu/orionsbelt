using System.Collections;
using Chronos.Core;

namespace Chronos.Interfaces {
	
	/// <summary>
	/// Interface implementada pelos tipos que podem participar
	/// numa batalha (Fleet por exemplo)
	/// </summary>
	public interface IBattle {
		Coordinate Coordinate{set;get;}
		bool IsInBattle{set;get;}
		ArrayList GetBattleElements();
		void SetBattleElements( ArrayList elements );
	}
}
