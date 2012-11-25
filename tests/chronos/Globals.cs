// created on 4/9/04 at 8:49 a

using System.Collections;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Utils;
using NUnit.Framework;

#if PERSIST_TO_SQLSERVER
using Chronos.Persistence.SqlServer;
#endif

#if PERSIST_TO_POSTGRE
using Chronos.Persistence.PostGreSql;
#endif

namespace Chronos.Tests {

	public class Globals {
		
		#region Properties
		
		public static Hashtable factories;
		
		static Globals()
		{
			factories = Universe.factories;
		}
		
		#endregion
		
		#region Utilities

		#region Conquest Utilities
		
		public static Fleet CreateFleetToConquer( Ruler owner )
		{
			Fleet fleet = new Fleet("tmp", Coordinate.First, owner);
			fleet.addShip("ColonyShip", 1);//, Universe.getFactory("planet", "Unit", "ColonyShip").create());
			return fleet;
		}
		
		#region General Utilities
		
		public static void Build( Planet planet, string category, string resource, int quantity )
		{
			Build(planet, category, resource, quantity, false);
		}
		
		public static void Build( Planet planet, string category, string resource, int quantity, bool log )
		{
			Result result = planet.canQueue(category, resource, quantity);
			Assert.IsTrue(result.Ok, result.log());
			int before = planet.getResourceCount(category, resource);
			
			if( log ) {
				Log.log("---- CanQueue Result ---");
				Log.log(result.log());
			}
			
			planet.queue(category, resource, quantity);
			planet.turn();
			while( planet.current(category) != null ) {
				planet.turn();	
			}
			planet.turn();
			
			Assert.AreEqual( planet.getResourceCount(category, resource), before + quantity, "Not Built!" );
		}
		
		#endregion
		
		#endregion
		
		#region Research Utilities
		
		public static void ToEspionage( Ruler ruler, Planet planet )
		{
			ToAllShips(ruler, planet);
			
			ruler.addResource("Research", "Warfare", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "Espionage", 1);
			ruler.checkDependencies();
			
			planet.checkDependencies();
			
			Build(planet, "Building", "Barracks", 1);
			
		}
		
		public static void ToRareResources( Ruler ruler, Planet planet )
		{
			planet.addResource("Intrinsic", "labor", 4000);
			ToWaterExploration(ruler, planet);
			ToResearchCampus(ruler, planet);
			ToWaterReclamation(ruler, planet);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "RareResources", 1);
			ruler.checkDependencies();
			planet.checkDependencies();
		}
		
		public static void ToAllShips( Ruler ruler, Planet planet )
		{
			ToResearchCampus(ruler, planet);
			ruler.Victories = 500;
			
			ruler.addResource("Research", "AdvancedFlightI", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "AdvancedFlightII", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "AdvancedFlightIII", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "ShipShields", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "ShipFirePower", 1);
			ruler.checkDependencies();
			
			planet.addResource("Building", "StarPort", 1);
			planet.checkDependencies();
		}
		
		public static void ToHospital(Ruler ruler, Planet planet )
		{
			ruler.addResource("Research", "Medicine", 1);
			ruler.checkDependencies();
			
			planet.checkDependencies();
		}
		
		public static void ToSpa(Ruler ruler, Planet planet )
		{
			ToWaterExploration(ruler, planet);
			
			ruler.addResource("Research", "Medicine", 1);
			
			ruler.checkDependencies();
			
			planet.checkDependencies();
		}
		
		public static void ToWaterReclamation(Ruler ruler, Planet planet )
		{
			ToWaterExploration(ruler, planet);
			ToWaterResearchCampus(ruler, planet);

			ruler.addResource("Research", "Geology", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "GeoPlanet", 1);
			ruler.checkDependencies();
			
			planet.checkDependencies();
		}
		
		public static void ToWaterExploration(Ruler ruler, Planet planet )
		{
			ruler.addResource("Research", "WaterExplorationI", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "WaterExplorationII", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "WaterExplorationIII", 1);
			ruler.checkDependencies();
			
			planet.checkDependencies();
		}
		
		public static void ToStarGate(Ruler ruler, Planet planet )
		{
			ToResearchCampus(ruler, planet);
			
			ruler.addResource("Research", "AdvancedResearchI"); ruler.checkDependencies();
			ruler.addResource("Research", "AdvancedResearchII"); ruler.checkDependencies();
			ruler.addResource("Research", "TeletransportationI"); ruler.checkDependencies();
			ruler.addResource("Research", "TeletransportationII"); ruler.checkDependencies();
			ruler.addResource("Research", "TeletransportationIII"); ruler.checkDependencies();
			ruler.addResource("Research", "PlanetExploration"); ruler.checkDependencies();
			ruler.addResource("Research", "PlanetLimit5"); ruler.checkDependencies();
			
			planet.checkDependencies();
		}
		
		public static void ToLandReclamation(Ruler ruler, Planet planet )
		{
			ToResearchCampus(ruler, planet);
			ruler.addResource("Research", "Geology", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "GeoPlanet", 1);
			ruler.checkDependencies();
			
			planet.checkDependencies();
		}
		
		public static void ToMineralExtractor(Ruler ruler, Planet planet )
		{
			ruler.addResource("Research", "PlanetExploration", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "Geology", 1);
			ruler.checkDependencies();
			
			planet.checkDependencies();
		}
		
		public static void ToStockMarckets(Ruler ruler, Planet planet )
		{
			ruler.addResource("Research", "Commerce", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "Economics", 1);
			ruler.checkDependencies();
			
			planet.checkDependencies();
		}
		
		public static void ToResearchCampus(Ruler ruler, Planet planet )
		{
			ruler.addResource("Research", "AdvancedResearchI", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "AdvancedResearchII", 1);
			ruler.checkDependencies();
			
			planet.checkDependencies();
			
			planet.addResource("Building", "University");
			planet.checkDependencies();
			
			planet.addResource("Building", "ResearchCampus");
			planet.checkDependencies();
		}
		
		public static void ToWaterResearchCampus(Ruler ruler, Planet planet )
		{
			ToWaterExploration(ruler, planet);
			
			planet.addResource("Building", "WaterSchool");
			planet.checkDependencies();
			
			planet.addResource("Building", "WaterUniversity");
			planet.checkDependencies();
			
			planet.addResource("Building", "WaterResearchCampus");
			planet.checkDependencies();
		}
		
		public static void ScanAvailable( Ruler ruler, Planet planet )
		{
			ruler.addResource("Research", "AdvancedResearchI", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "OrbitExploration", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "Scanning", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "PlanetExploration", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "ScanLevelI", 1);
			ruler.checkDependencies();
			
			ToResearchCampus(ruler, planet);
		}

		public static void FleetMovementAndExploration( Ruler ruler ) {
			ruler.Victories = 500;
			ruler.addResource("Research", "AdvancedFlightI", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "AdvancedFlightII", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "AdvancedFlightIII", 1);
			ruler.checkDependencies();
			
			ruler.addResource("Research", "PlanetExploration", 1);
			ruler.checkDependencies();

			ruler.addResource("Research", "SectorExploration", 1);
			ruler.Victories = 500;
			ruler.checkDependencies();

			ruler.addResource("Research", "SystemExploration", 1);
			ruler.checkDependencies();

			ruler.addResource("Research", "GalaxyExploration", 1);
			ruler.checkDependencies();

		}
		
		#endregion

		#endregion
	};

}
