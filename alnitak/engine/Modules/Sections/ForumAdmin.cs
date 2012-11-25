using System.Web;
using System.Web.UI;

namespace Alnitak {
	
	public class ForumAdmin : Control  {
		protected override void OnInit(System.EventArgs e) {
			HttpContext.Current.Response.Redirect( OrionGlobals.getSectionUrl( "forum" ) + "default.aspx?g=admin_admin" );
		}
	}
}
	