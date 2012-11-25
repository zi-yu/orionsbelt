using Chronos.Core;

namespace Alnitak {

	/// <summary>
	/// Incrementa a coordenada de uma sistema
	/// </summary>
	public class SectorUtil : IUtil {
		
		#region private 
	
		private string Parse( ref string str ) {
			int i = str.LastIndexOf(":");
			string s = str.Substring( i+1, str.Length - i-1 );
			str = str.Substring(0, i );
			return s;
		}

		#endregion

		#region IIncrement Members
	
		public bool increment( ref string coordinate, int max ) {
			string startStr = coordinate;
			string rightStr = Parse( ref startStr );
			int sector = int.Parse( rightStr );
			if( sector == Chronos.Core.Coordinate.MaximumSectors ) {
                int s = int.Parse( Parse( ref startStr ) );
				if( s == Chronos.Core.Coordinate.MaximumSystems ) {
					int g = int.Parse( startStr );
					if( g == Chronos.Core.Coordinate.MaximumGalaxies ) {
						coordinate = "1:1:1";
					} else {
						coordinate = (++g) + ":1:1";
					}
				}else {
					coordinate = startStr + ":" + (++s) + ":1";
				}
			} else{
				coordinate = startStr + ":" + (++sector);
			}
			return false;
		}

		public bool decrement( ref string coordinate, int max ) {
			string startStr = coordinate;
			string rightStr = Parse( ref startStr );
			int sector = int.Parse( rightStr );
			if( sector == 1 ) {
				rightStr = Parse( ref startStr );
                int s = int.Parse( rightStr );
				if( s == 1 ) {
					int g = int.Parse( startStr );
					if( g == 1 ) {
						coordinate = Chronos.Core.Coordinate.MaximumGalaxies + ":" + Chronos.Core.Coordinate.MaximumSystems + ":" + Chronos.Core.Coordinate.MaximumSectors;
					} else {
						coordinate = (--g) + ":" + Chronos.Core.Coordinate.MaximumSystems + ":" + Chronos.Core.Coordinate.MaximumSectors;
					}
				}else {
					coordinate = startStr + ":" + (--s) + ":" + Chronos.Core.Coordinate.MaximumSectors;
				}
			} else{
				coordinate = startStr + ":" + (--sector);
			}
			return false;
		}

		public bool isFirst( string coordinate, int max ) {
			if( max == 1 )
				return true;
			
			string rightStr = Parse( ref coordinate );
			int sector = int.Parse( rightStr );
			if( max != 4 ) {
				if( sector == 1 ) {
					return true;
				}
			}
			return false;
		}

		public bool isLast( string coordinate, int max ) {
			if( max == 1 )
				return true;

			string rightStr = Parse( ref coordinate );
			int sector = int.Parse( rightStr );
			if( max != 4 ) {
				if( sector == Chronos.Core.Coordinate.MaximumSectors ) {
					return true;
				}
			}
			return false;
		}

		#endregion
	}
}
