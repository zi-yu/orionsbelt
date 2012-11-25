using Alnitak.PostGre;

namespace Alnitak {
	
    public class PostGreNamedPageUtility : NamedPageUtilityBase {
		
		/// <summary>
		/// retorna a informao das namedPages da Base de Dados
		/// </summary>
		/// <returns>collection com as namedPages</returns>
		override public UtilityCollection getAllNamedPagesFromDB() {
			return storeNamedPages( PostGreServerUtility.getAllFromDB("OrionsBelt_NamedPagesGetAllNamedPages") );
		}
	}
}
