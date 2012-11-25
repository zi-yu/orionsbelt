// created on 4/7/04 at 11:16 a

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Alnitak {

	/// <summary>
	/// Summary description for SqlServerNamedPage.
	/// </summary>
	public class MySqlNamedPageUtility : NamedPageUtilityBase {
		
		/// <summary>
		/// retorna a informao das namedPages da Base de Dados
		/// </summary>
		/// <returns>collection com as namedPages</returns>
		public override UtilityCollection getAllNamedPagesFromDB() {
			DataSet result = MySqlUtility.getAll("NamedPage");
			return storeNamedPages(result);
		}
	};
}
