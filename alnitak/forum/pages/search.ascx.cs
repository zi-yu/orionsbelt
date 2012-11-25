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
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace yaf.pages
{
	/// <summary>
	/// Summary description for topics.
	/// </summary>
	public class search : ForumPage
	{
		/// <summary>
		/// Search Button.
		/// </summary>
		protected System.Web.UI.WebControls.Button btnSearch;

		/// <summary>
		/// Forum List Box.
		/// </summary>
		protected System.Web.UI.WebControls.DropDownList listForum;

		/// <summary>
		/// Text box for search string.
		/// </summary>
		protected System.Web.UI.WebControls.TextBox txtSearchString;

		/// <summary>
		/// A dropdown list.
		/// </summary>
		protected System.Web.UI.WebControls.DropDownList listResInPage;

		/// <summary>
		/// A dropdown list.
		/// </summary>
		protected System.Web.UI.WebControls.DropDownList listSearchWath;

		/// <summary>
		/// A list box.
		/// </summary>
		protected System.Web.UI.WebControls.DropDownList listSearchWhere;

		/// <summary>
		/// The repeater control.
		/// </summary>
		protected System.Web.UI.WebControls.Repeater SearchRes;

		/// <summary>
		/// Page links control.
		/// </summary>
		protected controls.PageLinks PageLinks;

		/// <summary>
		/// Pager control.
		/// </summary>
		protected controls.Pager Pager;
    
		protected System.Web.UI.WebControls.PlaceHolder NoResults;
	
		/// <summary>
		/// The search page constructor.
		/// </summary>
		public search() : base("SEARCH")
		{
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
				PageLinks.AddLink(GetText("TITLE"),Forum.GetLink(Pages.search));
				btnSearch.Text = GetText("btnsearch");

				// Load result dropdown
				listResInPage.Items.Add(new ListItem(GetText("result5"),"5"));
				listResInPage.Items.Add(new ListItem(GetText("result10"),"10"));
				listResInPage.Items.Add(new ListItem(GetText("result25"),"25"));
				listResInPage.Items.Add(new ListItem(GetText("result50"),"50"));

				// Load searchwhere dropdown
				listSearchWhere.Items.Add(new ListItem(GetText("posts"),"0"));
				listSearchWhere.Items.Add(new ListItem(GetText("postedby"),"1"));

				// Load listSearchWath dropdown
				listSearchWath.Items.Add(new ListItem(GetText("match_all"),"0"));
				listSearchWath.Items.Add(new ListItem(GetText("match_any"),"1"));
				listSearchWath.Items.Add(new ListItem(GetText("match_exact"),"2"));

				// Load forum's combo
				listForum.Items.Add(new ListItem(GetText("allforums"),"-1"));
				DataTable dt = DB.forum_listread(PageBoardID,PageUserID,null,null);

				int nOldCat = 0;
				for(int i=0;i<dt.Rows.Count;i++) 
				{
					DataRow row = dt.Rows[i];
					if((int)row["CategoryID"] != nOldCat) 
					{
						nOldCat = (int)row["CategoryID"];
						listForum.Items.Add(new ListItem((string)row["Category"],"-1"));
					}
					listForum.Items.Add(new ListItem(" - " + (string)row["Forum"],row["ForumID"].ToString()));
				}
			}
		}

		private void Pager_PageChange(object sender,EventArgs e)
		{
			BindData(false);
		}

		#region Web Form Designer generated code
		/// <summary>
		/// The initialization script for the search page.
		/// </summary>
		/// <param name="e">The EventArg object for the search page.</param>
		override protected void OnInit(EventArgs e)
		{
			Pager.PageChange += new EventHandler(Pager_PageChange);
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.SearchRes.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.SearchRes_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BindData( bool newSearch )
		{
			try
			{
				if( newSearch )
				{
					SEARCH_FIELD sf = (SEARCH_FIELD)System.Enum.Parse( typeof( SEARCH_FIELD ), listSearchWhere.SelectedValue );
					SEARCH_WHAT sw = (SEARCH_WHAT)System.Enum.Parse( typeof( SEARCH_WHAT ), listSearchWath.SelectedValue );
					int forumID = int.Parse( listForum.SelectedValue );

					DataView dv = DB.GetSearchResult( txtSearchString.Text, sf, sw, forumID, PageUserID ).DefaultView;
					Pager.CurrentPageIndex = 0;
					Pager.PageSize = int.Parse(listResInPage.SelectedValue);
					Pager.Count = dv.Count;
					Mession.SearchData = dv;

					bool bResults = (dv.Count > 0) ? true : false;

					SearchRes.Visible = bResults;
					NoResults.Visible = !bResults;
				}

				PagedDataSource pds = new PagedDataSource();
				pds.AllowPaging = true;
				pds.DataSource = Mession.SearchData;
				pds.PageSize = Pager.PageSize;
				pds.CurrentPageIndex = Pager.CurrentPageIndex;

				SearchRes.DataSource = pds;
				DataBind();
			}
			catch(System.Data.SqlClient.SqlException x)
			{
				Utils.LogToMail(x);
				if(IsAdmin)
					AddLoadMessage(string.Format("{0}",x));
				else
					AddLoadMessage("An error occurred in the database.");
			}
			catch(Exception x) 
			{
				Utils.LogToMail(x);
				if(IsAdmin)
					AddLoadMessage(string.Format("{0}",x));
				else
					AddLoadMessage("An error occured while searching.");
			}
		}

		/// <summary>
		/// Formats the message and replaces Forum Code to HTML.
		/// </summary>
		/// <param name="o">The object DataRowView that is going to be converted.</param>
		/// <returns>Returns the convertted string.</returns>
		public string FormatMessage( object o )
		{
			DataRowView row = (DataRowView)o;
			return FormatMsg.FormatMessage(this,row["Message"].ToString(),new MessageFlags(Convert.ToInt32(row["Flags"])));
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindData(true);
		}

		private void SearchRes_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl("CounterCol");
			if(cell!=null)
			{
				string messageID = cell.InnerText;
				int rowCount = e.Item.ItemIndex + 1 + (Pager.CurrentPageIndex * Pager.PageSize);
				cell.InnerHtml = string.Format( "<a href=\"{1}\">{0}</a>", rowCount, Forum.GetLink(Pages.posts,"m={0}#{0}",messageID));
			}
		}
	}
}
