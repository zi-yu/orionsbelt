// created on 5/22/04 at 12:45 a

using Chronos.Core;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'static'</summary>
	[FactoryKey("static")]
	public class StaticFactory : ActionFactory {
	
		/// <summary>
		///  Cria Static's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new Static( Loader.checkTrue(args["evaluate"]), Loader.checkTrue(args["action"]) );
		}
	
	};
}
