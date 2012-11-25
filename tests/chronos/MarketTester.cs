// created on 8/12/2005 at 2:35 PM

//#define DEBUG_RARE_RESOURCES
//#define DEBUG_MARKET

using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Core;
using Chronos;
using Chronos.Trade;
using Chronos.Utils;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	public class MarketTester {
		
		#region Instance Fields
		
		private Planet planet;
		private Ruler ruler;
		
		#endregion
		
		#region Set Up Methods
		
		[SetUp]
		public void prepare()
		{
			ruler = new Ruler(null, Universe.factories,"BuuBuu");
			planet = new Planet( ruler, Universe.factories, "Buu", Coordinate.First );
		}
		
		#endregion
		
		#region Market Information
		
		[Test]
		public void TestMarketRatio()
		{
			int ratio = 0;
			
			ratio = Market.MarketRatio(planet);
			Assert.AreEqual( ratio, 0, "#1");
			
			planet.addResource("Intrinsic", "housing", 50000);
			planet.addResource("Intrinsic", "labor", 50000);
			planet.addResource("Intrinsic", "mp", 50000);
			planet.addResource("Intrinsic", "gold", 50000);
			Globals.ToStockMarckets(ruler, planet);
			
			MarketRatioAux("#1", 0);
			MarketRatioAux("#2", 0);
			
			MarketRatioAux("#3", 1);
			MarketRatioAux("#4", 1);
			MarketRatioAux("#5", 1);
			
			MarketRatioAux("#6", 2);
			MarketRatioAux("#7", 2);
			MarketRatioAux("#8", 2);
			
			MarketRatioAux("#9", 3);
			MarketRatioAux("#10", 3);
			MarketRatioAux("#11", 3);
			
			MarketRatioAux("#12", 3);
			MarketRatioAux("#13", 3);
			MarketRatioAux("#14", 3);
		}
		
		private void BuildMarket()
		{
			planet.addResource("Intrinsic", "mp", 50000);
			planet.addResource("Intrinsic", "gold", 50000);
			planet.addResource("Intrinsic", "housing", 50000);
			planet.addResource("Intrinsic", "labor", 50000);
			planet.Modifiers["food"] = 1000; 
			Globals.Build(planet, "Building", "Marketplace", 1);
		}
		
		private void MarketRatioAux( string label, int expected )
		{
			BuildMarket();
			Assert.AreEqual( Market.MarketRatio(planet), expected, label + " -> " + planet.getResourceCount("Building", "Marketplace"));
		}
		
		[Test]
		public void ShowRareResourcesGenerated()
		{
#if DEBUG_MARKET
			Log.log("--- MARKET_DEBUG ----");
			string last = null;
			for( int i = 0; i < 100; ++i ) {
				MarketItem curr = Market.GetRandomRare(planet, i, planet.Info.Id+30);
				if( curr.Resource.Name != last ) {
					last = curr.Resource.Name;
					Log.log("i:{0} item:{1}", i, curr.Resource.Name);
				}
			}
#endif
		}
		
		[Test]
		public void TestRandomRare()
		{
			int info = 0;
			
			MarketItem rare = Market.GetRandomRare(planet, 0, info);
			MarketItem rare2 = Market.GetRandomRare(planet, 1, info);
			Assert.AreEqual( rare.Resource.Name, rare2.Resource.Name, "#1" );
			
			MarketItem rare3 = Market.GetRandomRare(planet, 2, info);
			Assert.AreEqual( rare3.Resource.Name, rare2.Resource.Name, "#2" );
			
			MarketItem rare4 = Market.GetRandomRare(planet, 3, info);
			Assert.AreEqual( rare3.Resource.Name, rare4.Resource.Name, "#3" );
			
			MarketItem rare5 = Market.GetRandomRare(planet, 4, info);
			Assert.AreEqual( rare5.Resource.Name, rare4.Resource.Name, "#4" );
			
			MarketItem rare6 = Market.GetRandomRare(planet, 5, info);
			Assert.AreEqual( rare5.Resource.Name, rare6.Resource.Name, "#5" );
			
			MarketItem rare7 = Market.GetRandomRare(planet, 6, info);
			Assert.AreEqual( rare7.Resource.Name, rare6.Resource.Name, "#6" );
			
			MarketItem rare8 = Market.GetRandomRare(planet, 7, info);
			Assert.AreEqual( rare7.Resource.Name, rare8.Resource.Name, "#7" );
			
			MarketItem rare9 = Market.GetRandomRare(planet, 8, info);
			Assert.AreEqual( rare9.Resource.Name, rare8.Resource.Name, "#8" );
			
			MarketItem rare10 = Market.GetRandomRare(planet, 9, info);
			Assert.IsTrue( rare10.Resource.Name != rare9.Resource.Name, "#8" );
		}
		
		[Test]
		public void TestBuy()
		{
			Result result = null;
			
			result = Market.Buy(planet, "asdasdasdas", 231);
			Assert.IsFalse( result.Ok, result.log() );
		}
		
		[Test]
		public void TestSell()
		{
			Result result = null;
			
			result = Market.Sell(planet, "asdasdasdas", 231);
			Assert.IsFalse( result.Ok, result.log());
		}
		
		#endregion
		
		#region Market Operations
		
		[Test]
		public void TestLackOfMarket()
		{
			Result result = new Result();
			
			MarketItem item = Market.ToBuy(planet)[0];
			result = Market.Buy(planet, item.Resource.Name, 1);
			Assert.IsFalse(result.Ok, result.log());
			
			item = Market.ToSell(planet)[0];
			result = Market.Sell(planet, item.Resource.Name, 1);
			Assert.IsFalse(result.Ok, result.log());
		}
		
		[Test]
		public void TestMarketShare()
		{
			Globals.ToStockMarckets(ruler, planet);
			
			for( int i = 0; i < 10; ++i ) {
				BuildMarket();
				foreach( Resource resource in planet.getResourceInfo("Intrinsic").Resources.Keys ) {
					if( !resource.MarketResource ) {
						continue;
					}
					
					int sellPrice = Market.GetSellPrice(resource, planet);
					int buyPrice = Market.GetBuyPrice(resource, planet);
					
					Assert.IsTrue( sellPrice < buyPrice , string.Format("SellPrice >= BuyPrice! MarketRatio:{0} BuyPrice:{1} SellPrice:{2}", Market.MarketRatio(planet), buyPrice, sellPrice));
				}
			}
		}
		
		#endregion
	
	};

}
