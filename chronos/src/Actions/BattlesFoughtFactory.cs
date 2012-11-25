// created on 5/22/04 at 12:45 a

using Chronos.Core;
using Chronos.Exceptions;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {
	
	/// <summary>Factory da acaoo 'static'</summary>
	[FactoryKey("battlesFought")]
	public class BattlesFoughtFactory : ActionFactory {
		
		/// <summary>
		///  Cria Static's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			try {
				return new BattlesFought( int.Parse(args["value"].ToString()) );
			} catch {
				throw new LoaderException("battlesFought expecting an integer 'value'");
			}
		}
		
	}
}
