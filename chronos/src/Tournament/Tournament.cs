// created on 9/2/2005 at 9:53 AM

using System;
using System.Collections;
using Chronos.Core;
using Chronos.Battle;

namespace Chronos.Tournaments {

	[Serializable]
	public class Tournament {
	
		#region Instance Fields
		
		private ArrayList registered;
		private TournamentState state;
		private Phase phase;
		private string type;
		private Fleet fleet;
		
		#endregion
		
		#region Instance Properties
		
		public ArrayList Registered {
			get { return registered; }
		}
		
		public string TournamentType {
			get { return type; }
		}
		
		public Phase CurrentPhase {
			get { return phase; }
		}
		
		public TournamentState State {
			get { return state; }
		}
		
		public bool AllowRegistering {
			get { return State == TournamentState.Subscriptions; }
		}
		
		public Fleet BaseFleet {
			get { return fleet; }
		}
		
		public int Participants {
			get {
				if( State == TournamentState.Subscriptions ) {
					return registered.Count;
				}
				return CurrentPhase.Participants;
			}
		}
		
		#endregion
		
		#region Ctors
		
		public Tournament( string _type, Fleet _fleet )
		{
			type = _type;
			fleet = _fleet;
			registered = new ArrayList();
			state = TournamentState.Subscriptions;
		}

		#endregion
		
		#region Public Methods
		
		public int GetPoints( RulerBattleInfo info )
		{
			if( State == TournamentState.Championship || State == TournamentState.Playoffs ) {
				return CurrentPhase.GetPoints(info);
			}
			return 0;
		}
		
		#endregion
		
		#region Utilities
		
		public bool Register( Ruler ruler )
		{
			if( !AllowRegistering ) {
				return false;
			}
			
			if( IsRegistered(ruler) ) {
				return false;
			}
			
			registered.Add( ruler);
			
			return true;
		}
		
		public bool IsRegistered( Ruler ruler )
		{		
			foreach( Ruler reg in Registered ) {
				if( reg.Id == ruler.Id ) {
					return true;
				}
			}
			return false;
		}
		
		public void EndSubscriptions()
		{
			state = TournamentState.Championship;			
			phase = new Championship(Registered, this);
		}
		
		public void EndChampionship()
		{	
			CurrentPhase.ForceEnd();
			ArrayList nextRound = CurrentPhase.GetWinners();
			
			Phase playoffs = new Playoffs(nextRound, this);
			phase = playoffs;
			
			state = TournamentState.Playoffs;
		}
		
		public IList EndPlayoffs()
		{
			CurrentPhase.ForceEnd();
			state = TournamentState.Finished;
			IList winners = CurrentPhase.GetWinners();
			return winners;
		}
		
		#endregion
	
	};

}