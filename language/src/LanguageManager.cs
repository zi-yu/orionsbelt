// created on 3/20/04 at 1:27 a

using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.IO;
using Language.Exceptions;

namespace Language {

	public class LanguageManager : ILanguageManager {
	
		private Cache cache;
		private string directory;
		private string defaultLocale;
		
		/// <summary>Cria um LanguageManager</summary>
		public LanguageManager( Cache c, string langDir, string _defaultLocale )
		{
			cache = c;
			directory = HttpContext.Current.Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + langDir;
			defaultLocale = _defaultLocale;
		}
		
		/// <summary>Retorna todas as culturas suportadas</summary>
		public string[] Languages {
			get {
				return Directory.GetDirectories(directory);
			}
		}
		
		/// <summary>Retorna um LanguageInfo associado a uma cultura</summary>
		public ILanguageInfo getLanguageInfo( string locale )
		{
			locale = locale.ToLower();
			
			string dir = getDir(locale);
			string key = "language:" + dir.Remove(0, dir.LastIndexOf(Path.DirectorySeparatorChar));
			
			lock(this) {
				object o = cache[key];
				if( o != null ) {
					return (LanguageInfo) o;
				}
				
				HttpContext.Current.Trace.Write("LanguageInfo","Criado o local: "+locale+" na key "+key  );
				LanguageInfo info = generateLanguageInfo(locale, dir);
				cache[key] = info;
				return info;
			}
		}
		
		/// <summary>Cria um LanguageInfo</summary>
		private LanguageInfo generateLanguageInfo( string locale, string dir )
		{
			return new LanguageInfo(dir);
		}
		
		/// <summary>Retorna a primeira parte da string pt-PT</summary>
		private static string getFirst( string locale )
		{
			int idx = locale.IndexOf('-');
			if( idx < 0 ) {
				return null;
			}
			return locale.Substring(0,idx);
		}
		
		/// <summary>Retorna a directoria associada a um locale</summary>
		private string getDir( string locale )
		{
			char sep = Path.DirectorySeparatorChar;
			
			string firstTry = directory + sep + locale;
			if( Directory.Exists(firstTry) ) {
				return firstTry;
			}
			
			string secondTry = getFirst(locale);
			if( secondTry != null) {
				secondTry = directory + sep + secondTry;
				if( Directory.Exists(secondTry) ) {
					return secondTry;
				}
			}
			
			string thirdTry = directory + sep + defaultLocale;
			if( Directory.Exists(thirdTry) ) {
				return thirdTry;
			}
			
			string fourthTry = getFirst(defaultLocale);
			if( fourthTry != null) {
				fourthTry = directory + sep + fourthTry;
				if( Directory.Exists(fourthTry) ) {
					return fourthTry;
				}
			}
			
			string exception = "Locale '"+locale+"' dir not found. Tryed " + firstTry;
			if( secondTry != null ) {
				exception += ", " + secondTry;
			}
			exception += " and the default " + thirdTry;
			if( fourthTry != null ) {
				exception += " and " + fourthTry;
			}
			
			exception += "; all in base directory " + directory + "/";
			
			throw new LanguageException(exception);
		}
	
	};

}
