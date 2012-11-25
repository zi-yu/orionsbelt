// created on 5/22/04 at 12:46 a

using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'static'</summary>
	public class BattlesFought : Action {
		
		#region Instance Fields
		
		protected int ammount;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor</summary>
		public BattlesFought( int _ammount )
		{
			ammount = _ammount;
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager manager )
		{
			if( manager is Ruler ) {
				return ((Ruler)manager).BattlesFought >= ammount;
			}
			
			Ruler owner = (Ruler) manager.Owner;
			return owner.BattlesFought >= ammount;
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
		
		/// <summary>Retorna "battlesfought"</summary>
		public override string Name {
			get{ return "battlesFought"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams(int requestedQuantity)
		{
			return new string[] { ammount.ToString() };
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			return base.log() + "Ammount: " + ammount;
		}
		
		#endregion
		
	};
	
}
