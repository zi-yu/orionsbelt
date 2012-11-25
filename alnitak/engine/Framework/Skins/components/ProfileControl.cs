// created on 5/1/04 at 10:16 a

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Core;
using Language;
using Chronos.Utils;

namespace Alnitak {

	public class ProfileControl : UserControl, INamingContainer {
	
		private ILanguageInfo info;
		protected System.Web.UI.WebControls.Button updateButton;
		protected System.Web.UI.WebControls.Label userMail;
		
		protected System.Web.UI.WebControls.TextBox nick;
		protected System.Web.UI.WebControls.TextBox website;
		protected System.Web.UI.WebControls.TextBox password;

		protected System.Web.UI.WebControls.TextBox avatar;
		protected System.Web.UI.WebControls.Image avatarImg;
		
		protected System.Web.UI.WebControls.DropDownList lang;
		protected System.Web.UI.WebControls.DropDownList skin;
		protected System.Web.UI.WebControls.TextBox imagesDir;
		protected System.Web.UI.WebControls.Label imagesDirText;
		
		protected System.Web.UI.WebControls.CheckBox vacation;
		
		protected System.Web.UI.WebControls.TextBox msn;
		protected System.Web.UI.WebControls.TextBox icq;
		protected System.Web.UI.WebControls.TextBox jabber;
		protected System.Web.UI.WebControls.TextBox aim;
		protected System.Web.UI.WebControls.TextBox yahoo;
		
		protected System.Web.UI.WebControls.TextBox signature;
		
		protected Language.Label userUpdated;

		protected System.Web.UI.WebControls.RequiredFieldValidator nickValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator nickRangeValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator nickTextValidator;
		
		protected System.Web.UI.WebControls.RegularExpressionValidator avatarUrlValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator avatarRangeValidator;

		protected System.Web.UI.WebControls.RegularExpressionValidator urlRangeValidator;
		protected System.Web.UI.WebControls.RegularExpressionValidator urlTextValidator;
		protected System.Web.UI.WebControls.BaseValidator imagesDirValidator;

		//protected System.Web.UI.WebControls.CompareValidator passValidator2;
        		
		protected System.Web.UI.WebControls.BaseValidator msnValidator;
		protected System.Web.UI.WebControls.BaseValidator icqValidator;
		protected System.Web.UI.WebControls.BaseValidator aimValidator;
		protected System.Web.UI.WebControls.BaseValidator jabberValidator;
		protected System.Web.UI.WebControls.BaseValidator yahooValidator;

		protected System.Web.UI.WebControls.BaseValidator signatureValidator;
		protected System.Web.UI.WebControls.BaseValidator signatureRangeValidator;
		
		/// <summary>Prepara os controlos</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			info = CultureModule.getLanguage();
			userUpdated.Visible = false;
			userMail.EnableViewState = true;
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Generic, info.getContent("section_profile"));
			
			updateButton.Text = info.getContent("update");
			
			nickValidator.ErrorMessage = LoginControl.getError("profile_provide-nick");
			nickTextValidator.ErrorMessage = LoginControl.getError("validators_bad-formed-nick");
			nickRangeValidator.ErrorMessage = LoginControl.getError("validators_big-nick");

			//passValidator2.ErrorMessage = LoginControl.getError("diferent-passwords");
			
			avatarUrlValidator.ErrorMessage = LoginControl.getError("validators_bad-formed-avatarUrl");
			avatarRangeValidator.ErrorMessage = LoginControl.getError("validators_big-avatarUrl");

			urlTextValidator.ErrorMessage = LoginControl.getError("validators_bad-formed-url");
			urlRangeValidator.ErrorMessage = LoginControl.getError("validators_big-url");
			imagesDirValidator.ErrorMessage = LoginControl.getError("validators_invalidDir");
			
			msnValidator.ErrorMessage = LoginControl.getError("validators_invalid-msn");
			icqValidator.ErrorMessage = LoginControl.getError("validators_invalid-icq");
			jabberValidator.ErrorMessage = LoginControl.getError("validators_invalid-jabber");
			aimValidator.ErrorMessage = LoginControl.getError("validators_invalid-aim");
			yahooValidator.ErrorMessage = LoginControl.getError("validators_invalid-yahoo");
			
			signatureValidator.ErrorMessage = LoginControl.getError("validators_invalid-signature");
			signatureRangeValidator.ErrorMessage = LoginControl.getError("validators_big-signature");
			
			if( !IsPostBack ) {
				povoateLang();
				povoateSkin();
				setFields();
			}
			
		}
		
		/// <summary>Povoa a DropDownLost das linguas</summary>
		private void povoateLang()
		{
			User user = (User) HttpContext.Current.User;
			string[] langs = CultureModule.Languages;
			foreach( object l in langs ) {
				string language = l.ToString();
				if( language == "CVS" ) {
					continue;
				}
				ListItem item = new ListItem();
				item.Text = info.getContent("Culture",language);
				item.Value = language;
				if( language == user.Lang ) {
					item.Selected = true;
				}
				lang.Items.Add(item);
			}
		}
		
		/// <summary>Povoa a DropDownLost das linguas</summary>
		private void povoateSkin()
		{
			User user = (User) HttpContext.Current.User;
			UtilityCollection skins = MasterSkinUtility.getAllMasterSkins();
			foreach( MasterSkinInfo info in skins.Values ) {
				ListItem item = new ListItem();
				item.Text = info.masterSkinDescription;
				item.Value = info.masterSkinId.ToString();
				if( info.masterSkinId == user.Skin ) {
					item.Selected = true;
				}
				skin.Items.Add(item);
			}
		}
		
		/// <summary>Prepara os controlos</summary>
		protected void setFields()
		{
			User user = (User) Page.User;
			
			userMail.Text = user.Mail;
			nick.Text = user.Nick;
			website.Text = user.Website;
			
			vacation.Enabled = user.IsInRole("ruler");
			if( vacation.Enabled ) {
				Ruler ruler = Universe.instance.getRuler( user.RulerId );
				vacation.Checked = ruler.InVacation;
			}

			string avatarUrl = user.Avatar;
			avatar.Text = avatarUrl;
			if( avatarUrl == string.Empty )
				avatarImg.ImageUrl = User.DefaultAvatar;
			else
				avatarImg.ImageUrl = avatarUrl;

			imagesDirText.Text = string.Format( info.getContent("profile_imagesDirText"), OrionGlobals.getConfigurationValue("alnitak","imagesFile") );
			imagesDir.Text = user.ImagesDir;
			
			msn.Text = user.Msn;
			icq.Text = user.Icq;
			jabber.Text = user.Jabber;
			aim.Text = user.Aim;
			yahoo.Text = user.Yahoo;
			
			signature.Text = user.Signature;
		}
		
		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender(e);
		}
	
		/// <summary>Evento de click no butão</summary>
		protected void onUpdateClick( object src, EventArgs args )
		{
			Console.WriteLine("Update click");
			if( ! Page.IsValid ) {
				Trace.Write("ProfileControl","Fields not valid");
				Console.WriteLine("Profile fields not valid");
				return;
			}
			Console.WriteLine("Profile fields valid");
			Trace.Write("ProfileControl","Login fields valid");
			
			userUpdated.Visible = true;
			User user = (User) Page.User;

			user.Nick = nick.Text;
			user.Avatar = avatar.Text;
			user.Website = website.Text;
			user.Lang = lang.SelectedValue;
			user.Skin = int.Parse(skin.SelectedValue);
			user.ImagesDir = imagesDir.Text;
			
			user.Msn = msn.Text;
			user.Icq = icq.Text;
			user.Jabber = jabber.Text;
			user.Aim = aim.Text;
			user.Yahoo = yahoo.Text;
			
			user.Signature = signature.Text;
			
			if( vacation.Enabled ) {
				Ruler ruler = Universe.instance.getRuler(user.RulerId);
				if( vacation.Checked ) {
					ruler.StartVacations();
				} else {
					int forcedTime = 300;
#if DEBUG
					forcedTime = 0;
#endif
					if( ruler.InVacation && ruler.VacationTurns < forcedTime && !user.IsInRole("admin") && !user.IsInRole("betaTester") ) {
						Information.AddInformation( string.Format(info.getContent("profile_vacation_error"), ruler.VacationTurns, forcedTime) );
						vacation.Checked = true;
						return;
					} else {
						ruler.EndVacations();
					}
				}
			}
			
			if( user.RulerId != -1 ) {
				Ruler ruler = Universe.instance.getRuler(user.RulerId);
				ruler.Name = user.Nick;
			}

			System.Web.HttpContext.Current.Cache.Remove( OrionGlobals.ForumSkinName );
			
			UserUtility.bd.saveUser(user,password.Text);
			Response.Redirect( OrionGlobals.calculatePath("default.aspx") );
		}
	
	};

}
