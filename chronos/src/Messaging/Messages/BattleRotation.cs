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
	[FactoryKey("BattleRotation")]
	[Serializable]
	public sealed class BattleRotation : NotReplyableMessageInfo {

		#region Ctors

		/// <summary>Construtor de Message</summary>
		public BattleRotation( ) : base(MessageType.Battle) 
			{}

		#endregion

		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string src = args[0].ToString(  );
			string rot = args[1].ToString(  );

			return string.Format("Ship at coordinate {0} made a {1} rotation.",src,rot);
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string src = args[0].ToString(  );
			string rot = args[1].ToString(  );

			
			return	string.Format( localization,
						translator.decorate(src),
						translator.decorate(rot)
					);
		}

		#endregion

		
	};
}
