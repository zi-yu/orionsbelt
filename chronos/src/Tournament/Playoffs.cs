// created on 9/2/2005 at 9:59 AM

using System;
using System.Collections;
using Chronos.Battle;
using Chronos.Core;

namespace Chronos.Tournaments {

	[Serializable]
	public class Playoffs : Phase {
	
		#region Instance Fields
		
		private ArrayList matches;
		private Ruler lucky;
		
		#endregion
		
		#region Instance Properties
		
		public Ruler Lucky {
			get { return lucky; }
		}
		
		public ArrayList Matches {
			get {
				if( matches == null ) {
					Prepare();
				} 
				return matches; 
			}
		}
		
		#endregion
		
		#region Ctors
		
		public Playoffs( ArrayList _registered, Tournament _owner ) : base(_owner)
		{
			Registered = _registered;
			Prepare();
		}
		
		#endregion
		
		#region Public Methods
		
		public void Advance()
		{
			Registered = GetWinners();
			Registered = Randomize(Registered);
			Prepare();
		}
		
		public void Prepare()
		{
			matches = new ArrayList();
			lucky = null;
			
			if( Registered.Count % 2 != 0 ) {
				int idx = Registered.Count -1;
				lucky = (Ruler) Registered[idx];
				Registered.RemoveAt(idx);
			}
			
			GenerateMatches();
		}
		
		public void GenerateMatches()
		{
			for( int i = 0; i < Registered.Count; i += 2 ) {
				Match match = new Match( (Ruler) Registered[i], (Ruler) Registered[i+1] );
				Matches.Add( match );
				GenerateBattle(match);
			}
		}
		
		#endregion
		
		#region Phase Implementation
		
		public override void ForceEnd()
		{
		}
		
		public override ArrayList GetWinners()
		{
			ArrayList list = new ArrayList();
			
			foreach( Match match in Matches ) {
				if( match.Result == BattleResult.NumberOneVictory ) {
					list.Add( match.NumberOne );
				} else {
					list.Add( match.NumberTwo );
				}
			}
			
			if( Lucky != null ) {
				list.Add(Lucky);
			}
			
			return list;
		}
		
		public override void BattleEnded( BattleInfo battle )
		{
			foreach( Match match in Matches ) {
				if( match.BattleId == battle.BattleId ) {
					match.Result = battle.Result( match.NumberOne, match.NumberTwo );
					match.NumberOnePoints = GetPoints(battle.RBI1);
					match.NumberTwoPoints = GetPoints(battle.RBI2);
				}
			}
		}
		
		public override int Participants {
			get {
				int hasLucky = Lucky == null ? 0 : 1;
				return Registered.Count + hasLucky;
			}
		}
		
		#endregion
		
		#region Utilities
		
		#endregion
	
	};

}