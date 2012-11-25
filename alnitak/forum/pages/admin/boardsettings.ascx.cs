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
using System.Globalization;

namespace yaf.pages.admin {
	/// <summary>
	/// Summary description for settings.
	/// </summary>
	public class boardsettings : AdminPage
	{
		protected Button Save;
		protected TextBox Name;
		protected CheckBox AllowThreaded;
		protected controls.PageLinks PageLinks;
		protected DropDownList  Language, ShowTopic;
		protected yaf.controls.AdminMenu Adminmenu1;
		protected yaf.controls.SmartScroller SmartScroller1;
	
		private void Page_Load(object sender, System.EventArgs e) 
		{
			if(!IsPostBack)
			{
				PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
				PageLinks.AddLink("Administration",Forum.GetLink(Pages.admin_admin));
				PageLinks.AddLink("Board Settings",Forum.GetLink(Pages.admin_boardsettings));

				Language.DataSource = Data.Languages();
				Language.DataTextField = "Language";
				Language.DataValueField = "FileName";

				ShowTopic.DataSource = Data.TopicTimes();
				ShowTopic.DataTextField = "TopicText";
				ShowTopic.DataValueField = "TopicValue";

				BindData();
				
				Language.Items.FindByValue(BoardSettings.Language).Selected = true;
				ShowTopic.Items.FindByValue(BoardSettings.ShowTopicsDefault.ToString()).Selected = true;
			}
		}

		private void BindData()
		{
			DataRow row;
			using(DataTable dt = DB.board_list(PageBoardID))
				row = dt.Rows[0];

			DataBind();
			Name.Text = (string)row["Name"];
			AllowThreaded.Checked = (bool)row["AllowThreaded"];
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
			this.Save.Click += new System.EventHandler(this.Save_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Save_Click(object sender, System.EventArgs e)
		{
			DB.board_save(PageBoardID,Name.Text,AllowThreaded.Checked);

			BoardSettings.Language = Language.SelectedValue;
			BoardSettings.ShowTopicsDefault = Convert.ToInt32(ShowTopic.SelectedValue);

			/// save the settings to the database
			BoardSettings.SaveRegistry();

			/// Reload forum settings
			BoardSettings = null;

			Forum.Redirect(Pages.admin_admin);
		}
	}
}
