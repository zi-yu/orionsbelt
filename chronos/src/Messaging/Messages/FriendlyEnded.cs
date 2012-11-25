using System;
using DesignPatterns.Attributes;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um Edificio foi construído
	/// </summary>
	[FactoryKey("FriendlyEnded")]
	[Serializable]
	public sealed class FriendlyEnded : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public FriendlyEnded() : base(MessageType.Battle)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			
			return "Friendly Battle Ended width ruler '" + args[0].ToString()+"'";
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string enemy = (string) args[0];
			string result = (string)args[1];
			
			return string.Format( localization,
					translator.decorate(enemy),
					translator.translate("Battle"+result)
				);
		}

		#endregion

		
	};
}
