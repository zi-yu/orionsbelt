// created on 3/21/2006 at 10:40 AM

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Chronos.Core;
using Chronos.Alliances;

namespace Alnitak {
	
	public class TopAlliances : Control {
	
		private ArrayList Sort()
		{
			string option = Page.Request.QueryString["option"];
			if( option != "round" && option != "rank" ) {
				option = "round";
			}
			
			ArrayList all = AllianceUtility.Persistance.GetAll();
			
			if( option == "round" ) {
				AllianceUtility.SortByRound(all);
			} else {
				AllianceUtility.SortByRanking(all);
			}
			
			return all;
		}
	
		protected override void Render( HtmlTextWriter writer )
		{
			 WriteMenu(writer);
			 WriteAlliances(writer, Sort());
		}
		
		protected void WriteMenu( HtmlTextWriter writer )
		{
			writer.WriteLine("<div style='text-align: right'>");
			writer.Write("<a href='{0}topalliances.aspx?option=round'>{1}</a> | ", OrionGlobals.AppPath, CultureModule.getContent("alliance_by_round"));
			writer.Write("<a href='{0}topalliances.aspx?option=rank'>{1}</a>", OrionGlobals.AppPath, CultureModule.getContent("alliance_by_rank"));
			writer.WriteLine("</div><p/>");
		}
		
		protected void WriteAlliances( HtmlTextWriter writer, ArrayList all )
		{
			int max = 50;
			if( all.Count < max ) {
				max = all.Count;
			}
			
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.WriteLine("<th class='resourceTitle'>#</th>");
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("section_alliance"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("alliance_tag"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("alliance_members"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("planetas"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("spaceTitle"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("section_topranks"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("score"));
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < max; ++i ) {
				AllianceInfo info = (AllianceInfo) all[i];
				if( !info.HasMembers ) {
					continue;
				}
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resourceCell'>{0}</td>", i+1);
				writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.getLink(info));
				writer.WriteLine("<td class='resourceCell'>{0}</td>", info.Tag);
				writer.WriteLine("<td class='resourceCell'>{0}</td>", info.Members.Count);
				writer.WriteLine("<td class='resourceCell'>{0}</td>", info.TotalPlanets);
				writer.WriteLine("<td class='resourceCell'>{0}</td>", info.TotalSpace);
				writer.WriteLine("<td class='resourceCell'><b title='Avg: {1}'>{0}</b> ({2})</td>", info.Ranking, info.AverageRanking, info.RankingBattles);
				writer.WriteLine("<td class='resourceCell'><b title='{1}'>{0}</b> (<span class='green'>{2}</span>/<span class='red'>{3}</span>)</td>", info.Score, info.AverageScore, info.TotalRoundWins, info.TotalRoundDefeats);
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
	};

}
