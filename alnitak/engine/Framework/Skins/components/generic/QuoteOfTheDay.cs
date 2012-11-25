// created on 12/23/2005 at 1:58 PM

using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;
using Chronos.Core;
using Chronos.Info;
using System;

namespace Alnitak {
	
	public class QuoteOfTheDay : Control {

		protected override void Render( HtmlTextWriter writer )
		{
			writer.WriteLine("<div id='quote'>{0}</div>", GetQuote());
		}
		
		private string GetQuote()
		{
			object quote = Page.Cache["Quote-of-the-Day"];
			if( quote != null ) {
				return quote.ToString();
			}
			
			string newQuote = "There is no quote for today...";
			ArrayList list = Wiki.GetTopicSpacedLines("Orionsbelt.Quotes");
			
			if( list.Count > 0 ) {
				newQuote = list[ DateTime.Now.DayOfYear % list.Count ].ToString();
			}
			
			Page.Cache["Quote-of-the-Day"] = newQuote;
			return newQuote;
		}
	};

}
