// created on 30-12-2004 at 12:02

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {

	public class LatestRuler : Control {
	
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			User user = (User) Page.Application["LatestRuler"];
			
			if ( null == user ) {
				writer.WriteLine("?");
				return;
			}			

			writer.WriteLine( OrionGlobals.getLink(user) );
		}
	
	};
	
}