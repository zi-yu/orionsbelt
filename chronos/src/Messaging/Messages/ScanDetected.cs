using System;
using DesignPatterns;
using DesignPatterns.Attributes;
using Chronos.Messaging;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Messaging.Messages {
	
	/// <summary>
	/// Mensagem que indica um scan detectado
	/// </summary>
	[FactoryKey("ScanDetected")]
	[Serializable]
	public sealed class ScanDetected : NotReplyableMessageInfo {
		
		#region Ctors
		
		/// <summary>Construtor de Message</summary>
		public ScanDetected() : base(MessageType.Scan)
		{
		}
		
		#endregion
		
		#region MemberInfo Implementation Members
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public override string log( Message message )
		{
			string[] args = message.Args;
			string planet = args[0];
			string other = args[1];
			string succeded = args[2];
			
			return "Scan detected from coordinate '" + planet +"' to planet '"+other+"'; succeded: " + succeded;
		}
		
		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public override string localize( Message message, string localization, ITranslator translator )
		{
			string[] args = message.Args;
			string planet = (string) args[0];
			string other = (string) args[1];
			string succeded = (string) args[2];
			
			return string.Format( localization,
								 translator.decorate(planet),
								 translator.decorate(other),
								 translator.operate(succeded)
							);
		}
		
		#endregion
		
	};
}
