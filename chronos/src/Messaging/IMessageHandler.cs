using System.Collections;

namespace Chronos.Messaging {
	
	/// <summary>
	/// Identifica uma entidade que pode receber mensagens
	/// </summary>
	public interface IMessageHandler {
		
		/// <summary>Indica um identificador deste Handler</summary>
		int HandlerId {get;}
		
		/// <summary>Indica uma string que identifica o tipo deste handler (ruler, planet, ...)</summary>
		string HandlerIdentifier {get;}
		
		/// <summary>Aceita uma mensagem e armazena-a</summary>
		void acceptMessage( Message message );

		/// <summary>Marca todas as mensagens como lidas</summary>
		void markAllRead();
		
		/// <summary>Indica a quantidade de mensagens nÃ£o lidas armazenadas</summary>
		int UnreadCount { get; }
	
		/// <summary>Indica as últimas mensagens de determinados tipos</summary>
		Message[] GetMessages( MessageType[] types, int quant );
		
	};
}
