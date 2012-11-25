using System;
using System.Data;

namespace yaf.controls
{
	/// <summary>
	/// Summary description for PageLinks.
	/// </summary>
	public class PageAccess : BaseControl
	{
		private void Page_Load(object sender, System.EventArgs e) 
		{
		}

		override protected void OnInit(EventArgs e)
		{
			this.Load += new System.EventHandler(this.Page_Load);
			base.OnInit(e);
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer) 
		{
			writer.WriteLine(ForumPage.GetText(ForumPage.ForumPostAccess ? "can_post" : "cannot_post"));
			writer.WriteLine("<br/>");
			writer.WriteLine(ForumPage.GetText(ForumPage.ForumReplyAccess ? "can_reply" : "cannot_reply"));
			writer.WriteLine("<br/>");
			writer.WriteLine(ForumPage.GetText(ForumPage.ForumDeleteAccess ? "can_delete" : "cannot_delete"));
			writer.WriteLine("<br/>");
			writer.WriteLine(ForumPage.GetText(ForumPage.ForumEditAccess ? "can_edit" : "cannot_edit"));
			writer.WriteLine("<br/>");
			writer.WriteLine(ForumPage.GetText(ForumPage.ForumPollAccess ? "can_poll" : "cannot_poll"));
			writer.WriteLine("<br/>");
			writer.WriteLine(ForumPage.GetText(ForumPage.ForumVoteAccess ? "can_vote" : "cannot_vote"));
			writer.WriteLine("<br/>");
		}
	}
}
