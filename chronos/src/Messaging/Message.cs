
using System;
using System.Collections;
using Chronos.Core;

namespace Chronos.Messaging {
	
	/// <summary>
	/// Uma mensagem para comunicação assíncrona entre entidades
	/// que implementem IMessageHandler
	/// </summary>
	[Serializable]
	public sealed class Message {
	
		#region Instance Fields

		private MessageInfo info;
		private string[] args;
		private int turn;

		#endregion

		#region Ctors

		/// <summary>Ctor sem parâmetros</summary>
		public Message()
		{
			info = null;
			args = null;
			turn = Universe.instance.TurnCount;
		}

		/// <summary>Construtor de Message</summary>
		public Message( MessageInfo _info, string[] _args )
		{
			info = _info;
			args = _args;
			turn = Universe.instance.TurnCount;
		}

		#endregion

		#region Instance Properties

		/// <summary>Indica o tipo da mensagem</summary>
		public MessageInfo Info {
			get { return info; }
		}

		/// <summary>Indica os argumentos extra relativos a MessageInfo</summary>
		public string[] Args {
			get { return args; }
		}
		
		/// <summary>Indica em que turno ocorreu a mensagem</summary>
		public int Turn {
			get { return turn; }
		}

		#endregion
		
		#region Information Methods
		
		/// <summary>Retorna uma string que descreve a mensagem</summary>
		public override string ToString()
		{
			if( info == null ) {
				return string.Empty;
			}
			return info.log(this);
		}
		
		#endregion
			
	};
}
