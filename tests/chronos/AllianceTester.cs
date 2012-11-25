// created on 4/13/04 at 9:13 a

using System;
using Chronos;
using Chronos.Resources;
using Chronos.Alliances;
using Chronos.Core;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class AllianceTester {
    
    	private Alliance alliance;
    	private Ruler ruler;
    	
    	[SetUp]
    	public void startTest()
    	{	
    		alliance = new Alliance(Universe.instance, "Test");
    		ruler = new Ruler( alliance, Globals.factories, "pre" );
    	}
    	
    	[Test]
    	public void constructorTest()
    	{
    		Assert.IsFalse( alliance.IsSharing, "Alliance must start not sharing" );
    		Assert.IsTrue( alliance.Members != null, "The alliance must have a Members container" );
    		Assert.IsTrue( alliance.Members.Length == 0, "The alliance must not have members when created" );
    	}
    	
    	private void checkRulerOwner( Ruler ruler )
    	{
    		Assert.IsTrue( ruler.Owner == alliance, "Ruler's owner not set to alliance" );
    	}
    	
    	private void checkMembersCount( int expected )
    	{
    		Assert.IsTrue( alliance.Members.Length == expected, "When adding, Members count not incremented. Expected: "+expected+" got "+ alliance.Members.Length);
    	}
    	
    	private void checkGetIndex( Ruler toFind )
    	{
    		int idx;
    		Assert.IsTrue( alliance.getIndex(toFind.Name) >= 0, "[1 ]Added a Ruler, but not found later in Members");
    		Assert.IsTrue( (idx=alliance.getIndex(toFind)) >= 0, "[2] Added a Ruler, but not found later in Members");
    		Assert.IsTrue( alliance.getIndex( alliance.Members[idx] ) >= 0, "[3] Added a Ruler, but not found later in Members");
    	}
    	
    	[Test]
    	public void addTest()
    	{
    		alliance.addRuler( ruler, AllianceMember.Role.Admiral );
    		
    		checkRulerOwner(ruler);
    		checkMembersCount( 1 );
    		
    		checkGetIndex(ruler);
    	}
    	
    	[Test]
    	public void multipleAddTest()
    	{
    		int toAdd = 5;
    		int count = alliance.Members.Length;
    		alliance.addRuler( ruler, AllianceMember.Role.Admiral );

    		for( int i = 0; i < toAdd; ++i ) {
    			Ruler newRuler = new Ruler(alliance, Globals.factories, "pre" + i);
    			alliance.addRuler( newRuler, AllianceMember.Role.Admiral );
    		}
    		
    		checkMembersCount( count + toAdd +1 );
    		checkGetIndex(ruler);
    	}
    	
    	[Test]
    	public void binarySearchTest()
    	{
			alliance.addRuler( new Ruler(null, Globals.factories, "name1"), AllianceMember.Role.Admiral);
			alliance.addRuler( new Ruler(null, Globals.factories, "name3"), AllianceMember.Role.Private);
			alliance.addRuler( new Ruler(null, Globals.factories, "name4"), AllianceMember.Role.Private);
			alliance.addRuler( new Ruler(null, Globals.factories, "name5"), AllianceMember.Role.Private);
			alliance.addRuler( new Ruler(null, Globals.factories, "name2"), AllianceMember.Role.Admiral);
			
			int idx = alliance.getIndex("name1");
			Assert.IsTrue( idx == alliance.getIndex(alliance.Members[idx]), "binary search not working" );
    	}

    	
    	[Test]
    	public void removeTest()
    	{
    		alliance.addRuler( ruler, AllianceMember.Role.Admiral );
    		AllianceMember member = alliance.Members[0];
    		
    		alliance.removeRuler( member );
    		Assert.IsTrue(alliance.Members.Length == 0, "Alliance should be empty");
    	}
    	
    	[Test]
    	public void multipleRemoveTest()
    	{
    		int toAdd = 5;
    		
    		for( int i = 0; i < toAdd; ++i ) {
    			Ruler newRuler = new Ruler(alliance, Globals.factories, "pr" + i);
    			alliance.addRuler( newRuler, AllianceMember.Role.Admiral );
    		}
    		alliance.addRuler( ruler, AllianceMember.Role.Private );

    		AllianceMember member = alliance.Members[ alliance.getIndex(ruler) ];
    		Assert.IsTrue( member.Ruler == ruler, "getIndex not working");
    			
    		alliance.removeRuler( member );
    		
    		Assert.IsTrue( alliance.getIndex(member) < 0, "Removed an element but he is still in the alliance" );
    		Assert.IsTrue( ruler.Owner == null, "When removing an Alliance member, he must get owner = null" );
    		Assert.IsTrue( alliance.Members.Length == toAdd, " Removed an element but count not decremented" );
    		
    		AllianceMember member2 = alliance.Members[ alliance.Members.Length - 1 ];
    		alliance.removeRuler( member2 );
    		Assert.IsTrue( alliance.getIndex(member2) < 0, "Removed an element but he is still in the alliance" );
    	
    		Assert.IsTrue( alliance.Members.Length == toAdd - 1, " Removed an element but count not decremented" );
    		
    		foreach( AllianceMember m in alliance.Members ) {
    			Assert.IsTrue(m != null, "No null members allowed");
    		}
    	}
    	
    	[Test]
    	public void ownerTest()
    	{
    		Assert.IsTrue( alliance.Owner == alliance, "The alliance it's owner of it self" );
    		alliance.Owner = new Alliance(Universe.instance,"Test2");
    		Assert.IsTrue( alliance.Owner == alliance, "The alliance it's owner of it self" );
    	}
    	
    	[Test]
    	public void resouceCountTest()
    	{
    		ruler.addResource("Intrinsic","score", 10);
    		alliance.addRuler(ruler, AllianceMember.Role.Admiral);
    		int count = alliance.getResourceCount("score");
    		Assert.IsTrue( count == 10, "Score not counting right. Expected 10 got "+count);
    		
    		for( int i = 0; i < 3; ++i ) {
    			Ruler toAdd = new Ruler( alliance, Globals.factories, "pre"+i );
    			toAdd.addResource("Intrinsic","score",10);
    			alliance.addRuler( toAdd,AllianceMember.Role.Admiral );
    		}
    		
    		count = alliance.getResourceCount("score");
    		Assert.IsTrue( count == 40, "Score not counting right. Expected 40 got "+count);
    	}
	
	};

}
