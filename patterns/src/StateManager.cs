using System.Collections;

namespace DesignPatterns {
	
	public class StateManager {

		#region Fields
		
		private State current;
		private State history;
		private Hashtable allStates;

		#endregion

		#region Properties

		public State Current {
			get { return current; }
			set {
				current = value;
				while( current.DirectSon != string.Empty ) {
					current = AllStates[current.DirectSon] as State;
				}
			}
		}

		public State History {
			get { return history; }
			set { history = value; }
		}

		public Hashtable AllStates {
			get { return allStates; }
			set { allStates = value; }
		}

		#endregion

		#region Private

		private void ProcessEvent( State e, string eventName ) {
			if( e.HasEvent( eventName ) ) {
				Event ev = e.GetEvent( eventName );
				if( ev.NewStateFromHistory ) {
					Current = History;
				} else {
					History = Current;
					Current = AllStates[ev.NewState] as State;
				}
			} else {
				State parent = AllStates[e.Parent] as State;
				if( parent != null) {
					ProcessEvent( parent, eventName );
				}
			}
		}

		#endregion

		#region Public

		public void ProcessEvent( string eventName ) {
			ProcessEvent(Current,eventName);
		}

		#endregion
	}
}
