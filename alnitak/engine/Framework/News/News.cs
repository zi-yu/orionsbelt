
using System;
using System.Collections;

namespace Alnitak.News {
	
	public class NewsList {
	
		#region Instance Fields
		
		private Hashtable atts = null;
		private ArrayList news = new ArrayList();
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Contentor com atributos genricos</summary>
		public Hashtable Attributes {
			get { return atts; }
		}
		
		/// <summary>Indica as notcias</summary>
		public ArrayList List {
			get { return news; }
		}
		
		#endregion
		
	};
}
