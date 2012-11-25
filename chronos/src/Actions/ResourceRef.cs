// created on 2/23/04 at 3:45 a

using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'resource-ref'</summary>
	public class ResourceRef : TypeValueAction {
	
		#region Ctors
	
		/// <summary>Construtor</summary>
		public ResourceRef( string resourceType, string resourceFactory )
			: base( resourceType, resourceFactory )
		{
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			return planet.isResourceAvailable(Key, Value, 1);
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
			get{ return "resource-ref"; }
		}
		
		#endregion
		
	};
	
}
