using System;
using DesignPatterns.Attributes;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica que um Edificio foi construído
	/// </summary>
	[FactoryKey("BattleTripleAttackLeft")]
	[Serializable]
	public sealed class BattleTripleAttackLeft : NotReplyableMessageInfo {

		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BattleTripleAttackLeft() : base(MessageType.Battle) 
			{}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message ) {
			string[] args = message.Args;
			string damage = args[0].ToString();
			string quant = args[1].ToString();
			string type = args[2].ToString();

			return string.Format("Triple Attack attack made {0} of damage to the Left that destroyed {1} {2}.",damage,quant,type);
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator ) {
			string[] args = message.Args;
			string damage = args[0].ToString();
			string quant = args[1].ToString();
			string type = args[2].ToString();

			
			return	string.Format( localization,
						translator.decorate(damage),
						translator.decorate(quant),
						translator.decorate(type)
					);
		}

		#endregion

		
	};
}
