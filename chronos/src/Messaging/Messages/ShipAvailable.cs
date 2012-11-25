using System;
using DesignPatterns;
using DesignPatterns.Attributes;
using Chronos.Messaging;
using Chronos.Core;
using Chronos.Utils;
using Chronos.Resources;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um edifício começou a ser construído
	/// </summary>
	[FactoryKey("ShipAvailable")]
	[Serializable]
	public sealed class ShipAvailable : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public ShipAvailable() : base(MessageType.FleetManagement)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string planet = (string) args[0];
			
			return "New ship category '"+args[1].ToString()+"' available on planet '"+planet+"'";
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string planet = (string) args[0];
			string factory = (string) args[1];
			
			return string.Format( localization,
				translator.decorate(planet),
				translator.translate(factory)
			);
		}

		#endregion
			
	};
}
