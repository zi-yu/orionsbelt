// created on 5/1/04 at 10:16 a

using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Language;
using Chronos.Core;

namespace Alnitak {

	public class ResetPassword : UserControl, INamingContainer {
		private ILanguageInfo info;

		protected Panel doReset;
		protected Panel resetMade;
		
		protected System.Web.UI.WebControls.Label userMail;
		protected TextBox mail;
		
		protected RegularExpressionValidator mailValidator;

		protected Button resetButton;

		private bool resetOk = false;

		#region Overrided
		
		/// <summary>Prepara os controlos</summary>
		protected override void OnLoad( EventArgs args ) {
			base.OnLoad(args);
			
			info = CultureModule.getLanguage();
									
			mailValidator.ErrorMessage = LoginControl.getError("validators_invalidMailFormat");
			resetButton.Text = info.getContent("reset_button");

			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Generic, info.getContent("section_resetpassword"));
			
		}

		protected override void OnPreRender(EventArgs e) {
			base.OnPreRender (e);

			if( !resetOk ) {
				doReset.Visible = true;
				resetMade.Visible = false;
			} else {
				doReset.Visible = false;
				resetMade.Visible = true;
			}
		}


		#endregion

		#region Events

		/// <summary>Evento de click no botão</summary>
		protected void onResetPassword( object src, EventArgs args ){
			Chronos.Utils.Log.log("Reset Password Click");

			string newPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(User.GeneratePassword(), "sha1");
			
			if( UserUtility.bd.resetPassword( mail.Text, newPassword ) ) {
				resetOk = true;
			} else {
				Information.AddError( info.getContent("validators_invalidEmail") ); 
			}
		}

		#endregion
	
	};

}
