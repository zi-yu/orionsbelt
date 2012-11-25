// created on 1/4/2006 at 7:39 PM

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;
using Alnitak.News; 

namespace Alnitak {
	
	public class ShopItemOfTheDay : Control {
	
		private static ShopItem item = null;	
		private static object sync = new object();
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			SetItem();
			if( item == null ) {
				throw new Exception("Item is NULL!");
			}
			writer.WriteLine("<a href='{0}shop/default.aspx' class='shopItemPreview'><img src='{1}' /></a>",
					OrionGlobals.AppPath,
					item.Img
				);
		}
		
		private static void SetItem()
		{
			lock(sync) {
				if( item != null ) {
					return;
				}
				Hashtable hash = Wiki.GetProductsFromWiki();
				int seed = DateTime.Now.DayOfYear;
				
				ArrayList all = new ArrayList();
				
				foreach( object obj in hash.Values ) {
					ArrayList list = (ArrayList) obj;
					foreach( object someItem in list ) {
						all.Add(someItem);
					}
				}
				if( all.Count != 0 ) {
					item = (ShopItem) all[ seed % all.Count ];
				}else {
					item = new ShopItem("","","");
				}
			}
		}
		
	};
	
}

