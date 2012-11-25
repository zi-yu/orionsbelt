using Chronos.Core;

namespace Alnitak {

	/// <summary>
	/// Incrementa a coordenada de uma sistema
	/// </summary>
	public class SystemUtil : IUtil {

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
			int system = int.Parse( rightStr );
			if( system == Chronos.Core.Coordinate.MaximumSystems ) {
				int galaxy = int.Parse( startStr );
				if( galaxy == Chronos.Core.Coordinate.MaximumGalaxies ) {
					coordinate = "1:1";
				} else {
					coordinate = ++galaxy + ":1";
				}
			} else{
				coordinate = startStr + ":" + (++system);
				/*if( max == 3 ) {
					if( system == Chronos.Core.Coordinate.MaximumSystems ) {
						return true;
					}
				}*/
			}
			return false;
		}

		/// <summary>
		/// decrementa uma coordenada
		/// </summary>
		/// <param name="coordinate"></param>
		/// <returns>true se não der para incrementar, false caso contrário</returns>
		public bool decrement( ref string coordinate, int max ) {
			string startStr = coordinate;
			string rightStr = Parse( ref startStr );
			int system = int.Parse( rightStr );
			if( system == 1 ) {
				int galaxy = int.Parse( startStr );
				if( galaxy == 1 ) {
					coordinate =  Chronos.Core.Coordinate.MaximumGalaxies + ":" + Chronos.Core.Coordinate.MaximumSystems;
				} else {
					coordinate = --galaxy + ":" + Chronos.Core.Coordinate.MaximumSystems;
				}
			} else{
				coordinate = startStr + ":" + (--system);
				if( max == 3 ) {
					if( system == 1 ) {
						return true;
					}
				}
			}
			return false;
		}

		public bool isLast( string coordinate, int max ) {
			if( max == 2 ) {
				return true;
			}
			string[] coord = coordinate.Split( new char[]{':'} );
			if( max != 4 ) {
				if( int.Parse( coord[1] ) == Chronos.Core.Coordinate.MaximumSystems ) {
					return true;
				}
			}
			return false;
		}

		public bool isFirst( string coordinate, int max ) {
			if( max == 2 ) {
				return true;
			}
			string[] coord = coordinate.Split( new char[]{':'} );
			if( max != 4 ) {
				if( int.Parse( coord[1] ) == 1 ) {
					return true;
				}
			}
			return false;
		}

		#endregion
	}
}
