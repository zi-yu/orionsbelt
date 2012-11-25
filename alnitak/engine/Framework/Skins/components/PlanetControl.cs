// created on 9/14/2004 at 11:19 AM

using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Resources;
using System;

using Alnitak.Exceptions;

namespace Alnitak {

	public class PlanetControl : UserControl {
	
		#region Instance Fields
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		
		#endregion
	
		#region General Utility Methods
		
		/// <summary>Retorna o Ruler da sessão sem lançar excepção</summary>
		protected Ruler getRulerSafe() {
			User user = Page.User as User;
			if( user == null )
				return null;

			Chronos.Core.Ruler ruler = Universe.instance.getRuler(user.RulerId);
			return ruler;
		}
		
		/// <summary>Retorna o Ruler da sessão</summary>
		protected Ruler getRuler() {
			User user = Page.User as User;
			if( user == null ) {
				OrionGlobals.forceLogin();
				return null;
			}

			Chronos.Core.Ruler ruler = Universe.instance.getRuler(user.RulerId);
			if( ruler == null )
				throw new AlnitakException("Ruler n�o existe @ PlanetControl::getRuler");
			return ruler;
		}
		
		/// <summary>Retorna o ID do planeta a mostrar</summary>
		protected Chronos.Core.Planet getPlanet()
		{
			string obj = Page.Request.QueryString["id"];
			if( obj == null ) {
				return null;
			}
			
			int planetId;
			try {
				 planetId = int.Parse(obj.ToString());
			} catch {
				return null;
			}

			return getRuler().getPlanet( planetId );
		}
		
		#endregion
		
		#region Error Methods
		
		/// <summary>Escreve um erro</summary>
		protected void writeErrorResponse()
		{
			// TODO : Retirar a porra disto do light daqui
			string text = "<div class='error'>" + info.getContent("errorNoPermissions") + "</div>";
			Controls.Add( new LiteralControl(text) );
		}
		
		#endregion
	};
}
