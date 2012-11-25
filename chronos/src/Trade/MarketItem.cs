// created on 8/11/2005 at 11:30 AM

using System;
using Chronos.Resources;

namespace Chronos.Trade {

	public class MarketItem {
	
		#region Instance Fields
		
		private Resource resource;
		private int price;
		private int available;
		
		#endregion
		
		#region Ctor
		
		public MarketItem( Resource _resource, int _price, int _available )
		{
			resource = _resource;
			price = _price;
			available = _available;
		}
		
		public MarketItem( Resource _resource, int _price )
		{
			resource = _resource;
			price = _price;
			available = -1;
		}
		
		#endregion
		
		#region Instance Properties
		
		public Resource Resource {
			get { return resource; }
		}
		
		public int Price {
			get { return price; }
		}
		
		public int Available {
			get { return available; }
		}
		
		#endregion
	
	};

}