using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;
using Alnitak.Exceptions;
using Chronos.Core;
using Chronos.Messaging;
using Language;

namespace Alnitak {

	/// <summary>
	/// Controlo para a conquista de planetas
	/// </summary>
	public class ConquerPlanet : UserControl {

		#region Fields

		protected ILanguageInfo info = CultureModule.getLanguage();

		protected Ruler _ruler = null;

		protected ItemsTable _itemsTable = new ItemsTable();

		private bool buttonClicked = false;

		#endregion

		#region Private

		/// <summary>
		/// obtm o Ruler corrente
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

		private void noResults() {
			ItemsTableLine line = new ItemsTableLine();
			ItemsTableText txt = new ItemsTableText( info.getContent("conquerplanet_noPlanet"), "resource" );
			txt.ColumnSpan = 6;
			line.add( txt );
			_itemsTable.addLine( line );
		}

		/// <summary>
		/// preenche o ttulo
		/// </summary>
		private void createTitle( ItemsTable _itemsTable ) {
			ItemsTableLine line = new ItemsTableLine();
			
			line.add( new ItemsTableText( info.getContent("section_fleet"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("coordenada"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("conquerplanet_ruler"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("conquerplanet_newName"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("conquerplanet_conquer"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("conquerplanet_return"), "resourceTitle" ) );

			_itemsTable.HeaderItem = line;
			_itemsTable.TableTitleCssClass = "resourceTitle";
		}

		/// <summary>
		/// Criar a table
		/// </summary>
		private void createTable() {

			_itemsTable.Reset();
			_itemsTable.Title = info.getContent("conquerplanet_title");
			_itemsTable.TitleCssClass = "planetInfoZoneTitle";
			_itemsTable.TableCssClass = "planetFrame";

			createTitle( _itemsTable );

			bool hasResults = false;

			ArrayList fleetsInConquerState = _ruler.FleetsInConquerState();

			foreach( Chronos.Core.Fleet fleet in fleetsInConquerState ) {

					Planet p = Universe.instance.getPlanet( fleet.Coordinate );

					ItemsTableLine line = new ItemsTableLine();
					
					ItemsTableText name = new ItemsTableText( fleet.Name, "resource" );
					ItemsTableText coordinate = new ItemsTableText( fleet.Coordinate.ToString(), "resource" );
					
					
					ItemsTableText rulerName;
					if( p.Owner != null ) {
						rulerName = new ItemsTableText( OrionGlobals.getLink( (Ruler)p.Owner ), "resource" );
					}else {
						rulerName = new ItemsTableText( info.getContent("conquerplanet_none"), "resource" );
					}

					ItemsTableTextBox textbox = new ItemsTableTextBox();
					textbox.CssClass = "resource";
					textbox.CssClass = "textbox";
                    					
					ItemsTableImageButton conquerPlanet = new ItemsTableImageButton( OrionGlobals.getCommonImagePath("ok.gif"), "conquer_" + fleet.Id.ToString()  );
					conquerPlanet.Click += new ImageClickEventHandler(conquerPlanet_Click);

					ItemsTableItem returnPlanet;

					if( p.InitMade ) {
						returnPlanet = new ItemsTableImageButton( OrionGlobals.getCommonImagePath("remove.gif"), "conquer_return" + fleet.Id.ToString() );

						((ItemsTableImageButton)returnPlanet).Click += new ImageClickEventHandler(returnPlanet_Click);
					}else{
						returnPlanet = new ItemsTableText("","resource");
					}

					line.add( name );
					line.add( coordinate );
					line.add( rulerName );
					line.add( textbox );
					line.add( conquerPlanet );
					line.add( returnPlanet );
					line.add( returnPlanet );

					_itemsTable.addLine( line );

					hasResults = true;
			}

			if( !hasResults ) {
				noResults();
			}
		}

		/// <summary>
		/// actualiza a tabela depois de um click
		/// </summary>
		private void updateTable() {
			string name = _itemsTable.getSpecificText( _itemsTable.SelectedIndex, 0 );
            
			IEnumerator iter = _itemsTable.Controls.GetEnumerator();
			while( iter.MoveNext() ) {
				ItemsTableLine line = (ItemsTableLine)iter.Current;
				string str = ((ItemsTableItem)line.Controls[0]).Item;
				if( str == name ) {
					_itemsTable.removeLine( line );
					iter = _itemsTable.Controls.GetEnumerator();
				}
			}
							
			if( _itemsTable.Count == 0 ) {
				noResults();
			}
		}

		/// <summary>
		/// actualiza a tabela no PreRender quando outro controlo fez um post
		/// </summary>
		private void preRenderUpdateTable() {
			/*foreach( Chronos.Core.Fleet fleet in _ruler.UniverseFleets.Values ) {
				if( fleet.IsMoving ) {
					string name = fleet.Name;
					IEnumerator iter = _itemsTable.Controls.GetEnumerator();
					while( iter.MoveNext() ) {
						ItemsTableLine line = (ItemsTableLine)iter.Current;
						string str = ((ItemsTableItem)line.Controls[0]).Item;
						if( str == name ) {
							_itemsTable.removeLine( line );
							iter = _itemsTable.Controls.GetEnumerator();
						}
					}
				}
			}
									
			if( _itemsTable.Count == 0 ) {
				noResults();
			}*/
			_itemsTable.removeAllLines();
			createTable();
		}

		#endregion

		#region Events

		private void conquerPlanet_Click(object sender, ImageClickEventArgs e) {
			if( _ruler.Planets.Length >= _ruler.MaxPlanets ) {
				Information.AddError(info.getContent( "conquerplanet_maxPlanetsError" ));
				return;
			}
			
			int index = _itemsTable.SelectedIndex;
			string fleetName  = _itemsTable.getSpecificText( index, 0 );
			string planetNewName = GetPlanetNewName(index);

			Chronos.Core.Fleet fleet = _ruler.getFleet(fleetName);
			if( fleet == null ) {
				throw new Exception("There is no fleet!");
			}

			Coordinate c = Coordinate.translateCoordinate( _itemsTable.getSpecificText( index, 1 ) );
			
			//Planet p = Universe.instance.getPlanet( c );
			
			if( !fleet.isQuantityAvailable("ColonyShip", 1) ) {
				Information.AddError(info.getContent( "conquerplanet_thereisnoColonyShip" ));
				return;
			}
			
			if( Regex.IsMatch( planetNewName, @"(\w|( )|[.!?])+" ) && !_ruler.hasFleet( planetNewName ) ) {
				Universe.instance.conquerPlanet( c, planetNewName, fleet );
				updateTable();
				buttonClicked = true;
			}else{
				Information.AddError(info.getContent( "conquerplanet_invalidName" ));
			}
		}

		private string GetPlanetNewName(int index) {
			return _itemsTable.getSpecificText( index, 3 );
		}

		private void returnPlanet_Click(object sender, ImageClickEventArgs e) {
			int index = _itemsTable.SelectedIndex;
			Coordinate c = Coordinate.translateCoordinate( _itemsTable.getSpecificText( index, 1 ) );

			Planet p = Universe.instance.getPlanet( c );
			
			if( null == p.Owner && null != p.OldOwner ) {
				Universe.instance.changePlanetOwner( p, p.OldOwner );
			}

			updateTable();
			buttonClicked = true;
		}

		#endregion

		#region Control Events
		
		protected override void OnInit(EventArgs e) {
			
			_ruler = getRuler();

			createTable();
			
			Controls.Add( _itemsTable );
			
			OrionGlobals.RegisterRequest(MessageType.Battle, info.getContent("section_military"));
			
			base.OnInit( e );
		}

		protected override void OnPreRender(EventArgs e) {
			if( Page.IsPostBack /*&& !buttonClicked  */ ) {
				preRenderUpdateTable();
				buttonClicked = false;
			}
			
			base.OnPreRender (e);
		}
	
		#endregion

	}
}
