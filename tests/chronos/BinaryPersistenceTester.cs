// created on 8/4/2004 at 9:40 AM

using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Core;
using Chronos.Utils;
using Chronos;
using Chronos.Persistence;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	public class BinaryPersistenceTester : PersistenceTests {
	
		#region Static Fields
	
		private static FilePersistence persistence = new FilePersistence("UniverseTests.bin");

		#endregion
	
		#region Set Up
	
		[TestFixtureSetUp]
		public void init()
		{
			init(persistence);
		}
		
		#endregion
	
	};
}
