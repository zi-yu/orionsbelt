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

namespace yaf.pages.admin
{
	/// <summary>
	/// Summary description for ranks.
	/// </summary>
	public class ranks : AdminPage
	{
		protected System.Web.UI.WebControls.LinkButton NewRank;
		protected System.Web.UI.WebControls.Repeater RankList;
		protected controls.PageLinks PageLinks;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack) 
			{
				PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
				PageLinks.AddLink("Administration",Forum.GetLink(Pages.admin_admin));
				PageLinks.AddLink("Ranks",Forum.GetLink(Pages.admin_ranks));

				BindData();
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.RankList.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.RankList_ItemCommand);
			this.NewRank.Click += new System.EventHandler(this.NewRank_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected void Delete_Load(object sender, System.EventArgs e) 
		{
			((LinkButton)sender).Attributes["onclick"] = "return confirm('Delete this rank?')";
		}

		private void BindData() 
		{
			RankList.DataSource = DB.rank_list(PageBoardID,null);
			DataBind();
		}

		private void RankList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			switch(e.CommandName) 
			{
				case "edit":
					Forum.Redirect(Pages.admin_editrank,"r={0}",e.CommandArgument);
					break;
				case "delete":
					DB.rank_delete(e.CommandArgument);
					BindData();
					break;
			}
		}

		private void NewRank_Click(object sender, System.EventArgs e)
		{
			Forum.Redirect(Pages.admin_editrank);
		}

		protected string LadderInfo(object _o) {
			DataRowView dr = (DataRowView)_o;

			///object IsLadder,object MinPosts
			///DataBinder.Eval(Container.DataItem, "IsLadder"),DataBinder.Eval(Container.DataItem, "MinPosts")
			
			bool isLadder = ((int)dr["Flags"] & (int)RankFlags.IsLadder) == (int)RankFlags.IsLadder;
			
			string tmp = String.Format("{0}",isLadder);
			if(isLadder) {
				tmp += String.Format(" ({0} posts)",dr["MinPosts"]);
			}
			return tmp;
		}

		protected bool BitSet(object _o,int bitmask) 
		{
			int i = (int)_o;
			return (i & bitmask)!=0;
		}
	}
}
