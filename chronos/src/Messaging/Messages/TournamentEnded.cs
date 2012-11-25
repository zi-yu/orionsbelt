using System;
using DesignPatterns;
using DesignPatterns.Attributes;
using Chronos.Messaging;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um Edificio foi construído
	/// </summary>
	[FactoryKey("TournamentEnded")]
	[Serializable]
	public sealed class TournamentEnded : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public TournamentEnded() : base(MessageType.Battle)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			
			return "Tournament Battle Ended width '" + args[0].ToString();
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
