using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alnitak.Exceptions;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Info.Results;
using Language;
using Label = System.Web.UI.WebControls.Label;

namespace Alnitak.Battle {
	
	public class BattleField : UserControl {

		#region Fields

		private ILanguageInfo info = CultureModule.getLanguage(  );

		private SimpleBattleInfo sInfo = null;

		private BattleInfo bInfo = null;

		private RulerBattleInfo rulerInfo = null;
		private RulerBattleInfo enemyInfo = null;

		private Ruler _ruler;

		private bool _adminView;

		#endregion

		#region Control Fields

		protected Label vs;

		protected Button turn;
		protected Button setPosition;
		protected Button giveUp;

		protected Panel field;
		protected Panel menu;

		protected QueueErrorReport queueError;

		protected MessageList messageList;

		#endregion

		#region Properties 

		private bool IsViewing {
			get { return HttpContext.Current.Request.QueryString["rulerid"] != null; }
		}

		#endregion

		#region Private

		/// <summary>
		/// retorna o ruler associado ao utilizador corrente
		/// </summary>
		/// <returns>objecto Ruler com o ruler corrente</returns>
		private Ruler GetRuler() {
			User user = Page.User as User;
			if( null == user ) {
				Redirect();
			}

			Ruler ruler = Universe.instance.getRuler(user.RulerId);
			if( null == ruler ) {
				throw new AlnitakException("Ruler no existe @ PlanetControl::getRuler");
			}
			return ruler;
		}

		private void InitQueryStrings( ) {
			string id = HttpContext.Current.Request.QueryString["id"];
			
			if( id == null && id == string.Empty && OrionGlobals.isInt( id ) ) {
				Redirect();
			}

			string rulerid = HttpContext.Current.Request.QueryString["rulerid"];
			if( null == rulerid ) {
				_ruler = GetRuler();
			}else {
				if( rulerid == null && rulerid == string.Empty && OrionGlobals.isInt( rulerid ) ) {
					Redirect();
				}
				_ruler = Universe.instance.getRuler( int.Parse( rulerid ) );
				_adminView = true;
			}
		}

		private void CheckEndBattle() {
			if( bInfo.EndBattleBase.HasEnded()) {
				bInfo.EndBattleBase.EndBattle();
				Redirect();
			}
		}

		private void RaiseError( Result r ) {
			queueError.Title = info.getContent( "battle_error" );
			queueError.ResultSet = r;
			queueError.Visible = true;
		}

		private void Redirect() {
			HttpContext.Current.Response.Redirect( OrionGlobals.getSectionBaseUrl("Battle") );
		}

		#region Get

		private void SetInfos( ) {
			int battleId = int.Parse( HttpContext.Current.Request.QueryString["id"] );
			bInfo = Universe.instance.GetBattle( battleId );
			sInfo =  _ruler.GetBattle( battleId, bInfo.BattleType );

			if( bInfo == null || sInfo == null) {
				Redirect();
			}
		}

		#endregion

		#region Render

		protected void QuestionScript( string question, Button button ) {
			string script = @"
				<script language='javascript'>
					function giveUp( question ) {
						var theform = document.pageContent;
						var resp = confirm(question);
						return resp;
					}
				</script>";
			Page.RegisterClientScriptBlock("Question",script);
			button.Attributes["OnClick"] = "return giveUp(\""+question+"\");";
		}

		private void RegisterScripts( ) {
			this.Controls.Add( new LiteralControl(string.Format("<script src='{0}' type='text/javascript' ></script>",OrionGlobals.resolveBase("skins/commonScripts/UnitDescriptor.js"))) );
			this.Controls.Add( new LiteralControl(string.Format("<script src='{0}' type='text/javascript' ></script>",OrionGlobals.resolveBase("skins/commonScripts/unit.js"))));
			this.Controls.Add( new LiteralControl(string.Format("<script src='{0}' type='text/javascript' ></script>",OrionGlobals.resolveBase("skins/commonScripts/buildings.js"))));
			if( sInfo.IsPositionTime ) {
				QuestionScript( info.getContent("battle_positioningQuestion"), setPosition );
				this.Controls.Add( new LiteralControl(string.Format("<script src='{0}' type='text/javascript' ></script>",OrionGlobals.resolveBase("skins/commonScripts/positioning.js"))));
			}else {
				QuestionScript( info.getContent("battle_giveUpQuestion"), giveUp );
				if( sInfo.IsTurn && !sInfo.EnemyIsPositionTime ) {
					QuestionScript( info.getContent("battle_endTurnQuestion"), turn );
					this.Controls.Add( new LiteralControl(string.Format("<script src='{0}' type='text/javascript' ></script>",OrionGlobals.resolveBase("skins/commonScripts/battle.js"))));
				}
			}
		}

		private void RenderTitle() {			
			Ruler enemy = Universe.instance.getRuler(enemyInfo.OwnerId);

			if( rulerInfo.BattleType == BattleType.BATTLE ) {
				int wonScore1 = BattleInfo.GetWonScore(rulerInfo,enemyInfo,enemy);
				int lostScore1 = BattleInfo.GetLostScore(enemyInfo,_ruler,enemy);

				int wonScore2 = BattleInfo.GetWonScore(enemyInfo,rulerInfo,_ruler);
				int lostScore2 = BattleInfo.GetLostScore(rulerInfo,enemy,_ruler);
				vs.Text = string.Format("<div align='center' id='vs'>{0} (<span class='green'>{1}</span>/<span class='red'>{2}</span>) VS {3} (<span class='green'>{4}</span>/<span class='red'>{5}</span>)</div><p/>",_ruler.Name,wonScore1,lostScore1,enemy.Name,wonScore2,lostScore2);
				return;
			}

			
			string rulerScore = Universe.GetTournamentPoints( rulerInfo ).ToString();
			string enemyScore = Universe.GetTournamentPoints( enemyInfo ).ToString();
			vs.Text = string.Format("<div align='center' id='vs'>{0} ({1}) VS {2} ({3})</div><p/>",_ruler.Name,rulerScore,enemy.Name,enemyScore);
		
		}

		private void RenderAll() {
			field.Controls.Add( new LiteralControl( RenderBattleGrid() ) );			
			menu.Controls.Add( new BattleMenu( bInfo.Terrain, _ruler.NumberOfMoves, sInfo.IsPositionTime ) );
		}

		private void RenderErrors() {
			string errors = "<script language='javascript'>\n";
			errors += " function RaiseQuantityError() { alert('"+info.getContent("battle_invalidQuantity")+"'); }";
			errors += "function RaiseShipsLeftError() { alert('"+ info.getContent("battle_UnitsLeft") +"');}";
			errors += "function RaiseMovesError() { alert('"+ info.getContent("battle_noMoves") +"');}";
			errors += "function RaiseAlreadyAttackedError() { alert('"+ info.getContent("battle_alreadyAttacked") +"');}";
			errors += "function RaiseMinimumMoveError( quantMoved, type, min ) { alert('"+info.getContent("battle_minimumMove1")+" ' + quantMoved + ' ' + type +' "+info.getContent("battle_minimumMove2")+" ' + min +'!' ); }";
			errors += "function RaiseMinimumRestError( quantLeft, type, min ) { alert('"+info.getContent("battle_minimumRest1")+" ' + quantLeft + ' ' + type +' "+info.getContent("battle_minimumRest2")+" ' + min +'!' ); }";
			errors += "</script>";
			Page.RegisterClientScriptBlock( "errors", errors);
		}

		#region Grid

		private string RenderBattleGrid() {
			StringBuilder writer = new StringBuilder();
			if( sInfo.EnemyIsPositionTime ) {
				RenderEnemySrcShips(enemyInfo,writer);
			}
			RenderGrid(writer);
			if( sInfo.IsPositionTime ) {
				RenderRulerSrcShips(rulerInfo,writer);
			}

			return writer.ToString(  );
		}

		private void RenderGrid( StringBuilder writer ) {
			writer.AppendFormat( "<table id='battleGrid' onmouseover='overFilter();' class='{0}'>", bInfo.Terrain );

			for( int i = 1; i < BattleInfo.ROWCOUNT+1 ; ++i ) {
				writer.AppendFormat("<tr>");
				
				for( int j = 1; j < BattleInfo.COLUMNCOUNT+1 ; ++j ) {
					string sector = i + "_" + j;
					if( ( sInfo.IsPositionTime || sInfo.EnemyIsPositionTime ) && IsViewing ) {
						writer.AppendFormat("<td id='{0}_{1}' >&nbsp;</td>",i,j);
						continue;
					}
					if( !RenderRulerShips( writer, sector ) && !RenderEnemyShips( writer, sector ) ) {
						if( _adminView || (!sInfo.IsTurn && !sInfo.IsPositionTime ) || (!sInfo.IsPositionTime && sInfo.EnemyIsPositionTime ) ) {
							writer.AppendFormat("<td id='{0}_{1}' >&nbsp;</td>",i,j);
						} else {
							writer.AppendFormat("<td id='{0}_{1}' onClick='selected(this.id);' onmouseover='canMoveOver(this.id,event);' onmouseout='canMoveOut();'>&nbsp;</td>",i,j);
						}
					}
				}
				writer.AppendFormat("</tr>");
			}

			writer.AppendFormat( "</table>" );
		}

		private bool RenderRulerShips( StringBuilder writer, string sector ) {
			if( rulerInfo.SectorHasElements( sector ) ) {
				Element elem = rulerInfo.SectorGetElement( sector );
				string shipImage = string.Format( "{0}_{1}.gif",elem.Type.ToLower(), elem.Position.ToString().ToLower() );
				if( (sInfo.IsTurn || sInfo.IsPositionTime) && !_adminView && !sInfo.EnemyIsPositionTime ) {
					writer.AppendFormat("<td id='{0}' onClick='selected(this.id);' onmouseover='canMoveOver(this.id,event);' onmouseout='canMoveOut();'><img id='{2}_unit' src='{1}' title='{3}'/></td>", sector, OrionGlobals.getCommonImagePath( shipImage.ToLower() ), elem.Type, elem.Quantity );
				}else {
					writer.AppendFormat("<td id='{0}' onmouseover='fillInformations(this.id);'><img id='{2}_unit' src='{1}' title='{3}' /></td>", sector, OrionGlobals.getCommonImagePath( shipImage ), elem.Type, elem.Quantity );
				}
				return true;
			}
			return false;
		}

		private bool RenderEnemyShips( StringBuilder writer, string sector ) {
			string newSector = RulerBattleInfo.InvertSector( sector );
			if( enemyInfo.SectorHasElements( newSector ) && !sInfo.IsPositionTime ) {
				Element elem = enemyInfo.SectorGetElement( newSector );
				string shipImage = string.Format( "{0}_{1}.gif",elem.Type.ToLower(), elem.InvertedPosition.ToString().ToLower() );
				if( sInfo.IsTurn && !_adminView ) {
					writer.AppendFormat("<td id='{0}' class='enemyBorder' onClick='selected(this.id);' onmouseover='canMoveOver(this.id,event);' onmouseout='canMoveOut();'><img id='{3}_enemy' src='{1}' title='{2}'/></td>", sector, OrionGlobals.getCommonImagePath( shipImage.ToLower() ), elem.Quantity, elem.Type );
				}else {
					writer.AppendFormat("<td id='{0}' class='enemyBorder' onmouseover='fillInformations(this.id);'><img id='{2}_unit' src='{1}' title='{3}' /></td>", sector, OrionGlobals.getCommonImagePath( shipImage.ToLower() ), elem.Type, elem.Quantity );
				}	
				return true;
			}
			return false;
		}
		
		#endregion

		#region Source Ships

		private void RenderEnemySrcShips( RulerBattleInfo rBattleInfo, StringBuilder writer ) {
			writer.AppendFormat("<table><tr>");
			foreach( Element e in rBattleInfo.InitialContainer ) {
				writer.AppendFormat( "<td><img src='{1}.gif' title='{2}' /></td>",e.Type, OrionGlobals.getCommonImagePath( e.Type.ToLower() + "_s" ),e.Quantity );
			}
			writer.AppendFormat("</tr></table>");
		}

		private void RenderRulerSrcShips( RulerBattleInfo rBattleInfo, StringBuilder writer ) {
			writer.AppendFormat("<table id='srcShips'><tr>");
			foreach( Element e in rBattleInfo.InitialContainer ) {
				if( _adminView ) {
					writer.AppendFormat( "<td id='{0}'><img id='{0}_unit' src='{1}.gif' title='{2}' /></td>",e.Type, OrionGlobals.getCommonImagePath( e.Type.ToLower() ),e.Quantity );
				}else {
					writer.AppendFormat( "<td id='{0}' onClick='selected(this.id);'><img id='{0}_unit' src='{1}.gif' title='{2}' /></td>",e.Type, OrionGlobals.getCommonImagePath( e.Type.ToLower() ),e.Quantity );
				}
			}
			writer.AppendFormat("</tr></table>");
		}

		#endregion

		#endregion

		private void SetPositionStuff() {
			turn.Visible = false;
			giveUp.Visible = false;
			setPosition.Visible = true;
			setPosition.Text = info.getContent( "battle_positioning" );
			setPosition.Click +=new EventHandler(setPositions_Click);
			setPosition.Attributes.Add( "onClick","if(!checkSrcShips())return false;" );
		}

		private void SetTurnStuff() {
			setPosition.Visible = false;
			if( sInfo.IsTurn && !sInfo.EnemyIsPositionTime && !_adminView ) {
				turn.Visible = true;
				turn.Text = info.getContent( "battle_endTurn" );
				turn.Click += new EventHandler(turn_Click);
			} else {
				turn.Visible = false;
			}
			giveUp.Text = info.getContent( "battle_giveUp" );
			giveUp.Click += new EventHandler(giveUp_Click);
		}

		#endregion

		#region Control Events

		protected override void OnInit( System.EventArgs e ) {
			_adminView = false;

			InitQueryStrings();
			SetInfos();
			
			queueError.Visible = false;
			messageList.Manager = bInfo.GetRulerBattleInfo( _ruler );
		}

		protected override void OnLoad(EventArgs e) {

			Page.RegisterHiddenField( "movesMade","" );
			Page.RegisterHiddenField( "imagePath",OrionGlobals.getCommonImagePath( "" ) );

			if( _adminView ) {
				turn.Visible = false;
				setPosition.Visible = false;
				giveUp.Visible = false;
				return;
			}

			if( sInfo.IsPositionTime ) {
				SetPositionStuff();
			} else {
				SetTurnStuff();
			}
			
			base.OnLoad (e);
		}

		protected override void OnPreRender(EventArgs e) {
			rulerInfo = bInfo.GetRulerBattleInfo( _ruler );
			enemyInfo = bInfo.GetEnemyBattleInfo( _ruler );

			RegisterScripts();
			RenderTitle();
			RenderAll();
			RenderErrors();
			
			base.OnPreRender (e);
		}

		protected override void Render(HtmlTextWriter writer) {			
			writer.WriteLine( "<img id='cannotMove' class='invisible' src='{0}' />", OrionGlobals.getCommonImagePath( "arrowRed.gif" ) );
			writer.WriteLine( "<img id='enemy' class='invisible' onclick='attack();' src='{0}' />", OrionGlobals.getCommonImagePath( "enemy.gif" ) );

			base.Render (writer);
		}

		
		#endregion

		#region Events

		private void turn_Click(object sender, EventArgs e) {
			if( !sInfo.IsTurn ) {
				return;
			}

			string moves = Request.Form["movesMade"];
			Result r = bInfo.MakeTurn( moves, _ruler );
			CheckEndBattle();
			if( r.Ok ) {
				sInfo.AddTurn();
				SetTurnStuff();
				Redirect();
			}
			RaiseError(r);
		}

		private void setPositions_Click(object sender, EventArgs e) {
			if( !sInfo.IsPositionTime ){
				return;
			}

			string moves = Request.Form["movesMade"];
			Result r = bInfo.MakePositioning( moves, _ruler );

			if( r.Ok ) {
				SetTurnStuff();
				Redirect();
			}

			RaiseError(r);
		}

		private void giveUp_Click(object sender, EventArgs e) {
			bInfo.ForceEndBattle(_ruler);
			Redirect();
		}

		#endregion
	}
}