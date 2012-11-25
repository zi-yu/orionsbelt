// created on 5/22/04 at 1:40 a

using Chronos.Actions;
using System.Collections;
using Chronos.Resources;
using Chronos.Core;
using Chronos.Info.Results;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	public class ActionTester {
	
		private Planet planet;
		private Ruler ruler;

		private const string type = "Intrinsic";
		private const string resource = "score";
		private const string seed = "__--´´";
		private const int quantity = 100;
	
		[SetUp]
		public void init()
		{
			ruler = new Ruler(null, Globals.factories, "Ruler" );
			planet = new Planet(ruler, Globals.factories, "Planet", Coordinate.First);
		}
		
		[Test]
		public void testAdd()
		{
			Action addAction = new Add(type, resource, quantity.ToString());
			
			int begin = ruler.getResourceCount(type, resource);
			
			Assert.IsTrue( addAction.evaluate(ruler), "Add evaluation failed" );
			Assert.IsTrue( addAction.action(ruler), "Add action failed" );
			
			int end = ruler.getResourceCount(type, resource);
			int count = begin + quantity;
			
			Assert.IsTrue( count == end, "Add operation failed. Expected "+ count + " got " + end );
		}
		
		[Test]
		public void testResourceAvailable()
		{
			Action avail = new ResourceAvailable(type, resource, quantity);
			Assert.IsTrue( avail.evaluate(ruler) == avail.action(ruler), "ResourceAvailable evaluate and action difer" );
			
			Action avail2 = new ResourceAvailable(type, resource + seed, quantity);
			Assert.IsFalse( avail2.evaluate(ruler), "evaluate should return false" );

			Action avail3 = new ResourceAvailable(type + seed, resource, quantity);
			Assert.IsFalse( avail3.evaluate(ruler), "evaluate should return false" );
		}
		
		[Test]
		public void testResourceNeeded()
		{
			Action avail = new ResourceNeeded(resource, quantity.ToString());
			Assert.IsFalse( avail.evaluate(ruler), "Eval should be false" );
			
			ruler.addResource(type, resource, quantity );
			int begin = ruler.getResourceCount(type, resource);
			Assert.IsTrue( avail.evaluate(ruler), "Eval should be true" );
			
			avail.action(ruler);
			int end = ruler.getResourceCount(type, resource);
			Assert.IsTrue( end < begin, "Problem: expected that " +end + " sould be less that "+ begin );
		}
		
		[Test]
		public void testResourceRef()
		{
			Action avail = new ResourceRef(type, resource);
			Assert.IsTrue( avail.evaluate(ruler) == avail.action(ruler) == true, "Ups, ResourceRef not working" );
			
			Action avail2 = new ResourceRef(type, resource + seed);
			Assert.IsTrue(  !avail2.evaluate(ruler) && !avail2.action(ruler), "Ups, ResourceRef not working" );
			
			Action avail3 = new ResourceRef(type + seed, resource);
			Assert.IsTrue(  !avail3.evaluate(ruler) && !avail3.action(ruler), "Ups, ResourceRef not working" );
		}
	
		[Test]
		public void testStatic()
		{
			bool eval = true;
			bool action = false;
		
			Static s = new Static(eval,action);
			
			Assert.IsTrue( s.evaluate(null) == eval );
			Assert.IsTrue( s.action(null) == action );
		}
		
		[Test]
		public void testSupressFactory()
		{
			Action avail = new SupressFactory(type, resource);
		
			Assert.IsTrue( avail.evaluate(ruler), "It should exist the " + type + "-" + resource + " factory" );
			avail.action(ruler);
			Assert.IsFalse( ruler.isFactoryAvailable(type, resource), "SupressFactory failed");
		}
	
		[Test]
		public void testRestriction()
		{
			Action avail = new Restriction(type, "score", "equal-to", type, "score");
			Assert.IsTrue( avail.evaluate(planet) );
		
			avail = new Restriction(type, "score", "less-than", type, "food");
			Assert.IsTrue( avail.evaluate(planet) );
			
			avail = new Restriction(type, "food", "bigger-than", type, "score");
			Assert.IsTrue( avail.evaluate(planet) );
		}

		[Test]
		public void testDisallowBuild()
		{
			planet.addResource( "labor", 10000 );
			planet.addResource( "mp", 10000 );
			planet.addResource( "gold", 10000 );
				
			ResourceInfo info = planet.getResourceInfo("Building");
			Action disallow = new DisallowBuild( "Building", "Farm" );
			int count = info.AvailableFactories.Count;

			disallow.action(planet);
			Assert.AreEqual( count, info.AvailableFactories.Count + 1, "DisallowBuild not decrementing" );
			Result result = planet.canQueue("Building", "Farm", 1);
			Assert.IsFalse( result.Ok, "Can build anyway " + result.log() );

			disallow.undo(planet);
			Assert.AreEqual( count, info.AvailableFactories.Count, "DisallowBuild not decrementing" );
			result = planet.canQueue("Building", "Farm", 1);
			Assert.IsTrue( result.Ok, "Can't build " + result.log() );
		}
		
		[Test]
		public void testQueuerestriction()
		{
			Action action = new QueueRestriction("Building", "Farm");
			
			Assert.IsTrue( action.evaluate(planet), "#1" );
			Result result = planet.canQueue("Building", "Farm", 1);
			Assert.IsTrue(result.Ok, result.log());
			planet.queue("Building", "Farm", 1);
			Assert.IsFalse( action.evaluate(planet), "#3" );
			planet.turn();
			Assert.IsFalse( action.evaluate(planet), "#4" );
		}
		
		[Test]
		public void testBattlesFought()
		{
			Action action = new BattlesFought(0);
			Assert.IsTrue(action.evaluate(planet));
			Assert.IsTrue(action.evaluate(ruler));
			
			action = new BattlesFought(10);
			Assert.IsFalse(action.evaluate(planet));
			Assert.IsFalse(action.evaluate(ruler));
			
			ruler.Victories = 500;
			Assert.IsTrue(action.evaluate(planet));
			Assert.IsTrue(action.evaluate(ruler));
		}
		
		[Test]
		public void NumberOfRestrictions()
		{
			Assert.AreEqual( Chronos.Core.Loader.Restrictions.Count, 3 );
		}
		
		[Test]
		public void TransformResource()
		{
			int factor = 100;
			string rare = "elk";
			string intrinsic = "food";
		
			TransformRareResourceFactory fact = new TransformRareResourceFactory();
			Hashtable hash = new Hashtable();
			hash.Add("rare", rare);
			hash.Add("intrinsic", intrinsic);
			hash.Add("factor", factor);
			
			Action trans = fact.Create(hash); 
			planet.addResource("Rare", "elk", 1);
			int before = planet.getResourceCount("Intrinsic", intrinsic);
			
			Assert.IsTrue( trans.evaluate(planet), trans.log() );
			Assert.IsTrue( trans.action(planet), trans.log() );
			Assert.AreEqual( planet.getResourceCount("Rare", rare), 0 );
			Assert.AreEqual( planet.getResourceCount("Intrinsic", intrinsic), before + factor );
			
			trans.undo(planet);
			Assert.AreEqual( planet.getResourceCount("Rare", rare), 1 );
			Assert.AreEqual( planet.getResourceCount("Intrinsic", intrinsic), before );
		}
		
		[Test]
		public void TestTerrainAction()
		{
			Terrain test = planet.Info.Terrain;
			TerrainFactory factory = new TerrainFactory();
			Hashtable args = new Hashtable();
			args.Add( "type", test.Description );
			
			Action action = factory.Create(args);
			Assert.IsTrue(action.evaluate(planet), action.log());
			Assert.IsTrue(action.action(planet), action.log());
		}
		
		[Test]
		public void TestTransform()
		{
			Action action = new Transform("gold", "mp", 10);
			
			int gold = planet.Gold;
			int mp = planet.MP;
			
			action.action(planet);
			
			Assert.AreEqual( gold - 10, planet.Gold, "Gold not consumed");
			Assert.AreEqual( mp + 10, planet.MP, "MP not added");
		}
		
		[Test]
		public void TestReserveResource()
		{
			int ammount = 10;
			
			Action action = new ReserveResource("housing", "1");
			int housing = planet.Housing;
			
			action.action(planet, ammount);
			Assert.AreEqual( housing - ammount, planet.Housing, "Resource not decremented");
			
			action.undo(planet, ammount);
			Assert.AreEqual( planet.Housing, housing, "Resource not replaced" );
		}
		
	};

}
