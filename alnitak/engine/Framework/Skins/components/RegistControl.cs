using System;
using System.Web.UI;
using Language;

namespace Alnitak {

	public class RegistControl : UserControl, INamingContainer {
	
		private ILanguageInfo info;
		protected System.Web.UI.WebControls.Button loginButton;
		protected System.Web.UI.WebControls.TextBox userMail;
		protected System.Web.UI.WebControls.TextBox nick;
		protected System.Web.UI.WebControls.TextBox password;
		protected System.Web.UI.WebControls.TextBox password2;
		protected System.Web.UI.WebControls.RequiredFieldValidator nickValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator nickTextValidator;
		protected System.Web.UI.WebControls.RequiredFieldValidator passValidator;
		protected System.Web.UI.WebControls.CompareValidator passValidator2;
		protected System.Web.UI.WebControls.RequiredFieldValidator userValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator mailValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator nickRangeValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator passRangeValidator;
		
		/// <summary>Prepara os controlos</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			info = CultureModule.getLanguage();
			
			loginButton.Text = info.getContent("send");
			passValidator.ErrorMessage = LoginControl.getError("provide-password");
			//passValidator2.ErrorMessage = LoginControl.getError("diferent-passwords");
			userValidator.ErrorMessage = LoginControl.getError("provide-mail");
			mailValidator.ErrorMessage = LoginControl.getError("bad-mail-format");
			nickValidator.ErrorMessage = LoginControl.getError("provide-nick");
			nickTextValidator.ErrorMessage = LoginControl.getError("validators_bad-formed-nick");
			nickRangeValidator.ErrorMessage = LoginControl.getError("validators_big-nick");
			passRangeValidator.ErrorMessage = LoginControl.getError("validators_big-pass");
		}
	
		/// <summary>Evento de click no botão</summary>
		protected void onLoginClick( object src, EventArgs args )
		{
			if( ! Page.IsValid ) {
				return;
			}
			
			if( UserUtility.bd.checkUser( userMail.Text ) ) {
				Information.AddError( info.getContent("register_mail-exists") );
				return;
			}
			
			UserUtility.bd.registerUser( nick.Text, userMail.Text, password.Text );
			foreach( Control control in Controls ) {
				control.Visible = false;
			}
			
			Information.AddInformation( string.Format(info.getContent("register_go-login"), userMail.Text) );
			Page.Cache.Remove("TotalUsers");
		}
	
	};

}
