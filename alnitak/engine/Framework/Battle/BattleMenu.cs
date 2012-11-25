using System.Text;
using System.Web.UI;
using Language;

namespace Alnitak.Battle {
	
	public class BattleMenu : Control {

		private ILanguageInfo info = CultureModule.getLanguage(  );
		private bool _positionTime;
		private int _numberOfMoves;
		private string _terrain;

		#region Private

		private string RenderMenu() {
			StringBuilder menu = new StringBuilder();

			menu.AppendFormat(@"<table id='battleMenu' class='planetFrame'>
				<tr>
					<td class='BattleMenuTitle'>
						{0}
					</td>
				</tr>
				<tr>
					<td class='borderBottom'>
						{1}:<span id='moves'>{2}</span><br/>
						{3}:<span id='terrain'>{4}</span>
					</td>
				</tr>
				<tr>
					<td class='borderBottom'>
						{5}:<input id='quantity' type='text'>
						{6}:<span id='minquantity'></span><br/>
						{7}:<span id='maxquantity'></span><br/>
					</td>
				</tr>
			",
				info.getContent( "battle_menu" ),
				info.getContent( "battle_moves" ),
				_numberOfMoves,
				info.getContent( "terrain" ),
				_terrain,
				info.getContent( "battle_insert" ),
				info.getContent( "battle_minQuant" ),
				info.getContent( "battle_maxQuant" )
			);

			if( !_positionTime ) {
				menu.AppendFormat( @"<tr>
					<td class='BattleMenuTitle'>
						{0}
					</td>
				</tr>
				<tr>
					<td class='borderBottom'>
						{1}:<span id='attackAgainst'></span><br/>
						{2}:<span id='defenseAgainst'></span><br/>
						{3}:<span id='targetDefense'></span><br/>
						{4}:<span id='targetLive'></span><br/>
						{5}:<span id='damage'></span><br/>
					</td>
				</tr>",
					info.getContent( "battle_info" ),
					info.getContent( "battle_attack" ),
					info.getContent( "battle_defense" ),
					info.getContent( "battle_targetDefense" ),
					info.getContent( "battle_targetLive" ),
					info.getContent( "battle_unitsDestroyed" )
				);
			}


			menu.AppendFormat(@"<tr>
					<td class='BattleMenuTitle'>
						{0}
					</td>
				</tr>
				<tr>
					<td class='borderBottom'>
						{1}:<span id='shipType'></span><br/>
						{2}:<span id='unitQuant'></span><br/>
						{3}:<span id='baseAttack'></span><br/>
						{4}:<span id='baseDefense'></span><br/>
						{5}:<span id='baseLive'></span><br/>
						{6}:<span id='movementCost'></span><br/>
						{7}:<span id='movementType'></span><br/>
						{8}:<span id='range'></span><br/>
						{9}:<span id='hasAttack'></span><br/>
						{10}:<span id='hasStrikeBack'></span><br/>
						{11}:<span id='hasCatapultAttack'></span><br/>
						{12}:<span id='hasDamageBehindUnits'></span><br/>
						{13}:<span id='hasTripleAttack'></span><br/>
						{14}:<span id='hasReplicator'></span><br/>
					</td>
				</tr>
			",
				info.getContent( "battle_unitInfo" ),
				info.getContent( "battle_shipType" ),
				info.getContent( "fleet_quant" ),
				info.getContent( "battle_attack" ),
				info.getContent( "battle_defense" ),
				info.getContent( "battle_live" ),
				info.getContent( "battle_moveCost" ),
				info.getContent( "battle_moveType" ),
				info.getContent( "battle_range" ),
				info.getContent( "battle_canAttack" ),
				info.getContent( "battle_strikeBack" ),
				info.getContent( "battle_catapult" ),
				info.getContent( "battle_damageBehind" ),
				info.getContent( "battle_tripleAttack" ),
				info.getContent( "battle_replicator" )
			);


			if( !_positionTime ) {
				menu.AppendFormat( @"<tr>
					<td class='BattleMenuTitle'>
						{0}
					</td>
				</tr>
				<tr>
					<td class='borderBottom' align='center'>"+
					"<input type='button' value='N' onClick='setPosition(\"n\");' /><br/>"+
					"<input type='button' value='W' onClick='setPosition(\"w\");' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' value='E' onClick='setPosition(\"e\");' /><br/>"+
					"<input type='button' value='S' onClick='setPosition(\"s\");' /><br/>"+
					@"</td>
				</tr>",info.getContent( "battle_position" ));
			}

			menu.Append( "</table>" );

			menu.Append( "<div align='center'><input type='button' value='Undo' onClick='undo();' /> <input type='button' value='Reset' onClick='resetMoves();' /></div>" );

			return menu.ToString();
		}


		#endregion

		#region Events

		protected override void Render(HtmlTextWriter writer) {
			writer.Write( RenderMenu() );
		}

		#endregion


		#region Constructor

		public BattleMenu( string terrain, int numberOfMoves, bool positionTime ) {
			_terrain = terrain;
			_numberOfMoves = numberOfMoves;
			_positionTime = positionTime;

		}

		#endregion
				
	}
}
