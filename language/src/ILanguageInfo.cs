// created on 3/31/04 at 11:35 a

using System.Collections;

namespace Language {

	public interface ILanguageInfo {
		string getContent( string target, string reference );
		string getContent( string reference );
		string getContent( string reference, bool force );
		Hashtable getTargetHash( string target );
	};

}
