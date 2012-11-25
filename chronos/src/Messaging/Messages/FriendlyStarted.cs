using System;
using DesignPatterns.Attributes;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um Edificio foi construído
	/// </summary>
	[FactoryKey("FriendlyStarted")]
	[Serializable]
	public sealed class FriendlyStarted : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public FriendlyStarted() : base(MessageType.Battle)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string ruler = (string) args[0];
			
			return "Friendly Battle Started width ruler '" + ruler +"'";
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string ruler = (string) args[0];
			
			return	string.Format( localization,
						translator.decorate(ruler)
					);
		}

		#endregion

		
	};
}
