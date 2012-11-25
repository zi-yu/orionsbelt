using System.Collections;
using System.Data;
using Alnitak.MsSqlServer;

namespace Alnitak {
	/// <summary>
	/// Utilitrio auxiliar  sesso. Contm alguns mtodos para carregar
	/// as sesses da base de dados
	/// </summary>
	public class SqlServerSectionUtility : SectionUtilityBase {
			
		/// <summary>
		/// Carrega as seccoes existentes para uma base de dados
		/// </summary>
		/// <returns>um objecto do tipo SectionCollection com todas as collections</returns>
		override public UtilityCollection getAllSectionsFromDB() {
			return storeSections( SqlServerUtility.getAllFromDB("OrionsBelt_SectionsGetAllSections") );
		}

		public override string[] getAllSectionsRolesFromDB( int section_id ) {
			Hashtable parameters = new Hashtable();
			parameters.Add( "@section_id", section_id.ToString() );
			DataSet roles = SqlServerUtility.getFromDB("OrionsBelt_SectionsGetAllSectionsRoles", parameters );
			
			int results = roles.Tables[0].Rows.Count;
			string[] rolesArray = new string[results];
			
			for( int i = 0 ; i < results ; ++i ) {
				DataRow row = roles.Tables[0].Rows[i];
				rolesArray[i] = (string)row["roles_roleName"];
			}

			return rolesArray;
		}

	}
}
