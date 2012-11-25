using System;
using System.Collections;
using Chronos.Core;

namespace Chronos.Battle {

	
	public class TotalAnnihilationUtil : BattleUtilBase {

		#region Fields
		
		private static ArrayList buildings = new ArrayList();
		private delegate void AddBuilding( RulerBattleInfo rbi );

		#endregion Fields

		#region Constructor

		static TotalAnnihilationUtil() {
			buildings.Add( new AddBuilding(AddTurret) );
			buildings.Add( new AddBuilding(AddIonCannon) );
		}

		#endregion Constructor

		#region Private

		private static Element CreateElement( string name ) {
			Element element = new Element();
			element.Quantity = 1;
			element.IsBuilding = true;
			element.Type = name;
			return element;
		}

		private static void AddTurret( RulerBattleInfo rbi ) {
			Planet p = rbi.IBattle as Planet;
			if( p.getResourceCount("Building", "Turret") > 0 ) {
				rbi.InitialContainer.Add( CreateElement("Turret") );
			}
		}

		private static void AddIonCannon( RulerBattleInfo rbi ) {
			Planet p = rbi.IBattle as Planet;
			if( p.getResourceCount("Building", "IonCannon") > 0 ) {
				rbi.InitialContainer.Add( CreateElement("IonCannon") );
			}
		}

		#endregion Private

		#region Static
		
		public override void Init(RulerBattleInfo rbi) {
			if( rbi.BattleType == BattleType.BATTLE && rbi.IBattle is Planet ) {
				foreach( AddBuilding addBuilding in buildings ) {
					addBuilding( rbi );
				}
			}
		}

		public override bool HasWon(RulerBattleInfo rbi) {
			return rbi.HasUnits;
		}

		#endregion
	}
}
