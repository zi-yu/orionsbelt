// created on 19-01-2005 at 17:55

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alnitak.Mail;

namespace Alnitak {
	
	/// <summary>
	/// ContactPage page
	/// </summary>
	public class ContactPage : UserControl {
		
		#region Instance Fields
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		
		protected Label from;
		protected PlaceHolder to;
		protected PlaceHolder messageSent;
		protected TextBox message;
		protected TextBox fromBlank;
		protected Button send;
		
		#endregion
		
		#region Control Events
		
		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Generic, info.getContent("section_contact"));
			send.Text = info.getContent("send");
			
			message.Attributes.Add("width", "100%");
			message.Attributes.Add("height", "250");
			fromBlank.Attributes.Add("width", "100%");
			
			SetTo();
			SetFrom();
		}
		
		/// <summary>Envia uma mensagem</summary>
		public void SendMessage( object src, EventArgs args )
		{
			string fromMail = null;
			
			if( fromBlank.Visible ) {
				fromMail = fromBlank.Text;
			} else {
				User user = (User) Context.User;
				fromMail = Mailer.GetFormattedMail(user);
			}
			bool status = false;
			if( ViewState["Mail-To"] == null ) {
				status = Mailer.SendToAdmin("(Orionsbelt) Contact Page", ParseMessage(message.Text, fromMail));
			} else {
				User to = (User) ViewState["Mail-To"];
				status = Mailer.Send(fromMail, Mailer.GetFormattedMail(to), "(Orionsbelt) You've Got Mail!", ParseMessage(message.Text, fromMail));
			}
			
			messageSent.Visible = status;
			if( !status ) {
				Information.AddError( "Mail Server Down" );
			}
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Indica o receptor</summary>
		private void SetTo()
		{
			string toId = Request.QueryString["id"];
			
			if( toId == null || !OrionGlobals.isInt(toId) ) {
				to.Controls.Add( new LiteralControl("Webmaster") );
			} else {
				User user = GetUser(int.Parse(toId));
				to.Controls.Add( new LiteralControl(OrionGlobals.getLink(user)) );
				ViewState["Mail-To"] = user;
			}
		}
		
		/// <summary>Indica o emissor</summary>
		private void SetFrom()
		{
			User user = Context.User as User;
			if( user == null ) {
				fromBlank.Visible = true;
				from.Visible = false;
			} else {
				fromBlank.Visible = false;
				from.Visible = true;
			}
			
			if( from.Visible ) {
				from.Text = OrionGlobals.getLink(user);
			}
		}
		
		/// <summary>Obtm o user</summary>
		private User GetUser( int id )
		{
			object obj = Cache["Ruler-" + id];
			if( obj == null ) {
				return UserUtility.bd.getUser(id);
			}
			
			return (User) obj;
		}
		
		/// <summary>Prepara o contedo de uma mensagem</summary>
		public string ParseMessage(string message, string from)
		{
			return string.Format("{0}\nhas sent you the following message\n---------------------------------------------------------\n\n{1}\n\n---------------------------------------------------------\nOrionsbelt Game :: {2}", from, message, OrionGlobals.AlnitakUrl);
		}
		
		#endregion
		
	};
}
