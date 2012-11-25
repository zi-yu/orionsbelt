using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {
	
	/// <summary>Factory da acaoo 'addRatio'</summary>
	[FactoryKey("addRatio")]
	public class AddRatioFactory : ActionFactory {
		
		/// <summary>
		///  Cria AddRatio's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new AddRatio( args["type"].ToString(), args["value"].ToString() );
		}
		
	}
}
