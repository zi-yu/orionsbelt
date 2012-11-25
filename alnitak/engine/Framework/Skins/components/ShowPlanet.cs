// created on 9/13/2004 at 2:32 PM

using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Resources;
using System;

namespace Alnitak {

	public class ShowPlanet : PlanetControl {
		
		#region Control Fields
		
		protected string controlToLoad = "ShowPlanetOverview.ascx";
		
		#endregion
	
		#region Control Events
			
		/// <summary>Pinta o controlo</summary>
		protected override void OnPreRender( EventArgs args )
		{
			Chronos.Core.Planet planet = getPlanet();
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.PlanetManagement, string.Format("{1} - {0}",info.getContent("section_planet"), planet.Name));
			
			if( planet == null ) {
				writeErrorResponse();
				return;
			}

			writePlanet(planet);
		}
		
		#endregion
		
		#region Control Rendering Methods
		
		/// <summary>Escreve um planeta</summary>
		private void writePlanet( Chronos.Core.Planet planet )
		{
			MasterSkinInfo masterSkinInfo = (MasterSkinInfo)Context.Items["MasterSkinInfo"];
			string control = OrionGlobals.AppPath + masterSkinInfo.masterSkinName + "/controls/" + controlToLoad;
			
			Control planetPage = Page.LoadControl(control);
			
			Image img = (Image) planetPage.FindControl("img");
			if( img != null ) {
				img.ImageUrl = OrionGlobals.getCommonImagePath("planets/" + planet.Info.Id + ".jpg");
				img.EnableViewState = false;
			}
			
			ResourcesList resources = (ResourcesList)planetPage.FindControl("resourcesList");
			if( resources != null ) {
				resources.Manager = planet;
				resources.Title = info.getContent("planetResources");
				resources.Category = "Intrinsic";
				resources.ShowImages = true;
				resources.ShowOnlyQuantity = false;
				resources.ShowZeroQuantity = true;
				resources.ShowDocumentation = false;
			}
			
			writeGeneral(planet, planetPage);
			
			Controls.Add(planetPage);
		}
		
		/// <summary>Escreve as informações gerais de um planeta</summary>
		private void writeGeneral( Chronos.Core.Planet planet, Control control )
		{
			fillLabel(control, "name", planet.Name);
			fillLabel(control, "coordinate", planet.Coordinate.ToString() );
			fillLabel(control, "diameter", planet.Info.Diameter.ToString() );
			fillLabel(control, "mass", planet.Info.Mass.ToString() );
			fillLabel(control, "temperature", planet.Info.Temperature.ToString() );
			fillLabel(control, "escape", planet.Info.EscapeVelocity.ToString() );
			fillLabel(control, "terrain", info.getContent(planet.Info.Terrain.Description) );
		
			fillPanel(control, "mineral", (int)planet.ModifiersRatio["mp"]);
			fillPanel(control, "food", (int)planet.ModifiersRatio["food"]);
			fillPanel(control, "gold", (int)planet.ModifiersRatio["gold"]);
			fillPanel(control, "energy", (int)planet.ModifiersRatio["energy"]);
			fillPanel(control, "groundSpace", planet.getResourceCount("Intrinsic", "groundSpace") );
			fillPanel(control, "waterSpace", planet.getResourceCount("Intrinsic", "waterSpace") );
			fillPanel(control, "orbitSpace", planet.getResourceCount("Intrinsic", "orbitSpace") );
		}
		
		/// <summary>Preenche um Panel</summary>
		private void fillPanel( Control container, string label, int val )
		{
			Panel panel = (Panel) container.FindControl(label);
			if( panel != null ) {
				panel.EnableViewState = false;
				panel.Width = new Unit(val, UnitType.Percentage);
				panel.Controls.Add( new LiteralControl(val.ToString() + "%") );
			}
		}
		
		/// <summary>Inicializa uma Label</summary>
		private void fillLabel( Control container, string label, string text )
		{
			Label control = (Label) container.FindControl(label);
			
			if( control != null ) {
				control.EnableViewState = false;
				control.Text = text;
			}
		}
		
		#endregion
	
	};
	
}
