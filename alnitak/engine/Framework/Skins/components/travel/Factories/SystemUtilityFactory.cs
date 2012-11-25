using System;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Alnitak {

	/// <summary>
	/// Factory de criação de um objecto TravelPlanet
	/// </summary>
	[FactoryKey("util2")]
	public class SystemUtilityFactory : UtilitiesFactory {
		
		private SystemUtil util = new SystemUtil();
		
		protected override IUtil CreateIncrementFactory() {
			return util;
		}
	}
}
