// created on 7/25/2005 at 4:24 PM

using Chronos.Core;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'planet-conquest-pending'</summary>
	[FactoryKey("planet-conquest-pending")]
	[Signal]
	public class PlanetConquestPendingFactory : ActionFactory {
	
		/// <summary>
		///  Cria PlanetConquestPending's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new PlanetConquestPending();
		}
	
	};
}