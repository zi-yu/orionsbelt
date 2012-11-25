using System;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Alnitak {

	/// <summary>
	/// Factory de cria��o de um objecto TravelPlanet
	/// </summary>
	[FactoryKey("Travel3")]
	public class TravelSystemFactory : TravelFactory {
		protected override TravelCoordControlBase CreateTravelFactory() {
			return new TravelSystemControl();
		}
	}
}
