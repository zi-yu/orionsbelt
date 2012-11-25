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
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using yaf.controls;
using yaf.editor;

namespace yaf.pages
{
	/// <summary>
	/// Summary description for pmessage.
	/// </summary>
	public class pmessage : ForumPage
	{
		protected TextBox Subject;
		protected ForumEditor Editor;
		protected TextBox To;
		protected HtmlTableRow ToRow;
		protected Button Cancel;
		protected Button Save;
		protected HtmlTableCell EditorLine;
		protected DropDownList ToList;
		protected Button FindUsers;
		protected Button AllUsers;
		protected PageLinks PageLinks;
	
		public pmessage() : base("PMESSAGE")
		{
		}

		private void Page_Load(object sender, EventArgs e)
		{
			Editor.BaseDir = Data.ForumRoot + "editors";

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
				Save.Text = GetText("Save");
				Cancel.Text = GetText("Cancel");
				FindUsers.Text = GetText("FINDUSERS");
				AllUsers.Text = GetText("ALLUSERS");

				if (IsAdmin)
				{
					AllUsers.Visible = true;
				}
				else
				{
					AllUsers.Visible = false;
				}

				int ToUserID = 0;

				if(Request.QueryString["p"] != null)
				{
					using(DataTable dt = DB.userpmessage_list(Request.QueryString["p"]))
					{
						DataRow row = dt.Rows[0];
						Subject.Text = (string)row["Subject"];

						if(Subject.Text.Length<4 || Subject.Text.Substring(0,4) != "Re: ")
							Subject.Text = "Re: " + Subject.Text;

						ToUserID = (int)row["FromUserID"];

						string body = row["Body"].ToString();
						bool isHtml = body.IndexOf('<')>=0;

						if (BoardSettings.RemoveNestedQuotes)
						{
							RegexOptions m_options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline;
							Regex	quote = new Regex(@"\[quote(\=.*)?\](.*?)\[/quote\]",m_options);
							// remove quotes from old messages
							body = quote.Replace(body,"");
						}

						if (isHtml) body = FormatMsg.HtmlToForumCode(body);
						body = String.Format("[QUOTE={0}]{1}[/QUOTE]",row["FromUser"],body);

						Editor.Text = body;
					}
				} 
				if(Request.QueryString["u"] != null)
					ToUserID = int.Parse(Request.QueryString["u"].ToString());

				if(ToUserID!=0) {
					using(DataTable dt = DB.user_list(PageBoardID,ToUserID,true)) 
					{
						To.Text = (string)dt.Rows[0]["user_nick"];
						To.Enabled = false;
					}
				}
			}
		}

		private void BindData() {
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			Editor = EditorHelper.CreateEditorFromType(BoardSettings.ForumEditor);
			EditorLine.Controls.Add(Editor);

			this.Save.Click += new EventHandler(this.Save_Click);
			this.Cancel.Click += new EventHandler(this.Cancel_Click);
			this.FindUsers.Click += new EventHandler(this.FindUsers_Click);
			this.AllUsers.Click += new EventHandler(this.AllUsers_Click);
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

		private void Save_Click(object sender, EventArgs e) {
			if(To.Text.Length<=0) {
				AddLoadMessage(GetText("need_to"));
				return;
			}
			if(ToList.Visible)
				To.Text = ToList.SelectedItem.Text;


			if(ToList.SelectedItem!=null && ToList.SelectedItem.Value == "0")
			{
				string body = Editor.Text;
				MessageFlags tFlags = new MessageFlags();
				tFlags.IsHTML = Editor.UsesHTML;
				tFlags.IsBBCode = Editor.UsesBBCode;
				DB.pmessage_save(PageUserID,0,Subject.Text,body,tFlags.BitValue);
				Forum.Redirect(Pages.cp_profile);
			}
			else
			{
				using(DataTable dt = DB.user_find(PageBoardID,false,To.Text,null)) 
				{
					if(dt.Rows.Count!=1) 
					{
						AddLoadMessage(GetText("NO_SUCH_USER"));
						return;
					} 
					else if((int)dt.Rows[0]["IsGuest"]>0) 
					{
						AddLoadMessage(GetText("NOT_GUEST"));
						return;	
					}

					if(Subject.Text.Length<=0) 
					{
						AddLoadMessage(GetText("need_subject"));
						return;
					}
					if(Editor.Text.Length<=0) 
					{
						AddLoadMessage(GetText("need_message"));
						return;
					}

					string body = Editor.Text;

					MessageFlags tFlags = new MessageFlags();
					tFlags.IsHTML = Editor.UsesHTML;
					tFlags.IsBBCode = Editor.UsesBBCode;

					DB.pmessage_save(PageUserID,dt.Rows[0]["User_ID"],Subject.Text,body,tFlags.BitValue);
					Forum.Redirect(Pages.cp_profile);
				}
			}
			
		}

		private void Cancel_Click(object sender, EventArgs e) {
			Forum.Redirect(Pages.cp_profile);
		}

		private void FindUsers_Click(object sender, EventArgs e) 
		{
			if(To.Text.Length<2) return;

			using(DataTable dt = DB.user_find(PageBoardID,true,To.Text,null)) 
			{
				if(dt.Rows.Count>0) 
				{
					ToList.DataSource = dt;
					ToList.DataValueField = "User_ID";
					ToList.DataTextField = "user_nick";
					ToList.SelectedIndex = 0;
					ToList.Visible = true;
					To.Visible = false;
					FindUsers.Visible = false;
				} 
				DataBind();
			}
		}
		private void AllUsers_Click(object sender, EventArgs e) 
		{
			ListItem li = new ListItem("All Users", "0");
			ToList.Items.Add(li);
			ToList.Visible = true;
			To.Text = "All Users";
			To.Visible = false;
			FindUsers.Visible = false;
			AllUsers.Visible = false;
		}
	}
}
