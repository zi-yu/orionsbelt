// created on 19-01-2005 at 17:55

using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Language;
using Chronos.Core;
using Chronos.Resources;
using Chronos.Actions;
using Chronos.Utils;
using Chronos.Info;

namespace Alnitak {
	
	/// <summary>
	/// Prizes page
	/// </summary>
	public class Prizes : Page {
	
		#region Instance Fields
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		
		#endregion
		
		#region Control Events
		
		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Prize, info.getContent("section_prizes"));
		}
		
		#endregion
		
		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			RenderPrizes(writer, "conquer_prizes", OrionGlobals.Conquer);
			RenderPrizes(writer, "building_prizes", OrionGlobals.Building);
			RenderPrizes(writer, "research_prizes", OrionGlobals.Research);
			RenderHelp(writer);
		}
		
		/// <summary>Pinta um conjunto de prémios</summary>
		private void RenderPrizes( HtmlTextWriter writer, string title, string[] array )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>",
					info.getContent(title)
				);
			
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("prize"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("winner"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("turn_current"));
			writer.WriteLine("</tr>");
			foreach( string prize in array ) {
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resource' valign='top'>{0}</td>", info.getContent(prize));
				PrizeManager prizes = Universe.instance.getPrizeManager(prize);
				if( prizes == null ) {
					writer.WriteLine("<td class='resourceCell'>?</td>");
					writer.WriteLine("<td class='resourceCell'>?</td>");
				} else {
					writer.WriteLine("<td class='resourceCell'>");
					writeWinners(writer, prizes);
					writer.WriteLine("</td>");
					writer.WriteLine("<td class='resourceCell'>");
					writeTurns(writer, prizes);
					writer.WriteLine("</td>");
				}
				writer.WriteLine("</tr>");
			}
			writer.WriteLine("</table>");
		}
		
		/// <summary>Indica os vencedores de um prémio</summary>
		private void writeWinners( HtmlTextWriter writer, PrizeManager prizes )
		{
			if( prizes.Gold != null ) {
				writer.WriteLine(OrionGlobals.getLink(prizes.Gold.Ruler));
			}
			if( prizes.Silver != null ) {
				writer.WriteLine("<br/>{0}", OrionGlobals.getLink(prizes.Silver.Ruler));
			}
			if( prizes.Bronze != null ) {
				writer.WriteLine("<br/>{0}", OrionGlobals.getLink(prizes.Bronze.Ruler));
			}
			if( prizes.Last != null ) {
				writer.WriteLine("<br/>{0}", OrionGlobals.getLink(prizes.Last.Ruler));
			}
		}
		
		/// <summary>Indica os turnos de um prémio</summary>
		private void writeTurns( HtmlTextWriter writer, PrizeManager prizes )
		{
			if( prizes.Gold != null ) {
				writer.WriteLine(prizes.Gold.Turn);
			}
			if( prizes.Silver != null ) {
				writer.WriteLine("<br/>{0}", prizes.Silver.Turn);
			}
			if( prizes.Bronze != null ) {
				writer.WriteLine("<br/>{0}", prizes.Bronze.Turn);
			}
			if( prizes.Last != null ) {
				writer.WriteLine("<br/>{0}", prizes.Last.Turn);
			}
		}
		
		/// <summary>Mostra links de ajuda</summary>
		private void RenderHelp( HtmlTextWriter writer )
		{
			writer.WriteLine("<ul class='help_zone'>");
			writer.WriteLine("<li><a href='{0}'>{1}</a></li>", Wiki.GetUrl("Pr�mios"), info.getContent("wiki_Prizes"));
			writer.WriteLine("<li><a href='{0}'>{1}</a></li>", Wiki.GetUrl("Medalhas"), info.getContent("wiki_Medals"));
			writer.WriteLine("</ul>");
		}
		
		#endregion
		
	};
}
