using System;

namespace Alnitak.Battle {
	
	public class FleetEventArgs : EventArgs {
		
		private Chronos.Core.Fleet _fleet = null;

		public Chronos.Core.Fleet Fleet {
			get { return _fleet; }
			set { _fleet = value; }
		}

		public FleetEventArgs( Chronos.Core.Fleet fleet ) {
			_fleet = fleet;
		}
	}
}
