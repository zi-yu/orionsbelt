// created on 3/20/2006 at 11:52 AM

using System;
using System.Web.UI;
using Chronos.Core;

namespace Alnitak {
	
	public class ManageAlliance : PlanetControl {

		#region Control Events
		
		protected override void OnInit( EventArgs args )
		{
			base.OnInit(args);
			User user = (User) Page.User;
			
			if( user.AllianceId == 0 ) {
				Controls.Add( new CreateAlliance() );
			} else {
				Controls.Add( new AllianceOverview() );
			}
		}
		
		#endregion Control Events
		
	};

}
