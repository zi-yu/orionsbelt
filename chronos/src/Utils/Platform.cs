// created on 5/22/04 at 9:03 a

using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using Chronos.Exceptions;

namespace Chronos.Utils {

	/// <summary>Informaes relativas  plataforma currente</summary>
	public class Platform {
	
		#region Const Fields
		
		private const string defaultConfDir = "conf";
		
		#endregion
		
		#region Static Fields
		
		private static string baseDir;
		private static string resourceDir;
		private static string generalDir;
		
		#endregion
		
		#region Static Ctors
		
		/// <summary>Construtor estático</summary>
		static Platform()
		{
			string location = parseUrl( ChronosAssembly.CodeBase );
			
			baseDir = codeBaseToPath( location );
			
			resourceDir = ConfigDir + System.IO.Path.DirectorySeparatorChar + "resources" + System.IO.Path.DirectorySeparatorChar;
			generalDir = ConfigDir + System.IO.Path.DirectorySeparatorChar + "general" + System.IO.Path.DirectorySeparatorChar;
		}
		
		#endregion

		#region Static Properties
		
		/// <summary>Retorna o Assembly do Chronos</summary>
		public static Assembly ChronosAssembly {
			get {
				return typeof(Platform).Assembly;
			}
		}

		/// <summary>Indica se a Plataforma currente é o Mono</summary>
		public static bool Mono {
			get {
				return Type.GetType("Mono.Runtime", false) != null;
			}
		}
		
		/// <summary>Indica que a plataforma currente é outra que não o Mono (MS .NET)</summary>
		public static bool MsDotNet {
			get {
				return !Mono;
			}
		}
		
		/// <summary>Indica se o Path é do tipo Windows</summary>
		public static bool WinPath {
			get {
				return Path.DirectorySeparatorChar == '\\';
			}
		}
		
		/// <summary>Indica se o Path é do tipo unix</summary>
		public static bool UnixPath {
			get {
				return Path.DirectorySeparatorChar == '/';
			}
		}
		
		/// <summary>Indica o dir base da aplicação. Nomedamente a dir abaixo do bin</summary>
		public static string BaseDir {
			get {
				return baseDir;
			}
		}
		
		/// <summary>Retorna a dir de configuração dos recursos</summary>
		public static string ConfigDir {
			get {
				NameValueCollection nvc = (NameValueCollection)
					ConfigurationSettings.GetConfig("OrionGroup/chronos");
				if( nvc == null ) {
					// ou há falha no ficheiro de configuração
					// ou estamos a correr, por exemplo, pelo NUnit
					return BaseDir + Path.DirectorySeparatorChar + defaultConfDir;
				}
						
				string confDir = nvc["confDir"];
				if( confDir == null ) {
					throw new RuntimeException("Key 'confDir' not found in config file");
				}
				
				return BaseDir + Path.DirectorySeparatorChar + confDir;
			}
		}
		
		/// <summary>Retorna a dir de configuração dos recursos</summary>
		public static string ResourceConfigDir {
			get {
				return resourceDir;
			}
		}
	
		/// <summary>Retorna a dir de configuração geral</summary>
		public static string GeneralConfigDir {
			get {
				return generalDir;
			}
		}
		
		/// <summary>Indica uma string que descreve o Chronos</summary>
		public static string ChronosInfo {
			get {
				return chronosInfo;
			}
		}
		
		#endregion
		
		#region Functional Methods
		
		/// <summary>Retira o inicio do CodeBase</summary>
		public static string trimCodeBase( string codeBase )
		{
			string start = "file://";
			if( codeBase.StartsWith(start) ) {
				int lenght = start.Length;
				if( WinPath ) {
					++lenght;
				}
				codeBase = codeBase.Remove(0, lenght );
			}
			return codeBase;
		}
		
		/// <summary>Retorna uma string sem o último elemento de um path</summary>
		public static string parseUrl( string dir )
		{
			int idx = dir.LastIndexOf( '/' );
			if( idx < 0 ) {
				throw new LoaderException("Can't isolate dir from " + dir);
			}
			return dir.Substring(0, idx);
		}
		
		/// <summary>Converte um URL para um path</summary>
		public static string urlToPath( string url )
		{
			if( WinPath ) {
				return url.Replace('/', '\\');
			}
			return url;
		}
		
		/// <summary>Trnasforma uma string de CodeBase em path</summary>
		public static string codeBaseToPath( string codeBase )
		{
			return urlToPath( parseUrl( trimCodeBase(codeBase) ) );
		}
		
		/// <summary>Indica informa��es sobre a vers�o de um Assembly</summary>
		public static string GetInfo( Assembly asm )
		{
			object[] attributes = asm.GetCustomAttributes(true);
			foreach (object attribute in attributes) {
				if (attribute is System.Reflection.AssemblyInformationalVersionAttribute)
					return ((System.Reflection.AssemblyInformationalVersionAttribute)attribute).InformationalVersion;
			}
			return "";
		}
		
		private static string chronosInfo = string.Format("Chronos {1} Version: {0}",
														  ChronosAssembly.GetName().Version,
														  GetInfo(ChronosAssembly)
														  );
		
		#endregion
	
	};

}
