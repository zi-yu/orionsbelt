// created on 9/4/2005 at 12:10 PM

using Chronos.Resources;
using Chronos.Core;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'housingAvailable'</summary>
	public class HousingAvailable : Action {
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager manager )
		{
			return evaluate(manager, 1);
		}
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager manager, int quantity )
		{
			Planet planet = manager as Planet;
			if( planet == null ) {
				return false;
			}
			
			return (planet.Population + quantity) <= planet.Housing;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
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
		
		/// <summary>Retorna "housingAvailable"</summary>
		public override string Name {
			get{ return "housingAvailable"; }
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
			return base.log() + "Housing Available"; 
		}
		
		#endregion
		
	};
	
}
