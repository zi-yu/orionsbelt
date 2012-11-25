using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Core;
using Chronos;
using Chronos.Persistence;
using Chronos.Utils;
using Chronos.Actions;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	public class ResourceManagerTester {
	
		private Ruler ruler;
		private Planet planet;
	
		[SetUp]
		public void init()
		{
			ruler = new Ruler( Universe.factories, "PRE" );
			planet = new Planet(ruler, Universe.factories, "PRE Planet", Coordinate.First );
			ruler.addPlanet(planet);
		}

		[Test]
		public void testDependencies()
		{
			planet.addResource("Building", "StarPort", 1);
			ruler.addResource("Research", "AdvancedFlightI", 1);
			ruler.turn();

			Assert.IsTrue( planet.isFactoryAvailable("Unit", "Rain"), "There is no Rain" );
		}
	
		[Test]
		public void testCancelDuringBuild()
		{
			planet.addResource("Intrinsic", "mp", 10000 );
			planet.addResource("Intrinsic", "labor", 10000 );
			
			Result result = planet.canQueue("Building", "Mine", 1);
			Assert.IsTrue( result.Ok, result.log() );

			int first = planet.getResourceCount("Intrinsic", "groundSpace");
			
			planet.queue("Building", "Mine", 1);
			int second = planet.getResourceCount("Intrinsic", "groundSpace");

			planet.turn();
			planet.cancel("Building");

			int third = planet.getResourceCount("Intrinsic", "groundSpace");

			Assert.IsTrue( first > second );
			Assert.AreEqual( first, third );
		}
		
		[Test]
		public void TestRareResourceRatio()
		{
			ResourceFactory factory = GetRareResourceNotFromPlanet(planet);
			Assert.AreEqual( 0, planet.getRatio(factory.Name), "Factory: " + factory.Name );
		}
		
		private ResourceFactory GetRareResourceNotFromPlanet( Planet planet )
		{
			ArrayList list = new ArrayList();
			planet.turn();
			
			foreach( ResourceFactory factory in Universe.getFactories("planet", "Rare").Values ) {
				if( planet.getResourceCount(factory.Category, factory.Name ) == 0 ) {
					list.Add(factory);
				}
			}
			
			Assert.AreEqual( Universe.getFactories("planet", "Rare").Values.Count - 2, list.Count );
			
			return (ResourceFactory) list[ MathUtils.random(0, list.Count) ];
		}
	};
}
