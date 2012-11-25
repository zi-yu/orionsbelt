// created on 9/10/2004 at 3:35 PM

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

	public class ShowPlanets : PlanetControl {

		protected Ruler ruler = null;
		protected Repeater planets;
			
		#region All Planets Utility Methods
	
		/// <summary>Preenche os controlos relativos a um recurso</summary>
		private void fillResourceType( string resource, Control control, Chronos.Core.Planet planet)
		{
			QueueItem item = planet.current(resource);
		
			Label type = (Label) control.FindControl("resource" + resource);
			if( type != null ) {
				if( item != null ) {
					type.EnableViewState = false;
					if( item == null ) {
						type.Text = "-";
					} else {
						string resType = info.getContent(item.FactoryName);
						type.Text = resType;
					}
				} else {
					type.Text = "-";
				}
			}
			
			Label queueCount = (Label) control.FindControl("queueCount" + resource);
			if( queueCount != null ) {
				queueCount.EnableViewState = false;
				queueCount.Text = planet.queueCount(resource).ToString();
			}
			
			Label quantity = (Label) control.FindControl("quantity" + resource);
			if( quantity != null ) {
				if( item != null ) {
					quantity.EnableViewState = false;
					quantity.Text = item.Quantity.ToString();
				} else {
					quantity.Text = "-";
				}
			}
			
			Label toGo = (Label) control.FindControl("toGo" + resource);
			if( toGo != null ) {
				if( item != null ) {
					toGo.EnableViewState = false;
					toGo.Text = "+ " + item.RemainingTurns;
				} else {
					toGo.Text = "-";
				}
			}
		}
	
		#endregion
			
		#region Control Events
			
		/// <summary>Pinta o controlo</summary>
		protected override void OnLoad(EventArgs e) {
			if( planets != null ) {
				ruler = getRuler();

				planets.ItemDataBound += new RepeaterItemEventHandler(planets_ItemDataBound);
				planets.DataSource = ruler.Planets;
				planets.DataBind();
			}
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.PlanetManagement, info.getContent("section_planets"));

			base.OnLoad (e);
		}
		
		private void planets_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item ) {
				
				Control planetSkin = e.Item;
				Chronos.Core.Planet planet = (Chronos.Core.Planet)e.Item.DataItem;

				Image img = (Image) planetSkin.FindControl("img");
				if( img != null ) {
					img.ImageUrl = string.Format("{0}planets/{1}.jpg",OrionGlobals.getCommonImagePath(""),planet.Info.Id);
				}
				
				HyperLink planetLink = (HyperLink) planetSkin.FindControl("planetLink");
				if( planetLink != null ) {
					//planetLink.NavigateUrl = OrionGlobals.resolveBase("ruler/planets/planet/default.aspx?id=" + planet.Id);
					planetLink.NavigateUrl = OrionGlobals.getSectionUrl("planet") + "default.aspx?id=" + planet.Id;
				}
				
				HyperLink buildingsLink = (HyperLink) planetSkin.FindControl("buildingsLink");
				if( buildingsLink != null ) {
					buildingsLink.NavigateUrl = OrionGlobals.resolveBase("ruler/planets/planet/buildings.aspx?id=" + planet.Id);
					buildingsLink.Text = info.getContent("Building");
				}
				
				Label name = (Label) planetSkin.FindControl("name");
				if( name != null ) {
					name.Text = planet.Name;
				}
				
				Label coordinate = (Label) planetSkin.FindControl("coordinate");
				if( coordinate != null ) {
					coordinate.Text = planet.Coordinate.ToString();
				}
				
				Control resources = (Control) planetSkin.FindControl("resourcesPanel");
				if( resources != null ) {
					resources.Controls.Add( new ShowPlanetResources(planet) );
				}
				
				fillResourceType("Building", planetSkin, planet);
			}
		}

		#endregion
	};
}
