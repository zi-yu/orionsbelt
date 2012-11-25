using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {
	
	public class RequestHistory : Control {
		
		#region Control Fields
		
		private int quantity;
		
		#endregion
		
		#region Control Properties
		
		/// <summary>Indica o nmero de items a mostar</summary>
		public int Quantity {
			get { return quantity; }
			set { quantity = value; }
		}
		
		#endregion
		
		#region Control
		
		/// <summary>Ctor</summary>
		public RequestHistory()
		{
			Quantity = 10;
		}
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			int count = 0;
			for( int i = OrionGlobals.RequestManager.List.Count -1; i >= 0; --i, ++count ) {
				OrionRequest request = (OrionRequest) OrionGlobals.RequestManager.List[i];
			
				writer.Write("<div class='history'><img src='{0}' /> ", OrionGlobals.getCommonImagePath("messages/" + request.Topic + ".gif"));
				writer.Write("<a href='{0}'>", request.Url);
				writer.Write(request.Caption);
				writer.Write("</a>");
				writer.Write("</div>");
				
				if( Controls.Count > 0 ) {
					foreach( Control control in Controls ) {
						control.RenderControl(writer);
					}
				}
				if( count == Quantity ) {
					break;
				}
			}
		}
		
		#endregion
		
	};
	
}

