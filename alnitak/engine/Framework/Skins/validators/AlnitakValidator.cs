// created on 5/7/04 at 11:30 a

using Language;
using System;
using System.Web.UI.WebControls;

namespace Alnitak {

	public class AlnitakValidator : RegularExpressionValidator {
	
		protected ILanguageInfo info;
	
		public AlnitakValidator()
		{
			info = CultureModule.getLanguage();
		}
	
		/// <summary>Retorna uma string HTML com um erro</summary>
		public static string getError( string reference )
		{
			ILanguageInfo info = CultureModule.getLanguage();
			return string.Format("{0} {1}{2}","*", info.getContent(reference), "<br/>");
		}
		
		/// <summary>Retorna uma string HTML com um erro</summary>
		public static string getError( string target, string reference )
		{
			ILanguageInfo info = CultureModule.getLanguage();
			return string.Format("{0} {1}{2}","*", info.getContent(target,reference), "<br/>");
		}
	};

}