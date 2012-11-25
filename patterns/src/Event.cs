namespace DesignPatterns {
	
	public class Event {

		#region Fields

		private string name;
		private string newState;
		private bool saveCurrentState;
		private bool newStateFromHistory;
		
		#endregion

		#region Properties

		public string Name {
			get { return name; }
			set { name = value; }
		}

		public string NewState {
			get { return newState; }
			set { newState = value; }
		}

		public bool SaveCurrentState {
			get { return saveCurrentState; }
			set { saveCurrentState = value; }
		}

		public bool NewStateFromHistory {
			get { return newStateFromHistory; }
			set { newStateFromHistory = value; }
		}

		#endregion

	}
}
