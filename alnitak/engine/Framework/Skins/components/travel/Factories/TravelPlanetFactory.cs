using System;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Alnitak {

	/// <summary>
	/// Factory de cria��o de um objecto TravelPlanet
	/// </summary>
	[FactoryKey("Travel1")]
	public class TravelPlanetFactory : TravelFactory {		
		protected override TravelCoordControlBase CreateTravelFactory() {
			return new TravelPlanetControl();
		}
	}
}
