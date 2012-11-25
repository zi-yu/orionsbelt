// created on 3/12/04 at 9:58 a

using System;
using System.Collections;
using Chronos.Interfaces;
using Chronos.Exceptions;
using Chronos.Utils;
using Chronos.Actions;
using Chronos.Info.Results;
using Chronos.Queue;
using Chronos.Messaging;
using Chronos.Core;

namespace Chronos.Resources {

	[Serializable]
	public abstract class ResourceManager : MessageManager,
						IResourceManager,
						IResourceOueue,
						ITask {
	
		#region Instance Fields
		
		protected IResourceManager owner;
		private string identifier;
		private bool sharing;
		private int id;

		// contm todos os recursos do planeta ( intrinsic, buidings, etc...)
		private Hashtable resources;
	
		//contm os modificadores do planeta (ouro e cenas assim)
		private Hashtable permanentModifiers;
		//contm os modificadores dos recursos raros
		private Hashtable rareModifiers;

		// contém as percentagem que incidem sobre os modificadores
		private Hashtable modifiersRatio;
		
		#endregion
		
		#region Ctors
		
		/// <summary>Construtor</summary>
		/// <remarks>
		///  O ResourceManager vai-se construir com a Hashtable que contém todos os recursos.
		///  Assim que povoar o seu contentor de All, vai ver quais é que estão disponíveis
		///  e coloca-as na lista Available (logo todas as Factories sem dependências)
		/// </remarks>
		public ResourceManager( IResourceManager ruler, Hashtable allFactories, string ident, int _id )
		{
			init( ruler, allFactories, ident, _id );
		}
		
		/// <summary>Construtor</summary>
		/// <remarks>
		///  O ResourceManager vai-se construir com a Hashtable que contém todos os recursos.
		///  Assim que povoar o seu contentor de All, vai ver quais é que estão disponíveis
		///  e coloca-as na lista Available (logo todas as Factories sem dependências)
		/// </remarks>
		public ResourceManager( IResourceManager ruler, string ident, int _id )
		{
			owner = ruler;
			identifier = ident;
			sharing = false;
			id = _id;

			permanentModifiers = new Hashtable();
			rareModifiers = new Hashtable();
			modifiersRatio = new Hashtable();
			resources = new Hashtable();
		}

		/// <summary>
		/// só existe porque sim :P
		/// </summary>
		public ResourceManager() {
		
		}

		/// <summary>
		/// inicia um resource manager.
		/// </summary>
		/// <param name="ruler">Owner do objecto</param>
		/// <param name="allFactories">todas as factories</param>
		/// <param name="ident">identidade: se é ruler ou planeta</param>
		/// <param name="_id">identifucador unico</param>
		protected void init( IResourceManager ruler, Hashtable allFactories, string ident, int _id ) {
			owner = ruler;
			identifier = ident;
			sharing = false;
			id = _id;

			permanentModifiers = new Hashtable();
			rareModifiers = new Hashtable();
			modifiersRatio = new Hashtable();
			
			Hashtable builders = (Hashtable) allFactories[Identifier];
			if( builders == null ) {
				throw new RuntimeException("Not found any '"+Identifier+"' builders in allFactories hash");
			}

			resources = new Hashtable();
			IDictionaryEnumerator it = builders.GetEnumerator();
			while( it.MoveNext() ) {
				ResourceBuilder builder = (ResourceBuilder) it.Value;
				resources.Add( it.Key, new ResourceInfo( this, builder, (string)it.Key ));
			}
			
			reset();

			checkIntrinsicDependencies();
			checkDependencies();
		}
		
		public void FullReset() {
			owner = null;
			sharing = false;

			permanentModifiers = null;
			rareModifiers = null;
			modifiersRatio = null;
			resources = null;
		}
		
		#endregion
		
		#region Instance Properties

		/// <summary>Indica se neste ResourceManager os recursos são partilhados</summry>
		public bool IsSharing {
			get { return sharing;  }
			set { sharing = value; }
		}
		
		/// <summary>Identifica este resource manager</summary>
		public string Identifier {
			get { return identifier; }
		}

		/// <summary>Identifica o owner deste recurso</summary>
		public IResourceManager Owner {
			get { return owner; }
			set { owner = value; }
		}

		/// <summary>Retorna os modificadores</summary>
		public Hashtable Modifiers {
			get {
				return permanentModifiers;
			}
			set {
				permanentModifiers = value;
			}
		}
		
		/// <summary>Retorna os modificadores dos recursos raros</summary>
		public Hashtable RareModifiers {
			get {
				return rareModifiers;
			}
			set {
				rareModifiers = value;
			}
		}
		
		/// <summary>Retorna as percentagens que incidem sobre os modificadores</summary>
		public Hashtable ModifiersRatio {
			get {
				return modifiersRatio;
			}
			set {
				modifiersRatio = value;
			}
		}
		
		/// <summary>Retorna o id(único) deste ResourceManager</summary>
		public int Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Retorna um valor aleatório</summary>
		private int randomize()
		{
			int limit = 20;
			return MathUtils.random(0,limit) - limit / 2;
		}
		
		#endregion
		
		#region Modifiers Related Methods
		
		/// <summary>Indica quanto por turno se recebe de um determinado recurso</summary>
		public int getPerTurn( string category, string resource )
		{
			object modRatio = ModifiersRatio[resource];
			object mod = Modifiers[resource];
	
			if( mod == null ) {
				mod = RareModifiers[resource];
				if( mod == null ) {
					return 0;
				}
			}
			
			int ratio = 100;
			
			if( modRatio != null ) {
				ratio = (int) modRatio;
			}
			
			int val = calcValue( (int) mod, ratio );
			
			foreach( Resource res in getResourceInfo("Building").Resources.Keys ) {
				int buildings = (int) getResourceInfo("Building").Resources[res];
				ResourceFactory factory = res.Factory;
				if( factory.OnTurnActions != null ) {
					foreach( Action action in factory.OnTurnActions ) {
						if( action is TransformRareResource ) {
							TransformRareResource trare = (TransformRareResource) action;
							if( resource != trare.Intrinsic ) {
								continue;
							}
							int rareValue = getResourceCount("Rare", trare.Rare); 
							if( rareValue > 0 ) {
								val += buildings * trare.Factor;
							}
						} else if( action is Transform ) {
							Transform trans = (Transform) action;
							if( resource != trans.Output ) {
								continue;
							}
							int intValue = getResourceCount("Intrinsic", trans.Input);
							if( intValue > trans.Factor ) {
								intValue = trans.Factor;
							}
							if( intValue < 0 ) {
								continue;
							}
							val += buildings * intValue;
						}
					}
				}
			}
			
			return val;
			
		}
		
		/// <summary>Indica a percentagem a incidir sobre um determinado recurso</summary>
		public int getRatio( string resource )
		{
			object modRatio = ModifiersRatio[resource];
			if( modRatio == null ) {
				return 0;
			}
			
			return (int) modRatio;
		}
		
		/// <summary>Regista um Modifier</summary>
		public void registerModifier( string intrinsic, int val )
		{
			object o = Modifiers[intrinsic];
			if( o == null ) {
				Modifiers.Add(intrinsic, val);
			} else {
				Modifiers[intrinsic] = val + ((int) o);
			}
		}
		
		/// <summary>Regista um Modifier para um recurso raro</summary>
		public void registerRareModifier( string rare, int val )
		{
			object o = RareModifiers[rare];
			if( o == null ) {
				RareModifiers.Add(rare, val);
			} else {
				RareModifiers[rare] = val + ((int) o);
			}
		}

		/// <summary>Regista um Modifier Ratio</summary>
		public void registerModifierRatio( string intrinsic, int val )
		{
			registerModifierRatio(intrinsic, val, true);
		}
		
		/// <summary>Regista um Modifier Ratio</summary>
		public void registerModifierRatio( string intrinsic, int val, bool randomVal )
		{
			if( randomVal ) {
				val += randomize();
				if( val < 20 ) {
					val = 20;
				}
			}
			modifiersRatio.Add( intrinsic, val );
		}
		
		/// <summary>Remove modificadores</summary>
		public void removeModifier( string resource, int quantity )
		{
			removeModifier( Modifiers, resource, quantity );
			removeModifier( RareModifiers, resource, quantity );
		}
		
		/// <summary>Remove modificadores intrinsecos</summary>
		private void removeModifier( Hashtable mods, string resource, int quantity )
		{
			if( !mods.ContainsKey(resource) ) {
				return;
			}
			int curr = (int) mods[resource];
			mods[resource] = curr - quantity;
		}
		
		/// <summary>Faz update a um contentor de modificadores</summary>
		public void updateModifiers( Resource resource, int quantity )
		{
			if( resource.Modifiers == null ) {
				return;
			}

			IDictionaryEnumerator it = resource.Modifiers.GetEnumerator();
			while( it.MoveNext() ) {
				string name = it.Key.ToString();
				Hashtable target = Modifiers;
				
				if( Resource.IsRare(name) ) {
					target = RareModifiers;
				}
				
				object o = target[it.Key];
				if( o == null ) {
					target.Add(it.Key,it.Value);
				} else {
					int val = (int) o;
					int toAdd = (int) it.Value;
					target[it.Key] = val + toAdd * quantity;
				}
			}
		}
		
		/// <summary>Aumenta o ratio de um recurso</summary>
		public void addRatio(string resource, int val )
		{
			object obj = ModifiersRatio[resource];
			if( obj == null ){
				ModifiersRatio.Add(resource, val);
			} else {
				int curr = (int) obj;
				ModifiersRatio[resource] = curr + val;
			}
		}
		
		/// <summary>Diminui o ratio de um recurso</summary>
		public void removeRatio(string resource, int val )
		{
			object obj = ModifiersRatio[resource];
			if( obj == null ){
				ModifiersRatio.Add(resource, -val);
			} else {
				int curr = (int) obj;
				ModifiersRatio[resource] = curr - val;
			}
		}
		
		#endregion
		
		#region Resource Related Methods
		
		/// <summary>Actualiza os recursos intrinsecos e raros com base nos modificadores</summary>
		protected void updateResources()
		{
			updateResources(Modifiers, "Intrinsic");
			
			if( Identifier != "ruler" ) {
				updateResources(RareModifiers, "Rare");
			}
		}
		
		/// <summary>Actualiza os recursos de determinada categoria</summary>
		private void updateResources( Hashtable mods, string category )
		{
			object o = resources[category];
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				updateResources(info, mods);
				return;
			}
	
			throw new RuntimeException("Resource category '"+category+"'/'"+Identifier+"' not registered [@ updateResources ]");
		}
		
		/// <summary>Indica se  para aplicao um factor de atenuao</summary>
		public virtual bool Attenuation  {
			get { return false; }
		}
		
		/// <summary>Retorna uma quantidade com base no valor e na percentagem atenuante</summary>
		public int calcValue( int val, int perc )
		{
			double rawVal = val * perc * Math.Pow(10,-2);
			if( Attenuation ) {
				rawVal /= 2;
			}
			return (int) Math.Round(rawVal);
		}
		
		/// <summary>Actualiza os recursos intrinsecos com base nos modificadores</summary>
		private void updateResources( ResourceInfo info, Hashtable mods )
		{
			try {
				Hashtable temp = new Hashtable();
			
				IDictionaryEnumerator it = mods.GetEnumerator();
				while( it.MoveNext() ) {
					string resource = it.Key.ToString();
					int val = (int) it.Value;
				
					object o = modifiersRatio[resource];
					if( o != null ) {
						val = calcValue( val, (int) o );
					}
				
					// não se pode fazer logo addResource
					// porque ele vai mexer no contentor de modificadores
					// e o Enumerator fica out of sync
					temp.Add( resource, val );
				}
				
				it = temp.GetEnumerator();
				while( it.MoveNext() ) {
					string resource = it.Key.ToString();
					int val = (int) it.Value;
					info.addResource( resource, val );
				}

			} catch( System.InvalidOperationException ex ) {
				Log.log("EXCEPTION: " + Identifier );
				Log.log(ex);
			}
		}
		
		/// <summary>Retorna todos os recursos construídos</summary>
		public Hashtable getResources( string type )
		{
			object o = resources[type];
			
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info.Resources;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ getResources ]");
		}
		
		/// <summary>Coloca uma quantidade de um recurso a zero</summary>
		public void reset( string category, string resource  )
		{
			object o = resources[category];
			
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				info.reset(resource);
				return;
			}
			
			throw new RuntimeException("Resource category '"+category+"' not registered [@ reset ]");
		}
		
		/// <summary>Coloca os recursos de uma categoria a zero</summary>
		public void reset(string category )
		{
			object o = resources[category];
			
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				info.reset();
				return;
			}
			
			throw new RuntimeException("Resource category '"+category+"' not registered [@ reset ]");
		}
		
		
		/// <summary>Coloca todos os recursos disponíveis desta categoria a zero</summary>
		public void reset()
		{
			foreach( ResourceInfo info in Resources.Values ) {
				info.reset();
			}
		}
		
		/// <summary>Adiciona uma quantidade de recursos ao planeta, sem passar pela Queue</summary>
		/// <remarks>Se a adição tem um custo, este deve ser deduzido com o método take.
		///	Se a categoria 'type' não exister neste manager, é lançada excepção.
		/// </remarks>
		public Resource addResource( string resource, int quantity )
		{
			if( Resource.IsRare(resource) ) {
				return addResource("Rare", resource, quantity);
			}
			return addResource("Intrinsic", resource, quantity);
		}
		
		/// <summary>Adiciona uma quantidade de recursos ao planeta, sem passar pela Queue</summary>
		/// <remarks>Se a adição tem um custo, este deve ser deduzido com o método take.
		///	Se a categoria 'type' não exister neste manager, é lançada excepção.
		/// </remarks>
		public Resource addResource( string type, string resource, int quantity )
		{
			object o = resources[type];
			
			if( o != null ) {
				ResourceInfo info;
				info = (ResourceInfo) o;

				Resource created = info.addResource(resource,quantity);
				return created;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ addResource, resource="+resource+", quantity="+quantity+"]");
		}
		
		/// <summary>Adiciona um recurso ao planeta, sem passar pela Queue</summary>
		/// <remarks>Se a adição tem um custo, este deve ser deduzido com o método take.</remarks>
		public Resource addResource( string type, string resource )
		{
			return addResource(type, resource, 1);
		}
		
		/// <summary>Retorna todos os ResourceInfo's</summary>
		public Hashtable Resources {
			get {
				return resources;
			}
		}
		
		/// <summary>Retorna true se determinada  quantidade de um recurso intrinseco existir</summary>
		public bool isResourceAvailable( string intrinsic, int quantity )
		{
			bool available = isResourceAvailable("Intrinsic", intrinsic, quantity);
			if( !available ) {
				return isResourceAvailable("Rare", intrinsic, quantity );
			}
			return available;
		}

		/// <summary>Retorna true se existir determinada quantidade de um recurso.</summary>
		public bool isResourceAvailable( string resourceType, string resource, int quantity )
		{
			bool available = false;
			object o = resources[resourceType];
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				available = info.isResourceAvailable(resource,quantity);
			}

			if( ! available && Owner != null ) {
				available = Owner.isResourceAvailable(resourceType, resource, quantity);
			}
			
			return available;
		}
		
		/// <summary>Retorna true se existir determinado recurso.</summary>
		public bool isResourceAvailable( string resourceType, string resource )
		{
			return isResourceAvailable(resourceType, resource, 1);
		}
		
		/// <summary>Retorna a quantidade de um recurso.</summary>
		public virtual int getResourceCount( string resourceType, string resource )
		{
			object o = resources[resourceType];
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				if( info.isFactoryAvailable(resource) ) {
					return info.getResourceCount(resource);
				}
			}
			
			if( Owner != null ) {
				return Owner.getSpecificResourceCount(resourceType, resource);
			}
			
			return 0;
		}
		
		/// <summary>Retorna a quantidade de um recurso intrinseco.</summary>
		public int getResourceCount( string resource ) {
			return getResourceCount("Intrinsic", resource);
		}
		
		/// <summary>Retorna a quantidade de recurso, sem delegar para ninguém</summary>
		public virtual int getSpecificResourceCount( string resourceType, string resource )
		{
			object o = resources[resourceType];
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				if( info.isFactoryAvailable(resource) ) {
					return info.getResourceCount(resource);
				}
			}
			
			return 0;
		}
		
		/// <summary>Retira uma certa quantidade de um recurso intrinseco</summary>
		/// <remarks>
		///  Este método tem em conta que defacto existe a quantidade necessária, se não
		///	 existir, é lançada uma excepção. Deve ser usado o método isResourceAvailable
		///  para averiguar se se pode fazer o take.
		/// </remarks>
		/// <returns>True se a operação for sucedida</returns>
		public bool take( string intrinsic, int quantity )
		{
			bool available = take("Intrinsic", intrinsic, quantity );
			if( !available ) {
				return take("Rare", intrinsic, quantity );
			}
			return available;
		}
		
		/// <summary>Retira uma certa quantidade de um recurso específicado</summary>
		/// <remarks>
		///  Este método tem em conta que defacto existe a quantidade necessária, se não
		///	 existir, a quantidade fica negativa.
		///  Se não tiver a categoria 'resourceType', delega para o owner.
		/// </remarks>
		/// <returns>True se a operação for sucedida</returns>
		public bool take( string resourceType, string resource, int quantity )
		{
			object o = resources[resourceType];
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info.take(resource,quantity);
			}

			throw new RuntimeException("Resource category '"+resourceType+"' not registered [@ take, resource="+resource+", quantity="+quantity+"]");
		}
		
		/// <summary>Retorna um ResourceInfo</summary>
		public ResourceInfo getResourceInfo(string resourceType)
		{
			object o = resources[resourceType];
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info;
			}
			
			throw new RuntimeException("Resource category '"+resourceType+"' not registered [@ getResourceInfo]");
			
		}
		
		#endregion
		
		#region Factories Related Methods
		
		/// <summary>Retorna todas as factories disponíveis.</summary>
		public ResourceBuilder getAvailableFactories( string type )
		{
			object o = resources[type];
			
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info.AvailableFactories;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ getAvailableFactories ]");
		}
		
		/// <summary>Retorna todas as factories não disponíveis.</summary>
		public ResourceBuilder getUnavailableFactories( string type )
		{
			object o = resources[type];
			
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info.UnavailableFactories;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ getUnavailableFactories ]");
		}

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public bool checkIntrinsicDependencies()
		{
			ResourceInfo intrinsic = (ResourceInfo)resources["Intrinsic"];
			return intrinsic.checkDependencies();
		}
		
		/// <summary>Verifica se há novas Factories disponíveis</summary>
		/// <remarks>Retorna true se há novas factories disponíveis.</remarks>
		public bool checkDependencies()
		{
			IDictionaryEnumerator it = resources.GetEnumerator();
			bool news = false;
			while( it.MoveNext() ) {
				ResourceInfo info = (ResourceInfo) it.Value;
				if( info.checkDependencies() ){
					news = true;
				}
			}
			return news;
		}
		
		/// <summary>Verifica se uma determinada ResourceFactory está disponível</summary>
		/// <remarks>
		///  Caso o resourceType seja um tipo de recurso relativo ao User e não ao planeta
		///  este método delega a pergunta para o User, sendo indiferente para quem chama este
		///  método se ele vai buscar informação ao planeta, user, ou aliança se houver partilha.
		/// </remarks>
		public bool isFactoryAvailable( string resourceType, string name )
		{
			return isFactoryAvailable(resourceType, name, true );
		}
		
		/// <summary>Verifica se uma determinada ResourceFactory está disponível</summary>
		/// <remarks>
		///  Caso o resourceType seja um tipo de recurso relativo ao User e não ao planeta
		///  este método delega a pergunta para o User, sendo indiferente para quem chama este
		///  método se ele vai buscar informação ao planeta, user, ou aliança se houver partilha.
		/// </remarks>
		public virtual bool isFactoryAvailable( string resourceType, string name, bool canDelegate  )
		{
			object o = resources[resourceType];
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info.isFactoryAvailable(name);
			}
			
			// se não há no this, ver se há no owner
			if( owner != null && canDelegate && owner.IsSharing) {
				return owner.isFactoryAvailable(resourceType,name,true);
			}
			
			return false;
		}
		
		/// <summary>Indica a percentagem a incidir sobre a produção</summary>
		public virtual double ProductionFactor {
			get { return 1.0; }
		}
		
		/// <summary>Elimina uma ResourceFactory das listas deste planeta</summary>
		/// <remarks>
		///  Este método elimina uma ResourceFactory deste planeta, estando ela na lista
		///  de available, ou all, ou seja o que for. Assim que este método seja chamado
		///  não será possível criar mais recursos associados áquela ResourceFactory.
		///  Se ele não tiver a categoria 'resourceType', delega para o owner.
		/// </remarks>
		public void supressFactory( string resourceType, string factory )
		{
			object o = resources[resourceType];
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				info.supressFactory(factory);
				return;
			}
			
			if( owner != null ) {
				owner.supressFactory(resourceType, factory);
			}
			
			throw new RuntimeException("Resource category '"+resourceType+"' not registered [@ supressFactory, factory ="+factory+"]");
		}
		
		#endregion
		
		#region IResourceQueue Implementation
		
		/// <summary>Indica o máximo de items que este queue suporta</summary>
		public int QueueCapacity {
			get { return 1; }
		}
		
		/// <summary>Agenda recursos para construcção</summary>
		/// <remarks>
		/// Se a Queue com tarefas, será colocada no fim. Se estiver vazia
		///	comecerá a ser construído o recurso no próximo turno.
		/// </remarks>
		public void queue( string type, string resource )
		{
			queue(type, resource, 1);
		}
		
		/// <summary>Agenda recursos para construcção</summary>
		/// <remarks>
		/// Se a Queue com tarefas, será colocada no fim. Se estiver vazia
		///	comecerá a ser construído o recurso no próximo turno. Este método não
		///	verifica se há recursos suficientes para criar este recurso. Usar o método
		///	canQueue para esse efeito. Por outro lado, o custo só será cobrado
		///	quando esta factory sair da queue e começar a ser desenvolvida.
		/// </remarks>
		public void queue( string type, string resource, int quantity )
		{
			object o = resources[type];
			
			if( o != null ) {
				ResourceInfo info = null;
				info = (ResourceInfo) o;
				info.enqueue( resource, quantity, ProductionFactor );
				return;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ canQueue, resource="+resource+", quantity="+quantity+" ]");
		}
		
		/// <summary>Verifica se os custos de um recurso existem</summary>
		public Result canQueue( string type, string resource, int quantity )
		{
			object o = resources[type];
			
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info.canQueue(resource,quantity);
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ canQueue, resource="+resource+", quantity="+quantity+" ]");
		}
		
		/// <summary>Retorna um array com todas as tarefas em lista de espera</summary>
		/// <remarks>
		///	O array vem organizado por ordem de prioridade. O primeiro índice corresponde
		///	à próxima tarefa a ser executada. O array retornado é um novo objecto
		///	só com as referências. Não se altera o queue através dele.
		/// <remarks>
		public QueueItem[] getQueueList( string type )
		{
			object o = resources[type];
			
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info.QueueList;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ getQueueList ]");
		}
		
		/// <summary>Retorna o número de tarefas em lista de espera</summary>
		/// <remarks>Se há uma tarefa a ser executada, essa não é contabilizada</remarks>
		public int queueCount( string type )
		{
			object o = resources[type];
			
			if( o != null ) {

				Chronos.Resources.ResourceInfo info = (Chronos.Resources.ResourceInfo) o;
				return info.QueueCount;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ queueCount ]");
		}
		
		/// <summary>Retorna a tarefa que está neste momento a ser realizada</summary>
		/// <remarks>Retorna null caso não haja nenhuma tarefa currente.</remarks>
		public QueueItem current( string type )
		{
			object o = resources[type];
			
			if( o != null ) {
				ResourceInfo info = (ResourceInfo) o;
				return info.Current;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ current ]");
		}
		
		/// <summary>Cancela a tarefa que se está a realizar</summary>
		/// <remarks>
		///	Será tirada uma nova tarefa da queue no próximo turno. Os custos não são
		///	repostos.
		///	</remarks>
		public void cancel( string type )
		{
			object o = resources[type];
			
			if( o != null ) {
			
				ResourceInfo info = (ResourceInfo) o;
				info.cancel();
				return;
			}
			
			throw new RuntimeException("Resource category '"+type+"' not registered [@ cancel]");
		}
		
		#endregion

		#region IComparable Implementation
		
		public abstract int CompareTo( object obj );

		#endregion
		
		#region ITask Implementation

		/// <summary>Realiza as operações a realizar num turno.</summary>
		public virtual void turn()
		{
			Universe.Events.trace("[START] Updating ResourceManager {0}(id={1})", Identifier, Id);
		
			IDictionaryEnumerator it = resources.GetEnumerator();
			while( it.MoveNext() ) {
				ResourceInfo info = (ResourceInfo) it.Value;
				info.turn();
				info.OnTurnActions();
			}
			updateResources();
			checkDependencies();
			
			Universe.Events.trace("[END] Updated ResourceManager {0}(id={1})", Identifier, Id);
		}
		
		#endregion
		
		#region MessageManager Implementation
		
		/// <summary>Indica um identificador deste Handler</summary>
		public override int HandlerId {
			get { return Id; }
		}
		
		/// <summary>Indica uma string que identifica o tipo deste handler (ruler, planet, ...)</summary>
		public override string HandlerIdentifier {
			get { return Identifier; }
		}
		
		#endregion
	
	};

}
