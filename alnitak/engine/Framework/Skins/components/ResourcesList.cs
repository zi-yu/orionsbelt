// created on 10/23/2004 at 9:46 AM

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Resources;
using Chronos.Queue;
using Chronos.Actions;
using Chronos.Core;
using Chronos.Info.Results;
using Language;

namespace Alnitak {

	public class ResourcesList : Control {
	
		#region Instance Fields
		
		private string title;
		private ResourceManager manager;
		private string category;
		private ILanguageInfo info = CultureModule.getLanguage();
		private bool showImages;
		private bool showOnlyQuantity;
		private bool showZeroQuantity;
		private bool showQuantity;
		private bool showDocumentation;
		private bool showRareResources;
		private bool showSpace;
		private bool checkRatio;
		private string[] resourcesToShow;
			
		#endregion
				
		#region Ctor
	
		/// <summary>Ctor</summary>
		public ResourcesList()
		{
			title = string.Empty;
			category = "Building";
			showImages = true;
			showOnlyQuantity = false;
			showZeroQuantity = true;
			showQuantity = true;
			showDocumentation = true;
			showRareResources = true;
			checkRatio = false;
			resourcesToShow = null;
			showSpace = false;
		}
	
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o título do controlo</summary>
		public string Title {
			get { return title; }
			set { title = value; }
		}
		
		/// <summary>Indica o ResourceManager</summary>
		public ResourceManager Manager {
			get { return manager; }
			set { manager = value; }
		}
		
		/// <summary>Indica a categoria a mostrar</summary>
		public string Category {
			get { return category; }
			set { category = value; }
		}

		/// <summary>Indica se  para mostrar as imagens referentes a cada recurso</summary>
		public bool ShowImages {
			get { return showImages; }
			set { showImages = value; }
		}
		
		/// <summary>Indica se são criados links para a documentação</summary>
		public bool ShowDocumentation {
			get { return showDocumentation; }
			set { showDocumentation = value; }
		}

		/// <summary>Indica se  somente para mostrar o recurso e a sua quantidade</summary>
		public bool ShowOnlyQuantity {
			get { return showOnlyQuantity; }
			set { showOnlyQuantity = value; }
		}

		/// <summary>Indica se  para no mostrar recursos que no tenham quantidade</summary>
		public bool ShowZeroQuantity {
			get { return showZeroQuantity; }
			set { showZeroQuantity = value; }
		}
		
		/// <summary>Indica se  para no mostrar recursos raros</summary>
		public bool ShowRareResources {
			get { return showRareResources; }
			set { showRareResources = value; }
		}

		/// <summary>Indica se  para mostrar a quantidade de um recurso</summary>
		public bool ShowQuantity {
			get { return showQuantity; }
			set { showQuantity = value; }
		}

		/// <summary>Nomes dos recursos a mostrar</summary>
		public string[] ResourcesToShow {
			get { return resourcesToShow; }
			set { resourcesToShow = value; }
		}
		
		/// <summary>Indica se  para mostrar uma tabela com o espao</summary>
		public bool ShowSpace {
			get { return showSpace; }
			set { showSpace = value; }
		}
		
		/// <summary>Indica se é para mostrar recuros com ratio maior que zero</summary>
		public bool CheckRatio {
			get { return checkRatio; }
		}
		
		#endregion
				
		#region Control Title Rendering
		
		/// <summary>Escreve o título da tabela</summary>
		private void writeTitle( HtmlTextWriter writer )
		{
			if( ShowImages ) {
				writer.WriteLine("<td class='resourceTitle'>-</td>");
			}
			
			writer.WriteLine("<td class='resourceTitle'>"+info.getContent("recurso")+"</td>");
			
			if( ! showOnlyQuantity ) {
				writer.WriteLine("<td class='resourceTitle' title='{1}'>{0}</td>", info.getContent("modificadores"), info.getContent("modificadores_title"));
				writer.WriteLine("<td class='resourceTitle' title='{1}'>{0}</td>", info.getContent("turnIncome"), info.getContent("turnIncome_title"));
			}
			if( ShowQuantity ) {
				writer.WriteLine("<td class='resourceTitle' title='{1}'>{0}</td>", info.getContent("quantidade"), info.getContent("quantidade_title"));
			}
		}
				
		#endregion
		
		#region Control Items Rendering
		
		/// <summary>Pinta o Controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>" + Title + "</b></div>");
			writeResources(writer, Category);
			
			ResourcesToShow = null;
			checkRatio = true;
			ShowZeroQuantity = true;
			if( ShowRareResources ) {
				writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", info.getContent("RareResources"));
				writeResources(writer, "Rare");
			}
			
			if( ShowSpace ) {
				writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", info.getContent("spaceTitle"));
				writeSpace(writer);
			}
		}
		
		/// <summary>Escreve os recursos</summary>
		private void writeResources( HtmlTextWriter writer, string category )
		{
			ResourceInfo resources = Manager.getResourceInfo(category);
			if( !CheckRatio && resources.Resources.Keys.Count == 0 ) {
				writer.WriteLine("<table class='planetFrame'><tr><td>{0}</td></tr></table>",info.getContent("noneAvailable"));
				return;
			}

			writer.WriteLine("<table class='planetFrame' width='100%'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writeTitle(writer);
			writer.WriteLine("</tr>");

			if( ResourcesToShow != null ) {
				foreach( string res in ResourcesToShow ) {
					writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
					writeLine(writer, resources, res);
					writer.WriteLine("</tr>");
				}
			} else {
				foreach( Resource available in resources.Resources.Keys ) {
					if( CheckRatio ) {
						int ratio = Manager.getRatio(available.Name);
						if( ratio == 0 && Manager.getResourceCount(available.Factory.Category, available.Name) == 0) {
							continue;
						}
					}
					writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
					writeLine(writer, resources, available.Name);
					writer.WriteLine("</tr>");
				}
			}
			
			writer.WriteLine("</table>");
		}
		
		/// <summary>Escreve um item</summary>
		private bool writeLine( HtmlTextWriter writer, ResourceInfo resources, string res )
		{
			int count = resources.getResourceCount(res);
			if( !ShowZeroQuantity && count == 0 ) {
				return false;
			}
			if( ShowImages ) {
				string img = OrionGlobals.getCommonImagePath("resources/" + res + ".gif");
				writer.WriteLine("<td class='resourceCell'><div align='center'><img src='{0}' title='{1}' alt='[{1}]'/></div></td>", img, info.getContent(res));
			}

			if( ShowDocumentation ) {
				writer.WriteLine( "<td class='resourceManagement'><a class='docs' href='{1}?category={2}#{3}'>{0}</a></td>",
					info.getContent(res),
					OrionGlobals.getSectionBaseUrl("docs"),
					resources.Category, res
				);
			} else {
				writer.WriteLine("<td class='resourceManagement'>"+info.getContent(res)+"</td>");
			}
			
			if( ! showOnlyQuantity ) {
				writer.WriteLine("<td class='resourceCell'>{0}%</td>", getRatio(res));
				writeCell( writer, res, getPerTurn(resources.Category, res), "", "+" );
			}

			if( ShowQuantity ) {
				writeCell( writer, res, count, "", "" );
			}

			return true;
		}
		
		/// <summary>Mostra uma tabela com o espao</summary>
		private void writeSpace(HtmlTextWriter writer)
		{
			string space = info.getContent("spaceTitle");
			Planet planet = (Planet) Manager;
			
			writer.WriteLine("<table class='planetFrame' width='100%'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.WriteLine("<td class='resourceTitle' title='{0}'>{0}</td>", space);
			writer.WriteLine("<td class='resourceTitle' title='{0}'><img src='{1}{2}.gif' /></td>", info.getContent("groundSpace"), OrionGlobals.getCommonImagePath("resources/"), "groundSpace");
			writer.WriteLine("<td class='resourceTitle' title='{0}'><img src='{1}{2}.gif' /></td>", info.getContent("waterSpace"), OrionGlobals.getCommonImagePath("resources/"), "waterSpace");
			writer.WriteLine("<td class='resourceTitle' title='{0}'><img src='{1}{2}.gif' /></td>", info.getContent("orbitSpace"), OrionGlobals.getCommonImagePath("resources/"), "orbitSpace");
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resourceCell'>{0}</td>", info.getContent("spaceAvailable"));
			writer.WriteLine("<td class='resourceCell'>{0} / {1}</td>", planet.GroundSpace, planet.TotalGroundSpace);
			writer.WriteLine("<td class='resourceCell'>{0} / {1}</td>", planet.WaterSpace, planet.TotalWaterSpace);
			writer.WriteLine("<td class='resourceCell'>{0} / {1}</td>", planet.OrbitSpace, planet.TotalOrbitSpace);
			writer.WriteLine("</tr>");
			writer.WriteLine("</table>");
		}
		
		#endregion

		#region Utilities

		/// <summary>Escreve uma clula</summary>
		private void writeCell( HtmlTextWriter writer, string res, int mod, string prefix, string prefixIfPositive )
		{
			string css = GetCss(res, mod);
			if( mod < 0 ) {
				writer.WriteLine("<td class='{0}'>{1}</td>", css, prefix+mod);
			} else if( mod > 0 ) {
				writer.WriteLine("<td class='{0}'>{1}</td>", css, prefix+prefixIfPositive+mod);
			} else {
				writer.WriteLine("<td class='{0}'>-</td>", css);
			}
		}
		
		private string GetCss( string res, int mod ) 
		{
			if( mod == 0 ) {
				return "resourceCell";
			}
		
			string css = "resourceCostCellFailed";
			
			if( res == "polution" ){
				if( mod < 0 ) {
					css = "resourceCostCellSucceeded";
				} 
				return css;
			}
			
			if( mod > 0 ) {
				css = "resourceCostCellSucceeded";
			}
			
			return css;
		}

		/// <summary>Indica o ratio de um recurso</summary>
		private int getRatio( string res )
		{
			object obj = manager.getRatio(res);
			if( null != obj ) {
				return (int) obj;
			}
			return 0;
		}

		/// <summary>Indica a quantidade de um recurso por turno</summary>
		private int getPerTurn( string category, string res )
		{
			object obj = manager.getPerTurn(category, res);
			if( null != obj ) {
				return (int) obj;
			}
			return 0;
		}


		#endregion
				
	};
}
