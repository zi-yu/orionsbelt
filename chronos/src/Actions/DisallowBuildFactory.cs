// created on 3/7/04 at 11:26 a

using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'disallow-build'</summary>
	[FactoryKey("disallow-build")]
	public class DisallowBuildFactory : ActionFactory {
	
		/// <summary>
		///  Cria DisallowBuildFactory's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new DisallowBuild( args["category"].ToString(), args["factory"].ToString() );
		}
	
	}
}
