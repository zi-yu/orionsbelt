// created on 4/16/04 at 9:24 a

namespace Chronos.Resources {

	public interface IResourceOwner {
	
		/// <summary>Retorna a quantidade de um recurso contando também todos os filhos</summary>
		/// <remarks>O recurso tem de existir no pai e nos filhos para ser contabilizado. Ex.: Pontuação</remarks>
		int getResourceCount( string resourceType, string resource );
		
		/// <summary>Retorna a quantidade de um recurso intrinseco contando também todos os filhos</summary>
		/// <remarks>O recurso tem de existir no pai e nos filhos para ser contabilizado. Ex.: Pontuação</remarks>
		int getResourceCount( string resource );
		
		/// <summary>Retorna a quantidade de recurso, sem delegar para ninguém</summary>
		int getSpecificResourceCount( string resourceType, string resource );
		
		/// <summary>Indica a percentagem a incidir sobre a produção</summary>
		double ProductionFactor {get;}
	
	};

}