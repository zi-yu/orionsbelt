
using System;
using System.Collections;

namespace Alnitak.News {
	
	public class Entry {
		
		#region Instance Fields
		
		private DateTime issued;
		private string content;
		private string title;
		private int id;
		private string lang;
		
		#endregion
		
		#region Ctors
		
		/// <summary>Cria a notcia</summary>
		public Entry( DateTime date, string content, string title, string lang)
		{
			this.issued = date;
			this.content = content;
			this.title = title;
			this.id = -1;
			this.lang = lang;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica a data da notcia</summary>
		public DateTime Issued {
			get { return issued; }
		}
		
		/// <summary>Indica o ttulo da notcia</summary>
		public string Title {
			get { return title; }
		}
		
		/// <summary>Indica o contedo da notcia</summary>
		public string Content {
			get { return content; }
		}
		
		/// <summary>Indica o ID da entrada</summary>
		public int Id {
			get { return id; }
			set { id = value; }
		}

		public string Language {
			get { return lang; }
			set { lang = value; }
		}
		
		#endregion
		
	};
}
