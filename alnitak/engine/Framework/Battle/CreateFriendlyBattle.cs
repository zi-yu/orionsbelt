using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alnitak.Exceptions;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Messaging;
using Language;

namespace Alnitak.Battle {
	
	public class CreateFriendlyBattle : UserControl {

		#region Private Fields

		protected ILanguageInfo info = CultureModule.getLanguage();
		
		protected Ruler _ruler = null;

		protected DropDownList rulers;
		protected DropDownList battleTypes;
		protected ImageButton createBattle;
		protected Panel chooseRuler;

		protected ShipSelector shipSelector;

		#endregion

		#region Private Methods

		/// <summary>Retorna o Ruler da sesso</summary>
		private Ruler getRuler() {
			User user = Page.User as User;
			if( user == null )
				throw new AlnitakException("User no esta autenticado @ ReadyForBattle::getRuler");

			Chronos.Core.Ruler _ruler = Universe.instance.getRuler(user.RulerId);
			if( _ruler == null )
				throw new AlnitakException("Ruler nao existe @ ReadyForBattle::getRuler");

			return _ruler;
		}

		private void PopulateRulers() {
			string qs = string.Empty;

			if( HttpContext.Current.Request.QueryString.HasKeys() ) {
				qs = HttpContext.Current.Request.QueryString["user"];
			}

			IDictionaryEnumerator iter = Universe.instance.rulers.GetEnumerator( );
			while( iter.MoveNext( ) ) {
				Ruler r = (Ruler)iter.Value;
				if( !r.Equals( _ruler ) ) {
					ListItem li = new ListItem( r.Name,r.Id.ToString() );
					if( qs != string.Empty && qs.Trim() == r.Name.Trim() ) {
						li.Selected = true;
					}

					rulers.Items.Add( li );	
				}
			}
		}

		private void PopulateBattleTypes() {
			foreach( string type in BattleInfo.EndBattleTypes ) {
				battleTypes.Items.Add( new ListItem( info.getContent(type), type ));
			}
		}

		private bool CanCreateBattle(Ruler r) {
			if( r.InVacation ) {
				return false;
			}

			if( r.Premium ) {
				return true;
			}
			
			int battleCount = r.GetAllBattles(BattleType.FRIENDLY).Count;
			int battleLimit = int.Parse(OrionGlobals.getConfigurationValue("alnitak","friendlyLimit"));

			return battleCount < battleLimit;
		}
		
		#endregion
	
		#region Events

		protected void createBattle_Click(object sender, EventArgs e) {
            if( rulers.SelectedIndex == -1) {
				Information.AddError( info.getContent("battle_noRulerSelected") );
				return;
			}
			Ruler enemy = Universe.instance.getRuler(int.Parse(rulers.SelectedValue));

			if( !CanCreateBattle(enemy) ) {
				Information.AddError(info.getContent("battle_enemycanthave"));
				return;
			}

			Chronos.Core.Fleet f1 = ((FleetEventArgs )e).Fleet;
			Chronos.Core.Fleet f2 = f1.Clone() as Chronos.Core.Fleet;
			f2.Owner = enemy;

			int count = int.Parse(OrionGlobals.getConfigurationValue("alnitak","shipsCount"));
			if( f1.Ships.Count > count) {
				Information.AddError( string.Format(info.getContent("battle_toMuchUnits"),count) );	
				return;
			}
			if( f1.HasShips ) {
				Universe.instance.CreateBattle( _ruler, f1, enemy, f2, BattleType.FRIENDLY, battleTypes.SelectedValue );
				Information.AddInformation(info.getContent("battle_friendlyCreated"));
			}else {
				Information.AddError( info.getContent("battle_noUnitSelected") );	
			}
		}

		#endregion
	
		#region Control events

		protected override void OnInit(EventArgs e) {
			OrionGlobals.RegisterRequest(MessageType.Battle, info.getContent("section_battle"));
			_ruler = getRuler();

			if( CanCreateBattle(_ruler) ) {
				chooseRuler.Visible = true;
				createBattle.ImageUrl = OrionGlobals.getCommonImagePath( "ok.gif" );

				PopulateRulers();
				PopulateBattleTypes();

				shipSelector.FleetCreated += new EventHandler( createBattle_Click );
			}else {
				chooseRuler.Visible = false;
				Information.AddInformation(string.Format(info.getContent("battle_cannotCreatefriendlys"),int.Parse(OrionGlobals.getConfigurationValue("alnitak","friendlyLimit"))));
			}
		
			base.OnInit( e );
		}

		#endregion
	}
}
