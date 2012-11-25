// created on 2/26/2006 at 6:51 PM

using System;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Sorter {

	public class SizeComparer : PlanetComparer {
	
		#region PlanetComparer Implementation
		
		protected override int Compare( Planet p1, Planet p2 )
		{
			int q1 = p1.Info.GroundSpace + p1.Info.WaterSpace + p1.Info.OrbitSpace;
			int q2 = p2.Info.GroundSpace + p2.Info.WaterSpace + p2.Info.OrbitSpace;
			
			return q1.CompareTo(q2);
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
				sum += p.Info.GroundSpace + p.Info.WaterSpace + p.Info.OrbitSpace;
			}
			return sum;
		}
		
		#endregion
	};
	
}
