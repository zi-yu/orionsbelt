using System;
using System.Collections;
using Chronos.Interfaces;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Battle {
	
	[Serializable]
	public abstract class EndBattleBase : IEndBattle {
		#region Instance Fields

		private BattleInfo _battleInfo;
		protected Hashtable delegates = new Hashtable();
		private delegate void BattleTypeEnd();

		#endregion

		#region Properties

		public BattleInfo BattleInfo {
			get { return _battleInfo; }
			set { _battleInfo = value; }
		}

		#endregion

		#region Abstract

		public abstract string Type{ get; }
		public abstract bool HasEnded();

		#endregion

		#region Protected Virtual
		
		public virtual void BattleEnd() {
			BattleInfo.BattleEnd();
		}

		public virtual void TournamentEnd() {
			Universe.instance.RegisterTournamentBattle(Type,BattleInfo);
			BattleInfo.TournamentEnd();
		}

		public virtual void FriendlyEnd() {
			BattleInfo.FriendlyEnd();
		}
		
		#endregion

		#region Protected

		protected Resource GetUnitResource( string unit ) {
			return Universe.getFactory("planet", "Unit", unit).create( );
		}
		
		#endregion

		#region Public

		public void EndBattle() {
			((BattleTypeEnd)delegates[BattleInfo.BattleType])();
			Universe.instance.RemoveBattle( BattleInfo.BattleId);
		}

		#endregion

		#region Constructor

		public EndBattleBase( BattleInfo battleInfo ) {
			_battleInfo = battleInfo;
			delegates.Add( BattleType.BATTLE, new BattleTypeEnd(BattleEnd) );
			delegates.Add( BattleType.TOURNAMENT, new BattleTypeEnd(TournamentEnd) );
			delegates.Add( BattleType.FRIENDLY, new BattleTypeEnd(FriendlyEnd) );
		}

		#endregion
	}
}