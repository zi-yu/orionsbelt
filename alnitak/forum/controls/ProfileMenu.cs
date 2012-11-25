using System;
using System.Data;
using Alnitak;

namespace yaf.controls
{
	/// <summary>
	/// Summary description for ForumUsers.
	/// </summary>
	public class ProfileMenu : BaseControl
	{
		protected override void Render(System.Web.UI.HtmlTextWriter writer) 
		{
			System.Text.StringBuilder html = new System.Text.StringBuilder(2000);

			html.Append("<table width='100%' cellspacing=0 cellpadding=0>");
			if (ForumPage.BoardSettings.AllowPrivateMessages)
			{
				html.AppendFormat("<tr class='header2'><td>{0}</td></tr>",ForumPage.GetText("MESSENGER"));
				html.AppendFormat("<tr><td>");
				html.AppendFormat("<li><a href='{0}'>{1}</a></li>",Forum.GetLink(Pages.cp_inbox),ForumPage.GetText("INBOX"));
				html.AppendFormat("<li><a href='{0}'>{1}</a></li>",Forum.GetLink(Pages.cp_inbox,"sent=1"),ForumPage.GetText("SENTITEMS"));
				html.AppendFormat("<li><a href='{0}'>{1}</a></li>",Forum.GetLink(Pages.pmessage),ForumPage.GetText("NEW_MESSAGE"));
				html.AppendFormat("</td></tr>");
				html.AppendFormat("<tr><td>&nbsp;</td></tr>");
			}
			html.AppendFormat("<tr class='header2'><td>{0}</td></tr>",ForumPage.GetText("PERSONAL_PROFILE"));
			html.AppendFormat("<tr><td>");
			html.AppendFormat("<li><a href='{0}'>{1}</a></li>",OrionGlobals.resolveBase( "profile.aspx" ),ForumPage.GetText("EDIT_PROFILE"));
			if (ForumPage.BoardSettings.AllowSignatures) html.AppendFormat("<li><a href='{0}'>{1}</a></li>",Forum.GetLink(Pages.cp_signature),ForumPage.GetText("SIGNATURE"));
			html.AppendFormat("<li><a href='{0}'>{1}</a></li>",Forum.GetLink(Pages.cp_subscriptions),ForumPage.GetText("SUBSCRIPTIONS"));
			html.AppendFormat("</td></tr>");
			html.Append("</table>");

			writer.Write(html.ToString());
		}
	}
}
