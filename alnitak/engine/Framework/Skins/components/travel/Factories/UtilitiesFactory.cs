using DesignPatterns;

namespace Alnitak {

	/// <summary>
	/// Base das Factories de travel
	/// </summary>
	public abstract class UtilitiesFactory : IFactory {

		#region IFactory Members

		public object create(object args) {
			return CreateIncrementFactory();
		}

		#endregion

		protected abstract IUtil CreateIncrementFactory();
	}
}
