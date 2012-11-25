// created on 3/7/04 at 11:18 a

using System;
using Chronos.Core;
using Chronos.Resources;
using Chronos.Queue;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'supress-factory'</summary>
	public class QueueRestriction : TypeValueAction {
		
		/// <summary>Construtor</summary>
		public QueueRestriction( string resourceType, string resourceFactory )
		: base( resourceType, resourceFactory )
		{
		}
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager planet )
		{
			ResourceManager manager = (ResourceManager) planet;

			if( manager.current(Key) != null && (manager.current(Key).FactoryName == Value) ) {
				return false;
			}
			foreach( QueueItem item in manager.getQueueList(Key) ) {
				if( item.FactoryName == Value) {
					return false;
				}
			}
			return true;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
		{
			return true;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager planet )
		{
			return true;
		}
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name
		{
			get{ return "queueRestriction"; }
		}
		
	};
	
}
