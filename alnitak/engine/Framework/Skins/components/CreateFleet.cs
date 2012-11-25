using System;
using System.Web.UI.WebControls;
using Chronos.Core;

namespace Alnitak {
	
	public class CreateFleet : PlanetControl {
		
		protected TextBox fleetName;
		protected Button createFleet;
		protected OnlyTextValidator nameValidator;

		private Chronos.Core.Planet planet;

		#region private methods

		private void createFleet_Click(object sender, EventArgs e) {
			if( Page.IsValid ) {
				if( fleetName.Text == string.Empty ) {
					Information.AddError( info.getContent("fleet_nameRequired") );
					return;
				}

				Ruler ruler = getRuler();
				
				//Chronos.Utils.Log.log("Total: {0} Has: {1}", ruler.TotalFleets, ruler.MaxFleets);
				if( ruler.TotalFleets >= ruler.MaxFleets ) {
					Information.AddError( string.Format(info.getContent("fleet_maxReached"), ruler.TotalFleets, ruler.MaxFleets) );
					return;
				}

				if( planet.addFleet( fleetName.Text ) ) {
					Information.AddInformation( info.getContent("fleet_fleetCreated") );
					fleetName.Text = string.Empty;
				}else{
					Information.AddError( info.getContent("fleet_fleetAlreadyExists") );
				}
			}
		}

		#endregion

		#region events

		protected override void OnInit(EventArgs e) {
			planet = getPlanet();

			nameValidator.ErrorMessage = info.getContent("fleet_invalidFleetName");
		
			createFleet.Text =  info.getContent("fleet_createFleet");
			createFleet.Click +=new EventHandler(createFleet_Click);

			base.OnInit( e );
		}


		#endregion

	}
}
