// created on 6/15/2004 at 6:14 PM

using System;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace Chronos.Utils {

	public class Log {
		
		/// <summary>
		/// Method log
		/// </summary>
		public static void log()
		{
			// TODO
		}
	
		#region Utility Methods
		
		/// <summary>Faz log de uma string</summary>
		public static void log( object src, EventArgs args )
		{
			log(args);
		}
		
		/// <summary>Faz log de uma string</summary>
		public static void log( object obj )
		{
#if DEBUG
			System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(true);

			string info = trace.GetFrame(1).GetMethod().DeclaringType.Name + '.' + trace.GetFrame(1).GetMethod().Name;
			info += ":" + trace.GetFrame(1).GetFileLineNumber().ToString();

			Console.WriteLine(info + "# " + obj.ToString());
			Debug.WriteLine( info + "# " + obj.ToString() );
#endif
		}

		/// <summary>Faz log de uma string</summary>
		public static void log( object obj, params object[] args )
		{
#if DEBUG
			System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(true);

			string info = trace.GetFrame(1).GetMethod().DeclaringType.Name + '.' + trace.GetFrame(1).GetMethod().Name;
			info += ":" + trace.GetFrame(1).GetFileLineNumber().ToString();

			Console.WriteLine(info + "# " + obj.ToString(), args);
			Debug.WriteLine( String.Format( info + "# " + obj.ToString(), args ));
#endif
		}
		
		/// <summary>Faz log de uma sorted list</summary>
		public static void log( SortedList hash )
		{
#if DEBUG
			foreach( object obj in  hash.Keys ) {
				log(obj.ToString() + " - " + hash[obj].ToString());
			}
#endif
		}
		
		/// <summary>Faz log de uma hash</summary>
		public static void log( Hashtable hash )
		{
#if DEBUG
			foreach( object obj in  hash.Keys ) {
				log(obj.ToString() + " - " + hash[obj].ToString());
			}
#endif
		}
		
		/// <summary>Faz log de um StringDictionary</summary>
		public static void log( StringDictionary hash )
		{
#if DEBUG
			foreach( string obj in  hash.Keys ) {
				log(obj.ToString() + " - " + hash[obj].ToString());
			}
#endif
		}
		
		/// <summary>Retorna o StackTrace</summary>
		public static string stackTrace()
		{
			StringWriter writer = new StringWriter();
			System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);

			for(int i = 0; i < st.FrameCount; i ++) {
				StackFrame sf = st.GetFrame(i);
				string info = sf.GetMethod().DeclaringType.Name + '.' + sf.GetMethod().Name;
				info += ":" + sf.GetFileLineNumber().ToString();
				writer.WriteLine(info);
			}
			
			return writer.ToString();
		}
		
		/// <summary>Indica se o Chronos foi compilado em Debug Mode</summary>
		public static bool IsDebugMode {
			get {
#if DEBUG
				return true;
#else
				return false;
#endif
			}
		}
	
		#endregion
	
	};

}
