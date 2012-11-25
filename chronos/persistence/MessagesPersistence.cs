
using Chronos.Core;
using Chronos.Messaging;

namespace Chronos.Persistence {
	
	/// <summary>Camada de dados para as Mensagens</summary>
	public abstract class MessagesPersistence {
		
		#region Instance Fields
		
		private PersistenceParameters parameters;
		
		#endregion
		
		#region Ctor
		
		public MessagesPersistence( PersistenceParameters param )
		{
			parameters = param;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Obtém os parâmetros de persistência</summary>
		public PersistenceParameters Parameters {
			get { return parameters; }
		}
		
		#endregion
		
		#region Members
		
		/// <summary>Permite Guardar uma mensagem</summary>
		public abstract void SaveMessage( int id, string identifier, Message message );
		
		/// <summary>Marca todas as mensagens como lidas</summary>
		public abstract void MarkAllRead( int id, string identifier );
		
		/// <summary>Indica quantas mensagens não lidas existem</summary>
		public abstract int UnreadCount( int id, string identifier );
		
		/// <summary>Obtém mensagens</summary>
		public abstract Message[] GetMessages( int id, string identifier, MessageType[] types, int count );
		
		#endregion
		
		#region Static Members
		
		private static MessagesPersistence persistence;
		
		/// <summary>Construtor esttico</summary>
		static MessagesPersistence()
		{
#if PERSIST_TO_FILE
			persistence = new MessagesFilePersistence(Universe.Parameters);
#elif PERSIST_TO_SQLSERVER
			persistence = new Chronos.Persistence.SqlServer.SqlServerMessagesPersistence(Universe.Parameters);
#elif PERSIST_TO_POSTGRE
			persistence = new Chronos.Persistence.PostGreSql.PostGreSqlMessagesPersistence(Universe.Parameters);
#else
#			error No foi encontrada menhuma macro de compilacao!
#endif
		}
		
		/// <summary>Retorna o objecto que se encarrega da persistncia</summary>
		public static MessagesPersistence Instance {
			get {
				return persistence;
			}
		}
		
		#endregion
		
		
	};
	
}
