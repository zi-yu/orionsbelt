// created on 9/4/2005 at 12:10 PM

using Chronos.Core;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'housingAvailable'</summary>
	[FactoryKey("housingAvailable")]
	public class HousingAvailableFactory : ActionFactory {
	
		/// <summary>
		///  Cria HousingAvailable's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new HousingAvailable();
		}
	
	};
}
