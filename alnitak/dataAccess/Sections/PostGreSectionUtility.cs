using System.Collections;
using System.Data;
using Alnitak.PostGre;
using NpgsqlTypes;

namespace Alnitak {
	/// <summary>
	/// Utilitrio auxiliar  sesso. Contm alguns mtodos para carregar
	/// as sesses da base de dados
	/// </summary>
	public class PostGreSectionUtility : SectionUtilityBase {
			
		/// <summary>
		/// Carrega as seccoes existentes para uma base de dados
		/// </summary>
		/// <returns>um objecto do tipo SectionCollection com todas as collections</returns>
		override public UtilityCollection getAllSectionsFromDB() {
			return storeSections( PostGreServerUtility.getAllFromDB("OrionsBelt_SectionsGetAllSections") );
		}

		public override string[] getAllSectionsRolesFromDB( int section_id ) {
			PostGre.PostGreParam [] param = new PostGreParam[1];
			param[0] = new PostGreParam(section_id,NpgsqlDbType.Integer);
			
			DataSet roles = PostGreServerUtility.getFromDB("OrionsBelt_SectionsGetAllSectionsRoles", param );
			
			int results = roles.Tables[0].Rows.Count;
			string[] rolesArray = new string[results];
			
			for( int i = 0 ; i < results ; ++i ) {
				DataRow row = roles.Tables[0].Rows[i];
				rolesArray[i] = (string)row[0];
			}

			return rolesArray;
		}

	}
}
