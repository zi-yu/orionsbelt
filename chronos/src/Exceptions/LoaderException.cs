// created on 2/23/04 at 8:44 a

namespace Chronos.Exceptions {
	
	/// <summary>Excepao em tempo de carregamento da configuraao</summary>
	public class LoaderException : ChronosException {
		
		/// <summary>Construtor com mensagem</summary>
		public LoaderException( string cause ) : base(cause )
		{
		}
		
	};
	
}
