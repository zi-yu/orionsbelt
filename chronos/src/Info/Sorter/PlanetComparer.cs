// created on 2/26/2006 at 10:05 AM

using System;
using System.Collections;
using Chronos.Core;

namespace Chronos.Sorter {

	public abstract class PlanetComparer : IComparer {
		
		#region IComparer Implementation
		
		int IComparer.Compare( object x, object y )
		{
			Planet p1 = x as Planet;
			Planet p2 = y as Planet;
			
			if( p1 == null || p2 == null ) {
				return -Compare( (Ruler)x, (Ruler)y );
			}
			
			return -Compare(p1, p2);
		}
		
		#endregion
		
		#region Abstract
		
		protected abstract int Compare( Planet p1, Planet p2 );
		
		protected abstract int Compare( Ruler r1, Ruler r2 );
		
		#endregion
		
	};
	
}
