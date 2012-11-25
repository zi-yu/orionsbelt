namespace Alnitak {

	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Web;

	using Alnitak.MsSqlServer;

	/// <summary>
	/// Summary description for SqlServerNamedPage.
	/// </summary>
	public class SqlServerNamedPageUtility : NamedPageUtilityBase {
		
		/// <summary>
		/// retorna a informao das namedPages da Base de Dados
		/// </summary>
		/// <returns>collection com as namedPages</returns>
		override public UtilityCollection getAllNamedPagesFromDB() {
			return storeNamedPages( SqlServerUtility.getAllFromDB("OrionsBelt_NamedPagesGetAllNamedPages") );
		}
	}
}
