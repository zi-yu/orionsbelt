// created on 9/7/2005 at 9:48 AM

using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Interfaces;
using DesignPatterns;

namespace Chronos.Sabotages.Factories {

	public abstract class SabotageFactory : IFactory {
		
		#region IFactory Implementation
		
		/// <summary>Retorna-se a sei próprio</summary>
		public abstract object create( object args );
		
		#endregion
	
	};

}