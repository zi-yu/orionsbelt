// created on 8/11/2005 at 11:00 AM

using System.Collections;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Messaging;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Utils;
using Chronos.Trade;
using System;
using System.Text.RegularExpressions;

namespace Alnitak {

	public class Market : PlanetControl {
	
		#region Control Fields
		
		protected ListBox options;
		protected ListBox resources;
		protected MarketItemList buyTable;
		protected MarketItemList sellTable;
		protected Label planetMarkets;
		protected Label planetMoney;
		protected Button operate;
		protected TextBox quantity;
		protected QueueErrorReport operationReport;
		protected PlanetNavigation planetNavigation;
		protected HyperLink marketHelp;
		
		#endregion
	
		#region Control Events
		
		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
	
			FillOptionsList();
			FillResourcesList();
			
			string sectionMarket = info.getContent("section_market");
			OrionGlobals.RegisterRequest(MessageType.PlanetManagement, string.Format("{0} - {1}",getPlanet().Name, sectionMarket));
			
			Page.RegisterHiddenField("currentOperation", "");
			Page.RegisterHiddenField("resource", "");
			
			operationReport.Title = sectionMarket;
			
			marketHelp.NavigateUrl = Wiki.GetUrl("Mercados");
			marketHelp.Text = info.getContent("wiki_Market");
		}
		
		protected void Operate( object sender, EventArgs args )
		{
			Planet planet = getPlanet();
			Result result = null;
			try {
			
				string operation = GetVar("currentOperation");
				string resource = GetVar("resource");
				string quantityToOperate = quantity.Text;
				quantity.Text = "";
				
				switch(operation) {
					case "sell": 
						result = Chronos.Trade.Market.Sell(planet, resource, int.Parse(quantityToOperate)); 
						break;
					case "buy": 
						result = Chronos.Trade.Market.Buy(planet, resource, int.Parse(quantityToOperate)); 
						break;
					default:
						throw new Exception();
				}
				
			} catch {
				result = new Result();
				result.failed( new InvalidOperation() ); 
			} finally {
				operationReport.ResultSet = result;
			}			
		}
		
		#endregion
		
		#region Utilities
		
		private string GetVar( string name )
		{
			string var = Page.Request.Form[name];
			if( var == null || var == string.Empty ) {
				throw new Exception("No '"+name+"' found");
			}
			return var;
		}
		
		private void FillOptionsList()
		{
			options.Items.Clear();
			options.Items.Add( new ListItem(info.getContent("buy"), "buy") );
			options.Items.Add( new ListItem(info.getContent("sell"), "sell") );
		}
		
		private void FillResourcesList()
		{
			resources.Items.Clear();
		}
		
		protected string MarketInformation()
		{
			Planet planet = getPlanet();
			
			MarketItem[] buy = Chronos.Trade.Market.ToBuy(planet);
			MarketItem[] sell = Chronos.Trade.Market.ToSell(planet);
			
			sellTable.Items = sell;
			buyTable.Items = buy;
			planetMoney.Text = planet.Gold.ToString();
			planetMarkets.Text = planet.getResourceCount("Building", "Marketplace").ToString();
			operate.Text = CultureModule.getContent("send");
			
			planetNavigation.Player = getRuler();
			planetNavigation.Current = planet;
			planetNavigation.Section = "Market";
			
			StringWriter writer = new StringWriter();
			WriteMarketItems(writer, buy, "buy", planet);
			WriteMarketItems(writer, sell, "sell", planet);
			WriteCaptions(writer);
			return writer.ToString();
		}
		
		private void WriteMarketItems( StringWriter writer, MarketItem[] items, string var, Planet planet )
		{
			writer.WriteLine("var {0} = new Object();", var);
			writer.WriteLine("var {0}Array = new Array();", var);
			writer.WriteLine("var {0}Totals = new Object();", var);
			writer.WriteLine("var {0}ArrayCount = {1};", var, items.Length);
			int i = 0;
			foreach( MarketItem item in items ) {
				
				int totalQuantity = planet.getResourceCount(item.Resource.Factory.Category, item.Resource.Name);
				int totalMoney = planet.Gold;
				int total = 1;
				if( var == "sell" ) {
					total = totalQuantity;
				} else {
					total = totalMoney / item.Price;
				}
				
				writer.WriteLine("{0}[\"{1}\"] = {2};", var, item.Resource.Name, item.Price);
				writer.WriteLine("{0}Array[{1}] = \"{2}\";", var, i++, item.Resource.Name);
				writer.WriteLine("{0}Totals[\"{1}\"] = {2};", var, item.Resource.Name, total);
			}
		}
		
		private void WriteCaptions( StringWriter writer )
		{
			writer.WriteLine("var captions = new Object();");
			WriteCaption(writer, "buy");
			WriteCaption(writer, "sell");
			
			WriteResources(writer, "Intrinsic");
			WriteResources(writer, "Rare");
		}
		
		private void WriteCaption( StringWriter writer, string str )
		{
			writer.WriteLine("captions[\"{0}\"] = \"{1}\";", str, CultureModule.getContent(str));
		}
		
		private void WriteResources( StringWriter writer, string category )
		{
			foreach( ResourceFactory factory in Universe.getFactories("planet", category).Values ) {
				if( factory.create().MarketResource ) {
					WriteCaption(writer, factory.Name);
				}
			}
		}
		
		#endregion
	
	};
	
}
