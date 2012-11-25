// created on 9/10/2005 at 2:41 PM

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

	public class SabotageQueue : Control {
	
		#region Control Fields
		
		protected Planet source;
		
		#endregion
		
		#region Instance Properties
	
		public Planet Source {
			get { return source; }
			set { source = value; }
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
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", CultureModule.getContent("SabotageQueue"));
			writer.WriteLine("<table class='planetFrame'>");
			
			writer.WriteLine("<tr><td>");
			if( Source.Tasks.HasTask( TaskDescriptor.Sabotage ) ) {
				WriteItem(writer);
			} else {
				writer.Write(CultureModule.getContent("SabotageQueueEmpty"));
			}
			writer.WriteLine("</td></tr>");
			writer.WriteLine("</table>");
		}
		
		#endregion
		
		#region Render Utilities
		
		private void WriteItem( HtmlTextWriter writer )
		{
			TaskItem item = (TaskItem) Source.Tasks.GetList( TaskDescriptor.Sabotage )[0];
			Sabotage sabotage = (Sabotage) item.Task;
			
			writer.WriteLine("<b>{0}</b>: {1}<br/>", CultureModule.getContent("Sabotage"), CultureModule.getContent(sabotage.Key));
			writer.WriteLine("<b>{0}</b>: {1}<br/>", CultureModule.getContent("scan_target"), sabotage.Target.Coordinate.ToString());
			writer.WriteLine("<b>{0}</b>: {1}<p/>", CultureModule.getContent("RemainingTime"), item.TurnsToAction);
			writer.WriteLine("<img src='{2}' /> <a href='javascript:cancelTaskItem({1});'>{0}</a><br/>", CultureModule.getContent("cancel"), item.Id, OrionGlobals.getCommonImagePath("move.gif"));
		}
		
		/// <summary>Escreve o javascript necessário</summary>
		private void WriteScripts()
		{
			string functions = @"
				<script language='javascript'>
				
				function cancelTaskItem( id )
				{
				    document.pageContent.taskId.value = id;
				    document.pageContent.submit();
				}
						
				</script>
			";
			
			Page.RegisterStartupScript("sabotage-queue", functions);
			Page.RegisterHiddenField("taskId","");
		}
	
		#endregion
		
		#region Utilities
		
		private bool SpiesAvailable( int spies )
		{
			return source.Spies >= spies;
		}
		
		private void CheckCommand()
		{
			try {
				string command = Page.Request.Form["taskId"];
				
				if( command == null || command == "") {
					return;
				}
				
				int id = int.Parse(command);
				
				Source.Tasks.Remove( TaskDescriptor.Sabotage, id );
			
			} catch ( Exception ex ) {
				Log.log(ex);
			}
		}
		
		#endregion
	
	};
	
}
