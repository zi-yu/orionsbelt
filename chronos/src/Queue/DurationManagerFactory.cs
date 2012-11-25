// created on 6/15/2004 at 9:51 AM

using System;
using System.Collections;
using DesignPatterns;
using System.Xml;
using System.Runtime.Serialization;
using Chronos.Exceptions;
using Chronos.Utils;
using Chronos.Resources;

namespace Chronos.Queue {
	
	/// <summary>Classe base das ActionFactory's</summary>
	[Serializable]
	public abstract class DurationManagerFactory : IFactory, IDurationManager, ISerializable {

		#region IFactory Implementation
	
		/// <summary>Retorna o DurationManager associado</summary>
		public virtual object create( object args )
		{
			return this;
		}

		#endregion

		#region IDurationManager Implementation
		
		/// <summary>Retorna uma duração base</summary>
		public int baseDuration( IResourceOwner owner, ResourceFactory factory, int quantity )
		{
			float val = factory.Duration.Value * quantity / factory.Duration.Quantity;
			if( val < 1 ) {
				return 1;
			}
			return MathUtils.round(val);
		}
		
		/// <summary>Retorna a duração de um QueueItem</summary>
		public virtual int initialDuration( IResourceOwner owner, ResourceFactory factory, int quantity )
		{
			int val = baseDuration(owner, factory, quantity);
			val += forceAdjust(owner, factory);
			if ( val < 1 ) {
				return 1;
			}
			return val;
		}
		
		/// <summary>Ajusta a duração de um QueueItem</summary>
		public virtual int adjust( IResourceOwner owner, ResourceFactory factory )
		{
			return 0;
		}
		
		/// <summary>Ajusta a duração de um QueueItem</summary>
		public abstract int forceAdjust( IResourceOwner owner, ResourceFactory factory );

		/// <summary>Indica o tipo deste manager</summary>
		public abstract string Name {get;}

		#endregion

		#region Serialization

		/// <summary>Classe auxiliar</summary>
		[Serializable]
		private sealed class DurationManagerSerializationHelper : IObjectReference {
			
			#region Instance Fields
			
			public string key = null;

			#endregion
			
			/// <summary>Retorna a ResourceFactoryAssociada
			public object GetRealObject( StreamingContext context )
			{
				object obj = QueueItem.Managers[key];
				if( obj == null ) {
					throw new Exception("NULL!");
				}
				return obj;
			}
			
		};

		/// <summary>Serializa este objecto</summary>
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.SetType( typeof(DurationManagerSerializationHelper) );
			info.AddValue("key", Name );
		}

		#endregion

	
	};
}
