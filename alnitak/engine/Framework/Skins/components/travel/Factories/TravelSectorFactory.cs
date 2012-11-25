using System;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Alnitak {

	/// <summary>
	/// Factory de criação de um objecto TravelPlanet
	/// </summary>
	[FactoryKey("Travel2")]
	public class TravelSectorFactory : TravelFactory {
		protected override TravelCoordControlBase CreateTravelFactory() {
			return new TravelSectorControl();
		}
	}
}
