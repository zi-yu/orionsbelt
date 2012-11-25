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
	public class TravelControl : TravelControlBase {

		#region Private Fields

		protected static FactoryContainer factory = new FactoryContainer( typeof( TravelFactory ) );
		
		protected PlaceHolder travelControlBase;
		protected TravelPanel travelPanel;

		#endregion

		#region Public Methods

		public Coordinate Coordinate {
			get{ 
				object o = Page.Request.Form[TravelPlanetControl.PlanetFieldName];
				if( o != null && o.ToString() != string.Empty ) {
					return Coordinate.translateCoordinate( travelPanel.CurrentCoordinate + ":" + o.ToString() );
				} else {
					//return Coordinate.translateCoordinate( travelPanel.DropListCoordinate );
					return null;
				}
			}
		}

		#endregion

		#region Private Methods

		private object createInstance( string obj ) {
			Type type = Type.GetType( string.Format("Alnitak.Travel{0}Control",obj), true );
			return Activator.CreateInstance(type);
		}

		private TravelCoordControlBase createControl( string id, string coordClicked ) {

			if( coordClicked == null ) {
				throw new Exception("coordClicked == null");
			}

			TravelCoordControlBase ctrl = (TravelCoordControlBase)factory.create("Travel" + id );

			ctrl.Coord = coordClicked;
			if( ctrl.Coord == null ) {
				throw new Exception("ctrl.Coord == null -> coordClicked == null ? " + (coordClicked == null) + " Control: " + ctrl.GetType().Name);
			}

			travelPanel.PreviousCoordinate = coordClicked;
			travelPanel.CurrentControl = id;
			travelPanel.MaximumAvailableControl = _maximumAvailableControl;

			travelPanel.CurrentCoordinate = ctrl.Coord;

			return ctrl;
		}

		/// <summary>
		/// escolhe o contrlo a carregar
		/// </summary>
		private TravelCoordControlBase chooseControl() {
			string id = "1";
			string coordClicked = "";
			TravelCoordControlBase ctrl;
			if( Page.IsPostBack ) { // Houve um post na pgina
				string postedStr = Page.Request.Form["controlToShow"];
				if( postedStr != null && postedStr != string.Empty  ) { //posted por click no controlo
					coordClicked = Page.Request.Form["numberClicked"];
					id = postedStr;
				} else { //post feito por outro controlo
					coordClicked = travelPanel.CurrentCoordinate;
					id = travelPanel.CurrentControl;
				}

				ctrl = createControl( id, coordClicked );

			}else { // Situao inicial 
				id = _maximumAvailableControl.ToString();

				coordClicked = StartCoordinate.ToString();

				ctrl = createControl( id, coordClicked );
			}

			if( !travelPanel.verifyCoordinate( coordClicked ) ) {
				travelPanel.Visible = false;
			} else {
				travelPanel.Visible = true;
			}
			
			return ctrl;		
		}

		#endregion

		#region Override Methods

		protected override void OnPreRender(EventArgs e) {
			if( _maximumAvailableControl != 0 ) {
				Control o = chooseControl();
				if( o != null ) {
					travelControlBase.Controls.Add( o );
				}
			}
			
			base.OnPreRender (e);
		}

		#endregion
	}
}
