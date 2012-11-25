// created on 3/20/04 at 12:26 a

using System;

namespace Language.Exceptions {

	public class LanguageException : Exception {
	
		public LanguageException( string reason )
			: base(reason)
		{
		}
	
	};

}