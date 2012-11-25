// created on 12/27/2005 at 6:44 PM

using System;
using System.Web.UI;
using Chronos.Core;
using Chronos.Messaging;

namespace Alnitak {

	public class TopRanks : Control {	
	
		#region Instance Fields
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		
		#endregion

		#region Control Events

		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			OrionGlobals.RegisterRequest(MessageType.Generic, info.getContent("section_topranks"));
		}
		
		#endregion

		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{	
			User[] users = UserUtility.bd.getUsersRanking();
			if( users == null || users.Length == 0 ) {
				writer.WriteLine(info.getContent("no_rank_avalable"));
				return;
			}
		
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", info.getContent("section_topranks"));
			RenderRanks( writer, users );
		}

		private void RenderRanks( HtmlTextWriter writer, User[] users ) 
		{
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.WriteLine("<td class='resourceTitle'>#</td>");
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("stats_rulers"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("battle_rank"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("batalhas"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("score"));
			writer.WriteLine("</tr>");
							
			for( int i = 0; i < users.Length; ++i ) {
				if( users[i] == null ) {
					continue;
				}
				
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resourceCell'>{0}&ordm;</td>", i +1 );

				bool rulerViewing = false;

				// mostrar o nome do ruler
				writer.WriteLine("<td class='resource'>");
				if( rulerViewing ) {
					writer.WriteLine("<u><b>{0}</b></u>", OrionGlobals.getLink(users[i]));
				} else {
					writer.WriteLine(OrionGlobals.getLink(users[i]));
				}
					
				writer.WriteLine("</td>");
				
				writer.WriteLine("<td class='resourceCell'>{0}</td>", users[i].EloRankDescription.ToString() );
				writer.WriteLine("<td class='resourceCell'><span class='green'>{0}</span> / <span class='red'>{1}</span></td>", users[i].Wins, users[i].Losses );	
				writer.WriteLine("<td class='resourceCell'>{0}</td>", users[i].EloRanking );
				
				writer.WriteLine("</tr>");
			}
			writer.WriteLine("</table>");
		}

		#endregion
	
	};
}
