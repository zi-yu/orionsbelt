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

namespace yaf.pages
{
	/// <summary>
	/// Summary description for cp_signature.
	/// </summary>
	public class cp_signature : ForumPage
	{
		protected Button save, cancel;
		protected yaf.editor.ForumEditor sig;
		protected controls.PageLinks PageLinks;
		protected System.Web.UI.HtmlControls.HtmlTableCell EditorLine;

		public cp_signature() : base("CP_SIGNATURE")
		{
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			sig.BaseDir = Data.ForumRoot + "editors";

			if(!User.IsAuthenticated)
			{
				if(User.CanLogin)
					Forum.Redirect(Pages.login,"ReturnUrl={0}",Utils.GetSafeRawUrl());
				else
					Forum.Redirect(Pages.forum);
			}

			if(!IsPostBack) 
			{
				string msg = DB.user_getsignature(PageUserID);
				bool isHtml = msg.IndexOf('<')>=0;
				if(isHtml)
					msg = FormatMsg.HtmlToForumCode(msg);
				sig.Text = msg;

				PageLinks.AddLink(BoardSettings.Name,Forum.GetLink(Pages.forum));
				PageLinks.AddLink(PageUserName,Forum.GetLink(Pages.cp_profile));
				PageLinks.AddLink(GetText("TITLE"),Utils.GetSafeRawUrl());

				save.Text = GetText("Save");
				cancel.Text = GetText("Cancel");
			}
		}

		private void save_Click(object sender,EventArgs e) 
		{
			string body = sig.Text;
			body = FormatMsg.RepairHtml(this,body,false);

			if(sig.Text.Length>0)
				DB.user_savesignature(PageUserID,body);
			else
				DB.user_savesignature(PageUserID,DBNull.Value);
			Forum.Redirect(Pages.cp_profile);
		}

		private void cancel_Click(object sender,EventArgs e) {
			Forum.Redirect(Pages.cp_profile);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			// since signatures are so small only allow BBCode in them...
			sig = new yaf.editor.BBCodeEditor();
			EditorLine.Controls.Add(sig);

			save.Click += new EventHandler(save_Click);
			cancel.Click += new EventHandler(cancel_Click);
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
