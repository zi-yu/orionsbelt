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
	[FactoryKey("BattleStarted")]
	[Serializable]
	public sealed class BattleStarted : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BattleStarted() : base(MessageType.Battle)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string ruler = (string) args[0];
			
			return "Battle Started width '" + ruler + "' at " + args[1].ToString();
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string ruler = (string) args[0];
			
			return string.Format( localization,
					translator.decorate(ruler),
					translator.decorate(args[1].ToString())
				);
		}

		#endregion

		
	};
}
