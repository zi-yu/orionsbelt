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
	[FactoryKey("FleetArrived")]
	[Serializable]
	public sealed class FleetArrived : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public FleetArrived() : base(MessageType.FleetManagement)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message ) {
			string[] args = message.Args;
				
			string f = args[0];

			return string.Format( "Fleet {0} arrived at {1}", f, args[2] );
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator ) {
			string[] args = message.Args;
			
			string f = args[0];
			
			return string.Format( localization,
				translator.decorate(f),
				translator.translate("fleetArrived_coordinate"),
				translator.decorate( args[2] )
			);

		}

		#endregion

		
	};
}
