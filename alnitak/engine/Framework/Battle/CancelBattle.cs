using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Alnitak.Exceptions;
using Chronos.Battle;
using Chronos.Core;
using Language;

namespace Alnitak.Battle {
	
	public class CancelBattle : UserControl {
	
		#region Private Fields

		protected ILanguageInfo info = CultureModule.getLanguage();
		protected ItemsTable itemsTable = new ItemsTable();
		protected Ruler _ruler = null;

		#endregion

		#region Private Methods

		/// <summary>Retorna o Ruler da sessão</summary>
		private Ruler getRuler() {
			User user = Page.User as User;
			if( user == null )
				throw new AlnitakException("User não está autenticado @ ReadyForBattle::getRuler");

			Ruler _ruler = Universe.instance.getRuler(user.RulerId);
			if( _ruler == null )
				throw new AlnitakException("Ruler não existe @ ReadyForBattle::getRuler");

			return _ruler;
		}

		/// <summary>
		/// preenche o título
		/// </summary>
		private void createTitle( ItemsTable itemsTable) {
			ItemsTableLine line = new ItemsTableLine();
			line.add( new ItemsTableText( "#", "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("cancelBattle_ruler"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("battleAdminUser_battle"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("cancelBattle_accept"), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("cancelBattle_reject"), "resourceTitle" ) );
			itemsTable.HeaderItem = line;
		}

		private void CreateUnits() {
			itemsTable.Title = info.getContent("cancelBattle_acceptReject");
			itemsTable.TitleCssClass = "planetInfoZoneTitle";
			itemsTable.TableCssClass = "planetFrame";

			createTitle( itemsTable );

			ICollection battles = _ruler.GetAllBattles(BattleType.FRIENDLY);
			
			foreach( SimpleBattleInfo battleInfo in battles ) {
				if( battleInfo.Accepted ) {
					continue;
				}

				ItemsTableLine line = new ItemsTableLine();

				ItemsTableText id = new ItemsTableText( battleInfo.BattleId.ToString(), "resource" );
				ItemsTableText ruler = new ItemsTableText( OrionGlobals.getLink( battleInfo.Enemy ), "resource" );

				ItemsTableLink view = new ItemsTableLink(
					info.getContent("currentBattle_viewBattle"),
					OrionGlobals.calculatePath( "battle.aspx?id=" + battleInfo.BattleId ),
					"resource"
					);

				ItemsTableImageButton accept = new ItemsTableImageButton( OrionGlobals.getCommonImagePath( "ok.gif"), "accept_" + battleInfo.BattleId );
				accept.Click += new ImageClickEventHandler( Accept_Click );

				ItemsTableImageButton reject = new ItemsTableImageButton( OrionGlobals.getCommonImagePath( "remove.gif"), "reject_" + battleInfo.BattleId );
				reject.Click += new ImageClickEventHandler( Reject_Click );
				
				line.add( id );
				line.add( ruler );
				line.add( view );
				line.add( accept );
				line.add( reject );

				itemsTable.addLine( line );
			}
		}

		#endregion

		#region Events

		private void Accept_Click(object sender, ImageClickEventArgs e) {
			int idx = itemsTable.SelectedIndex;
			int id = int.Parse(itemsTable.getSpecificText(idx,0));
			SimpleBattleInfo battleInfo = _ruler.GetBattle(id,BattleType.FRIENDLY);
			battleInfo.Accepted = true;

			Universe.instance.AcceptBattle( id, _ruler );
			
			itemsTable.removeLine(idx);
			HttpContext.Current.Response.Redirect( OrionGlobals.getSectionBaseUrl("Battle") );
		}

		private void Reject_Click(object sender, ImageClickEventArgs e) {
			int idx = itemsTable.SelectedIndex;
			int id = int.Parse(itemsTable.getSpecificText(idx,0));
			
			Universe.instance.RejectBattle( id, _ruler );

			itemsTable.removeLine(idx);
			HttpContext.Current.Response.Redirect( OrionGlobals.getSectionBaseUrl("Battle") );
		}

		#endregion

		#region Public

		protected override void OnInit(EventArgs e) {
			_ruler = getRuler();
			CreateUnits();

			if( !itemsTable.HasControls() ) {
				Visible = false;
			}else {
				Visible = true;
				Controls.Add(itemsTable);
			}
		
			base.OnInit (e);
		}

		#endregion
		
	}
}
