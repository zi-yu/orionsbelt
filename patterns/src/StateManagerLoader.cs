using System;
using System.Collections;
using System.Xml;

namespace DesignPatterns {
	
	public class StateManagerLoader {

		#region Static Fields

		private static Hashtable pointers = new Hashtable();

		private static Hashtable states = new Hashtable();

		private delegate void ParseStateDelegate( XmlNode node );
		private delegate void ParseEventDelegate( State state, XmlNode node );

		#endregion

		#region Private Static

		private static XmlDocument LoadXmlDoc( string filename ) {
			XmlValidatingReader reader = null;
			XmlDocument doc = null;

			try {

				XmlTextReader txtReader = new XmlTextReader(filename);
				reader = new XmlValidatingReader(txtReader);

				doc = new XmlDocument();
				doc.Load(reader);

				return doc;

			} finally {
				if ( reader != null ) 
					reader.Close();
			}
		}

		private static string GetString( string attr, XmlNode node ) {
			XmlAttribute obj = (XmlAttribute)node.Attributes[attr];
			if( null != obj )
				return obj.Value;

			return string.Empty;
		}

		private static bool GetBool( string attr, XmlNode node ) {
			XmlAttribute obj = (XmlAttribute)node.Attributes[attr];
			if( null != obj )
				return bool.Parse(obj.Value);

			return false;
		}

		#endregion
		
		#region Public Static
		
		public static void Load( string filename, StateManager manager) {
			Load( LoadXmlDoc(filename), manager );
		}
		
		public static void LoadXml( string xml, StateManager manager) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			Load( doc, manager );
		}

		public static void Load(XmlDocument doc, StateManager manager) {
			if( doc != null) {
                foreach( XmlNode node in doc.DocumentElement.ChildNodes ) {
                	if( node is XmlComment) {
                		continue;
                	}
					((ParseStateDelegate)pointers[node.Name])(node);
                }
				manager.AllStates = states;
				manager.Current = manager.AllStates[doc.DocumentElement.Attributes["start"].Value] as State;
			}
		}

		#endregion
		
		#region Constructor

		static StateManagerLoader() {
			pointers["State"] = new ParseStateDelegate(ParseState);
			pointers["Event"] = new ParseEventDelegate(ParseEvent);
		}

		#endregion

		#region Parsers

		private static void ParseState( XmlNode node ) {
			State state = new State();
			state.Name = GetString("name",node);
			state.Parent = GetString("parent",node);
			state.DirectSon = GetString("directSon",node);

			foreach( XmlNode child in node.ChildNodes ) {
				if( node is XmlComment) {
                		continue;
                	}
				((ParseEventDelegate)pointers[child.Name])(state,child);
			}
			states[state.Name] = state;
		}

		private static void ParseEvent( State state, XmlNode node ) {
			Event e = new Event();
			e.Name = GetString("name",node);
			e.NewState = GetString("newState",node);
			e.NewStateFromHistory= GetBool("newStateFromHistory",node);
			e.SaveCurrentState = GetBool("saveCurrentState",node);

			state.AddEvent( e );
		}

		#endregion
	}
}
