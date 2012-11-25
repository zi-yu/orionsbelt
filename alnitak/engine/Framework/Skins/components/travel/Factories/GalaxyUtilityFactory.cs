using System;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Alnitak {

	/// <summary>
	/// Factory de criação de um objecto TravelPlanet
	/// </summary>
	[FactoryKey("util3")]
	public class GalaxyUtilityFactory : UtilitiesFactory {

		private GalaxyUtil util = new GalaxyUtil();
		
		protected override IUtil CreateIncrementFactory() {
			return util;
		}

	}
}
