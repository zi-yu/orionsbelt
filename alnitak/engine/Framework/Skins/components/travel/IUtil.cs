namespace Alnitak {
	
	public interface IUtil {
		bool increment( ref string coordinate, int max );
		bool decrement( ref string coordinate, int max );
		bool isLast( string coordinate, int max );
		bool isFirst( string coordinate, int max );
	}

}
