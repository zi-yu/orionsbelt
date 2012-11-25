
using System;
using System.Collections;
using System.IO;

namespace Language {

	public class Validator {
	
		/// <summary>Verifica as diferenas entre dois LanguageInfos</summary>
		public static int Main( string[] args )
		{
			if( args == null || args.Length != 1 ) {
				Console.WriteLine("usage: LanguageValidator <root-dir>");
				return 1;
			}
			try {
				Console.WriteLine("LanguageValidator");
				Console.WriteLine("Checking up '{0}'...", args[0]);
				string[] dirs = Directory.GetDirectories(args[0]);
				
				Hashtable hash = new Hashtable();
				bool check = true;
				int count = -1;
				
				foreach( string dir in dirs ) {
					if( dir.ToLower().IndexOf("cvs") != -1 ) {
						continue;
					}
					
					DateTime start = DateTime.Now;
					LanguageInfo info = new LanguageInfo(dir);
					DateTime end = DateTime.Now;
					string baseDir = ParseDir(dir);
					
					hash.Add(baseDir, info);
					
					Console.WriteLine("  {0}: {1} entries parsed in {2}", baseDir, info.Root.Count, end-start);
					
					if( count == -1 ) {
						count = info.Root.Count;
					} else {
						if( check ) {
							check = count == info.Root.Count;
						}
					}
				}
				
				Console.WriteLine();
				if( !check ) {
					Console.WriteLine("*** Diferences detected!");
					WriteDiferences(hash);
					return 1;
				}
				
				Console.WriteLine("No errors found!");
				Console.WriteLine();
				return 0;
				
			} catch( Exception ex ) {
				Console.WriteLine(ex);
				return 1;
			}
		}
		
		/// <summary>Indica o locale do dir</summary>
		public static string ParseDir(string dir)
		{
			int idx = dir.LastIndexOf(Path.DirectorySeparatorChar);
			if( idx == -1 ) {
				return dir;
			}
			return dir.Substring(idx + 1, dir.Length - 1 - idx);
		}
		
		/// <summary>Mostra diferenas</summary>
		public static void WriteDiferences( Hashtable hash )
		{
			IDictionaryEnumerator it = hash.GetEnumerator();
			while( it.MoveNext() ) {
				LanguageInfo info = (LanguageInfo) it.Value;
				Console.Write("***** Checking '{0}' ", it.Key);
				
				IDictionaryEnumerator other = hash.GetEnumerator();
				while( other.MoveNext() ) {
					LanguageInfo otherInfo = (LanguageInfo) other.Value;
					if( info == otherInfo ) {
						continue;
					}
					
					Console.WriteLine("against '{0}'...", other.Key);
					
					foreach( string key in info.Root.Keys ) {
						if( !otherInfo.Root.ContainsKey(key) ) {
							Console.WriteLine("******** Key '{0}' in '{1}' not found in '{2}'", key, it.Key, other.Key);
						}
					}
				}
			}
		}
	};

}
