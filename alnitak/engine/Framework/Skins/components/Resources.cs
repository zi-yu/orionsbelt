// created on 10/23/2004 at 9:46 AM

using System;
using System.Web.UI;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using Chronos.Resources;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Actions;
using Chronos.Info.Results;
using Chronos.Utils;

namespace Alnitak {

	public class Resources : Control {
	
		#region Instance Fields
		
		private string title;
		private ResourceManager manager;
		private string category;
		private string filter;
		private string[] cost;
		private bool showDemolish;
		private bool showQuantity;
		private bool askBuildQuantity;
		private bool showSpaceCost;
		private bool showRareResourceCost;
		private bool showDocumentation;
		private bool showDuration;
		private bool allowKeywords;
		private bool includeOnMouseOver;
		private string command;
		private string postID;
		private string resource;
		private string categoryDesc;
		private bool showImagePreview;
		private int quantity;
		private QueueErrorReport queue;
		private SortedList keywords = null;
		private string tooltip = null;
		
		#endregion
		
		#region Static Members
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		protected string[] DefaultCost = new string[] {
					"gold", "mp", "energy", "labor"
			};
		protected string[] SpaceCost = new string[] {
					"groundSpace", "waterSpace", "orbitSpace"
			};
		
		#endregion
		
		#region Ctor
	
		/// <summary>Ctor</summary>
		public Resources()
		{
			title = string.Empty;
			category = "Building";
			cost = DefaultCost;
			ShowQuantity = true;
			showSpaceCost = false;
			allowKeywords = false;
			ShowDemolish = true;
			showDuration = false;
			askBuildQuantity = false;
			showDocumentation = true;
			ShowRareResourceCost = false;
			command = null;
			postID = null;
			showImagePreview = false;
			resource = null;
			includeOnMouseOver = true;
			categoryDesc = null;
			queue = null;
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
		
		/// <summary>Indica se é para mostrar a duraçãp</summary>
		public bool ShowDuration {
			get { return showDuration; }
			set { showDuration = value; }
		}
		
		/// <summary>Indica se são criados links para a documentação</summary>
		public bool ShowDocumentation {
			get { return showDocumentation; }
			set { showDocumentation = value; }
		}
		
		/// <summary>Indica se é incluído no custo Recursos Raros</summary>
		public bool ShowRareResourceCost {
			get { return showRareResourceCost; }
			set { showRareResourceCost = value; }
		}
		
		/// <summary>Indica se são criados links para a documentação</summary>
		public bool ShowImagePreview {
			get { return showImagePreview; }
			set { showImagePreview = value; }
		}
		
		/// <summary>Indica que recursos de custo são para mostrar</summary>
		/// <remarks>Exemplo: Cost = new string[] { "gold", "waterSpace"}</remarks>
		public string[] Cost {
			get { return cost; }
			set { cost = value; }
		}
		
		/// <summary>Indica se é para mostar o custo</summary>
		public bool ShowSpaceCost {
			get { return showSpaceCost; }
			set { showSpaceCost = value; }
		}
		
		/// <summary>Indica se é para mostar o custo</summary>
		public bool ShowCost {
			get { return cost != null; }
		}
		
		/// <summary>Indica se o botão de demolish é para ser mostrado</summary>
		public bool ShowDemolish {
			get { return showDemolish; }
			set { showDemolish = value; }
		}
		
		/// <summary>Indica se é para mostrar a quantIDade de um recurso</summary>
		public bool ShowQuantity {
			get { return showQuantity; }
			set { showQuantity = value; }
		}
		
		/// <summary>Indica se é para mostrar a quantIDade de um recurso</summary>
		public bool AskBuildQuantity {
			get { return askBuildQuantity; }
			set { askBuildQuantity = value; }
		}
		
		/// <summary>Indica o QueueError a usar</summary>
		public QueueErrorReport QueueError {
			get { return queue; }
			set { queue = value; }
		}
		
		/// <summary>Indica se há filtro por keyword</summary>
		public bool AllowKeywords {
			get { return allowKeywords; }
			set { allowKeywords = value; }
		}
		
		/// <summary>Indica o que mostrar na tooltip</summary>
		public string Tooltip {
			get { return tooltip; }
			set { tooltip = value; }
		}
		
		/// <summary>Indica se  para incluir o efeito de onmouseover</summary>
		public bool IncludeOnMouseOver {
			get { return includeOnMouseOver; }
			set { includeOnMouseOver = value; }
		}
		
		/// <summary>Indica a categoria de tipos de factory a mostrar</summary>
		public string CategoryDescription {
			get { return categoryDesc; }
			set { categoryDesc = value; }
		}
		
		#endregion
		
		#region Control Events
		
		/// <summary>Verifica se há comandos a executar</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			
			command = (string) Page.Request.Form["buildingAction"];
			resource = (string) Page.Request.Form["buildingType"];
			postID = (string) Page.Request.Form["postID"];
			
			string number = (string) Page.Request.Form["buildingQuantity"];
			if( OrionGlobals.isInt(number) ) {
				quantity = int.Parse(number);
			} else {
				quantity = 1;
			}
			
			checkFilter();
			
			checkCommand(Manager);
		}
		
		/// <summary>Verifica o filtro do pedIDo</summary>
		private void checkFilter()
		{
			filter = Page.Request.QueryString["filter"];
			if( filter == null ) {
				filter = "all";
			}
		}
		
		#endregion
		
		#region Control Title Rendering
		
		/// <summary>Escreve o título da tabela</summary>
		private void writeTitle( HtmlTextWriter writer )
		{
			if( ShowImagePreview ) {
				writer.WriteLine("<td class='resourceTitle'>&nbsp;</td>");
			}
			
			if( ShowQuantity ) {
				writer.WriteLine("<td class='resourceTitle' title='{0}'>#</td>", info.getContent("quantidade"));
			}
		
			writer.WriteLine("<td class='resourceTitle'><b>");
			writer.WriteLine( info.getContent("recursos") );
			writer.WriteLine("</b></td>");
			
			if( ShowCost ) {
				writeTitleCost(writer);
			}
			
			if( ShowDuration ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("Duration"));
			}
			
			if( AskBuildQuantity ) {
				writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("quantidade"));
			}
			
			writer.WriteLine("<td class='resourceTitle'><img src='{0}' title='{1}'/></td>", OrionGlobals.getCommonImagePath("addt.gif"), info.getContent("add_title"));
			if( ShowDemolish ) {
				writer.WriteLine("<td class='resourceTitle'><img src='{0}' title='{1}'/></td>", OrionGlobals.getCommonImagePath("removet.gif"), info.getContent("remove_title"));
			}
		}
		
		/// <summary>Escreve o title do custo</summary>
		private void writeTitleCost( HtmlTextWriter writer )
		{
			foreach( string cost in Cost ) {
				string img = OrionGlobals.getCommonImagePath("resources/" + cost + ".gif");
				writer.Write("<td class='resourceTitle'><img src='{0}'title='{1}' alt='{1}'/></td>", img, info.getContent(cost));
			}
			
			if( ShowSpaceCost ) {
				writer.WriteLine("<td class='resourceTitle'><img src='{0}' title='{1}' alt='{1}'/></td>",
					OrionGlobals.getCommonImagePath("resources/SpaceNeeded.gif"), info.getContent("space")
				);
			}
			
			if( ShowRareResourceCost ) {
				writer.WriteLine("<td class='resourceTitle'><img src='{0}' title='{1}' alt='{1}'/></td>",
					OrionGlobals.getCommonImagePath("resources/RareResources.gif"), info.getContent("Rare")
				);
			}
		}
		
		#endregion
		
		#region Control Items Rendering
		
		/// <summary>Pinta o Controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			writeScripts();
			
			if(!AllowKeywords ) {
				writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", Title);
			} else {
				writer.WriteLine("<table class='planetInfoZoneTitle'><tr><td class='planetInfoZoneTitle'><b>{0}</b></td><td><img src='{1}' onmouseover='showHideFilter(this, event);' class='filter' align='right'/></td></tr></table>", Title, OrionGlobals.getCommonImagePath("Filter.gif"));
			}
			
			writeResources(writer);
			if( AllowKeywords ) {
				writeKeywords(writer);
			}
		}
		
		/// <summary>Escreve os recursos</summary>
		private void writeResources( HtmlTextWriter writer )
		{
			ResourceInfo resources = Manager.getResourceInfo(Category);
			ArrayList list = new ArrayList();
			
			foreach( ResourceFactory available in resources.AvailableFactories.Values ) {
				if( CategoryDescription != null && CategoryDescription == available.CategoryDescription ) {
					list.Add(available);
				}
			}
			
			if( list.Count == 0 ) {
				writer.WriteLine("<table class='planetFrame'><tr><td>{0}</td></tr></table>",info.getContent("noneAvailable"));
				return;
			}
			
			writer.WriteLine("<table class='planetFrame'>");
			
			writer.WriteLine("<tr class='resourceTitle'>");
			writeTitle(writer);
			writer.WriteLine("</tr>");
			
			if( AllowKeywords ) {
				keywords = new SortedList();
				keywords.Add("all", "all");
			}
			
			foreach( ResourceFactory available in list ) {
				
				if( AllowKeywords ) {
					if( available.Keywords != null && available.Keywords.Length != 0 ) {
						foreach( string keyword in available.Keywords ) {
							if( !keywords.ContainsKey(keyword) ) {
								keywords.Add(keyword, keyword);
							}
						}
					}
				}
				
				if( filter != "all" && !hasFilter(available) && AllowKeywords ) {
					continue;
				}
				
				if( IncludeOnMouseOver ) {
					writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				} else {
					writer.WriteLine("<tr>");
				}
				
				writeLine(writer, resources, available.Name);
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}
		
		/// <summary>Indica se determinado recurso tem determinado filtro</summary>
		private bool hasFilter( ResourceFactory factory )
		{
			if( factory.Keywords == null ) {
				return false;
			}
			return Array.IndexOf(factory.Keywords, filter) >= 0;
		}
		
		/// <summary>Escreve um item</summary>
		private void writeLine( HtmlTextWriter writer, ResourceInfo resources, string res )
		{
			if( ShowImagePreview ) {
				writer.WriteLine("<td class='resourceManagement'><img src='{0}.gif' style='width:20px;height:20px;'/></td>", OrionGlobals.getCommonImagePath(res.ToLower()));
			}
		
			if( ShowQuantity ) {
				writeQuantity(writer, resources, res);
			}
		
			writer.WriteLine("<td class='resource'>");
			if( ShowDocumentation ) {
				writer.WriteLine( "<a class='docs' href='{1}/{2}.{3}' {4}>{0}</a>",
					info.getContent(res),
					OrionGlobals.getSectionBaseUrl("wiki"),
					resources.Category, res,
					getTooltipText(resources, res)
				);
			} else {
				writer.WriteLine( info.getContent(res) );
			}
			
			writer.WriteLine("</td>");
			
			bool canBuild = resources.canQueue(res, 1).Ok;
			if( ShowCost ) {
				writeCost( writer, resources, res);
			}
			
			if( ShowDuration ) {
				ResourceFactory factory = (ResourceFactory) resources.AvailableFactories[res];
				int duration = QueueItem.BaseDuration(Manager, factory, 1);
				int real = QueueItem.RealDuration(Manager, factory, 1);
				if( duration == real ) {
					writer.WriteLine("<td class='resourceCell'>{0}</td>", factory.Duration.Value);
				} else {
					string css = "green";
					if( real > duration ) {
						css = "red";
					}
					writer.WriteLine("<td class='resourceCell'><span class='{0}'>{1}</span> ({2})</td>", css, real, duration);
				}
			}

			writeBuild( writer, resources, res, canBuild );
			
			if( ShowDemolish ) {
				writeDemolish( writer, res, resources );
			}
		}
		
		/// <summary>Escreve a quantIDade de um recurso</summary>
		private void writeQuantity( HtmlTextWriter writer, ResourceInfo resources, string res )
		{
			writer.Write("<td class='resourceCell'>");
			writer.Write( resources.getResourceCount(res) );
			writer.Write("</td>");
		}
		
		/// <summary>Escreve o custo do recurso</summary>
		private bool writeCost( HtmlTextWriter writer, ResourceInfo resources, string res )
		{
			bool canBuild = true;
		
			foreach( string resourceCost in Cost ) {
				int amount;
				bool available = getResourceCost(resources, res, "Intrinsic", resourceCost, out amount );
				
				string toolColor = null;
				if( available ) {
					toolColor = "resourceCostCellSucceeded";
				} else {
					canBuild = false;
					toolColor = "resourceCostCellFailed";
				}
				
				writer.Write("<td class='{0}'>", toolColor);
				writer.WriteLine( amount );
				writer.Write("</td>");
			}
			
			if( ShowRareResourceCost ) {
				WriteRareResourcesCost(writer, resources, res );
			}
			
			bool found = false;
			
			if( ShowSpaceCost ) {
				foreach( string resourceCost in SpaceCost ) {
					int amount;
					bool available = getResourceCost(resources, res, "Intrinsic", resourceCost, out amount );
					if( amount == 0 ) {
						continue;
					}
					
					found = true;
					
					string toolColor = null;
					if( available ) {
						toolColor = "resourceCostCellSucceeded";
					} else {
						toolColor = "resourceCostCellFailed";
					}
					
					string img = OrionGlobals.getCommonImagePath("resources/" + resourceCost);
					string title = info.getContent(resourceCost);
					
					writer.Write("<td class='{0}'>", toolColor);
					for(int i = 0; i < amount; ++i) {
						writer.WriteLine(" <img src='{0}.gif' class='spaceImage' title='{1}'/>", img, title	);
					}
					writer.Write("</td>");
				}
			}
			
			if( ShowSpaceCost && !found ) {
				writer.Write("<td class='resourceCostCellSucceeded'>&nbsp;</td>");
			}
			
			return canBuild;
		}
		
		/// <summary>Escreve o custo do recurso</summary>
		private bool WriteRareResourcesCost( HtmlTextWriter writer, ResourceInfo resources, string res )
		{
			bool canBuild = true;
			int amount = 0;
			string cost = null;
			bool available = false;
			
			foreach( string resourceCost in Universe.getFactories("planet", "Rare").Keys ) {
				available = getResourceCost(resources, res, "Rare", resourceCost, out amount );
				if( amount > 0 ) {
					cost = resourceCost;
					break;
				}
			}
			
			string toolColor = null;
			if( available ) {
				toolColor = "resourceCostCellSucceeded";
			} else {
				canBuild = false;
				toolColor = "resourceCostCellFailed";
			}
			
			writer.Write("<td class='{0}'>", toolColor);
			if( cost == null ) {
				writer.Write( "&nbsp;");
			} else {
				writer.WriteLine( "<img src='{0}.gif' alt='{1}' title='{1}'/>", OrionGlobals.getCommonImagePath("resources/"+cost), info.getContent(cost));
			}
			writer.Write("</td>");
			
			return canBuild;
		}
		
		private int GetBuildQuantity( ResourceInfo resources, string res )
		{
			int mpNeeded = 1;
			int goldNeeded = 1;
			int energyNeeded = 1;
			
			getResourceCost(resources, res, "Intrinsic", "mp", out mpNeeded  );
			getResourceCost(resources, res, "Intrinsic", "gold", out goldNeeded  );
			getResourceCost(resources, res, "Intrinsic", "energy", out energyNeeded  );
			
			if( mpNeeded == 0 ) mpNeeded = 1;
			if( goldNeeded == 0 ) goldNeeded = 1;
			if( energyNeeded == 0 ) energyNeeded = 1;

			int mpAvailable = Manager.getResourceCount("Intrinsic", "mp");
			int goldAvailable = Manager.getResourceCount("Intrinsic", "gold");
			int energyAvailable = Manager.getResourceCount("Intrinsic", "energy");

			int quantity1 = mpAvailable / mpNeeded;
			int quantity2 = goldAvailable / goldNeeded;
			int quantity3 = energyAvailable / energyNeeded;
			
			int quantity = quantity1 < quantity2 ? quantity1 : quantity2;
			quantity = quantity < quantity3 ? quantity : quantity3;
			
			if( res == "marine" || res == "spy" ) {
				int labor = Manager.getResourceCount("Intrinsic", "labor") / 2;
				if( labor < quantity ) {
					quantity = labor;
				}
			}
			
			if( quantity < 0 ) {
				quantity = 0;
			}
			
			if( ShowRareResourceCost ) {
				int rarequantity = -1;
				foreach( Action action in Universe.getFactory("planet", Category, res ).CostActions ) {
					if( action is ResourceNeeded ) {
						ResourceNeeded resn = (ResourceNeeded) action;
						if( Resource.IsRare(resn.Key) ) {
							rarequantity = Manager.getResourceCount("Rare", resn.Key);
							break;
						}
					}
				}
				if( rarequantity >= 0 && rarequantity < quantity ) {
					quantity = rarequantity;
				}
			}
			
			return quantity;
		}
		
		/// <summary>Escreve o custo do recurso</summary>
		private void writeBuild( HtmlTextWriter writer, ResourceInfo resources, string res, bool canBuild  )
		{
			if ( AskBuildQuantity ) {
				int quantity = GetBuildQuantity(resources, res);
				
				writer.Write("<td class='resourceManagement'>");
				writer.Write("<input style='width:60px;' class='textbox' type='text' name='quantity{0}' id='quantity{0}' value='{1}' />",
						res, quantity
					);
				writer.Write("</td>");
			}
			writer.Write("<td class='resourceManagement'>");
			writer.Write("<a href='javascript:build(\"{0}\", \"{1}\",{2},\"{3}\")'>", res, info.getContent(res), AskBuildQuantity?"-1":"1", ID);
			if( canBuild ) {
				writer.Write("<img src='{0}'/>", OrionGlobals.getCommonImagePath("ok.gif") );
			} else {
				writer.Write("<img src='{0}'/>", OrionGlobals.getCommonImagePath("no.gif") );
			}
			writer.Write("</a>");
			writer.Write("</td>");
		}
		
		/// <summary>Escreve o custo do recurso</summary>
		private void writeDemolish( HtmlTextWriter writer, string res, ResourceInfo resources )
		{
			writer.Write("<td class='resourceManagement'>");
			if( resources.canTake(res, 1) && resources.getResourceCount(res) > 0 ) {
				writer.Write("<a href='javascript:demolish(\"{0}\", \"{1}\",\"{2}\")'>", res, info.getContent(res), ID);
				writer.Write("<img src='{0}'/>", OrionGlobals.getCommonImagePath("remove.gif") );
				writer.Write("</a>");
			}
			writer.Write("</td>");
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Mostra texto de tooltip</summary>
		private string getTooltipText( ResourceInfo resources, string res )
		{
			if( tooltip == null ) {
				return string.Empty;
			}

			ResourceFactory resource = (ResourceFactory) resources.AvailableFactories[res];
			
			Hashtable data = null;
			bool modifiers = tooltip == "modifiers";
			
			if( modifiers ) {
				data = resource.Modifiers;
			} else {
				data = resource.Attributes;
			}
			
			if( data == null ) {
				return string.Empty;
			}
			
			return getTooltipTextHelper( info.getContent(tooltip), data, modifiers );
		}
		
		
		/// <summary>Mostra texto de tooltip</summary>
		private string getTooltipTextHelper( string title, Hashtable data, bool modifier )
		{
			if( data == null || data.Count == 0 ) {
				return string.Empty;
			}
		
			StringWriter writer = new StringWriter();
			writer.Write("title='{0}:", title);
				
			IDictionaryEnumerator it = data.GetEnumerator();
			while( it.MoveNext() ) {
				string text = it.Key.ToString();
				string number = null;
				
				if( modifier ) {
					int ratio = Manager.getRatio(text);
					int val = (int) it.Value;
					number = Manager.calcValue(val, ratio).ToString();
				} else {
					number = it.Value.ToString();
				}
				
				writer.Write(" {0} {1};", number, info.getContent(text));
			}
			
			writer.Write("'");
			return writer.ToString();
		}
		
		/// <summary>Retorna o custo de um recurso num outro recurso</summary>
		private bool getResourceCost( ResourceInfo resources, string originalRes, string costResCategory, string costRes, out int amount )
		{
			ResourceFactory factory = resources.getAvailableFactory(originalRes);
			amount = 0;
		
			if( factory.CostActions == null ) {
				return true;
			}

			foreach( Action action in factory.CostActions ) {
				if( action is ResourceNeeded ) {
					ResourceNeeded needed = (ResourceNeeded) action;
					//Chronos.Utils.Log.log("needed.Value=" + needed.Value + " needed.Key=" +  needed.Key + " res=" + costRes);
					if( needed.Key == costRes ) {
						amount = needed.Quantity;
						return needed.Quantity <= Manager.getResourceCount(costResCategory, costRes);
					}
				} else {
					if( action is ResourceAvailable ) {
						ResourceAvailable avail = (ResourceAvailable) action;
						if( avail.Value == costRes ) {
							amount = avail.Quantity;
							return avail.Quantity <= Manager.getResourceCount(costResCategory, costRes);
						}
					}
				}
			}

			return true;
		}
		
		/// <summary>Escreve o javascript necessário</summary>
		private void writeScripts()
		{
			string functions = @"
				<script language='javascript'>
				
				var theform = document.pageContent;
				
				function changeCost( elem )
				{
					alert(elem);
				}
				
				function demolish( resource, caption, ID )
				{
					var resp = confirm('" + info.getContent("confirmDemolish") + @" ' + caption + '?');
					if( resp == false ) {
						return;
					}
					
				    theform.buildingAction.value = 'demolish';
				    theform.buildingType.value = resource;
					theform.postID.value = ID;
				    theform.submit();
				}
				
				function build( resource, caption, quantity, ID )
				{
					var qt = quantity;
					if( -1 == quantity ) {
						qt = document.getElementById('quantity' + resource).value;
						if( qt == null ) {
							return;
						}
						if( !isPositiveInt(qt) ) {
							alert('" + info.getContent("invalidQuantity") + @"');
							return;
						}
					}

					theform.buildingAction.value = 'build';
				    theform.buildingType.value = resource;
					theform.postID.value = ID;
				    theform.buildingQuantity.value = qt;
				    theform.submit();
				}
		
				</script>
			";
			
			Page.RegisterStartupScript("build-demolish", functions);
			Page.RegisterHiddenField("buildingAction","");
			Page.RegisterHiddenField("buildingType","");
			Page.RegisterHiddenField("buildingQuantity","");
			Page.RegisterHiddenField("postID","");
		}
		
		#endregion
		
		#region Command Region
		
		/// <summary>Verifica se há comandos e executa-os</summary>
		private void checkCommand( ResourceManager planet )
		{
			if( command == null || resource == null || postID == null) {
				return;
			}
			
			if( postID != ID ) {
				return;
			}
			
			if( command == "build" ) {
				build(planet);
			} else if( command == "demolish" ) {
				demolish(planet);
			}
		}
		
		/// <summary>Comando de build de um recurso</summary>
		private void build( ResourceManager planet )
		{
			Result result = planet.canQueue(Category, resource, quantity);
			if( result.Ok ) {
				planet.queue(Category, resource, quantity);
			} else {
				QueueError.ResultSet = result;
			}
		}
		
		/// <summary>Comando de build de um recurso</summary>
		private void demolish( ResourceManager planet )
		{
			if( planet is Planet ) {
				Planet p = (Planet) planet;
				if( p.IsInBattle ) {
					Result result = new Result();
					result.failed( new PlanetInBattle() );
					QueueError.ResultSet = result;
					return;
				}
				if( p.BuilingsDemolished > 2 ) {
					Result result = new Result();
					//result.failed( new CanNotDemolish() );
					QueueError.ResultSet = result;
					return;
				}
				++p.BuilingsDemolished;
			}
			int curr = planet.getResourceCount(Category, resource);
			if( quantity <= curr ) { 
				planet.take(Category, resource, quantity);
			}
		}
		
		#endregion
		
		#region Keywords
		
		/// <summary>Escreve as keywords</summary>
		private void writeKeywords( HtmlTextWriter writer )
		{
			if( keywords == null || keywords.Count == 0 ) {
				return;
			}
			int i = 0;
			bool nextRow = false;
			
			char separator = '?';
			string request = arrangeUrl(Page.Request.RawUrl);
			
			if( request.IndexOf('?') > 0 ) {
				separator = '&';
			}
					
			writer.WriteLine("<div onmouseout='showHideFilter(this, event);' onmouseover='event.cancelBubble = true;' class='filteroff' id='filterTable'>");
			writer.WriteLine("<table class='filter' onmouseover='overFilter();'>");

			IDictionaryEnumerator it = keywords.GetEnumerator();
			while( it.MoveNext() ) {
				string keyword = it.Value.ToString();
				bool newRow = (i % 2) == 0;
				nextRow = (++i % 2) == 0;
			
				if( newRow ) writer.Write("<tr>");
				writer.Write("<td class='resourceCell' ><a href='{1}{3}filter={2}' class='filter'>{0}</a></td>", info.getContent(keyword), request, keyword, separator);
				if( nextRow ) writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table></div>");
		}
		
		/// <summary>Retorna um URL como deve ser</summary>
		private string arrangeUrl( string url )
		{
			int IDx = url.IndexOf("filter");
			if( IDx < 0 ) {
				return url;
			}
			return url.Substring(0,IDx-1);
		}
		
		#endregion

	};
}
