// created on 3/29/04 at 12:50 a

using Language;
using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Globalization;

using Alnitak.Exceptions;

namespace Alnitak {

	public class CultureModule : LanguageModule {
		
		/// <summary>Retorna o locale a ser usado num pedido</summary>
		public override string getRequestLanguage()
		{
			return RequestLanguage;
		}
		
		/// <summary>Retorna o locale a ser usado num pedido</summary>
		public static string RequestLanguage{
			get {
				User user = null;
				if( HttpContext.Current.User is User ) {
					user = (User) HttpContext.Current.User;
					return user.Lang;
				}
				if( null == HttpContext.Current.Request.UserLanguages ) {
					return "en";
				}
				return HttpContext.Current.Request.UserLanguages[0].Substring(0,2);
			}
		}
		
		/// <summary>Retorna o locale por defeito</summary>
		/// <remarks>Procura no Web.config, se não encontrar usa LanguageModule.DEFAULT_LOCALE</remarks>
		public override string getLocale()
		{
			return getValue("default-locale");
		}
	
		/// <summary>Retorna o dir por defeito</summary>
		/// <remarks>Procura no Web.config, se não encontrar usa LanguageModule.DEFAULT_DIR</remarks>
		public override string getDir()
		{
			return getValue("default-lang-dir");
		}
		
		/// <summary>retorna o valor associado a uma key no OrionGroup/language</summary>
		private string getValue( string key )
		{
			return OrionGlobals.getConfigurationValue("language",key);
		}
		
		/// <summary>Retorna o ResourceInfo adquado</summary>
		/// <remarks>
		///	Para saber a cultura, ele vai ao CurrentUICulture.
		/// </remarks>
		public static ILanguageInfo getLanguage()
		{
			string locale = RequestLanguage;
			return getLanguage(locale);
		}

		/// <summary>Retorna o ResourceInfo adquado</summary>
		public static ILanguageInfo getLanguage( string locale )
		{	
			LanguageManager man = (LanguageManager) HttpContext.Current.Application["LanguageManager"];
			if( man == null ) {
				throw new AlnitakException("LanguageManager não foi encontrado em 'Application'");
			}
			
			return man.getLanguageInfo( locale );
		}

		/// <summary>Retorna uma string localizada</summary>
		public static string getContent( string key )
		{
			return getLanguage().getContent(key);
		}
		
		/// <summary>Retorna uma string localizada</summary>
		public static string getContent( string key, bool t )
		{
			return getLanguage().getContent(key, t);
		}
	}

}
