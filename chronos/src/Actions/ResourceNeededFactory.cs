// created on 2/29/04 at 9:49 a

using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'resource-needed'</summary>
	[FactoryKey("resource-needed")]
	public class ResourceNeededFactory : ActionFactory {
	
		/// <summary>
		///  Cria ResourceNeeded's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new ResourceNeeded( (string) args["type"], (string) args["value"] );
		}
	
	}
}
