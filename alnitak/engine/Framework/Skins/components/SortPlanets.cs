// created on 2/26/2006 at 10:25 AM

using System;
using System.Collections;
using System.IO;
using System.Web.UI;
using Chronos.Core;
using Chronos.Sorter;
using Chronos.Utils;

namespace Alnitak {

	public class SortPlanets : Control {	

		#region Control Events
		
		const int NumberOfPlanets = 25;

		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			if( Page.Request.Form["sort"] != null ) {
				Sort();
			}
		}
		
		#endregion

		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			if( Page.User.IsInRole("admin") ) {
				writer.WriteLine("<div class='stats-box'>");
				writer.WriteLine("<div align='center'><b>Sort Planets</b></div>");
				writer.WriteLine("<input type='submit' value='Generate Planet Top Page' name='sort' />");
				writer.WriteLine("</div>");
			} else {
				writer.WriteLine("There is no spoon!");
			}
		}

		#endregion
		
		#region Utils
		
		private ArrayList GetAllPlanets()
		{
			ArrayList list = new ArrayList();
			
			foreach( Planet planet in Universe.instance.planets.Values ) {
				if( planet.InitMade ) {
					list.Add( planet );
				}
			}
			
			return list;
		}
		
		private void Sort()
		{
			ArrayList planets = GetAllPlanets();
			
			using( StreamWriter writer = new StreamWriter(Path.Combine(Platform.BaseDir, "TopPlanets.aspx.raw")) ) {
				//using( StreamWriter wiki = new StreamWriter(Path.Combine(Platform.BaseDir, "TopPlanets.wiki")) ) {
					writer.WriteLine("<div id='topPlanets'>");
					WriteBaseInfo(writer, planets);
					
					planets.Sort(new PopulationComparer());
					WriteByPopulation(writer, planets);
					//WikiWriteByPopulation(wiki, planets);
					
					planets.Sort(new FleetComparer());
					WriteByFleet(writer, planets);
					//WikiWriteByFleet(wiki, planets);
					
					planets.Sort(new CultureComparer());
					WriteByCulture(writer, planets);
					//WikiWriteByCulture(wiki, planets);
					
					planets.Sort(new ScoreComparer());
					WriteByScore(writer, planets);
					//WikiWriteByScore(wiki, planets);
					
					planets.Sort(new RatioComparer("gold"));
					WriteByRatio(writer, planets, "gold");
					//WikiWriteByRatio(wiki, planets, "gold");
					
					planets.Sort(new RatioComparer("mp"));
					WriteByRatio(writer, planets, "mp");
					//WikiWriteByRatio(wiki, planets, "mp");
					
					planets.Sort(new RatioComparer("energy"));
					WriteByRatio(writer, planets, "energy");
					//WikiWriteByRatio(wiki, planets, "energy");
					
					planets.Sort(new RatioComparer("food"));
					WriteByRatio(writer, planets, "food");
					//WikiWriteByRatio(wiki, planets, "food");
					
					planets.Sort(new SizeComparer());
					WriteBySize(writer, planets);
					//WikiWriteBySize(wiki, planets);
					
					writer.WriteLine("</div>");
				//}
			}
		}
		
		private void WriteBaseInfo( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("<div>");
			writer.WriteLine("<p><b>{0}</b>: {1}</p>", CultureModule.getContent("turn_current"), Universe.instance.TurnCount);
			writer.WriteLine("<p><b>{0}</b>: {1}</p>", CultureModule.getContent("planetas"), planets.Count);
			writer.WriteLine("</div>");
		}
		
		private void WriteByPopulation( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("<h2>Top {0}</h2>", CultureModule.getContent("labor"));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("planet"));
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("labor"));
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", planet);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink((Ruler)planet.Owner));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(planet.Population));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByPopulation( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("!!Top by Labor");
			
			writer.Write("||{{!^}}*{0}*", "#");
			writer.Write("||{{!^}}*{0}*", "Planet");
			writer.Write("||{{!^}}*{0}*", "Ruler");
			writer.WriteLine("||{{!^}}*{0}*||", CultureModule.getContent("labor"));
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", planet);
				writer.Write("||{0}", ((Ruler)planet.Owner).Name);
				writer.WriteLine("||{0}||", OrionGlobals.format(planet.Population));
			}
		}
		
		private void WriteByFleet( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("<h2>Top {0}</h2>", CultureModule.getContent("section_fleet"));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("planet"));
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>L</td>");
			writer.WriteLine("<th>M</td>");
			writer.WriteLine("<th>H</td>");
			writer.WriteLine("<th>A</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("section_fleet"));
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", planet);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink((Ruler)planet.Owner));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(FleetComparer.Count(planet, "light")));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(FleetComparer.Count(planet, "medium")));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(FleetComparer.Count(planet, "heavy")));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(FleetComparer.Count(planet, "animal")));
				writer.WriteLine("<td>{0}</td>",  OrionGlobals.format((FleetComparer.Count(planet.getDefenseFleet()))));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByFleet( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("!!!Top Rulers by Fleet");
			
			writer.Write("||{{!^}}*{0}*", "#");
			writer.Write("||{{!^}}*{0}*", "Planet");
			writer.Write("||{{!^}}*{0}*", "Ruler");
			writer.Write("||{{!^}}*{0}*", "L");
			writer.Write("||{{!^}}*{0}*", "M");
			writer.Write("||{{!^}}*{0}*", "H");
			writer.Write("||{{!^}}*{0}*", "A");
			writer.WriteLine("||{{!^}}*{0}*||", CultureModule.getContent("section_fleet"));
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", planet);
				writer.Write("||{0}", ((Ruler)planet.Owner).Name);
				writer.Write("||{0}", OrionGlobals.format(FleetComparer.Count(planet, "light")));
				writer.Write("||{0}", OrionGlobals.format(FleetComparer.Count(planet, "medium")));
				writer.Write("||{0}", OrionGlobals.format(FleetComparer.Count(planet, "heavy")));
				writer.Write("||{0}", OrionGlobals.format(FleetComparer.Count(planet, "animal")));
				writer.WriteLine("||{0}||",  OrionGlobals.format((FleetComparer.Count(planet.getDefenseFleet()))));
			}
		}
		
		private void WriteByCulture( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("<h2>Top {0}</h2>", CultureModule.getContent("culture"));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("planet"));
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("culture"));
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", planet);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink((Ruler)planet.Owner));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(planet.Culture));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByCulture( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("!!!Top by Culture");
			
			writer.Write("||{!^}*#*");
			writer.Write("||{!^}*Planet*");
			writer.Write("||{!^}*Ruler*");
			writer.WriteLine("||{{!^}}*{0}*||", CultureModule.getContent("culture"));
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", planet);
				writer.Write("||{0}", ((Ruler)planet.Owner).Name);
				writer.WriteLine("||{0}||", OrionGlobals.format(planet.Culture));
			}
			
		}
		
		private void WriteByScore( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("<h2>Top {0}</h2>", CultureModule.getContent("score"));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("planet"));
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("score"));
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", planet);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink((Ruler)planet.Owner));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(planet.Score));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByScore( StreamWriter writer, ArrayList planets )
		{
			writer.WriteLine("!!!Top by Score");
			
			writer.Write("||{!^}*#*");
			writer.Write("||{!^}*Planet*");
			writer.Write("||{!^}*Ruler*");
			writer.WriteLine("||{!^}*Score*||");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", planet);
				writer.Write("||{0}", ((Ruler)planet.Owner).Name);
				writer.WriteLine("||{0}||", OrionGlobals.format(planet.Score));
			}
		}
		
		private void WriteByRatio( StreamWriter writer, ArrayList planets, string resource )
		{
			writer.WriteLine("<h2>Top {0}</h2>", CultureModule.getContent(resource));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("planet"));
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>{0}</td>", "Per Turn");
			writer.WriteLine("<th>{0}</td>", "Ratio");
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", planet);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink((Ruler)planet.Owner));
				writer.WriteLine("<td>+{0}</td>", OrionGlobals.format(planet.getPerTurn("Intrinsic", resource)));
				writer.WriteLine("<td>%{0}</td>", planet.ModifiersRatio[resource]);
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByRatio( StreamWriter writer, ArrayList planets, string resource )
		{
			writer.WriteLine("!!!Top by {0}", CultureModule.getContent(resource));
			
			writer.Write("||{!^}*#*");
			writer.Write("||{!^}*Planet*");
			writer.Write("||{!^}*Ruler*");
			writer.Write("||{!^}*Per turn*");
			writer.WriteLine("||{!^}*Ratio*||");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", planet);
				writer.Write("||{0}", ((Ruler)planet.Owner).Name);
				writer.Write("||+{0}", OrionGlobals.format(planet.getPerTurn("Intrinsic", resource)));
				writer.WriteLine("||%{0}||", planet.ModifiersRatio[resource]);
			}
		}
		
		private void WriteBySize( StreamWriter writer, ArrayList planets  )
		{
			writer.WriteLine("<h2>Top {0}</h2>", "Size");
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("planet"));
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>{0}</td>", "Ground");
			writer.WriteLine("<th>{0}</td>", "Water");
			writer.WriteLine("<th>{0}</td>", "Orbit");
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", planet);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink((Ruler)planet.Owner));
				writer.WriteLine("<td>{0}</td>", planet.Info.GroundSpace);
				writer.WriteLine("<td>{0}</td>", planet.Info.WaterSpace);
				writer.WriteLine("<td>{0}</td>", planet.Info.OrbitSpace);
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteBySize( StreamWriter writer, ArrayList planets  )
		{
			writer.WriteLine("!!!Top by Size");
			
			writer.Write("||{!^}*#*");
			writer.Write("||{!^}*Planet*");
			writer.Write("||{!^}*Ruler*");
			writer.Write("||{!^}*Ground*");
			writer.Write("||{!^}*Water*");
			writer.WriteLine("||{!^}*Orbit*||");
			
			for( int i = 0; i < NumberOfPlanets; ++i ) {
				Planet planet = (Planet) planets[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", planet);
				writer.Write("||{0}", ((Ruler)planet.Owner).Name);
				writer.Write("||{0}", planet.Info.GroundSpace);
				writer.Write("||{0}", planet.Info.WaterSpace);
				writer.WriteLine("||{0}||", planet.Info.OrbitSpace);
			}
		}
		
		#endregion
	
	};
}
