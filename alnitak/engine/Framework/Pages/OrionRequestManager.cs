
using System;
using System.Collections;
using Chronos.Messaging;

namespace Alnitak {
	
	/// <summary>Gere vários pedidos de uma mesma sessão</summary>
	public class OrionRequestManager {
		
		#region Instance Fields
		
		private ArrayList list;
		
		#endregion
		
		#region Ctor
		
		public OrionRequestManager()
		{
			list = new ArrayList();
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o contentor de pedidos</summary>
		public ArrayList List {
			get { return list; }
			set { list = value; }
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>Regista um Url</summary>
		public void Register( OrionRequest request )
		{
			if( list.Count == 0 ) {
				list.Add(request);
				return;
			}
			
			OrionRequest last = (OrionRequest) list[ list.Count - 1];
			if( last.Url != request.Url ) {
				list.Add(request);
			}
			
		}
		
		/// <summary>Regista um url</summary>
		public void Register( MessageType type, string url, string caption )
		{
			Register( new OrionRequest(type, url, caption) );
		}
		
		#endregion
	};
}
