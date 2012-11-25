// created on 11/17/2004 at 4:53 PM

//#define DEBUG_SORTED_DATES

using System;
using System.Collections;

using Alnitak;
using Chronos.Utils;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class UserWatcherTester {
			
		#region User Watcher Tests
		
		[Test]
		public void testeCtor()
		{
			UserWatcher users = new UserWatcher();

			Assert.AreEqual(false, users.HasRegisteredUsers);
			Assert.IsNotNull(users.List);
			Assert.AreEqual(0, users.List.Count);
		}
		
		[Test]
		public void TestFirstRegister()
		{
			UserWatcher users = new UserWatcher();

			User user = new User();
			users.Register(user);
			Assert.AreEqual(true, users.HasRegisteredUsers, "There are registered users");
			Assert.AreEqual(1, users.List.Count);
		}
		
		[Test]
		public void TestFirst5()
		{
			UserWatcher users = new UserWatcher();

			for( int i = 0; i < 5; ++i ) {
				User user = new User();
				user.UserId = i;
				users.Register(user);
			}
			
			for( int i = 0; i < 5; ++i ) {
				User user = (User) users.List.GetByIndex(i);
				Assert.AreEqual(i, user.UserId, "Wrong order. Index " + i + " got user " + user.UserId );
			}
		}
		
		[Test]
		[Ignore("Later!")]
		public void TestFirstMany()
		{
			UserWatcher users = new UserWatcher();

			for( int i = 0; i < users.Limit + 5; ++i ) {
				User user = new User();
				user.UserId = i;
				users.Register(user);
				System.Threading.Thread.Sleep(100);
			}
			
			for( int i = 5; i < users.Limit + 5; ++i ) {
				User user = (User) users.List.GetByIndex(i - users.Limit);
				Assert.AreEqual(i, user.UserId, "Wrong order. Index " + i + " got user " + user.UserId );
			}
		}
		
		[Test]
		[Ignore("Later!")]
		public void TestFirstManyAndAddTheSame()
		{
			UserWatcher users = new UserWatcher();

			ShowContent("1", users.List);

			for( int i = 0; i < users.Limit + 10; ++i ) {
				User user = new User();
				user.UserId = i;
				users.Register(user);
				System.Threading.Thread.Sleep(100);
				ShowContent("1.1", users.List);
			}

			ShowContent("2", users.List);
			
			User user1 = new User();
			user1.UserId = 1;
			users.Register(user1);
			
			ShowContent("3", users.List);

			for( int i = 15; i < users.Limit + 10; ++i ) {
				User user = (User) users.List.GetByIndex(i - 15);
				Assert.AreEqual(i, user.UserId, "Wrong order. Index " + i + " got user " + user.UserId );
			}
			
			User user2 = (User) users.List.GetByIndex(4);
			Assert.AreEqual(1, user2.UserId, "Wrong order. Index 5 got user " + user2.UserId );
		}

		private void ShowContent( string id, SortedList list )
		{
#if DEBUG_SORTED_DATES	
			Console.WriteLine("-- {0} --- DEBUG SORTED DATES ---------", id);
			IDictionaryEnumerator it = list.GetEnumerator();
			while( it.MoveNext() ) {
				Console.WriteLine("#$ list[ {0} ] = {1}", ((DateTime)it.Key).Millisecond, ((User)it.Value).UserId);
			}
#endif
		}

		#endregion
		
	};
	
}
