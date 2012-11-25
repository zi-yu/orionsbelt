// created on 8/10/2005 at 10:52 AM

//#define DEBUG_RARE_RESOURCES

using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Core;
using Chronos;
using Chronos.Utils;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	public class RareResources {
		
		#region Instance Fields
		
		private Planet planet;
		private Ruler ruler;
		
		#endregion
		
		#region Set Up Methods
		
		[SetUp]
		public void prepare()
		{
			ruler = new Ruler(null, Universe.factories,"BuuBuu");
			planet = new Planet( ruler, Universe.factories, "Buu", Coordinate.First );
		}
		
		#endregion
		
		#region Rare Resources
		
		[Test]
		public void TestRareProperty()
		{
			Assert.IsTrue(Universe.getFactory("planet", "Rare", "elk").create().Rare, "#1");
			Assert.IsFalse(Universe.getFactory("planet", "Intrinsic", "food").create().Rare, "#2");
			Assert.IsTrue(Resource.IsRare("elk"), "#3");
			Assert.IsFalse(Resource.IsRare("food"), "#4");
		}
		
		[Test]
		public void TestPlanetRareResources()
		{
			for( int i = 0; i < 10; ++i ) {
				Planet tmp = new Planet( Coordinate.First );
				tmp.init(new Ruler(null, Universe.factories, "a"), "buu", Universe.factories); 
#if DEBUG_RARE_RESOURCES
				Log.log("--------- DEBUG RARE MODIFIERS --");
				Log.log("Terrain: {0}", tmp.Info.Terrain.Description);
				Log.log( tmp.ModifiersRatio );
				Log.log("---------------------------------");
#endif
			}
		}
	
		#endregion
		
		#region Rare Resources Buildings
		
		[Test]
		public void TestOilBuildings()
		{
			Globals.ToRareResources(ruler, planet);
			Assert.AreEqual( planet.getResourceCount("oil"), 0 );

			int oil = planet.getResourceCount("Rare", "oil");
			planet.addResource("Intrinsic", "food", 5000000);
			planet.addResource("Intrinsic", "housing", 50000);
			planet.addResource("Intrinsic", "labor", 50000);
			planet.addResource("Intrinsic", "gold", 5000);
			planet.addResource("Intrinsic", "mp", 5000);
			
			planet.turn();
			Assert.IsTrue( planet.getResourceCount("Rare", "oil") > oil, "Planet Oil: " + planet.getResourceCount("oil") + " Had before: " + oil );
			
			planet.addResource("Intrinsic", "mp", 5000);
			planet.addResource("Intrinsic", "housing", 50000);
			planet.addResource("Intrinsic", "labor", 5000);
			
			Globals.Build(planet, "Building", "OilPlant", 1);
			
			oil = planet.getResourceCount("Rare", "oil");
			int mp = planet.getResourceCount("Intrinsic", "mp");
			int energy = planet.getResourceCount("Intrinsic", "energy");
			planet.turn();
			
			int expectedOil = oil + planet.getPerTurn("Rare","oil");
			int expectedMp = mp + planet.getPerTurn("Intrinsic","mp");
			int expectedEnergy = energy + planet.getPerTurn("Intrinsic","enery");
			
			oil = planet.getResourceCount("Rare", "oil");
			mp = planet.getResourceCount("Intrinsic", "mp");
			energy = planet.getResourceCount("Intrinsic", "energy");
			
			Assert.IsTrue( expectedOil > oil, "Oil not spent" );
			Assert.IsTrue( expectedMp == mp, "MP not incresed - expected: " + expectedMp + " got: " +mp );
			//Assert.IsTrue( expectedEnergy == energy, "Energy not incresed" ); 
		}
		
		#endregion
	
	};

}
