using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Alliances;
using Chronos;
using Chronos.Queue;
using Chronos.Persistence;
using NUnit.Framework;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chronos.Tests {

	[TestFixture]
	public class BinaryFormatterTester {
		
		[Test]
		public void testBinaryFormatter()
		{
			Universe universe = new Universe();
			universe.init();
			FilePersistence serializer = new FilePersistence("UniverseBinaryFormatter.bin");
			serializer.Formatter = new BinaryFormatter();

			Ruler ruler = new Ruler("PRE");
			universe.rulers.Add(ruler.Id, ruler);

			ruler.Resources.Remove("Research");
			
			serializer.save(universe, new PersistenceParameters());
			serializer.load(new PersistenceParameters());
		}
	};
}
	
