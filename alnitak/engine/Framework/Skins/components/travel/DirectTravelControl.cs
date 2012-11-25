using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;

using Chronos.Core;

namespace Alnitak {
	
	public class DirectTravelControl: TravelControlBase {

		#region Fields
		
		protected DropDownList galaxy;
		protected DropDownList system;
		protected DropDownList sector;
		protected DropDownList planet;

		protected System.Web.UI.WebControls.Label prevGalaxy;
		protected System.Web.UI.WebControls.Label prevSystem;
		protected System.Web.UI.WebControls.Label prevSector;
		protected System.Web.UI.WebControls.Label test;

		protected string _currentCoordinate;

		private char[] spliter = new char[]{':'};
		private string[] coord;

		#endregion

		#region Private Methods

		private void insertDropDownData( DropDownList drop, int lenght, int coord ) {
			drop.Items.Clear();

			ListItem item = new ListItem("1","1");
			if( coord == 1 )
				item.Selected = true;

			item.Attributes.Add("onClick","alert('buu');");

			drop.Items.Add( item );

			for( int i = 2; i <= lenght ; ++i ) {
				item = new ListItem(i+"",i+"");
				if( i == coord ) {
					item.Selected = true;
				}
				item.Attributes.Add("onClick","alert('buu');");
				drop.Items.Add( item );

			}
		}

		private void populateDropDownLists( ) {
			insertDropDownData( galaxy, Chronos.Core.Coordinate.MaximumGalaxies, int.Parse( coord[0] ) );
			insertDropDownData( system, Chronos.Core.Coordinate.MaximumSystems, int.Parse( coord[1] ) );
			insertDropDownData( sector, Chronos.Core.Coordinate.MaximumSectors, int.Parse( coord[2] ) );
			insertDropDownData( planet, Chronos.Core.Coordinate.MaximumPlanets, int.Parse( coord[3] ) );
		}

		private void initDropDownLists() {
			if( (4 - _maximumAvailableControl) != 0 ) {
				DropDownList[] drop = new DropDownList[]{ galaxy, system, sector };
				System.Web.UI.WebControls.Label[] labels = new System.Web.UI.WebControls.Label[]{ prevGalaxy , prevSystem , prevSector };
				
				if( _maximumAvailableControl != 0 ) {
					for( int i = 0 ; i < (4 - _maximumAvailableControl) ; ++i ) {
						drop[i].Visible = false;
						labels[i].Text = coord[i];
					}
				}
			}
		}

		/// <summary>
		/// obtém o valor de um dos peda~ços da coordenada consuante a parte
		/// que está disponível
		/// </summary>
		/// <param name="d">DropList</param>
		/// <param name="l">Label</param>
		private string getCoordValue( DropDownList d, System.Web.UI.WebControls.Label l ) {
			if( d.Visible ) {
				return d.SelectedValue;
			} else {
				return l.Text;
			}
		}

		#endregion

		#region Properties

		public Coordinate Coordinate {
			get{
				string coord = getCoordValue( galaxy, prevGalaxy ) + ":";
				coord += getCoordValue( system, prevSystem ) + ":";
				coord += getCoordValue( sector, prevSector ) + ":";
				coord += planet.SelectedValue;
				return Chronos.Core.Coordinate.translateCoordinate( coord );
			}
		}

		#endregion
		
		#region Overriden

		protected override void OnInit(EventArgs e) {
			base.OnInit(e);

			if( _maximumAvailableControl == 0 ) {
				return;
			}

			coord = StartCoordinate.ToString().Split(spliter);

            populateDropDownLists();
			initDropDownLists();
		}

		protected override void OnPreRender(EventArgs e) {
			base.OnPreRender (e);
		}

		#endregion
	}
}
