// created on 9/7/2005 at 9:50 AM

using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Interfaces;
using DesignPatterns;
using DesignPatterns.Attributes;

namespace Chronos.Sabotages.Factories {

	[FactoryKey("BuildingQueue")]
	public class BuildingQueueSabotageFactory : SabotageFactory {
		
		#region IFactory Implementation
		
		/// <summary>Retorna-se a sei próprio</summary>
		public override object create( object args )
		{
			return new BuildingQueueSabotage();
		}
		
		#endregion
	
	};

}