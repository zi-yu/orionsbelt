// created on 8/11/2005 at 11:28 AM

using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Core;
using Chronos.Utils;

namespace Chronos.Trade {

	public class Market {
	
		#region Market Operation Methods
		
		public static Result Sell( Planet planet, string resource, int quantity )
		{
			Result result = new Result();
			
			if( planet.getResourceCount("Building", "Marketplace") == 0 ) {
				result.failed( new ResourceNotAvailable("Building", "Marketplace") );
				return result;
			}
		
			MarketItem[] items = ToSell(planet);
			MarketItem toProcess = null;
			foreach( MarketItem item in items ) {
				if( item.Resource.Name == resource ) {
					toProcess = item;
				}
			}
			
			if( toProcess == null ) {
				result.failed( new ResourceNotAvailable(toProcess.Resource.Factory.Category, resource) );
				return result;
			}
			
			int available = planet.getResourceCount(toProcess.Resource.Factory.Category, resource);
			if( quantity > available ) {
				result.failed( new ResourceQuantityNotAvailable(resource) );
				return result;
			}
			
			planet.take( toProcess.Resource.Factory.Category, resource, quantity );
			planet.addResource( "Intrinsic", "gold", quantity * toProcess.Price );
			
			result.passed( new OperationSucceded() );
			return result;
		}
		
		public static Result Buy( Planet planet, string resource, int quantity )
		{
			Result result = new Result();
			
			if( planet.getResourceCount("Building", "Marketplace") == 0 ) {
				result.failed( new ResourceNotAvailable("Building", "Marketplace") );
				return result;
			}
		
			MarketItem[] items = ToBuy(planet);
			MarketItem toProcess = null;
			foreach( MarketItem item in items ) {
				if( item.Resource.Name == resource ) {
					toProcess = item;
				}
			}
			
			if( toProcess == null ) {
				result.failed( new ResourceNotAvailable(toProcess.Resource.Factory.Category, resource) );
				return result;
			}
			
			int gold = planet.Gold;
			int cost = toProcess.Price * quantity;

			if( cost > gold ) {
				result.failed( new ResourceQuantityNotAvailable("gold") );
				return result;
			}
			
			planet.take( "Intrinsic", "gold", cost );
			planet.addResource( toProcess.Resource.Factory.Category, resource, quantity );
			
			result.passed( new OperationSucceded() );
			return result;
		}
		
		#endregion
	
		#region Market Information Methods
		
		public static MarketItem[] ToBuy( Planet planet )
		{
			ArrayList list = new ArrayList();
			AddRandomRare(list, planet);
			foreach( Resource resource in planet.getResources("Intrinsic").Keys ) {
				if( resource.MarketResource ) {
					list.Add( new MarketItem(resource, GetBuyPrice(resource, planet)) );
				}
			}
			return (MarketItem[]) list.ToArray(typeof(MarketItem));
		}
		
		public static MarketItem[] ToSell( Planet planet )
		{
			ArrayList list = new ArrayList();
			foreach( Resource resource in planet.getResources("Intrinsic").Keys ) {
				if( resource.MarketResource && planet.getResourceCount(resource.Factory.Category, resource.Name) > 0 ) {
					list.Add( new MarketItem(resource, GetSellPrice(resource, planet), planet.getResourceCount("Intrinsic", resource.Name)) );
				}
			}
			foreach( Resource resource in planet.getResources("Rare").Keys ) {
				if(  planet.getResourceCount(resource.Factory.Category, resource.Name) > 0 ) { 
					list.Add( new MarketItem(resource, GetSellPrice(resource, planet), planet.getResourceCount("Rare", resource.Name)) );
				}
			}
			return (MarketItem[]) list.ToArray(typeof(MarketItem));
		}
		
		public static void AddRandomRare( ArrayList list, Planet planet )
		{
			MarketItem rare = GetRandomRare( planet, Universe.instance.TurnCount, planet.Info.Id);
			list.Add(rare);
		}
		
		public static MarketItem GetRandomRare( Planet planet, int turn, int info )
		{
			int seed = turn + info;
			ResourceBuilder resources = Universe.getFactories("planet", "Rare");
			int idx = (seed % (resources.Count*resources.Count) )/ resources.Count;
			
			ResourceFactory factory = null;
			int i = 0;
			foreach( ResourceFactory it in resources.Values ) {
				if( i++ == idx ) {
					factory = it; 
				}
			}
			Resource resource = factory.create();
			
			return new MarketItem(resource, GetBuyPrice(resource, planet) );
		}
		
		public static int GetBuyPrice( Resource resource, Planet planet )
		{
			if( resource.Rare ) {
				return 2500;
			}
			return resource.Factory.GetAtt("TeletransportationCost") * (10 - MarketRatio(planet));
		}
		
		public static int GetSellPrice( Resource resource, Planet planet )
		{
			if( resource.Rare ) {
				return 50;
			}
			if( resource.Name == "food" ) {
				return 1;
			}
			return resource.Factory.GetAtt("TeletransportationCost") * (2 + MarketRatio(planet));
		}
		
		public static int MarketRatio( Planet planet )
		{
			int markets = planet.getResourceCount("Building", "Marketplace");
		
			int ratio = markets / 3;
			if( ratio > 3 ) {
				ratio = 3;
			}
			return ratio;
		}
		
		#endregion
	
	};

}