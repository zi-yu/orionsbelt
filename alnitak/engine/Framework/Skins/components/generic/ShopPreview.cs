// created on 1/4/2006 at 3:37 PM

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;
using Alnitak.News; 

namespace Alnitak {

	public class ShopItem {
		public string Img;
		public string Url;
		public string Price;
		public ShopItem( string img, string url, string price ) 
		{
			Img = img;
			Url = url;
			Price = price;
		}
	};
	
	public class ShopPreview : Control {	
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			IDictionaryEnumerator it = Wiki.GetProductsFromWiki().GetEnumerator();
			while( it.MoveNext() ) {
				ArrayList list = (ArrayList) it.Value;
				writer.WriteLine( "<h2>{0} ({1})</h2>", CultureModule.getContent(it.Key.ToString()), list.Count );
				foreach( ShopItem item in list ) {
					writer.WriteLine("<a href='{0}' class='shopItem' title='{1} EUR'><img src='{2}'/></a>", item.Url, item.Price, item.Img);
				}
			}
		}
		
	};
	
}

