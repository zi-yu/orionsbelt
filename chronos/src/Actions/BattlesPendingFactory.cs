// created on 7/25/2005 at 3:41 PM

using Chronos.Core;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'battles-pending'</summary>
	[FactoryKey("battles-pending")]
	[Signal]
	public class BattlesPendingFactory : ActionFactory {
	
		/// <summary>
		///  Cria BattlesPending's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			return new BattlesPending();
		}
	
	};
}