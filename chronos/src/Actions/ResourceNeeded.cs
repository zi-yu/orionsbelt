// created on 2/29/04 at 9:48 a

using Chronos.Resources;
using Chronos.Utils;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'resource-needed'</summary>
	public class ResourceNeeded : TypeValueAction {
	
		#region Instance Fields
	
		private int quantity;
	
		#endregion
		
		#region Ctors
	
		/// <summary>Construtor</summary>
		public ResourceNeeded( string intrinsic, string quantity )
			: base( intrinsic, quantity )
		{
#if DEBUG
			this.quantity = 1;
#else
			this.quantity = int.Parse(quantity);
#endif
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			return evaluate(planet,1);
		}
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		/// <remarks>Pode não fazer sentido chamar este método. Se não houver o seu override
		///	por defeito ele vai chamar o evaluate( IResourceManager planet );. Quando este método
		///	é necessário, o 'repeatNumber' indica o múmero de vezes que accção se vai repetir
		/// </remarks>
		public override bool evaluate( IResourceManager planet, int repeatNumber )
		{
			return planet.isResourceAvailable(Key, quantity * repeatNumber);
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
		{
			return action(planet,1);
		}
		
		/// <summary>Efectua a acaoo desta Action para uma determinada quantidade de recursos</summary>
		public override bool action( IResourceManager planet, int repeatNumber )
		{
			planet.take( Key, quantity * repeatNumber );
			return true;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			return undo(manager, 1);
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager, int repeatNumber )
		{
			return manager.addResource(Key, quantity * repeatNumber) != null;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name {
			get{ return "resource-needed"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams( int requestedQuantity )
		{
			return new string[] { Key, (requestedQuantity*quantity).ToString(), Value };
		}
		
		/// <summary>Indica a quantidade necessária</summary>
		public int Quantity {
			get { return quantity; }
		}
		
		#endregion
	};

	
}

