// created on 2/28/2006 at 12:23 PM

using System;
using System.Collections;
using System.IO;
using System.Web.UI;
using Chronos.Core;
using Chronos.Sorter;
using Chronos.Utils;

namespace Alnitak {

	public class SortRulers : Control {	

		#region Control Events
		
		const int NumberOfRulers = 30;

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
				writer.WriteLine("<div align='center'><b>Sort rulers</b></div>");
				writer.WriteLine("<input type='submit' value='Generate Ruler Top Page' name='sort' />");
				writer.WriteLine("</div>");
			} else {
				writer.WriteLine("There is no spoon!");
			}
		}

		#endregion
		
		#region Utils
		
		private ArrayList GetAllRulers()
		{
			ArrayList list = new ArrayList();
			
			foreach( Ruler ruler in Universe.instance.rulers.Values ) {
				list.Add(ruler);
			}
			
			return list;
		}
		
		private void Sort()
		{
			ArrayList rulers = GetAllRulers();
			
			using( StreamWriter writer = new StreamWriter(Path.Combine(Platform.BaseDir, "TopRulers.aspx.raw")) ) {
				//using( StreamWriter wiki = new StreamWriter(Path.Combine(Platform.BaseDir, "TopRulers.wiki")) ) {
					writer.WriteLine("<div id='topPlanets'>");
					WriteBaseInfo(writer, rulers);
					//WikiWriteBaseInfo(wiki, rulers);
					
					rulers.Sort(new PopulationComparer());
					WriteByPopulation(writer, rulers);
					//WikiWriteByPopulation(wiki, rulers);
					
					rulers.Sort(new FleetComparer());
					WriteByFleet(writer, rulers);
					//WikiWriteByFleet(wiki, rulers);
					
					rulers.Sort(new CultureComparer());
					WriteByCulture(writer, rulers);
					//WikiWriteByCulture(wiki, rulers);
					
					rulers.Sort(new ScoreComparer());
					WriteByScore(writer, rulers);
					//WikiWriteByScore(wiki, rulers);
					
					rulers.Sort(new RatioComparer("gold"));
					WriteByRatio(writer, rulers, "gold");
					//WikiWriteByRatio(wiki, rulers, "gold");
					
					rulers.Sort(new RatioComparer("mp"));
					WriteByRatio(writer, rulers, "mp");
					//WikiWriteByRatio(wiki, rulers, "mp");
					
					rulers.Sort(new RatioComparer("energy"));
					WriteByRatio(writer, rulers, "energy");
					//WikiWriteByRatio(wiki, rulers, "energy");
					
					rulers.Sort(new RatioComparer("food"));
					WriteByRatio(writer, rulers, "food");
					//WikiWriteByRatio(wiki, rulers, "food");
					
					rulers.Sort(new SizeComparer());
					WriteBySize(writer, rulers);
					//WikiWriteBySize(wiki, rulers);
					
					writer.WriteLine("</div>");
				//}
			}
		}
		
		private void WriteBaseInfo( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("<div>");
			writer.WriteLine("<p><b>{0}</b>: {1}</p>", CultureModule.getContent("turn_current"), Universe.instance.TurnCount);
			writer.WriteLine("<p><b>{0}</b>: {1}</p>", "Rulers", rulers.Count);
			writer.WriteLine("</div>");
		}
		
		private void WikiWriteBaseInfo( StreamWriter writer, ArrayList rulers )
		{
		}
		
		private void WriteByPopulation( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("<h2>Top Rulers by {0}</h2>", CultureModule.getContent("labor"));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>Planets</td>");
			writer.WriteLine("<th>Avg</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("labor"));
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink(ruler));
				writer.WriteLine("<td>{0}</td>", ruler.Planets.Length);
				
				int count = PopulationComparer.Count(ruler);
				
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(count / ruler.Planets.Length));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(count));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByPopulation( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("!!!Top Rulers by {0}", CultureModule.getContent("labor"));
			
			writer.Write("||{{!^}}*{0}*", "#");
			writer.Write("||{{!^}}*{0}*", "Ruler");
			writer.Write("||{{!^}}*{0}*", "Planets");
			writer.Write("||{{!^}}*{0}*", "Avg");
			writer.WriteLine("||{{!^}}*{0}*||", CultureModule.getContent("labor"));
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", ruler.Name);
				writer.Write("||{0}", ruler.Planets.Length);
				
				int count = PopulationComparer.Count(ruler);
				
				writer.Write("||{0}", OrionGlobals.format(count / ruler.Planets.Length));
				writer.WriteLine("||{0}||", OrionGlobals.format(count));
			}
		}
		
		private void WriteByFleet( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("<h2>Top Rulers by {0}</h2>", CultureModule.getContent("section_fleet"));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>L</td>");
			writer.WriteLine("<th>M</td>");
			writer.WriteLine("<th>H</td>");
			writer.WriteLine("<th>A</td>");
			writer.WriteLine("<th>Planets</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("section_fleet"));
			writer.WriteLine("<th>Average</td>");
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink(ruler));
				writer.WriteLine("<td>{0}</td>",OrionGlobals.format( FleetComparer.Count(ruler, "light")));
				writer.WriteLine("<td>{0}</td>",OrionGlobals.format( FleetComparer.Count(ruler, "medium")));
				writer.WriteLine("<td>{0}</td>",OrionGlobals.format( FleetComparer.Count(ruler, "heavy")));
				writer.WriteLine("<td>{0}</td>",OrionGlobals.format( FleetComparer.Count(ruler, "animal")));
				writer.WriteLine("<td>{0}</td>", ruler.Planets.Length);
				
				int count = FleetComparer.Count(ruler);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(count));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(count / ruler.Planets.Length));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByFleet( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("!!!Top Rulers by Fleet");
			
			writer.Write("||{{!^}}*{0}*", "#");
			writer.Write("||{{!^}}*{0}*", "Ruler");
			writer.Write("||{{!^}}*{0}*", "L");
			writer.Write("||{{!^}}*{0}*", "M");
			writer.Write("||{{!^}}*{0}*", "H");
			writer.Write("||{{!^}}*{0}*", "A");
			writer.Write("||{{!^}}*{0}*", "Planets");
			writer.Write("||{{!^}}*{0}*", CultureModule.getContent("section_fleet"));
			writer.WriteLine("||{{!^}}*{0}*||", "Avg");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", ruler.Name);
				writer.Write("||{0}",OrionGlobals.format( FleetComparer.Count(ruler, "light")));
				writer.Write("||{0}",OrionGlobals.format( FleetComparer.Count(ruler, "medium")));
				writer.Write("||{0}",OrionGlobals.format( FleetComparer.Count(ruler, "heavy")));
				writer.Write("||{0}",OrionGlobals.format( FleetComparer.Count(ruler, "animal")));
				writer.Write("||{0}", ruler.Planets.Length);
				
				int count = FleetComparer.Count(ruler);
				writer.Write("||{0}", OrionGlobals.format(count));
				writer.WriteLine("||{0}||", OrionGlobals.format(count / ruler.Planets.Length));
			}
		}
		
		private void WriteByCulture( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("<h2>Top Rulers by {0}</h2>", CultureModule.getContent("culture"));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>Planets</td>");
			writer.WriteLine("<th>Avg</td>");
			writer.WriteLine("<th>{0}</td>", CultureModule.getContent("culture"));
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink(ruler));
				writer.WriteLine("<td>{0}</td>", ruler.Planets.Length);
				
				int count = ruler.getResourceCount("Intrinsic", "culture");
				
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(count/ruler.Planets.Length));
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format(count));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByCulture( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("!!!Top Rulers by {0}", CultureModule.getContent("culture"));
			
			writer.Write("||{!^}*#*");
			writer.Write("||{!^}*Ruler*");
			writer.Write("||{!^}*Planets*");
			writer.Write("||{!^}*Avg*");
			writer.WriteLine("||{{!^}}*{0}*||", CultureModule.getContent("culture"));
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", ruler.Name);
				writer.Write("||{0}", ruler.Planets.Length);
				
				int count = ruler.getResourceCount("Intrinsic", "culture");
				
				writer.Write("||{0}", OrionGlobals.format(count/ruler.Planets.Length));
				writer.WriteLine("||{0}||", OrionGlobals.format(count));
			}
		}
		
		private void WriteByScore( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("<h2>Total Planet{0}/Number of Planets</h2>", CultureModule.getContent("score"));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>Planets</td>");
			writer.WriteLine("<th>{0}</td>", "Average");
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink(ruler));
				writer.WriteLine("<td>{0}</td>", ruler.Planets.Length);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.format((int)Math.Round(ScoreComparer.Count(ruler))));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByScore( StreamWriter writer, ArrayList rulers )
		{
			writer.WriteLine("!!!Total PlanetScore/Number of Planets");
			
			writer.Write("||{!^}*#*");
			writer.Write("||{!^}*Ruler*");
			writer.Write("||{!^}*Planets*");
			writer.WriteLine("||{!^}*Average*||");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", ruler.Name);
				writer.Write("||{0}", ruler.Planets.Length);
				writer.WriteLine("||{0}||", OrionGlobals.format((int)Math.Round(ScoreComparer.Count(ruler))));
			}
			
		}
		
		private void WriteByRatio( StreamWriter writer, ArrayList rulers, string resource )
		{
			writer.WriteLine("<h2>Top Rulers by {0}</h2>", CultureModule.getContent(resource));
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>Planets</td>");
			writer.WriteLine("<th>Avg</td>");
			writer.WriteLine("<th>{0}</td>", "Per Turn");
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink(ruler));
				writer.WriteLine("<td>{0}</td>", ruler.Planets.Length);
				
				int ratio = RatioComparer.Count(ruler, resource);
				
				writer.WriteLine("<td>+{0}</td>", OrionGlobals.format(ratio/ruler.Planets.Length));
				writer.WriteLine("<td>+{0}</td>", OrionGlobals.format(ratio));
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteByRatio( StreamWriter writer, ArrayList rulers, string resource )
		{
			writer.WriteLine("!!!Top Rulers by {0}", CultureModule.getContent(resource));
			
			writer.Write("||{!^}*#*");
			writer.Write("||{!^}*Ruler*");
			writer.Write("||{!^}*Planets*");
			writer.Write("||{!^}*Avg*");
			writer.WriteLine("||{!^}*Per Turn*||");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];

				writer.Write("||{0}", i+1);
				writer.Write("||{0}", ruler.Name);
				writer.Write("||{0}", ruler.Planets.Length);
				
				int ratio = RatioComparer.Count(ruler, resource);
				
				writer.Write("||+{0}", OrionGlobals.format(ratio/ruler.Planets.Length));
				writer.WriteLine("||+{0}||", OrionGlobals.format(ratio));
			}
		}
		
		private void WriteBySize( StreamWriter writer, ArrayList rulers  )
		{
			writer.WriteLine("<h2>Top Rulers by Total {0}</h2>", "Size");
			writer.WriteLine("<table>");
			
			writer.WriteLine("<tr>");
			writer.WriteLine("<th>#</td>");
			writer.WriteLine("<th>Ruler</td>");
			writer.WriteLine("<th>Planets</td>");
			writer.WriteLine("<th>Avg</td>");
			writer.WriteLine("<th>{0}</td>", "Space");
			writer.WriteLine("</tr>");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", i+1);
				writer.WriteLine("<td>{0}</td>", OrionGlobals.getLink(ruler));
				writer.WriteLine("<td>{0}</td>", ruler.Planets.Length);
				
				int count = SizeComparer.Count(ruler);
				
				writer.WriteLine("<td>{0}</td>",OrionGlobals.format(count/ruler.Planets.Length) );
				writer.WriteLine("<td>{0}</td>",OrionGlobals.format(count) );
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		private void WikiWriteBySize( StreamWriter writer, ArrayList rulers  )
		{
			writer.WriteLine("!!!Top Rulers by Total {0}", "Size");
			
			writer.Write("||{!^}*#*");
			writer.Write("||{!^}*Ruler*");
			writer.Write("||{!^}*Planets*");
			writer.Write("||{!^}*Avg*");
			writer.WriteLine("||{!^}*Space*||");
			
			for( int i = 0; i < NumberOfRulers; ++i ) {
				Ruler ruler = (Ruler) rulers[i];
				writer.Write("||{0}", i+1);
				writer.Write("||{0}", ruler.Name);
				writer.Write("||{0}", ruler.Planets.Length);
				
				int count = SizeComparer.Count(ruler);
				
				writer.Write("||{0}",OrionGlobals.format(count/ruler.Planets.Length) );
				writer.WriteLine("||{0}||",OrionGlobals.format(count) );
			}
		}
		
		#endregion
	
	};
}
