// created on 3/12/04 at 9:58 a

using System;
using Chronos.Interfaces;

namespace Chronos.Resources {

	public interface IResourceManager : IResourceOwner, IComparable, IIdentifiable {
	
		/// <summary>Indica se este manager partilha recuros/summry>
		bool IsSharing {get;set;}
		
		/// <summary>Identifica o owner deste recurso</summary>
		IResourceManager Owner { get; set; }

		/// <summary>Identifica este resource manager</summary>
		string Identifier { get; }
	
		/// <summary>Adiciona uma quantidade de recursos Intrinsic ao planeta</summary>
		/// <remarks>Se a adição tem um custo, este deve ser deduzido com o método take.</remarks>
		Resource addResource( string resource, int quantity );
		
		/// <summary>Adiciona uma quantidade de recursos ao planeta</summary>
		/// <remarks>Se a adição tem um custo, este deve ser deduzido com o método take.</remarks>
		Resource addResource( string type, string resource, int quantity );
	
		/// <summary>Verifica se há novas Factories disponíveis</summary>
		/// <remarks>Retorna true se há novas factories disponíveis.</remarks>
		bool checkDependencies();
		
		/// <summary>Verifica se uma determinada ResourceFactory está disponível</summary>
		/// <remarks>
		///  Caso o resourceType seja um tipo de recurso relativo ao User e não ao planeta
		///  este método delega a pergunta para o User, sendo indiferente para quem chama este
		///  método se ele vai buscar informação ao planeta ou ao user.
		/// </remarks>
		bool isFactoryAvailable( string resourceType, string name, bool canDelegate );
		
		/// <summary>Retorna true se determinada  quantidade de um recurso intrinseco existir</summary>
		bool isResourceAvailable( string intrinsic, int quantity );
		
		/// <summary>Retorna true se existir determinada quantidade de um recurso.</summary>
		bool isResourceAvailable( string resourceType, string resource, int quantity );
		
		/// <summary>Retira uma certa quantidade de um recurso intrinseco</summary>
		/// <remarks>
		///  Este método tem em conta que defacto existe a quantidade necessária, se não
		///	 existir, é lançada uma excepção. Deve ser usado o método isResourceAvailable
		///  para averiguar se se pode fazer o take.
		/// </remarks>
		/// <returns>True se a operação for sucedida</returns>
		bool take( string intrinsic, int quantity );
		
		/// <summary>Retira uma certa quantidade de um recurso específicado</summary>
		/// <remarks>
		///  Este método tem em conta que defacto existe a quantidade necessária, se não
		///	 existir, a quantidade fica negativa.
		/// </remarks>
		/// <returns>True se a operação for sucedida</returns>
		bool take( string resourceType, string resource, int quantity );
		
		/// <summary>Elimina uma ResourceFactory das listas deste planeta</summary>
		/// <remarks>
		///  Este método elimina uma ResourceFactory deste planeta, estando ela na lista
		///  de available, ou all, ou seja o que for. Assim que este método seja chamado
		///  não será possível criar mais recursos associados áquela ResourceFactory.
		/// </remarks>
		void supressFactory( string resourceType, string factory );
		
		/// <summary>Remove modificadores</summary>
		void removeModifier( string resource, int quantity );
		
		/// <summary>Faz update a um contentor de modificadores</summary>
		void updateModifiers( Resource resource, int quantity );
		
		/// <summary>Aumenta o ratio de um recurso</summary>
		void addRatio(string resource, int val );
		
		/// <summary>Diminui o ratio de um recurso</summary>
		void removeRatio(string resource, int val );
		
	};

}
