using System;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Alnitak {

	/// <summary>
	/// Factory de criação de um objecto TravelPlanet
	/// </summary>
	[FactoryKey("util1")]
	public class SectorUtilityFactory : UtilitiesFactory {

		private SectorUtil util = new SectorUtil();
		
		protected override IUtil CreateIncrementFactory() {
			return util;
		}

	}
}
