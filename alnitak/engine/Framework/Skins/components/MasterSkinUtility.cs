namespace Alnitak {

	using System;
	using System.Web;

	/// <summary>
	/// Summary description for MasterSkinUtility.
	/// </summary>
	public class MasterSkinUtility {
		private static object lockObj = new object();

		public static UtilityCollection getAllMasterSkins() {
			HttpContext context = HttpContext.Current;

			UtilityCollection masterSkinCollection = (UtilityCollection)context.Cache["masterSkins"];
			
			lock( lockObj ) {
				if(masterSkinCollection == null) {
					string value = OrionGlobals.getConfigurationValue("utilities",MasterSkinUtilityBase.getMasterSkinUtilityKey());
					
					MasterSkinUtilityBase masterSkinUtilityBase = 
						(MasterSkinUtilityBase)Activator.CreateInstance( Type.GetType( value , true) );
					masterSkinCollection = masterSkinUtilityBase.getAllMasterSkinsFromDB();
					context.Cache["masterSkins"] = masterSkinCollection;
				}
			}

			return masterSkinCollection;
		}

		public static MasterSkinInfo getMasterSkinInfoFromId( int id ) {
			UtilityCollection masterSkinCollection = getAllMasterSkins();
			return (MasterSkinInfo)masterSkinCollection[id];
		}

		public static MasterSkinInfo getDefaultMasterSkinInfo() {
			return getMasterSkinInfoFromId(1);
		}
	}
}
