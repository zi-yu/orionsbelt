using System.Collections;
using System.Text;
using Alnitak.News;

using Alnitak.PostGre;

using System.Data;
using NpgsqlTypes;

namespace Alnitak {
	
	public class PostGreNewsUtility : NewsUtility {
		
		#region NewsUtility Implementation
		
		/// <summary>Regista uma nova notícia</summary>
		protected override void Register( Entry entry ) {
			PostGre.PostGreParam [] param = new PostGreParam[3];
			param[0] = new PostGreParam( entry.Title,NpgsqlDbType.Varchar );
			param[1] = new PostGreParam( entry.Content,NpgsqlDbType.Varchar );
			param[2] = new PostGreParam( entry.Language,NpgsqlDbType.Varchar );

			PostGreServerUtility.executeNonQuery2("OrionsBelt_InsertNews",param);
		}
		
		/// <summary>Obtém as notícias existentes</summary>
		protected override NewsList GetNewsFromDB() {
			DataSet ds = PostGreServerUtility.getAllFromDB("OrionsBelt_GetNews");
			return NewsFromDataSet(ds);
		}

		/// <summary>Obtém as notícias existentes por linguagem </summary>
		protected override NewsList GetNewsFromDBByLang( string lang) {
			PostGre.PostGreParam [] param = new PostGreParam[1];
			param[0] = new PostGreParam( lang,NpgsqlDbType.Varchar );

			DataSet ds = PostGreServerUtility.getFromDB("OrionsBelt_GetNewsByLang", param);
			return NewsFromDataSet(ds);
		}
		
		#endregion
		
	};
	
}
