// created on 9/14/2004 at 11:14 AM

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Core;

namespace Alnitak {

	public class ManageBuildings : PlanetControl {
	
		#region Control Events
		
		/// <summary>Pinta o controlo</summary>
		protected override void OnPreRender( EventArgs args )
		{
			Ruler ruler = getRuler();
			
			Chronos.Core.Planet planet = getPlanet();
			
			if( planet == null ) {
				writeErrorResponse();
				return;
			}
			
			writeControl(ruler, planet);
			
			base.OnPreRender(args);
		}
		
		#endregion
		
		#region Control Rendering
		
		/// <summary>Pinta o Controlo</summary>
		protected void writeControl(Chronos.Core.Ruler ruler, Chronos.Core.Planet planet)
		{
			//MasterSkinInfo masterSkinInfo = (MasterSkinInfo)Context.Items["MasterSkinInfo"];
			string control = OrionGlobals.AppPath + "skins/planetaria/controls/ManageBuildings.ascx";
			Control planetSkin = Page.LoadControl(control);
			
			ResourcesList resources = (ResourcesList) planetSkin.FindControl("resourcesList");
			if( resources != null ) {
				resources.Manager = planet;
				resources.Title = info.getContent("planetResources");
				resources.Category = "Intrinsic";
				resources.ShowImages = true;
				resources.ShowDocumentation = false;
				resources.ResourcesToShow = new string[] {
					"gold", "mp", "food", "labor", "housing", "energy", "culture", "polution"
				};
				resources.ShowSpace = true;
			}
			
			QueueNotifier queue = (QueueNotifier)planetSkin.FindControl("queue");
			if( queue != null ) {
				queue.Manager = planet;
				queue.Category = "Building";
				queue.Title = info.getContent("inProduction");
				queue.EmptyMessage = info.getContent("buildingsQueueEmpty");
			}

			PlanetNavigation nav = (PlanetNavigation) planetSkin.FindControl("planetNavigation");
			if ( null != nav ) {
				nav.Player = ruler;
				nav.Current = planet;
				nav.Section = "Buildings";
			}
			
			writeBuildingList(planet, planetSkin, "general", true);
			writeBuildingList(planet, planetSkin, "upgrade", false);
			
			HyperLink intrinsicHelp = (HyperLink) planetSkin.FindControl("intrinsicHelp");
			intrinsicHelp.NavigateUrl = Wiki.GetUrl("Intrinsic", "Intrinsic");
			intrinsicHelp.Text = info.getContent("wiki_Intrinsecos");
			
			HyperLink buildingHelp = (HyperLink) planetSkin.FindControl("buildingHelp");
			buildingHelp.NavigateUrl = Wiki.GetUrl("Building", "Building");
			buildingHelp.Text = info.getContent("wiki_Edificios");
			
			HyperLink queueHelp = (HyperLink) planetSkin.FindControl("queueHelp");
			queueHelp.NavigateUrl = Wiki.GetUrl("FilaDeEspera");
			queueHelp.Text = info.getContent("wiki_FilaDeEspera");
			
			Controls.Add(planetSkin);
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.PlanetManagement, string.Format("{1} - {0}",info.getContent("section_buildings"), planet.Name));
		}
		
		/// <summary>Escreve a lista de edificios</summary>
		private void writeBuildingList( Chronos.Core.Planet planet, Control planetSkin, string cat, bool keys )
		{
			Resources resources = (Resources) planetSkin.FindControl(cat);
			resources.Manager = planet;
			resources.ShowSpaceCost = true;
			resources.ShowDocumentation = true;
			resources.AllowKeywords = keys;
			resources.Tooltip = "modifiers";
			resources.Title = string.Format("<b>{0}</b> - {1}", info.getContent("recursos"), info.getContent(cat));
			resources.ShowDuration = true;
			resources.CategoryDescription = cat;
			resources.ShowDemolish = keys;
			
			QueueErrorReport queueError = (QueueErrorReport)planetSkin.FindControl("queueError");
			resources.QueueError = queueError;
		}
		
		#endregion
	
	};
	
}
