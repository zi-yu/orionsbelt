using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Chronos.Core;

namespace Alnitak {

	/// <summary>
	/// Summary description for ScanUniverse.
	/// </summary>
	public class TravelSectorControl : TravelCoordControlBase {
		
		#region Private Fields

		private readonly int _itemsPerLine = 5;
		private string _system;

		#endregion
		
		#region Overrided Methods

		public override string Coord {
			get{ return _system; }
			set{
				string[] s = value.Split( new char[]{':'} );
				_system = string.Format("{0}:{1}",s[0],s[1]);
			}
		}

		public override void renderTitle(HtmlTextWriter writer) {
			//escrever título
			writer.WriteLine("<div align='center' class='planetInfoZoneTitle'><b>");
			writer.WriteLine( info.getContent("scan_sectorTitle"), _system);
			writer.WriteLine("</b></div>");
		}

		public override void renderLayout(HtmlTextWriter writer) {

			int lines = Coordinate.MaximumSectors / _itemsPerLine;

			writer.WriteLine("<table align='center' class='frame' width='{0}'>",size);

			//escrever a primeira linha das coordenadas as galáxias
			writer.WriteLine("<tr>");
			writer.WriteLine("<td width='16px'></td>");
			for( int i = 0; i < _itemsPerLine ; ++i ) {
				writer.WriteLine("<td class='resourceTitle' >{0}</td>", i+1);
			}
			writer.WriteLine("</tr>");
						
			//escrever as imagens do Universo
			Random r = new Random(0);
			for( int i = 0; i < lines; ++i ) {
				writer.WriteLine("<tr>");
				writer.WriteLine( string.Format("<td width='16px' class='resourceTitle'>{0}</td>",i+1) );
				for( int j = 0; j < _itemsPerLine ; ++j ) {
					int a = (i*_itemsPerLine+j+1);
					string param = string.Format( "{0}:{1}",_system, a);
					writer.WriteLine( "<td class='deselectedCell' width='102px' height='50px' align='center' valign='middle' onmouseover='borderOn(this)' onmouseout='borderOff(this)' background='{0}' title='{1}' onClick='post(\"1\",\"{1}\");'>&nbsp;",OrionGlobals.getCommonImagePath("travel/space"+r.Next(1,5)+".gif"),param  );
					writer.WriteLine("</td>");
				}
				writer.WriteLine("</tr>");
			}

			writer.WriteLine("</table>");
		}

		#endregion

		#region Constructor

		public TravelSectorControl() {
			ruler = getRuler();
			Coordinate c = ruler.Planets[0].Coordinate;
			_system = c.Galaxy + ":" + c.System;
		}

		#endregion

	}
}
