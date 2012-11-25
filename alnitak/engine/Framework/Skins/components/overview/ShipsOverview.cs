// created on 7/6/2005 at 9:17 AM

using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;
using Chronos.Core;
using Chronos.Resources;
using System;

namespace Alnitak {
	
	public class ShipsOverview : ResourcesOverview {
	
		#region Control Rendering
		
		/// <summary>Envia notcias</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			try {
				OrionGlobals.RegisterRequest( Chronos.Messaging.MessageType.FleetManagement, CultureModule.getContent("section_ships") );
				
				User user = (User) Context.User;
				Ruler ruler = Universe.instance.getRuler(user.RulerId);
				
				WriteShips(ruler, writer, new string[]{"light", "medium"});
				WriteShips(ruler, writer, new string[]{"heavy", "animal", "special"});
				
			} catch(Exception ex) {
				writer.WriteLine("<!-- {0} -->", ex.ToString());
			}
		}
		
		/// <summary>Mostra os recursos</summary>
		private void WriteShips( Ruler ruler, HtmlTextWriter writer, string[] categories )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'>{0}</div>", CultureModule.getContent("Unit"));
			writer.WriteLine("<table class='planetFrame'>");
			WriteTitle(writer, categories);
		
			Hashtable counter = new Hashtable();
			foreach( Planet planet in ruler.Planets ) {
				WritePlanetShips(planet, writer, counter, categories);
			}
			
			WriteTotal(writer, Ships.Keys, counter, true);
			
			writer.WriteLine("</table>");
		}
		
		/// <summary>Escreve o título</summary>
		private void WriteTitle( HtmlTextWriter writer, string[] categories )
		{
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.Write("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("stats_planets"));
			foreach( ResourceFactory fact in Ships.Values ) {
				if( !FromCategory(categories, fact.Unit.UnitType) ) {
					continue;
				}
				writer.Write("<td class='resourceTitle'>");
				writer.Write("<img src='{0}.gif' width='20' height='20' title='{1}'/>", OrionGlobals.getCommonImagePath(fact.Name.ToLower()), CultureModule.getContent(fact.Name));
				writer.Write("</td>");
			}
			writer.Write("<td class='resourceTitle'>Total</td>");
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Escreve os recursos de um planeta</summary>
		private void WritePlanetShips( Planet planet, HtmlTextWriter writer, Hashtable counter, string[] categories )
		{
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			WritePlanet(planet, writer, "fleet");
			
			int total = 0;

			foreach( ResourceFactory factory in Ships.Values ) {
				string res = factory.Name;
				if( !FromCategory(categories, factory.Unit.UnitType) ) {
					continue;
				}
				int quantity = ShipCount(planet, res);
				writer.WriteLine("<td class='{0}'>{1}</td>", GetCss(quantity), quantity);
				RegisterQuantity(counter, res, quantity);
				total += quantity;
			}
			
			writer.WriteLine("<td class='{0}'>{1}</td>", GetCss(total), total);
			
			writer.WriteLine("</tr>");
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Obtém todas as factories de naves</summary>
		private SortedList Ships {
			get {
				return Universe.getFactories("planet", "Unit");
			}
		}
		
		/// <summary>Indica a quantidade de naves de um tipo num planeta</summary>
		private int ShipCount( Planet planet, string ship ) 
		{
			if( planet.Fleets == null ) {
				return 0;
			}
			int count = 0;
			foreach( Chronos.Core.Fleet fleet in planet.Fleets.Values ) {
				if( !fleet.Ships.Contains(ship) ) {
					continue;
				}
				count += (int) fleet.Ships[ship];
			}
			return count;
		}
		
		private bool FromCategory( string[] cats, string cat ) 
		{
			foreach( string c in cats ) {
				if( cat == c ) {
					return true;
				}
			}
			return false;
		}
		
		#endregion
	
	};
	
}