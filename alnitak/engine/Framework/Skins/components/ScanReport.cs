using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;
using Chronos.Core;
using Chronos.Info;
using System;

namespace Alnitak {
	
	public class ScanReportControl : PlanetControl {
		
		#region Control Fields
		
		protected Image img;
		protected System.Web.UI.WebControls.Label name;
		protected System.Web.UI.WebControls.Label ruler;
		protected System.Web.UI.WebControls.Label coordinate;
		protected System.Web.UI.WebControls.Label diameter;
		protected System.Web.UI.WebControls.Label mass;
		protected System.Web.UI.WebControls.Label temperature;
		protected System.Web.UI.WebControls.Label escape;
		protected System.Web.UI.WebControls.Label terrain;
		protected System.Web.UI.WebControls.Label cultureValue;
		protected System.Web.UI.WebControls.Label travelTime;
		
		protected PlaceHolder level2;
		protected Panel mineral;
		protected Panel food;
		protected Panel gold;
		protected Panel energy;
		protected Panel groundSpace;
		protected Panel waterSpace;
		protected Panel orbitSpace;
		
		protected PlaceHolder level3;
		protected System.Web.UI.WebControls.Label fleetNumber;
		protected System.Web.UI.WebControls.Label shipsNumber;
		protected Image hasStarPort;
		protected Image hasCommsSatellite;
		protected Image hasGate;
		protected Image hasStarGate;
		protected Image inBattle;
		protected Image hasHospital;
		protected Image hasLandReclamation;
		protected Image hasMineralExtractor;
		protected Image hasSpa;
		protected Image hasStockMarkets;
		protected Image hasWaterReclamation;
		protected Image hasTurret;
		protected Image hasIonCannon;
		
		protected HyperLink scanWiki;
		
		#endregion
		
		#region Control Events
		
		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			try {
				
				Ruler scanner = getRuler();
				Scan scan = ScanUtility.Persistence.GetScan(ScanId);
				if( scan == null ) {
					throw new Exception("No scan with id '"+ScanId+"' found");
				}
				
				if( !scan.Success ) {
					throw new Exception("Trying to access an unsucceceful scan");
				}
				
				Planet source = scanner.getPlanet(scan.SourcePlanetId);
				if( source == null ) {
					throw new Exception("Ruler '"+scanner.Id+"' don't own planet '"+scan.SourcePlanetId+"");
				}
				
				Planet planet = scan.TargetPlanet;
				
				OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Scan, string.Format("{0} - {1}",info.getContent("section_scanreport"), planet.Coordinate));
				scanWiki.NavigateUrl = Wiki.GetUrl("Scan");
				scanWiki.Text = info.getContent("Wiki_Scan");
				scanWiki.CssClass = "docs";
				
				img.ImageUrl = OrionGlobals.getCommonImagePath("planets/"+ planet.Info.Id + ".jpg");

				img.EnableViewState = false;
				
				if( scan.TargetPlanetOwner != -1 ) {
					name.Text = planet.Name;
					ruler.Text = OrionGlobals.getLink( Universe.instance.getRuler(scan.TargetPlanetOwner));
				} else {
					name.Text = "?";
					ruler.Text = info.getContent("scan_inhabited");
				}
				coordinate.Text = planet.Coordinate.ToString();
				diameter.Text = planet.Info.Diameter.ToString();
				mass.Text = planet.Info.Mass.ToString();
				temperature.Text = planet.Info.Temperature;
				escape.Text = planet.Info.EscapeVelocity.ToString();
				terrain.Text = info.getContent(planet.Info.Terrain.Description);
				cultureValue.Text = scan.Culture.ToString();
				travelTime.Text = Chronos.Core.Fleet.TravelTime(source.Coordinate, scan.Target).ToString();
				
				inBattle.ImageUrl = OrionGlobals.YesNoImage(scan.InBattle);
				
				if( scan.ScanLevel == 1 ) {
					return;
				}
				
				level2.Visible = true;
				fillPanel( mineral, "mineral", planet.Info.MPRatio );
				fillPanel( food, "food", planet.Info.FoodRatio);
				fillPanel( gold, "gold", planet.Info.GoldRatio);
				fillPanel( energy, "energy", planet.Info.EnergyRatio);
				fillPanel( groundSpace, "groundSpace", planet.Info.GroundSpace);
				fillPanel( waterSpace, "waterSpace", planet.Info.WaterSpace);
				fillPanel( orbitSpace, "orbitSpace", planet.Info.OrbitSpace);
				
				if( scan.ScanLevel == 2 ) {
					return;
				}
				
				level3.Visible = true;
				fleetNumber.Text = scan.NumberOfFleets.ToString();
				shipsNumber.Text = scan.TotalShips.ToString();
				hasStarPort.ImageUrl = OrionGlobals.YesNoImage(scan.HasStarPort);
				hasCommsSatellite.ImageUrl = OrionGlobals.YesNoImage(scan.HasCommsSatellite);
				hasGate.ImageUrl = OrionGlobals.YesNoImage(scan.HasGate);
				hasStarGate.ImageUrl = OrionGlobals.YesNoImage(scan.HasStarGate);

				hasHospital.ImageUrl = OrionGlobals.YesNoImage(scan.HasHospital);
				hasLandReclamation.ImageUrl = OrionGlobals.YesNoImage(scan.HasLandReclamation);
				hasMineralExtractor.ImageUrl = OrionGlobals.YesNoImage(scan.HasMineralExtractor);
				hasSpa.ImageUrl = OrionGlobals.YesNoImage(scan.HasSpa);
				hasStockMarkets.ImageUrl = OrionGlobals.YesNoImage(scan.HasStockMarkets);
				hasWaterReclamation.ImageUrl = OrionGlobals.YesNoImage(scan.HasWaterReclamation);
				hasIonCannon.ImageUrl = OrionGlobals.YesNoImage(scan.HasIonCannon);
				hasTurret.ImageUrl = OrionGlobals.YesNoImage(scan.HasTurret);
		
			} catch( Exception ex ) {
				Visible = false;
				Chronos.Utils.Log.log(ex);
				ExceptionLog.log(ex);
			}
		}
		
		#endregion
		
		#region Control Properties
		
		/// <summary>Indica o Id do scan</summary>
		private int ScanId {
			get {
				string scan = Page.Request.QueryString["scan"];
				if( scan == null ) {
					throw new Exception("No 'scan' found on query string");
				}
				if( !OrionGlobals.isInt(scan) ) {
					throw new Exception("Can't convert '"+scan+"' to id");
				}
				return int.Parse(scan);
			}
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Preenche um Panel</summary>
		private void fillPanel( Panel panel, string label, int val )
		{
			panel.EnableViewState = false;
			panel.Width = new Unit(val, UnitType.Percentage);
			panel.Controls.Add( new LiteralControl(val.ToString() + "%") );
		}
		
		#endregion
		
	};
	
}
