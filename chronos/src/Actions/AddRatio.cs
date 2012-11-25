// created on 3/12/04 at 11:53 a

using Chronos.Exceptions;
using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'add'</summary>
	public class AddRatio : TypeValueAction {
		
		#region Ctor
		
		/// <summary>Construtor</summary>
		public AddRatio( string type, string resource )
			: base( type, resource )
		{
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			return true;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager manager )
		{
			manager.addRatio(Key,int.Parse(Value) );
			return true;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			manager.removeRatio(Key, int.Parse(Value));
			return true;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name
		{
			get{ return "addRatio"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams( int requestedQuantity )
		{
			return new string[] { Key, Value };
		}
		
		#endregion
		
	};
	
	
}
