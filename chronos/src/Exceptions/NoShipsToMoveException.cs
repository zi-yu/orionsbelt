namespace Chronos.Exceptions {
	
	/// <summary>
	/// Excepção que ocorre quando se quer mover uma fleet que 
	/// não tem naves 
	///</summary>
	public class NoShipsToMoveException : ChronosException {

		/// <summary>Construtor com mensagem</summary>
		public NoShipsToMoveException()
			: base("There are no ships to move!!!") {}
	};
}
