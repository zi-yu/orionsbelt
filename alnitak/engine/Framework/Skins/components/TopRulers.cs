using System;
using System.Web.UI;
using Chronos.Core;
using Chronos.Messaging;

namespace Alnitak {

	public class TopRulers : PlanetControl {
		
		#region Private Fields

		private bool showOnline = true;
		private bool showPlanets = true;
		private bool showBattles = true;
		private bool showRank = true;
		private bool showScore = true;
		private bool showOnlyTopPlayers = false;

		#endregion

		#region Properties

		public bool ShowOnline {
			get { return showOnline; }
			set { showOnline = value; }
		}

		public bool ShowPlanets {
			get { return showPlanets; }
			set { showPlanets = value; }
		}

		public bool ShowBattles {
			get { return showBattles; }
			set { showBattles = value; }
		}

		public bool ShowRank {
			get { return showRank; }
			set { showRank = value; }
		}

		public bool ShowScore {
			get { return showScore; }
			set { showScore = value; }
		}

		public bool ShowOnlyTopPlayers {
			get { return showOnlyTopPlayers; }
			set { showOnlyTopPlayers = value; }
		}

		#endregion

		#region Control Events

		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			OrionGlobals.RegisterRequest(MessageType.Generic, info.getContent("section_toprulers"));
		}
		
		#endregion

		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			if ( null == Universe.instance.Rank || Universe.instance.Rank.Length == 0) {
				writer.WriteLine(info.getContent("no_rank_avalable"));
				return;
			}
			
			int count = Universe.instance.Rank.Length;
			if ( count > 30 ) {
				count = 30;
			}

			Ruler current = getRulerSafe();
			string onlineImage = OrionGlobals.getCommonImagePath("online.gif");
			string offlineImage = OrionGlobals.getCommonImagePath("offline.gif");

			writer.WriteLine("<div class='planetInfoZoneTitle'><b>" + info.getContent("top_3rulers") + "</b></div>");

			int top = 3;
			if( top > Universe.instance.Rank.Length ) {
				top = Universe.instance.Rank.Length;
			}

			RenderRulers( writer, 0, top, current, onlineImage, offlineImage, true );

			if( !ShowOnlyTopPlayers ) {
				writer.WriteLine("<br/>");

				if( Universe.instance.Rank.Length > 3 ) {
					writer.WriteLine("<div class='planetInfoZoneTitle'><b>" + info.getContent("top_rulers") + "</b></div>");
					RenderRulers( writer, 3, count, current, onlineImage, offlineImage, false );
				}
				
				if( current != null && current.Rank > count ) {
					writer.WriteLine("<div class='planetInfoZoneTitle'><b>" + string.Format(info.getContent("top_rulers_focus"), current.Name) + "</b></div>");
					int start = current.Rank - 3;
					if( start <= count ) {
						start = count + 1;
					}
					int end = current.Rank + 3;
					if( end > Universe.instance.Rank.Length ) {
						end = Universe.instance.Rank.Length;
					}
					RenderRulers( writer, start-1, end, current, onlineImage, offlineImage, false );
				}
			}
		}

		private void RenderRulers( HtmlTextWriter writer, int start, int count, Ruler current, string onlineImage, string offlineImage, bool pod ) {
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.WriteLine("<td class='resourceTitle'>#</td>");
			
			if( pod ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("profile_avatarText"));
			}
			
			writer.WriteLine("<td class='resourceTitle'>Ruler</td>");
			
			if( ShowOnline ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("online"));
			}
			if( ShowPlanets) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("planetas"));
			}

			if( ShowBattles ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("batalhas"));
			}
				
			if( ShowRank ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("battle_rank"));
			}
				
			if( ShowScore ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("score"));
			}

			writer.WriteLine("</tr>");
	
			for( int i = start; i < count; ++i ) {
                Ruler ruler = Universe.instance.Rank[i];
				
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resourceCell'>{0}&ordm;</td>", ruler.Rank );

				if( pod ) {
					writer.WriteLine("<td class='resourceCell'><img class='avatar' src='{0}'/></td>", UserUtility.bd.getAvatar( ruler.Id ) );
				}

				bool rulerViewing = (current != null && current.Id == ruler.Id);

				writer.WriteLine("<td class='resource'>");

				// mostrar a imagem de estado de ranking
				if( ruler.Rank == ruler.LastRank || ruler.LastRank == -1 ) {
					writer.WriteLine("<img src='{0}' /> ",OrionGlobals.getCommonImagePath("equal.gif"));
				} else {
					if( ruler.Rank > ruler.LastRank ) {
						writer.WriteLine("<img src='{0}' /> ",OrionGlobals.getCommonImagePath("down.gif"));
					} else {
						writer.WriteLine("<img src='{0}' /> ",OrionGlobals.getCommonImagePath("up.gif"));
					}
				}

				// mostrar o nome do ruler
				if( rulerViewing ) {
					writer.WriteLine("<b>{0}</b>", OrionGlobals.getLink(ruler));
				} else {
					writer.WriteLine(OrionGlobals.getLink(ruler));
				}
					
				writer.WriteLine("</td>");
					
				// indicar se o utilizador está online
				if( ShowOnline ) {
					if( OrionGlobals.isUserOnline(ruler.ForeignId) ) {
						writer.WriteLine("<td class='resourceCell'><img src='{0}' /></td>", onlineImage);
					} else {
						writer.WriteLine("<td class='resourceCell'><img src='{0}' /></td>", offlineImage);
					}
				}
				
				if( ShowPlanets ) {
					writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Planets.Length );
				}
				
				if( ShowOnline ) {
					writer.WriteLine("<td class='resourceCell'><span class='green'>{0}</span>/<span class='orange'>{1}</span>/<span class='red'>{2}</span></td>", ruler.Victories, ruler.Draws, ruler.Defeats );
				}

				if( ShowRank ) {
					writer.WriteLine("<td class='resourceCell'>{0}</td>", info.getContent(ruler.Ranking) );
				}

				if( ShowScore ) {
					writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.format(ruler.getResourceCount("score")) );
				}

				writer.WriteLine("</tr>");
			}
			writer.WriteLine("</table>");
		}

		#endregion
	
	};
}
