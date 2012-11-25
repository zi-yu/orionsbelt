// created on 6/15/2004 at 9:42 AM
using Chronos.Resources;

namespace Chronos.Queue {

	public interface IDurationManager {
		
		/// <summary>Indica a dura��o base de um item</summary>
		int baseDuration( IResourceOwner owner, ResourceFactory factory, int quantity );
	
		/// <summary>Retorna a duração de um QueueItem</summary>
		int initialDuration( IResourceOwner owner, ResourceFactory factory, int quantity );
		
		/// <summary>Ajusta a duração de um QueueItem< ou não, dependendo de vários factores/summary>
		int adjust( IResourceOwner owner, ResourceFactory factory );

		/// <summary>Ajusta a duração de um QueueItem</summary>
		int forceAdjust( IResourceOwner owner, ResourceFactory factory );
	};

}
