
using System;
using System.Collections;
using Chronos.Persistence;

namespace Chronos.Messaging {
	
	/// <summary>
	/// Uma mensagem para comunicação assíncrona entre entidades
	/// que implementem IMessageHandler
	/// </summary>
	[Serializable]
	public abstract class MessageManager : IMessageHandler {
		
		#region Abstract Methods
		
		/// <summary>Indica um identificador deste Handler</summary>
		public abstract int HandlerId {get;}
		
		/// <summary>Indica uma string que identifica o tipo deste handler (ruler, planet, ...)</summary>
		public abstract string HandlerIdentifier {get;}
		
		#endregion

		#region IMessageHandler Implementation

		/// <summary>Aceita uma mensagem e armazena-a</summary>
		public virtual void acceptMessage( Message message )
		{
			MessagesPersistence.Instance.SaveMessage(HandlerId, HandlerIdentifier, message);
		}

		/// <summary>Marca todas as mensagens como lidas</summary>
		public virtual void markAllRead()
		{
			MessagesPersistence.Instance.MarkAllRead(HandlerId, HandlerIdentifier);
		}
		
		/// <summary>Indica a quantidade de mensagens n�o lidas armazenadas</summary>
		public int UnreadCount {
			get {
				return MessagesPersistence.Instance.UnreadCount(HandlerId, HandlerIdentifier);
			}
		}

		/// <summary>Indica todas as mensages armazenadas</summary>
		public virtual Message[] GetMessages( MessageType[] types, int quant )
		{
			return MessagesPersistence.Instance.GetMessages(HandlerId, HandlerIdentifier, types, quant);
		}
		
		#endregion
		
	};
}
