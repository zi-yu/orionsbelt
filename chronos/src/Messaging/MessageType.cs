
namespace Chronos.Messaging {

	public enum MessageType {
		/* Management */
		PlanetManagement,
		ResearchManagement,
		FleetManagement,
		
		/* Battle */
		Battle,
		Scan,
		Radar,
		Sabotage,
		
		/* Other */
		Information,
		Error,
		Alliance,
		Alert,
		Prize,
		Generic,
		None
	};
}
