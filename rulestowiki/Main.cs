// project created on 8/1/2005 at 11:36 AM
using System;

namespace RulesToWiki {

	public class Generator	{
	
		public static void Main(string[] args)
		{
			try {
			
			Docer docer = new Docer( args[0], args[1], args[2] );
			docer.Generate();
			
			Console.WriteLine("Language : {0}", args[0]);
			Console.WriteLine("Rules    : {0}", args[1]);
			Console.WriteLine("Output   : {0}", args[2]);
			
			} catch ( Exception ex ) {
				Console.WriteLine("Usage: <lang-dir> <rules-dir> <target-dir>");
				Console.WriteLine(ex);
			}
		}
	};
	
}