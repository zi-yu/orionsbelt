namespace Alnitak {
	
	using System;
	using System.Data;
	using System.Data.SqlClient;

	/// <summary>
	/// Summary description for NamedPageUtilityBase.
	/// </summary>
	public abstract class NamedPageUtilityBase {

		#region static

		/// <summary>
		/// obtém qual a fonte de dados que vai ser utilizada
		/// </summary>
		/// <returns></returns>
		public static string getNamedPageUtilityKey(){
			return OrionGlobals.resolveDataAccessName("namedPageUtility");
		}

		#endregion

		#region protected 
		
			/// <summary>
			/// cria um page info com a informao retirada da base de dados
			/// </summary>
			/// <param name="dr">data reader com toda a informao</param>
			/// <returns>PageInfo com a informao retirada do SqldataReader</returns>
			protected PageInfo populateNamedPages(DataRow page) {
				return new PageInfo (
						(int)page["namedPage_id"],
						-1,
						(string)page["namedPage_name"],
						(string)page["namedPage_title"],
						string.Empty,
						(string)page["namedPage_description"],
						(string)page["namedPage_content"]
					);
			}


			public UtilityCollection storeNamedPages( DataSet dsNamedPages ) {
				UtilityCollection namedPageCollection = new UtilityCollection();
				DataTable dataTable = dsNamedPages.Tables[0];

				foreach( DataRow dataRow in dataTable.Rows ) {
					PageInfo info = populateNamedPages( dataRow );
					//Console.WriteLine("url: "+((string)dataRow["namedPage_path"]) + " skin: " + info.pageSkin);
					namedPageCollection.Add( (string)dataRow["namedPage_path"], info );
				}

				return namedPageCollection;
			}

		#endregion

		public abstract UtilityCollection getAllNamedPagesFromDB();
	}
}
