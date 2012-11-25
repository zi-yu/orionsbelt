// created on 10/25/2005 at 10:13 AM

//#define DEBUG_ADD_DUMMY_RULERS

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
using Alnitak.Battle;


namespace Alnitak {

	public class TournamentAdmin : PlanetControl {
	
		#region General
		
		private delegate void StateViewer( HtmlTextWriter writer, Tournament tour );
		private Hashtable States = new Hashtable();
		private ShipSelector createBattle;
		private Button button;
		
		private void InitTournamentViewer()
		{
			States.Add( TournamentState.NotStarted, new StateViewer(this.NotStartedViewer) );
			States.Add( TournamentState.Subscriptions, new StateViewer(this.SubscriptionsViewer) );
			States.Add( TournamentState.Championship, new StateViewer(this.ChampionshipViewer) );
			States.Add( TournamentState.Playoffs, new StateViewer(this.PlayoffsViewer) );
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
		
		protected override void OnInit(EventArgs e) 
		{
			button = new Button();
			button.Text = "Create Tournament";
			createBattle = new ShipSelector();
			createBattle.FleetCreated += new EventHandler(this.FleetCreated);
			Controls.Add(createBattle);
			Controls.Add(button);
			base.OnInit(e);
		}
		
		protected void FleetCreated( object src, EventArgs args )
		{
			Chronos.Core.Fleet fleet = ((FleetEventArgs)args).Fleet;
			Tournament tour = new Tournament(TournamentType, fleet);
			Universe.instance.PersistenceServices.Register(TournamentType, tour);
		}
		
		protected override void OnLoad(EventArgs e) 
		{
			base.OnLoad(e);
			CheckDelete();
			CheckStartChampionship();
			CheckStartPlayoffs();
			CheckManagePlayoffs();
		}
		
		private void CheckDelete()
		{
			if( InvalidTournamentType ) {
				return;
			}
			
			string str = Page.Request.QueryString["delete"];
			if( str != null ) {
				Universe.instance.PersistenceServices.Remove(TournamentType);
			}
		}
		
		private void CheckStartChampionship()
		{
			if( InvalidTournamentType ) {
				return;
			}
			
			string str = Page.Request.QueryString["ToChampionship"];
			Tournament tour = GetTournament();
			if( str != null && tour != null ) {
#if DEBUG && DEBUG_ADD_DUMMY_RULERS
				for( int i = 0; i < 100; ++i ) {
					tour.Register( Universe.instance.CreateRuler("Boing", "alfredo") );
				}			
#endif
				tour.EndSubscriptions();
			}
		}
		
		private void CheckStartPlayoffs()
		{
			if( InvalidTournamentType ) {
				return;
			}
			
			string str = Page.Request.QueryString["ToPlayoffs"];
			Tournament tour = GetTournament();
			if( str != null && tour != null ) {
				tour.EndChampionship();
			}
		}
		
		private void CheckManagePlayoffs()
		{
			if( InvalidTournamentType ) {
				return;
			}
			
			string str = Page.Request.QueryString["AdvancePlayoffs"];
			Tournament tour = GetTournament();
			if( str != null && tour != null ) {
				Playoffs plays = (Playoffs) tour.CurrentPhase;
				plays.Advance();
				return;
			}
			
			str = Page.Request.QueryString["FinishPlayoffs"];
			if( str != null && tour != null ) {
				tour.EndPlayoffs();
			}
		}
		
		protected override void Render( HtmlTextWriter writer ) 
		{
			if( InvalidTournamentType ) {
				WriteIndex(writer);
				return;
			}

			TournamentState state = TournamentState.NotStarted;
			Tournament tour = GetTournament();
			if( tour != null ) {
				state = tour.State;
			}
			
			createBattle.Visible = button.Visible = (state == TournamentState.NotStarted);
			
			InitTournamentViewer();
			
			WriteBasicOptions(writer);
			WriteUnits(writer, tour);
			WriteState(writer, tour);
			
			StateViewer viewer = (StateViewer) States[state];
			if( viewer == null ) {
				writer.WriteLine("Ups... don't know how to handle `{0}'", state);
				return;
			}
			
			viewer(writer, tour);
			base.Render(writer);
		}
		
		#endregion
		
		#region Utilities
		
		private void WriteIndex( HtmlTextWriter writer ) 
		{
			writer.WriteLine("<ul>");
			foreach( string type in BattleInfo.EndBattleTypes ) {
				writer.WriteLine("<li><a href='{0}?t={2}'>{1}</a></li>", 
						OrionGlobals.getSectionBaseUrl("tadmin"),
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
		
		private void WriteBasicOptions( HtmlTextWriter writer ) 
		{
			writer.WriteLine("<h2>Options for {0}</h2>", TournamentType);
			writer.WriteLine("<ul>");
			writer.WriteLine("<li><a href='{0}&delete=1'>Erase this tournament</a></li>", Page.Request.RawUrl);
			writer.WriteLine("</ul>");
		}
		
		private void WriteUnits( HtmlTextWriter writer, Tournament tour ) 
		{
			if( tour == null ) {
				return;
			}
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
			if( tour == null ) {
				return;
			}
			writer.WriteLine("<h2>{0}</b></h2>", CultureModule.getContent("tournament_info"));
			writer.WriteLine("<table class='planetFrame' width='100%'>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>");
			writer.WriteLine("{0}: <b>{1}</b><br/>", CultureModule.getContent("tournament_registered"), tour.Participants);
			writer.WriteLine("{0}: <b>{1}</b>", CultureModule.getContent("tournament_phase"), tour.State);
			writer.WriteLine("</td>");
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");

			writer.WriteLine("</tr>");
			writer.WriteLine("</table>");
		}
		
		#endregion
		
		#region NotStarted
		
		private void NotStartedViewer( HtmlTextWriter writer, Tournament tour )
		{
		}
		
		#endregion
		
		#region Subscriptions
		
		private void SubscriptionsViewer( HtmlTextWriter writer, Tournament tour )
		{
			writer.WriteLine("<h2>Start Group Phase</h2>");
			writer.WriteLine("<a href='{2}&{3}=1'><img src='{0}' /> {1}</a>", OrionGlobals.getCommonImagePath("ok.gif"), "Start Championship", Page.Request.RawUrl, "ToChampionship");	
		}
		
		#endregion
		
		#region Championship
		
		private void ChampionshipViewer( HtmlTextWriter writer, Tournament tour )
		{
			writer.WriteLine("<h2>Start Playoffs Phase</h2>");
			writer.WriteLine("<a href='{2}&{3}=1'><img src='{0}' /> {1}</a>", OrionGlobals.getCommonImagePath("ok.gif"), "Start Playoffs", Page.Request.RawUrl, "ToPlayoffs");	
		}
		
		#endregion
		
		#region Playoffs
		
		private void PlayoffsViewer( HtmlTextWriter writer, Tournament tour )
		{
			writer.WriteLine("<h2>Admin Playoffs Phase</h2>");
			writer.WriteLine("<a href='{2}&{3}=1'><img src='{0}' /> {1}</a><br/>", OrionGlobals.getCommonImagePath("ok.gif"), "Advance Playoffs", Page.Request.RawUrl, "AdvancePlayoffs");
			writer.WriteLine("<a href='{2}&{3}=1'><img src='{0}' /> {1}</a><br/>", OrionGlobals.getCommonImagePath("ok.gif"), "Finish Playoffs", Page.Request.RawUrl, "FinishPlayoffs");	
		}
		
		#endregion
	
	};
	
}
