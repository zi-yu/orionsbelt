using System;
using System.Collections;
using Alnitak.Exceptions;
using Chronos.Core;

namespace Alnitak {

	public class ShowPlanetFleet : ShowFleet {

		/// <summary>
		/// Retorna o ID do planeta a mostrar
		/// </summary>
		protected int getId() {
			object obj = Page.Request.QueryString["id"];
			try {
				return int.Parse(obj.ToString());
			} catch( Exception ) {
				return -1;
			}
		}
		
		/// <summary>
		/// obtém todas as fleets associadas a um planeta
		/// </summary>
		/// <returns>um array com todas as fleets</returns>
		override protected ArrayList getAllFleets() {
			ArrayList fleetsArray = new ArrayList();

			int id = getId();
			Planet planet = ruler.getPlanet(id);
			if( id < 0 || planet == null ) {
				throw new AlnitakException( "Planeta é inválido @ ShowPlanetFleet:getAllFleets " );
			}

			IDictionaryEnumerator iter = planet.Fleets.GetEnumerator();
			while( iter.MoveNext() ) {
				fleetsArray.Add( iter.Value );
			}

			return fleetsArray;
		}

		#region Events

		protected override void OnInit(EventArgs e) {
			title = info.getContent("fleet_planetShips");
			base.OnInit (e);
		}

		#endregion

	};
}
