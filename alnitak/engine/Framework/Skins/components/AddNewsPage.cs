using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {
	
	public class AddNewsPage : UserControl {
	
		#region Instance Fields
		
		protected TextBox title;
		protected TextBox message;
		protected DropDownList languages;
		
		#endregion
		
		#region Events

		protected override void OnPreRender(EventArgs e) {
			languages.DataSource = CultureModule.Languages;
			languages.DataBind();
			base.OnPreRender (e);
		}

		
		/// <summary>Envia notícias</summary>
		protected void SendNews( object src, EventArgs args )
		{
			NewsUtility.Persistence.AddNews( title.Text, message.Text, languages.SelectedValue );
			HttpContext.Current.Cache.Remove("NewsList");
		}
		
		#endregion
	};
	
}

