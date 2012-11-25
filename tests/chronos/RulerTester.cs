// created on 4/16/04 at 10:03 a

using System;
using System.Collections;
using Chronos;
using Chronos.Resources;
using Chronos.Alliances;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Info.Results;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class RulerTester {
    
    	private Ruler ruler;
    	private Alliance alliance;
    	private Planet planet;
    	
    	[SetUp]
    	public void startTest()
    	{
    		alliance = new Alliance(Universe.instance,"Test");
    		ruler = new Ruler( Globals.factories, "pre" );
    		planet = new Planet( Coordinate.First );

    		ruler.addResource("Intrinsic", "maxPlanets", 500);
    	}
    	
    	[Test]
    	public void constructorTest()
    	{
    		Assert.IsFalse( ruler.IsSharing, "Alliance must start not sharing" );
    		Assert.IsTrue( ruler.Planets != null, "The ruler must have a Planets container" );
    		Assert.IsTrue( ruler.Planets.Length == 0, "The ruler must not have planets when created" );
    		Assert.IsTrue( ruler.Owner == null, "Ruler Owner must start null" );
    	}
    	
    	private void checkPlanetOwner( Planet planet )
    	{
    		Assert.IsTrue( planet.Owner == ruler, "Ruler's owner not set to alliance" );
    	}
    	
    	private void checkPlanetsCount( int expected )
    	{
    		Assert.IsTrue( ruler.Planets.Length == expected, "When adding, Members count not incremented. Expected: "+expected+" got "+ alliance.Members.Length);
    	}
    	
    	private void checkGetIndex( Planet toFind )
    	{
    		int idx;
    		Assert.IsTrue( (idx=ruler.getIndex(toFind.Name)) >= 0, "[1 ]Added a Ruler, but not found later in Members");
    		int idx2 = ruler.getIndex( ruler.Planets[idx] );
    		Assert.IsTrue( idx2 >= 0, "[2] Added a Ruler, but not found later in Members");
    		Assert.IsTrue( idx2 == idx, "[3] getIndex's returning diferent index's for the same planet");
    	}
    	
    	[Test]
    	public void addTest()
    	{
			planet.init( ruler, "Terra", Globals.factories );
    		
    		checkPlanetOwner(planet);
    		checkPlanetsCount( 1 );
    		
    		checkGetIndex(planet);
    	}
    	
    	[Test]
    	public void multipleAddTest()
    	{
    		int toAdd = 5;
    		int count = ruler.Planets.Length;

			planet.init( ruler, "Terra", Globals.factories );

    		for( int i = 0; i < toAdd; ++i ) {
    			new Planet(ruler, Globals.factories, "pre" + i, Coordinate.First);
    		}
    		
    		checkPlanetsCount( count + toAdd +1 );
    		checkGetIndex(planet);
    	}
    	
    	
    	[Test]
    	public void binarySearchTest()
    	{
			new Planet(ruler, Globals.factories, "name1", Coordinate.First);
			new Planet(ruler, Globals.factories, "name3", Coordinate.First);
			new Planet(ruler, Globals.factories, "name4", Coordinate.First);
			new Planet(ruler, Globals.factories, "name5", Coordinate.First);
			new Planet(ruler, Globals.factories, "name2", Coordinate.First);
			
			int idx = ruler.getIndex("name1");
			Assert.IsTrue( idx == ruler.getIndex(ruler.Planets[idx]), "binary search not working" );
    	}

    	
    	[Test]
    	public void removeTest()
    	{
			planet.init( ruler, "Terra", Globals.factories );
    		
    		Planet member = ruler.Planets[0];
    		
    		ruler.removePlanet( member );
    		Assert.IsTrue(ruler.Planets.Length == 0, "Alliance should be empty");
    	}
    	
    	[Test]
    	public void multipleRemoveTest()
    	{
    		int toAdd = 5;
    		
    		for( int i = 0; i < toAdd; ++i ) {
    			new Planet(ruler, Globals.factories, "pr" + i, Coordinate.First);
    		}
			planet.init( ruler, "Terra", Globals.factories );

    		Planet member = ruler.Planets[ ruler.getIndex(planet) ];
    		Assert.IsTrue( member == planet, "getIndex not working");
    			
    		ruler.removePlanet( member );
    		
    		Assert.IsTrue( ruler.getIndex(member) < 0, "Removed an element but he is still in the alliance" );
    		Assert.IsTrue( planet.Owner == null, "When removing an Alliance member, he must get owner = null" );
    		Assert.IsTrue( ruler.Planets.Length == toAdd, " Removed an element but count not decremented" );
    		
    		Planet member2 = ruler.Planets[ ruler.Planets.Length - 1 ];
    		ruler.removePlanet( member2 );
    		
    		Assert.IsTrue( ruler.getIndex(member2) < 0, "Removed an element but he is still in the alliance" );
    		Assert.IsTrue( ruler.Planets.Length == toAdd - 1, " Removed an element but count not decremented" );
    		
    		foreach( Planet m in ruler.Planets ) {
    			Assert.IsTrue(m != null, "No null members allowed");
    		}
    	}
    	
    	[Test]
    	public void testTech()
    	{
    		Hashtable hash = (Hashtable) Universe.factories["ruler"];
    		SortedList factories = (SortedList) hash["Research"];
    		ResourceFactory fact = (ResourceFactory) factories["AdvancedFlightI"];

    		int baseDuration = fact.Duration.Value;
    
			Result result = ruler.canQueue("Research", "AdvancedFlightI", 1);
			Assert.IsTrue( result.Ok, "First : " + result.log() );

    		ruler.queue( "Research", "AdvancedFlightI");
    		ruler.turn();
    		Assert.AreEqual( ruler.current("Research").RemainingTurns, baseDuration );
    		
    		ruler.cancel("Research");
    		ruler.addResource("Intrinsic", "culture", 500);
    	
			result = ruler.canQueue("Research", "AdvancedFlightI", 1);
			Assert.IsTrue( result.Ok, "Second: " + result.log() );

    		ruler.queue( "Research", "AdvancedFlightI");
    		ruler.turn();
    		Assert.IsTrue( ruler.current("Research").RemainingTurns <= baseDuration, "Culture should decrease duration" );
    	}
		
		#region Bugs
		
		[Test]
		public void Bug_1186934()
		{
			ruler.addPlanet(planet);
			planet.init(ruler, "buu", Universe.factories);
			
			Coordinate c = Coordinate.First;
			
			Fleet fleet = new Fleet("Mobile",c, planet);
			planet.addFleet(fleet);
			Assert.AreEqual(1, ruler.TotalFleets, "Ruler should have 1 Fleets");

			
			fleet = new Fleet("Mobile 2",c, planet);
			planet.addFleet(fleet);
			Assert.AreEqual(2, ruler.TotalFleets, "Ruler should have 2 Fleets");
			
			planet.removeFleet(fleet);
			ruler.addUniverseFleet(fleet);
			Assert.AreEqual(2, ruler.TotalFleets, "Ruler should *still* have 2 Fleets");
		}
		
		#endregion
		
		#region Far Away From home Tests
		
		private void CheckHandicap( string reference, string coor, double handicap )
		{
			Assert.AreEqual(handicap, Planet.GenerateFarAwayFromHomeFactor(Coordinate.translateCoordinate(reference), Coordinate.translateCoordinate(coor)), "Wrong FarAwayFromHomeFactor for | Ref-" + reference+ " Test-" + coor);
		}
		
		[Test]
		public void NoHandicap()
		{	
			CheckHandicap("1:1:5:1", "1:1:11:2", 0);
			CheckHandicap("1:1:5:1", "1:1:12:2", 0);
			CheckHandicap("1:1:5:1", "1:1:1:2", 0);
		}
	
		[Test]
		public void CloseToHomeHandicap()
		{
			CheckHandicap("1:1:5:1", "1:1:5:1", -5);
			CheckHandicap("1:1:5:1", "1:1:5:2", -4);
			CheckHandicap("1:1:5:1", "1:1:5:3", -4);
			
			CheckHandicap("1:1:5:1", "1:1:6:2", -3);
			CheckHandicap("1:1:5:1", "1:1:7:2", -2);
			CheckHandicap("1:1:5:1", "1:1:8:2", -1);
			
			CheckHandicap("1:1:5:1", "1:1:4:2", -3);
			CheckHandicap("1:1:5:1", "1:1:3:2", -2);
			CheckHandicap("1:1:5:1", "1:1:2:2", -1);
		}
		
		[Test]
		public void SmallHandicap()
		{
			CheckHandicap("1:10:1:1", "1:11:11:2", 3);
			CheckHandicap("1:10:1:1", "1:12:12:2", 6);
			CheckHandicap("1:10:1:1", "1:13:13:2", 9);
			
			CheckHandicap("1:10:1:1", "1:9:9:2", 3);
			CheckHandicap("1:10:1:1", "1:8:8:2", 6);
			CheckHandicap("1:10:1:1", "1:7:7:2", 9);
		}
		
		[Test]
		public void MediumHandicap()
		{
			CheckHandicap("1:1:5:1", "1:10:11:2", 38);
			CheckHandicap("1:1:5:1", "1:13:20:2", 38);
			CheckHandicap("1:1:5:1", "1:20:1:2", 38);
		}
		
		[Test]
		public void LargeHandicap()
		{
			CheckHandicap("1:1:5:1", "2:2:11:2", 75);
			CheckHandicap("1:1:5:1", "3:3:20:2", 75);
		}
		/**/
		#endregion
		
		[Test]
    	public void DestroyRandomBuildingsTest()
    	{
    		Ruler ruler = new Ruler( Globals.factories, "pre" );
    		Planet planet = new Planet(ruler, Globals.factories, "Planet", Coordinate.First);
    		
    		for( int i = 0; i < 3; ++i ) {
    			planet.DestroyRandomBuildings(i);
    		}
    	}

	};

}
