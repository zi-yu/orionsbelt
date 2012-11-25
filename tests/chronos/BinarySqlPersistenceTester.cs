// created on 8/4/2004 at 9:40 AM

using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Core;
using Chronos.Utils;
using Chronos;
using Chronos.Persistence.SqlServer;
using Chronos.Persistence;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	public class BinarySqlPersistenceTester : PersistenceTests {
	
		#region Static Fields
	
		private static BinarySqlPersistence persistence = new BinarySqlPersistence(Globals.QueryString);

		#endregion
	
		#region Set Up
	
		[TestFixtureSetUp]
		[Ignore("Ignore a fixture")]
		public void init() {
			init(persistence);
		}
		
		#endregion
	
	};
}
