// created on 9/2/2005 at 9:59 AM

//#define DEBUG_BAGS
//#define DEBUG_PLAYOFF_BAGS
#define DEBUG_GET_WINNERS

using System;
using System.Threading;
using System.Collections;
using Chronos.Core;
using Chronos.Battle;
using Chronos.Resources;
using Chronos.Utils;

namespace Chronos.Tournaments {

	[Serializable]
	public class Championship : Phase {
	
		#region Instance Fields
		
		private Group[] groups;
		
		#endregion
		
		#region Instance Properties
		
		public Group[] Groups {
			get { return groups; }
		}
		
		#endregion
		
		#region Ctors
		
		public Championship( ArrayList registered, Tournament _owner ) : base(_owner)
		{
			Registered = registered;
			GenerateGroups();
			GenerateMatches();
		}
		
		#endregion
		
		#region Public Methods
		
		public static int GetNumberOfGroups( int nRulers )
		{
			double n = nRulers;
			n /= 10;
			double integer = Math.Ceiling(n);
			return (int) integer;
		}
		
		public int GetNumberOfRulersPerGroups( int nGroups, int nRulers )
		{
			return nRulers / nGroups;
		}
		
		public void GenerateGroups()
		{
			int currentRulerIdx = 0;
			int nGroups = GetNumberOfGroups(Registered.Count);
			int rulersPerGroup = GetNumberOfRulersPerGroups(nGroups, Registered.Count);
			
			groups = new Group[ nGroups ];
			ArrayList rulersBags = BagRulers(Registered, nGroups);
			
			for( int i = 0; i < nGroups; ++i ) {
				Group group = new Group(i);
				for( int j = 0; j < rulersBags.Count; ++j ) {
					ArrayList bag = (ArrayList) rulersBags[j];
					if( bag.Count == 0 ) {
						continue;
					}
					int idx = MathUtils.random(0, bag.Count);
					Ruler ruler = (Ruler) bag[idx];
					bag.RemoveAt(idx);
					group.Register(ruler);
				}
				groups[i] = group;
			}
		}
		
		private ArrayList BagRulers( ArrayList rulers, int numberOfBags )
		{
#if DEBUG_BAGS
			Log.log("");
			Log.log("***** Bagging {0} Rulers...", rulers.Count);
#endif
			ArrayList bags = new ArrayList();
			while( rulers.Count > 0 ) {
				ArrayList bag = new ArrayList();
				bags.Add(bag);
				for( int j = 0; j < numberOfBags; ++j ) {
					if( rulers.Count == 0 ) {
						break;
					}
					bag.Add( rulers[0] );
					rulers.RemoveAt(0);
				}
				
#if DEBUG_BAGS
				Log.log("--- Bag Created {0} Rulers :: {1} ---", bag.Count, rulers.Count);
				foreach( Ruler ruler in bag ) {
					Log.log(ruler);			
				}
#endif		
			}

			return bags;
		}
		
		public void GenerateMatches()
		{
			ThreadPool.QueueUserWorkItem( new WaitCallback(AsyncGenerateMatches), null);
		}
		
		private void AsyncGenerateMatches(object ignore)
		{
			DateTime time = DateTime.Now;
			foreach( Group group in Groups ) {
				group.GenerateMatches();
				WaitCallback callBack = new WaitCallback(AsyncGenerateBattle);
				WaitHandle handle = callBack.BeginInvoke(group, null, null).AsyncWaitHandle;
				handle.WaitOne();
			}
		}
		
		private void AsyncGenerateBattle(object stateInfo) 
		{
			Group group = (Group) stateInfo;
			foreach( Match match in group.Matches.Values ) {
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
			ArrayList bags = GetPassingBags();
			ArrayList winners = new ArrayList();

#if DEBUG_GET_WINNERS
			Log.log("***** Champ GetWinners bags:{0}", bags.Count);
#endif
			
			for( int s = 0, e = bags.Count, c = 0; c < bags.Count/2; ++s, --e, ++c ) {
				ArrayList bag1 = (ArrayList) bags[s];
				ArrayList bag2 = (ArrayList) bags[e-1];
			
				for( int i = 0, j = bag1.Count -1; i < bag1.Count; ++i, --j ) {
					int idx1 = i;
					int idx2 = j;
					
					Ruler one = (Ruler) bag1[idx1];
					Ruler two = (Ruler) bag2[idx2];
					
					winners.Add(one);
					winners.Add(two);
				}
			}
			
#if DEBUG_GET_WINNERS
			Log.log("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
			foreach( Ruler ruler in winners ) {
				Log.log(ruler);
			}
#endif
			
			return winners;
		}
		
		private ArrayList GetPassingBags()
		{
			int players = GetPassingPlayersCount();
			int nbags = players / 4;
			
#if DEBUG_PLAYOFF_BAGS
			Log.log("");
			Log.log("----- GetPAssingBags players: {0}/{1} nbags:{2}", players, Participants, nbags);
#endif
			
			ArrayList list = new ArrayList();
			foreach( Group group in Groups ) {
				int max = group.Registered.Count;
				for( int i = 0; i < max; ++i ) {
					int factor = 1;
					Classification registered = (Classification) group.Registered[i];
					Classification c = new Classification(registered.Player, registered.Points);
					if( i < 3 ) {
						factor = factor + (3-i) * 1000000;
						c.Points += factor;
					}
					list.Add( c );
				}
			}
			list.Sort();
			
			int PlayersPerBag = 4;
			ArrayList bags = new ArrayList();
			for( int i = 0; i < nbags; ++i ) {
				ArrayList bag = new ArrayList();
				bags.Add(bag);
				for( int j = 0; j < PlayersPerBag; ++j ) {
					Ruler ruler = ((Classification)list[0]).Player;
					list.RemoveAt(0);
					bag.Add( ruler );
				}
			}
			
#if DEBUG_PLAYOFF_BAGS
			
			Log.log("%%%%%%% Playoff {0}-{1} Bags Players: {2}", bags.Count, nbags, players);
			foreach( ArrayList bag in bags ) {
				Log.log("----BAG {0}---", bag.Count);
				foreach( Ruler ruler in bag ) {
					Log.log(ruler);
				}
			}
#endif
			
			return bags;
		}
		
		public override void BattleEnded( BattleInfo battle )
		{
			foreach( Group group in Groups ) {
				Match match = group.HasMatch(battle.BattleId);
				if( match != null ) {
					MatchEnded(battle, group, match);
					break;
				}
			}
		}
		
		public override int Participants {
			get {
				int total = 0;
				foreach( Group group in Groups ) {
					total += group.Registered.Count;
				}
				return total;
			}
		}
		
		#endregion
		
		#region Utilities
		
		private void MatchEnded( BattleInfo battle, Group group, Match match )
		{
			match.Result = battle.Result( match.NumberOne, match.NumberTwo );
			match.NumberOnePoints = GetPoints(battle.RBI1);
			match.NumberTwoPoints = GetPoints(battle.RBI2);
			group.MatchEnded(match);
		}
		
		public static int NextPow( int number )
		{
			for( int i = 1; ; ++i ) {
				int pow = (int) Math.Pow(2, i);
				if( pow >= number ) {
					return pow;
				}
			}
		}
		
		private int GetPassingPlayersCount()
		{
			int first = Groups.Length * 3;
			return NextPow(first);
		}
		
		#endregion
	
	};

}