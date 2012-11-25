using System;
using System.Collections;
using System.Web.UI;
using Alnitak.Exceptions;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Messaging;
using Language;

namespace Alnitak {

	/// <summary>
	/// Lista das Batalhas correntes
	/// </summary>
	public class CurrentBattles : Control	{

		#region Private Fields

		protected ILanguageInfo info = CultureModule.getLanguage();
		
		protected Ruler ruler = null;
		
		private BattleType type = Chronos.Battle.BattleType.BATTLE;

		private string _title = null;

		#endregion

		#region Properties

		public string BattleType {
			set { type = (BattleType)Chronos.Battle.BattleType.Parse( typeof(BattleType), value.ToUpper() ); }
		}

		public string TitleRef {
			get{ return _title; }
			set{ _title = value; }
		}

		#endregion

		#region Private Methods

		/// <summary>Retorna o Ruler da sessão</summary>
		private Ruler getRuler() {
			User user = Page.User as User;
			if( user == null )
				throw new AlnitakException("User não está autenticado @ CurrentBattles::getRuler");

			Ruler ruler = Universe.instance.getRuler(user.RulerId);
			if( ruler == null )
				throw new AlnitakException("Ruler não existe @ CurrentBattles::getRuler");
			return ruler;
		}

		private void createTitle( ItemsTable _itemsTable ) {
			ItemsTableLine line = new ItemsTableLine();
			line.add( new ItemsTableText( info.getContent("currentBattle_turn"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("currentBattle_opponent"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("online"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battle_type"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("currentBattle_status"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("currentBattle_timeout"), "resourceTitle" ) );
			
			if( type == Chronos.Battle.BattleType.BATTLE ) {
				line.add( new ItemsTableText( info.getContent("currentBattle_location"), "resourceTitle" ) );
			}

			ItemsTableText timeLeft = new ItemsTableText( info.getContent("currentBattle_timeLeft"), "resourceTitle" );
			timeLeft.Width = "150px";
			line.add( timeLeft );
			

			_itemsTable.HeaderItem = line;
			_itemsTable.TableTitleCssClass = "resourceTitle";
		}

		private string getPlanetLink( Planet p ) {
			return string.Format("<a href='{0}?id={1}'>{2}</a>",OrionGlobals.getSectionBaseUrl( "planet" ),p.Id,p.Name);
		}
        
		/// <summary>
		///
		/// </summary>
		private ItemsTable CreateBattles() {
			
			ICollection battles = ruler.GetAllBattles( type );

			ItemsTable _itemsTable = new ItemsTable();
			
			if( TitleRef == null ) {
				_itemsTable.Title = info.getContent("currentBattle_title");
			}else {
				_itemsTable.Title = info.getContent(TitleRef);
			}

			_itemsTable.TitleCssClass = "planetInfoZoneTitle";
			_itemsTable.TableCssClass = "planetFrame";

			createTitle( _itemsTable );

            if( battles != null && battles.Count > 0 ) {
				
				string onlineImage = OrionGlobals.getCommonImagePath("online.gif");
				string offlineImage = OrionGlobals.getCommonImagePath("offline.gif");

				IEnumerator iter = battles.GetEnumerator( );
				while( iter.MoveNext() ) {
					SimpleBattleInfo battleInfo = iter.Current as SimpleBattleInfo;

					if( null == battleInfo) 
						continue;

					if( !battleInfo.Accepted ) {
						continue;
					}

					ItemsTableLine line = new ItemsTableLine();
					
					ItemsTableText turn = new ItemsTableText( battleInfo.CurrentTurn.ToString(  ), "resource" );

					ItemsTableText opponent = new ItemsTableText( OrionGlobals.getLink( battleInfo.Enemy ) , "resource" );

					string onlineState;

					// indicar se o utilizador está online
					if( OrionGlobals.isUserOnline(battleInfo.Enemy.ForeignId) ) {
						onlineState = string.Format("<img src='{0}' />", onlineImage);
					} else {
						onlineState = string.Format("<img src='{0}' />", offlineImage);
					}

					ItemsTableText opponentStatus = new ItemsTableText( onlineState , "resource" );

					ItemsTableText battleType = new ItemsTableText( info.getContent(battleInfo.Type) , "resource" );

					ItemsTableItem status;
					string rulerTurn;
					if( battleInfo.IsPositionTime ) {
						rulerTurn = info.getContent("currentBattle_rulerPosition");
					}else {
						if( battleInfo.IsTurn ) {
							if( battleInfo.EnemyIsPositionTime ) {
								rulerTurn = info.getContent("currentBattle_enemyPositioning");
							}else {
								rulerTurn = info.getContent("currentBattle_rulerTurn");
							}
						}else{
							rulerTurn = info.getContent("currentBattle_enemyTurn");
						}
					}

					status = new ItemsTableLink(
						rulerTurn,
						OrionGlobals.calculatePath( "battle.aspx?id=" + battleInfo.BattleId ),
						"resource"
					);

					ItemsTableText timeLeft = new ItemsTableText( modifyTime(battleInfo.TurnsLeft.ToString()) , "resource" );

					ItemsTableText coordPlanet = null;
					if( type == Chronos.Battle.BattleType.BATTLE ) {
						if( battleInfo.IsPlanet  ) {
							Planet p = Universe.instance.getPlanet( battleInfo.Coordinate );
							coordPlanet = new ItemsTableText( getPlanetLink( p ), "resource" );
						} else {
							coordPlanet = new ItemsTableText( battleInfo.Coordinate.ToString(), "resource" );
						}
					}

					ItemsTableText timeout = new ItemsTableText( battleInfo.MissedTurns.ToString() , "resource" );
						
					line.add( turn );
					line.add( opponent );
					line.add( opponentStatus );
					line.add( battleType );
					line.add( status );
					line.add( timeout );

					if( type == Chronos.Battle.BattleType.BATTLE ) {
						line.add( coordPlanet );
					}

					line.add( timeLeft );
					_itemsTable.addLine( line );
				}
			}


			if( !_itemsTable.HasControls() ) {
				ItemsTableLine line = new ItemsTableLine();
				ItemsTableText txt = new ItemsTableText( info.getContent("currentBattle_noBattles"), "resource" );
				
				if( type == Chronos.Battle.BattleType.BATTLE ) {
					txt.ColumnSpan = 8;
				}else {
					txt.ColumnSpan = 7;
				}

				line.add( txt );
				_itemsTable.addLine( line );
			}

			return _itemsTable;
		}

		private string modifyTime( string time ) {
			if( time.IndexOf("d")!= -1 )
				time = time.Replace("d", info.getContent("d") );
			return time;
		}

		#endregion

		#region Events
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			OrionGlobals.RegisterRequest(MessageType.Battle, info.getContent("section_battle"));
		}
		
		protected override void OnPreRender(EventArgs e) {
			ruler = getRuler();
			
			this.Controls.Add( CreateBattles() );
			
			base.OnPreRender(e);
		}


		#endregion
	}
}
