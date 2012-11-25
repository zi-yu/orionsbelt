
using System;
using Chronos.Messaging;

namespace Alnitak {
	
	/// <summary>Indica um pedido para o alnitak</summary>
	public class OrionRequest {
		
		#region Instance Fields
		
		private MessageType type;
		private string url;
		private string caption;
		
		#endregion
		
		#region Ctor
		
		public OrionRequest( MessageType _type, string _url, string _caption )
		{
			type = _type;
			url = _url;
			caption = _caption;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o tipo do url</summary>
		public MessageType Topic {
			get { return type; }
			set { type = value; }
		}
		
		/// <summary>Indica o URL</summary>
		public string Url {
			get { return url; }
			set { url = value; }
		}
		
		/// <summary>Indica a caption a mostrar</summary>
		public string Caption {
			get { return caption; }
			set { caption = value; }
		}
		
		#endregion
	};
}
