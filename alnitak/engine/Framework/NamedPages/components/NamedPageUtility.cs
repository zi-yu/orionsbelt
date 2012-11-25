namespace Alnitak {

	using System;
	using System.Web;
	using System.Collections;
	using Chronos.Utils;

	/// <summary>
	/// Summary description for NamedPageUtility.
	/// </summary>
	public class NamedPageUtility {

		#region static methods

			/// <summary>
			/// obtm uma NamedPageCollection com todas as namedPages
			/// </summary>
			/// <returns>a collections com as NamedPages</returns>
			public static UtilityCollection getAllNamedPages() {
				HttpContext context = HttpContext.Current;
				UtilityCollection namedPageCollection = (UtilityCollection)context.Cache["NamedPages"];
				if( namedPageCollection == null ) {
					string value = OrionGlobals.getConfigurationValue("utilities",NamedPageUtilityBase.getNamedPageUtilityKey());

					NamedPageUtilityBase namedPageUtilityBase =
						(NamedPageUtilityBase)Activator.CreateInstance( Type.GetType( value , true) );
					namedPageCollection = namedPageUtilityBase.getAllNamedPagesFromDB();

#if DEBUG
					Log.log("---- NamedPages ----");
					IDictionaryEnumerator it = namedPageCollection.GetEnumerator();
					while( it.MoveNext() ) {
						PageInfo info = (PageInfo) it.Value;
						Log.log("{0} - {1}", it.Key, info.pageName );
					}
#endif
					
					context.Cache["NamedPages"] = namedPageCollection;
				}
				return namedPageCollection;
			}


			/// <summary>
			/// retorna a informao da seco baseado no path do pedido
			/// </summary>
			/// <param name="requestedFile">path do pedido</param>
			/// <returns>a section do path</returns>
			public static PageInfo getNamedPageInfo(string requestedFile) {
				UtilityCollection namedPageCollection = getAllNamedPages();
				return (PageInfo)namedPageCollection[requestedFile];
			}

		#endregion

	}
}
