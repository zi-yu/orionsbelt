using System.Web;
using System;

namespace Alnitak {

	/// <summary>
	/// classe que representa a página principal
	/// </summary>
	public class Index : BasePageModule {
		
		protected override void OnInit(EventArgs e) {
			if( !Page.Request.RawUrl.EndsWith("index.aspx") ) {
				User user = HttpContext.Current.User as User;
				if( null != user && user.IsInRole( "ruler" ) ) {
					HttpContext.Current.Response.Redirect( OrionGlobals.getSectionBaseUrl("Ruler") );
				}
			}
			base.OnInit (e);
		}
	}

}
