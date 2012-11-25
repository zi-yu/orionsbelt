using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Alnitak{
	
	public class AdminResetPassword : UserControl {
		protected Button reset;
		protected TextBox mail;
		protected PlaceHolder reset_done;

		protected override void OnLoad(EventArgs e) {
			reset_done.Visible=true;
			reset.Text = CultureModule.getLanguage(  ).getContent( "reset_button" );
			reset.Click += new EventHandler(reset_Click);
			base.OnLoad (e);
		}

		private void reset_Click(object sender, EventArgs e) {
			UserUtility.bd.resetPassword( mail.Text, UserUtility.bd.hashPassword( "password" ) );
			Information.AddInformation( "Done" );
			reset_done.Visible=false;

		}
	}
}
