// created on 9/4/2005 at 9:23 AM

using System;
using System.Collections;
using Chronos.Interfaces;

namespace Chronos.Core {

	[Serializable]
	public class TaskManager : ITask {
		
		#region Instance Fields
		
		private Hashtable tasks;
		
		#endregion
		
		#region Instance Properties
		
		public Hashtable Tasks {
			get { return tasks; }
		}
		
		#endregion
		
		#region Ctors
		
		public TaskManager() 
		{
			tasks = null;
		}
		
		#endregion
		
		#region Public Methods
		
		public TaskItem Register( TaskDescriptor type, ITask task, int interval, int times )
		{
			ArrayList list = SafeGetList(type);
			TaskItem item = new TaskItem(type, task, interval, times);
			list.Add( item );
			
			return item;
		}
		
		public ArrayList GetList( TaskDescriptor type )
		{
			if( tasks == null ) {
				return null;
			}
			
			return (ArrayList) tasks[type];
		}
		
		private ArrayList SafeGetList( TaskDescriptor type )
		{
			if( tasks == null ) {
				tasks = new Hashtable();
			}
			
			ArrayList list = (ArrayList) tasks[type];
			if( list == null ) {
				list = new ArrayList();
				tasks.Add( type, list );
			}
			
			return list;
		}
		
		public void Remove( TaskItem item )
		{
			Remove( item.Descriptor, item.Id );
		}
		
		public void Remove( TaskDescriptor type, int taskId )
		{
			ArrayList list = (ArrayList) tasks[type];
			if( list == null ) {
				return;
			}
			
			TaskItem item = GetTaskItem(list, taskId);
			if( item == null ) {
				return;
			}
			
			list.Remove(item);
			
			if( list.Count == 0 ) {
				tasks.Remove(type);
			}
			
			if( tasks.Count == 0 ) {
				tasks = null;
			}
		}
		
		public bool HasTask( TaskDescriptor type, int taskId )
		{
			if( tasks == null ) {
				return false;
			}
			
			ArrayList list = (ArrayList) tasks[type];
			if( list == null ) {
				return false;
			}
			
			return GetTaskItem( list, taskId ) != null;
		}
		
		public bool HasTask( TaskDescriptor type )
		{
			if( tasks == null ) {
				return false;
			}
			
			return tasks[type] != null;
		}
		
		public TaskItem GetTaskItem( ArrayList list, int taskId )
		{	
			foreach( TaskItem item in list ) {
				if( item.Id == taskId ) {
					return item;
				}
			}
			return null;
		}
		
		#endregion
		
		#region ITaks Implementation
		
		public void turn()
		{
			if( tasks == null ) {
				return;
			}
			
			ArrayList toRemove = new ArrayList();
		
			foreach( ArrayList list in tasks.Values ) {
				foreach( TaskItem item in list ) {
					item.turn();
					if( item.Finished ) {
						toRemove.Add(item);
					}
				}
			}
			
			foreach( TaskItem item in toRemove ) {
				Remove( item );
			}
		}
		
		#endregion
	
	};

}