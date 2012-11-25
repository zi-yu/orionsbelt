// created on 11/17/2004 at 4:53 PM

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
	public class Bugs {
		
		#region Instance Fields
		
		private Hashtable factories;
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
		}
		
		#endregion
		
		#region Bugs
		
		[Test]
		public void test_updateResources()
		{
			ruler.turn();
		}
		
		[Test]
		public void test_1049363()
		{
			int startSpace = 10;
		
			planet.reset();
			planet.addResource("Intrinsic", "groundSpace", startSpace);
			planet.addResource("Intrinsic", "housing", 50000);
		
			Assert.AreEqual( planet.getResourceCount("groundSpace"), startSpace );
			
			planet.addResource("mp", 10000);
			planet.addResource("gold", 10000);
			planet.addResource("housing", 10000);
			planet.addResource("labor", 10000);
			
			Result result = planet.canQueue("Building", "Farm", 1);
			Assert.IsTrue( result.Ok, result.log() );
			
			planet.queue("Building", "Farm", 1);
			do {
				planet.turn();
			} while( planet.current("Building") != null );
			
			Assert.IsTrue( startSpace > planet.getResourceCount("Intrinsic","groundSpace") );
			
			planet.take("Building", "Farm", 1);
			
			Assert.AreEqual( startSpace, planet.getResourceCount("Intrinsic","groundSpace") );
			
		}


		/// <summary>
		/// Bug que ocorria quando o resource da fleet j existia
		/// </summary>
		[Test]
		public void teste_addShip() {
			Ruler owner = new Ruler( Universe.factories, "Pyro" );
			Coordinate c = Coordinate.First;
			Fleet fleet = new Fleet("Pyro's Fleet",c, owner);

			ResourceFactory crusader = Universe.getFactory("planet", "Unit", "Crusader");

			fleet.addShip("crusader",10);//,crusader.create() ) ;
			fleet.removeAllShips();
			fleet.addShip("crusader",10);//,crusader.create());
		}
		
		#endregion
	
	};

}
