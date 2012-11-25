using System;
using System.Collections;
using Alnitak.News;

using Alnitak.MsSqlServer;

using System.Data;

namespace Alnitak {
	
	public class SqlServerNewsUtility : NewsUtility {
		
		#region NewsUtility Implementation
		
		/// <summary>Regista uma nova not�cia</summary>
		protected override void Register( Entry entry ) {
			Hashtable parameters = new Hashtable();
			parameters.Add( "@title", entry.Title );
			parameters.Add( "@content", entry.Content );

			SqlServerUtility.executeNonQuery("OrionsBelt_InsertNews",parameters);
		}
		
		/// <summary>Obt�m as not�cias existentes</summary>
		protected override NewsList GetNewsFromDB() {
			DataSet ds = SqlServerUtility.getAllFromDB("OrionsBelt_GetNews");
			return NewsFromDataSet(ds);
		}

		/// <summary>Obt�m as not�cias existentes por linguagem </summary>
		protected override NewsList GetNewsFromDBByLang( string lang) {
			Hashtable parameters = new Hashtable();
			parameters.Add( "@lang", lang );
			
			DataSet ds = SqlServerUtility.getFromDB("OrionsBelt_GetNewsByLang", parameters);
			
			return NewsFromDataSet(ds);
		}
		
		#endregion
		
	};
	
}
