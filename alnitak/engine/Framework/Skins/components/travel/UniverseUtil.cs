using Chronos.Core;

namespace Alnitak {

	/// <summary>
	/// Incrementa a coordenada de uma sistema
	/// </summary>
	public class UniverseUtil : IUtil {
		
		#region IIncrement Members
	
		public bool increment( ref string coordinate, int max ) {
			return false;
		}

		public bool decrement( ref string coordinate, int max ) {
			return false;
		}

		public bool isLast( string coordinate, int max ) {
			return true;
		}

		public bool isFirst( string coordinate, int max ) {
			return true;
		}

		#endregion
	}
}
