// created on 19-12-2004 at 17:07

using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Language;
using Chronos.Core;
using Chronos.Resources;
using Chronos.Actions;
using Chronos.Utils;

namespace Alnitak {
	
	/// <summary>
	/// Documentation page
	/// </summary>
	public class Docs : Page {
	
		#region Static Members
		
		private static string[] AllowedCategories = new string[] {
			"Building", "Unit", "Research"
		};
		
		#endregion
	
		#region Instance Fields
		
		private string category;
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		
		#endregion
		
		#region Control Events
		
		/// <summary>Inicializa o controlo</summary>
		protected override void OnInit( EventArgs args )
		{
			base.OnInit(args);
			category = null;
		}
		
		/// <summary>Builds up the control</summary>
		protected override void OnLoad( EventArgs e )
		{
			base.OnLoad(e);
			category = Page.Request.QueryString["category"];
		}
		
		#endregion
		
		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			if( invalidArgs() ) {
				showMainMenu(writer);
				return;
			}
			
			showDocs(writer);
		}
		
		/// <summary>Indica se há parâmetros válidos na QueryString</summary>
		private bool invalidArgs()
		{
			if( category == null ) {
				return true;
			}
			
			foreach( string allowed in AllowedCategories ) {
				if ( allowed == category ) {
					return false;
				}
			}
			
			return true;
		}
		
		/// <summary>Mostra o menu que indica todas as categorias possíveis</summary>
		private void showMainMenu( HtmlTextWriter writer )
		{
			OrionGlobals.RegisterRequest( Chronos.Messaging.MessageType.ResearchManagement, CultureModule.getContent("section_docs") );
			
			writer.WriteLine("<ul>");
			foreach( string allowed in AllowedCategories ) {
				writer.WriteLine("<li><a href='{0}?category={1}'>",
						OrionGlobals.getSectionBaseUrl("docs"),
						allowed
					);
				writer.WriteLine(info.getContent(allowed));
				writer.WriteLine("</a> ({0})</li>", getHash(allowed).Count);
				
			}
			writer.WriteLine("</ul>");
		}
		
		/// <summary>Mostra a documentação</summary>
		private void showDocs( HtmlTextWriter writer )
		{
			ResourceBuilder toShow = getHash();
			
			string caption = string.Format("{0} / {1}", CultureModule.getContent("section_docs"), CultureModule.getContent(category));
			OrionGlobals.RegisterRequest( Chronos.Messaging.MessageType.ResearchManagement, caption );
			
			writeIndex( writer, toShow );
			writeEntries( writer, toShow );
		}
		
		/// <summary>Mostra o índice</summary>
		private void writeIndex(HtmlTextWriter writer, ResourceBuilder toShow)
		{
			Ruler current = null;
			if(Context.User.IsInRole("ruler")) {
				User user = (User) Context.User;
				current = Universe.instance.getRuler(user.RulerId);
			}
			
			writer.WriteLine("<a name='TOP'></a>");
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>", info.getContent(category));
			writer.WriteLine("<table class='planetFrame'>");
			foreach( ResourceFactory factory in toShow.Values ) {
				writer.WriteLine("<tr>");
				if( current != null && factory.Category == "Research") {
					string img = "no.gif";
					if(current.isResourceAvailable(category, factory.Name) ){
						img = "yes.gif";
					}
					writer.WriteLine("<td><img src='{0}' /></td>", OrionGlobals.getCommonImagePath(img));
				}
				writer.WriteLine("<td class='resource'><a href='#{0}' class='note'>{1}</a></td>",
						factory.Name, info.getContent(factory.Name),
						info.getContent(factory.Name + "_description")
					);
				writer.WriteLine("<td class='resourceCell'>{0}</td>",
						info.getContent(factory.Name + "_description")
					);
				writer.WriteLine("</tr>");
			}
			writer.WriteLine("</table>");
		}
		
		/// <summary>Mostra todos os recursos</summary>
		private void writeEntries( HtmlTextWriter writer, ResourceBuilder toShow )
		{
			foreach( ResourceFactory factory in toShow.Values ) {
				
				writer.WriteLine("<a name='{0}'></a>", factory.Name);
				writer.WriteLine("<div class='planetInfoZoneTitle'><a href='#TOP'>^</a> <b>{0}</b></div>", info.getContent(factory.Name));
				writer.WriteLine("<table class='planetFrame'>");
				
				writer.WriteLine("<tr>");
				writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("description"));
				writer.WriteLine("<td class='resourceCell'>{0}</td>", info.getContent(factory.Name + "_description"));
				writer.WriteLine("</tr>");
				
				writeActions(writer, "dependencies", factory.Dependencies);
				writeActions(writer, "onavailable", factory.OnAvailableActions);
				writeActions(writer, "cost", factory.CostActions);
				writeDuration(writer, factory );
				writeActions(writer, "onturn", factory.OnTurnActions);
				writeActions(writer, "oncomplete", factory.OnCompleteActions);
				writeActions(writer, "onremove", factory.OnRemoveActions);
				writeActions(writer, "onCancelDuringBuild", factory.OnCancelDuringBuild);
				writeModifiers(writer, "modifiers", factory.Modifiers);
				writeAtts(writer, "attributes", factory.Attributes);

				
				writer.WriteLine("</table>");
			}
		}
		
		/// <summary>Escreve atributos numa hash</summary>
		private void writeDuration( HtmlTextWriter writer, ResourceFactory factory )
		{
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("duration"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", factory.Duration.Value);
			writer.WriteLine("</tr>");
		}

		/// <summary>Escreve modificadores duma hash</summary>
		private void writeModifiers( HtmlTextWriter writer, string title, Hashtable hash )
		{
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent(title));
			if( hash == null || hash.Count == 0 ) {
				writer.WriteLine("<td class='resourceCell'>-</td></tr>");
				return;
			}
			string dependencies = "";
			IDictionaryEnumerator it = hash.GetEnumerator();
			while( it.MoveNext() ) {
				string perturn = info.getContent("turnIncome");
				string caption = info.getContent(it.Key.ToString());
				int v = (int) it.Value;
				string val = (v<0?v.ToString():"+"+v.ToString());
				dependencies += string.Format("{0}: {1} {2}<br />", caption, val, perturn);
				
			}
			writer.WriteLine("<td class='resourceCell'>{0}</td>", dependencies);
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Escreve atributos duma hash</summary>
		private void writeAtts( HtmlTextWriter writer, string title, Hashtable hash )
		{
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent(title));
			if( hash == null || hash.Count == 0 ) {
				writer.WriteLine("<td class='resourceCell'>-</td></tr>");
				return;
			}
			string dependencies = "";
			IDictionaryEnumerator it = hash.GetEnumerator();
			while( it.MoveNext() ) {
				string caption = info.getContent(it.Key.ToString());
				dependencies += string.Format("{0}: {1}<br />", caption, it.Value);
			}
			writer.WriteLine("<td class='resourceCell'>{0}</td>", dependencies);
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Escreve as dependências</summary>
		private void writeActions( HtmlTextWriter writer, string title, Action[] actions )
		{
			writer.WriteLine("<tr>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent(title));
			if( actions == null ) {
				writer.WriteLine("<td class='resourceCell'>-</td></tr>");
				return;
			}
			string dependencies = "";
			foreach( Action action in actions ) {
				dependencies += string.Format("{0}<br />",
						string.Format(info.getContent(action.Name), getParams(action.Params))
					);
			}
			writer.WriteLine("<td class='resourceCell'>{0}</td>", dependencies);
			writer.WriteLine("</tr>");
		}
		
		/// <summary>Retorna a Hash a mostrar</summary>
		private ResourceBuilder getHash()
		{
			return getHash(category);
		}
		
		/// <summary>Retorna a Hash a mostrar</summary>
		private ResourceBuilder getHash(string cat)
		{
			Hashtable root = (Hashtable) Universe.factories["ruler"];
			if( root != null && root.ContainsKey(cat) ) {
				return (ResourceBuilder) root[cat];
			}
			
			root = (Hashtable) Universe.factories["planet"];
			if( root != null && root.ContainsKey(cat) ) {
				return (ResourceBuilder) root[cat];
			}
			
			return null;
		}
		
		/// <summary>Retorna parâmetros internacionalizados</summary>
		private string[] getParams( string[] original )
		{
			string[] result = new string[original.Length];
			for( int i = 0; i < original.Length; ++i ) {
				if( !OrionGlobals.isInt(original[i]) ) {
					result[i] = info.getContent(original[i]);
				} else {
					result[i] = original[i];
				}
			}
			return result;
		}
		
		#endregion
		
	};
}
