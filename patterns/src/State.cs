using System.Collections;

namespace DesignPatterns {
	
	public class State {
		
		#region Fields

		private string name;
		private string parent;
		private string directSon;
		private Hashtable events = new Hashtable();

		#endregion

		#region Public 

		public Event GetEvent( string name ) {
			return events[name] as Event;
		}

		public bool HasEvent( string name ) {
			return  events.ContainsKey( name );
		}

		public void AddEvent( Event e ) {
			events.Add( e.Name, e );
		}

		#endregion

		#region Properties

		public string Name {
			get { return name; }
			set { name = value; }
		}

		public string Parent {
			get { return parent; }
			set { parent = value; }
		}

		public string DirectSon {
			get { return directSon; }
			set { directSon = value; }
		}

		#endregion
		
	}
}
