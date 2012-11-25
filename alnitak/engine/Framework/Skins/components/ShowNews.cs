using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;
using Alnitak.News;

namespace Alnitak {
	
	public class ShowNews : Control {
		
		#region Instance Fields
		
		private FeedFormat format;
		
		#endregion
		
		#region Instance Fields
		
		/// <summary>Indica o formato do Url</summary>
		public FeedFormat Format {
			get { return format; }
			set { format = value; }
		}
	
		#endregion
		
		#region Control Rendering
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			OrionGlobals.RegisterRequest( Chronos.Messaging.MessageType.Generic, CultureModule.getContent("section_index") );
			
			NewsList news = NewsUtility.Persistence.GetNews();
			if( news != null && news.List.Count > 0 ) {
				writeNews(writer, news);
			} else {
				ILanguageInfo info = CultureModule.getLanguage();
				/*writer.WriteLine("<div><b>{0}</b></div>",
								 	info.getContent("no_news_title")
								 );*/
				writer.WriteLine("<div>{0}</div>", info.getContent("no_news_message"));
			}
		}
		
		/// <summary>Mostra uma lista de notcias</summary>
		private void writeNews( HtmlTextWriter writer, NewsList list )
		{
			foreach( Entry entry in list.List ) {
				writer.WriteLine("<div><span>{1}/{2}/{3}</span><br/>{0}</b></div>",
								entry.Title,
								entry.Issued.Day,
								entry.Issued.Month,
								entry.Issued.Year
							);
				writer.WriteLine("<div>{0}</div>", entry.Content);
				writer.WriteLine("<hr noshade='noshade' size='1'/>");

			}
			
			writer.WriteLine("<div align='center'><a href='{2}'>{1} <img src='{0}' /></a></div><br/>",
							OrionGlobals.getCommonImagePath("xml.gif"),
							CultureModule.getLanguage().getContent("news_feed"),
							OrionGlobals.resolveBase("rss.aspx")
						);
		}
		
		#endregion
	};
	
}
