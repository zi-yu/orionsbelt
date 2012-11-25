using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;
using Alnitak;

namespace Alnitak {
	
	public class TotalUsers : Control {
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			object obj = Page.Cache["TotalUsers"];
			int total = 0;
			
			if(obj == null) {
				total = UserUtility.bd.getUserCount();
				Page.Cache["TotalUsers"] = total;
			} else {
				total = (int) obj;
			}
			writer.Write(total);
		}
		
	};
	
}

