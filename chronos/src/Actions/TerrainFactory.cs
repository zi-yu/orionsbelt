// created on 8/10/2005 at 12:53 PM

using System;
using Chronos.Core;
using Chronos.Exceptions;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acaoo 'terrain'</summary>
	[FactoryKey("terrain")]
	public class TerrainFactory : ActionFactory {
	
		/// <summary>
		///  Cria Static's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			string type = "null";
		
			try {			
					
				type = args["type"].ToString();
				
				Terrain target = null;
				foreach( Terrain terrain in Terrain.All ) {
					if( terrain.Description == type ) {
						target = terrain;
					}
				}
				
				if( target == null ) {
					string msg = "Got: ";
					foreach( Terrain terrain in Terrain.All ) {
						msg += terrain.Description;
					}
					throw new Exception(msg);
				}
			
				return new TerrainAction( target );
			} catch( Exception ex) {
				string msg = string.Format("Invalid terrain type: {0} [{1}]", type, ex.ToString());
				throw new LoaderException(msg);
			}
		}
	
	}
}
