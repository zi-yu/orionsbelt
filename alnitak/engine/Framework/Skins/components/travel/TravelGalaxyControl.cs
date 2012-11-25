using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Chronos.Core;

namespace Alnitak {

	/// <summary>
	/// Summary description for ScanUniverse.
	/// </summary>
	public class TravelGalaxyControl : TravelCoordControlBase {

		#region Overrided Methods

		public override string Coord {
			get{ return ""; }
			set{}
		}

		public override void renderTitle(HtmlTextWriter writer) {
			//escrever título
			writer.WriteLine("<div align='center' class='planetInfoZoneTitle'><b>{0}</b></div>",info.getContent("scan_galaxyTitle"));
		}

		public override void renderLayout(HtmlTextWriter writer) {
			
			writer.WriteLine("<table align='center' class='frame' width='{0}'>",size);

			//escrever a primeira linha das coordenadas as galáxias
			writer.WriteLine("<tr>");
			for( int i = 0; i < Coordinate.MaximumGalaxies; ++i ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", i+1);
			}
			writer.WriteLine("</tr>");
			
			//escrever as imagens do Universo
			writer.WriteLine("<tr>");
			for( int i = 0; i < Coordinate.MaximumGalaxies; ++i ) {
				writer.WriteLine("<td align='center' class='deselectedCell' onmouseover='borderOn(this)' onmouseout='borderOff(this)'>");
				writer.WriteLine("<img class='hand' id='galaxy{0}' src='{1}' onClick='post(\"3\",{0})' />",i+1,OrionGlobals.getCommonImagePath("travel/galaxy"+i+".jpg"));
				writer.WriteLine("</td>");
			}
			writer.WriteLine("</tr>");

			writer.WriteLine("</table>");
		}

		#endregion

	}
}
