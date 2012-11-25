// created on 9/6/2005 at 6:08 PM

using Chronos.Core;
using Chronos.Exceptions;
using Chronos.Resources;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'transformRareResource'</summary>
	[FactoryKey("transform")]
	public class TransformFactory : ActionFactory {
	
		/// <summary>
		///  Cria TransformRareResource's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{		
			string input = ParseAndCheck( args, "input" );
			string output = ParseAndCheck( args, "output" );
			int factor = GetInt( args, "factor" );
		
			return new Transform(input, output, factor);
		}
		
		/// <summary>Obtém uma string</summary>
		private string ParseAndCheck( Hashtable args, string key )
		{
			object obj = args[key];
			if( key == null ) {
				throw new LoaderException("Required attribute '"+key+"' not found");
			}
			
			return obj.ToString();
		}
	
	};
}
