using System;
using DesignPatterns;

namespace Alnitak {

	/// <summary>
	/// Base das Factories de travel
	/// </summary>
	public abstract class TravelFactory : IFactory {

		#region IFactory Members

		public object create(object args) {
			return CreateTravelFactory();
		}

		#endregion

		protected abstract TravelCoordControlBase CreateTravelFactory();
	}
}
