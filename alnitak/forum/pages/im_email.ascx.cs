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
using System.Web;
using System.Web.UI.WebControls;
using yaf.controls;

namespace yaf.pages
{
	/// <summary>
	/// Summary description for active.
	/// </summary>
	public class im_email : ForumPage
	{
		protected PageLinks PageLinks;
		protected Button Send;
		protected TextBox Subject, Body;

		public im_email() : base("IM_EMAIL")
		{
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if(!User.IsAuthenticated)
				Data.AccessDenied();

			if(!IsPostBack) 
			{
				using(DataTable dt=DB.user_list(PageBoardID,Request.QueryString["u"],null)) 
				{
					foreach(DataRow row in dt.Rows) 
					{
						PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
						PageLinks.AddLink(row["user_nick"].ToString(),string.Format("userinfo.aspx?id={0}",row["User_ID"]));
						PageLinks.AddLink(GetText("TITLE"),Forum.GetLink(Pages.im_email,"u={0}",row["User_ID"]));
						break;
					}
				}
				Send.Text = GetText("SEND");
			}
		}

		private void Send_Click(object sender,EventArgs e)
		{
			try
			{
 				string from = string.Empty, to = string.Empty;
 				string fromName = string.Empty, toName = string.Empty;
				using(DataTable dt=DB.user_list(PageBoardID,Request.QueryString["u"],null)) 
				{
					foreach(DataRow row in dt.Rows) 
					{
 						to = row["Email"].ToString();
 						toName = row["Name"].ToString();
						break;
					}
				}
				using(DataTable dt=DB.user_list(PageBoardID,PageUserID,null)) 
				{
					foreach(DataRow row in dt.Rows) 
					{
 						from = row["user_mail"].ToString();
 						fromName = row["user_nick"].ToString();
						break;
					}
				}
 				Utils.SendMail(this, from, fromName, to, toName, Subject.Text, Body.Text);
				HttpContext.Current.Response.Redirect(string.Format("userinfo.aspx?id={0}",Request.QueryString["u"]));
			}
			catch(Exception x) 
			{
				if(IsAdmin)
					AddLoadMessage(x.Message);
				else
					AddLoadMessage(GetText("ERROR"));
			}
		}

		override protected void OnInit(EventArgs e)
		{
			this.Load += new EventHandler(this.Page_Load);
			this.Send.Click += new EventHandler(Send_Click);
			base.OnInit(e);
		}
	}
}
