using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Chronos.Core;

namespace Chronos.Persistence {

	/// <summary>Encarrega-se de armazenar/carregar um Universo</summary>
	public abstract class UniverseSerializer : IPersistence {

		#region Instance Fields

		private IFormatter formatter;
		private long size;
		
		#endregion

		#region Instance Properties

		/// <summary>Indica o formatter a utilizar</summary>
		public IFormatter Formatter {
			get { return formatter; }
			set { formatter = value; }
		}

		/// <summary>Indica o tamanho da ltima stream criada</summary>
		public long StreamSize {
			get { return size; }
			set { size = value; }
		}

		#endregion

		#region Ctors

		/// <summary>Ctor</summary>
		public UniverseSerializer()
		{
			formatter = new BinaryFormatter();
		}

		#endregion

		#region IPersistence Implementation
		
		/// <summary>Salva um Universo</summary>
		public virtual void save( Universe universe, PersistenceParameters parameters )
		{
			MemoryStream stream = new MemoryStream();

			formatter.Serialize(stream, universe);
			byte[] data = stream.ToArray();
			size = data.Length;
			saveData( data, parameters );
		}
		
		/// <summary>Carrrega um Universo</summary>
		public Universe load(PersistenceParameters parameters)
		{
			Stream stream = loadData(parameters);
			if( null == stream ) {
				return null;
			}
			return (Universe) formatter.Deserialize(stream);
		}
		
		#endregion

		#region Abstract Methods

		/// <summary>Armazena uma Stream com persistncia</summary>
		protected abstract void saveData( byte[] data, PersistenceParameters parameters );

		/// <summary>Carrega uma Stream com um Universo</summary>
		protected abstract Stream loadData( PersistenceParameters parameters );
		
		#endregion

		#region Static Members
		
		private static UniverseSerializer persistence;
		
		/// <summary>Construtor esttico</summary>
		static UniverseSerializer()
		{
//#if PERSIST_TO_FILE
		persistence = new FilePersistence("Universe.bin");
/*#elif PERSIST_TO_SQLSERVER
		persistence = new Chronos.Persistence.SqlServer.BinarySqlPersistence();
#elif PERSIST_TO_POSTGRE
		persistence = new Chronos.Persistence.PostGreSql.BinaryPostGreSqlPersistence();
#else
#			error No foi encontrada menhuma macro de compilacao!
#endif*/
		}
		
		/// <summary>Retorna o objecto que se encarrega da persistncia</summary>
		public static UniverseSerializer Instance {
			get {
				return persistence;
			}
		}
		
		#endregion

	
	};
}

