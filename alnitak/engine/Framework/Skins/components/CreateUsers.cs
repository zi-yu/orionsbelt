using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {
	
	public class CreateUsers : UserControl {
	
		#region Instance Fields
		
		protected TextBox user;
		protected TextBox quant;
		protected Button create;
		
		#endregion
		
		#region Events

		protected override void OnLoad(EventArgs e) {
			
			create.Click +=new EventHandler(create_Click);
			base.OnLoad (e);

			create.Text = "criar";
		}
		
		#endregion

		private void create_Click(object sender, EventArgs e) {
			for( int i = 0 ; i < int.Parse( quant.Text ) ; ++i ) {
				int id = Universe.instance.addRulerToUniverse(user.Text+i.ToString(  ),"TempPlanet"+i);	
				Universe.instance.getRuler(id).ForeignId = 1;
			}
		}
	};
	
}

