namespace Alnitak {
	
	using System.Web;
	using System.Collections.Specialized;
    using System.Data;
	
	using Alnitak.Exceptions;
	
	/// <summary>
	/// Summary description for SectionUtilityBase.
	/// </summary>
	public abstract class SectionUtilityBase {
		
		#region protected methods

			/// <summary>
			/// retorna o path da secco baseado nos pais que tem
			/// </summary>
			/// <param name="drowSection"></param>
			/// <param name="dstSections"></param>
			/// <returns>o path completo da aplicao</returns>
			protected string calculateSectionPath(DataRow section, DataSet dstSections) {

				string strPath = "";
				DataRow[] drowMatches = { section };
				while ( (int)drowMatches[0]["section_parentId"]!=-1) {
					//vai adicionando ao path o nome de todos os pais at ao paiz
					strPath = System.Web.HttpUtility.UrlEncode( (string)drowMatches[0]["section_name"] ) + "/" +strPath;

					string strMatchString = "section_id=" + drowMatches[0]["section_parentID"].ToString();
					drowMatches = dstSections.Tables[0].Select( strMatchString );
					if( drowMatches == null || drowMatches.Length == 0 ) {
						throw new AlnitakException( string.Format("No existe nenhuma seco com o {0} @ SectionUtilityBase::calculateSectionPath",strMatchString) );
					}
				}
				//no fim adiciona o path da applicao
				return OrionGlobals.AppPath + strPath.ToLower();
			}
			
			/// <summary>
			/// retorna a o valor da propriedade herdada
			/// </summary>
			/// <param name="intSectionID"></param>
			/// <param name="strColName"></param>
			/// <param name="dstSections"></param>
			/// <returns></returns>
			protected object getInheritedField(int intSectionID, string strColName, DataSet dstSections) {
				//procura a strColName na section corrente.Se no encontrar procura
				//em todos os seus pais at encontrar um estilo que na pior
				//das hipoteses ser o do PAIZO :P
				
				string strMatchString = "section_id=" + intSectionID.ToString();
				DataRow[] drowMatches = dstSections.Tables[0].Select( strMatchString );

				if( drowMatches == null || drowMatches.Length == 0 ) {
					throw new AlnitakException( string.Format("No existe nenhuma seco com o {0} @ SectionUtilityBase::getInheritedField ",strMatchString) );
				}

				while ( drowMatches[0][strColName] == System.DBNull.Value && (int)drowMatches[0]["section_parentId"]!=-1) {
					strMatchString = "section_id=" + drowMatches[0]["section_parentId"].ToString();
					drowMatches = dstSections.Tables[0].Select( strMatchString );
					if( drowMatches == null || drowMatches.Length == 0 ) {
						throw new AlnitakException( string.Format("No existe nenhuma seco com o {0}  @ SectionUtilityBase::getInheritedField",strMatchString) );
					}
				}

				object result = drowMatches[0][strColName];
				if( result == null ) {
					throw new AlnitakException( string.Format("A tabela seco no possui a coluna {0} @ SectionUtilityBase::getInheritedField ",strColName) );
				}

				return result;
			}

			protected int getInheritedSkinId( DataRow section, DataSet sections ) {
				return (int)getInheritedField( (int)section["section_id"], "section_skin", sections );
				
			}
		
			/// <summary>
			/// 
			/// </summary>
			/// <param name="sections"></param>
			/// <returns>a collection com a informao de todas as seces</returns>
			protected UtilityCollection storeSections(DataSet dsSections ) {
				if( dsSections == null || dsSections.Tables.Count == 0 ) {
					throw new AlnitakException("O DataSet com as seccoes  invalido  @ SectionUtilityBase::storeSections");
				}

				SectionCollection sectionCollection = new SectionCollection();

				DataTable sections = dsSections.Tables[0];

				//StringDictionary de urls de seces
				StringDictionary urls = new StringDictionary();
						
				// adicionar o path visto n estar na base de dados
				sections.Columns.Add("section_path");
				sections.Columns.Add("section_skinId");
		        
				// Calculated inherited properties for each section
				foreach (DataRow section in sections.Rows) {
						string path = calculateSectionPath( section, dsSections );

						section["section_path"] = path + "default.aspx";
						section["section_skinId"] = getInheritedSkinId( section, dsSections );
				
						string[] roles = getAllSectionsRolesFromDB( (int)section["section_id"] );

						// Add to section collection
						sectionCollection.Add( (string)section["section_path"], new SectionInfo( section, roles ) );

						urls.Add( section["section_name"].ToString(), path ); 
				}

				HttpContext.Current.Cache["SectionUrl"] = urls;

				return sectionCollection;
			}

        #endregion

		public static string getSectionUtilityKey(){
			return OrionGlobals.resolveDataAccessName("sectionUtility");
		}

		public abstract UtilityCollection getAllSectionsFromDB();
		public abstract string[] getAllSectionsRolesFromDB( int section_id );
	}
}
