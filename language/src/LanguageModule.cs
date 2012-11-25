// created on 3/21/04 at 12:46 a

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Configuration;
using System.Globalization;

namespace Language {

	public class LanguageModule : IHttpModule {
	
		public static readonly string DEFAULT_LOCALE = "pt";
		public static readonly string DEFAULT_DIR = "lang";
		private static string[] langs;

		public void Dispose()
		{
		}
		
		/// <summary>Coloca m LanguageManager no Application</summary>
		public void Init( HttpApplication context )
		{	
			LanguageManager man = new LanguageManager(context.Context.Cache, getDir(), getLocale());
			langs = parse(man.Languages);
				
			context.Application["LanguageManager"] = man;
			context.PreRequestHandlerExecute += new EventHandler(this.beginRequest);
		}
		
		/// <summary>Retorna um array só com a última dir de um path</summary>
		private string[] parse( string[] dirs )
		{
			string[] output = new string[dirs.Length];
			for( int i = 0; i < dirs.Length; ++i ) {
				int idx = dirs[i].LastIndexOf( Path.DirectorySeparatorChar );
				if( idx < 0 ) {
					output[i] = dirs[i];
				} else {
					output[i] = dirs[i].Substring(idx + 1);
				}
			}
			return output;
		}
		
		/// <summary>Retorna todas as culturas suportadas</summary>
		public static string[] Languages {
			get {
				return langs;
			}
		}
		
		/// <summary>Prepara CurrentThread.CurrentUICulture com a cultura do pedido</summary>
		private void beginRequest( object src, EventArgs args )
		{
			string requestString = getRequestLanguage();
			if( requestString.IndexOf(';') != -1 ) {
				requestString = DEFAULT_LOCALE;
			}

			CultureInfo culture = CultureInfo.CreateSpecificCulture( requestString );
			System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
		}
		
		/// <summary>Retorna o locale a ser usado num pedido</summary>
		public virtual string getRequestLanguage()
		{
			return HttpContext.Current.Request.UserLanguages[0];
			//return getLocale();
		}
		
		/// <summary>Retorna o locale por defeito</summary>
		/// <remarks>Procura no Web.config, se não encontrar usa LanguageModule.DEFAULT_LOCALE</remarks>
		public virtual string getLocale()
		{
			string def = ConfigurationSettings.AppSettings["default-locale"];
			if( def != null ) {
				return def;
			}
			return DEFAULT_LOCALE;
		}
	
		/// <summary>Retorna o dir por defeito</summary>
		/// <remarks>Procura no Web.config, se não encontrar usa LanguageModule.DEFAULT_DIR</remarks>
		public virtual string getDir()
		{
			string def = ConfigurationSettings.AppSettings["default-lang-dir"];
			if( def != null ) {
				return def;
			}
			return DEFAULT_DIR;
		}
	};

}
