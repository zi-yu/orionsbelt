// created on 3/10/04 at 9:22 a

using Chronos.Actions;
using Chronos.Exceptions;
using Chronos.Queue;
using Chronos.Info.Results;
using Chronos.Utils;
using Chronos.Messaging;
using Chronos.Core;
using Chronos.Info;
using System;
using System.Collections;
using System.IO;

namespace Chronos.Resources {

	/// <summary>Usada pelo Planet para agrupar um tipo de Resursos</summary>
	[Serializable]
	public class ResourceInfo {
	
		#region Events
		
		public delegate void ResourceNotification( Resource res, int quantity );
		public delegate void FactoryNotification( ResourceFactory factory );
		
		public event ResourceNotification OnComplete;
		public event ResourceNotification OnRemove;
		public event FactoryNotification OnAvailable;
		
		#endregion
	
		#region Instance Fields
	
		private ResourceManager owner;
		private string category;
		private Hashtable resources;
		private ResourceBuilder all;
		private ResourceBuilder available;
		private System.Collections.Queue queue;
	
		private QueueItem current;
		
		#endregion

		#region Ctors
	
		/// <summary>Construtor</summary>
		internal ResourceInfo( ResourceManager owner, ResourceBuilder source, string cat )
		{
			all = (ResourceBuilder) source.Clone();
			available = new ResourceBuilder(source.AppliesTo);
			resources = new Hashtable();
			queue = new System.Collections.Queue();
			current = null;
			this.owner = owner;
			category = cat;
		}
		
		/// <summary>Construtor</summary>
		public ResourceInfo( ResourceManager owner, string cat )
		{
			available = new ResourceBuilder( ((ResourceManager)owner).Identifier );
			resources = new Hashtable();
			queue = new System.Collections.Queue();
			current = null;
			this.owner = owner;
			category = cat;
		}
		
		#endregion
		
		#region Turn Methods
		
		/// <summary>Realiza as operações a realizar num turno.</summary>
		public void turn()
		{
			Universe.Events.trace("[START] Updating ResourceInfo {0}(id={1}) category {2}", Owner.Identifier, Owner.Id, Category);
		
			if( current == null ) {	// não tá a construir nada
				Universe.Events.trace("ResourceInfo {0}(id={1}) category {2} - not processing", Owner.Identifier, Owner.Id, Category);
				fetchFromQueue();
				return;
			}
			
			current.process();
			Universe.Events.trace("ResourceInfo {0}(id={1}) missing {2} to complete {3}", Owner.Identifier, Owner.Id, current.RemainingTurns, Category);
			
			if( current.IsFinished ) {
				
				notify( "Completed", new string[] {owner.ToString(), current.Factory.Name, current.Quantity.ToString() });
				checkPlanetPrizes(current);
				
				Universe.Events.trace("ResourceInfo {0}(id={1}) category {2} finished {3}", Owner.Identifier, Owner.Id, Category, current.FactoryName);

				checkUndoAfterBuildActions(current);
				addResource(current);
				fetchFromQueue();
				
				Universe.Events.trace("[END] Updated ResourceInfo {0}(id={1}) category {2}", Owner.Identifier, Owner.Id, Category);
				return;
			}
			
			Universe.Events.trace("[END] Updated ResourceInfo {0}(id={1}) category {2}", Owner.Identifier, Owner.Id, Category);
		}
		
		/// <summary>Chama as acções de turno</summary>
		public void OnTurnActions()
		{
			foreach( Resource resource in Resources.Keys ) {
				ResourceFactory factory = resource.Factory;
				if( factory.OnTurnActions != null ) {
					foreach( Action action in factory.OnTurnActions ) {
						if( action.evaluate(Owner) ) {
							action.action(Owner, (int) Resources[resource]);
						}
					}
				}
			}
		}
		
		/// <summary>Faz o undo às acções necessárias</summary>
		private void checkUndoAfterBuildActions( QueueItem item )
		{
			if( item.Factory.CostActions == null ) {
				return;
			}
			
			foreach( Action action in item.Factory.CostActions ) {
				if( action is IUndoAfterBuild ) {
					action.undo(Owner, item.Quantity);
				}
			}
		}
		
		#endregion
		
		#region Queue Interface Methods
		
		/// <summary>Retira a primeira tarefa da Queue e coloca-a em execução</summary>
		private void fetchFromQueue()
		{
			Universe.Events.trace("ResourceInfo {0}(id={1}) queue count: {2}", Owner.Identifier, Owner.Id, queue.Count);
			if( queue.Count > 0 ) {
				current = dequeue();
				notify( "Started", new string[] {owner.ToString(), current.Factory.Name, current.Quantity.ToString() } );
		} else {
				current = null;
			}
			Universe.Events.trace("ResourceInfo {0}(id={1}) Fetched from queue: {2}", Owner.Identifier, Owner.Id, (current==null?"nothing":current.FactoryName));
		}

		/// <summary>Realiza as acções de cancel quando um recurso já estava a ser construído</summary>
		public void onCancelDuringBuild( QueueItem item )
		{
			if( item.Factory.OnCancelDuringBuild == null ) {
				return;
			}

			foreach( Action action in item.Factory.OnCancelDuringBuild ) {
				action.action(owner,item.Quantity);
			}
		}
		
		/// <summary>Cobra o custo de um recurso</summary>
		public void charge( QueueItem item )
		{
			if( item.Factory.CostActions == null ) {
				return;
			}
		
			foreach( Action action in item.Factory.CostActions ) {
				action.action(owner,item.Quantity);
			}
		}
		
		/// <summary>Devolve o custo de um recurso</summary>
		public void undoCharge( QueueItem item )
		{
			if( item.Factory.CostActions == null ) {
				return;
			}
		
			foreach( Action action in item.Factory.CostActions ) {
				action.undo(owner,item.Quantity);
			}
		}
		
		/// <summary>Indica o máximo de items que este queue suporta</summary>
		public int QueueCapacity {
			get {
				return owner.getResourceCount("Intrinsic", "queueCapacity");
			}
		}
		
		/// <summary>Indica se não há mais espaço para items em queue</summary>
		public bool FullQueue {
			get {
				return queue.Count >= QueueCapacity;
			}
		}
		
		/// <summary>Verifica se se pode adicionar um item à queue</summary>
		public Result canQueue( string resource, int quantity )
		{
			Result result = new Result();
			
			if( quantity <= 0 ) {
				result.failed( new InvalidQuantity() );
				return result;
			}
			
			if( !resource.StartsWith("PlanetLimit") ) {
				Universe.CheckRestrictions(owner, result);
			}
			
			if( FullQueue ) {
				result.failed( new FullQueue(QueueCapacity, queue.Count) );
			}

			ResourceFactory factory = getAvailableFactory(resource);
			if( factory == null ) {
				result.failed( new FactoryNotAvailable(category, resource) );
				return result;
			}

			if( factory.CostActions != null ) {
				foreach( Action action in factory.CostActions ) {
					if( action.evaluate(owner,quantity) ) {
						result.passed( new ActionPassed(action, quantity) );
					} else {
						result.failed( new ActionFailed(action, quantity) );
					}
				}
			}
			
			return result;
		}
		
		/// <summary>Coloca uma ResourceFactory em lista de espera para construção</summary>
		public void enqueue( string factory )
		{
			enqueue(factory, 1, 1.0);
		}
		
		/// <summary>Coloca uma ResourceFactory em lista de espera para construção</summary>
		public void enqueue( string factory, int quantity )
		{
			enqueue(factory, quantity, 1.0);
		}
		
		/// <summary>Coloca uma ResourceFactory em lista de espera para construção</summary>
		public void enqueue( string factory, int quantity, double productionFactor )
		{
			ResourceFactory fact = (ResourceFactory) available[factory];
			if( fact == null ) {
				throw new RuntimeException("["+category+"] Trying to enqueue an unavailable factory: " + factory);
			}
			
			QueueItem item = new QueueItem(owner, fact, quantity, productionFactor);
			charge(item);
			queue.Enqueue(item);
		}
		
		/// <summary>Retira a próxima ResourceFactory em lista de espera para construção</summary>
		public QueueItem dequeue()
		{
			return (QueueItem) queue.Dequeue();
		}
		
		/// <summary>Retira todas as ResourceFactory pretendidas em lista de espera para constru§£o</summary>
		/// <remarks>
		///  Este m©todo deve ser usado s³ em ºltimo caso, pois © mais pesado. N£o © uma
		///  opera§£o natural remover coisas do meio do Queue (pelo menos em programa§£o).
		/// </remarks>
		public void dequeue( string factory )
		{
			System.Collections.Queue temp = new System.Collections.Queue();
			ArrayList toRemove = new ArrayList();
			
			while( queue.Count > 0 ) {
				QueueItem fact = (QueueItem) queue.Dequeue();
				if( fact.FactoryName != factory ) {
					temp.Enqueue(fact);
				} else {
					toRemove.Add(fact);
				}
			}
			queue = temp;
			
			foreach( QueueItem item in toRemove ) {
				undoCharge(item);
			}
		}
				
		/// <summary>Retira uma ResourceFactory em lista de espera para construção</summary>
		/// <remarks>
		///  Este método deve ser usado só em último caso, pois é mais pesado. Não é uma
		///  operação natural remover coisas do meio do Queue (pelo menos em programação).
		/// </remarks>
		public QueueItem dequeue( int idx )
		{
			if( idx < 0 || idx >= queue.Count ) {
				throw new RuntimeException("["+category+"] Trying to dequeue a non-existing index: "+idx);
			}
			
			System.Collections.Queue temp = new System.Collections.Queue();
			QueueItem toRemove = null;
			
			int count = 0;
			while( queue.Count > 0 ) {
				object fact = queue.Dequeue();
				if( count++ != idx ) {
					temp.Enqueue(fact);
				} else {
					toRemove = (QueueItem) fact;
				}
			}
			
			queue = temp;
			undoCharge(toRemove);
			
			return toRemove;
		}
		
		/// <summary>Cancela a tarefa que se está a realizar</summary>
		/// <remarks>Será tirada uma nova tarefa da queue no próximo turno.</remarks>
		public void cancel()
		{
			if( null != current && current.Factory.AllowUndoDuringBuild ) {
				undoCharge(current);
			}
			if( null != current ) {
				onCancelDuringBuild(current);
			}
			current = null;
		}
		
		/// <summary>Retorna um array com todas as tarefas em lista de espera</summary>
		/// <remarks>
		///	O array vem organizado por ordem de prioridade. O primeiro índice corresponde
		///	à próxima tarefa a ser executada. O array retornado é um novo objecto
		///	só com as referências. Não se altera o queue através dele.
		/// <remarks>
		public QueueItem[] QueueList {
			get {
				object[] raw = RawQueueList;
				QueueItem[] items = new QueueItem[raw.Length];
				for( int i = 0; i < raw.Length; ++i ) {
					items[i] = (QueueItem) raw[i];
				}
				return items;
			}
		}
		
		/// <summary>Retorna um array com todas as tarefas em lista de espera</summary>
		public object[] RawQueueList {
			get {
				return queue.ToArray();
			}
		}
		
		/// <summary>Retorna o número de tarefas em lista de espera</summary>
		/// <remarks>Se há uma tarefa a ser executada, essa não é contabilizada</remarks>
		public int QueueCount {
			get {
				return queue.Count;
			}
		}
		
		/// <summary>Retorna o Queue</summary>
		public System.Collections.Queue Queue {
			set {
				queue = value;
			}
		}
		
		/// <summary>Retorna a tarefa que está neste momento a ser realizada</summary>
		/// <remarks>Retorna null caso não haja nenhuma tarefa currente.</remarks>
		public QueueItem Current {
			get {
				return current;
			}
			set {
				current = value;
			}
		}
		
		#endregion
		
		#region Resource Factory Utilities
		
		/// <summary>Retorna uma factory</summary>
		public ResourceFactory getAvailableFactory( string name )
		{
			return (ResourceFactory) available[name];
		}
		
		/// <summary>Retorna as factories disponíveis</summary>
		public ResourceBuilder AvailableFactories {
			get {
				return available;
			}
			set {
				available = value;
			}
		}
		
		/// <summary>Retorna as factories não disponíveis</summary>
		public ResourceBuilder UnavailableFactories {
			get {
				return all;
			}
		 }
		 
		/// <summary>Retorna todas as factories não disponiveis</summary>
		public ResourceBuilder AllFactories {
			get {
				return all;
			}
			set {
				all = value;
			}
		}
		
		/// <summary>Verifica se uma determinada ResourceFactory está disponível</summary>
		public bool isFactoryAvailable( string name )
		{
			return available.ContainsKey(name);
		}
		
		/// <summary>Retorna true se determinada  quantidade de um recurso existir</summary>
		public bool isResourceAvailable( string resource, int quantity )
		{
			IDictionaryEnumerator it = resources.GetEnumerator();
			while( it.MoveNext() ) {
				Resource res = (Resource) it.Key;
				if( res.Name == resource ) {
					return quantity <= ((int)it.Value);
				}
			}
			
			if( quantity == 0 && isFactoryAvailable(resource) ) {
				return true;
			}
			
			return false;
		}
		
		/// <summary>Retorna o recurso associado à string</summary>
		public Resource getResource( string name )
		{
			IDictionaryEnumerator it = resources.GetEnumerator();
			while( it.MoveNext() ) {
				Resource resource = (Resource) it.Key;
				if( resource.Name == name ) {
					return resource;
				}
			}
			return null;
		}
		
		/// <summary>Retorna true se determinado recurso existir</summary>
		public bool isResourceAvailable( string resource )
		{
			return isResourceAvailable(resource, 1);
		}
		
		/// <summary>Retorna a quantidade de um recurso intrinseco.</summary>
		public int getResourceCount( string name )
		{
			IDictionaryEnumerator it = resources.GetEnumerator();
			while( it.MoveNext() ) {
				Resource resource = (Resource) it.Key;
				if( resource.Name == name ) {
					return (int) it.Value;
				}
			}
			return 0;
		}
		
		/// <summary>Elimina uma ResourceFactory das listas deste ResourceInfo</summary>
		/// <remarks>
		///  Este método elimina uma ResourceFactory deste planeta, estando ela na lista
		///  de available, ou all, ou seja o que for. Assim que este método seja chamado
		///  não será possível criar mais recursos associados áquela ResourceFactory.
		/// </remarks>
		public void supressFactory( string factory )
		{
			dequeue(factory);
			
			if( supressFactory(all, factory) )
				return;

			if( supressFactory(available, factory) )
				return;
		}
		
		/// <summary>Elimina uma ResourceFactory de uma ICollection</summary>
		/// <remarks>Retorna true se removeu</remarks>
		private bool supressFactory( ResourceBuilder collection, string factory )
		{
			IDictionaryEnumerator it = collection.GetEnumerator();
			object key = null;

			while( it.MoveNext() ) {
				ResourceFactory info = (ResourceFactory) it.Value;
				if( info.Name == factory ){
					key = it.Key;
					break;
				}
			}

			if( key != null ) {
				collection.Remove(key);
				return true;
			}
				
			return false;
		}
	
		/// <summary>Verifica se há novas Factories disponíveis</summary>
		/// <remarks>Retorna true se há novas factories disponíveis.</remarks>
		public bool checkDependencies()
		{
			Universe.Events.trace("ResourceInfo {0}(id={1}) Checking Dependencies", Owner.Identifier, Owner.Id);
			Hashtable toAdd = null;
			IDictionaryEnumerator it = all.GetEnumerator();
			bool news = false;
			
			Universe.Events.trace("ResourceInfo {0}(id={1}) Unavailable: {2}", Owner.Identifier, Owner.Id, all.Count);
			while( it.MoveNext() ) {
				bool dependenciesAvailable = true;
				Action[] dependencies = ( (ResourceFactory) it.Value ).Dependencies;
		
				if( dependencies != null ) {
					foreach( Action action in dependencies ) {
						if( ! action.evaluate(owner) ) {
							dependenciesAvailable = false;
							break;
						}
					}
				}
		
				// se estiverem...
				if( dependenciesAvailable ) {
					news = true;
					if( toAdd == null ) {
						toAdd = new Hashtable();
					}
					toAdd.Add( it.Key, it.Value );
				}
			}
			
			Universe.Events.trace("ResourceInfo {0}(id={1}) New Techs Available? {2}", Owner.Identifier, Owner.Id, news);
			// remover
			if( news ) {
				IDictionaryEnumerator e = toAdd.GetEnumerator();
				while( e.MoveNext() ) {
					ResourceFactory fact = (ResourceFactory) e.Value;
					
					Universe.Events.trace("ResourceInfo {0}(id={1}) Now Available: {2}", Owner.Identifier, Owner.Id, fact.Name);
					
					all.Remove(e.Key);
					available.Add( e.Key, e.Value );
					callOnAvailable( fact );
					if( OnAvailable != null ) {
						OnAvailable(fact);
					}

					notify( "Available", new string[] {owner.ToString(), fact.Name } );
			}
			}
			
			return news;
		}
		
		#endregion
		
		#region Resource Methods
		
		/// <summary>Retorna os recursos disponíveis</summary>
		public Hashtable Resources {
			get {
				return resources;
			}
			set {
				resources = value;
			}
		}
				
		/// <summary>Coloca uma quantidade de um recurso a zero</summary>
		public void reset( string resource )
		{
			Resource res = getResource(resource);

			if( res == null ) {
				res = available.create(resource);
				resources.Add( res, 0 );
			} else {
				resources[res] = 0;
			}
		}
		
		/// <summary>Coloca todos os recursos disponíveis desta categoria a zero</summary>
		public void reset()
		{
			foreach( string resource in AvailableFactories.Keys ) {
				reset(resource);
			}
		}

		/// <summary>Adiciona uma quantidade de recursos ao planeta</summary>
		public Resource addResource( QueueItem item )
		{
			if( !available.ContainsKey( item.FactoryName ) ) {
				available.Add( item.FactoryName, item.Factory );
				Resource res = addResource( item.FactoryName, item.Quantity );
				available.Remove( item.FactoryName );
				return res;
			} else {
				return addResource( item.FactoryName, item.Quantity );
			}
		}
		
		/// <summary>Adiciona uma quantidade de recursos ao planeta</summary>
		public Resource addResource( string resource, int quantity )
		{
			Universe.Events.trace("[START] ResourceInfo {2} {0}(id={1}) adding {3} {4}", Owner.Identifier, Owner.Id, Category, quantity, resource);
		
			if( ! available.ContainsKey(resource) ) {
				StringWriter writer = new StringWriter();
				writer.Write("Available[");
				foreach( string a in AvailableFactories.Keys ) {
					writer.Write(a+" ");
				}
				writer.Write("] All [" );
				foreach( string a in AllFactories.Keys ) {
					writer.WriteLine(a+" ");
				}
				writer.Write("]");
				Universe.Events.trace("ResourceInfo {2} {0}(id={1}) Resource {3} not available! {4}", Owner.Identifier, Owner.Id, Category, resource, writer.ToString());
				throw new RuntimeException(category+" - Trying to add an unavailable resource: "+resource + " " + writer.ToString() );
			}

			Resource res = getResource(resource);
			
			if( !checkIntrinsicCost(res, quantity) ) {
				return res;
			}

			if( res == null ) {
				res = available.create(resource);
				resources.Add( res, quantity );
			} else {
				int val = (int) resources[res];
				resources[res] = quantity + val;
			}

			owner.updateModifiers(res, quantity);
			
			Action[] actions = res.Factory.OnCompleteActions;
			if( actions != null ) {
				foreach( Action action in actions ) {
					action.action(Owner, quantity);
				}
			}
			
			if( OnComplete != null ) {
				OnComplete( res, quantity);
			}
			
			Universe.Events.trace("[END] ResourceInfo {2} {0}(id={1}) added {3} {4}", Owner.Identifier, Owner.Id, Category, quantity, resource);

			return res;
		}
		
		/// <summary>verifica as Action's de Cost aquando de um addResource</summary>
		private bool checkIntrinsicCost( Resource res, int quantity )
		{
			if( res == null || Category != "Intrinsic" || res.Name == "spy" || res.Name == "marine") {
				return true;
			}
			
			Action[] cost = res.Factory.CostActions;
			if( cost != null ) {
				foreach( Action action in cost ) {
					if( !action.evaluate(Owner, quantity) ) {
						return false;
					}
				}
			}
			
			return true;
		}
		
		#endregion
			
		#region General Instance Properties
			
		/// <summary>Retorna o Dono deste ResourceInfo</summary>
		public IResourceManager Owner {
			get {
				return owner;
			}
		}
		
		/// <summary>Retorna a categoria deste ResourceInfo</summary>
		public string Category {
			get  {
				return category;
			}
		}
		
		#endregion
		
		#region General Utilities
		
		/// <summary>Indica se um recurso pode ser removido</summary>
		public bool canTake( Resource res, int quantity )
		{
			Action[] onremove = res.Factory.OnRemoveActions;
			if( onremove != null ) {
				foreach( Action a in onremove ) {
					if( ! a.evaluate(owner) ) {
						return false;
					}
				}
			}
			
			return true;
		}
		
		/// <summary>Indica se um recurso pode ser removido</summary>
		public bool canTake( string resource, int quantity )
		{
			Resource res = getResource(resource);
			if( res == null ) {
				return false;
			}
			return canTake( res, quantity);
		}
		
		/// <summary>Retira uma certa quantidade de um recurso específicado</summary>
		/// <remarks>
		///  Este método tem em conta que defacto existe a quantidade necessária, se não
		///	 existir, o valor fica negativo
		/// </remarks>
		/// <returns>True se a operação for sucedida</returns>
		public bool take( string resource, int quantity )
		{
			Resource key = getResource(resource);
			if( key == null ) {
				return false;
			}
						
			if( !canTake(key, quantity) ) {
				return false;
			}
			
			removeModifiers(key, quantity);

			Action[] onremove = key.Factory.OnRemoveActions;
			if( onremove != null ) {
				foreach( Action a in onremove ) {
					a.action(owner);
				}
			}

			object o = resources[key];
			resources[key] = ((int)o) - quantity;

			if( OnRemove != null ) {
				OnRemove(key, quantity);
			}

			return true;
		}
		
		/// <summary>Remove os Modifiers que um recurso tinha adicionado ao Owner</summary>
		public void removeModifiers( Resource res, int quantity )
		{
			Hashtable hash = res.Modifiers;
			if( hash == null ) {
				return;
			}
			
			IDictionaryEnumerator it = hash.GetEnumerator();
			while( it.MoveNext() ) {
				owner.removeModifier( (string)it.Key, ((int) it.Value) * quantity );
			}
		}
		
		/// <summary>Invoca todas as action's de OnAvailable da ResourceFactory</summary>
		private void callOnAvailable( ResourceFactory factory )
		{
			Action[] onavailable = factory.OnAvailableActions;
	
			if( onavailable != null ) {
				foreach( Action action in onavailable ) {
					action.action(owner);
				}
			}
		}
		
		#endregion

		#region Messaging Utility

		/// <summary>Envia uma mensagem relativa a um evento</summary>
		private void notify( string eventType, string[] args )
		{
			if( Category == "Intrinsic" ) {
				return;
			}

			MessageInfo info = Messenger.getMessageInfo(Category + eventType);
			if( info == null ) {
				return;
			}

			Messenger.Send(owner, info, args);
		}

		#endregion
		
		#region Prizes
		
		/// <summary>Verifica se há prémios a atribuir</summary>
		private void checkPlanetPrizes( QueueItem current )
		{
			if( Category == "Building" ) {
				if( current.FactoryName == "StarPort" ) {
					Universe.instance.addPrize(PrizeCategory.Building, "StarPortPrize", (Planet) Owner);
				}
				if( current.FactoryName == "CommsSatellite" ) {
					Universe.instance.addPrize(PrizeCategory.Building, "CommsSatellitePrize", (Planet) Owner);
				}
			}
		}
		
		#endregion
	
	};

}
