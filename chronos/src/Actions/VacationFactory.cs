// created on 9/14/2005 at 9:07 AM

using Chronos.Core;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'vacation'</summary>
	[FactoryKey("vacation")]
	[Signal]
	public class VacationFactory : ActionFactory {
	
		/// <summary>
		///  Cria Vacation's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new Vacation();
		}
	
	};
}
