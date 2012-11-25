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
	public abstract class TravelCoordControlBase : Control {
		
		#region Fields

		protected Ruler ruler = null;
		protected ILanguageInfo info = CultureModule.getLanguage();
		protected readonly int size = 480;

		#endregion

		#region Private Methods

		/// <summary>
		/// Retorna o Ruler da sessão
		/// </summary>
		protected Ruler getRuler() {
			User user = HttpContext.Current.User as User;
			if( user == null )
				throw new AlnitakException("User não está autenticado @ ScanControl::getRuler");

			Ruler ruler = Universe.instance.getRuler(user.RulerId);
			if( ruler == null )
				throw new AlnitakException("Ruler não existe @ ScanControl::getRuler");

			return ruler;
		}

		/// <summary>
		/// regista o script de cliente
		/// </summary>
		protected virtual void registerScript() {
			string script = @"
				<script language='javascript'>
					function post( control, number ) {
						var theform = document.pageContent;
						theform.controlToShow.value = control;
						theform.numberClicked.value = number;
						theform.submit();
					}
				</script>";
						
			Page.RegisterClientScriptBlock("TravelControlBase",script);

			Page.RegisterHiddenField("controlToShow","");
			Page.RegisterHiddenField("numberClicked","");
		}


		#endregion

		#region Properties



		#endregion

		#region Public Methods

		public string ControlClicked {
			get{ return Page.Request.Form["controlClicked"];}
		}

		#endregion

		#region Overrided Methods

		protected override void OnPreRender(EventArgs e) {
			if( ruler == null )
				ruler = getRuler();
			
			registerScript();
			base.OnPreRender (e);
		}


		#endregion
		
		#region Control Methods

		/// <summary>
		/// Método de Render do controlo
		/// </summary>
		protected override void Render( HtmlTextWriter writer ) {
			renderTitle( writer );
			renderLayout( writer );
		}

		#endregion

		#region 

		#endregion

		#region Abstract Methods

		public abstract void renderTitle( HtmlTextWriter writer );
		public abstract void renderLayout( HtmlTextWriter writer );
		public abstract string Coord {get;set;}

		#endregion

	}
}
