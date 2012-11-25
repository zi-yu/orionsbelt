// created on 4/8/04 at 10:23 a

using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Core;
using Chronos;
using Chronos.Utils;
using Chronos.Actions;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	[Serializable]
	public class ResourceInfoTest {
		
		#region Instance Fields
		
		private Hashtable factories;
		private ResourceInfo info;
		private Planet planet;
		private Ruler ruler;
		
		#endregion
		
		#region Set Up Methods
		
		[TestFixtureSetUp]
		public void init()
		{
			factories = Globals.factories;
		}
		
		[SetUp]
		public void prepare()
		{
			ruler = new Ruler(null, Universe.factories,"BuuBuu");
			planet = new Planet( ruler, factories, "Buu", Coordinate.First );
			info = planet.getResourceInfo("Building");
			
			planet.addResource("Intrinsic", "labor", 10000);
			planet.addResource("Intrinsic", "mp", 10000);
			planet.addResource("Intrinsic", "gold", 1000);
			planet.addResource("Intrinsic", "energy", 1000);
		}
		
		#endregion
		
		#region Queue Tests
		
		[Test]
		public void queueAddTest()
		{
			Assert.IsTrue( planet.Owner != null, "We need a owner for the planet in this test" );
		
			Assert.IsFalse( info.canQueue("asdaas__sd---asd",4).Ok, "Trying to Queue a non existing factory" );
			Assert.IsFalse( info.canQueue("fAm",1).Ok, "names are case sensitive" );
			
			Result result = info.canQueue("Farm",1);
			Assert.IsFalse( !result.Ok, "Cannot queue Farm " + result.log());
			
			int count = info.getResourceCount("Farm");
			
			planet.addResource("Intrinsic","energy",30);
			planet.addResource("Intrinsic","mp",1600);
			planet.addResource("Intrinsic","gold",1000);
			planet.addResource("Intrinsic","labor",4000);
			planet.addResource("Intrinsic","groundSpace",4000);
			
			Result res = info.canQueue("Farm",1);
			Assert.IsTrue( res.Ok , "Cannot queue Farm: " + res.log() );
			
			info.enqueue("Farm");
			Assert.IsTrue( info.Current == null, "should start on turn" );
			
			for( int i = 0; i < info.getAvailableFactory("Farm").Duration.Value+1; ++i ) {
				planet.turn();
			}
			
			int newCount = info.getResourceCount("Farm");
			Assert.IsTrue( info.Current == null, "should be null" );
			Assert.IsTrue(count + 1 == newCount, "farm count not incremented Expected: "+(count+1)+", got "+ newCount);
		}
		
		[Test]
		public void queueActionsTest()
		{
			planet.addResource("Intrinsic","energy",4000);
			planet.addResource("Intrinsic","mp",4000);
			planet.addResource("Intrinsic","gold",4000);
			planet.addResource("Intrinsic","labor",4000);
			
			Assert.IsTrue( info.QueueCount == 0, "Queue should be empty" );
			
			info.enqueue("Farm");
			info.enqueue("PowerPlant");
			info.enqueue("Farm");
			
			Assert.IsTrue( info.QueueCount == 3, "Queue count error. Expected 3 got " +info.QueueCount );
			
			string factoryName = info.dequeue().FactoryName;
			Assert.IsTrue( factoryName == "Farm", "dequed wrong factory, got: " +factoryName);
			Assert.IsTrue( info.QueueCount == 2, "Queue count error. Expected 2 got " +info.QueueCount );
		}
		
		[Test]
		public void queueRemoveTest()
		{
			planet.addResource("Intrinsic","energy",4000);
			planet.addResource("Intrinsic","mp",4000);
			planet.addResource("Intrinsic","gold",4000);
			planet.addResource("Intrinsic","labor",4000);
			
			info.enqueue("Farm");
			info.enqueue("PowerPlant");
			info.enqueue("Farm");
			
			string factoryName = info.dequeue(1).FactoryName;
			Assert.IsTrue( factoryName == "PowerPlant", "dequed wrong factory, got: " +factoryName);
			Assert.IsTrue( info.QueueCount == 2, "Queue count error. Expected 2 got " +info.QueueCount );
		}
		
		[Test]
		public void queueCapacityTest()
		{
			int cap = planet.getResourceCount("Intrinsic", "queueCapacity");
			
			planet.addResource("Intrinsic", "groundSpace", 100000 );
			planet.addResource("Intrinsic", "housing", 100000 );
			planet.addResource("Intrinsic", "labor", 100000 );
			
			Result res = null;
			
			for( int i = 0; i < cap; ++i ) {
				res = planet.canQueue("Building", "Farm", 1);
				Assert.IsTrue( res.Ok, "Can't queue at position " + (i+1) + "/" + cap + " -> " + res.log());
				planet.queue("Building","Farm");
			}
			
			res = planet.canQueue("Building", "Farm", 1);
			Assert.IsFalse( res.Ok, "Queue should be full : "+ res.log());
		}
		
		[Test]
		public void queueAddRemoveTest()
		{
			ResourceInfo info = planet.getResourceInfo("Building");
		
			int gold1 = planet.getResourceCount("gold");
			planet.queue("Building", "Farm", 1);
			info.dequeue(0);
			int gold3 = planet.getResourceCount("gold");
			
			Assert.AreEqual(gold1, gold3);
		}
		
		#endregion
		
		#region Intrinsic Add Cost Tests
		
		[Test]
		public void intrinsicCostTestStart()
		{
			ResourceInfo info = planet.getResourceInfo("Intrinsic");
			info.reset("housing");
			info.reset("labor");
			
			info.addResource("labor", 1);
			Assert.AreEqual( info.getResourceCount("labor"), 0, "Can't add labor... No housing!");
		}
		
		[Test]
		public void intrinsicCostTestAddSomeAndExpand()
		{
			ResourceInfo info = planet.getResourceInfo("Intrinsic");
			info.reset("housing");
			info.reset("labor");
			
			info.addResource("housing", 5);
			Assert.AreEqual( info.getResourceCount("housing"), 5, "where's the housing?");
			
			Action action = new HousingAvailable();
			Assert.IsTrue( action.evaluate(planet, 5) );
			
			info.addResource("labor", 5);
			Assert.AreEqual( info.getResourceCount("labor"), 5, "Can't add labor... But housing exists!");
			
			info.addResource("labor", 3);
			Assert.AreEqual( info.getResourceCount("labor"), 5, "Can't add labor... No housing!");
		}
		
		[Test]
		public void intrinsicCostTestLaborGrow()
		{
			ResourceInfo info = planet.getResourceInfo("Intrinsic");
			info.reset("housing");
			info.reset("labor");
			info.addResource("food", 99999);
			
			info.addResource("housing", 500);

			int first = 0;
			for( int i = 0; i < 5; ++i ) {
				planet.turn();
				int count = info.getResourceCount("labor");
				Assert.IsTrue( count >= first, "There is no grow" );
				first = count;
			}
			
			planet.turn();
			planet.turn();
			planet.turn();
			
			int labor = info.getResourceCount("labor");
			Assert.IsTrue( labor <= 600, "Can't add labor("+labor+")... No housing!" );
		}
		
		#endregion
		
		#region Events Tests
		
		bool invoked = false;
		string resourceName;
		int quantity;
		
		[Test]
		public void testOnCompleteEvent()
		{
			invoked = false;
			info.OnComplete += new ResourceInfo.ResourceNotification(onComplete);
			
			info.enqueue("Farm",10);
			info.turn();
			while( info.Current != null ) {
				info.turn();
			}
			
			Assert.IsTrue(invoked, "Event not invoked");
			Assert.AreEqual("Farm", resourceName, "Resource notified check failed");
			Assert.AreEqual(10, quantity, "Resource quantity check failed");
		}
		
		public void onComplete( Resource res, int quant )
		{
			invoked = true;
			resourceName = res.Name;
			quantity = quant;
		}
		
		[Test]
		public void testOnRemoveEvent()
		{
			invoked = false;
			info.OnRemove += new ResourceInfo.ResourceNotification(onRemove);
			
			info.addResource("Farm",2);
			info.take("Farm", 2);
			
			Assert.IsTrue(invoked, "Event not invoked");
			Assert.AreEqual("Farm", resourceName, "Resource notified check failed");
			Assert.AreEqual(2, quantity, "Resource quantity check failed");
		}
		
		public void onRemove( Resource res, int quant )
		{
			invoked = true;
			resourceName = res.Name;
			quantity = quant;
		}
		
		[Test]
		public void testSoldierBuild()
		{	
			planet.addResource("Intrinsic", "housing", 12000);
			planet.addResource("Intrinsic", "labor", 1200);
			
			planet.Modifiers["labor"] = 0;
			planet.Modifiers["food"] = 10000;
			
			Globals.ToEspionage(ruler, planet);
			
			Result result = planet.canQueue( "Intrinsic", "marine", 10 );
			Assert.IsTrue( result.Ok, result.log() );
			
			Assert.AreEqual( planet.Marines, 0, "#1" );
			int labor = planet.Labor;
			Globals.Build(planet, "Intrinsic", "marine", 10 );
			
			Assert.AreEqual( 10, planet.Marines, "#2" );
			Assert.AreEqual( labor - 10, planet.Labor, "#3" );
		}
		
		[Test]
		public void testMarineOnCancelDuringBuild()
		{
			Assert.IsNotNull( Universe.getFactory("planet", "Intrinsic", "spy").OnCancelDuringBuild, "Spy - No OnCancelDuringBuild" );
			Assert.IsNotNull( Universe.getFactory("planet", "Intrinsic", "marine").OnCancelDuringBuild, "Marine - No OnCancelDuringBuild" );
		}
		
		[Test]
		public void testOnCancelDuringBuild()
		{	
			planet.addResource("Intrinsic", "housing", 12000);
			planet.addResource("Intrinsic", "labor", 1200);
			
			planet.Modifiers["labor"] = 0;
			planet.Modifiers["food"] = 10000;
			
			Globals.ToEspionage(ruler, planet);
			
			Result result = planet.canQueue( "Intrinsic", "marine", 10 );
			Assert.IsTrue( result.Ok, result.log() );
			
			Assert.AreEqual( planet.Marines, 0, "#1" );
			int labor = planet.Labor;
			
			planet.queue( "Intrinsic", "marine", 10 );
			planet.turn();
			planet.cancel("Intrinsic");
			
			Assert.AreEqual( labor, planet.Labor, "#2" );
			Assert.AreEqual( 0, planet.Marines, "#3" );
		}
		
		[Test]
		public void SpyBug()
		{
			planet.addResource("Intrinsic", "food", 500000);
			planet.addResource("Intrinsic", "energy", 500000);
			planet.addResource("Intrinsic", "mp", 500000);
			planet.addResource("Intrinsic", "gold", 500000);
			
			planet.addResource("Intrinsic", "labor", planet.Housing - planet.Labor);
			Assert.AreEqual( planet.Housing, planet.Population );
			
			Globals.ToEspionage(ruler, planet);
			Globals.Build(planet, "Intrinsic", "marine", 1000);
			Assert.AreEqual( planet.Housing, planet.Population, "#1" );
			Assert.AreEqual( planet.Marines, 1000, "#2" );
			Assert.AreEqual( planet.Labor, planet.Housing - 1000, "#3" );
			
			Globals.Build(planet, "Intrinsic", "spy", 700);
			Assert.AreEqual( planet.Housing, planet.Population, "#4" );
			Assert.AreEqual( planet.Spies, 700 );
			
			Globals.Build(planet, "Intrinsic", "spy", 700, false);
			Assert.AreEqual( planet.Spies, 1400, "#5" );
			Assert.AreEqual( planet.Housing, planet.Population, "#6" );
		}
		
		#endregion
		
		#region General Tests
		
		[Test]
		public void testResetAll()
		{
			planet.reset();
		}
		
		#endregion
	};

}
