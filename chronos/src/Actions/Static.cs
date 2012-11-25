// created on 5/22/04 at 12:46 a

using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'static'</summary>
	public class Static : Action {
	
		#region Instance Fields
	
		protected bool _eval;
		protected bool _action;
		
		#endregion
		
		#region Ctor
	
		/// <summary>Construtor</summary>
		public Static( bool e, bool a )
		{
			_eval = e;
			_action = a;
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			return _eval;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
		{
			return _action;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			return !_action;
		}
		
		#endregion
		
		#region Instance Properties
		
		public bool Evaluate {
			get { return _eval; }
		}
		
		public bool Action {
			get { return _action; }
		}
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name {
			get{ return "static"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams(int requestedQuantity) 
		{
			return new string[] { _eval.ToString(), _action.ToString() };	
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			return base.log() + "Evaluate: " + _eval.ToString() + " Action: " + _action.ToString(); 
		}
		
		#endregion
		
	};
	
}
