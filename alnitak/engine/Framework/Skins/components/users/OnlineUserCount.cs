// created on 30-12-2004 at 11:35

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {

	public class OnlineUserCount : Control {
	
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			object obj = Page.Application["AlnitakOnlineUsersCount"];
			if( obj != null ) {
				writer.Write(obj.ToString());
			} else {
				writer.Write("?");
			}
		}
	
	};
	
}
