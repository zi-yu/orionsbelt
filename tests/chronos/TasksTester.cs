// created on 9/4/2005 at 10:10 AM

//#define DEBUG_TASK_MANAGER

using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using NUnit.Framework;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Interfaces;

namespace Chronos.Tests {

	[TestFixture]
	public class TasksTester {
	
		#region Test Classes
		
		public int number;
	
		private class TestTask : ITask {
			public void turn() {
#if DEBUG_TASK_MANAGER
				Log.log("Test Task Turn");
#endif
			}
		};
		
		private class ActionTask : ITask {
			private TasksTester t;
			public ActionTask(TasksTester tester) { t = tester; }
			public void turn() {
				t.number = 0;
#if DEBUG_TASK_MANAGER
				Log.log("Number Set to Zero");
#endif
			}
		};
		
		private class IncTask : ITask {
			private TasksTester t;
			public IncTask(TasksTester tester) { t = tester; }
			public void turn() {
				++t.number;
#if DEBUG_TASK_MANAGER
				Log.log("Number Incremented to {0}", t.number);
#endif
			}
		};
		
		#endregion
	
		#region Instance Fields
		
		private TaskManager manager;
		private TaskDescriptor descriptor = TaskDescriptor.Sabotage;
		
		#endregion
		
		#region Set Up Methods
		
		[SetUp]
		public void prepare()
		{
			manager = new TaskManager();
		}
		
		#endregion
	
		#region TaskManager Tests
		
		[Test]
		public void TestInitialState()
		{
			Assert.IsNull( manager.Tasks );
		}
		
		[Test]
		public void TestRegister()
		{
			manager.Register( descriptor, new TestTask(), 10, 10 );
			Assert.IsNotNull( manager.GetList(descriptor) );
			Assert.AreEqual( 1, manager.GetList(descriptor).Count );
		}
		
		[Test]
		public void TestRemove()
		{
			TaskItem item = manager.Register( descriptor, new TestTask(), 10, 10 );
			manager.Remove(item);
			Assert.IsNull( manager.Tasks );
			
			manager.Register( descriptor, new TestTask(), 10, 10 );
			item = manager.Register( descriptor, new TestTask(), 10, 10 );
			manager.Remove(item);
			
			Assert.IsNotNull( manager.GetList(descriptor) );
			Assert.AreEqual( 1, manager.GetList(descriptor).Count );
		}
		
		[Test]
		public void TestInterval()
		{
			int interval = 10;
			manager.Register( descriptor, new ActionTask(this), interval, 10 );
			
			number = 3382;
			
			for( int i = 0; i < interval; ++i ) {
				manager.turn();
				Assert.AreEqual( number, 3382, "#1" );
			}
			
			manager.turn();
			Assert.AreEqual( number, 0, "#2" );
		}
		
		[Test]
		public void TestTimes()
		{
			int times = 10;
			number = 0;
			manager.Register( descriptor, new IncTask(this), 0, times );
			
			for( int i = 0; i < times; ++i ) {
				manager.turn();
			}
			
			Assert.AreEqual( number, times, "#1" );
		}
		
		[Test]
		public void TestAutoRepeat()
		{
			int times = TaskItem.AutoRepeat;
			number = 0;
			manager.Register( descriptor, new IncTask(this), 0, times );
			
			for( int i = 0; i < 100; ++i ) {
				int before = number;
				manager.turn();
				int after = number;
				
				Assert.AreEqual( before + 1, after );
			}
		}
    
		#endregion
	};

}