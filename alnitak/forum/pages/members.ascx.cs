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
using Alnitak;
using yaf.controls;

namespace yaf.pages
{
	/// <summary>
	/// Summary description for members.
	/// </summary>
	public class members : ForumPage
	{
		protected Repeater MemberList;
		protected LinkButton UserName,Joined,Posts, Rank;
		protected HtmlImage SortUserName, SortRank, SortJoined, SortPosts;
		protected PageLinks PageLinks;
		protected Pager Pager;
		protected HtmlTableRow LetterRow;

		public members() : base("MEMBERS")
		{
		}

		private object QLetter
		{
			get
			{
				string rletter = string.Empty;
				if(Request.QueryString["letter"]!=null) 
				{
					rletter = Request.QueryString["letter"];
					if(rletter=="_")
						rletter = "#";
					return rletter;
				}
				return null;
			}
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if(!User.IsAuthenticated){
				OrionGlobals.forceLogin();
			}

			if(!IsPostBack) 
			{
				PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
				PageLinks.AddLink(GetText("TITLE"),Forum.GetLink(Pages.members));

				SetSort("user_nick",true);

				UserName.Text = GetText("username");
				Rank.Text = GetText("rank");
				Joined.Text = GetText("joined");
				Posts.Text = GetText("posts");

				BindData();
			}
		}

		private void SetSort(string field,bool asc) 
		{
			if(ViewState["SortField"]!=null && (string)ViewState["SortField"] == field) 
			{
				ViewState["SortAscending"] = !(bool)ViewState["SortAscending"];
			}
			else 
			{
				ViewState["SortField"] = field;
				ViewState["SortAscending"] = asc;
			}
		}

		private void UserName_Click(object sender, EventArgs e) 
		{
			SetSort("user_nick",true);
			BindData();
		}

		private void Joined_Click(object sender, EventArgs e) 
		{
			SetSort("user_registdate",true);
			BindData();
		}

		private void Posts_Click(object sender, EventArgs e) 
		{
			SetSort("user_NumPosts",false);
			BindData();
		}

		private void Rank_Click(object sender, EventArgs e) 
		{
			SetSort("RankName",true);
			BindData();
		}

		private void Pager_PageChange(object sender, EventArgs e)
		{
			BindData();
		}

		private void BindData() 
		{
			Pager.PageSize = 20;

			DataView dv = DB.user_list(PageBoardID,null,true).DefaultView;
			
			if(QLetter!=null) {
				if(QLetter.ToString()=="#") 
				{
					string filter = string.Empty;
					foreach(char letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZ") 
					{
						if(filter==string.Empty)
							filter = string.Format("user_nick not like '{0}%'",letter);
						else
							filter += string.Format("and user_nick not like '{0}%'",letter);
					}
					dv.RowFilter = filter;
				}
				else
					dv.RowFilter = string.Format("user_nick like '{0}%'",QLetter);
			}

			Pager.Count = dv.Count;

			dv.Sort = String.Format("{0} {1}",ViewState["SortField"],(bool)ViewState["SortAscending"] ? "asc" : "desc");
			PagedDataSource pds = new PagedDataSource();
			pds.DataSource = dv;
			pds.AllowPaging = true;
			pds.CurrentPageIndex = Pager.CurrentPageIndex;
			pds.PageSize = Pager.PageSize;
			
			MemberList.DataSource = pds;
			DataBind();

			SortUserName.Visible = (string)ViewState["SortField"] == "user_nick";
			SortUserName.Src = GetThemeContents("SORT",(bool)ViewState["SortAscending"] ? "ASCENDING" : "DESCENDING");
			SortRank.Visible = (string)ViewState["SortField"] == "RankName";
			SortRank.Src = SortUserName.Src;
			SortJoined.Visible = (string)ViewState["SortField"] == "user_registdate";
			SortJoined.Src = SortUserName.Src;
			SortPosts.Visible = (string)ViewState["SortField"] == "user_NumPosts";
			SortPosts.Src = SortUserName.Src;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			this.UserName.Click += new EventHandler(this.UserName_Click);
			this.Joined.Click += new EventHandler(this.Joined_Click);
			this.Posts.Click += new EventHandler(this.Posts_Click);
			this.Rank.Click += new EventHandler(this.Rank_Click);
			this.Pager.PageChange += new EventHandler(Pager_PageChange);
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		
			foreach(char letter in "#ABCDEFGHIJKLMNOPQRSTUVWXYZ") 
			{
				HtmlTableCell cell = new HtmlTableCell();
				cell.Align = "center";
				if(QLetter!=null && QLetter.ToString()==letter.ToString())
					cell.Attributes["class"] = "postheader";
				else
					cell.Attributes["class"] = "post";
	
				HyperLink btn = new HyperLink();
				btn.Text = letter.ToString();
				btn.NavigateUrl = Forum.GetLink(Pages.members,"letter={0}",letter=='#'?'_':letter);
				cell.Controls.Add(btn);

				LetterRow.Cells.Add(cell);
			}
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
