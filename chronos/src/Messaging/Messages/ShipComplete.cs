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
	[FactoryKey("ShipCompleted")]
	[Serializable]
	public sealed class ShipCompleted : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public ShipCompleted() : base(MessageType.FleetManagement)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string planet = (string) args[0];
			
			return "Construction of " + args[2].ToString() + " '" + args[1].ToString() + "'s on planet '" + planet +"' is terminated";
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			object[] args = message.Args;
			string planet = (string) args[0];
			string factory = (string) args[1];
			
			return string.Format( localization,
					translator.translate(factory),
					translator.decorate(planet),
					translator.decorate(args[2].ToString())
				);
		}

		#endregion

		
	};
}
