// created on 9/11/2005 at 12:32 PM

using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'resource-needed'</summary>
	[FactoryKey("reserve-resource")]
	public class ReserveResourcedFactory : ActionFactory {
	
		/// <summary>
		///  Cria ResourceNeeded's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new ReserveResource( (string) args["type"], (string) args["value"] );
		}
	
	}
}
