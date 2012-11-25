using System;
using DesignPatterns.Attributes;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um Edificio foi construído
	/// </summary>
	[FactoryKey("BattlePenalty")]
	[Serializable]
	public sealed class BattlePenalty : NotReplyableMessageInfo {

		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BattlePenalty() : base(MessageType.Battle) {
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string penalty = args[0].ToString();
			string distance = args[1].ToString();

			return string.Format("The attack will have a penalty of {0}% because of the distance (was {1})",penalty,distance);
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string penalty = args[0].ToString();
			string distance = args[1].ToString();

			
			return	string.Format( localization,
						translator.decorate(penalty),
						translator.decorate(distance)
					);
		}

		#endregion

		
	};
}
