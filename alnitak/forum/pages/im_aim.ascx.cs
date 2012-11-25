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
using System.Web.UI.WebControls;
using yaf.controls;

namespace yaf.pages
{
	/// <summary>
	/// Summary description for active.
	/// </summary>
	public class im_aim : ForumPage
	{
		protected PageLinks PageLinks;
		protected HyperLink Msg, Buddy;

		public im_aim() : base("IM_AIM")
		{
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if(!User.IsAuthenticated)
				Data.AccessDenied();

			if(!IsPostBack) {
				using(DataTable dt=DB.user_list(PageBoardID,Request.QueryString["u"],null)) 
				{
					foreach(DataRow row in dt.Rows) 
					{
						PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
						PageLinks.AddLink(row["user_nick"].ToString(),string.Format("userinfo.aspx?id={0}",row["User_ID"]));
						PageLinks.AddLink(GetText("TITLE"),Forum.GetLink(Pages.im_aim,"u={0}",row["User_ID"]));

						Msg.NavigateUrl = string.Format("aim:goim?screenname={0}&message=Hi.+Are+you+there?",row["user_AIM"]);
						Buddy.NavigateUrl = string.Format("aim:addbuddy?screenname={0}",row["user_AIM"]);
						break;
					}
				}
			}
		}

		override protected void OnInit(EventArgs e)
		{
			this.Load += new EventHandler(this.Page_Load);
			base.OnInit(e);
		}
	}
}
