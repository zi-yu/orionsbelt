// created on 3/7/04 at 11:18 a

using System;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'supress-factory'</summary>
	public class DisallowBuild : TypeValueAction {
	
		/// <summary>Construtor</summary>
		public DisallowBuild( string resourceType, string resourceFactory )
			: base( resourceType, resourceFactory )
		{
		}
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			return true;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
		{
			ResourceManager manager = planet as ResourceManager;
			if( null == manager ) {
				return true;
			}
			
			ResourceInfo info = manager.getResourceInfo(Key);
			if( info.AvailableFactories.Contains(Value) ) {
				info.AvailableFactories.Remove(Value);
				return true;
			}
			return false;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager planet )
		{
			ResourceManager manager = planet as ResourceManager;
			if( null == manager ) {
				return true;
			}

			ResourceInfo info = manager.getResourceInfo(Key);
			if( info.AvailableFactories.Contains(Value) ) {
				return true;
			}
			info.AvailableFactories.Add(Value, Universe.getFactory( manager.Identifier, Key, Value) );
			return true;
		}
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name
		{
			get{ return "disallow-build"; }
		}
		
	};
	
}
