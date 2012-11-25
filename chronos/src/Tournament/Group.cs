// created on 9/2/2005 at 9:58 AM

using System;
using System.Collections;
using Chronos.Core;
using Chronos.Battle;

namespace Chronos.Tournaments {

	[Serializable]
	public class Group {
	
		#region Instance Fields
		
		private int id;
		private ArrayList list;
		private Hashtable matches;
		
		#endregion
		
		#region Instance Properties
		
		public int Id {
			get { return id; }
		}
		
		public ArrayList Registered {
			get { return list; }
		}
		
		public Hashtable Matches {
			get { return matches; }
		}
		
		#endregion
		
		#region Ctors
		
		public Group( int _id )
		{
			id = ++_id;
			list = new ArrayList();
			matches = new Hashtable();
		}
		
		#endregion
		
		#region Public Methods
		
		public void Register( Ruler ruler )
		{
			list.Add( new Classification(ruler) );
		}
		
		public void GenerateMatches()
		{
			for( int i = 0; i < Registered.Count; ++i ) {
				Ruler ruler = ((Classification) Registered[i]).Player;
				for( int j = 0; j < Registered.Count; ++j ) {
					if( j == i ) {
						continue;
					}
					Ruler other = ((Classification) Registered[j]).Player;
					if( !HasMatch(ruler, other) ) {
						AddMatch( ruler, other );
					}
				}
			}
		}
		
		#endregion
		
		#region Utilities
		
		public void MatchEnded( Match match )
		{
			Classification c1 = GetClassification( match.NumberOne );
			c1.Points += match.NumberOnePoints;
			
			Classification c2 = GetClassification( match.NumberTwo );
			c2.Points += match.NumberTwoPoints;
			
			switch( match.Result ) {
				case BattleResult.Draw:
					GenerateDraw(c1);
					GenerateDraw(c2); 
					break;
				case BattleResult.NumberOneVictory:
					GenerateVictory(c1);
					GenerateDefeat(c2); 
					break;
				case BattleResult.NumberTwoVictory: 
					GenerateVictory(c2);
					GenerateDefeat(c1); 
					break;
			}
			
			Registered.Sort();
		}
		
		private void GenerateDraw( Classification c )
		{
			++c.Draws;
		}
		
		private void GenerateVictory( Classification c )
		{
			++c.Wins;
		}
		
		private void GenerateDefeat( Classification c )
		{
			++c.Defeats;
		}
		
		public Classification GetClassification( Ruler ruler )
		{
			foreach( Classification c in Registered ) {
				if( c.Player.Id == ruler.Id ) {
					return c;
				}
			}
			return null;
		}
		
		public void AddMatch( Ruler a, Ruler b )
		{
			Matches.Add( GetKey(a,b), new Match(a, b) );
		}
		
		public bool HasMatch( Ruler a, Ruler b ) 
		{
			if( Matches.ContainsKey( GetKey(a,b) ) ) {
				return true;
			}
			return Matches.ContainsKey( GetKey(b,a) );
		}
		
		public Match HasMatch( int battleId ) 
		{
			foreach( Match match in Matches.Values ) {
				if( match.BattleId == battleId ) {
					return match;
				}
			}
			return null;
		}
		
		public object GetKey( Ruler a, Ruler b )
		{
			return string.Format("{0}-{1}", a.Id, b.Id);
		}
		
		#endregion
	
	};

}