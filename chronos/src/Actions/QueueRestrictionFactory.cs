// created on 3/7/04 at 11:26 a

using System;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {
	
	/// <summary>Factory da acaoo 'disallow-build'</summary>
	[FactoryKey("queueRestriction")]
	public class QueueRestrictionFactory : ActionFactory {
		
		/// <summary>
		///  Cria DisallowBuildFactory's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			try {
				
				return new QueueRestriction( (string) args["type"], (string) args["value"]);
				
			} catch( Exception ) {
				throw new Exception("Error creating a QueueRestriction!");
			}
		}
		
	}
}
