// created on 3/15/04 at 3:37 a

using System;
using Chronos.Exceptions;
using Chronos.Resources;
using Chronos.Core;
using Chronos.Utils;
using DesignPatterns;

namespace Chronos.Queue {

	[Serializable]
	public class QueueItem {
	
		#region Private Instance Fields
	
		private ResourceFactory factory;
		private IResourceManager owner;
		private IDurationManager duration;
		private int remainingTurns;
		private int quantity;
		
		#endregion
		
		#region Static Members
		
		private static readonly FactoryContainer managers = new FactoryContainer( typeof(DurationManagerFactory) );
		private const string defaultManagerKey = "default";
		
		/// <summary>Retorna todos os IDurationManager's registados</summary>
		public static FactoryContainer Managers {
			get {
				return managers;
			}
		}
		
		#endregion
		
		#region Instance Ctors
		
		/// <summary>Construtor</summary>
		public QueueItem( IResourceManager _owner, ResourceFactory fact, int quant, double productionFactor )
		{
			factory = fact;
			quantity = quant;
			owner = _owner;
			duration = getManager(fact);
			
			remainingTurns = ProcessDuration(Duration, productionFactor);
			if( remainingTurns < 1 ) {
				remainingTurns = 1;
			}
		}
		
		#endregion
		
		#region Functional Methods
		
		/// <summary>Actualiza os turnos que faltam para acabar</summary>
		public static IDurationManager getManager( ResourceFactory resFactory )
		{
			string factory = resFactory.Duration.DependencyFunction;
			if( factory == null || factory.Length == 0 ) {
				factory = defaultManagerKey;
			}

			return (IDurationManager) Managers.create(factory);
		}
		
		/// <summary>Decrementa o tempo que falta para acabar</summary>
		public void process()
		{
			--remainingTurns;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica se esta operação já está finalizada</summary>
		public bool IsFinished {
			get {
				return RemainingTurns <= 0;
			}
		}
		
		/// <summary>Retorna o número de turnos que faltam para acabar</summary>
		public int RemainingTurns {
			get {
				return remainingTurns;
			}
			set {
				remainingTurns = value;
			}
		}
		
		/// <summary>Retorna a ResourceFactory que tá a ser usada</summary>
		public ResourceFactory Factory {
			get {
				return factory;
			}
		}
		
		/// <summary>Retorna o nome da ResourceFactory que está a ser usada</summary>
		public string FactoryName {
			get {
				return Factory.Name;
			}
		}
		
		/// <summary>Retorna a quantidade de recursos que vai ser construída</summary>
		public int Quantity {
			get {
				return quantity;
			}
		}
		
		/// <summary>Retorna o tempo base total necessário para realizar esta terafa</summary>
		public int Duration {
			get {
				return duration.initialDuration(Owner, Factory, Quantity);
			}
		}
		
		/// <summary>Retorna o Owner deste QueueItem</summary>
		public IResourceManager Owner {
			get {
				return owner;
			}
		}
		
		#endregion
		
		#region Static Members
		
		/// <summary>Indica o tempo base</summary>
		public static int BaseDuration( IResourceOwner owner, ResourceFactory fact, int quantity )
		{
			IDurationManager manager = getManager(fact);
			return manager.baseDuration(owner, fact, quantity);
		}
		
		/// <summary>Indica o tempo real</summary>
		public static int RealDuration( IResourceOwner owner, ResourceFactory fact, int quantity )
		{
			IDurationManager manager = getManager(fact);
			return ProcessDuration(manager.initialDuration(owner, fact, quantity), owner.ProductionFactor);
		}
		
		/// <summary>Indica a duração real com base num factor de produção</summary>
		public static int ProcessDuration( int duration, double factor )
		{
			return (int) Math.Ceiling(duration * factor);
		}
		
		#endregion
	
	};

}
