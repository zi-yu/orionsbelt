// created on 16-01-2005 at 18:15

using System;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Sorter {

	public class CultureComparer : PlanetComparer {
	
		#region PlanetComparer Implementation
		
		protected override int Compare( Planet p1, Planet p2 )
		{
			return p1.Culture.CompareTo(p2.Culture);
		}
		
		protected override int Compare( Ruler r1, Ruler r2 )
		{
			int q1 = r1.getResourceCount("Intrinsic", "culture");
			int q2 = r2.getResourceCount("Intrinsic", "culture");
			
			return q1.CompareTo(q2);
		}
		
		#endregion
	};
	
}
