using System;
using System.Web.Security;
using System.Web.UI;
using Chronos.Utils;
using Language;

namespace Alnitak {

	public class LoginControl : UserControl, INamingContainer {
	
		private ILanguageInfo info;
		protected System.Web.UI.WebControls.Button loginButton;
		protected System.Web.UI.WebControls.TextBox userMail;
		protected System.Web.UI.WebControls.TextBox password;
		protected System.Web.UI.WebControls.CheckBox autoLogin;
		protected System.Web.UI.WebControls.RequiredFieldValidator passValidator;
		protected System.Web.UI.WebControls.RequiredFieldValidator userValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator mailValidator;
		
		/// <summary>Prepara os controlos</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			info = CultureModule.getLanguage();
		
			loginButton.Text = info.getContent("login");

			passValidator.ErrorMessage = getError("provide-password");
			userValidator.ErrorMessage = getError("provide-mail");
			mailValidator.ErrorMessage = getError("bad-mail-format");
		}
		
		/// <summary>Retorna uma string HTML com um erro</summary>
		public static string getError( string reference )
		{
			ILanguageInfo info = CultureModule.getLanguage();
			return string.Format("{0} {1}{2}","*", info.getContent(reference), "<br/>");
		}
		
		/// <summary>Retorna uma string HTML com um erro</summary>
		public static string getError( string target, string reference )
		{
			ILanguageInfo info = CultureModule.getLanguage();
			return string.Format("{0} {1}{2}","*", info.getContent(target,reference), "<br/>");
		}

		/// <summary>Retorna a hash de uma pass para guardar na BD</summary>
		public string hashPassword( string text  )
		{
			return FormsAuthentication.HashPasswordForStoringInConfigFile(text, "sha1");
		}
	
		/// <summary>Evento de click no butão</summary>
		protected void onLoginClick( object src, EventArgs args )
		{
			Log.log("-------------------------------");
			if( ! Page.IsValid ) {
				Trace.Write("LoginControl","Login fields not valid");
				return;
			}
			Trace.Write("LoginControl","Login fields valid");
		
			bool user = UserUtility.bd.checkUser( userMail.Text, password.Text ); 
			Log.log("-------------------------------");
			Log.log("Authentication for'{0}': {1}", userMail.Text, user);
			if( !user ) {
				Information.AddError( info.getContent("login-error") ); 
				return;
			} else {
				Trace.Write("go redirect");
				FormsAuthentication.RedirectFromLoginPage( userMail.Text, autoLogin.Checked );
			}
		}
	
	};

}
