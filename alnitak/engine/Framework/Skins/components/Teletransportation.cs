using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;
using Chronos.Core;
using Chronos.Resources;
using Chronos.Info.Results;
using System;
using System.IO;

namespace Alnitak {
	
	public class Teletransportation : PlanetControl {
		
		#region Control Fields
		
		protected PlaceHolder intrinsicTeletransport;
		protected PlaceHolder fleetTeletransport;
		protected PlaceHolder noFleets;
		protected DropDownList intrinsicResources;
		protected DropDownList toSend;
		protected DropDownList intrinsicDestiny;
		protected DropDownList fleetDestiny;
		protected Button moveIntrinsic;
		protected Button moveFleet;
		protected System.Web.UI.WebControls.Label energyQuantity;
		protected System.Web.UI.WebControls.Label energyQuantity2;
		protected QueueErrorReport errorReport;
		protected DropDownList planetFleets;
		protected PlanetNavigation planetNavigation;
		
		protected HyperLink teletransportationWiki;
		
		#endregion
		
		#region Control Events
		
		/// <summary>Inicializa o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			if( Page.IsPostBack ) {
				string action = Page.Request.Form["action"];
				if( action != null && action == "intrinsic" ) {
					CheckIntrinsicInput();
				} else {
					CheckFleetInput();
				}
				intrinsicResources.Items.Clear();
				intrinsicDestiny.Items.Clear();
				fleetDestiny.Items.Clear();
				planetFleets.Items.Clear();
			}
			
			Page.RegisterHiddenField("action", "");
			Planet planet = getPlanet();
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.PlanetManagement, string.Format("{0} - {1}",planet.Name, info.getContent("section_tele")));
			
			intrinsicTeletransport.Visible = planet.CanTeletransportIntrinsic;
			fleetTeletransport.Visible = planet.CanTeletransportFleets && planet.Fleets.Count > 1;
			noFleets.Visible = planet.Fleets.Count == 1;
			
			planetNavigation.Player = (Ruler) planet.Owner;
			planetNavigation.Current = planet;
			planetNavigation.Section = "Tele";
			
			teletransportationWiki.NavigateUrl = Wiki.GetUrl("TeleTransporte");
			teletransportationWiki.Text = info.getContent("Wiki_Teletransporte");
			teletransportationWiki.CssClass = "docs";
			
			if( intrinsicTeletransport.Visible ) {
				GetIntrinsic(planet, intrinsicResources);
				intrinsicResources.DataBind();
				intrinsicResources.Attributes.Add("onchange", string.Format("intrinsicChanged('{0}');", intrinsicResources.ClientID) );
				toSend.Attributes.Add("onchange", string.Format("intrinsicChanged('{0}');", intrinsicResources.ClientID) );
				IntrinsicPlanets(intrinsicDestiny, planet.IntrinsicTeletransportationPlanets);
				intrinsicDestiny.DataBind();
				moveIntrinsic.Text = info.getContent("send");
				SetQuantities(toSend);
				moveIntrinsic.Attributes.Add("onclick", "return moveIntrinsic();");
				WriteIntrinsicScripts(planet);
				energyQuantity.Text = planet.Energy.ToString();
			}
			
			if( fleetTeletransport.Visible ) {
				energyQuantity2.Text = planet.Energy.ToString();
				moveFleet.Text = info.getContent("send");
				SetFleetsToMove(planetFleets, planet);
				IntrinsicPlanets(fleetDestiny, planet.FleetTeletransportationPlanets);
				WriteFleetScripts(planet);
				moveFleet.Attributes.Add("onclick", "return moveFleet();");
			}
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Indica os recursos intrinsecos a mostrar</summary>
		private void GetIntrinsic( Planet planet, DropDownList drop )
		{
			drop.Items.Add( new ListItem(string.Empty, string.Empty) );
			
			foreach( ResourceFactory factory in Universe.getFactories("planet", "Intrinsic").Values ) {
				int quantity = planet.getResourceCount("Intrinsic", factory.Name);
				if( quantity > 0 && Resource.IsTeletransportable(factory.create()) ) {
					drop.Items.Add( new ListItem(info.getContent(factory.Name), factory.Name) );		
				}
			}
			
			foreach( ResourceFactory factory in Universe.getFactories("planet", "Rare").Values ) {
				int quantity = planet.getResourceCount("Rare", factory.Name);
				if( quantity > 0 && Resource.IsTeletransportable(factory.create()) ) {
					drop.Items.Add( new ListItem(info.getContent(factory.Name), factory.Name) );		
				}
			} 
		}
		
		/// <summary>Indica todos os planetas que recebem recursos</summary>
		private void IntrinsicPlanets( DropDownList drop, Planet[] planets )
		{
			drop.Items.Add( new ListItem("",""));
			foreach( Planet planet in planets ) {
				drop.Items.Add( new ListItem(planet.ToString(), planet.Id.ToString()) );
			}
		}
		
		/// <summary>Indica quantidades de teletransporte</summary>
		private void SetQuantities( DropDownList drop )
		{
			drop.Items.Clear();
			drop.Items.Add("100");
			drop.Items.Add("500");
			drop.Items.Add("1000");
			drop.Items.Add("5000");
			drop.Items.Add("10000");
			drop.Items.Add("50000");
			drop.Items.Add("100000");
			drop.Items.Add("300000");
			drop.Items.Add("600000");
		}
		
		/// <summary>Verifica se h input de fleets</summary>
		private void CheckFleetInput()
		{
			string fleet = Page.Request.Form["fleetToMove"];
			string id = Page.Request.Form["fleetDestinyPlanet"];
			if( fleet == null || !OrionGlobals.isInt(id) ) {
				return;
			}
			
			Planet planet = getPlanet();
			Planet destiny = getRuler().getPlanet(int.Parse(id));
			
			Result result =  planet.CanMoveFleet(destiny, fleet);
			
			errorReport.ResultSet = result;
			errorReport.Visible = true;
			errorReport.Title = info.getContent("tele_report");
			
			if( result.Ok ) {
				Chronos.Utils.Log.log("##### fleet {1} moved to {0}", planet, fleet);
				planet.MoveFleet( destiny, fleet);
			}
		}
		
		/// <summary>Verifica se h input intrinseco</summary>
		private void CheckIntrinsicInput()
		{
			string quantity = Page.Request.Form["intrinsicQuantity"];
			string res = Page.Request.Form["intrinsicResourceToMove"];
			string id = Page.Request.Form["intrinsicDestinyPlanet"];
			
			if( quantity == null || res == null || id == null ) {
				return;
			}
			
			if( !OrionGlobals.isInt(quantity) || !OrionGlobals.isInt(id) ) {
				return;
			}
			
			int quant = int.Parse(quantity);
			Planet planet = getPlanet();
			Planet destiny = getRuler().getPlanet(int.Parse(id));
			
			Result result =  planet.CanMoveResource(destiny, null, res, quant);
			
			errorReport.ResultSet = result;
			errorReport.Visible = true;
			errorReport.Title = info.getContent("tele_report");

			if( result.Ok ) {
				Chronos.Utils.Log.log("##### {1} of {2} moved to {0}", planet, res, quant);
				planet.MoveResource( destiny, null, res, quant);
			}
		}
		
		/// <summary>Preenche as fleets de um planeta</summary>
		private void SetFleetsToMove(DropDownList drop, Planet planet)
		{
			drop.Items.Add( new ListItem("","") );
			foreach( Chronos.Core.Fleet fleetToMove in planet.Fleets.Values ) {
				if( fleetToMove.IsMoveable ) {
					drop.Items.Add( new ListItem(fleetToMove.Name, fleetToMove.Name) );
				}
			}
			drop.Attributes.Add("onchange", "fleetChanged();");
		}
	
		#endregion
		
		#region JavaScript Related Methods
		
		private string GetQuantities( Planet planet )
		{
			StringWriter writer = new StringWriter();
			
			foreach( ResourceFactory factory in Universe.getFactories("planet", "Intrinsic").Values ) {
				if( Resource.IsTeletransportable(factory.create()) ) {
					writer.WriteLine("iQuantities['{0}'] = {1};", factory.Name, planet.getResourceCount(factory.Category, factory.Name));				
				}
			} 
			
			foreach( ResourceFactory factory in Universe.getFactories("planet", "Rare").Values ) {
				int quantity = planet.getResourceCount(factory.Category, factory.Name);
				if( Resource.IsTeletransportable(factory.create()) && quantity > 0 ) {
					writer.WriteLine("iQuantities['{0}'] = {1};", factory.Name, quantity);		
				}
			} 
			
			return writer.ToString();
		}
		
		private string GetTeletransportationCost( Planet planet ) 
		{
			StringWriter writer = new StringWriter();
			
			foreach( ResourceFactory factory in Universe.getFactories("planet", "Intrinsic").Values ) {
				if( Resource.IsTeletransportable(factory.create()) ) {
					writer.WriteLine("teleCost['{0}'] = {1};", factory.Name, Resource.TeletransportationCost("Intrinsic", factory.Name, 1));				
				}
			} 
			
			foreach( ResourceFactory factory in Universe.getFactories("planet", "Rare").Values ) {
					if( planet.getResourceCount(factory.Category, factory.Name) > 0 ) {
						writer.WriteLine("teleCost['{0}'] = {1};", factory.Name, Resource.TeletransportationCost("Rare", factory.Name, 1));
					}		
			} 
			
			return writer.ToString();
		}
		
		/// <summary>Escreve os scripts necessrios em JS</summary>
		private void WriteIntrinsicScripts(Planet planet)
		{
			string script = @"
<script language='javascript'>
			
var availableEnergy = " + planet.Energy + @";

var iQuantities = new Object();
" + GetQuantities(planet) + @"
			
var teleCost = new Object();
" + GetTeletransportationCost(planet) + @"
			
function intrinsicChanged(id)
{
	var avail = document.getElementById('resourceAvailable');
	var drop = document.getElementById(id);
	var totalCost = document.getElementById('resourceCost');
	pageContent.intrinsicResourceToMove.value = '';
	pageContent.intrinsicQuantity.value = 0;
	pageContent.intrinsicDestinyPlanet.value = 0;
	pageContent.action.value = '';
			
	if( drop.value == '' ) {
		avail.innerHTML = '-';
		totalCost.innerHTML = '-';
		totalCost.className= '';
	} else {
		avail.innerHTML = iQuantities[drop.value];
			
		var toSend = document.getElementById('" + toSend.ClientID + @"');
		var cost = toSend.value * teleCost[drop.value];
		totalCost.innerHTML = cost;
		if( cost < availableEnergy ) {
			totalCost.className= 'green';
		} else {
			totalCost.className= 'red';
		}
	}
}
			
function moveIntrinsic()
{
	var planet = document.getElementById('" + intrinsicDestiny.ClientID + @"');
	var resource = document.getElementById('" + intrinsicResources.ClientID + @"');
			
	if( planet.value == '' || resource.value == '' ) {
		alert('" + info.getContent("tele_invalid_fields") + @"');
		return false;
	}
			
	var toSend = document.getElementById('" + toSend.ClientID + @"');
	var cost = toSend.value * teleCost[resource.value];
	var available = iQuantities[resource.value];
			
	if( toSend.Value > available ) {
		alert('" + info.getContent("tele_no_resource") + @"');
		return false;
	}

	if( cost > availableEnergy ) {
		alert('" + info.getContent("tele_no_energy") + @"');
		return false;
	}
			
	pageContent.intrinsicResourceToMove.value = resource.value;
	pageContent.intrinsicQuantity.value = toSend.value;
	pageContent.intrinsicDestinyPlanet.value = planet.value;
	pageContent.action.value = 'intrinsic';
	
	return true;
}
</script>
			";
			
			Page.RegisterClientScriptBlock("teletransportation", script);
			Page.RegisterHiddenField("intrinsicResourceToMove", "");
			Page.RegisterHiddenField("intrinsicQuantity", "");
			Page.RegisterHiddenField("intrinsicDestinyPlanet", "");
		}
		
		/// <summary>Escreve os scripts necessrios em JS</summary>
		private void WriteFleetScripts(Planet planet)
		{
			StringWriter writer = new StringWriter();
			foreach( Chronos.Core.Fleet fleet in planet.Fleets.Values ) {
				if( fleet.IsMoveable ) {
					writer.WriteLine("fleetsCost['{0}'] = {1};", fleet.Name, planet.FleetMoveCost(fleet));
				}
			}
			
			string script = @"
<script language='javascript'>

var fleetsCost = new Object();
" + writer.ToString() + @"

function fleetChanged()
{
	pageContent.action.value = '';
			
	var fleet = document.getElementById('" + planetFleets.ClientID + @"').value;
	var cost = document.getElementById('fleetCost');
	var fleetCost = fleetsCost[fleet];
			
	if( fleet == '' ){
		cost.innerHTML = '';
		cost.className = '';
	} else {
		cost.innerHTML = fleetCost;
		if( " + planet.Energy + @" > fleetCost ) {
			cost.className = 'green';
		} else {
			cost.className = 'red';
		}
	}
}

function moveFleet()
{
	var fleet = document.getElementById('" + planetFleets.ClientID + @"').value;
	var cost = document.getElementById('fleetCost');
	var fleetCost = fleetsCost[fleet];
	var planet = document.getElementById('" + fleetDestiny.ClientID + @"');
			
	if( planet.value == '' || fleet == '' ) {
		alert('" + info.getContent("tele_invalid_fields") + @"');
		return false;
	}
			
	if( " + planet.Energy + @" < fleetCost ) {
		alert('" + info.getContent("tele_no_energy") + @"');
		return false;
	}
	
	pageContent.fleetToMove.value = fleet;
	pageContent.fleetDestinyPlanet.value = planet.value;
	pageContent.action.value = 'fleet';
			
	return true;
}
</script>
			";
			
			Page.RegisterClientScriptBlock("fleet teletransportation", script);
			Page.RegisterHiddenField("fleetToMove", "");
			Page.RegisterHiddenField("fleetDestinyPlanet", "");
		}
		
		#endregion
	};
	
}

