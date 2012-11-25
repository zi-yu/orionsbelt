using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

namespace Alnitak {

	/// <summary>
	/// Página para onde são reencaminhados todos os erros
	/// </summary>
	public class OrionsBeltError : UserControl {
		protected Label exceptionName;
		protected Label exceptionTrace;
		protected Label exceptionMessage;
		protected PlaceHolder adminContent;

		private bool renderMade = false;

		#region events

		protected override void OnError(EventArgs e) {
			base.OnError (e);
			Exception exception = Server.GetLastError();
			ExceptionLog.log( exception );
			HttpContext.Current.Response.Redirect( OrionGlobals.resolveBase( OrionGlobals.getConfigurationValue("pagePath","globalError") ) );
		}

		protected override void OnLoad(EventArgs e) {
			try {
				ExceptionInfo exceptionInfo = (ExceptionInfo)HttpContext.Current.Cache[ OrionGlobals.SessionId + "AlnitakException"];
				IPrincipal user = HttpContext.Current.User;
				if( user != null ) {
					if( user.IsInRole("admin") ) {
						adminContent.Visible = true;
						if( exceptionInfo != null ) { 
							setException( exceptionInfo );
						}
					} else {
						adminContent.Visible = false;
					}
				}
			} catch( Exception exp ) {
				setException( ExceptionLog.log( exp ) );
			}
			base.OnLoad (e);
		}

		protected override void Render(HtmlTextWriter writer) {
			base.Render (writer);
			renderMade = true;
		}


		protected override void OnUnload(EventArgs e) {
			base.OnUnload (e);
			if( renderMade ) {
				HttpContext.Current.Cache.Remove( OrionGlobals.SessionId + "ExceptionNumber");
				HttpContext.Current.Cache.Remove( OrionGlobals.SessionId + "AlnitakException");
			}
		}

		#endregion

		#region private

		private void setException( ExceptionInfo exceptionInfo ) {
			exceptionName.Text = exceptionInfo.Name;
			exceptionMessage.Text = exceptionInfo.Message;
			exceptionTrace.Text = exceptionInfo.StackTrace;
		}

		#endregion	
	}
}