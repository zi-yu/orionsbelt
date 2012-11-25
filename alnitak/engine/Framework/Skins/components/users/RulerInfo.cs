// created on 18-12-2004 at 12:51

using System.Security.Principal;
using System.Web;
using System.Web.UI;
using Chronos.Core;
using Chronos.Messaging;

namespace Alnitak {

	public class RulerInfo : PlanetControl {

		#region Instance Fields
		
		private Ruler ruler;
		private string title;

		#endregion

		#region Instance Properties
		
		/// <summary>Indica o ruler associado ao controlo</summary>
		public Ruler Ruler {
			get { return ruler; }
			set { ruler = value; }
		}

		/// <summary>Indica o título do controlo</summary>
		public string Title {
			get { return title; }
			set { title = value; }
		}

		#endregion

		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			OrionGlobals.RegisterRequest(MessageType.Generic, string.Format("{1} {0}",info.getContent("section_home"), ruler.Name));
			
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>" + Title + "</b></div>");
			
			writer.WriteLine("<table class='planetFrame' style='margin-bottom: 0px; border-bottom:none;'>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("name"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.getLink(ruler));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("stats_alliances"));
			string allianceText = null;
			if ( ruler.AllianceId == 0 ) {
				allianceText = "<i>"+info.getContent("no_alliance")+"</i>";
			} else {
				AllianceInfo allianceInfo = AllianceUtility.Persistance.Get(ruler.AllianceId);
				if( allianceInfo != null ) {
					allianceText = OrionGlobals.getLink( allianceInfo );
				}else {
					allianceText = "<i>"+info.getContent("no_alliance")+"</i>";	
				}
			}

			writer.WriteLine("<td class='resourceCell'>{0}</td>", allianceText  );
			writer.WriteLine("</tr>");
			writer.WriteLine("</table>");
			
			writer.WriteLine("<table class='planetFrame' style='margin-top: 0px;'>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("planetas"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Planets.Length);
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("maxPlanets"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.getResourceCount("maxPlanets"));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("victories"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Victories);
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("defeats"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Defeats);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("culture"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.getResourceCount("culture"));
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("score"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.getResourceCount("score"));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("battle_rank"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", info.getContent(ruler.Ranking));
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("battle_rank"));
			writer.WriteLine("<td class='resourceCell'><b>{0}</b>º</td>", (ruler.Rank==-1?"~":ruler.Rank.ToString()));
			writer.WriteLine("</tr>");
			
			writer.WriteLine("</table>");
		}

		#endregion
	
	};
}
