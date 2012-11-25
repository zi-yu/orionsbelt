// created on 3/12/04 at 11:53 a

using Chronos.Exceptions;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'add'</summary>
	public class Add : TypeValueAction {
	
		#region Instance Fields
	
		private int quantity;
		
		#endregion
		
		#region Ctor
	
		/// <summary>Construtor</summary>
		public Add( string type, string resource, string quantity )
			: base( type, resource )
		{
			this.quantity = int.Parse(quantity);
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			return planet.isFactoryAvailable(Key, Value, true);
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
		{
			return action(planet, 1);
		}
		
		public override bool action( IResourceManager planet, int repeatNumber )
		{
			return planet.addResource( Key, Value, quantity*repeatNumber ) != null;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			return manager.take( Key, Value, quantity );
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name
		{
			get{ return "add"; }
		}
		
		public int Quantity {
			get { return quantity; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams( int requestedQuantity )
		{
			return new string[] { Key, Value, (requestedQuantity*quantity).ToString() };
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
