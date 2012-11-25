namespace Chronos.Exceptions {
	
	/// <summary>Excepcao em tempo de carregamento da configuraao</summary>
	public class AlreadyInMoveException : ChronosException {

		/// <summary>Construtor com mensagem</summary>
		public AlreadyInMoveException()
			: base("this fleet is already in move!") {}
	};
}
