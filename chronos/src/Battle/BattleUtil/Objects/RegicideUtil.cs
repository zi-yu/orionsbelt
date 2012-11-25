namespace Chronos.Battle {
	
	public class RegicideUtil : BattleUtilBase {

		#region Fields

		private static Element flagShip = null;

		#endregion Fields

		#region Private

		private void InitFlagShip() {
			//Resource resource = GetUnitResource("FlagShip");
			flagShip = new Element();
			flagShip.Quantity = 1;
			flagShip.Type = "FlagShip";
			flagShip.RetrieveShip = false;
		}

		#endregion Private

		#region Static
			
		public override void Init(RulerBattleInfo rbi) {
			if( null == flagShip ) {
				InitFlagShip();
			}
			rbi.InitialContainer.Add(flagShip.Clone());
		}

		public override bool HasWon(RulerBattleInfo rbi) {
			return rbi.HasUnit("FlagShip");
		}

		#endregion Static
	}
}
