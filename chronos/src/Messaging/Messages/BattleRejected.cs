using System;
using DesignPatterns.Attributes;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um Edificio foi construído
	/// </summary>
	[FactoryKey("BattleRejected")]
	[Serializable]
	public sealed class BattleRejected : NotReplyableMessageInfo {

		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BattleRejected() : base(MessageType.Battle) {
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			
			return string.Format("Ruler {0} rejected the friendly battle.",args[0]);
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			
			return	string.Format( localization,
						translator.decorate(args[0])
					);
		}

		#endregion

		
	};
}
