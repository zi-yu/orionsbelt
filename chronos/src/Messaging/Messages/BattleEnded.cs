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
	[FactoryKey("BattleEnded")]
	[Serializable]
	public sealed class BattleEnded : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BattleEnded() : base(MessageType.Battle)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			
			return "Battle Ended width '" + args[0].ToString() + "' at " + args[1].ToString() + " has ended. You " + args[2].ToString() + " Score: " + args[3].ToString();
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			
			return string.Format( localization,
					translator.decorate(args[0].ToString()),
					translator.decorate(args[1].ToString()),
					translator.translate("Battle"+args[2].ToString()),
					args[3]
			);
		}

		#endregion

		
	};
}
