namespace Alnitak {
	
	using System;
	using System.Data;
	using System.Data.SqlClient;

	using Alnitak.Exceptions;

	/// <summary>
	/// MasterSkinUtilityBase.
	/// </summary>
	public abstract class MasterSkinUtilityBase {

		public static string getMasterSkinUtilityKey(){
			return OrionGlobals.resolveDataAccessName("masterSkinUtility");
		}
		
		public UtilityCollection storeSkins( DataSet dsSkins ) {
			UtilityCollection masterSkinsCollection = new UtilityCollection();
			DataTable dataTable = dsSkins.Tables[0];
			
			if( dataTable.Rows.Count == 0 )
				throw new AlnitakException("Não existem skins disponiveis na base de dados!!!");

			foreach( DataRow dataRow in dataTable.Rows ) {
				masterSkinsCollection.Add((int)dataRow["masterSkin_id"],new MasterSkinInfo(dataRow) );
			}
			return masterSkinsCollection;
		}

		public abstract UtilityCollection getAllMasterSkinsFromDB();

	}
}
