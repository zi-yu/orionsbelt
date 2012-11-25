using System;
using System.Data;
using Alnitak.News;

namespace Alnitak {
	
	public class MySqlNewsUtility : NewsUtility {
		
		#region NewsUtility Implementation
		
		/// <summary>Regista uma nova notícia</summary>
		protected override void Register( Entry entry )
		{
			/*CREATE TABLE News (
				Id int NOT NULL auto_increment,
			Title varchar(100) NOT NULL,
			Content mediumtext NOT NULL,
			Date datetime NOT NULL default '0000-00-00 00:00:00',
			PRIMARY KEY  (Id)
			) TYPE=MyISAM;
			*/
			string query = string.Format("INSERT INTO News(Title, Content, Date) VALUES('{0}', '{1}', NOW())",
										 entry.Title,
										entry.Content
								);
			MySqlUtility.executeNonQuery(query);
		}
		
		/// <summary>Obtém as notícias existentes</summary>
		protected override NewsList GetNewsFromDB()
		{
			DataSet ds = MySqlUtility.getQuery( "SELECT * FROM News ORDER BY Date DESC LIMIT 0, 10" );
			return NewsFromDataSet(ds);
		}

		protected override NewsList GetNewsFromDBByLang( string lang) {
			return GetNewsFromDB();
		}
		
		#endregion
		
	};
	
}
