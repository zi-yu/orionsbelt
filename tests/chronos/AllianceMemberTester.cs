// created on 4/13/04 at 11:27 a

using System;
using Chronos;
using Chronos.Resources;
using Chronos.Alliances;
using Chronos.Core;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	public class AllianceMemberTester {

		[Test]
		public void sortTest()
		{
			int count = 5;
			AllianceMember[] array = new AllianceMember[count];
			
			array[0] = new AllianceMember( new Ruler(null, Globals.factories, "name3"), AllianceMember.Role.Private);
			array[1] = new AllianceMember( new Ruler(null, Globals.factories, "name4"), AllianceMember.Role.Private);
			array[2] = new AllianceMember( new Ruler(null, Globals.factories, "name5"), AllianceMember.Role.Private);
			array[3] = new AllianceMember( new Ruler(null, Globals.factories, "name1"), AllianceMember.Role.Admiral);
			array[4] = new AllianceMember( new Ruler(null, Globals.factories, "name2"), AllianceMember.Role.Admiral);
			
			AllianceMember[] members = new AllianceMember[count];
			members[0] = array[2];
			members[1] = array[3];
			members[2] = array[4];
			members[3] = array[1];
			members[4] = array[0];
		
			Array.Sort(members);
			
			for( int i = 0; i < count; ++i ) {
				Assert.IsTrue( object.ReferenceEquals(array[i],members[i]), "AllianceMember[] not corretly sorted" );
			}
		}
	
	};

}
