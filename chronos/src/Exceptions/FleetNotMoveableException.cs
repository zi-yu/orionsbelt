namespace Chronos.Exceptions {
	
	/// <summary>Excepcao em tempo de carregamento da configuraao</summary>
	public class FleetNotMoveableException : ChronosException {

		/// <summary>Construtor com mensagem</summary>
		public FleetNotMoveableException()
			: base("This Fleet cannot move! Probably this fleet is the default fleet of a planet") {}
	};
}
