// created on 2/26/2006 at 6:27 PM

using System;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Sorter {

	public class RatioComparer : PlanetComparer {
	
		#region Ctor
		
		private string resource = null;
		
		public RatioComparer( string _resource )
		{
			resource = _resource;
		}
		
		#endregion
	
		#region PlanetComparer Implementation
		
		protected override int Compare( Planet p1, Planet p2 )
		{
			int q1 = p1.getPerTurn("Intrinsic", resource);
			int q2 = p2.getPerTurn("Intrinsic", resource);
			
			return q1.CompareTo(q2);
		}
		
		protected override int Compare( Ruler r1, Ruler r2 )
		{
			int q1 = Count(r1, resource);
			int q2 = Count(r2, resource);
			
			return q1.CompareTo(q2);
		}
		
		#endregion
		
		#region Utils
		
		public static int Count( Ruler r, string resource )
		{
			int sum = 0;
			foreach( Planet p in r.Planets ) {
				sum += p.getPerTurn("Intrinsic", resource);
			}
			return sum;
		}
		
		#endregion
	};
	
}
