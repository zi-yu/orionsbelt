// created on 19-01-2005 at 14:46

using System;
using DesignPatterns;
using DesignPatterns.Attributes;
using Chronos.Messaging;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um planeta recebeu um prémio
	/// </summary>
	[FactoryKey("PlanetPrize")]
	[Serializable]
	public sealed class PlanetPrize : NotReplyableMessageInfo {
	
		#region Ctors

		/// <summary>Construtor de Message</summary>
		public PlanetPrize() : base(MessageType.Prize)
		{
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;

			string planet = args[1];
			string turn = args[2].ToString();
			
			return "Prize " + args[0].ToString() + " to planet " + planet + " on turn " + turn;
		}

		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;

			string prize = args[0].ToString();
			string turn = args[1].ToString();
			string medal = args[2].ToString();
			
			return string.Format( localization,
					translator.operate(prize),
					translator.decorate(turn),
					translator.operate(medal)
				);
		}

		#endregion
			
	};
}
