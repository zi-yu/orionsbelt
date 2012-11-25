/* Yet Another Forum.net
 * Copyright (C) 2003 Bj�rnar Henden
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace yaf.pages
{
	/// <summary>
	/// Summary description for postmessage.
	/// </summary>
	public class postmessage : ForumPage
	{
		protected yaf.editor.ForumEditor Message;
		protected System.Web.UI.WebControls.TextBox Subject;
		protected System.Web.UI.WebControls.Button PostReply;
		protected System.Web.UI.WebControls.Label Title;
		protected System.Web.UI.HtmlControls.HtmlTableRow SubjectRow;
		protected System.Web.UI.WebControls.Button Preview;
		protected System.Web.UI.HtmlControls.HtmlTableRow PriorityRow;
		protected System.Web.UI.WebControls.DropDownList Priority;
		protected System.Web.UI.HtmlControls.HtmlTableRow CreatePollRow;
		protected System.Web.UI.WebControls.LinkButton CreatePoll;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow1;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow2;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow3;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow4;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow5;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow6;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow7;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow8;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow9;
		protected System.Web.UI.HtmlControls.HtmlTableRow PollRow10;
		protected System.Web.UI.WebControls.TextBox PollChoice1;
		protected System.Web.UI.WebControls.TextBox PollChoice2;
		protected System.Web.UI.WebControls.TextBox PollChoice3;
		protected System.Web.UI.WebControls.TextBox PollChoice4;
		protected System.Web.UI.WebControls.TextBox PollChoice5;
		protected System.Web.UI.WebControls.TextBox PollChoice6;
		protected System.Web.UI.WebControls.TextBox PollChoice7;
		protected System.Web.UI.WebControls.TextBox PollChoice8;
		protected System.Web.UI.WebControls.TextBox PollChoice9;
		protected System.Web.UI.WebControls.Button Cancel;
		protected System.Web.UI.WebControls.TextBox Question;
		protected System.Web.UI.WebControls.TextBox From;
		protected System.Web.UI.HtmlControls.HtmlTableRow FromRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow PreviewRow;
		protected System.Web.UI.HtmlControls.HtmlTableCell PreviewCell;
		protected System.Web.UI.WebControls.Repeater LastPosts;
		protected System.Web.UI.WebControls.Label NoEditSubject;
		protected System.Web.UI.HtmlControls.HtmlTableCell EditorLine;
		protected System.Web.UI.HtmlControls.HtmlGenericControl LastPostsIFrame;
		protected controls.PageLinks PageLinks;

		public postmessage() : base("POSTMESSAGE")
		{

		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			DataRow msg = null;
			
			if(Request.QueryString["q"] != null)
				using(DataTable dt = DB.message_list(Request.QueryString["q"]))
					msg = dt.Rows[0];
			else if(Request.QueryString["m"] != null) {
				using(DataTable dt = DB.message_list(Request.QueryString["m"]))
					msg = dt.Rows[0];
			
				if(!ForumModeratorAccess && PageUserID != (int)msg["UserID"])
					Data.AccessDenied();
			}
	
			if(PageForumID == 0)
				Data.AccessDenied();
			if(Request["t"]==null && !ForumPostAccess)
				Data.AccessDenied();
			if(Request["t"]!=null && !ForumReplyAccess)
				Data.AccessDenied();

			//Message.EnableRTE = BoardSettings.AllowRichEdit;
			Message.BaseDir = Data.ForumRoot + "editors";

			Title.Text = GetText("NEWTOPIC");
						
			if(!IsPostBack) 
			{
				Priority.Items.Add(new ListItem(GetText("normal"),"0"));
				Priority.Items.Add(new ListItem(GetText("sticky"),"1"));
				Priority.Items.Add(new ListItem(GetText("announcement"),"2"));
				Priority.SelectedIndex = 0;

				Preview.Text = GetText("preview");
				PostReply.Text = GetText("Save");
				Cancel.Text = GetText("Cancel");
				CreatePoll.Text = GetText("createpoll");

				PriorityRow.Visible = ForumPriorityAccess;
				CreatePollRow.Visible = Request.QueryString["t"]==null && ForumPollAccess;

				PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
				PageLinks.AddLink(PageCategoryName,Forum.GetLink(Pages.forum,"c={0}",PageCategoryID));
				PageLinks.AddForumLinks(PageForumID);

				if(Request.QueryString["t"] != null) 
				{
					// new post...
					DataRow topic = DB.topic_info(Request.QueryString["t"]);
					if(((int)topic["Flags"] & (int)TopicFlags.Locked)==(int)TopicFlags.Locked)
						Response.Redirect(Request.UrlReferrer.ToString());
					SubjectRow.Visible = false;
					Title.Text = GetText("reply");

					if (Config.IsDotNetNuke || Config.IsRainbow)
					{
						// can't use the last post iframe
						LastPosts.Visible = true;
						LastPosts.DataSource = DB.post_list_reverse10(Request.QueryString["t"]);
						LastPosts.DataBind();
					}
					else
					{
						LastPostsIFrame.Visible = true;
						LastPostsIFrame.Attributes.Add("src","framehelper.aspx?g=lastposts&t=" + Request.QueryString["t"]);
					}
				}

				if(Request.QueryString["q"] != null)
				{
					// reply to post...
					bool isHtml = msg["Message"].ToString().IndexOf('<')>=0;

					string tmpMessage = msg["Message"].ToString();

					if (BoardSettings.RemoveNestedQuotes)
					{
						RegexOptions m_options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline;
						Regex	quote = new Regex(@"\[quote(\=.*)?\](.*?)\[/quote\]",m_options);
						// remove quotes from old messages
						tmpMessage = quote.Replace(tmpMessage,"");
					}

					if(isHtml)
						Message.Text = String.Format("[quote={0}]{1}[/quote]",msg["username"],FormatMsg.HtmlToForumCode(tmpMessage));
					else
						Message.Text = String.Format("[quote={0}]{1}[/quote]",msg["username"],tmpMessage);
				}
				else if(Request.QueryString["m"] != null)
				{
					// edit message...
					string body = msg["message"].ToString();
					bool isHtml = body.IndexOf('<')>=0;
					if(isHtml) 
					{
						//throw new Exception("TODO: Convert this html message to forumcodes");
						body = FormatMsg.HtmlToForumCode(body);
					}
					Message.Text = body;
					Title.Text = GetText("EDIT");

					Subject.Text = Server.HtmlDecode(Convert.ToString(msg["Topic"]));

					if ((Convert.ToInt32(msg["TopicOwnerID"]) == Convert.ToInt32(msg["UserID"])) || ForumModeratorAccess)
					{
						// allow editing of the topic subject
						Subject.Enabled = true;
					}
					else
					{
						// disable the subject
						Subject.Enabled = false;
					}					
					
					CreatePollRow.Visible = false;
					Priority.SelectedItem.Selected = false;
					Priority.Items.FindByValue(msg["Priority"].ToString()).Selected = true;
				}

				From.Text = PageUserName;
				if(User.IsAuthenticated)
					FromRow.Visible = false;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			// get the forum editor based on the settings
			Message = yaf.editor.EditorHelper.CreateEditorFromType(BoardSettings.ForumEditor);
			EditorLine.Controls.Add(Message);

			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.CreatePoll.Click += new System.EventHandler(this.CreatePoll_Click);
			this.Preview.Click += new System.EventHandler(this.Preview_Click);
			this.PostReply.Click += new System.EventHandler(this.PostReply_Click);
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void PostReply_Click(object sender, System.EventArgs e)
		{
			if(SubjectRow.Visible && Subject.Text.Length<=0) {
				AddLoadMessage(GetText("need_subject"));
				return;
			}

			if(PollRow1.Visible) 
			{
				if(Question.Text.Trim().Length==0) 
				{
					AddLoadMessage(GetText("NEED_QUESTION"));
					return;
				}

				string p1 = PollChoice1.Text.Trim();
				string p2 = PollChoice2.Text.Trim();
				if(p1.Length==0 || p2.Length==0) 
				{
					AddLoadMessage(GetText("NEED_CHOICES"));
					return;
				}
			}


			// see if there is a post delay
			if (!(IsAdmin || IsModerator) && BoardSettings.PostFloodDelay > 0)
			{
				// see if they've past that delay point
				if (Mession.LastPost > DateTime.Now.AddSeconds(-BoardSettings.PostFloodDelay) && Request.QueryString["m"] == null) 
				{
					AddLoadMessage(String.Format(GetText("wait"),(Mession.LastPost - DateTime.Now.AddSeconds(-BoardSettings.PostFloodDelay)).Seconds));
					return;
				}
			}

			long TopicID;
			long nMessageID = 0;
			object replyTo = null;

			if(Request.QueryString["q"]!=null)
				replyTo = int.Parse(Request.QueryString["q"]);
			else
				// Let save procedure find first post
				replyTo = -1;

			string msg = Message.Text;

			Mession.LastPost = DateTime.Now;
			
			if(Request.QueryString["t"] != null)
			{
				if(!ForumReplyAccess)
					Data.AccessDenied();

				TopicID = long.Parse(Request.QueryString["t"]);
				// make message flags
				MessageFlags tFlags = new MessageFlags();

				tFlags.IsHTML = Message.UsesHTML;
				tFlags.IsBBCode = Message.UsesBBCode;

				if(!DB.message_save(TopicID,PageUserID,msg,User.IsAuthenticated ? null : From.Text,Request.UserHostAddress,null,replyTo,tFlags.BitValue,ref nMessageID))
					TopicID = 0;
			} 
			else if(Request.QueryString["m"] != null)
			{
				if(!ForumEditAccess)
					Data.AccessDenied();

				string SubjectSave = "";
				if (Subject.Enabled) SubjectSave = Server.HtmlEncode(Subject.Text);

				// make message flags
				MessageFlags tFlags = new MessageFlags();

				tFlags.IsHTML = Message.UsesHTML;
				tFlags.IsBBCode = Message.UsesBBCode;

				DB.message_update(Request.QueryString["m"],Priority.SelectedValue,msg,SubjectSave,tFlags.BitValue);
				TopicID = PageTopicID;
				nMessageID = long.Parse(Request.QueryString["m"]);
			} 
			else
			{
				if(!ForumPostAccess)
					Data.AccessDenied();

				object PollID = null;
				
				if (PollRow1.Visible)
				{
					PollID = DB.poll_save(Question.Text,
						PollChoice1.Text,
						PollChoice2.Text,
						PollChoice3.Text,
						PollChoice4.Text,
						PollChoice5.Text,
						PollChoice6.Text,
						PollChoice7.Text,
						PollChoice8.Text,
						PollChoice9.Text);
				}

				// make message flags
				MessageFlags tFlags = new MessageFlags();

				tFlags.IsHTML = Message.UsesHTML;
				tFlags.IsBBCode = Message.UsesBBCode;

				string subject = Server.HtmlEncode(Subject.Text);
				TopicID = DB.topic_save(PageForumID,subject,msg,PageUserID,Priority.SelectedValue,PollID,User.IsAuthenticated ? null : From.Text,Request.UserHostAddress,null,tFlags.BitValue,ref nMessageID);
			}

			// Check if message is approved
			bool bApproved = false;
			using(DataTable dt = DB.message_list(nMessageID)) 
				foreach(DataRow row in dt.Rows) 
					bApproved = ((int)row["Flags"] & 16)==16;

			// Create notification emails
			if(bApproved) 
			{
				//TO DO: n�o funciona o Watch Email
				//Utils.CreateWatchEmail(this,nMessageID);
				Forum.Redirect(Pages.posts,"m={0}&#{0}",nMessageID);
			} 
			else 
			{
				// Tell user that his message will have to be approved by a moderator
				//AddLoadMessage("Since you posted to a moderated forum, a forum moderator must approve your post before it will become visible.");
				string url = Forum.GetLink(Pages.topics,"f={0}",PageForumID);
				if(Config.IsRainbow)
					Forum.Redirect(Pages.info,"i=1");
				else
					Forum.Redirect(Pages.info,"i=1&url={0}",Server.UrlEncode(url));
			}
		}

		private void CreatePoll_Click(object sender, System.EventArgs e) {
			CreatePollRow.Visible = false;
			PollRow1.Visible = true;
			PollRow2.Visible = true;
			PollRow3.Visible = true;
			PollRow4.Visible = true;
			PollRow5.Visible = true;
			PollRow6.Visible = true;
			PollRow7.Visible = true;
			PollRow8.Visible = true;
			PollRow9.Visible = true;
			PollRow10.Visible = true;
		}

		private void Cancel_Click(object sender, System.EventArgs e)
		{
			if (Request.QueryString["t"] != null || Request.QueryString["m"] != null)
			{
				// reply to existing topic or editing of existing topic
				Forum.Redirect(Pages.posts,"t={0}",PageTopicID);
			}
			else
			{
				// new topic -- cancel back to forum
				Forum.Redirect(Pages.topics,"f={0}",PageForumID);				
			}
		}

		private void Preview_Click(object sender, System.EventArgs e) {
			PreviewRow.Visible = true;

			MessageFlags tFlags = new MessageFlags();
			tFlags.IsHTML = Message.UsesHTML;
			tFlags.IsBBCode = Message.UsesBBCode;

			string body = FormatMsg.FormatMessage(this,Message.Text,tFlags);

			using(DataTable dt = DB.user_list(PageBoardID,PageUserID,true)) 
			{
				if(!dt.Rows[0].IsNull("user_Signature"))
					body += "<br/><hr noshade/>" + FormatMsg.FormatMessage(this,dt.Rows[0]["user_Signature"].ToString(),new MessageFlags());
			}
			
			PreviewCell.InnerHtml = body;
		}

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
