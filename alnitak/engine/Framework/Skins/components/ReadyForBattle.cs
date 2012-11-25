using Chronos.Battle;

namespace Alnitak {

	using System;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Collections;

	using Alnitak.Exceptions;

	using Chronos.Core;
	using Language;

	/// <summary>
	/// 
	/// </summary>
	public class ReadyForBattle : Control {

		#region Private Fields

		protected ILanguageInfo info = CultureModule.getLanguage();
		
		protected Ruler _ruler = null;

		protected ItemsTable _itemsTable = new ItemsTable();

		#endregion

		#region Private Methods

		/// <summary>Retorna o Ruler da sessão</summary>
		private Ruler getRuler() {
			User user = Page.User as User;
			if( user == null )
				throw new AlnitakException("User não está autenticado @ ReadyForBattle::getRuler");

			Chronos.Core.Ruler _ruler = Universe.instance.getRuler(user.RulerId);
			if( _ruler == null )
				throw new AlnitakException("Ruler não existe @ ReadyForBattle::getRuler");

			return _ruler;
		}

		private void insertNoFleets() {
			ItemsTableLine line = new ItemsTableLine();
			ItemsTableText item = new ItemsTableText( info.getContent("battle_noFleets"), "resource" );
			item.ColumnSpan = 4;
			line.add( item );
			_itemsTable.addLine( line );
		}

		private void updateTable() {
			_itemsTable.removeLine( _itemsTable.SelectedIndex );
			if( _itemsTable.Count == 0 ) {
				insertNoFleets();
			}
		}
		
		/// <summary>
		/// preenche o título
		/// </summary>
		private void createTitle( ItemsTable _itemsTable ) {
			ItemsTableLine line = new ItemsTableLine();
			line.add( new ItemsTableText( info.getContent("section_fleet"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("coordenada"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("conquerplanet_ruler"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battle_startBattle"), "resourceTitle" ) );
			_itemsTable.HeaderItem = line;
			//_itemsTable.TableTitleCssClass = "resourceTitle";
		}


		private void CreateFleets() {
			Hashtable universeFleets = _ruler.UniverseFleets;
						
			_itemsTable.Title = info.getContent("battle_fleetsReadyToBattle");
			_itemsTable.TitleCssClass = "planetInfoZoneTitle";
			_itemsTable.TableCssClass = "planetFrame";

			createTitle( _itemsTable );
			bool isOneReady = false;
			
			if( universeFleets != null && universeFleets.Count > 0 ) {
				IDictionaryEnumerator iter = universeFleets.GetEnumerator();
				
				while( iter.MoveNext() ){
					Chronos.Core.Fleet f = (Chronos.Core.Fleet)iter.Value;
					Planet p = Universe.instance.getPlanet(f.Coordinate);

					if( f.GoodForBattle ) {
						ItemsTableLine line = new ItemsTableLine();
					
						ItemsTableText fleet = new ItemsTableText( f.Name, "resource" );
						ItemsTableText coord = new ItemsTableText( f.Coordinate.ToString(), "resource" );
						
						ItemsTableText rulerName;
						if( p.Owner != null ) {
							rulerName = new ItemsTableText( OrionGlobals.getLink( (Ruler)p.Owner ), "resource" );
						}else {
							rulerName = new ItemsTableText( info.getContent("conquerplanet_none"), "resource" );
						}

						ItemsTableItem action;
						
						if( p.IsInBattle ) {
							action = new ItemsTableText( info.getContent( "battle_isInBattle" ), "resource" );
						} else {
							if( p.HasImmunity ) {
								action = new ItemsTableText( string.Format( info.getContent("BattleImmunity"), p.Immunity), "resource" );
							}else{
								if( p.Owner != null && Ruler.IsSameAlliance( (Ruler)p.Owner, (Ruler)f.Owner) ) {
									action = new ItemsTableText( info.getContent("battle_sameAlliance"), "resource" );
								}else{
									if( !p.HasProtection ) {
										action = new ItemsTableText( info.getContent("planet_not_protected"), "resource" );
									}else{
										action = new ItemsTableImageButton( OrionGlobals.getCommonImagePath("ok.gif"), fleet.ID );
										((ItemsTableImageButton)action).Click += new ImageClickEventHandler( submit_Click );
									}
								}
							}
						}
												
						line.add( fleet );
						line.add( coord );
						line.add( rulerName );
						line.add( action );

						_itemsTable.addLine( line );
						
						isOneReady = true;
					}
				}
			}

			if( !isOneReady ) {
				insertNoFleets();
			}
		}
		
		#endregion
	
		#region Control events

		protected override void OnInit(EventArgs e) {
			
			_ruler = getRuler();
			
			CreateFleets();

			Controls.Add( _itemsTable );
			
			base.OnInit( e );
		}


		#endregion

		#region button events

		private void submit_Click(object sender, ImageClickEventArgs e) {
			string fleetName = _itemsTable.getSpecificText( _itemsTable.SelectedIndex, 0 );
			Chronos.Core.Fleet fleet = _ruler.getFleet( fleetName );
			if( fleet == null )
				throw new AlnitakException("Fleet é null no evento de Click @ ReadyForBattle::toBattle_Click");
			
			Chronos.Core.Planet currentPlanet = Universe.instance.getPlanet( fleet.Coordinate );
			if( currentPlanet == null )
				throw new AlnitakException("O planeta é null no evento de Click @ ReadyForBattle::toBattle_Click");
				
			Ruler enemy = currentPlanet.Owner as Ruler;
			if( enemy == null )
				throw new AlnitakException("O planeta onde a fleet " + fleetName +" está não tem dono");

			if( !currentPlanet.IsInBattle ) {
				Universe.instance.CreateBattle(_ruler, fleet, enemy, currentPlanet, BattleType.BATTLE, "totalannihilation" );
				updateTable();
			}
		}

		#endregion

		
	}
}
