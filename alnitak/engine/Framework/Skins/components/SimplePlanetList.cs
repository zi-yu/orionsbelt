// created on 25-12-2004 at 10:27

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Resources;
using Chronos.Queue;
using Chronos.Actions;
using Chronos.Info.Results;
using Chronos.Messaging;
using Chronos.Utils;
using Chronos.Core;
using Language;

namespace Alnitak {

	public class SimplePlanetList : PlanetControl {

		#region Instance Fields

		private Ruler owner;

		#endregion

		#region Ctor
	
		/// <summary>Ctor</summary>
		public SimplePlanetList()
		{
		}
	
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o Ruler a mostrar</summary>
		public Ruler Owner {
			get { return owner; }
			set { owner = value; }
		}
				
		#endregion
		
		#region Control Events
		
		/// <summary>Verifica se há comandos a executar</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
		}
		
		#endregion
		
		#region Control Items Rendering
		
		/// <summary>Pinta o Controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			Owner = getRuler();
			for( int i = Owner.Planets.Length - 1; i >= 0 ; --i ) {
				writePlanet(writer, Owner.Planets[i]);
			}
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.PlanetManagement, info.getContent("section_planets"));
		}
		
		/// <summary>Pinta um planeta</summary>
		private void writePlanet( HtmlTextWriter writer, Planet planet )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'>");
			writer.WriteLine("<b>{0}</b> - {1}", planet.Name, planet.Coordinate);
			if( planet.HasImmunity ) {
				writer.Write("- <b class='green'>{0}: {1} </b>", info.getContent("Immunity"), planet.Immunity);
			}
			if( planet.IsInBattle ) {
				writer.Write("- <b class='red'>{0}</b>", info.getContent("PlanetInBattle"));
			}
			writer.WriteLine("</div>");
			
			writer.WriteLine("<table class='planetFrame'>");
		
			writer.WriteLine("<tr>");
			writer.WriteLine("<td valign='top'>");
			writer.WriteLine("<a href='{1}?id={2}'><img src='{0}' width='100' height='100'/>",
					OrionGlobals.getCommonImagePath("planets/" + planet.Info.Id + ".jpg"),
					OrionGlobals.getSectionBaseUrl("planet"), planet.Id
				);
			writer.WriteLine("</td>");
			
			writer.WriteLine("<td width='100%' valign='top'>");
			writer.WriteLine("<table width='100%'>");
			writeTitle(writer);
			writeContent(writer, planet);
			writer.WriteLine("</table>");
			writer.WriteLine("</td>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("</table>");
		}
		
		/// <summary>Pinta o título</summary>
		private void writeTitle( HtmlTextWriter writer )
		{
			writer.WriteLine("<tr class='resourceTitle'>");

			writer.WriteLine("<td class='resourceTitle'>");
			writer.WriteLine( info.getContent("category") );
			writer.WriteLine("</td>");

			writer.WriteLine("<td class='resourceTitle'><b>");
			writer.WriteLine( info.getContent("inProduction") );
			writer.WriteLine("</b></td>");
			
			writer.WriteLine("<td class='resourceTitle'>");
			writer.WriteLine( info.getContent("conclusao") );
			writer.WriteLine("</td>");
			
			writer.WriteLine("<td class='resourceTitle'>");
			writer.WriteLine( info.getContent("waiting") );
			writer.WriteLine("</td>");
			
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Pinta a informação de um planeta</summary>
		private void writeContent( HtmlTextWriter writer, Planet planet )
		{
			writeCategory(writer, planet, "Building", "buildings");
			if( SubSectionMenu.BarracksAvailable(planet) ) {
				writeCategory(writer, planet, "Intrinsic", "barracks");
			}
			writeCategory(writer, planet, "Unit", "fleet");
			
		}
		
		/// <summary>Escreve uma categoria de recursos</summary>
		private void writeCategory( HtmlTextWriter writer, Planet planet, string category, string section )
		{
			ResourceInfo resInfo = planet.getResourceInfo(category);
			if( resInfo.AvailableFactories.Count == 0 ) {
				return;
			}
		
			writer.WriteLine("<tr>");
			
			QueueItem current = planet.current(category);
			
			writer.WriteLine("<td class='resourceCell'><a class='docs' href='{1}?id={2}'>{0}</a></td>",
					info.getContent("section_"+section),
					OrionGlobals.getSectionBaseUrl(section), planet.Id
				);
			writer.WriteLine("<td class='resourceCell'>({1}) {0}</td>", (current==null?"-":info.getContent(current.FactoryName)),
				(current==null?"-":current.Quantity.ToString())
			 );
			 
			writer.WriteLine("<td class='resourceCell'>{0}</td>", (current==null?"-":current.RemainingTurns.ToString()));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", planet.queueCount(category));
			
			writer.WriteLine("</tr>");
		}
		
		#endregion
		
	};
	
}
