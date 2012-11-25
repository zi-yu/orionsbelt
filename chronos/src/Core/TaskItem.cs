// created on 9/4/2005 at 9:30 AM

using System;
using System.Collections;
using Chronos.Interfaces;
using Chronos.Core;

namespace Chronos.Core {

	[Serializable]
	public class TaskItem : IIdentifiable, ITask {
	
		#region Static
		
		public const int AutoRepeat = -1; 
		
		#endregion
		
		#region Instance Fields
		
		private ITask task;
		private int interval;
		private int currentInterval;
		private int times;
		private int currentTimes;
		private int id;
		private TaskDescriptor type;
		
		#endregion
		
		#region Instance Properties
		
		public int TurnsToAction {
			get { return interval - currentInterval; }
		}
		
		public int Id {
			get { return id; }
		}
		
		public ITask Task {
			get { return task; }
		}
		
		public bool Finished {
			get {
				if( times == AutoRepeat ) {
					return false;
				}
				return currentTimes >= times;
			}
		}
		
		public TaskDescriptor Descriptor {
			get { return type; }
		}
		
		#endregion
		
		#region Ctors
		
		public TaskItem( TaskDescriptor _type, ITask _task, int _interval, int _times ) 
		{
			type = _type;
			task = _task;
			interval = _interval;
			currentInterval = 0;
			times = _times;
			currentTimes = 0;
			id = Universe.instance.generateTaskId();
		}
		
		#endregion
		
		#region Public Methods
		
		public override string ToString()
		{
			return string.Format("Task: {0} - Interval: {1} - Repeat {2} times", task, interval, times);
		}
		
		#endregion
		
		#region ITasks Implementation
		
		public void turn()
		{
			if( currentInterval < interval ) {
				++currentInterval;
				return;
			}
			
			currentInterval = 0;
			
			if( currentTimes < times || times == AutoRepeat ) {
				task.turn();
			}
			
			++currentTimes;
		}
		
		#endregion
	
	};

}