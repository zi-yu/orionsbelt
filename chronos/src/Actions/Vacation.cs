// created on 9/14/2005 at 9:06 AM

using Chronos.Resources;
using Chronos.Core;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'vacation'</summary>
	public class Vacation : Action {
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager manager )
		{
			Ruler ruler = manager as Ruler;
			if( ruler == null ) {
				ruler = (Ruler) ((Planet) manager).Owner;
			}
			
			return !ruler.InVacation;
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
			get{ return "vacation"; }
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
			return base.log() + "Vacation Mode"; 
		}
		
		#endregion
		
	};
	
}
