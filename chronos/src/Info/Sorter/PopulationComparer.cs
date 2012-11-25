// created on 2/26/2006 at 10:16 AM

using System;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Sorter {

	public class PopulationComparer : PlanetComparer {
	
		#region PlanetComparer Implementation
		
		protected override int Compare( Planet p1, Planet p2 )
		{
			return p1.Population.CompareTo(p2.Population);
		}
		
		protected override int Compare( Ruler r1, Ruler r2 )
		{
			int q1 = Count(r1);
			int q2 = Count(r2);
			
			return q1.CompareTo(q2);
		}
		
		#endregion
		
		#region Utils
		
		public static int Count( Ruler r )
		{
			int sum = 0;
			foreach( Planet p in r.Planets ) {
				sum += p.Population;
			}
			return sum;
		}
		
		#endregion
	};
	
}
