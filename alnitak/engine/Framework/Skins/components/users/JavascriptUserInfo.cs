// created on 18-12-2004 at 12:51

using System;
using System.Web.UI;
using Chronos.Core;
using Language;

namespace Alnitak {

	public class JavascriptUserInfo : Page {

		#region Control Rendering

		private HtmlTextWriter writer;
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter _writer )
		{
			try {
			writer = _writer;
			string locale = Request.QueryString["locale"];
			if( locale == null || locale == string.Empty ) {
				locale =  "en";
			}
			string url = OrionGlobals.AlnitakUrl;
				ILanguageInfo info = CultureModule.getLanguage(locale);
			int rulerId = int.Parse(Request.QueryString["id"]);

			User user = UserUtility.bd.getUser(rulerId);
				Ruler ruler = null;
			if( user != null ) {
				ruler = Universe.instance.getRuler(user.RulerId);
			}

			Write("<div class=\"orionsbelt_title\">");
			Write("<a href=\"{0}\" class=\"orionsbelt_link\">Orion`s Belt</a></div>", url);
			if( ruler == null || user.RulerId == -1 ) {
				Write(info.getContent("ruler_not_playing"));
				return;
			}
			
			Write("<table class=\"orionsbelt_table\">");
			Write("<tr>");
			Write("<td class=\"orionsbelt_td\">{0}</td>", info.getContent("name"));
			Write("<td class=\"orionsbelt_td\">{0}</td>", OrionGlobals.getCompleteLink(ruler).Replace("'", "\""));
			Write("</tr>");
			Write("<tr>");
			Write("<td class=\"orionsbelt_td\">{0}</td>", info.getContent("stats_alliances"));
			string allianceText = null;
			if ( Universe.instance.isDefaultAlliance(ruler) ) {
				allianceText = "<i>"+info.getContent("no_alliance")+"</i>";
			} else {
				allianceText = ruler.Alliance.Name;
			}
			Write("<td class=\"orionsbelt_td\">{0}</td>", allianceText  );
			Write("</tr>");
			Write("<tr>");
			Write("<td class=\"orionsbelt_td\">{0}</td>", info.getContent("planetas"));
			Write("<td class=\"orionsbelt_td\">{0}</td>", ruler.Planets.Length);
			Write("</tr>");
			Write("<tr>");
			Write("<td class=\"orionsbelt_td\">{0}</td>", info.getContent("batalhas"));
			Write("<td class=\"orionsbelt_td\"><span class=\"green\">{0}</span>/<span class=\"orange\">{1}</span>/<span class=\"red\">{2}</span></td>", ruler.Victories, ruler.Draws, ruler.Defeats );
			Write("</tr>");
			Write("<tr>");
			Write("<td class=\"orionsbelt_td\">{0}</td>", info.getContent("score"));
			Write("<td class=\"orionsbelt_td\">{0}</td>", ruler.getResourceCount("score"));
			Write("</tr>");
			Write("<tr>");
			Write("<td class=\"orionsbelt_td\">{0}</td>", info.getContent("battle_rank"));
			Write("<td class=\"orionsbelt_td\"><b>{0}</b></td>", (ruler.Rank==-1?"~":ruler.Rank.ToString()));
			Write("</tr>");
			Write("</table>");
			
			} catch( Exception ) {
				Write("<div class=\"orionsbelt_title\">Error processing the URL</div>");
			}
		}

		private void Write( string str, params object[] args )
		{
			str = string.Format(str, args);
			writer.WriteLine("document.write('{0}');", str);
		}

		#endregion
	
	};
}
