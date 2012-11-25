using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chronos.Persistence {
	
	/// <summary>Class utilitria para entidades que se queiram persistir
	/// juntamente com o Universo</summary>
	[Serializable]
	public sealed class PersistenceServices {
		
		#region Instance Fields
		
		private Hashtable content = new Hashtable();
		
		#endregion
		
		#region Instance Members
		
		/// <summary>Permite registar estado</summary>
		public void Register( string entity, object state )
		{
			content[entity] = state;
		}
		
		/// <summary>Permite obter estado</summary>
		public object GetState( string entity )
		{
			return content[entity];
		}
		
		public void Remove( string entity )
		{
			content.Remove(entity);
		}
		
		#endregion
		
		#region Serialization Utilities
		
		public static IFormatter Formatter = new BinaryFormatter();
		
		public static object DeepClone( object source )
		{
			return FromBytes( ToBytes(source) );
		}
		
		public static byte[] ToBytes( object source )
		{
			MemoryStream stream = new MemoryStream();

			Formatter.Serialize(stream, source);
			return stream.ToArray();
		}
		
		public static object FromBytes( byte[] source )
		{
			Stream stream = new MemoryStream(source);
			return Formatter.Deserialize(stream);
		}
		
		#endregion
		
	};
	
}
