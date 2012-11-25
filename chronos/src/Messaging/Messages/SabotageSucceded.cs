// created on 9/7/2005 at 10:37 AM


using System;
using DesignPatterns.Attributes;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um edifício começou a ser construído
	/// </summary>
	[FactoryKey("SabotageSucceded")]
	[Serializable]
	public sealed class SabotageSucceded : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public SabotageSucceded() : base(MessageType.Sabotage)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			return args[0] +" Sabotage Attempt to `"+args[1]+"'succeded - " + args[2] + " Marines Killed";
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator)
		{
			string[] args = message.Args;
			string type = args[0];
			string coordinate = args[1];
			
			return string.Format( localization,
					translator.operate(type), translator.decorate(coordinate), translator.decorate(args[2])
				);
		}

		#endregion
			
	};
}
