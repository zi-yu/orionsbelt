// created on 3/7/04 at 11:18 a

using System;
using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'supress-factory'</summary>
	public class SupressFactory : TypeValueAction {
	
		/// <summary>Construtor</summary>
		public SupressFactory( string resourceType, string resourceFactory )
			: base( resourceType, resourceFactory )
		{
		}
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			return planet.isFactoryAvailable( Key, Value, true );
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
		{
			planet.supressFactory( Key, Value );
			return true;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			throw new Exception("Impossível anular o efeito do 'supress-factory'!");
		}
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name
		{
			get{ return "supress-factory"; }
		}
		
	};
	
}
