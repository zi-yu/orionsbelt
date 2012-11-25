// created on 3/10/04 at 10:55 a

namespace Chronos.Exceptions {
	
	/// <summary>Excepao em tempo de carregamento da configuraao</summary>
	public class RuntimeException : ChronosException {
		
		/// <summary>Construtor com mensagem</summary>
		public RuntimeException( string cause ) : base(cause )
		{
		}
		
	};
	
}
