// created on 6/15/2004 at 9:46 AM

using System;
using System.Runtime.Serialization;
using Chronos.Utils;
using Chronos.Resources;
using DesignPatterns.Attributes;

namespace Chronos.Queue {

	[FactoryKey("default")]
	[Serializable]
	public class GeneralDurationManager : DurationManagerFactory {
		
		#region IDurationManager Implementation
		
		/// <summary>Ajusta a duração de um QueueItem</summary>
		public override int forceAdjust( IResourceOwner owner, ResourceFactory factory )
		{
			return 0;
		}

		/// <summary>Indica o tipo deste manager</summary>
		public override string Name {
			get { return "default"; }
		}

		
		#endregion
	
	};

}
