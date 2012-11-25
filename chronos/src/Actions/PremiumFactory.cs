// created on 9/14/2005 at 10:09 AM

using Chronos.Core;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'premium'</summary>
	[FactoryKey("premium")]
	public class PremiumFactory : ActionFactory {
	
		/// <summary>
		///  Cria Premium's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new Premium();
		}
	
	};
}