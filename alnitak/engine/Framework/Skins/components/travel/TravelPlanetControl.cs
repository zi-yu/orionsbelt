using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Chronos.Core;
using Chronos.Alliances;

namespace Alnitak {

	/// <summary>
	/// Summary description for ScanUniverse.
	/// </summary>
	public class TravelPlanetControl : TravelCoordControlBase {
		
		#region Overrided Methods

		private string _sector = null;

		#endregion

		//public event PlanetClicked PlanetClicked;

		#region Private Methods

		/// <summary>
		/// escolhe o border que o planeta vai ter
		/// </summary>
		/// <returns>a classe</returns>
		private string chooseClass( Planet planet ) {
			if( ruler == null || planet == null || planet.Owner == null )
				return "none";
			
			if( ruler == planet.Owner )
				return "yours";

			if( ruler.Alliance == planet.Owner.Owner && ruler.Alliance.Name != Alliance.defaultAllianceName)
				return "allied";

			return "enemyPlanet";
		}

		#endregion

		#region Overrided Methods

		public override string Coord {
			get{ return _sector; }
			set{
				string[] s = value.Split( new char[]{':'} );
				if( s.Length >= 3 ) {
					_sector = string.Format("{0}:{1}:{2}",s[0],s[1],s[2]);
				}
			}
		}

		public static string PlanetFieldName {
			get{ return "planetClicked"; }
		}

		protected override void registerScript() {
			base.registerScript ();

			string script = @"
				<script language='javascript'>
					function planetClick( planetNumber, ctrlClicked ) {
						var theform = document.pageContent;
						var v = theform.planetClicked.value;
						if( v == planetNumber ) {
							theform.planetClicked.value = '';
						} else {
							theform.planetClicked.value = planetNumber;
						}
						borderOnOff( ctrlClicked );
					}
				</script>";
						
			Page.RegisterClientScriptBlock("TravelPlanetControl",script);
			Page.RegisterHiddenField("planetClicked","");
		}

		protected override void OnInit(EventArgs e) {
			ruler = getRuler();
			
			if( _sector == null ) {
				Coordinate c = ruler.HomePlanet.Coordinate;
				_sector = c.Galaxy + ":" + c.System + ":" + c.Sector;
			}
			
			Page.RegisterHiddenField("planetClicked","");
		}


		public override void renderTitle(HtmlTextWriter writer) {
			//escrever ttulo
			writer.WriteLine("<div  align='center' class='planetInfoZoneTitle'><b>");
			writer.WriteLine( info.getContent("scan_planetTitle"), _sector );
			writer.WriteLine("</b></div>");
		}

		public override void renderLayout(HtmlTextWriter writer) {
			writer.WriteLine("<table align='center' class='frame' width='{0}'>",size);

			//escrever a primeira linha das coordenadas as galxias
			writer.WriteLine("<tr>");
			for( int i = 0; i < Coordinate.MaximumPlanets; ++i ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", i+1);
			}
			writer.WriteLine("</tr>");
			
			//escrever as imagens do Universo
			writer.WriteLine("<tr>");
			for( int i = 1; i <= Coordinate.MaximumPlanets ; ++i ) {
				
				string coordinate = _sector + ":" + i ;
				Planet p = Universe.instance.getPlanet( Coordinate.translateCoordinate( coordinate ) );

				string className = chooseClass( p );
				writer.WriteLine( string.Format("<td align='center' class='{0}' width='33%' >",className) );
	
				TravelItem travelItem = new TravelItem( p, string.Format("planetClick({0},this);",i), className=="yours" );
				travelItem.RenderControl( writer );

				writer.WriteLine("</td>");
			}
			writer.WriteLine("</tr>");

			writer.WriteLine("</table>");
		}


		#endregion
	}
}
