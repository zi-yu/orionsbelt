using System;

namespace Chronos.Exceptions {
	
	/// <summary>Class base das excepçoes do Chronos</summary>
	public class ChronosException : Exception {
	
		/// <summary>Construtor com mensagem</summary>
		public ChronosException( string cause ) :  base(cause)
		{
		}
		
		/// <summary>Construtor por defeito</summary>
		public ChronosException() : this("General Exception")
		{
		}
		
	};
	
}
