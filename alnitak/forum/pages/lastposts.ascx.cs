using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace yaf.pages
{
	/// <summary>
	///		Summary description for LastPosts.
	/// </summary>
	public class lastposts : ForumPage
	{
		protected System.Web.UI.WebControls.Repeater repLastPosts;

		public lastposts() : base("POSTMESSAGE")
		{
			ShowToolBar = false;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!ForumReadAccess)
				Data.AccessDenied();

			if (Request.QueryString["t"] != null)
			{
				repLastPosts.DataSource = DB.post_list_reverse10(Request.QueryString["t"]);
				repLastPosts.DataBind();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		protected string FormatBody(object o) 
		{
			DataRowView row = (DataRowView)o;
			string html = FormatMsg.FormatMessage(this,row["Message"].ToString(),new MessageFlags(Convert.ToInt32(row["Flags"])));

			string sig = row["user_signature"].ToString();
			if(sig!=string.Empty) 
			{
				sig = FormatMsg.FormatMessage(this,sig,new MessageFlags());
				html += "<br/><hr noshade/>" + sig;
			}

			return html;
		}
	}
}
