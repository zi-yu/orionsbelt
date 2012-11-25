/* Yet Another Forum.net
 * Copyright (C) 2003 Bjørnar Henden
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

namespace yaf.pages
{
	/// <summary>
	/// Summary description for editprofile.
	/// </summary>
	public class cp_profile : ForumPage
	{
		protected Label TitleUserName;
		protected Label NumPosts;
		protected Label Name;
		protected Label Joined;
		protected Label AccountEmail;
		protected Repeater Groups;
		protected HtmlImage AvatarImage;
		protected PageLinks PageLinks;

		public cp_profile() : base("CP_PROFILE")
		{
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if(!User.IsAuthenticated)
			{
				if(User.CanLogin)
					Forum.Redirect(Pages.login,"ReturnUrl={0}",Utils.GetSafeRawUrl());
				else
					Forum.Redirect(Pages.forum);
			}

			if(!IsPostBack) 
			{
				BindData();

				PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
				PageLinks.AddLink(PageUserName,Utils.GetSafeRawUrl());
			}
		}

		private void BindData()
		{
			DataRow row;

			Groups.DataSource = DB.usergroup_list(PageUserID);

			// Bind			
			DataBind();
			using(DataTable dt = DB.user_list(PageBoardID,PageUserID,true)) {
				row = dt.Rows[0];
			}

			TitleUserName.Text = Server.HtmlEncode((string)row["user_nick"]);
			AccountEmail.Text = row["user_mail"].ToString();
			Name.Text = Server.HtmlEncode((string)row["user_nick"]);
			Joined.Text = FormatDateTime((DateTime)row["user_registdate"]);
			NumPosts.Text = String.Format("{0:N0}",row["user_NumPosts"]);

			if(row["user_Avatar"].ToString().Length > 0) {
				AvatarImage.Src = String.Format("{0}",row["user_Avatar"].ToString());
				
			} else {
				AvatarImage.Src = String.Format("{0}",Alnitak.User.DefaultAvatar);
			}
			AvatarImage.Width = BoardSettings.AvatarWidth;
			AvatarImage.Height = BoardSettings.AvatarHeight;
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
