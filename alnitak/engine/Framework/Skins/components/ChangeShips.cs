using System;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using Alnitak.Exceptions;
using Chronos.Resources;

namespace Alnitak {
	//TO DO: Text box berra sem input.Mudar a expresso regular
	/// <summary>
	/// 
	/// </summary>
	public class ChangeShips : PlanetControl {
		
		protected TextBox quant;
		protected DropDownList originFleet;
		protected DropDownList destinyFleet;
		protected DropDownList availableShips;
		protected RegularExpressionValidator quantityValidator;
		protected Button moveShips;
		protected Button moveAllShips;
		protected Information message = (Information) HttpContext.Current.Items["ErrorMessage"];
		protected PlaceHolder content;

		private Chronos.Core.Planet planet;

		#region private methods

		/// <summary>
		/// obtm todas as fleets associadas a um planeta
		/// </summary>
		/// <returns>um array com todas as fleets</returns>
		private string[] getAllFleets() {
			string[] fleetsArray = new string[planet.Fleets.Count];

			int i = 0;
			IDictionaryEnumerator iter = planet.Fleets.GetEnumerator();	
			while( iter.MoveNext() ) {
				fleetsArray[i++] = ((Chronos.Core.Fleet)iter.Value).Name;
			}

			return fleetsArray;
		}

		/// <summary>
		/// obtm todas as naves que esto disponiveis neste planeta
		/// </summary>
		/// <returns>array com todas as naves disponiveis no planeta</returns>
		private void populateAvailableShips() {
			IEnumerator iter = Chronos.Core.Universe.getFactories("planet", "Unit").Keys.GetEnumerator();
			availableShips.Items.Clear();
			while( iter.MoveNext() ) {
				string s = iter.Current.ToString();
				availableShips.Items.Add( new ListItem(info.getContent(s),s) );
			}
		}

		/// <summary>
		/// povoa as fleets disponveis
		/// </summary>
		private void populateAvailableFleets() {
			//povoar as datagrids
			string[] fleets = getAllFleets();
			
			originFleet.DataSource = fleets;
			originFleet.DataBind();
			destinyFleet.DataSource = fleets;
			destinyFleet.DataBind();
		}


		private bool CheckInBattle() {
			if( planet.IsInBattle ) {
				Information.AddError( info.getContent( "fleet_moveWhileBattle" ) );
				return true;
			}
			return false;
		}


		/// <summary>
		/// evento para mudar naves de uma fleet para outra
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void moveShips_Click(object sender, EventArgs e) {
			if( CheckInBattle() )
				return;

			if( Page.IsValid ) {
				try {
					Chronos.Core.Fleet srcFleet = planet.getFleet( originFleet.SelectedValue );
					if( quant.Text != string.Empty && originFleet.SelectedValue != destinyFleet.SelectedValue ) {
						int q = int.Parse( quant.Text );
						if( srcFleet.isQuantityAvailable( availableShips.SelectedValue, q ) ) {
							Chronos.Core.Fleet dstFleet = planet.getFleet( destinyFleet.SelectedValue );
							if( dstFleet.swapShips( srcFleet , availableShips.SelectedValue, q ) ) {
								Information.AddInformation( info.getContent( "fleet_moveOk" ) );
							}else {
								Information.AddInformation( info.getContent( "fleet_full" ) );
							}
						} else {
							Information.AddError( info.getContent( "fleet_moveNotOk" ) );
						}
					} else {
						Information.AddError(  info.getContent( "fleet_moveInvalid" ) );
					}
				}catch( OverflowException ){
					Information.AddError( info.getContent( "fleet_moveNotOk" ) );
				}
			}
		}

		private void moveAllShips_Click(object sender, EventArgs e) {
			if( CheckInBattle() )
				return;

			try {
				Chronos.Core.Fleet srcFleet = planet.getFleet( originFleet.SelectedValue );
				if( originFleet.SelectedValue != destinyFleet.SelectedValue ) {
					if( srcFleet.HasShips ) {
						Chronos.Core.Fleet dstFleet = planet.getFleet( destinyFleet.SelectedValue );
						IDictionaryEnumerator iter = srcFleet.Ships.GetEnumerator();
						bool allMoved = true;
						while( iter.MoveNext() ) {
							if( !dstFleet.swapShips( srcFleet , iter.Key.ToString() , int.Parse( iter.Value.ToString() ) ) ) {
								allMoved = false;
							}else {
								iter = srcFleet.Ships.GetEnumerator();
							}
						}

						if( allMoved ) {
							Information.AddInformation( info.getContent( "fleet_moveOk" ) );
						}else {
							Information.AddInformation( info.getContent( "fleet_moveIncomplete" ) );
						}
					} else {
						Information.AddError( info.getContent( "fleet_hasntUnits" ) );
					}
				} else {
					Information.AddError( info.getContent( "fleet_moveInvalid" ) );
				}
			}catch( OverflowException ){
				Information.AddError( info.getContent( "fleet_moveNotOk" ) );
			}
		}

		#endregion

		#region events

		protected override void OnLoad(EventArgs e) {
			planet = getPlanet();
			if( planet == null ) {
				throw new AlnitakException( "Planeta  invlido @ ShowPlanetFleet:getAllFleets " );
			}

			quantityValidator.ErrorMessage = info.getContent("fleet_quantError");
			moveShips.Text =  info.getContent("fleet_moveShips");
			moveAllShips.Text = info.getContent("fleet_moveAllShips");
		
			moveShips.Click += new EventHandler(moveShips_Click);
			moveAllShips.Click += new EventHandler(moveAllShips_Click);

		}

		protected override void OnPreRender(EventArgs e) {
			populateAvailableFleets();
			populateAvailableShips();
			base.OnPreRender (e);
		}

		#endregion
	}
}
