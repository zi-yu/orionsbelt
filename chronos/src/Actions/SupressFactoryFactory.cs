// created on 3/7/04 at 11:26 a

using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'supress-factory'</summary>
	[FactoryKey("supress-factory")]
	public class SupressFactoryFactory : ActionFactory {
	
		/// <summary>
		///  Cria SupressFactoryFactory's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new SupressFactory( (string) args["type"], (string) args["value"] );
		}
	
	}
}