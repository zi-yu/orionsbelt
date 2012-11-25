// created on 10/20/2004 at 4:52 PM

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Resources;
using Chronos.Queue;
using Chronos.Utils;

namespace Alnitak {

	public class QueueNotifier : Control {
	
		#region Instance Fields
		
		private string title;
		private ResourceManager manager;
		private string category;
		private string cancelTitle;
		private string emptyMessage;
		private string errorMessage;
		private bool showQuantity;
		private bool showDocumentation;
		private bool showProductionFactor;
		private bool showImagePreview;
		
		#endregion
		
		#region Static Members
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		
		#endregion
		
		#region Ctor
	
		/// <summary>Ctor</summary>
		public QueueNotifier()
		{
			title = string.Empty;
			category = string.Empty;
			CancelTitle = info.getContent("cancel");
			EmptyMessage = info.getContent("researchQueueEmpty");
			errorMessage = null;
			showQuantity = true;
			showDocumentation = true;
			ShowProductionFactor = true;
			ShowImagePreview = false;
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
		
		/// <summary>Indica se são criados links para a documentação</summary>
		public bool ShowDocumentation {
			get { return showDocumentation; }
			set { showDocumentation = value; }
		}
		
		/// <summary>Indica a categoria a mostrar</summary>
		public string Category {
			get { return category; }
			set { category = value; }
		}
		
		/// <summary>Indica a string correspondente a Cancelar</summary>
		public string CancelTitle {
			get { return cancelTitle; }
			set { cancelTitle = value; }
		}
		
		/// <summary>
		/// Indica a string correspondente a queue vazio
		/// </summary>
		public string EmptyMessage {
			get { return emptyMessage; }
			set { emptyMessage = string.Format("<table class='planetFrame'><tr><td>{0}</td></tr></table>",value); }
		}

		/// <summary>Indica se a quantidade de recursos vai ser mostrada</summary>
		public bool ShowQuantity {
			get { return showQuantity; }
			set { showQuantity = value; }
		}
		
		/// <summary>Indica se o item em produção aparece com uma imagem</summary>
		public bool ShowImagePreview {
			get { return showImagePreview; }
			set { showImagePreview = value; }
		}
		
		/// <summary>Indica se o factor de produção é mostrado</summary>
		public bool ShowProductionFactor {
			get { return showProductionFactor; }
			set { showProductionFactor = value; }
		}
		
		#endregion
		
		#region Control Events

		/// <summary>Inicializa o controlo</summary>
		protected override void OnInit( EventArgs args )
		{
			base.OnInit(args);
			errorMessage = null;
		}

		/// <summary>Verifica se h comandos para executar</summary>
		protected override void OnLoad( EventArgs args )
		{
			ResourceInfo resource = Manager.getResourceInfo(Category);
			checkEvents(resource);
		}
		
		/// <summary>Verifica se houve eventos</summary>
		private void checkEvents( ResourceInfo resource )
		{
			if( Page.IsPostBack ) {
				string queueStatus = Page.Request.Form["queueStatus"];
				//string action = Page.Request.Form["queueAction"];
				string idx = Page.Request.Form["queueParam"];
				
				if( idx == null || idx == "" ) {
					return;
				}
				
				if( !OrionGlobals.isInt(idx) ) {
					errorMessage = "InvalidQueueRequest";
					Log.log(idx + " is not integer");
					return;
				}
				
				int index = int.Parse(idx);

				string state = (resource.Current != null).ToString() + ";" + resource.QueueCount;
				if( queueStatus != state ) {
					errorMessage = "InvalidQueueState";
					Log.log("Invalid state: " + state + " should be: " + queueStatus);
					return;
				}
				
				if( index < -1 || index >= resource.QueueCount ) {
					errorMessage = "InvalidQueueRequest";
					Log.log("Invalid index: " + index);
					return;
				}
				
				if( index == -1 ) {
					resource.cancel();
				} else {
					resource.dequeue(index);
				}
			}
		}
		
		#endregion
		
		#region Control Rendering
		
		/// <summary>Pinta o Controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b>", Title);
			if( ShowProductionFactor ) {
				double factor = Math.Ceiling(Manager.ProductionFactor * 100);
				string css = "green";
				if( factor > 100 ) {
					css = "red";
				}
				writer.WriteLine(" - {1}: <span class='{2}'>{0}%</span>", factor, info.getContent("ProductionFactor"), css);
			}
			writer.WriteLine("</div>");
			writeQueue(writer);
		}
		
		/// <summary>Escreve a Queue</summary>
		private void writeQueue( HtmlTextWriter writer )
		{
			QueueItem[] items = Manager.getQueueList(Category);
			QueueItem current = Manager.current(Category);
			
			if( current == null  && items.Length == 0 ) {
				writer.WriteLine( EmptyMessage );
				return;
			}

			if( errorMessage != null ) {
				string msg = info.getContent(errorMessage);
				writer.WriteLine("<div class='error'>{0}</div>", msg);
			}
			
			writeScriptBlock( (current!=null).ToString() + ";" + items.Length );
			
			int order = 1;
			ViewState["QueueStatus"] = (current!=null).ToString() + ";" + items.Length;

			if( ShowImagePreview && current != null ) {
				++order;
				writer.WriteLine("<div id='inProductionPreview'>");
				writer.WriteLine("<img src='{0}units/{1}_preview.gif' />", OrionGlobals.getCommonImagePath(), current.FactoryName);
				writer.WriteLine("<ul>");
				writer.WriteLine( "<li>{2}: <a class='docs' href='{1}'>{0}</a></li>",
					info.getContent(current.FactoryName),
					Wiki.GetUrl(current.Factory.Category, current.Factory.Name),
					info.getContent("recurso")
				);
				writer.WriteLine("<li>{0}: {1}</li>", info.getContent("quantidade"), current.Quantity);
				writer.WriteLine("<li>{0}: {1} {2}</li>", info.getContent("conclusao"), current.RemainingTurns, info.getContent("Turns"));
				writer.WriteLine("<li><a href='javascript:dequeue(-1);'>{0}</a></li>", info.getContent("cancel"));
				writer.WriteLine("</ul>");
				writer.WriteLine("</div>");
			}
			
			if( items.Length == 0 && current == null ) {
				return;
			}

			writer.WriteLine("<table class='planetFrame' width='100%'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writeTitle(writer);
			writer.WriteLine("</tr>");

			if( !ShowImagePreview && current != null ) {
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writeItem(-1, order, current, writer, current.RemainingTurns, false);
				writer.WriteLine("</tr>");

				++order;
			}
	
			for( int i = 0; i < items.Length; ++i, ++order ) {
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writeItem(i, order, items[i], writer, QueueItem.RealDuration(Manager, items[i].Factory, items[i].Quantity), true);
				writer.WriteLine("</tr>");
			}
			
			writer.WriteLine("</table>");
		}

		/// <summary>Escreve o ttulo da tabela</summary>
		private void writeTitle(HtmlTextWriter writer )
		{
			writer.WriteLine("<td class='resourceTitle'>#</td>");
			writer.WriteLine("<td class='resourceTitle'>" + info.getContent("recurso") + "</td>");
			if( ShowQuantity ) {
				writer.WriteLine("<td class='resourceTitle'>" + info.getContent("quantidade") + "</td>");
			}
			writer.WriteLine("<td class='resourceTitle'>" + info.getContent("conclusao") + "</td>");
			writer.WriteLine("<td class='resourceTitle'>" + info.getContent("cancel") + "</td>");
		}
		
		/// <summary>Escreve um QueueItem</summary>
		private void writeItem( int idx, int order, QueueItem item, HtmlTextWriter writer, int turns, bool queue )
		{
			writer.WriteLine("<td class='resourceCell'><div align='center'>" + order + "</div></td>");
			
			if( ShowDocumentation ) {
				writer.WriteLine( "<td class='resource'><a class='docs' href='{1}'>{0}</a></td>",
					info.getContent(item.FactoryName),
					Wiki.GetUrl(item.Factory.Category, item.Factory.Name)
				);
			} else {
				writer.WriteLine("<td class='resource'>" + info.getContent(item.FactoryName) + "</td>");
			}

			if( ShowQuantity ) {
				writer.WriteLine("<td class='resourceCell'>" + item.Quantity + "</td>");
			}

			if( !queue ) {
				writer.WriteLine("<td class='resourceCell'>+ " + turns + "</td>");
			} else {
				writer.WriteLine("<td class='resourceCell'>{0} (+{1})</td>", info.getContent("waiting"), turns);
			}
			
			string cross = OrionGlobals.getCommonImagePath("remove.gif");
			string str = "<a href='javascript:dequeue("+idx+")'><img src='"+cross+"' /></a>";
			writer.WriteLine("<td class='resourceManagement'>" + str + "</td>");
		}
		
		/// <summary>Escreve o código Javascript necessário</summary>
		private void writeScriptBlock( string queueStatus )
		{
			string functions = @"
				<script language='javascript'>
				
				var theform = document.pageContent;
				
				function dequeue( idx )
				{
					var resp = confirm('" + info.getContent("confirmDequeue") + @"');
					if( resp == false ) {
						return;
					}
					
				    theform.queueAction.value = 'dequeue';
				    theform.queueParam.value = idx;
				    theform.submit();
				}
		
				</script>
			";
			
			Page.RegisterStartupScript("dequeue", functions);
			Page.RegisterHiddenField("queueAction","");
			Page.RegisterHiddenField("queueParam","");
			Page.RegisterHiddenField("queueStatus", queueStatus);
			
		}
	
		#endregion
	};

}
