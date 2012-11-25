// created on 9/4/2005 at 7:07 PM

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
using Chronos.Trade;
using System;
using System.Text.RegularExpressions;

namespace Alnitak {

	public class Barracks : PlanetControl {
	
		#region Control Fields
		
		protected PlanetNavigation planetNavigation;
		protected ResourcesList resourcesHelp;
		protected QueueErrorReport queueError;
		protected Travel travelControl;
		protected Resources resources;
		protected QueueNotifier queue;
		protected SabotageList sabotageList;
		protected SabotageQueue sabotageQueue;
		protected HyperLink intrinsicHelp;
		protected HyperLink queueHelp;
		protected HyperLink sabotageHelp;
		
		public static string[] ToShow = new string[]{ "gold","mp", "energy", "housing", "labor","marine", "spy" };
		public static string[] Cost = new string[]{ "gold","mp", "energy", "labor" };
	
		#endregion
	
		#region Control Events
		
		protected override void OnLoad(EventArgs e) 
		{
			Ruler ruler = getRuler();
			Planet planet = getPlanet();
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Battle, string.Format("{0} - {1}", planet.Name, info.getContent("section_barracks")));
			
			resources.Manager = queue.Manager = planet;
			
			resources.Category = queue.Category = "Intrinsic";

			resources.QueueError = queueError;

			resources.AskBuildQuantity = true;
			resources.ShowQuantity = true;
			resources.ShowDemolish = false;
			resources.Cost = Cost;
			resources.ShowDocumentation = true;
			resources.IncludeOnMouseOver = false;
			resources.CategoryDescription = "soldier";

			resourcesHelp.Title = info.getContent("planetResources");
			resourcesHelp.Manager = planet;
			resourcesHelp.ShowImages = true;
			resourcesHelp.Category = "Intrinsic";
			resourcesHelp.ShowRareResources = false;
			resourcesHelp.ShowDocumentation = false;
			resourcesHelp.ShowDocumentation = false;
			resourcesHelp.ResourcesToShow = ToShow;

			queue.Title = info.getContent("inProduction");
			queue.EmptyMessage = info.getContent("buildingsQueueEmpty");

			resources.Title = info.getContent("fleet_buildShip");

			planetNavigation.Player = ruler;
			planetNavigation.Current = planet;
			planetNavigation.Section = "Barracks";
			
			sabotageList.Source = planet;
			sabotageList.Report = queueError;
			sabotageList.TravelControl = travelControl;
			
			sabotageQueue.Source = planet;
			
			// Help
			intrinsicHelp.NavigateUrl = Wiki.GetUrl("Intrinsic", "Intrinsic");
			intrinsicHelp.Text = info.getContent("wiki_Intrinsecos");
			
			sabotageHelp.NavigateUrl = Wiki.GetUrl("Sabotagens");
			sabotageHelp.Text = info.getContent("wiki_Sabotage");
			
			queueHelp.NavigateUrl = Wiki.GetUrl("FilaDeEspera");
			queueHelp.Text = info.getContent("wiki_FilaDeEspera");
		}
		
		#endregion
		
		#region Utilities
		
		
		#endregion
	
	};
	
}
