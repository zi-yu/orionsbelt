// created on 6/15/2004 at 10:14 AM

using System;
using Chronos.Utils;
using Chronos.Resources;
using Chronos.Core;
using DesignPatterns.Attributes;

namespace Chronos.Queue {

	[FactoryKey("value")]
	[Serializable]
	public class ValueDurationManager : DurationManagerFactory, IDurationManager {
		
		#region IDurationManager Implementation
		
		/// <summary>Ajusta a duração de um QueueItem</summary>
		public override int forceAdjust( IResourceOwner owner, ResourceFactory factory )
		{
			if( owner == null ) {
				return 0;
			}
			
			int val = owner.getResourceCount(factory.Duration.Dependecy);
			int trunc = 15;

			int toReturn = -val / 400;
		
			if( toReturn < -trunc ) {
				return -trunc;
			}
			
			if( toReturn > trunc ) {
				return trunc;
			}
			
			return toReturn;
		}

		/// <summary>Indica o tipo deste manager</summary>
		public override string Name {
			get { return "value"; }
		}
		
		#endregion
		
	};

}
