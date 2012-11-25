using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Persistence;
using Language;

namespace Alnitak.Battle {
	
	public class BattleAdminUser : UserControl {

		#region Private Fields

		protected ILanguageInfo info = CultureModule.getLanguage();

		protected ItemsTable battle;
		protected ItemsTable tournament;
		protected ItemsTable friendly;

		protected TextBox userId;
		protected Button set;

		#endregion

		#region Private Methods

		#region Battle Type 1

		/// <summary>
		/// preenche o título
		/// </summary>
		private void CreateTitle( ItemsTable itemsTable ) {
			ItemsTableLine line = new ItemsTableLine();
			line.add( new ItemsTableText( "Id", "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_enemy"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_view"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_winner"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_winner"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_remove"), "resourceTitle" ) );
			itemsTable.HeaderItem = line;
			itemsTable.TableTitleCssClass = "resourceTitle";
		}

		private void FillBattle( Ruler ruler, ItemsTable table, string tableTitle, Chronos.Battle.BattleType type ) {
			table.Title = tableTitle;
			table.TitleCssClass = "planetInfoZoneTitle";
			table.TableCssClass = "planetFrame";

			CreateTitle(table);

			if( ruler == null ) {
				NoBattle( table, 6 );
				return;
			}

			ICollection collection = ruler.GetAllBattles( type );

			if( collection.Count == 0 ) {
				NoBattle( table, 6 );
				return;
			}
			
			IEnumerator iter = collection.GetEnumerator( );

			while( iter.MoveNext( ) ) {
				ItemsTableLine line = new ItemsTableLine();

				SimpleBattleInfo bInfo = (SimpleBattleInfo)iter.Current;

				ItemsTableText id = new ItemsTableText( bInfo.BattleId.ToString(), "resource" );
				ItemsTableText enemy = new ItemsTableText( OrionGlobals.getLink( bInfo.Enemy ), "resource" );

				ItemsTableLink link = new ItemsTableLink(
						info.getContent( "battleAdminUser_viewBattle" ),
						OrionGlobals.calculatePath( string.Format("battle.aspx?id={0}&rulerid={1}",bInfo.BattleId,ruler.Id) ),
						"resource"
				);

				ItemsTableLinkButton linkButton1 = new ItemsTableLinkButton( bInfo.Owner.Name );
                linkButton1.Click += new EventHandler(linkButton1_Click);

				ItemsTableLinkButton linkButton2 = new ItemsTableLinkButton( bInfo.Enemy.Name );
				linkButton2.Click += new EventHandler(linkButton2_Click);

				ItemsTableImageButton remove = new ItemsTableImageButton( OrionGlobals.getCommonImagePath( "remove.gif" ), "battle_" + bInfo.BattleId );
				remove.Click += new ImageClickEventHandler(remove_Click);

				line.add( id );
				line.add( enemy );
				line.add( link );
				line.add( linkButton1 );
				line.add( linkButton2 );
				line.add( remove );

				table.addLine( line );
			}

			this.Controls.Add( table );
		}

		#endregion

		#region Battle Type 2

		/// <summary>
		/// preenche o título
		/// </summary>
		private void CreateTitle2( ItemsTable itemsTable ) {
			ItemsTableLine line = new ItemsTableLine();
			line.add( new ItemsTableText( "Id", "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_enemy"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_view"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_winner"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_winner"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("coordenada"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_remove"), "resourceTitle" ) );
			itemsTable.HeaderItem = line;
			itemsTable.TableTitleCssClass = "resourceTitle";
		}

		private void FillBattle2( Ruler ruler, ItemsTable table, string tableTitle, Chronos.Battle.BattleType type ) {
			table.Title = tableTitle;
			table.TitleCssClass = "planetInfoZoneTitle";
			table.TableCssClass = "planetFrame";

			CreateTitle2(table);

			if( ruler == null ) {
				NoBattle( table, 7 );
				return;
			}

			ICollection collection = ruler.GetAllBattles( type );

			if( collection.Count == 0 ) {
				NoBattle( table, 7 );
				return;
			}
			
			IEnumerator iter = collection.GetEnumerator( );

			while( iter.MoveNext( ) ) {
				ItemsTableLine line = new ItemsTableLine();

				SimpleBattleInfo bInfo = (SimpleBattleInfo)iter.Current;

				ItemsTableText id = new ItemsTableText( bInfo.BattleId.ToString(), "resource" );
				ItemsTableText enemy = new ItemsTableText( OrionGlobals.getLink( bInfo.Enemy ), "resource" );

				ItemsTableLink link = new ItemsTableLink(
						info.getContent( "battleAdminUser_viewBattle" ),
						OrionGlobals.calculatePath( string.Format("battle.aspx?id={0}&rulerid={1}",bInfo.BattleId,ruler.Id) ),
						"resource"
				);

				ItemsTableText coord = new ItemsTableText( bInfo.Coordinate.ToString( ), "resource" );

				ItemsTableLinkButton linkButton1 = new ItemsTableLinkButton( bInfo.Owner.Name );
				linkButton1.Click += new EventHandler(linkButton1_Click);

				ItemsTableLinkButton linkButton2 = new ItemsTableLinkButton( bInfo.Enemy.Name );
				linkButton2.Click += new EventHandler(linkButton2_Click);

				ItemsTableImageButton remove = new ItemsTableImageButton( OrionGlobals.getCommonImagePath( "remove.gif" ), "battle_" + bInfo.BattleId );
				remove.Click += new ImageClickEventHandler(remove_Click);

				line.add( id );
				line.add( enemy );
				line.add( link );
				line.add( linkButton1 );
				line.add( linkButton2 );
				line.add( coord );
				line.add( remove );

				table.addLine( line );
			}

			this.Controls.Add( table );
		}

		#endregion

		private void NoBattle( ItemsTable table, int span ) {
			ItemsTableLine line = new ItemsTableLine();
			ItemsTableText text = new ItemsTableText(info.getContent("currentBattle_noBattles"),"resource");
			text.ColumnSpan = span;
			line.add( text );
			table.addLine( line );
		}

		private void UpdateTable(ItemsTable itemsTable, int battleId, int span) {
			IEnumerator iter = itemsTable.Controls.GetEnumerator();
			while( iter.MoveNext() ) {
				ItemsTableLine line = (ItemsTableLine)iter.Current;
				string str = ((ItemsTableItem)line.Controls[0]).Item;
				if( str == battleId.ToString() ) {
					itemsTable.removeLine( line );	
					iter = itemsTable.Controls.GetEnumerator();
				}
			}
	
			if( itemsTable.Count == 0 ) {
				NoBattle( itemsTable, span );
			}
		}

		/// <summary>
		/// actualiza a tabela depois de um click
		/// </summary>
		private void update( ItemsTable itemsTable, int span ) {
			if( itemsTable.SelectedIndex != -1 ) {
				int battleId = int.Parse(itemsTable.getSpecificText( itemsTable.SelectedIndex, 0 ));
                Universe.instance.RemoveBattle( battleId );
				UpdateTable(itemsTable, battleId, span);
				Information.AddInformation( "batalha com o id " + battleId + " removida");
			}
		}

		private void update2( ItemsTable itemsTable, int span ) {
			if( itemsTable.SelectedIndex != -1 ) {
				int battleId = int.Parse(itemsTable.getSpecificText( itemsTable.SelectedIndex, 0 ));
				if( battleId > 0 ) {
					BattlePersistence.Instance.RemoveBattle( battleId );
					Ruler r = GetRuler();
					r.RemoveBattle(battleId, BattleType.FRIENDLY);
				}
				UpdateTable(itemsTable, battleId, span);
				Information.AddInformation( "batalha com o id " + battleId + " removida");
			}
		}

		private void SetWinner( ItemsTable itemsTable, int i, int span ) {
			if( itemsTable.SelectedIndex != -1 ) {
				int battleId = int.Parse(itemsTable.getSpecificText( itemsTable.SelectedIndex, 0 ));
				Ruler r = GetRuler();
				if( null == r ) {
					Information.AddError(info.getContent("battleAdminUser_norulerid"));
					return;
				}

				BattleInfo battleInfo = Universe.instance.GetBattle(battleId);
				
				if( r.GetBattle(battleId,battleInfo.BattleType) == null ) {
					Information.AddError(info.getContent("battleAdminUser_invalidBattleOwner"));
					return;
				}

				string winner = "";
				if( i == 1 ) {
					Ruler enemy = Universe.instance.getRuler(battleInfo.GetEnemyBattleInfo(r).OwnerId);
					battleInfo.ForceEndBattle(enemy);
					winner = itemsTable.getSpecificText( itemsTable.SelectedIndex, 3 );
				}else {
					battleInfo.ForceEndBattle(r);
					winner = itemsTable.getSpecificText( itemsTable.SelectedIndex, 4 );
				}

                Information.AddInformation( string.Format(info.getContent("battleAdminUser_setWinnerDone"),battleId,winner) );

				UpdateTable(itemsTable, battleId, span);
			}
		}

		

		private Ruler GetRuler() {
			Ruler r = null;
			int id = -1;
			if( userId.Text != string.Empty && OrionGlobals.isInt( userId.Text ) ) {
				id = int.Parse(userId.Text);
			}else {
				string tmp = HttpContext.Current.Request.QueryString["id"];
				if( tmp != null && tmp != string.Empty && OrionGlobals.isInt( tmp ) ) {
					id = int.Parse(tmp);
				}
			}
	
			if( id != -1 && Universe.instance.rulers.ContainsKey( id ) ) {
				r = Universe.instance.getRuler( id );	
			}
			return r;
		}

		private void FillRulersBattles() {
			Ruler r = GetRuler();

			FillBattle2( r, battle, info.getContent( "battleAdminUser_battle" ), Chronos.Battle.BattleType.BATTLE );
			FillBattle( r, tournament, info.getContent( "battleAdminUser_tournament" ),Chronos.Battle.BattleType.TOURNAMENT );
			FillBattle( r, friendly, info.getContent( "battleAdminUser_friendly" ),Chronos.Battle.BattleType.FRIENDLY );
		}

		

		#endregion

		#region Control Events

		protected override void OnLoad(EventArgs e) {
			FillRulersBattles();
			base.OnLoad (e);
		}

		#endregion

		#region Events

		private void remove_Click(object sender, ImageClickEventArgs e) {
			update(battle,7);
			update(tournament,6);
			update(friendly,6);
		}

		private void linkButton1_Click(object sender, EventArgs e) {
			SetWinner(battle,1,7);
			SetWinner(tournament,1,6);
			SetWinner(friendly,1,6);
		}

		private void linkButton2_Click(object sender, EventArgs e) {
			SetWinner(battle,2,7);
			SetWinner(tournament,2,6);
			SetWinner(friendly,2,6);
		}

		#endregion

	}
}