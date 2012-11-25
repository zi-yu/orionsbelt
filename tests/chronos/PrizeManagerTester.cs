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
using Chronos.Info;
using NUnit.Framework;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chronos.Tests {
	
	[TestFixture]
	public class PrizeManagerTester {
		
		#region Setup
		
		private Planet planet;
		private Ruler dumb;
		private Ruler ruler;
		
		[SetUp]
		public void init()
		{
			ruler = new Ruler("PRE");
			planet = new Planet(ruler, Globals.factories, "Planet", Coordinate.First);
			
			dumb = new Ruler("PRE");
			new Planet(dumb, Globals.factories, "Planet", Coordinate.First);
			
		}
		
		#endregion
		
		#region Tests
		
		[Test]
		public void TestManaget()
		{
			PrizeManager manager = new PrizeManager("SamplePrize");
			Assert.AreEqual(false, PrizeManager.alreadyWinner(ruler, "SamplePrize"), "No prizes yet");
			manager.register(PrizeCategory.Conquer, 0, ruler);
			Assert.AreEqual(true, PrizeManager.alreadyWinner(ruler, "SamplePrize"), "First Prize");
			
			Assert.AreEqual(false, PrizeManager.alreadyWinner(dumb, "SamplePrize"), "no Silver medal");
			manager.register(PrizeCategory.Conquer, 1, dumb);
			Assert.AreEqual(true, PrizeManager.alreadyWinner(dumb, "SamplePrize"), "Silver medal");
			
			Ruler bronze = new Ruler("r");
			Assert.AreEqual(true, manager.register(PrizeCategory.Conquer,  3, bronze));
			Assert.AreEqual(true, PrizeManager.alreadyWinner(bronze, "SamplePrize"), "bronze");
			
			Ruler plastic = new Ruler("t");
			Assert.AreEqual(false, manager.register(PrizeCategory.Conquer, 1, plastic), "plastic 1 res");
			Assert.AreEqual(false, PrizeManager.alreadyWinner(plastic, "SamplePrize"), "plastic 1");
			
			Ruler plastic2 = new Ruler("t");
			Assert.AreEqual(false, manager.register(PrizeCategory.Conquer,  5 +2, plastic2), "plastic 2 res");
			Assert.AreEqual(false, PrizeManager.alreadyWinner(plastic2, "SamplePrize"), "plastic 2");
		}
		
		#endregion
		
		#region Bugs
		
		[Test]
		public void TestAllResearchsPrize_Bug_1187588()
		{
			/*ResourceInfo info = ruler.getResourceInfo("Research");
			Result result = ruler.canQueue("Research", "Cybernetics", 1);
			Assert.IsTrue(result.Ok, result.log());
			ruler.queue("Research", "Cybernetics", 1);
			
			info.AvailableFactories.Clear();
			info.UnavailableFactories.Clear();
			
			Assert.AreEqual(0, ruler.Prizes.Count, "#1 Ruler cannot have research prize");
			ruler.turn();
			Assert.AreEqual(0, ruler.Prizes.Count, "#2 Ruler cannot have research prize");
			
			while( info.QueueCount != 0 || info.Current != null ) {
				ruler.turn();
			}
			
			Assert.AreEqual(1, ruler.Prizes.Count, "#3 Ruler must have research prize");*/
		}
		
		#endregion
		
	};
}

