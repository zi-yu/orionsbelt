// created on 8/10/2005 at 3:58 PM

using Chronos.Core;
using Chronos.Exceptions;
using Chronos.Resources;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'transformRareResource'</summary>
	[FactoryKey("transformRareResource")]
	public class TransformRareResourceFactory : ActionFactory {
	
		/// <summary>
		///  Cria TransformRareResource's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{		
			string rare = ParseAndCheck( args, "rare", "Rare" );
			string intrinsic = ParseAndCheck( args, "intrinsic", "Intrinsic" );
			int factor = GetInt( args, "factor" );
		
			return new TransformRareResource(rare, intrinsic, factor);
		}
		
		/// <summary>Obtém uma string</summary>
		private string ParseAndCheck( Hashtable args, string key, string category )
		{
			object obj = args[key];
			if( key == null ) {
				throw new LoaderException("Required attribute '"+key+"' not found");
			}
			
			return obj.ToString();
		}
	
	};
}
