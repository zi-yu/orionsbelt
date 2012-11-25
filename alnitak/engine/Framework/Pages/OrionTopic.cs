
namespace Alnitak {
	
	public class OrionTopic {
		
		#region Instance Fields
		
		private string topic;
		private bool exists;
		
		#endregion
		
		#region Ctros
		
		public OrionTopic() : this(string.Empty)
		{
		}
		
		public OrionTopic( string _topic )
		{
			topic = _topic;
			exists = true;
		}

		public OrionTopic( string _topic, bool _exists ) : this(_topic)
		{
			exists = _exists;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica um nome completo de um WikiTopic</summary>
		public string FullTopic {
			get { return topic; }
		}

		/// <summary>Indica se o tópico existe</summary>
		public bool Exists {
			get { return exists; }
		}
		
#if WIKI
		/// <summary>Indica um WikiTopic com base neste OrionTopic</summary>
		public FlexWiki.AbsoluteTopicName FlexWikiTopic {
			get { return new FlexWiki.AbsoluteTopicName(FullTopic); }
		}
#endif
		
		#endregion
		
		#region Utilities
		
		public override string ToString()
		{
			return FullTopic;
		}
		
		#endregion
	
	};
}
