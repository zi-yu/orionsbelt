using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alnitak.Exceptions;
using Chronos.Core;
using Chronos.Resources;
using Language;

namespace Alnitak.Battle {
	/// <summary>
	/// Summary description for ShipSelector.
	/// </summary>
	public class ShipSelector : UserControl {

		#region Private Fields

		protected ILanguageInfo info = CultureModule.getLanguage();
		
		protected Ruler _ruler = null;

		protected ItemsTable lightTable = new ItemsTable();
		protected ItemsTable mediumTable = new ItemsTable();
		protected ItemsTable heavyTable = new ItemsTable();
		protected ItemsTable animalsTable = new ItemsTable();

		private ArrayList tables = new ArrayList();

		protected DropDownList rulers;
		protected DropDownList battleTypes;
		protected ImageButton createBattle;
		private ResourceBuilder units =  Universe.getFactories("planet", "Unit");

		private Hashtable allUnits = new Hashtable();

		protected Panel choseRuler;
		
		#endregion

		#region Events

		public event EventHandler FleetCreated;

		#endregion

		#region Private Methods

		private void notify( EventHandler ev, EventArgs args ) {
			if ( ev != null ) {
				ev(this, args);
			}
		}

		/// <summary>Retorna o Ruler da sesso</summary>
		private Ruler getRuler() {
			User user = Page.User as User;
			if( user == null )
				throw new AlnitakException("User no est autenticado @ ReadyForBattle::getRuler");

			Ruler _ruler = Universe.instance.getRuler(user.RulerId);
			if( _ruler == null )
				throw new AlnitakException("Ruler no existe @ ReadyForBattle::getRuler");

			return _ruler;
		}

		/// <summary>
		/// preenche o ttulo
		/// </summary>
		private void createTitle( ItemsTable itemsTable, string title ) {
			ItemsTableLine line = new ItemsTableLine();
			line.add( new ItemsTableText( "#", "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent(title), "resourceTitle" ) );
			line.add( new ItemsTableText( info.getContent("quantidade"), "resourceTitle" ) );
			itemsTable.HeaderItem = line;
		}

		private void CreateUnits( ItemsTable itemsTable, string tableTitle, string type, string title  ) {
			itemsTable.Title = info.getContent(tableTitle);
			itemsTable.TitleCssClass = "planetInfoZoneTitle";
			itemsTable.TableCssClass = "planetFrame";

			createTitle( itemsTable, title );
			
			ArrayList unitsAvailable = new ArrayList();
			foreach( string res in units.Keys ) {
				Resource r = Universe.getFactory("planet", "Unit", res).create( );
				if( r.Unit.UnitType.ToLower() != type ) {
					continue;
				}
				
				unitsAvailable.Add( res );

				ItemsTableLine line = new ItemsTableLine();

				ItemsTableImage image = new ItemsTableImage( OrionGlobals.getCommonImagePath( res.ToLower()+".gif") );
				image.Height = "30px";
				image.Width = "30px";
				image.CssClass = "resource";

				ItemsTableText name = new ItemsTableText( info.getContent(res), "resource" );

				ItemsTableTextBox quant = new ItemsTableTextBox();
				
				line.add( image );
				line.add( name );
				line.add( quant );

				itemsTable.addLine( line );
			}
			
			if( itemsTable.Count == 0 ) {
				Information.AddInformation(info.getContent("battle_noUnits"));
			}else {
				allUnits[type] = unitsAvailable;
			}
		}

		private Resource GetUnitResource( string unit, string type ) {
			return Universe.getFactory("planet", type, unit).create( );
		}

		private bool FillFleet( ItemsTable table, Chronos.Core.Fleet f1, string type) {
			ArrayList unitNames = (ArrayList) allUnits[type];
			for( int i = 0 ; i < unitNames.Count; ++i ) {
				string textBoxText = table.getSpecificText( i,2 );
				if( string.Empty == textBoxText ) {
					continue;	
				}
				
				string unit = unitNames[i].ToString( );
				
				try {
					if( OrionGlobals.isInt( textBoxText.ToString() ) ) {
						//Resource r = GetUnitResource(unit,"Unit");
						int quant = int.Parse( textBoxText.ToString());
						if( 0 == quant )
							continue;
						f1.addShip( unit, quant);
					}else {
						Information.AddError(info.getContent("battle_noInt"));
						return false;
					}
				}catch( OverflowException ) {
					Information.AddError(string.Format(info.getContent("battle_IntToBig"),unit) );
					return false;
				}
			}
			return true;
		}

		private bool CheckShips() {
			Chronos.Core.Fleet f1 = new Chronos.Core.Fleet("fleet",null,_ruler);

			string[] types = new string[]{"light","medium","heavy","animal"};
			for( int i = 0; i < tables.Count ; ++i ) {
				if( allUnits.ContainsKey( types[i] )) {
					ItemsTable table = (ItemsTable)tables[i];
					if( !FillFleet( table, f1, types[i] ) ) {
						return false;
					}
				}
			}
			if( f1.HasShips ) {
				notify(FleetCreated,new FleetEventArgs(f1) );
				return true;
			}
			return false;
		}

		#endregion
	
		#region Control events

		private void InitTypes() {
			tables.Add( lightTable );
			tables.Add( mediumTable );
			tables.Add( heavyTable );
			tables.Add( animalsTable );
		}

		private void CreateUnits() {
			CreateUnits(lightTable,"battle_lightAvailable","light","battle_lightUnitType");
			CreateUnits(mediumTable,"battle_mediumAvailable","medium","battle_mediumUnitType");
			CreateUnits(heavyTable,"battle_heavyAvailable","heavy","battle_heavyUnitType");
			CreateUnits(animalsTable,"battle_animalsAvailable","animal","battle_animalType");

			Controls.AddAt(0, lightTable );
			Controls.AddAt(1, mediumTable );
			Controls.AddAt(2, heavyTable );
			Controls.AddAt(3, animalsTable );
		}

		protected override void OnInit(EventArgs e) {
			_ruler = getRuler();

			InitTypes();
			CreateUnits();
			
			base.OnInit( e );
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad( e );

			if( Page.IsPostBack ) {
				CheckShips();
			}
		}

		#endregion

		
	}
}
