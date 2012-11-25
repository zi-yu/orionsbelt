using System;
using System.Web.Security;

namespace Alnitak {
	
	/// <summary>
	/// Frequently asked Questions
	/// </summary>
	public class Logout : BasePageModule {
		public Logout() {}
		
		protected override void OnInit( EventArgs e )
		{
			base.OnInit(e);
			if( Page.User.IsInRole("guest") ) {
				return;
			}
			
			OrionGlobals.RequestManager.List.Clear();
			OrionGlobals.clearOnlineUserInformation();
			FormsAuthentication.SignOut();
			Page.Response.Redirect( OrionGlobals.resolveBase("index.aspx") );
		}
	}
}
