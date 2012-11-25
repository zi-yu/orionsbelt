// created on 3/19/04 at 10:52 a

using System;
using System.Data;
using ByteFX.Data;
using ByteFX.Data.MySqlClient;
using Alnitak;

namespace Alnitak {

	/// <summary>
	/// Utilitrio auxiliar  sesso. Contm alguns mtodos para carregar
	/// as sesses da base de dados
	/// </summary>
	public class MySqlSectionUtility : SectionUtilityBase {
			
		/// <summary>
		/// Carrega as seces existentes para uma base de dados
		/// </summary>
		/// <returns>um objecto do tipo SectionCollection com todas as collections</returns>
		public override UtilityCollection getAllSectionsFromDB()
		{
			return storeSections( MySqlUtility.getAll("Sections") );
		}
		
		/// <summary>Retorna todas as roles da BD</summ<ry>
		public override string[] getAllSectionsRolesFromDB( int section_id )
		{
			//string query = "select roles_roleName from SectionRoles inner join Roles on sectionRoles_role_id = IDRoles where sectionroles_section_id  = " + section_id;
		
			//DataSet roles = MySqlUtility.getQuery(query);
			DataSet roles = MySqlUtility.getAll("Roles");
			
			int results = roles.Tables[0].Rows.Count;
			string[] rolesArray = new string[results];
			
			for( int i = 0 ; i < results ; ++i ) {
				DataRow row = roles.Tables[0].Rows[i];
				rolesArray[i] = (string)row["roles_roleName"];
			}

			return rolesArray;
		}
	};
}
