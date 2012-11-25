
using System;
using System.Collections;
using System.Runtime.Serialization;
using DesignPatterns;

namespace Chronos.Messaging {
	
	/// <summary>
	/// Entidade que simboliza um tipo de mensagem
	/// </summary>
	[Serializable]
	public abstract class MessageInfo : IFactory, ISerializable {
	
		#region Instance Fields

		private MessageType messageType;
			
		#endregion

		#region Ctors

		/// <summary>Construtor de MessageInfo</summary>
		public MessageInfo( MessageType type )
		{
			messageType = type;
		}

		#endregion

		#region Instance Properties

		/// <summary>Indica a categoria deste tipo de mensagem</summary>
		public MessageType Category {
			get { return messageType; }
		}

		/// <summary>Indica o nome desta mensagem</summary>
		public string Name {
			get { return GetType().Name; }
		}

		#endregion

		#region Overridable Members

		/// <summary>Indica se é possível enviar uma resposta a mensagens deste tipo</summary>
		public abstract bool MayReply { get; }
		
		/// <summary>Retorna uma string que descreve esta mensagem</summary>
		public virtual string log( Message message )
		{
			return Name;
		}

		/// <summary>Retorna uma string que descreve esta mensagem com base numa string de localização</summary>
		public abstract string localize( Message message, string localization, ITranslator translator );

		#endregion

		#region IFactory Implementation

		/// <summary>Retorna este tipo de mensagem</summary>
		public object create( object args )
		{
			return this;
		}

		#endregion

		#region Serialization 

		/// <summary>Classe auxiliar</summary>
		[Serializable]
		private sealed class MessageInfoSerializationHelper : IObjectReference {
			
			#region Instance Fields
			
			public string key = null;

			#endregion
			
			/// <summary>Retorna a ResourceFactoryAssociada
			public object GetRealObject( StreamingContext context )
			{
				return Messenger.getMessageInfo(key);
			}
			
		};

		/// <summary>Serializa este objecto</summary>
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.SetType( typeof(MessageInfoSerializationHelper) );
			info.AddValue("key", Name );
		}

		#endregion
			
	};
}
