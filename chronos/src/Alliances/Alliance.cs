// created on 4/10/04 at 9:16 a

using System;
using Chronos.Exceptions;
using Chronos.Interfaces;
using Chronos.Resources;
using Chronos.Core;

namespace Chronos.Alliances {

	/// <summary>
	/// classe que representa uma aliança de rulers
	/// </summary>
	[Serializable]
	public class Alliance : IResourceManager, ITask, IResourceOwner {
	
		private Universe universe;
		private bool sharing;
		private AllianceMember[] members;
		private string _name = string.Empty;
		private int id;
				
		/// <summary>Construtor</summary>
		public Alliance(Universe universe, string name) : this(universe, name,false)
		{
		}
		
		/// <summary>Construtor</summary>
		public Alliance(Universe universe, string name, bool _sharing )
			: this(universe, name, _sharing, universe.generateAllianceId() )
		{
		}
		
			/// <summary>Construtor</summary>
		public Alliance(Universe universe, string name, int id )
			: this(universe, name, false, id )
		{
		}
		
		/// <summary>Construtor</summary>
		public Alliance( Universe _universe, string name, bool _sharing, int _id )
		{
			id = _id;
			_name = name;
			universe = _universe;
			sharing = _sharing;
			members = new AllianceMember[0];
		}
		
		/// <summary>Adiciona um ruler a esta aliança</summary>
		public void addRuler( Ruler _ruler, AllianceMember.Role _role )
		{
			int count = members.Length;
		
			AllianceMember[] array = new AllianceMember[ count + 1 ];
			members.CopyTo(array,0);
			array[count] = new AllianceMember( _ruler, _role );
			members = array;
			_ruler.Owner = this;
			Array.Sort(members);
		}
		
		/// <summary>Retorna o índice no Array de um Ruler</summary>
		public int getIndex( string rulerName )
		{
			for( int i = 0; i < members.Length; ++i ) {
				if( members[i].Ruler.Name == rulerName ) {
					return i;
				}
			}
			return -1;
		}
		
		/// <summary>Retorna o índice no Array de um Ruler</summary>
		public int getIndex( Ruler ruler )
		{
			for( int i = 0; i < members.Length; ++i ) {
				if( members[i].Ruler == ruler ) {
					return i;
				}
			}
			return -1;
		}
		
		/// <summary>Retorna o índice no Array de um Ruler</summary>
		public int getIndex( AllianceMember member )
		{
			return Array.BinarySearch(members, member);
		}
		
		/// <summary>Remove um membro da aliança</summary>
		public bool removeRuler( AllianceMember member )
		{
			int idx = getIndex( member );
			if( idx < 0 ) {
				return false;
			}
			return removeRuler(idx);
		}
		
		/// <summary>Remove um membro da aliança</summary>
		public bool removeRuler( int idx )
		{
			AllianceMember[] array = new AllianceMember[members.Length - 1];
			AllianceMember toRemove = members[idx];
			toRemove.Ruler.Owner = null;
			
			if( array.Length > 0 ) {
    			Array.Copy(members, 0, array, 0, idx);
    			Array.Copy(members, idx+1, array, idx, members.Length - idx - 1);
			}
			
			members = array;
			Array.Sort(members);
			
			return true;
		}

		/// <summary>Obtém o nome da alinça por omissão</summary>
		public static string defaultAllianceName {
			get{ return "no-alliance"; }
		}

		/// <summary>Retorna um array com os membros da aliança</summary>
		public AllianceMember[] Members {
			get {
				return members;
			}
		}
		
		/// <summary>Identifica o owner deste recurso</summary>
		public IResourceManager Owner {
			get { return this; }
			set { }
		}

		/// <summary>Retorna o nome da aliança</summary>
		public string Name {
			get { return _name; }
		}
		
		/// <summary>Retorna o ID desta entidade</summary>
		public int Id {
			get { return id; }
		}
		
		// <summary>Indica a percentagem a incidir sobre a produção</summary>
		public virtual double ProductionFactor {
			get { return 1.0; }
		}

		/// <summary>Identifica este resource manager</summary>
		public string Identifier {
			get { return "alliance"; }
		}
		
		/// <summary>Retorna a quantidade de um recurso contando também todos os filhos</summary>
		/// <remarks>O recurso tem de existir no pai e nos filhos para ser contabilizado. Ex.: Pontuação</remarks>
		public int getResourceCount( string resourceType, string resource )
		{
			int count = 0;
			
			foreach( AllianceMember member in members ) {
				Ruler ruler = member.Ruler;
				if( ruler.isResourceAvailable( resourceType, resource ) ) {
					count += ruler.getResourceCount( resourceType, resource );
				}
			}
			
			return count;
		}
		
		/// <summary>Retorna a quantidade de um recurso intrinseco contando também todos os filhos</summary>
		/// <remarks>O recurso tem de existir no pai e nos filhos para ser contabilizado. Ex.: Pontuação</remarks>
		public int getResourceCount( string resource )
		{
			return getResourceCount("Intrinsic",resource);
		}
		
		/// <summary>Retorna a quantidade de recurso, sem delegar para ninguém</summary>
		public int getSpecificResourceCount( string resourceType, string resource )
		{
			return 0;
		}
	
		/// <summary>Adiciona uma quantidade de recursos ao planeta, sem passar pela Queue</summary>
		/// <remarks>Se a adição tem um custo, este deve ser deduzido com o método take.
		///	Se a categoria 'type' não exister neste manager, é lançada excepção.
		/// </remarks>
		public Resource addResource( string resource, int quantity )
		{
			return addResource("Intrinsic", resource, quantity);
		}
		
		/// <summary>Adiciona uma quantidade de recursos ao planeta</summary>
		/// <remarks>Se a adição tem um custo, este deve ser deduzido com o método take.</remarks>
		public Resource addResource( string type, string resource, int quantity )
		{
			return addResource(type, resource, quantity, true);
		}
	
		/// <summary>Adiciona uma quantidade de recursos ao planeta</summary>
		/// <remarks>Se a adição tem um custo, este deve ser deduzido com o método take.</remarks>
		public Resource addResource( string type, string resource, int quantity, bool callOnComplete )
		{
			throw new RuntimeException("addResource called on an Alliance ["+quantity+" of "+resource+"]");
		}
	
		/// <summary>Verifica se há novas Factories disponíveis</summary>
		/// <remarks>Retorna true se há novas factories disponíveis.</remarks>
		public bool checkDependencies()
		{
			return false;
		}
		
		/// <summary>Verifica se uma determinada ResourceFactory está disponível</summary>
		/// <remarks>
		///  Se a Aliança estiver a partilhar, vai procurar nos seus membros,
		///  caso contrário retorna falso
		/// </remarks>
		public bool isFactoryAvailable( string resourceType, string name, bool canDelegate  )
		{
			if( ! IsSharing || ! canDelegate ) {
				return false;
			}
			
			foreach( AllianceMember member in members ) {
				if( member.Ruler.IsSharing && member.Ruler.isFactoryAvailable(resourceType, name, false) ) {
					return true;
				}
			}
			
			return false;
		}
		
		/// <summary>Indica se nesta aliança os recursos são partilhados</summry>
		public bool IsSharing {
			get { return sharing;  }
			set { sharing = value; }
		}
		
		/// <summary>Retorna true se determinada  quantidade de um recurso intrinseco existir</summary>
		public bool isResourceAvailable( string intrinsic, int quantity )
		{
			return false;
		}
		
		/// <summary>Retorna true se existir determinada quantidade de um recurso.</summary>
		public bool isResourceAvailable( string resourceType, string resource, int quantity )
		{
			return false;
		}
		
		/// <summary>Retira uma certa quantidade de um recurso intrinseco</summary>
		/// <remarks>
		///  Este método tem em conta que defacto existe a quantidade necessária, se não
		///	 existir, é lançada uma excepção. Deve ser usado o método isResourceAvailable
		///  para averiguar se se pode fazer o take.
		/// </remarks>
		public bool take( string intrinsic, int quantity )
		{
			return take("Intrinsic",intrinsic,quantity);
		}
		
		/// <summary>Retira uma certa quantidade de um recurso específicado</summary>
		/// <remarks>
		///  Este método tem em conta que defacto existe a quantidade necessária, se não
		///	 existir, a quantidade fica negativa.
		/// </remarks>
		public bool take( string resourceType, string resource, int quantity )
		{
			throw new RuntimeException("take called on an Alliance ["+quantity+" of "+resource+"] type:"+resourceType);
		}
		
		/// <summary>Elimina uma ResourceFactory das listas deste planeta</summary>
		/// <remarks>
		///  Este método elimina uma ResourceFactory deste planeta, estando ela na lista
		///  de available, ou all, ou seja o que for. Assim que este método seja chamado
		///  não será possível criar mais recursos associados áquela ResourceFactory.
		/// </remarks>
		public void supressFactory( string resourceType, string factory )
		{
		}
		
		/// <summary>Faz update a um contentor de modificadores</summary>
		public void updateModifiers( Resource resource, int quantity )
		{
		}
		
		/// <summary>Remove modificadores</summary>
		public void removeModifier( string resource, int quantity )
		{
		}
		
		/// <summary>Aumenta o ratio de um recurso</summary>
		public void addRatio(string resource, int val )
		{
		}
		
		/// <summary>Diminui o ratio de um recurso</summary>
		public void removeRatio(string resource, int val )
		{
		}

		/// <summary>Efectua as operações de passagem de turno</summary>
		public virtual void turn()
		{
			Universe.Events.trace("[START] Updating alliance {0}(id={1})", Name, Id);
			try {
				foreach( AllianceMember member in members ) {
					member.Ruler.turn();
				}
			} catch( Exception ex ) {
				Universe.Events.turnError( ex );
				Universe.Events.trace("[EXCEPTION] {0}", ex);
			}
			Universe.Events.trace("[END] Updated alliance {0}(id={1})", Name, Id);
		}

		/// <summary>Compara 2 duas alianças de acordo com a sua pontuação</summary>
		public int CompareTo( object obj )
		{
			Alliance alliance = (Alliance) obj;
			int thisScore = getResourceCount("Intrinsic","score");
			int otherScore = alliance.getResourceCount("Intrinsic","score");
			
			if( thisScore == otherScore ) {
				return 0;
			} else if ( thisScore > otherScore ) {
				return -1;
			} else {
				return 1;
			}
		}
	};

}
