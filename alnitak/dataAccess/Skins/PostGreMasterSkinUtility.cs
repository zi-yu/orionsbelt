using Alnitak.PostGre;

namespace Alnitak {
	/// <summary>
	/// Summary description for SqlServerMasterSkinUtility.
	/// </summary>
	public class PostGreMasterSkinUtility : MasterSkinUtilityBase {

		override public UtilityCollection getAllMasterSkinsFromDB(){
			return storeSkins( PostGreServerUtility.getAllFromDB("OrionsBelt_MasterSkinsGetAllMasterSkins") );
		}
	}
}
