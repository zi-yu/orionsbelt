using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;

using Language;

using Chronos.Core;
using DesignPatterns;

using Alnitak.Exceptions;

namespace Alnitak {
	
	/// <summary>
	/// Painel que aparece de lado no Controlo
	/// </summary>
	public class TravelPanel : UserControl {

		#region Fields

		private static FactoryContainer incrementor = new FactoryContainer( typeof( UtilitiesFactory ) ) ;

		protected ILanguageInfo info = CultureModule.getLanguage();
		
		protected int _currentCtrl = 1;
		protected string _previousCoordinate = string.Empty;
		protected string _currentCoordinate = string.Empty;
		
		//representa o id máximo disponivel
		protected int _maximumAvailableControl = 0;

		private Ruler ruler = null;

		#endregion

		#region Controls

		protected Button next;
		protected Button up;
		protected Button prev;

		#endregion

		#region Private Methods

		/// <summary>
		/// Retorna o Ruler da sessão
		/// </summary>
		private Ruler getRuler() {
			User user = Page.User as User;
			if( user == null )
				throw new AlnitakException("User não está autenticado @ TravelControl::getRuler");

			Ruler ruler = Universe.instance.getRuler(user.RulerId);
			if( ruler == null )
				throw new AlnitakException("Ruler não existe @ TravelControl::getRuler");

			return ruler;
		}

		#endregion

		#region Public Methods
		
		public void DisableButtons() {
			next.Visible = up.Visible = prev.Visible = false;
		}

		public void EnableButtons() {
			next.Visible = up.Visible = prev.Visible = true;
		}

		/// <summary>
		/// Verifica se tem de introduzir controlos
		/// </summary>
		/// <param name="coordClicked"></param>
		/// <returns><code>false</code> se já n tiver controlos, <code>true</code> caso contrário</returns>
		public bool verifyCoordinate( string coordClicked ) {
			IUtil util = (IUtil)incrementor.create( "util" + _currentCtrl );

			bool first = util.isFirst( coordClicked, _maximumAvailableControl );
			bool last = util.isLast( coordClicked, _maximumAvailableControl );

			if( first ) {
				Controls.Remove( prev );
			}
			if( last ) {
				Controls.Remove( next );
			}

			if( last && first ) {
				Controls.Remove( up );
				return false;
			}

			return true;
		}

		#endregion

		#region Overrided

		protected override void OnInit(EventArgs e) {
			ruler = getRuler();

			object c = HttpContext.Current.Session["currentCtrl"];
			if( c != null ) {
				_currentCtrl = int.Parse( c.ToString() );
				_previousCoordinate = HttpContext.Current.Session["previousCoordinate"].ToString();
				_currentCoordinate = HttpContext.Current.Session["currentCoordinate"].ToString();
			}
			
			base.OnInit (e);
		}

		protected override void OnLoad(EventArgs e) {
			object o = HttpContext.Current.Session["MaximumAvailableControl"];
			if( o != null ) {
				MaximumAvailableControl = (int)o;
			}

			next.Click += new EventHandler(next_Click);
			up.Click += new EventHandler(up_Click);
			prev.Click += new EventHandler(prev_Click);

			base.OnLoad(e);
		}

		protected override void OnUnload(EventArgs e) {
			HttpContext.Current.Session.Add("currentCtrl",_currentCtrl.ToString() );
			HttpContext.Current.Session.Add("previousCoordinate",_previousCoordinate);
			HttpContext.Current.Session.Add("currentCoordinate",_currentCoordinate);
			base.OnUnload (e);
		}

		#endregion

		#region Properties
		
		public string CurrentControl{
			set{ 
				_currentCtrl = int.Parse( value );
				if( _currentCtrl != 4 ) {
					up.ToolTip = string.Format( info.getContent( "travel_" + value ), PreviousCoordinate );
				}
			}
			get{ return _currentCtrl.ToString(); }
		}

		public string PreviousCoordinate{
			set{
				if( value == null ) {
					throw new Exception("Value == null");
				}
				_currentCoordinate = value;
				int i = value.LastIndexOf(":");
				if( i != -1 ) {
					_previousCoordinate = value.Substring(0, i );
				} else {
					_previousCoordinate = string.Empty;
				}
			}
			get{ return _previousCoordinate; }
		}

		public string CurrentCoordinate{
			get{ return _currentCoordinate;	}
			set{ _currentCoordinate = value; }
		}

		/// <summary>
		/// Id do Control disponivel
		/// </summary>
		public int MaximumAvailableControl {
			set{ _maximumAvailableControl = value; }
			get{ return _maximumAvailableControl; }
		}

		#endregion

		#region Events

		private void next_Click(object sender, EventArgs e) {
			IUtil increment = (IUtil)incrementor.create( "util" + _currentCtrl ); 
			if( increment.increment( ref _currentCoordinate, MaximumAvailableControl ) )
				Controls.Remove( next );
		}

		private void up_Click(object sender, EventArgs e) {
			if( _currentCtrl < 4 ) {
				_currentCtrl = ++_currentCtrl;
				_currentCoordinate = _previousCoordinate;
			}
		}

		private void prev_Click(object sender, EventArgs e) {
			IUtil decrement = (IUtil)incrementor.create( "util" + _currentCtrl ); 
			if( decrement.decrement( ref _currentCoordinate, MaximumAvailableControl ) )
				Controls.Remove( prev );
		}

		#endregion

		
	}
}
