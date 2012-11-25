// created on 10/25/2005 at 10:13 AM

using System.Collections;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Messaging;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Utils;
using Chronos.Battle;
using Chronos.Tournaments;
using System;

namespace Alnitak {

	public class TournamentViewer : PlanetControl {
	
		#region General
		
		private delegate void StateViewer( HtmlTextWriter writer, Tournament tour );
		private Hashtable States = new Hashtable();
		
		private void InitTournamentViewer()
		{
			States.Add( TournamentState.Subscriptions, new StateViewer(this.SubscriptionsViewer) );
			States.Add( TournamentState.Championship, new StateViewer(this.ChampionshipViewer) );
			States.Add( TournamentState.Playoffs, new StateViewer(this.PlayoffsViewer) );
			States.Add( TournamentState.Finished, new StateViewer(this.FinishedViewer) );
		}

		#endregion
		
		#region Control Properties
			
		private string TournamentType {
			get {
				string str = Page.Request.QueryString["t"];
				if( str == null ) {
					return string.Empty;
				}
				return str;
			}
		}
		
		private bool InvalidTournamentType {
			get {
				string type = TournamentType;
				if( type == null ) {
					return true;
				}
				
				foreach( string str in BattleInfo.EndBattleTypes ) {
					if( str == type ) {
						return false;
					}
				}
				
				return true;
			}
		}

		#endregion
	
		#region Control Events
		
		protected override void OnLoad(EventArgs e) 
		{
			base.OnLoad(e);
			
			Tournament tour = GetTournament();
			if( tour == null && !InvalidTournamentType ) {
				Information.AddInformation(CultureModule.getContent("tournament_notavailable"));
				return;
			}
			
			CheckRegister(tour);
			CheckRemove(tour);
		}
		
		private void CheckRegister(Tournament tour)
		{
			string str = Page.Request.QueryString["register"];
			if( str == null ) {
				return;
			}
			
			if( tour.Register(getRuler()) ) {
				Information.AddInformation(CultureModule.getContent("tournament_ruler_registered"));
				Sort( tour.Registered );
			} else {
				Information.AddError( CultureModule.getContent("tournament_ruler_already_registered") );
			}
		}
		
		private void CheckRemove(Tournament tour)
		{
			try {
				string str = Page.Request.QueryString["remove"];
				if( str == null ) {
					return;
				}
				
				int uid = int.Parse(str); 
				
				if( Page.User.IsInRole("admin") ) {
					Ruler toRemove = null;
					foreach( Ruler ruler in tour.Registered ) {
						if( ruler.ForeignId == uid ) {
							toRemove = ruler;
						}
					}
					if( toRemove != null ) {
						tour.Registered.Remove(toRemove);
					}
				}
			} catch { 
			}
		}
		
		protected override void Render( HtmlTextWriter writer ) 
		{
			if( InvalidTournamentType ) {
				WriteIndex(writer);
				return;
			}
			
			Tournament tour = GetTournament();
			if( tour == null ) {
				return;
			}
			
			InitTournamentViewer();
			
			WriteUnits(writer, tour);
			WriteState(writer, tour);
			
			StateViewer viewer = (StateViewer) States[tour.State];
			if( viewer == null ) {
				writer.WriteLine("Ups... don't know how to handle `{0}'", tour.State);
				return;
			}
			
			viewer(writer, tour);
		}
		
		#endregion
		
		#region Utilities
		
		private void WriteIndex( HtmlTextWriter writer ) 
		{
			writer.WriteLine("<ul>");
			foreach( string type in BattleInfo.EndBattleTypes ) {
				writer.WriteLine("<li><a href='{0}?t={2}'>{1}</a></li>", 
						OrionGlobals.getSectionBaseUrl("tournament"),
						CultureModule.getContent(type),
						type
					);
			}
			writer.WriteLine("</ul>");
		}
		
		private Tournament GetTournament()
		{
			Tournament tour = (Tournament) Universe.instance.PersistenceServices.GetState(TournamentType);
			return tour;
		}
		
		private Tournament GenerateSampleTournament()
		{
			Chronos.Core.Fleet fleet = new Chronos.Core.Fleet("TournamentFleet", Coordinate.First, null, false);
			fleet.addShip("Rain", 100 );
			return new Tournament(TournamentType, fleet);
		}
		
		private void WriteUnits( HtmlTextWriter writer, Tournament tour ) 
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", CultureModule.getContent("tournament_units"));
			writer.WriteLine("<table class='planetFrame' width='100%'>");
			writer.WriteLine("<tr>");
			foreach( string ship in tour.BaseFleet.Ships.Keys ) {
				writer.WriteLine("<td class='resourceCell'><a href='{2}' alt='{3}' title='{3}'><img src='{0}units/{1}_preview.gif' class='unit_small_preview' /></a></td>", OrionGlobals.getCommonImagePath(), ship, Wiki.GetUrl("Unit", ship), CultureModule.getContent(ship));
			}
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			foreach( object quantity in tour.BaseFleet.Ships.Values ) {
				writer.WriteLine("<td class='resourceCell'>{0}</td>", quantity);
			}
			writer.WriteLine("</tr>");
			writer.WriteLine("</table>");
		}
		
		private void WriteState( HtmlTextWriter writer, Tournament tour ) 
		{
			//writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", CultureModule.getContent("tournament_info"));
			writer.WriteLine("<h2>{0}</b></h2>", CultureModule.getContent("tournament_info"));
			writer.WriteLine("<table class='planetFrame' width='100%'>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>");
			writer.WriteLine("{0}: <b>{1}</b><br/>", CultureModule.getContent("tournament_registered"), tour.Participants);
			if( tour.State == TournamentState.Championship ) {
				Championship champ = (Championship) tour.CurrentPhase;
				writer.WriteLine("{0}: <b>{1}</b><br/>", CultureModule.getContent("tournament_groups"), champ.Groups.Length);	
			}
			writer.WriteLine("{0}: <b>{1}</b>", CultureModule.getContent("tournament_phase"), CultureModule.getContent(tour.State.ToString()));
			writer.WriteLine("</td>");
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");

			writer.WriteLine("</tr>");
			writer.WriteLine("</table>");
		}
		
		private void Sort( ArrayList registered )
		{
			registered.Sort( new RankingComparer() );
		}
		
		#endregion
		
		#region Subscriptions
		
		private void SubscriptionsViewer( HtmlTextWriter writer, Tournament tour )
		{
			if( Page.User.IsInRole("ruler")) {
				string register = "&register=1";
				if( Page.Request.RawUrl.IndexOf("&register") >= 0 ) {
					register = string.Empty;
				}
				writer.WriteLine("<a href='{2}{3}'><img src='{0}' /> {1}</a>", OrionGlobals.getCommonImagePath("ok.gif"), CultureModule.getContent("tournament_register"), Page.Request.RawUrl, register);
			}
			
#if DEBUG
			for( int i = 0; i < 100; ++i ) { 
				tour.Register( Universe.instance.CreateRuler("Zen " +i, "Zen"+i) );
			}
#endif			
			
			WriteRegistered(writer, tour);
		}
		
		private void WriteRegistered( HtmlTextWriter writer, Tournament tour ) 
		{
			writer.WriteLine("<h2>{0}</h2>", CultureModule.getContent("tournament_registered"));
			writer.WriteLine("<ul class='registered'>");
			
			int bags = Championship.GetNumberOfGroups(tour.Registered.Count);
			int count = -1;
			int currBag = 1;
			
			writer.WriteLine("<li class='title'>{0} {1}</li>", CultureModule.getContent("tournament_bag"), currBag++, bags);
			
			foreach( Ruler ruler in tour.Registered ) {
				if( ++count == bags ) {
					count = 0;
					writer.WriteLine("<li class='title'>{0} {1}</li>", CultureModule.getContent("tournament_bag"), currBag++, bags);
				}
				writer.Write("<li>{0}", OrionGlobals.getLink(ruler) );
				if( Page.User.IsInRole("admin") ) {
					writer.Write("<a href='{0}&remove={1}'><img src='{2}' /></a>", Page.Request.RawUrl, ruler.ForeignId, OrionGlobals.getCommonImagePath("remove.gif"));
				}
				writer.WriteLine("</li>");
			}
			writer.WriteLine("</ul>");
		}
		
		#endregion
		
		#region Championship
		
		private void ChampionshipViewer( HtmlTextWriter writer, Tournament tour )
		{
			writer.WriteLine("<h2>{0}</h2>", CultureModule.getContent("tournament_groups"));
			Championship champ = (Championship) tour.CurrentPhase;
			
			string groupId = Page.Request.QueryString["group"];
			if( groupId == null ) {
				foreach( Group group in champ.Groups ) {
					WriteGroup(writer, group, true);
				}
				WriteRoaster(writer, champ);
			} else {
				Group selected = champ.Groups[int.Parse(groupId) - 1];
				WriteGroup(writer, selected, false);
				WriteMatches(writer, selected);
			}
		}
		
		private void WriteRoaster( HtmlTextWriter writer, Championship champ )
		{
			writer.WriteLine("<h2>Playoffs Preview</h2>");
			ArrayList list = champ.GetWinners();
			
			writer.WriteLine("<ul id='PlayoffsPreview'>");
			for( int i = 0; i < list.Count; i += 2 ) {
				Ruler one = (Ruler) list[i];
				Ruler two = (Ruler) list[i+1];
				
				writer.WriteLine("<li>{0} <span class='green'>vs</span> {1}</li>",
						OrionGlobals.getLink(one),
						OrionGlobals.getLink(two)
				);
			}
			writer.WriteLine("</ul>");
		}
		
		private void WriteGroup( HtmlTextWriter writer, Group group, bool link )
		{
			if( link ) {
				writer.WriteLine("<div class='planetInfoZoneTitle'><b>Group {0}</b> <a href='{1}&group={0}'><img src='{2}' /></a></div>", group.Id, Page.Request.RawUrl, OrionGlobals.getCommonImagePath("Filter.gif"));
			} else {
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>Group {0}</b></div>", group.Id);
			}
			writer.WriteLine("<table class='planetFrame' width='100%'>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resourceTitle'>#</td>");
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("section_ruler"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("batalhas"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("tournament_group_results"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("score"));
			writer.WriteLine("</tr>");
			
			int pos = 0;
			foreach( Classification classif in group.Registered ) {
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resourceCell'>{0} &ordm;</td>", ++pos);
				writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.getLink(classif.Player));
				writer.WriteLine("<td class='resourceCell'>{0}</td>", classif.Games);
				writer.WriteLine("<td class='resourceCell'><span class='green'>{0}</span>/<span class='orange'>{1}</span>/<span class='red'>{2}</span></td>", classif.Wins, classif.Draws, classif.Defeats);
				writer.WriteLine("<td class='resourceCell'>{0}</td>", classif.Points);
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WriteMatches( HtmlTextWriter writer, Group group )
		{
			writer.WriteLine("<h2>Group {0} - {1}</h2>", group.Id, CultureModule.getContent("tournament_matches"));
			WriteMatches(writer, group.Matches.Values);
		}
		
		private void WriteMatches( HtmlTextWriter writer, ICollection matches )
		{
			writer.WriteLine("<table class='planetFrame' width='100%'>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("tournament_matches"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("section_ruler"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("score"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("score"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("section_ruler"));
			
			writer.WriteLine("</tr>");
						
			foreach( Match match in matches ) {
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");

				if( match.Result == BattleResult.None ) {
					writer.WriteLine("<td class='resourceCell'><a href='{1}ruler/battle/battle.aspx?id={2}&rulerid={3}'><img src='{0}' /></a></td>", OrionGlobals.getCommonImagePath("Filter.gif"), OrionGlobals.AppPath, match.BattleId,match.NumberOne.Id);
				} else {
					writer.WriteLine("<td class='resourceCell'>-</td>");
				}
				
				writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.getLink(match.NumberOne));
				
				string css = "resourceCostCellSucceeded";
				if( match.Result != BattleResult.NumberOneVictory ) {
					css = "resourceCostCellFailed";
				}
				writer.WriteLine("<td class='{1}'>{0}</td>", match.NumberOnePoints, css);
					
				css = "resourceCostCellSucceeded";
				if( match.Result != BattleResult.NumberTwoVictory ) {
					css = "resourceCostCellFailed";
				}
				writer.WriteLine("<td class='{1}'>{0}</td>", match.NumberTwoPoints, css);
				
				writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.getLink(match.NumberTwo));

				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		#endregion
		
		#region Playoffs
		
		private void PlayoffsViewer( HtmlTextWriter writer, Tournament tour )
		{
			writer.WriteLine("<h2>{0}</h2>", CultureModule.getContent("tournament_playoffs"));
			Playoffs plays = (Playoffs) tour.CurrentPhase;
			
			if( plays.Lucky != null ) {
				writer.WriteLine("<p>{0}: {1}</p>", CultureModule.getContent("tournament_lucky"), OrionGlobals.getLink(plays.Lucky));
			}
			
			WriteMatches(writer, plays.Matches);
			
		}
		
		#endregion
		
		#region Finished
		
		private void FinishedViewer( HtmlTextWriter writer, Tournament tour )
		{
			writer.WriteLine("<h2>{0}</h2>", CultureModule.getContent("tournament_finished"));
			Playoffs plays = (Playoffs) tour.CurrentPhase;
			
			
			
		}
		
		#endregion

	};
	
}
