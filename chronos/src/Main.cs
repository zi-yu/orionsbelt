// project created on 2/23/04 at 8:50 a

using System;
using System.IO;
using System.Xml;
using System.Collections;
using Chronos.Exceptions;
using Chronos.Alliances;
using Chronos.Core;
using Chronos.Resources;
using System.Configuration;

namespace Chronos {

	class MainClass {
	
		public static int Main(string[] args)
		{
			Console.WriteLine("Chronos XML Validator");
			Console.WriteLine("Running from: " + Directory.GetCurrentDirectory() );
			Console.WriteLine();
			try {
				DateTime start = DateTime.Now;
				Universe.instance.init();
				DateTime end = DateTime.Now;
				Console.WriteLine("No errors found. Universe built in {0}", end-start);
				Console.WriteLine();
				WriteStats();
				Console.WriteLine();
				
				if( PrintShipComp(args) ) {
					PrintShipComp();
				}
				
				return 0;
			} catch( Exception ex ) {
				Console.WriteLine(ex.ToString());
				return 1;
			}
		}
		
		public static void PrintShipComp()
		{
			ResourceFactory lightship = Universe.getFactory("planet", "Unit", "LightShip");
			int attackBase = (int) lightship.Attributes["attack"];
			int defenseBase = (int) lightship.Attributes["defense"];
			
			Console.WriteLine("Ship\t\t: #\t: Dur\t: Quant");
			Console.WriteLine("----\t\t: -\t: ---\t: -----");
			
			ResourceBuilder ships = Universe.getFactories("planet", "Unit");
			foreach( ResourceFactory factory in ships.Values ) {
				int specificAttack = (int) factory.Attributes["attack"];
				int specificDefense = (int) factory.Attributes["defense"];
				int cmp = (specificAttack / attackBase + specificDefense / defenseBase ) / 2;
				
				int durationValue = cmp;
				
				Console.WriteLine("{0}\t: {1}\t: {2}\t: 100",
								  factory.Name, cmp, durationValue
						);
			}
			Console.WriteLine();
		}
		
		public static bool PrintShipComp( string[] args )
		{
			if( args == null || args.Length == 0 ) {
				return false;
			}
			foreach( string str in args ) {
				if( str == "--ships" ) {
					return true;
				}
			}
			return false;
		}

		public static void WriteStats()
		{
			Hashtable root = (Hashtable) Universe.factories["ruler"];
			IDictionaryEnumerator it = root.GetEnumerator();
			Console.WriteLine("Ruler Resources Count:");
			while( it.MoveNext() ) {
				Console.WriteLine("   {0}: {1}", it.Key, ((ResourceBuilder)it.Value).Count);
			}
			
			root = (Hashtable) Universe.factories["planet"];
			it = root.GetEnumerator();
			Console.WriteLine("Planet Resources Count:");
			while( it.MoveNext() ) {
				Console.WriteLine("   {0}: {1}", it.Key, ((ResourceBuilder)it.Value).Count);
			}
		}
		
	};
}
