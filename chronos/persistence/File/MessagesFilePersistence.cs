using System;
using System.Collections;
using Chronos.Core;
using Chronos.Messaging;

namespace Chronos.Persistence {
	
	public class MessagesFilePersistence : MessagesPersistence {
		
		#region Ctor
		
		public MessagesFilePersistence( PersistenceParameters param ) : base(param)
		{
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Obtém o contentor de mensagens</sumary>
		public Hashtable Container {
			get {
				object state = Universe.instance.PersistenceServices.GetState("--MESSAGES--");
				if( state == null ) {
					state = new Hashtable();
					Universe.instance.PersistenceServices.Register("--MESSAGES--", state);
				}
				return (Hashtable) state;
			}
		}
		
		/// <summary>Indica o número de mensagens não lidas</summary>
		public int Unread {
			get {
				object state = Universe.instance.PersistenceServices.GetState("--UNREAD-MESSAGES--");
				if( state == null ) {
					return 0;
				}
				return (int) state;
			}
			set {
				Universe.instance.PersistenceServices.Register("--UNREAD-MESSAGES--", value);
			}
		}
		
		#endregion
		
		#region Members
		
		/// <summary>Permite Guardar uma mensagem</summary>
		public override void SaveMessage( int id, string identifier, Message message )
		{
			string key = MakeKey(id, identifier);
			ArrayList messages = (ArrayList) Container[key];
			if( messages == null ) {
				messages = new ArrayList();
				Container.Add(key, messages);
			}
			
			messages.Insert(0, message);
			
			++Unread;
		}
		
		/// <summary>Marca todas as mensagens como lidas</summary>
		public override void MarkAllRead( int id, string identifier )
		{
			Unread = 0;
		}
		
		/// <summary>Indica quantas mensagens não lidas existem</summary>
		public override int UnreadCount( int id, string identifier )
		{
			return Unread;
		}
		
		/// <summary>Obtém mensagens</summary>
		public override Message[] GetMessages( int id, string identifier, MessageType[] types, int count )
		{
			string key = MakeKey(id, identifier);
			ArrayList messages = (ArrayList) Container[key];
			if( messages == null ) {
				return new Message[0];
			}
			
			ArrayList list = new ArrayList();
			
			if( messages.Count < count ) {
				count = messages.Count;
			}
			
			for( int i = 0; i < count; ++i ) {
				Message message = (Message) messages[i];
				foreach( MessageType type in types ) {
					if( message.Info.Category == type ) {
						list.Add(message);
						break;
					}
				}
			}
			
			return (Message[]) list.ToArray(typeof(Message));
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Cria um ID</summary>
		private string MakeKey( int id, string identifier )
		{
			return string.Format("{0}-{1}", identifier, id);
		}
		
		#endregion
		
	};
}
