// created on 3/6/2006 at 9:19 PM

using System;
using System.Collections;
using System.IO;
using System.Web.UI;
using Chronos.Core;
using Chronos.Sorter;
using Chronos.Utils;

namespace Alnitak {

	public class PurgeUsers : Control {	

		#region Control Events

		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			if( Page.Request.Form["purge"] != null ) {
				Purge();
			}
		}
		
		#endregion

		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			if( Page.User.IsInRole("admin") ) {
				if( Page.Request.Form["purge"] == null ) {
				
					IList list = UserUtility.bd.getInactiveUsers();
					//Console.ReadLine();
				
					writer.WriteLine("<div class='stats-box'>");
					writer.WriteLine("<div align='center'><b>Purge Users</b></div>");
					writer.WriteLine("<input type='submit' value='Purge {0} Inactive Users' name='purge' />", list.Count);
					
					writer.WriteLine("<ul>");
					foreach( User user in list ) {
						Ruler ruler = Universe.instance.getRuler(user.RulerId);
						writer.WriteLine("<li>{0} - Planetas: {1}</li>", OrionGlobals.getLink(user), ruler.Planets.Length);
					}
					writer.WriteLine("</ul>");
					
					writer.WriteLine("</div>");
				} else {
					writer.WriteLine("<div>Done!</div>");
				}
			} else {
				writer.WriteLine("There is no spoon!");
			}
		}

		#endregion
		
		#region Utils
		
		private void Purge()
		{
			IList list = UserUtility.bd.getInactiveUsers();
			
			foreach( User user in list ) {
				Ruler ruler = Universe.instance.getRuler(user.RulerId);
				
				ruler.LooseAllBattles();
				ruler.FullReset();
				Universe.instance.removeRulerFromAlliance( ruler );
				
				foreach( Planet planet in ruler.Planets ) {
					planet.FullReset();
				}
				
				Universe.instance.planets.Remove(ruler.HomePlanet.Coordinate);
				Universe.instance.rulers.Remove(ruler.Id);
				
				user.RulerId = -1;
				user.AllianceId = 0;

				UserUtility.bd.saveUser(user,"");
				
				Log.log("User `{0}' (id:{1}) Removed", user.Name, user.UserId);
			}
		}
		
		
		#endregion
	
	};
}
