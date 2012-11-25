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
	[FactoryKey("BattleAttack")]
	[Serializable]
	public sealed class BattleAttack : NotReplyableMessageInfo {

		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BattleAttack() : base(MessageType.Battle) {
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string src = args[0].ToString(  );
			string dst = args[1].ToString(  );

			return "Ship in coordinate" + src + "attacked the ship at the position" + dst + ".";
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string src = args[0].ToString(  );
			string dst = args[1].ToString(  );

			
			return	string.Format( localization,
						translator.decorate(src),
						translator.decorate(dst)
					);
		}

		#endregion

		
	};
}
