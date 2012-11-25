// created on 5/22/04 at 12:49 a

namespace Chronos.Actions {
	
	/// <summary>Class base das Action's com tipo e valor</summary>
	public abstract class TypeValueAction : Action {
		
		#region Instance Fields
		
		private string actionKey;
		private string actionValue;
		
		#endregion
		
		#region Ctors
			
		/// <summary>Construtor</summary>
		protected TypeValueAction( string k, string v )
		{
			actionKey = k;
			actionValue = v;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna a key desta Action</summary>
		public virtual string Key {
			get { return actionKey; } 
		}
		
		/// <summary>Retorna o Value desta Action</summary>
		public virtual string Value {
			get { return actionValue; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams( int requestedQuantity )
		{
			return new string[] { Key, Value };	
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			return base.log() + Key + " -> " + Value; 
		}
		
		#endregion
	
	};
	
}
