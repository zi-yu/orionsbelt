// created on 9/10/2005 at 10:47 AM

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
using Chronos.Sabotages;
using Chronos.Sabotages.Factories;
using System;
using System.Text.RegularExpressions;

namespace Alnitak {

	public class SabotageList : Control {
	
		#region Control Fields
		
		protected QueueErrorReport report;
		protected Planet source;
		protected Travel travel;
		
		#endregion
		
		#region Instance Properties
		
		public QueueErrorReport Report {
			get { return report; }
			set { report = value; }
		}
		
		public Planet Source {
			get { return source; }
			set { source = value; }
		}
		
		public Travel TravelControl {
			get { return travel; }
			set { travel = value; }
		}
		
		public Planet Target {
			get {
				return Universe.instance.getPlanet(TravelControl.Coordinate);
			}
		}
			
		#endregion
	
		#region Control Events
		
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			if( Page.IsPostBack ) {
				CheckCommand();
			}
		}
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{	
			WriteScripts();
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", CultureModule.getContent("Sabotage"));
			writer.WriteLine("<table class='planetFrame'>");
			
			writer.WriteLine("<tr class='resourceTitle'>");
			WriteTitle(writer);
			writer.WriteLine("</tr>");
			WriteItems(writer);
			writer.WriteLine("</table>");
		}
		
		#endregion
		
		#region Render Utilities
		
		private void WriteTitle( HtmlTextWriter writer )
		{
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("Sabotage"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("stats_totalTurns"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("spy"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", CultureModule.getContent("Perform"));
		}
		
		private void WriteItems( HtmlTextWriter writer )
		{
			foreach( SabotageFactory factory in Sabotage.Factories.Values ) {
				Sabotage sabotage = (Sabotage) factory.create(null);
				bool canSabotage = sabotage.CanSabotage(Source, Source).Ok;
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resource'>{0}</td>", CultureModule.getContent(sabotage.Key));
				writer.WriteLine("<td class='resourceCell'>{0}</td>", sabotage.Turns);
				writer.WriteLine("<td class='resourceCell'>{0}</td>", sabotage.Spies);
				writer.WriteLine("<td class='resourceCell'><a href='javascript:performSabotage(\"{1}\");'><img src='{0}' /></a></td>", OrionGlobals.YesNoImage(canSabotage), sabotage.Key);
				writer.WriteLine("</tr>");
			}
		}
		
		/// <summary>Escreve o javascript necessário</summary>
		private void WriteScripts()
		{
			string functions = @"
				<script language='javascript'>
				
				function performSabotage( sabotageType )
				{
				    document.pageContent.sabotage.value = sabotageType;
				    document.pageContent.submit();
				}
						
				</script>
			";
			
			Page.RegisterStartupScript("sabotage", functions);
			Page.RegisterHiddenField("sabotage","");
		}
	
		#endregion
		
		#region Utilities
		
		private void CheckCommand()
		{
			try {
				string command = Page.Request.Form["sabotage"];
				
				if( command == null || command == "") {
					return;
				}
				
				Planet target = Target;
				
				Sabotage sabotage = Sabotage.GetSabotage(command);
				Result result = sabotage.CanSabotage( Source, target );
				report.ResultSet = result;
				
				if( result.Ok ) {
					sabotage.PrepareSabotage(Source, target);
				}
				
				Log.log("----------SABOTAGE----------------");
				Log.log(result.log());
			
			} catch ( Exception ex ) {
				Log.log(ex);
			}
		}
		
		#endregion
	
	};
	
}
