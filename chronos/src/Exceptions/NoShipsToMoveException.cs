namespace Chronos.Exceptions {
	
	/// <summary>
	/// Excep��o que ocorre quando se quer mover uma fleet que 
	/// n�o tem naves 
	///</summary>
	public class NoShipsToMoveException : ChronosException {

		/// <summary>Construtor com mensagem</summary>
		public NoShipsToMoveException()
			: base("There are no ships to move!!!") {}
	};
}
