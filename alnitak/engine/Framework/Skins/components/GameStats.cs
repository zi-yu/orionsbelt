// created on 5/17/04 at 9:52 a

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using Chronos.Persistence;
using System;

namespace Alnitak {

	public class GameStats : Control {

		/// <summary>Realiza operações relacionadas com eventos</summary>
		protected override void OnLoad( EventArgs args )
		{
			if( Page.IsPostBack ) {
				string action = Page.Request.Form["deleteCacheNotifier"];
				string target = Page.Request.Form["cacheTarget"];
				if( target != null && action != null && action == "true" ) {
					Page.Cache.Remove(target);
				}
			}
		}
	
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			EnableViewState = false;

			Universe universe = Universe.instance;
			ILanguageInfo info = CultureModule.getLanguage();
			
			ChronosStats stats = (ChronosStats) Page.Application["ChronosStats"];
			
			writer.WriteLine("<div class='stats-box'>");
			writer.WriteLine("<div align='center'><b>" + info.getContent("stats_title") + "</b></div>" );
			writer.WriteLine( info.getContent("stats_alliances") + "<b>: " + (universe.allianceCount-1) + "</b><br />" );
			writer.WriteLine( info.getContent("stats_rulers") + ": <b>" + universe.rulerCount +"</b><br />" );

			int maxP = Coordinate.MaximumGalaxies * Coordinate.MaximumSystems * Coordinate.MaximumSectors * Coordinate.MaximumPlanets;
			writer.WriteLine( info.getContent("stats_planets") + ": <b>" + universe.planetCount + "/"+ maxP +"</b><br />" );
			
			
			writer.WriteLine( info.getContent("stats_nextCoordinate") + ": <b>" + universe.CurrentCoordinate + "</b><br />" );
			writer.WriteLine("<div>--</div>");
			writer.WriteLine( info.getContent("stats_timeFromLastReboot") + ": <b>" + stats.StartTime + "</b><br />" );
			writer.WriteLine( "Dress Up: <b>" + stats.DressUp + "</b><br />" );
			writer.WriteLine( info.getContent("stats_lastTurnTime") + ": <b>" + stats.LastTurnTime + "</b><br />" );
			writer.WriteLine( info.getContent("stats_turnSinceReboot") + ": <b>" + stats.TurnCount + "</b><br />" );
			writer.WriteLine( info.getContent("stats_totalTurns") + ": <b>" + universe.TurnCount + "</b><br />" );
			writer.WriteLine( info.getContent("stats_turnTime") + ": <b>" + universe.TurnTime + "</b><br />" );
			writer.WriteLine( info.getContent("stats_lastTurn") + ": <b>" + stats.LastTurn.ToLongTimeString() + "</b><br />" );
			writer.WriteLine( info.getContent("stats_nextTurn") + ": <b>" + stats.LastTurn.AddMilliseconds(universe.TurnTime).ToLongTimeString() + "</b><br />" );
			if(Universe.instance.Persistence is UniverseSerializer) {
				writer.WriteLine( "Stream Size: <b>" + (((UniverseSerializer)Universe.instance.Persistence).StreamSize/1024) + " Kb</b><br />" );
			}
			writer.WriteLine("<div>--</div>");
			writer.WriteLine( info.getContent("stats_persistence") + ": <b>" + Chronos.Persistence.UniverseSerializer.Instance.GetType().Name + "</b><br />" );
			writer.WriteLine("<div>--</div>");
			writer.WriteLine( "<b>Alnitak: </b><br/>{0}<p/>", OrionGlobals.AlnitakInfo.Replace(";", "<br/>") );
			writer.WriteLine( "<b>Chronos: </b><br/>{0}", Chronos.Utils.Platform.ChronosInfo.Replace(";", "<br/>") );
			writer.WriteLine("</div>");

			registerScripts();

			writer.WriteLine("<p />");
			writeCacheInfo(writer, info);
			writer.WriteLine("<p />");
			writeApplicationInfo(writer, info);
			
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Generic, info.getContent("section_admin"));

		}

		/// <summary>Escreve informações sobre a Cache</summary>
		private void writeCacheInfo(HtmlTextWriter writer, ILanguageInfo info)
		{
			writer.WriteLine("<div class='stats-box'>");
			writer.WriteLine("<div align='center'><b>" + info.getContent("stats_cache_title") + "</b></div>" );
			writer.WriteLine(info.getContent("stats_cache_count") + " <b>" + Page.Cache.Count + "</b><br />" );
			writer.WriteLine("<b>" + info.getContent("stats_cache_content")+ "</b><br />");
			writer.WriteLine("<table class='stats_cache_items' >");
			foreach( DictionaryEntry item in Page.Cache ) {
				if( item.Key.ToString().StartsWith("@@") ) {
					continue;
				}
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", item.Key.ToString());
				writer.WriteLine("<td>{0}</td>", item.Value.ToString());
				writer.WriteLine("<td><a href='javascript:{1}'><img src='{0}' /></a></td>",
						OrionGlobals.getCommonImagePath("remove.gif"),
						"deleteCache(\"" + item.Key.ToString() + "\")"
					);
				writer.WriteLine("</tr>");
			}
			writer.WriteLine("</table>");
			writer.WriteLine("</div>");
		}

		/// <summary>Escreve informações sobre o ApplicationState</summary>
		private void writeApplicationInfo(HtmlTextWriter writer, ILanguageInfo info)
		{
			writer.WriteLine("<div class='stats-box'>");
			writer.WriteLine("<div align='center'><b>" + info.getContent("stats_application_title") + "</b></div>" );
			writer.WriteLine(info.getContent("stats_cache_count") + " <b>" + Page.Application.Count + "</b><br />" );
			writer.WriteLine("<b>" + info.getContent("stats_cache_content")+ "</b><br />");
			writer.WriteLine("<table class='stats_cache_items'>");

			string[] keys = Page.Application.AllKeys;
			foreach( string item in keys ) {
				writer.WriteLine("<tr>");
				writer.WriteLine("<td>{0}</td>", item);
				writer.WriteLine("<td>{0}</td>", Page.Application[item].ToString());
				writer.WriteLine("</tr>");
			}
			writer.WriteLine("</table>");
			writer.WriteLine("</div>");
		}

		/// <summary>Regista scripts de lado cliente necessários</summary>
		private void registerScripts()
		{
			string function = @"
				<script language='javascript'>
				
				var theform = document.pageContent;
				
				function deleteCache(item)
				{
					var resp = confirm('Really erase ' + item + '?');
					if( resp == false ) {
						return;
					}
					
					theform.deleteCacheNotifier.value = 'true';
					theform.cacheTarget.value = item;

				    theform.submit();
				}
				</script>
			";
			
			Page.RegisterStartupScript("deleteCache", function);
			Page.RegisterHiddenField("deleteCacheNotifier","false");
			Page.RegisterHiddenField("cacheTarget","");
		}
	};
	
}
