using System;

namespace Alnitak.Exceptions {
	
	/// <summary>
	/// Classe base das excep��es do Alnitak
	/// </summary>
	public class AlnitakException : Exception {
	
		/// <summary>
		/// Construtor com uma mensagem e com a excep��o que gerou 
		/// a excep��o corrente
		/// </summary>
		/// <param name="cause"></param>
		public AlnitakException( string message, Exception innerException ) : base(message,innerException) {}

		/// <summary>
		/// Construtor com uma mensagem de erro
		/// </summary>
		/// <param name="cause"></param>
		public AlnitakException( string message ) : base(message) {}
		
	};
	
}