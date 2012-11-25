// created on 2/26/2006 at 10:17 AM

using System;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Sorter {

	public class FleetComparer : PlanetComparer {
	
		#region PlanetComparer Implementation
		
		protected override int Compare( Planet p1, Planet p2 )
		{
			Fleet f1 = p1.getDefenseFleet();
			Fleet f2 = p2.getDefenseFleet();
			
			int c1 = Count(f1);
			int c2 = Count(f2);
			
			return c1.CompareTo(c2);
		}
		
		protected override int Compare( Ruler r1, Ruler r2 )
		{
			int q1 = Count(r1);
			int q2 = Count(r2);;
			
			return q1.CompareTo(q2);
		}
		
		#endregion
		
		#region Utils
		
		public static int Count ( Fleet f )
		{
			int count = 0;
			foreach( int i in f.Ships.Values ) {
				count += i;
			}
			return count;
		}
		
		public static int Count ( Ruler r )
		{
			int sum = 0;
			foreach( Planet p in r.Planets ) {
				foreach( Fleet f in p.Fleets.Values ) {
					sum += Count(f);
				}
			}
			return sum;
		}
		
		public static int Count ( Ruler r, string type )
		{
			int sum = 0;
			foreach( Planet p in r.Planets ) {
				sum += Count(p, type);
			}
			return sum;
		}
		
		public static int Count ( Planet p, string type )
		{
			int sum = 0;
			foreach( Fleet f in p.Fleets.Values ) {
				foreach( Resource res in f.ShipsResources ) {
					if( res.Unit.UnitType == type ) {
						sum += (int) f.Ships[res.Name];
					}
				}
			}
			return sum;
		}
		
		#endregion
	};
	
}
