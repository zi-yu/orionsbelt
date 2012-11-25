// created on 3/23/2006 at 3:43 PM

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
	[FactoryKey("BuildingDestroyed")]
	[Serializable]
	public sealed class BuildingDestroyed : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BuildingDestroyed() : base(MessageType.PlanetManagement)
		{
		}

		#endregion
		
		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string planet = args[0];
			
			return "Building '" + args[1].ToString() + "' on planet '" + planet +"' has been destroyed in battle";
		}

		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string planet = args[0];
			string factory = (string) args[1];
			
			return string.Format( localization,
					translator.decorate(planet),
					translator.translate(factory)
				);
		}

		#endregion
			
	};
}
