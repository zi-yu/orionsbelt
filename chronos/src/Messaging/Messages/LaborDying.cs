// created on 13-01-2005 at 18:10

using System;
using DesignPatterns;
using DesignPatterns.Attributes;
using Chronos.Messaging;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um edifício começou a ser construído
	/// </summary>
	[FactoryKey("LaborDying")]
	[Serializable]
	public sealed class LaborDying : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public LaborDying() : base(MessageType.Alert)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;

			string planet = args[0];
			string reason = args[1].ToString();
			
			return "Labor dying in " + planet + " due to the lack of " + reason;
		}

		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;

			string planet = args[0];
			string reason = args[1].ToString();
			
			return string.Format( localization,
					translator.decorate(planet),
					translator.translate(reason)
				);
		}

		#endregion
			
	};
}
