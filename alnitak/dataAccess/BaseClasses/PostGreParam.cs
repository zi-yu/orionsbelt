using NpgsqlTypes;

namespace Alnitak.PostGre {
	
	public class PostGreParam {
		
		#region Private Fields

		private object _value;
		private NpgsqlDbType _type;
		private int _size = -1;

		#endregion

		#region Properties

		public object Value {
			get { return _value; }
			set { _value = value; }
		}

		public NpgsqlDbType Type {
			get { return _type; }
			set { _type = value; }
		}

		public int Size {
			get { return _size; }
			set { _size = value; }
		}

		public bool HasSize {
			get { return _size != -1; }
		}

		#endregion

		#region Constructors

		public PostGreParam( object value, NpgsqlDbType type ) {
			_value = value;
			_type = type;
		}

		public PostGreParam( object value, NpgsqlDbType type, int size ) {
			_value = value;
			_type = type;
			_size = size;
		}

		#endregion
	}
}
