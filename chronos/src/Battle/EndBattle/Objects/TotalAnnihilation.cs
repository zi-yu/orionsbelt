using System;
using Chronos.Core;

namespace Chronos.Battle {
	[Serializable]
	public class TotalAnnihilation : EndBattleBase {
		#region Constructor

		public TotalAnnihilation( BattleInfo battleInfo) : base( battleInfo )
			{}

		#endregion

		#region Public

		public override string Type {
			get { return "totalannihilation"; }
		}

		public override bool HasEnded() {
			return (!BattleInfo.RBI1.HasUnits || !BattleInfo.RBI2.HasUnits ) && !BattleInfo.RBI1.InitialContainerHasUnits && !BattleInfo.RBI2.InitialContainerHasUnits;
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
		}

		#endregion
	}
}
