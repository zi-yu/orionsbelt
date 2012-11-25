namespace Alnitak{

	using System.Web.UI;
	using System.Web;

	public class PageTitle : Control {

		private string _pageTitle;

    	protected override void Render(HtmlTextWriter writer) {
			string title = CultureModule.getLanguage().getContent( "section_" + _pageTitle.ToLower() );
			MasterSkinInfo skin = (MasterSkinInfo) Context.Items["MasterSkinInfo"];
			
			if( skin != null && skin.masterSkinName.IndexOf("galaxy") != -1 && title.IndexOf(" ") != -1 ) {
				writer.Write( "<table width='{0}'><tr><td align='center'>{1}</tr></td></table>", title.Length*7 ,title );
			} else {
				writer.Write( title );
			}
			
		}

		public PageTitle() {
			_pageTitle = ((PageInfo)HttpContext.Current.Items["PageInfo"]).pageTitle;
		}

	}
}
