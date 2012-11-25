// created on 2/26/2006 at 10:16 AM

using System;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Sorter {

	public class ScoreComparer : PlanetComparer {
	
		#region PlanetComparer Implementation
		
		protected override int Compare( Planet p1, Planet p2 )
		{
			return p1.Score.CompareTo(p2.Score);
		}
		
		protected override int Compare( Ruler r1, Ruler r2 )
		{
			double q1 = Count(r1);
			double q2 = Count(r2);
			
			return q1.CompareTo(q2);
		}
		
		#endregion
		
		#region Utils
		
		public static double Count( Ruler r )
		{
			double sum = 0;
			foreach( Planet p in r.Planets ) {
				sum += p.Score;
			}
			return sum / r.Planets.Length;
		}
		
		#endregion
	};
	
}
