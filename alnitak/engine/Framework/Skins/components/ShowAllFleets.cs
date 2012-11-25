using System;
using System.Collections;
using Chronos.Core;

namespace Alnitak {
	public class ShowAllFleets : ShowFleet {

		/// <summary>
		/// obtém as todas as fleets do utilizador
		/// </summary>
		/// <returns></returns>
		override protected ArrayList getAllFleets() {
			ArrayList fleetsArray = new ArrayList();
			foreach( Planet planet in ruler.Planets ) {
				IDictionaryEnumerator iter = planet.Fleets.GetEnumerator();	
				while( iter.MoveNext() ) {
					Chronos.Core.Fleet f = (Chronos.Core.Fleet)iter.Value;
					if( f.IsMoveable )
						fleetsArray.Add( f );
				}
			}
			return fleetsArray;
		}

		#region Events

		protected override void OnInit(EventArgs e) {
			title = info.getContent("fleet_planetsShips");
			base.OnInit (e);
		}

		#endregion

	};
	
}