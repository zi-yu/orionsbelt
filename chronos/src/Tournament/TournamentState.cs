// created on 9/2/2005 at 10:01 AM

using System;

namespace Chronos.Tournaments {

	[Serializable]
	public enum TournamentState {
		NotStarted,
		Subscriptions,
		Championship,
		Playoffs,
		Finished
	};

}