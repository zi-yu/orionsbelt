// created on 3/12/04 at 11:54 a

using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'add'</summary>
	[FactoryKey("add")]
	public class AddFactory : ActionFactory {
	
		/// <summary>
		///  Cria Add's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new Add( (string) args["type"], (string) args["resource"], (string) args["value"] );
		}
	
	}
}
