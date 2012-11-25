// created on 2/23/04 at 3:56 a

using System.Collections;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Chronos.Actions {
	
	/// <summary>Factory da acçaoo 'resource-ref'</summary>
	[FactoryKey("resource-ref")]
	public class ResourceRefFactory : ActionFactory {
	
		/// <summary>
		///  Cria ResourceRef's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new ResourceRef( (string) args["type"], (string) args["value"] );
		}
	
	};
	
}
