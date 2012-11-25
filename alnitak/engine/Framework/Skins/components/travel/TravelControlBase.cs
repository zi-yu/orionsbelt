using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Chronos.Core;
using Alnitak.Exceptions;
using Language;

namespace Alnitak {

	/// <summary>
	/// Summary description for ScanUniverse.
	public abstract class TravelControlBase : UserControl {
		
		#region Fields

		protected Ruler _ruler = null;

		protected string[] research = { "Planet","Sector","System","Galaxy" };
		protected int _maximumAvailableControl;

		protected Coordinate _startCoordinate = null;

		#endregion

		#region Properties

		/// <summary>
		/// define a coordenada inicial do controlo
		/// </summary>
		public Coordinate StartCoordinate {
			get{ return _startCoordinate; }
			set{ _startCoordinate = value; }
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Retorna o Ruler da sessão
		/// </summary>
		protected Ruler getRuler() {
			User user = Page.User as User;
			if( user == null ) {
				OrionGlobals.forceLogin();
				throw new AlnitakException("User não está autenticado @ TravelControl::getRuler");
			}

			Ruler ruler = Universe.instance.getRuler(user.RulerId);
			if( ruler == null )
				throw new AlnitakException("Ruler não existe @ TravelControl::getRuler");

			return ruler;
		}

		/// <summary>
		/// obtém o TravelControl disponivel e desactiva os botões
		/// do travelPanel caso necessário
		/// </summary>
		/// <returns></returns>
		public int getTravelControlId() {
			for( int i = research.Length ; i > 0 ; --i ) {
				if( _ruler.isResourceAvailable( "Research", research[i-1] + "Exploration" ) ) {
					return i;
				}
			}
			return 0;
		}

		#endregion

		#region Override Methods

		protected override void OnInit(EventArgs e) {
			_ruler = getRuler();
			_maximumAvailableControl = getTravelControlId();

			_startCoordinate = _ruler.HomePlanet.Coordinate;
			if( _startCoordinate == null ) {
				throw new Exception("Ruler HomePlanet Coordinate == null");
			}

			if( _maximumAvailableControl == 0 ) {
				return;
			}
			
			HttpContext.Current.Session["MaximumAvailableControl"] = _maximumAvailableControl;

			base.OnInit (e);
		}

		#endregion

	}
}
