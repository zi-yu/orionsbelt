namespace Alnitak {
	
	using System;
	using System.Data;
	using System.Data.SqlClient;

	using Alnitak.MsSqlServer;
	
	/// <summary>
	/// Summary description for SqlServerMasterSkinUtility.
	/// </summary>
	public class SqlServerMasterSkinUtility : MasterSkinUtilityBase {

		override public UtilityCollection getAllMasterSkinsFromDB(){
			return storeSkins( SqlServerUtility.getAllFromDB("OrionsBelt_MasterSkinsGetAllMasterSkins") );
		}
	}
}
