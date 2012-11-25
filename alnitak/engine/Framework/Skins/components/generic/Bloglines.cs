// created on 5/17/04 at 9:52 a

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {
	
	public class Bloglines : Control {	
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			writer.WriteLine("<a href='http://www.bloglines.com/sub/{0}/rss.aspx'>", OrionGlobals.AlnitakUrl);
			writer.WriteLine("<img src='{0}' class='logo' alt='Subscribe with Bloglines' />", OrionGlobals.getCommonImagePath("logos/bloglines.gif"));
			writer.WriteLine("</a>");
		}
		
	};
	
}

