// created on 10/20/2004 at 5:19 PM

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Core;
using Chronos.Messaging;

namespace Alnitak {

	public class ManageResearch : PlanetControl {
		
		#region Control Events
		
		/// <summary>Pinta o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			Ruler ruler = getRuler();

			if( ruler == null ) {
				throw new Exception("ShowPlanets:OnLoad : Ruler doesn't exists");
			}

			//MasterSkinInfo masterSkinInfo = (MasterSkinInfo)Context.Items["MasterSkinInfo"];
			string controlPath = OrionGlobals.AppPath + "skins/planetaria/controls/ManageResearch.ascx";
			Control control = Page.LoadControl(controlPath);

			QueueNotifier queue = (QueueNotifier) control.FindControl("queue");
			if( queue != null ) {
				queue.Title = info.getContent("researchQueueTitle");
				queue.Manager = ruler;
				queue.Category = "Research";
				queue.ShowQuantity = false;
				queue.ShowProductionFactor = true;
			}

			QueueErrorReport queueError = (QueueErrorReport) control.FindControl("queueError");

			initResearchList( ruler, control, queueError, "tech" );
			initResearchList( ruler, control, queueError, "planet" );
			initResearchList( ruler, control, queueError, "exploration" );
			initResearchList( ruler, control, queueError, "fleet" );
			
			HyperLink availableResearch = (HyperLink) control.FindControl("availableResearch");
			availableResearch.NavigateUrl = string.Format( "{0}?category=Research", OrionGlobals.getSectionBaseUrl("docs") );
			availableResearch.Text = info.getContent("you_can_use_docs");
			availableResearch.CssClass = "docs";
			
			HyperLink researchHelp = (HyperLink) control.FindControl("researchHelp");
			researchHelp.NavigateUrl = Wiki.GetUrl("Research", "Research");
			researchHelp.Text = info.getContent("go_to_research_wiki");
			researchHelp.CssClass = "docs";
			
			HyperLink aboutThisPage = (HyperLink) control.FindControl("aboutThisPage");
			aboutThisPage.NavigateUrl = Wiki.GetUrl("FilaDeEspera");
			aboutThisPage.Text = info.getContent("wiki_FilaDeEspera");
			aboutThisPage.CssClass = "docs";
			
			Controls.Add(control);
			
			OrionGlobals.RegisterRequest(MessageType.ResearchManagement, info.getContent("section_research"));
		}
		
		#endregion

		#region Utility Methods

		/// <summary>Inicializa o controlo de construco de pesquisas</summary>
		private void initResearchList( Ruler ruler, Control control, QueueErrorReport queueError, string cat )
		{
			Resources resources = (Resources) control.FindControl(cat);
			if( resources == null ) {
				return;
			}
			resources.Manager = ruler;
			resources.Cost = null;
			resources.ShowDemolish = false;
			resources.ShowQuantity = false;
			resources.QueueError = queueError;
			resources.Title = string.Format("<b>{0}</b> - {1}", info.getContent(cat), info.getContent("researchTitle"));
			resources.Category = "Research";
			resources.ShowDocumentation = true;
			resources.ShowDuration = true;
			resources.CategoryDescription = cat;
		}

		/// <summary>Inicializa o controlo de listagem de pesquisas</summary>
		private void initResearchOwned( Ruler ruler, ResourcesList resources )
		{
			resources.Manager = ruler;
			resources.Title = info.getContent("rulerResearch");
			resources.Category = "Research";
			resources.ShowImages = false;
			resources.ShowOnlyQuantity = true;
			resources.ShowZeroQuantity = false;
			resources.ShowQuantity = false;
			resources.ShowDocumentation = true;
		}

		#endregion
	
	};
	
}
