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
	[FactoryKey("BattleDamage")]
	[Serializable]
	public sealed class BattleDamage : NotReplyableMessageInfo {

		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BattleDamage() : base(MessageType.Battle) {
		}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string quant = args[0].ToString(  );
			string ship1 = args[1].ToString(  );
			string damage = args[2].ToString(  );
			string destroyed = args[3].ToString(  );
			string ship2 = args[4].ToString(  );

			return string.Format("{0} {1} made {2} of damage which destroyed {3} {4}.", quant,ship1,damage,destroyed,ship2);
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string quant = args[0].ToString(  );
			string ship1 = args[1].ToString(  );
			string damage = args[2].ToString(  );
			string destroyed = args[3].ToString(  );
			string ship2 = args[4].ToString(  );

			
			return	string.Format( localization,
						translator.decorate(quant),
						translator.decorate(ship1),
						translator.decorate(damage),
						translator.decorate(destroyed),
						translator.decorate(ship2)
					);
		}

		#endregion

		
	};
}
