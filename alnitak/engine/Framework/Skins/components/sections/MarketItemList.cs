// created on 8/12/2005 at 11:27 AM

using System.Collections;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Messaging;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Trade;
using System;
using System.Text.RegularExpressions;

namespace Alnitak {

	public class MarketItemList : Control {
	
		#region Control Fields
		
		protected MarketItem[] items;
		
		#endregion
		
		#region Instance Properties
		
		public MarketItem[] Items {
			get { return items; }
			set { items = value; }
		}
		
		public string Caption {
			get {
				if( Items[0].Available < 0 ) {
					return CultureModule.getContent("BuyTable");
				}
				return CultureModule.getContent("SellTable");
			}
		}
		
		public bool ShowQuantity {
			get {
				return Items[0].Available >= 0;
			}
		}
		
		#endregion
	
		#region Control Events
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			if( Items == null ) {
				return;
			}
			
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", Caption);
			writer.WriteLine("<table class='planetFrame'>");
			
			writer.WriteLine("<tr class='resourceTitle'>");
			WriteTitle(writer);
			writer.WriteLine("</tr>");
			WriteItems(writer);
			writer.WriteLine("</table>");
		}
		
		#endregion
		
		#region Utilities
		
		private void WriteTitle( HtmlTextWriter writer )
		{
			writer.WriteLine("<td class='resourceTitle'>-</td>");
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("section_resources"));
			writer.WriteLine("<td class='resourceTitle'>{0} / 1</td>", CultureModule.getContent("Price"));
			if( ShowQuantity ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("quantidade"));	
			}
		}
		
		private void WriteItems( HtmlTextWriter writer )
		{
			foreach( MarketItem item in Items ) {
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resourceCell'><img src='{0}.gif' /></td>", OrionGlobals.getCommonImagePath("resources/"+item.Resource.Name));
				writer.WriteLine("<td class='resourceCell'>{0}</td>", CultureModule.getContent(item.Resource.Name));
				writer.WriteLine("<td class='resourceCell'>{0} <img src='{1}' title='{2}' /></td>", item.Price, OrionGlobals.getCommonImagePath("resources/gold.gif"), CultureModule.getContent("gold"));
				if( ShowQuantity ) {
					writer.WriteLine("<td class='resourceCell'>{0}</td>", item.Available);	
				}
				writer.WriteLine("</tr>");
			}
		}
	
		#endregion
	
	};
	
}
