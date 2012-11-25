using System;
using System.Web;
using System.Web.UI;

using Chronos.Core;
using Language;

namespace Alnitak {
	
	public class Fleet: PlanetControl {

		public static string[] Cost = new string[]{ "gold","mp", "energy" };

		protected Resources resources;
		protected ResourcesList resourcesHelp;
		protected QueueNotifier queue;
		protected QueueErrorReport queueError;
		protected PlanetNavigation planetNavigation;

		protected override void OnLoad(EventArgs e) {
			Ruler ruler = getRuler();
			Planet planet = getPlanet();
			ILanguageInfo info = CultureModule.getLanguage();
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Battle, string.Format("{0} - {1}", planet.Name, info.getContent("section_fleet")));
			
			resources.Manager = queue.Manager = planet;
			
			resources.Category = queue.Category = "Unit";

			resources.QueueError = queueError;

			resources.AskBuildQuantity = true;
			resources.ShowQuantity = false;
			resources.ShowDemolish = false;
			resources.Tooltip = "attributes";
			resources.ShowImagePreview = true;
			resources.Cost = Cost;
			resources.ShowRareResourceCost = true;
			resources.ShowDocumentation = true;
			resources.IncludeOnMouseOver = false;
			resources.CategoryDescription = "general";
			resources.AllowKeywords = false;

			resourcesHelp.Title = info.getContent("planetResources");
			resourcesHelp.Manager = planet;
			resourcesHelp.ShowImages = true;
			resourcesHelp.Category = "Intrinsic";
			resourcesHelp.ShowDocumentation = false;
			resourcesHelp.ShowDocumentation = false;
			resourcesHelp.ResourcesToShow = Cost;

			queue.Title = info.getContent("inProduction");
			queue.EmptyMessage = info.getContent("buildingsQueueEmpty");
			queue.ShowImagePreview = true;

			resources.Title = info.getContent("fleet_buildShip");

			planetNavigation.Player = ruler;
			planetNavigation.Current = planet;
			planetNavigation.Section = "Fleet";
		}
	}
}
