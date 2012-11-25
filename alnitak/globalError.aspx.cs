using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Utils;

namespace Alnitak {
	/// <summary>
	/// Página para onde são reencaminhados todos os erros
	/// </summary>
	public class GlobalError : Page {
		private bool renderMade = false;

		protected override void OnLoad(EventArgs e) {
			string key = OrionGlobals.SessionId + "AlnitakException";

			Log.log("Retriving exception from Cache[\""+key+"\"]...");	
			ExceptionInfo exceptionInfo = (ExceptionInfo)HttpContext.Current.Cache[key];
			Log.log("... Got: " + (exceptionInfo == null ? "null" : exceptionInfo.Message) );
			
			base.OnLoad (e);
		}

		protected override void Render(HtmlTextWriter writer) {
			base.Render (writer);
			renderMade = true;
		}


		protected override void OnUnload(EventArgs e) {
			base.OnUnload (e);
			if( renderMade ) {
				HttpContext.Current.Cache.Remove("ExceptionNumber");
				HttpContext.Current.Cache.Remove("AlnitakException");
			}
		}


		
	}
}
