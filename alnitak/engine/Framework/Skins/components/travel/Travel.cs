using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Chronos.Core;

using Alnitak.Exceptions;
using DesignPatterns;

namespace Alnitak {

	/// <summary>
	///
	/// </summary>
	public class Travel : UserControl {

		#region Private Fields
		
		protected DirectTravelControl directTravel;
		protected TravelControl advancedTravel;
		private string defaultTab = "tab1";
		private string defaultContent = "content1";
	
		#endregion

		#region Public Methods

		public Coordinate Coordinate {
			get{
				Coordinate coord = advancedTravel.Coordinate;
				if( null == coord ) {
					coord = directTravel.Coordinate;
				}

				return coord;
			}
		}

		public bool StartInPlanet {
			set{}
		}

		public bool IsTravelAvailable {
			get{
				if( directTravel.getTravelControlId() == 0 ) {
					return false;
				}
				return true;
			}
		}

		#endregion

		#region Private Methods

	
		#endregion

		#region Override Methods

		protected override void OnInit(EventArgs e) {
			//só por causa de n mostrar isto se n tiver as cenas certas

			if( Page.IsPostBack ) {
				defaultTab = Page.Request.Form["oldTabCtrl"];
				defaultContent = Page.Request.Form["oldTabContent"];
			}

            Page.RegisterHiddenField("oldTabCtrl",defaultTab);
			Page.RegisterHiddenField("oldTabContent",defaultContent);

			base.OnLoad (e);
		}
	
		#endregion
	}
}
