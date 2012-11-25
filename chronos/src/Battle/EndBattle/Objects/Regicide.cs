using System;
using Chronos.Core;

namespace Chronos.Battle {
	
	[Serializable]
	public class Regicide : EndBattleBase {

		#region Constructor

		public Regicide( BattleInfo battleInfo) : base( battleInfo )
			{}

		#endregion

		#region Public

		public override string Type {
			get { return "regicide"; }
		}
		
		public override bool HasEnded() {
			return !BattleInfo.RBI1.HasUnit("FlagShip") || !BattleInfo.RBI2.HasUnit("FlagShip");
		}

		#endregion

		#region Protected Virtual

		public override void BattleEnd() {
			if( BattleInfo.RBI1.Won && BattleInfo.RBI2.Won ) {
				//algo está mal;
				return;
			}
			
			base.BattleEnd();
		}

		public override void TournamentEnd() {
			if( BattleInfo.RBI1.Won && BattleInfo.RBI2.Won ) {
				//algo está mal;
				return;
			}
			base.TournamentEnd();
			BattleInfo.RBI1.RemoveUnit("FlagShip");
			BattleInfo.RBI2.RemoveUnit("FlagShip");
		}

		#endregion
	}
}
