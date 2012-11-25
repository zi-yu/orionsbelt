using Chronos.Core;
using Chronos.Queue;
using Chronos.Resources;
using Chronos.Utils;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class ShipDurationTester {
		
		#region Setup
		
		private Planet planet;
		private Ruler ruler;
		
		[SetUp]
		public void init()
		{
			ruler = new Ruler( Universe.factories, "PRE" );
			planet = new Planet(ruler, Universe.factories, "PRE Planet", Coordinate.First );
			ruler.addPlanet(planet);
			
			planet.addResource("mp", 99999999);
			planet.addResource("gold", 99999999);
			planet.addResource("energy", 99999999);
			planet.addResource("labor", 99999999);
			
			Globals.ToAllShips(ruler, planet);
		}
		
		#endregion
		
		#region Tests
		
		[Test]
		public void Test_Rain()
		{
			PerformDurationTest("Rain");
		}
		
		[Test]
		public void Test_Krill()
		{
			PerformDurationTest("Krill");
		}
		
		[Test]
		public void Test_Crusader()
		{
			PerformDurationTest("Crusader");
		}
		
		[Test]
		public void Test_Kamikaze()
		{
			PerformDurationTest("Kamikaze");
		}
		
		[Test]
		public void Test_Pretorian()
		{
			PerformDurationTest("Pretorian");
		}
		
		#endregion
		
		#region Utilities
		
		private void PerformDurationTest( string ship )
		{
			ResourceFactory factory = Universe.getFactory("planet", "Unit", ship);
			
			Assert.AreEqual(true, factory.Duration.HasQuantity);
			
			int quant = MathUtils.random(1, factory.Duration.Quantity * 5);
			AssertDuration(factory, planet, ship, quant);
			AssertDuration(factory, planet, ship, factory.Duration.Quantity);
			AssertDuration(factory, planet, ship, factory.Duration.Quantity - 1);
			AssertDuration(factory, planet, ship, factory.Duration.Quantity * 2);
			AssertDuration(factory, planet, ship, factory.Duration.Quantity * 2 + quant);
			AssertDuration(factory, planet, ship, factory.Duration.Quantity * 3 - quant);
			AssertDuration(factory, planet, ship, 1);
		}
		
		private int AssertDuration( ResourceFactory factory, Planet planet, string ship, int quantity )
		{
			int expected = GetExpected(factory, quantity, planet);
			planet.queue("Unit", ship, quantity);
			planet.turn();
			int turns = planet.current("Unit").RemainingTurns;
			Assert.AreEqual(expected, turns, string.Format("Expected {0} but got {1} turns for {2} {3}", expected, turns, quantity, ship));
			planet.cancel("Unit");
			return turns;
		}
		
		private int GetExpected( ResourceFactory factory, int quantity, Planet planet )
		{
			int d = QueueItem.RealDuration(planet, factory, quantity);
			
			if( d < 1 ) {
				d = 1;
			}
			
			return d;
		}
		
		#endregion
		
	};
}

