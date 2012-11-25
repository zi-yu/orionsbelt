// created on 3/29/04 at 9:42 a

namespace Alnitak {

	public class MySqlMasterSkinUtility : MasterSkinUtilityBase {
	
		public override UtilityCollection getAllMasterSkinsFromDB()
		{
			return storeSkins( MySqlUtility.getAll("MasterSkins") );
		}
	
	};

}