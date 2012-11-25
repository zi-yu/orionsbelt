
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.IO;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Info;

namespace Alnitak {

	/// <summary>
	/// classe que representa a pgina de scans
	/// </summary>
	public class ManageScan : PlanetControl {
		
		#region Control Fields
		
		protected Travel travelControl;
		protected LinkButton performScan;
		protected LinkButton performSystemScan;
		protected System.Web.UI.WebControls.Label pleaseChooseCoord;
		protected QueueErrorReport errorReport;
		protected PlaceHolder reports;
		protected PlaceHolder performScanPanel;
		protected Label planetEnergy;
		protected Label scanCost;
		protected PlanetNavigation planetNavigation;
		
		protected HyperLink scanWiki;
		
		#endregion
		
		#region Control Events
		
		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
		
			RegisterRequest();
			InitControlOnLoad();
			
			scanWiki.NavigateUrl = Wiki.GetUrl("Scan");
			scanWiki.Text = info.getContent("Wiki_Scan");
			scanWiki.CssClass = "docs";
		}
		
		/// <summary>Mostra os scans</summary>
		protected override void OnPreRender( EventArgs args )
		{
			base.OnPreRender(args);
			Scan[] scans = GetScans();
			InitControlOnPreRender();

			StringWriter writer = new StringWriter();
			
			if( scans == null || scans.Length == 0 ) {
				writer.Write("<tr><td>{0}</td></tr>", info.getContent("scan_no_reports"));
			} else {
				GenerateScanReport(writer, scans);
			}
			
			reports.Controls.Add( new LiteralControl(writer.ToString()) );
		}
		
		/// <summary>Realiza o Scan</summary>
		private void performScan_Click(object src, EventArgs args)
		{
			Planet planet = getPlanet();
			
			Result result = planet.canScan(Coordinate);
			
			errorReport.Visible = true;
			errorReport.ResultSet = result;
			errorReport.Title = info.getContent("scan_performScan");
			
			if( result.Ok ) {
				Scan scan = planet.performScan(Coordinate);
				ScanUtility.Persistence.Register(scan);
			}
		}
		
		/// <summary>Realiza o Scan ao sistema</summary>
		private void performSystemScan_Click(object src, EventArgs args)
		{
			Planet planet = getPlanet();
			Result result = null;
			Coordinate target = Coordinate; 
			
			for( int i = 1; i <= 20; ++i ) {
				target.Sector = i;
				for( int j = 2; j <= 3; ++j ) {
					target.Planet = j;
					result = planet.canScan(target);
					if( !result.Ok ) {
						break;
					}
					Scan scan = planet.performScan(target);
					ScanUtility.Persistence.Register(scan);		
				}
			}
			
			errorReport.Visible = true;
			errorReport.ResultSet = result;
			errorReport.Title = info.getContent("scan_performScan");
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Indica se ao pedido est associada uma coordenada vlida</summary>
		private bool ValidCoordinate {
			get {
				return true;
			}
		}
		
		/// <summary>Indica a coordenada corrente a coordenar</summary>
		private Coordinate Coordinate {
			get {
				return travelControl.Coordinate;
			}
		}
		
		/// <summary>Gera o cdigo dos relatrios de scan</summary>
		private void GenerateScanReport( StringWriter writer, Scan[] scans )
		{
			Ruler ruler = getRuler();
			
			writer.Write("<tr class='resourceTitle'>");
			writer.Write("<td class='resourceTitle'>{0}</td>", info.getContent("turn_current"));
			if( ShowSourcePlanet ) {
				writer.Write("<td class='resourceTitle'>{0}</td>", info.getContent("section_planet"));
			}
			writer.Write("<td class='resourceTitle'>{0}</td>", info.getContent("scan_target"));
			writer.Write("<td class='resourceTitle'>{0}</td>", info.getContent("section_ruler"));
			writer.Write("<td class='resourceTitle'>{0}</td>", info.getContent("scan_success"));
			writer.Write("<td class='resourceTitle'>{0}</td>", info.getContent("scan_cloaked"));
			writer.Write("<td class='resourceTitle'>{0}</td>", info.getContent("scan_report"));
			writer.Write("</tr>");
			foreach( Scan scan in scans ) {
				writer.Write("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.Write("<td class='resourceCell'>{0}</td>", scan.Turn);
				if( ShowSourcePlanet ) {
					writer.Write("<td class='resourceCell'><a href='{0}default.aspx?id={2}'>{1}</a></td>", OrionGlobals.getSectionUrl("scan"), ruler.getPlanet(scan.SourcePlanetId).Name, scan.SourcePlanetId);
				}
				writer.Write("<td class='resourceCell'>{0}</td>", scan.Target.ToString());
				if( scan.TargetPlanetOwner >= 0 ) {
					writer.Write("<td class='resourceCell'>{0}</td>", OrionGlobals.getLink( Universe.instance.getRuler(scan.TargetPlanetOwner)));
				} else {
					writer.Write("<td class='resourceCell'>{0}</td>", info.getContent("scan_inhabited"));
				}
				writer.Write("<td class='resourceCell'><img src='{0}' /></td>", OrionGlobals.YesNoImage(scan.Success));
				writer.Write("<td class='resourceCell'><img src='{0}' /></td>", OrionGlobals.YesNoImage(!scan.Intercepted));
				if( !scan.Success ) {
					writer.Write("<td class='resourceCell'>&nbsp;</td>");
				} else {
					writer.Write("<td class='resourceCell'><a href='scanreport.aspx?id={0}&scan={2}'><img src='{1}' /></a></td>",
								 	scan.SourcePlanetId,
									OrionGlobals.getCommonImagePath("Filter.gif"),
								 	scan.Id
							);
				}
				
				writer.Write("</tr>");
			}
		}
		
		#endregion
		
		#region Virtual Zone
		
		protected virtual Scan[] GetScans()
		{
			return ScanUtility.Persistence.GetScans(getPlanet());
		}
		
		protected virtual void RegisterRequest()
		{
			Planet planet = getPlanet();
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Radar, string.Format("{1} - {0}",info.getContent("section_scan"), planet.Name));
		}
		
		protected virtual void InitControlOnLoad()
		{
			performScanPanel.Visible = false;
			pleaseChooseCoord.Visible = false;
			performScan.Click += new EventHandler(performScan_Click);
			performSystemScan.Click += new EventHandler(performSystemScan_Click); 	
		}
		
		protected virtual void InitControlOnPreRender()
		{
			Planet planet = getPlanet();
			
			if( ShowNavigation ) {
				planetNavigation.Player = (Ruler) planet.Owner;
				planetNavigation.Current = planet;
				planetNavigation.Section = "Scan";
			}
		
			if( !ValidCoordinate ) {
				pleaseChooseCoord.Visible = true;
				pleaseChooseCoord.Text = info.getContent("scan_choose_coord");
				return;
			}

			if( planet.ScanCost > planet.Energy ) {
				pleaseChooseCoord.Text = string.Format(info.getContent("scan_no_energy"), planet.ScanCost, planet.Energy);
				pleaseChooseCoord.Visible = true;
			} else {
				performScanPanel.Visible = true;
				performScan.Text = info.getContent("scan_performScan");
				performSystemScan.Text = info.getContent("scan_performSystemScan");
				scanCost.Text = planet.ScanCost.ToString();
				planetEnergy.Text = planet.Energy.ToString();
			}
		}
		
		protected virtual bool ShowNavigation {
			get { return true; }
		}
		
		protected virtual bool ShowSourcePlanet {
			get { return false; }
		}
		
		#endregion
			
	};

}
