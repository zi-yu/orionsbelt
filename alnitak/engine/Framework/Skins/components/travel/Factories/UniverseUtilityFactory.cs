using System;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Alnitak {

	/// <summary>
	/// Factory de criação de um objecto TravelPlanet
	/// </summary>
	[FactoryKey("util4")]
	public class UniverseUtilityFactory : UtilitiesFactory {

		private UniverseUtil util = new UniverseUtil();
		
		protected override IUtil CreateIncrementFactory() {
			return util;
		}

	}
}
