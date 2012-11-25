// created on 4/4/04 at 9:55 a

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;

namespace Alnitak {

	/// <summary>Mostra todas as perguntas</summ<ry>
	public class FaqControl : Control {
	
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			ILanguageInfo info = CultureModule.getLanguage();
			
			Hashtable questions = info.getTargetHash("faq");
			writeIndex( writer, questions );
			writeContent( writer, questions );
		}
		
		/// <summary>Escreve todas as perguntas</summary>
		private void writeIndex( HtmlTextWriter writer, Hashtable hash )
		{
			writer.WriteLine("<div class='faq-index'><ol>");
			for( int i = 0; i < hash.Count / 2; ++i ) {
				string key = "P" + i;
				string value = (string) hash[key];
				if( value == null ) {
					continue;
				}
				writer.WriteLine("\t<li>");
				writer.WriteLine("\t\t<a href=\"#{0}\">{1}</a>", key, value);
				writer.WriteLine("\t</li>");
			}
			writer.WriteLine("</ol></div>");
		}
		
		/// <summary>Escreve todas as perguntas e respostas</summary>
		private void writeContent( HtmlTextWriter writer, Hashtable hash )
		{
			writer.WriteLine("<div class='faq-content'>");
			for( int i = 0; i < hash.Count / 2; ++i ) {
				string questionkey = "P" + i;
				string questionvalue = (string) hash[questionkey];
				writer.WriteLine("\t<a name='{0}' href='#{0}'><div class='faq-content-question'><b>{1}. </b>{2}</div></a>", questionkey, i + 1, questionvalue );
				
				string replykey = "R" + i;
				string replyvalue = (string) hash[replykey];
				writer.WriteLine("\t<div class='faq-content-reply'>{0}</div>", replyvalue );
			}
			writer.WriteLine("</div>");
		}
	
	};

}