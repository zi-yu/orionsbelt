// created on 9/9/2005 at 9:56 AM

using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Sabotages;
using DesignPatterns;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class SabotageTester {
	
		#region Test Initialization
    
    	private Ruler ruler;
    	private Planet target;
    	private Planet source;
    	
    	[SetUp]
    	public void startTest()
    	{
    		ruler = new Ruler( Globals.factories, "pre" );
    		target = new Planet(ruler, Globals.factories, "Planet 1", Coordinate.First);
    		source = new Planet(ruler, Globals.factories, "Planet 2", Coordinate.translateCoordinate("1:1:1:3"));
    		
    		target.addResource("Intrinsic", "housing", 500000);
    		target.addResource("Intrinsic", "labor", 80000);
    		target.addResource("Intrinsic", "gold", 40000);
    		target.addResource("Intrinsic", "food", 4000000);
    		target.addResource("Intrinsic", "mp", 40000);
    		target.addResource("Intrinsic", "energy", 40000);
    		
    		source.addResource("Intrinsic", "housing", 500000);
    		source.addResource("Intrinsic", "labor", 80000);
    		source.addResource("Intrinsic", "gold", 40000);
    		source.addResource("Intrinsic", "food", 4000000);
    		source.addResource("Intrinsic", "mp", 40000);
    		source.addResource("Intrinsic", "energy", 40000);
    		
    		source.Modifiers["food"] = 100000;
    		target.Modifiers["food"] = 100000;
    		
    		Globals.ToEspionage(ruler, target);
    		Globals.ToEspionage(ruler, source);
    	}
    	
    	private void AddSoldiers()
    	{
    		Globals.Build( source, "Intrinsic", "spy", 5000 );
    		Globals.Build( target, "Intrinsic", "marine", 5000 );
    	}
    	
    	#endregion
    	
    	#region Sabotage Existence Tests
    	
    	[Test]
		public void TestBuildingQueueExistence()
		{
			Assert.IsNotNull( Sabotage.GetSabotage("BuildingQueue") );
		}
		
		[Test]
		public void TestIntrinsicQueueExistence()
		{
			Assert.IsNotNull( Sabotage.GetSabotage("IntrinsicQueue") );
		}
    	
    	[Test]
		public void TestUnitQueueExistence()
		{
			Assert.IsNotNull( Sabotage.GetSabotage("UnitQueue") );
		}
    	
    	#endregion
    	
		#region Base Tests
		
		[Test]
		public void TestTechRequirements()
		{
			Planet planet = new Planet(new Ruler( Globals.factories, "pre3" ), Globals.factories, "Planet 3", Coordinate.First);
			Sabotage sabotage = Sabotage.GetSabotage("BuildingQueue");
			
			Result result = sabotage.CanSabotage(planet, target);
			Assert.IsFalse( result.Ok, result.log() ); 
		}
		
		[Test]
		public void TestSpyRequirements()
		{
			Assert.AreEqual( 0, source.Spies, "Shoulnd't have spies");
			
			AddSoldiers();
			
			foreach( IFactory factory in Sabotage.Factories.Values ) {
				Sabotage sabotage = (Sabotage) factory.create(null);
				Result result = sabotage.CanSabotage(source, target);
				Assert.IsTrue( result.Ok, result.log() );
			}

			Assert.IsTrue( source.take("Intrinsic", "spy", 4500), "Tem de permitir remover spies" );
			
			foreach( IFactory factory in Sabotage.Factories.Values ) {
				Sabotage sabotage = (Sabotage) factory.create(null);
				Result result = sabotage.CanSabotage(source, target);
				
				Assert.IsTrue( sabotage.Spies > source.Spies, "Sabotage: " +sabotage.GetType().Name+ " SpiesNeeded: " + sabotage.Spies + " PlanetSpies: " + source.Spies );
				Assert.IsFalse( result.Ok, sabotage.GetType().Name +" - "+result.log() );
			} 
		}
		
		[Test]
		public void TestMarinesKilled()
		{
			AddSoldiers();
			
			Sabotage sabotage = Sabotage.GetSabotage("BuildingQueue");
			int marines = target.Marines;
			int spies = source.Spies;
			
			Result result = sabotage.CanSabotage(source, target);
			Assert.IsTrue( result.Ok, result.log() );
			
			sabotage.PrepareSabotage(source, target);
			sabotage.turn();
			
			Assert.IsTrue( marines > target.Marines, "Marines don't die" );
			Assert.AreEqual( spies - sabotage.Spies, source.Spies, "Spies don't die" );
		}
		
		#endregion
		
		#region Specific Sabotages Tests
		
		private void CheckQueueSabotage( string sabotageName, string category, string resource, int quantity )
		{
			AddSoldiers();
			target.take("Intrinsic", "marine", 4000);
			int marines = target.Marines;
			
			Result result = target.canQueue(category, resource, quantity);
			Assert.IsTrue(result.Ok, result.log());
			
			target.queue(category, resource, quantity);
			target.turn();
			
			Assert.IsNotNull( target.current(category));
			
			Sabotage sabotage = Sabotage.GetSabotage(sabotageName);
			result = sabotage.CanSabotage(source, target);
			Assert.IsTrue( result.Ok, result.log() );
			
			sabotage.PrepareSabotage(source, target);
			Assert.IsNotNull(source.Tasks.GetList( TaskDescriptor.Sabotage ), "Where's the task?");
			sabotage.turn();
			
			Assert.IsTrue( sabotage.SabotageSucceded() ,"Sabotage Failed");
			Assert.IsNull( target.current(category), "Queue prevails!" );
			Assert.IsTrue( marines > target.Marines, "Marines not killed");
		}
		
		[Test]
		public void TestBuildingQueue()
		{
			CheckQueueSabotage("BuildingQueue", "Building", "Farm", 1);
		}
		
		[Test]
		public void TestIntrinsicQueue()
		{
			CheckQueueSabotage("IntrinsicQueue", "Intrinsic", "marine", 1);
		}
		
		[Test]
		public void TestUnitQueue()
		{
			CheckQueueSabotage("UnitQueue", "Unit", "Rain", 1);
		}
		
		#endregion

	};

}
