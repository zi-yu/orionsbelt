using System.Web.UI;
using Chronos.Core;

namespace Alnitak {
	
	public class BattleNotifier : PlanetControl {

		protected override void Render( HtmlTextWriter writer )
		{
			Ruler ruler = getRuler();
			if( ruler.BattleWaiting ) {
				base.Render(writer);
			}
		}
	};

}
