using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;

namespace Alnitak {
	
	public class LoginBox : BaseControl {

		private ILanguageInfo info;
		protected ImageButton loginButton;
		protected ImageButton registerButton;
		protected ImageButton logoutButton;
		protected ImageButton profileButton;
		
		protected TextBox userMail;
		protected TextBox password;
		protected CheckBox autoLogin;
		protected Panel login;
		protected Panel logout;
		protected Panel becomeRulerPanel;

		/// <summary>Retorna a hash de uma pass para guardar na BD</summary>
		public string hashPassword( string text  ) {
			return FormsAuthentication.HashPasswordForStoringInConfigFile(text, "sha1");
		}
	
		/// <summary>Evento de click no buto</summary>
		protected void onLoginClick( object src, ImageClickEventArgs args ) {
			//verificar se as string n esto vazias
			if(userMail.Text==string.Empty) {
				Information.AddError( info.getContent("provide-mail") );
				return;
			}

			if( !Regex.IsMatch(userMail.Text,@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ) ) {
				Information.AddError( info.getContent("bad-mail-format") );
				return;
			}

			if( password.Text==string.Empty ) {
				Information.AddError(info.getContent("provide-password") );
				return;
			}

			if( (userMail.Text.IndexOf(" ")) == 0 || (password.Text.IndexOf(" ")) == 0 ){
				Information.AddError(info.getContent("login-error") );
				return;
			}
			
			bool user = UserUtility.bd.checkUser( userMail.Text, password.Text );

			if( !user ) {
				Information.AddError(info.getContent("login-error") );
				return;
			}
			login.Visible = false;
			logout.Visible = true;
			FormsAuthentication.SetAuthCookie( userMail.Text, autoLogin.Checked );
			string redirectUrl = FormsAuthentication.GetRedirectUrl( userMail.Text, autoLogin.Checked ).ToLower(); 
			
			HttpContext context = HttpContext.Current;
			if( context.Request.QueryString["ReturnUrl"] != null ) {
				context.Response.Redirect( redirectUrl );
			} else {
				context.Response.Redirect( OrionGlobals.resolveBase( "default.aspx" ) );
			}
		}

		/// <summary>
		/// Evento de click no botao de registo
		/// </summary>
		protected void onRegisterClick( object src, ImageClickEventArgs args ) {
			HttpContext.Current.Response.Redirect(OrionGlobals.calculatePath("regist.aspx") );
		}

		/// <summary>
		/// Evento de click no botao de logout
		/// </summary>
		protected void onLogoutClick( object src, ImageClickEventArgs args ) {
			OrionGlobals.clearOnlineUserInformation();

			logout.Visible = false;
			login.Visible = true;
			FormsAuthentication.SignOut();
			HttpContext.Current.Response.Redirect( OrionGlobals.resolveBase("default.aspx") );
		}

		protected void onProfileClick(object src, ImageClickEventArgs args ) {
			HttpContext.Current.Response.Redirect(OrionGlobals.calculatePath("profile.aspx") );
		}

		protected void onBecomeRuler(object src, ImageClickEventArgs args ) {
			HttpContext.Current.Response.Redirect(OrionGlobals.calculatePath("addRuler.aspx") );
		}

		protected override void initializeSkin(Control skin) {
			info = CultureModule.getLanguage();

			string c = OrionGlobals.getCulture();

			login = (Panel)getControl(skin,"login");
			logout = (Panel)getControl(skin,"logout");
			becomeRulerPanel = (Panel)getControl(skin,"becomeRulerPanel");

			loginButton = (ImageButton)getControl(skin,"loginButton");
			loginButton.ImageUrl = OrionGlobals.getSkinImagePath("buttons/login_"+ c +".gif" );

			registerButton  = (ImageButton)getControl(skin,"registerButton");
			registerButton.ImageUrl = OrionGlobals.getSkinImagePath("buttons/register_"+ c +".gif" );

			logoutButton  = (ImageButton)getControl(skin,"logoutButton");
			logoutButton.ImageUrl = OrionGlobals.getSkinImagePath("buttons/logout_"+ c +".gif" );

			profileButton = (ImageButton)getControl(skin,"profileButton");
			profileButton.ImageUrl = OrionGlobals.getSkinImagePath("buttons/profile_"+ c +".gif" );

			userMail = (TextBox)getControl(skin,"userMail");
			
			if( ! Page.IsPostBack ) {
				userMail.Text = string.Empty;
			}

			password = (TextBox)getControl(skin,"password");
			autoLogin = (CheckBox)getControl(skin,"autoLogin");
			
			loginButton.ToolTip = info.getContent("login_login");
			registerButton.ToolTip = info.getContent("login_register");
			logoutButton.ToolTip = info.getContent("login_logout");
			profileButton.ToolTip = info.getContent("login_profile");

			//eventos
			loginButton.Click += new ImageClickEventHandler( onLoginClick );
				
			registerButton.Click += new ImageClickEventHandler(onRegisterClick);
			logoutButton.Click += new ImageClickEventHandler(onLogoutClick);
			profileButton.Click += new ImageClickEventHandler(onProfileClick);
		
			if( HttpContext.Current.User.IsInRole("guest") ) {
				logout.Visible = false;
				login.Visible = true;
			}else{
				logout.Visible = true;
				login.Visible = false;
				if( !HttpContext.Current.User.IsInRole("ruler") ) {
					becomeRulerPanel.Controls.Clear();
					if( becomeRulerPanel.Controls.Count == 0 ) { 
						ImageButton becomeRuler;
						object o = HttpContext.Current.Cache["ImageButton_BecomeRulers"];
						if( o == null ) {
							becomeRuler = new ImageButton();
							
							becomeRuler.ToolTip = info.getContent("login_becomeRuler");
							becomeRuler.Click += new ImageClickEventHandler(onBecomeRuler);
							becomeRuler.ImageUrl = OrionGlobals.getSkinImagePath("buttons/becomeRuler_"+ c +".gif" );
							becomeRuler.CausesValidation = false;
							becomeRuler.CssClass = "imageButton";
							becomeRulerPanel.Visible = true;

							HttpContext.Current.Cache["ImageButton_BecomeRulers"] = becomeRuler;
						}else{
							becomeRuler = (ImageButton)o;
						}

						becomeRulerPanel.Controls.Add( becomeRuler );
					}
				}else {
					if( becomeRulerPanel.Controls.Count > 0 )
						becomeRulerPanel.Controls.Clear();
				}
			}
		}

		public LoginBox() {
			_skinFileName = "LoginBox.ascx";
			_skinName = "loginbox";
			
			EnableViewState = false;
		}
	}
}
