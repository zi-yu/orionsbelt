using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Chronos.Core;


namespace Alnitak {
	
	/// <summary>
	/// Item Final de um Scan, ou seja um Planet e alguma informação sobre ele
	/// </summary>
	public class TravelItem : Control {
		
		#region Private Fields
			
		private Planet _planet = null;
		private string _jsCall = string.Empty;
		private bool _currentRulerOwns = false;

		#endregion

		#region Private Methods

		#endregion

		#region Overridden Methods

		protected override void Render(HtmlTextWriter writer) {
			writer.WriteLine( string.Format( "<table><tr>") );

			string planetId = "0";
			if( _planet != null ) {
				planetId = _planet.Info.Id.ToString();
			}

			bool b = _planet!= null && ( _planet.Coordinate.Planet != 1 || _currentRulerOwns );
			if( b )
				writer.WriteLine( "<div onClick='{0}' class='deselectedCell' style='cursor:pointer;' >", _jsCall );

			writer.WriteLine( string.Format( "<img width='50' height='50' id='planet{0}' src='{1}' />",planetId,OrionGlobals.getCommonImagePath( "planets/" + planetId + ".jpg" ) ) );

			if( b )
				writer.WriteLine( "</div>" );

			writer.WriteLine("</table>");
		}

		#endregion

		#region Constructor
		
		public TravelItem( Planet planet, string jsCall, bool currentRulerOwns ) {
			_planet = planet;
			_jsCall = jsCall;
			_currentRulerOwns = currentRulerOwns;
		}

		#endregion

	}
}
