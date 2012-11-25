using System;
using System.Collections;
using System.Web;
using Chronos.Utils;

namespace Alnitak { //considerar trocar por application

	/// <summary>
	/// Utilitario auxiliar  seccao. Contem alguns metodos para carregar
	/// as sesses da base de dados
	/// </summary>
	public class SectionUtility {
		private static object lockObj = new object();

		#region static methods

			/// <summary>
			/// retorna a informao da seco baseado no path do pedido
			/// </summary>
			/// <param name="requestPath">path do pedido</param>
			/// <returns>a section do path</returns>
			public static SectionInfo getSectionInfoFromFullPath(string requestPath) {
			
				Log.log("Setting up requestPath '{0}'...", requestPath);
				requestPath = requestPath.Replace( OrionGlobals.InternalAppPath, OrionGlobals.AppPath );
				requestPath = requestPath.Replace( OrionGlobals.InternalAppPath, OrionGlobals.AppPath );
				Log.log("\tGot: '{0}'", requestPath);
			
				UtilityCollection sectionCollection = getAllSections();
				Log.log("Getting SectionInfo for '{0}'", requestPath);
				SectionInfo info = (SectionInfo)sectionCollection[requestPath];
#if DEBUG
				if( info == null ) {
					Log.log("\tNot Found!");
				} else {
					Log.log("\tFound!");
				}
#endif
				return info;
			}
			
        	/// <summary>
			/// retorna a informacao tendo em conta o path base
			/// </summary>
			/// <param name="basePath">path base</param>
			/// <returns>a section do path</returns>
			public static SectionInfo getSectionInfoFromBasePath(string basePath) {
				UtilityCollection sectionCollection = getAllSections();
				
				Log.log("Setting up requestPath '{0}'...", basePath);
				basePath = basePath.Replace( OrionGlobals.InternalAppPath, OrionGlobals.AppPath );
				basePath = basePath.Replace( OrionGlobals.InternalAppPath, OrionGlobals.AppPath );
				Log.log("\tGot: '{0}'", basePath);
				
				return (SectionInfo)sectionCollection [basePath + "default.aspx"];
			}

			/// <summary>
			/// Obtm todas as seccoes existentes na base de dados
			/// </summary>
			/// <returns>um objecto do tipo SectionCollection com todas as collections</returns>
			public static UtilityCollection getAllSections() {
				HttpContext context = HttpContext.Current;

				UtilityCollection sectionCollection = null;
				//SectionCollection sectionCollection = null;
				lock( lockObj ) {
					sectionCollection = (UtilityCollection)context.Cache["sections"];
					if(sectionCollection == null) {
						string value = OrionGlobals.getConfigurationValue("utilities",SectionUtilityBase.getSectionUtilityKey());
						SectionUtilityBase sectionUtilityBase = 
							(SectionUtilityBase)Activator.CreateInstance( Type.GetType( value , true) );
						sectionCollection = sectionUtilityBase.getAllSectionsFromDB();
						context.Cache["sections"] = sectionCollection;
					}
				}
				return sectionCollection;
			}

		#endregion

	}
}
