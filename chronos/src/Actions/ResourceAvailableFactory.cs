// created on 3/17/04 at 6:45 a

using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'resource-available'</summary>
	[FactoryKey("resource-available")]
	public class ResourceAvailableFactory : ActionFactory {
	
		/// <summary>
		///  Cria ResourceAvailableFactory's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new ResourceAvailable( 
					(string) args["type"], 
					(string) args["resource"],
					int.Parse( (string) args["value"]) 
				);
		}
	
	}
}