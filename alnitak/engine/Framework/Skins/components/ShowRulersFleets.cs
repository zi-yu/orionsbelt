using System;
using System.Collections;
using Chronos.Core;

namespace Alnitak {

	public class ShowRulersFleets : ShowFleet {

		/// <summary>
		/// obtém todas as fleets associadas a um planeta
		/// </summary>
		/// <returns>um array com todas as fleets</returns>
		override protected ArrayList getAllFleets() {
			ArrayList fleetsArray = new ArrayList();

			IDictionaryEnumerator iter = ruler.UniverseFleets.GetEnumerator();	
			while( iter.MoveNext() ) {
				fleetsArray.Add( iter.Value );
			}

			return fleetsArray;
		}

		#region events

		protected override void OnInit(EventArgs e) {
			title = info.getContent("fleet_fleetsInSpace");
			base.OnInit (e);
		}

		#endregion

	};
}