namespace Chronos.Exceptions {
	
	/// <summary>Excepcao em tempo de carregamento da configuraao</summary>
	public class NoShipToRemoveException : ChronosException {

		/// <summary>Construtor com mensagem</summary>
		public NoShipToRemoveException()
			: base("There is no ship of that type to be removed!!!") {}
	};
}
