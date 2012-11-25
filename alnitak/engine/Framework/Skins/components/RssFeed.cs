using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;
using Chronos.Core;
using System;
using Alnitak.News;

namespace Alnitak {
	
	public class RssFeed : Page {
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			string url = OrionGlobals.getConfigurationValue("alnitak", "url");
			
			writer.WriteLine("<rss version=\"2.0\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:trackback=\"http://madskills.com/public/xml/rss/module/trackback/\" xmlns:wfw=\"http://wellformedweb.org/CommentAPI/\" xmlns:slash=\"http://purl.org/rss/1.0/modules/slash/\">");
			writer.WriteLine("\t<channel>");
			writer.WriteLine("\t<title>Orion's Belt</title>");
			writer.WriteLine("\t<link>{0}</link>", url);
			writer.WriteLine("\t<description>Orion's Belt :: Notícias</description>");
			writer.WriteLine("\t<dc:language>pt-PT</dc:language>");
			writer.WriteLine("\t<generator>Alnitak.RssFeed</generator>");
			writer.WriteLine("\t</channel>");
			writeEntries(writer, url);
			writer.WriteLine("</rss>");
		}
		
		private void writeEntries( HtmlTextWriter writer, string url )
		{
			NewsList list = NewsUtility.Persistence.GetNews();
			foreach( Entry entry in list.List ) {
				writer.WriteLine("\t<item>");
				writer.WriteLine("\t\t<dc:creator>Orion's Belt</dc:creator>");
				writer.WriteLine("\t\t<title>{0}</title>", entry.Title);
				writer.WriteLine("\t\t<link>{0}index.aspx</link>", url);
				writer.WriteLine("\t\t<pubDate>{0}</pubDate>", ParseDate(entry.Issued));
				writer.WriteLine("\t\t<guid>{0}</guid>", entry.Id);
				writer.WriteLine("\t\t<description></description>");
				writer.WriteLine("\t\t<body xmlns=\"http://www.w3.org/1999/xhtml\">{0}</body>", entry.Content);
				writer.WriteLine("\t</item>");
			}
		}
		
		private string ParseDate( DateTime date )
		{
			return date.ToString();
		}
	};
	
}

