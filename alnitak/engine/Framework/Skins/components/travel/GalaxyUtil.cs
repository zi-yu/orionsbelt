using Chronos.Core;

namespace Alnitak {

	/// <summary>
	/// Incrementa a coordenada de uma sistema
	/// </summary>
	public class GalaxyUtil : IUtil {
		
		#region IIncrement Members
	
		public bool increment( ref string coordinate, int max ) {
			int c = int.Parse(coordinate);
			if( c == Chronos.Core.Coordinate.MaximumGalaxies ) {
				coordinate = "1";
			}else {
				coordinate = (++c).ToString();
			}
			
			return false;
		}

		/// <summary>
		/// decrementa o endereço de galáxia
		/// </summary>
		/// <param name="coordinate"></param>
		/// <returns>Retorna sempre true visto que o controlo de navegação tem de ser eliminado</returns>
		public bool decrement( ref string coordinate, int max ) {
			int c = int.Parse(coordinate);
			if( c == 1 ) {
				coordinate = (Chronos.Core.Coordinate.MaximumGalaxies).ToString();				
			} else {
				coordinate = (--c).ToString();
			}
			return false;
		}

		public bool isLast( string coordinate, int max ) {
			return max == 4 ? false : true;
		}

		public bool isFirst( string coordinate, int max ) {
			return max == 4 ? false : true;
		}

		#endregion
	}
}
