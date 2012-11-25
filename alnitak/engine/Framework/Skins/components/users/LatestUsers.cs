// created on 30-12-2004 at 11:43

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {

	public class LatestUsers : Control {
		
		#region Instance Fields
		
		private string separator;
		private bool showDate;
		
		#endregion
		
		#region Instance Properties
		
		public string Separator {
			get { return separator; }
			set { separator = value; }
		}
		
		
		public bool ShowDate {
			get { return showDate; }
			set { showDate = value; }
		}
		
		#endregion
		
		#region Ctors
		
		/// <summary>Constri o controlo</summary>
		public LatestUsers()
		{
			Separator = " - ";
			ShowDate = true;
		}
		
		#endregion
		
		#region Control Rendering
	
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			UserWatcher queue = (UserWatcher) Page.Application["AlnitakOnlineUsers"];
			if( queue == null || !queue.HasRegisteredUsers ) {
				writer.WriteLine("?");
				return;
			}
			int i = queue.List.Count;
			IDictionaryEnumerator it = queue.List.GetEnumerator();
			
			while(it.MoveNext()) {
				writer.Write( OrionGlobals.getLink((User)it.Value) );
				/*if(ShowDate) {
					writer.Write(ParseDate((DateTime) it.Key));
				}*/
				if ( --i != 0 ) {
					writer.WriteLine(Separator);
				}
			}
		}
		
		private string ParseDate( DateTime date )
		{
			return string.Format("({0}/{1} - {2}:{3})", date.Day, date.Month, date.Hour, date.Minute);
		}
		
		#endregion
	
	};
	
}
