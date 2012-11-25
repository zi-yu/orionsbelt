// created on 5/17/04 at 9:52 a

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;
using Alnitak.News; 

namespace Alnitak {
	
	public class HotNews : Control {	
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			NewsList news = NewsUtility.Persistence.GetNews();
			if( news.List.Count > 0 ) {
				Entry last = (Entry)news.List[0];
				
				string id = "hotNews";
				if( (DateTime.Now.DayOfYear - last.Issued.DayOfYear) <= 2 ) {
					id = "reallyHotNews";
				} 
					
				writer.WriteLine("<div id='{2}'><a href='{0}index.aspx'>{1}</a></div>",
					OrionGlobals.AppPath, 
					last.Title,
					id
					);
			}else {
				Visible = false;
			}
		}
		
	};
	
}

