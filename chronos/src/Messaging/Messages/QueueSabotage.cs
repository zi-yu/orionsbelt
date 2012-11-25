using System;
using DesignPatterns.Attributes;
using Chronos.Messaging;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um edifício começou a ser construído
	/// </summary>
	[FactoryKey("QueueSabotage")]
	[Serializable]
	public sealed class QueueSabotage : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public QueueSabotage() : base(MessageType.Sabotage)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			return "The `"+args[0].ToString()+"' Queue was Sabotaged. Source: "+args[2]+" Marines Killed:"+args[1];
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator)
		{
			string[] args = message.Args;
			string type = args[0];
			
			return string.Format( localization,
					translator.translate(type), translator.decorate(args[1]), translator.decorate(args[2])
				);
		}

		#endregion
			
	};
}
