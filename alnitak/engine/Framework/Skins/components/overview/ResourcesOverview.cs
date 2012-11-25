// created on 7/6/2005 at 7:57 AM

using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {
	
	public class ResourcesOverview : Page {
	
		#region Static
		
		private static string[] Resources = new string[] {
			"gold", "mp", "food", "labor", "energy", "polution", "culture"
		};
		
		private static string[] Modifiers = new string[] {
			"gold", "mp", "food", "labor", "energy", "polution", "culture"
		};
		
		private static string[] General = new string[] {
			"ProductionFactor", "FarAwayFromHomeFactor", "labor", "culture", "polution", "housing", "groundSpace", "waterSpace", "orbitSpace", "score"
		};
		
		#endregion
	
		#region Control Rendering
		
		/// <summary>Envia notcias</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			try {
				OrionGlobals.RegisterRequest( Chronos.Messaging.MessageType.PlanetManagement, CultureModule.getContent("section_resources") );
				
				User user = (User) Context.User;
				Ruler ruler = Universe.instance.getRuler(user.RulerId);
				
				WriteResources(ruler, writer);
				WriteModifiers(ruler, writer);
				WriteRareResources(ruler, writer);
				WriteGeneral(ruler, writer);
				
			} catch(Exception ex) {
				writer.WriteLine("<!-- {0} -->", ex.ToString());
			}
		}
		
		/// <summary>Mostra os modificadores</summary>
		private void WriteModifiers( Ruler ruler, HtmlTextWriter writer )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'>{0}</div>", CultureModule.getContent("modificadores"));
			writer.WriteLine("<table class='planetFrame'>");
			WriteTitle(writer, Modifiers);
		
			Hashtable counter = new Hashtable();
			foreach( Planet planet in ruler.Planets ) {
				WritePlanetModifiers(planet, writer, counter);
			}
			
			WriteTotal(writer, Modifiers, counter, false);
			
			writer.WriteLine("</table>");
		}
		
		/// <summary>Mostra os recursos</summary>
		private void WriteResources( Ruler ruler, HtmlTextWriter writer )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'>{0}</div>", CultureModule.getContent("section_resources"));
			writer.WriteLine("<table class='planetFrame'>");
			WriteTitle(writer, Resources);
		
			Hashtable counter = new Hashtable();
			foreach( Planet planet in ruler.Planets ) {
				WritePlanetResources(planet, writer, counter);
			}
			
			WriteTotal(writer, Resources, counter, false);
			
			writer.WriteLine("</table>");
		}
		
		/// <summary>Mostra os recursos</summary>
		private void WriteRareResources( Ruler ruler, HtmlTextWriter writer )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'>{0}</div>", CultureModule.getContent("Rare"));
			writer.WriteLine("<table class='planetFrame'>");
			WriteTitle(writer, Universe.getFactories("planet", "Rare").Keys);
		
			Hashtable counter = new Hashtable();
			foreach( Planet planet in ruler.Planets ) {
				WritePlanetRareResources(planet, writer, counter);
			}
			
			WriteTotal(writer, Universe.getFactories("planet", "Rare").Keys, counter, false);
			
			writer.WriteLine("</table>");
		}
		
		/// <summary>Escreve o título</summary>
		private void WriteTitle( HtmlTextWriter writer, IEnumerable resources )
		{
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.Write("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("stats_planets"));
			foreach( string res in resources ) {
				writer.Write("<td class='resourceTitle'>");
				writer.Write("<img src='{0}.gif' title='{1}' alt='{1}'/>", OrionGlobals.getCommonImagePath("resources/"+res), CultureModule.getContent(res));
				writer.Write("</td>");
			}
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Escreve os recursos de um planeta</summary>
		private void WritePlanetResources( Planet planet, HtmlTextWriter writer, Hashtable counter )
		{
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			WritePlanet(planet, writer, "buildings");
			foreach( string res in Resources ) {
				int quantity = planet.getResourceCount("Intrinsic", res);
				writer.WriteLine("<td class='{0}'>{1}</td>", GetCss(res, quantity), quantity);
				RegisterQuantity(counter, res, quantity);
			}
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Escreve os recursos de um planeta</summary>
		private void WritePlanetRareResources( Planet planet, HtmlTextWriter writer, Hashtable counter )
		{
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			WritePlanet(planet, writer, "buildings");
			foreach( string res in Universe.getFactories("planet", "Rare").Keys ) {
				int quantity = planet.getResourceCount("Rare", res);
				writer.WriteLine("<td class='{0}'>{1}</td>", GetCss(quantity), quantity);
				RegisterQuantity(counter, res, quantity);
			}
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Escreve os recursos de um planeta</summary>
		private void WritePlanetModifiers( Planet planet, HtmlTextWriter writer, Hashtable counter )
		{
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			WritePlanet(planet, writer, "buildings");
			foreach( string res in Modifiers ) {
				int mod = GetModifier(planet, res);
				int perTurn = GetPerTurn(planet, "Intrinsic", res);
				writer.WriteLine("<td class='{0}'>%{1} -&gt; {2}</td>", GetCss(res, perTurn), mod, perTurn);
				RegisterQuantity(counter, res, perTurn);
			}
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Escreve o nome de um planeta</summary>
		protected void WritePlanet( Planet planet, HtmlTextWriter writer, string targetSection )
		{
			writer.WriteLine("<td class='resourceManagement'><a href='{0}default.aspx?id={1}' title='{3}'>{2}</a>", 
				OrionGlobals.getSectionUrl(targetSection),
				planet.Id,
				planet.Name,
				planet.ToString()
			);
			if( planet.IsInBattle ) {
				writer.WriteLine("<img src='{0}'/>", OrionGlobals.getCommonImagePath("messages/Battle.gif"));	
			}
			writer.WriteLine("</td>");
		}
		
		/// <summary>Escreve o total</summary>
		protected void WriteTotal( HtmlTextWriter writer, ICollection resources, Hashtable counter, bool addTotal )
		{
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.Write("<td class='resourceManagement'>Total</td>");
			
			int total = 0;
			
			foreach( string res in resources ) {
				object count = counter[res];
				if( count == null ) {
					continue;
				}
				int quantity = (int)count;
				writer.Write("<td class='{0}'>{1}</td>", GetCss(quantity), counter[res]);
				total += quantity;
			}
			
			if( addTotal ) {
				writer.Write("<td class='{0}'>{1}</td>", GetCss(total), total);
			}
			
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Mostra informação geral</summary>
		private void WriteGeneral( Ruler ruler, HtmlTextWriter writer )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'>{0}</div>", CultureModule.getContent("generalInformation"));
			writer.WriteLine("<table class='planetFrame'>");
			WriteTitle(writer, General);
		
			Hashtable counter = new Hashtable();
			foreach( Planet planet in ruler.Planets ) {
				WritePlanetGeneral(planet, writer, counter);
			}
			
			WriteTotal(writer, General, counter, false);
			
			writer.WriteLine("</table>");
		}
		
		private void WritePlanetGeneral( Planet planet, HtmlTextWriter writer, Hashtable counter )
		{
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			WritePlanet(planet, writer, "buildings");
			
			int factor = (int)(planet.ProductionFactor*100);
			writer.WriteLine("<td class='{0}'>{1} %</td>", GetFactorCss(factor), factor);
			RegisterQuantity(counter, "ProductionFactor", factor);
			
			factor = (int)(planet.FarAwayFromHomeFactor) * 1;
			writer.WriteLine("<td class='{0}'>{1} %</td>", GetInverseCss(factor), factor);
			RegisterQuantity(counter, "FarAwayFromHomeFactor", factor);
			
			factor = (int)(planet.LaborInfluence) * -1;
			writer.WriteLine("<td class='{0}'>{1} %</td>", GetInverseCss(factor), factor);
			RegisterQuantity(counter, "labor", factor);
			
			factor = (int)(planet.CultureInfluence) * -1;
			writer.WriteLine("<td class='{0}'>{1} %</td>", GetInverseCss(factor), factor);
			RegisterQuantity(counter, "culture", factor);
			
			factor = (int)(planet.PolutionInfluence) * 1;
			writer.WriteLine("<td class='{0}'>{1} %</td>", GetInverseCss(factor), factor);
			RegisterQuantity(counter, "polution", factor);
			
			factor = (int)(planet.HousingInfluence) * 1;
			writer.WriteLine("<td class='{0}'>{1} %</td>", GetInverseCss(factor), factor);
			RegisterQuantity(counter, "housing", factor);
			
			for( int i = 6; i < General.Length; ++i ) {
				string res = General[i];
				int quantity = planet.getResourceCount("Intrinsic", res);
				writer.WriteLine("<td class='{0}'>{1}</td>", GetCss(quantity), quantity);
				RegisterQuantity(counter, res, quantity);
			}
			
			writer.WriteLine("</tr>");
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Regista uma quantidade numa Hash</summary>
		protected void RegisterQuantity( Hashtable hash, string res, int quantity )
		{
			if( hash.ContainsKey(res) ) {
				int curr = (int) hash[res];
				hash[res] = curr + quantity;
				return;
			}
			hash.Add(res, quantity);
		}
		
		/// <summary>Indica o CSS de uma quantidade</summary>
		public static string GetCss( string res, int quantity ) 
		{
			if( res != "polution" ) {
				return GetCss(quantity);
			}
			
			return GetCss(-quantity);
		}
		
		/// <summary>Indica o CSS de uma quantidade</summary>
		public static string GetCss( int quantity ) 
		{
			string css = "resourceCostCellSucceeded";
			if( quantity < 0 ) {
				css = "resourceCostCellFailed";
			}
			return css;
		}
		
		/// <summary>Indica o CSS de uma quantidade</summary>
		public static string GetInverseCss( int quantity ) 
		{
			string css = "resourceCostCellSucceeded";
			if( quantity > 0 ) {
				css = "resourceCostCellFailed";
			}
			return css;
		}
		
		/// <summary>Indica o CSS de uma quantidade</summary>
		public static string GetFactorCss( int quantity ) 
		{
			string css = "resourceCostCellSucceeded";
			if( quantity >100 ) {
				css = "resourceCostCellFailed";
			}
			return css;
		}
		
		/// <summary>Indica o ratio de um recurso</summary>
		static public int GetModifier( Planet planet, string res )
		{
			object obj = planet.getRatio(res);
			if( null != obj ) {
				return (int) obj;
			}
			return 0;
		}

		/// <summary>Indica a quantidade de um recurso por turno</summary>
		static public int GetPerTurn( Planet planet, string category, string res )
		{
			object obj = planet.getPerTurn(category, res);
			if( null != obj ) {
				return (int) obj;
			}
			return 0;
		}
		
		#endregion
	};
	
}