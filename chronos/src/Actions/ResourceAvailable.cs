// created on 3/17/04 at 6:45 a

using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'resource-available'</summary>
	public class ResourceAvailable : TypeValueAction {
	
		#region Instance Fields
	
		private int quantity;
		
		#endregion
		
		#region Ctors
	
		/// <summary>Construtor</summary>
		public ResourceAvailable( string resourceType, string resourceFactory, int quant )
			: base( resourceType, resourceFactory )
		{
			quantity = quant;
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			return planet.isResourceAvailable( Key, Value, quantity );
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
		{
			return evaluate(planet);
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			return true;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name {
			get{ return "resource-available"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams( int requestedQuantity )
		{
			return new string[] { Key, Value, (requestedQuantity*quantity).ToString() };
		}
		
		/// <summary>Indica a quantidade</summary>
		public int Quantity {
			get { return quantity; }
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			return base.log() + " -> " + quantity;
		}
		
		#endregion
		
	};
	
}
