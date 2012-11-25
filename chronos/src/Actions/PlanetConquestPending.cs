// created on 7/25/2005 at 4:26 PM

using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'planet-conquest-pending'</summary>
	public class PlanetConquestPending : Action {
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager manager )
		{
			Ruler ruler = manager as Ruler;
			if( ruler == null ) {
				ruler = (Ruler)(((Planet) manager).Owner);
			}

			if( ruler.HasFleetsInConquerState ) {
				return false;
			}

			return true;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager manager )
		{
			return true;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			return true;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna "battles-pending"</summary>
		public override string Name {
			get{ return "planet-conquest-pending"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams(int requestedQuantity) 
		{
			return new string[0];	
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			return base.log() + "There conquest's pending!"; 
		}
		
		#endregion
		
	};
	
}
