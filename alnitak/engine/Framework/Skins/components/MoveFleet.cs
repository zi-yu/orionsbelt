using System;
using System.Collections;
using System.Web.UI;
using Alnitak.Exceptions;
using Chronos.Core;
using Language;

namespace Alnitak {
	/// <summary>
	/// Classe responsvel por fazer uma fleet mover-se
	/// </summary>
	public class MoveFleet : UserControl {

		#region fields

		private ILanguageInfo info = CultureModule.getLanguage();
		private Ruler _ruler = null;
		private string _message = null;
		
		protected Travel travel; 
		protected ItemsTable itemsTable;
		
		#endregion
		
		#region private

		/// <summary>
		/// obtém o Ruler corrente
		/// </summary>
		private Ruler getRuler() {
			User user = Page.User as User;
			if( user == null )
				throw new AlnitakException("User no est autenticado @ ConquerPlanet::OnInit");

			_ruler = Universe.instance.getRuler(user.RulerId);
			if( _ruler == null )
				throw new AlnitakException("Ruler no existe @ ConquerPlanet::OnInit");

			return _ruler;
		}

		/// <summary>
		/// obtm todas as fleets que se podem mover
		/// </summary>
		/// <returns>ArrayList com as fleets que se podem mover</returns>
		private ArrayList getMoveableFleets() {	
			IDictionaryEnumerator iter;
			ArrayList names = new ArrayList();
			foreach( Planet planet in _ruler.Planets ) {
				iter = planet.Fleets.GetEnumerator();	
				while( iter.MoveNext() ) {
					Chronos.Core.Fleet f = (Chronos.Core.Fleet)iter.Value;
					if( f.CanBeMoved ) {
						names.Add( f.Name );
					}
				}
			}
			
			iter = _ruler.UniverseFleets.GetEnumerator();
			while( iter.MoveNext() ) {
				Chronos.Core.Fleet f = (Chronos.Core.Fleet)iter.Value;
				if( f.CanBeMoved ) {
					names.Add( f.Name );
				}
			}

			names.Sort();
			
			return names;
		}

		/// <summary>
		/// preenche o título
		/// </summary>
		private void createTitle( ItemsTable itemsTable ) {
			ItemsTableLine line = new ItemsTableLine();
			
			line.add( new ItemsTableText( info.getContent("fleet_fleetToMove"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("coordenada"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("fleet_moveFleet"), "resourceTitle" ) );

			itemsTable.HeaderItem = line;
			itemsTable.TableTitleCssClass = "resourceTitle";
		}

		private void addLine( string text, int span ) {
			addLine( text, span, null );
		}

		private void addLine( string text, int span, string css ) {
			ItemsTableLine line = new ItemsTableLine();
			ItemsTableText l = new ItemsTableText( text , "resource" );
			if( css != null ) {
				l.CssClass += " " + css;
			}
			l.ColumnSpan = span;
			line.add( l );
			itemsTable.addLine( line );
		}


		/// <summary>
		/// Criar a tabela com as naves e o 
		/// </summary>
		private void createTable() {
						
			itemsTable.Title = info.getContent("fleet_moveFleet");
			itemsTable.TitleCssClass = "planetInfoZoneTitle";
			itemsTable.TableCssClass = "planetFrame";

			createTitle( itemsTable );

			bool hasResults = false;

			if( travel.IsTravelAvailable ) {
				foreach( string fleet in getMoveableFleets() ) {
					
					ItemsTableLine line = new ItemsTableLine();
					
					ItemsTableText fleetName = new ItemsTableText( fleet , "resource" );
					
					ItemsTableText coord = new ItemsTableText( _ruler.getFleet(fleet).Coordinate.ToString() , "resource" );
					
					ItemsTableImageButton moveFleet = new ItemsTableImageButton( OrionGlobals.getCommonImagePath("move.gif"), "move_" +  _ruler.getFleet( fleet ).Id.ToString()  );
					moveFleet.Click += new ImageClickEventHandler(moveFleet_Click);
					
					line.add( fleetName );
					line.add( coord );
					line.add( moveFleet );
					
					itemsTable.addLine( line );

					hasResults = true;
				}

				if( !hasResults ) {
					addLine( info.getContent("fleet_noFleetsToMove"), 3 );
					travel.Visible = false;
				} else {
					addLine( info.getContent( "fleet_help" ), 3 );
				}
			}else{
				travel.Visible= false;
				addLine( info.getContent( "fleet_moveNotAvailable" ), 3 );
			}
		}

		/// <summary>
		/// actualiza a tabela depois de um click
		/// </summary>
		private void updateTable() {
			string fleetName = itemsTable.getSpecificText( itemsTable.SelectedIndex, 0 );
            
			IEnumerator iter = itemsTable.Controls.GetEnumerator();
			while( iter.MoveNext() ) {
				ItemsTableLine line = (ItemsTableLine)iter.Current;
				string str = ((ItemsTableItem)line.Controls[0]).Item;
				if( str == fleetName ) {
					itemsTable.removeLine( line );	
					iter = itemsTable.Controls.GetEnumerator();
				}
			}
							
			if( itemsTable.Count == 0 ) {
				addLine( info.getContent("fleet_noFleetsToMove"), 3 );
				travel.Visible = false;
			}
		}

		#endregion
		
		#region events

		protected override void OnLoad(EventArgs e) {
			_ruler = getRuler();

			createTable();
		
			Controls.Add( itemsTable );

			base.OnLoad(e);
		}

		protected override void OnPreRender(EventArgs e) {
			if( _message != null ) {
				addLine( _message , 3 );
				_message = null;
			}
		
			base.OnPreRender (e);
		}

		#endregion

		#region webcontrol events

		/// <summary>
		/// evento para mover a fleet
		/// </summary>
		private void moveFleet_Click(object sender, ImageClickEventArgs e) {
			int index = itemsTable.SelectedIndex;

			Chronos.Core.Fleet fleet = _ruler.getFleet( itemsTable.getSpecificText( index, 0 ) );
			
			if( fleet == null )
				throw new AlnitakException("Fleet é null no evento de Click @ MoveFleet::moveFleet_Click");

			Coordinate coordinate = travel.Coordinate;
            
			if( fleet.Coordinate.CompareTo( coordinate ) == 0  ) {
				_message = info.getContent("fleet_alreadAtCoordinate");
			} else {
				if( !fleet.startMoving( coordinate ) ) {
					_message  = string.Format( info.getContent("fleet_cantMove"), coordinate.ToString() );
				} else {
					updateTable();
				}
			}
		}

		#endregion

		
	}
}
