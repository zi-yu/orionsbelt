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
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using yaf.controls;
using yaf.install;

namespace yaf.pages.admin
{
	/// <summary>
	/// Summary description for main.
	/// </summary>
	public class admin : AdminPage
	{
		protected Repeater ActiveList, UserList;
		protected Label NumPosts,NumTopics,NumUsers,BoardStart,DayPosts,DayTopics,DayUsers,DBSize;
		protected HtmlGenericControl UpgradeNotice;
		protected AdminMenu Adminmenu1;
		protected SmartScroller SmartScroller1;
		protected PageLinks PageLinks;
	
		private void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack) 
			{
				PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
				PageLinks.AddLink("Administration",Forum.GetLink(Pages.admin_admin));
				BindData();
				UpgradeNotice.Visible = _default.GetCurrentVersion() < Data.AppVersion;
			}
		}

		protected void Delete_Load(object sender, EventArgs e) 
		{
			((LinkButton)sender).Attributes["onclick"] = "return confirm('Delete this user?')";
		}

		protected void Approve_Load(object sender, EventArgs e) 
		{
			((LinkButton)sender).Attributes["onclick"] = "return confirm('Approve this user?')";
		}

		private void BindData() 
		{
			ActiveList.DataSource = DB.active_list(PageBoardID,true);
			UserList.DataSource = DB.user_list(PageBoardID,null,false);
			DataBind();

			DataRow row = DB.board_stats();
			NumPosts.Text	= String.Format("{0:N0}",row["NumPosts"]);
			NumTopics.Text	= String.Format("{0:N0}",row["NumTopics"]);
			NumUsers.Text	= String.Format("{0:N0}",row["NumUsers"]);

			TimeSpan span = DateTime.Now - (DateTime)row["BoardStart"];
			double days = span.Days;

			BoardStart.Text	= String.Format("{0:d} ({1:N0} days ago)",row["BoardStart"],days);

			if(days<1) days = 1;
			DayPosts.Text = String.Format("{0:N2}",(int)row["NumPosts"] / days);
			DayTopics.Text = String.Format("{0:N2}",(int)row["NumTopics"] / days);
			DayUsers.Text = String.Format("{0:N2}",(int)row["NumUsers"] / days);

			DBSize.Text = "---";
		}

		private void UserList_ItemCommand(object source, RepeaterCommandEventArgs e) 
		{
			switch(e.CommandName) 
			{
				case "edit":
					Forum.Redirect(Pages.admin_edituser,"u={0}",e.CommandArgument);
					break;
				case "delete":
					DB.user_delete(e.CommandArgument);
					BindData();
					break;
				case "approve":
					DB.user_approve(e.CommandArgument);
					BindData();
					break;
				case "deleteall":
					DB.user_deleteold(PageBoardID);
					BindData();
					break;
			}
		}

		protected string FormatForumLink(object ForumID,object ForumName) 
		{
			if(ForumID.ToString()=="" || ForumName.ToString()=="")
				return "";

			return String.Format("<a target=\"_top\" href=\"{0}\">{1}</a>",Forum.GetLink(Pages.topics,"f={0}",ForumID),ForumName);
		}

		protected string FormatTopicLink(object TopicID,object TopicName) {
			if(TopicID.ToString()=="" || TopicName.ToString()=="")
				return "";

			return String.Format("<a target=\"_top\" href=\"{0}\">{1}</a>",Forum.GetLink(Pages.posts,"t={0}",TopicID),TopicName);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			this.UserList.ItemCommand += new RepeaterCommandEventHandler(this.UserList_ItemCommand);
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
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion
	}
}
