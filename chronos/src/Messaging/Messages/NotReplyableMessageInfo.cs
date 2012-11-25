
using DesignPatterns;
using DesignPatterns.Attributes;
using Chronos.Messaging;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Tipo de mensagem que não permite resposta
	/// </summary>
	[NoAutoRegister()]
	public abstract class NotReplyableMessageInfo : MessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public NotReplyableMessageInfo( MessageType type ) : base(type)
		{
		}

		#endregion

		#region MessageInfo Implementation

		/// <summary>Indica se é possível enviar uma resposta a mensagens deste tipo</summary>
		public override bool MayReply { 
			get { return false; }
		}

		#endregion
			
	};
}
