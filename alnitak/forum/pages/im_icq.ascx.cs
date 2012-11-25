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
	/// Summary description for active.
	/// </summary>
	public class im_icq : ForumPage
	{
		protected PageLinks PageLinks;
		protected Button Send;
		protected TextBox From, Email, Body;
		protected HtmlImage Status;

		public im_icq() : base("IM_ICQ")
		{
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if(!User.IsAuthenticated)
				Data.AccessDenied();

			if(!IsPostBack) 
			{
				Send.Text = GetText("SEND");
				From.Text = PageUserName;
				using(DataTable dt=DB.user_list(PageBoardID,Request.QueryString["u"],null)) 
				{
					foreach(DataRow row in dt.Rows) 
					{
						PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
						PageLinks.AddLink(row["user_nick"].ToString(),string.Format("userinfo.aspx?id={0}",row["User_ID"]));
						PageLinks.AddLink(GetText("TITLE"),Forum.GetLink(Pages.im_icq,"u={0}",row["User_ID"]));
						ViewState["to"] = (int)row["user_ICQ"];
						Status.Src = string.Format("http://web.icq.com/whitepages/online?icq={0}&img=5",row["ICQ"]);
						break;
					}
				}
				using(DataTable dt=DB.user_list(PageBoardID,PageUserID,null))
				{
					foreach(DataRow row in dt.Rows)
					{
						Email.Text = row["user_mail"].ToString();
						break;
					}
				}
			}
		}

		private void Send_Click(object sender,EventArgs e)
		{
			string html = string.Format("http://wwp.icq.com/scripts/WWPMsg.dll?from={0}&fromemail={1}&subject={2}&to={3}&body={4}",
				Server.UrlEncode(From.Text),
				Server.UrlEncode(Email.Text),
				Server.UrlEncode("From WebPager Panel"),
				ViewState["to"],
				Server.UrlEncode(Body.Text)
				);
			Response.Redirect(html);
		}

		override protected void OnInit(EventArgs e)
		{
			this.Load += new EventHandler(this.Page_Load);
			this.Send.Click += new EventHandler(Send_Click);
			base.OnInit(e);
		}
	}
}
