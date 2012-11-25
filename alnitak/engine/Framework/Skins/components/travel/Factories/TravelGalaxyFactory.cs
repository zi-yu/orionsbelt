using System;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Alnitak {

	/// <summary>
	/// Factory de cria��o de um objecto TravelPlanet
	/// </summary>
	[FactoryKey("Travel4")]
	public class TravelGalaxyFactory : TravelFactory {		
		protected override TravelCoordControlBase CreateTravelFactory() {
			return new TravelGalaxyControl();
		}
	}
}
