// created on 12/28/2005 at 10:40 AM

using System;
using System.Collections;
using Chronos.Battle;
using Chronos.Core;

namespace Alnitak {

	public class RankingComparer : IComparer {
	
		#region Fields
		
		private Hashtable cache = new Hashtable();
		
		#endregion
	
		#region IComparer Implementation
		
		public int Compare( object x, object y )
		{
			Ruler one = x as Ruler;
			Ruler two = y as Ruler;
			
			if( one == null || two == null ) {
				return 0;
			}
			
			User uone = GetUser(one);
			User utwo = GetUser(two);
			
			return -uone.EloRanking.CompareTo( utwo.EloRanking );
		}
		
		private User GetUser( Ruler ruler )
		{
			if( cache.ContainsKey(ruler) ) {
				return (User) cache[ruler];
			}
			User user = UserUtility.bd.getUser(ruler.ForeignId);
			cache[ruler] = user;
			return user;
		}
		
		#endregion
		
	};

}