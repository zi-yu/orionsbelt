// created on 9/13/2004 at 5:06 PM

using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Resources;
using System;

namespace Alnitak {

	public class ShowPlanetResources : PlanetControl {
	
		#region Instance Fields
		
		private Chronos.Core.Planet planet;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor</summary>
		public ShowPlanetResources( Chronos.Core.Planet p )
		{
			planet = p;
		}
		
		#endregion
		
		#region Control Events
		
		/// <summary>Pinta o Controlo</summary>
		protected override void OnPreRender( EventArgs args )
		{
			MasterSkinInfo masterSkinInfo = (MasterSkinInfo)Context.Items["MasterSkinInfo"];
			string control = OrionGlobals.AppPath + masterSkinInfo.masterSkinName + "/controls/ShowPlanetResources.ascx";
			Control planetSkin = Page.LoadControl(control);
		
			foreach( string resource in planet.getResourceInfo("Intrinsic").AvailableFactories.Keys ) {
				fillResource(resource, planetSkin, planet);
			}
			
			Controls.Add(planetSkin);
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Preenche os controlos relativos a um recurso</summary>
		private void fillResource( string resource, Control control, Chronos.Core.Planet planet) 
		{
			Label quantity = (Label) control.FindControl(resource + "Quantity");
			if( quantity != null ) {
				quantity.EnableViewState = false;
				int val = planet.getResourceCount(resource);
				quantity.Text = val.ToString();
				if( val < 0 ) {
					quantity.CssClass = "error";
				}
			}
			
			Label ratio = (Label) control.FindControl(resource + "Ratio");
			if( ratio != null ) {
				ratio.EnableViewState = false;
				int val = planet.getRatio(resource);
				if( val > 0 ) {
					ratio.Text = val.ToString() + "%";
				} else {
					ratio.Text = "-";
				}
			}
			
			Label perTurn = (Label) control.FindControl(resource + "PerTurn");
			if( perTurn != null ) {
				perTurn.EnableViewState = false;
				int val = planet.getPerTurn("Intrinsic",resource);
				string str = val.ToString();
				
				if( val > 0 ) {
					str = "+" + str;
					perTurn.Text = str;
				} else if( val == 0 ) {
					perTurn.Text = "-";
				} else {	
					perTurn.CssClass = "error";
					perTurn.Text = str;
				}
			}
		}	
		
		#endregion
	};
	
}