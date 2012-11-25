// created on 19-12-2004 at 17:07

using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using Language;
using Chronos.Core;
using Chronos.Resources;
using Chronos.Actions;
using Chronos.Utils;
#if WIKI
using FlexWiki;
using System.Text.RegularExpressions;
#endif

namespace Alnitak {
	
	/// <summary>
	/// Documentation page
	/// </summary>
	public class Wiki : Page  {
		
		#region Control Methods

		private string WikiSearch {
			get {
				string search = Page.Request.Form["wiki_search"];
				if( search != null ) {
					return search;
				}
				search = Page.Request.QueryString["wiki_search"];
				return search;
			}
		}

		/// <summary>Mostra o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
#if WIKI
	#if DEBUG
			Check(GetFederation(), "Federation");
	#endif
			if( WikiSearch != null ) {
				WriteSearchResults(writer);
				return;
			}

			string currentTopic = "Orionsbelt.Orionsbelt";

			OrionTopic obj = (OrionTopic) Context.Items["WikiTopic"];
			if( obj != null ) {
				currentTopic = obj.ToString();
			}

			if( obj != null && !obj.Exists ) {
				writer.WriteLine("Lamentamos, mas o tpico {0} ainda no foi adicionado.", obj.ToString());
				ExceptionLog.log("WikiTopic", string.Format("Access detected to WikiTopic '{0}' that does not exist.", obj.ToString()) );
				return;
			}

			FlexWiki.AbsoluteTopicName topic = new FlexWiki.AbsoluteTopicName(currentTopic);

			string display = GetDisplay(topic);
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.ResearchManagement, string.Format("{0} - {1}", CultureModule.getContent("help"), display));

			FlexWiki.Federation fed = GetFederation();

			string formattedBody = fed.GetTopicFormattedContent(topic, false);
			writer.WriteLine("<div id='TopicTip'></div>");
			writer.WriteLine("<div id='wiki'>");
			WriteLocation(writer, topic, display);
			CheckPreviewImage(writer, topic);
			writer.WriteLine("<h1>{0}</h1>", display);
			writer.WriteLine(formattedBody);
			writer.WriteLine("</div>");
#else
			writer.WriteLine("<p>This <b>orionsbelt</b> version was compiled without Wiki support!</p>");
			writer.WriteLine("<p>If this is an online version... then... er... maybe we forgot the wiki, please warn us!</p>");
#endif
		}

		#endregion

		#region Utilities
#if WIKI

		private void CheckPreviewImage( HtmlTextWriter writer, AbsoluteTopicName topic )
		{
			string preview = GetTopicField(topic, "PreviewImage");
			if( preview == null ) {
				return;
			}
			writer.WriteLine("<img src='{0}' class='PreviewImage' />", preview);
		}

		private void Check( object obj, string msg )
		{
			if( obj == null ) {
				throw new Exception(msg +" is null");
			}
		}

		private void WriteSearchResults( HtmlTextWriter writer )
		{
			string search = WikiSearch;
			if (search != null) {
				writer.WriteLine("<p>Referncias Encontradas para <b>{0}</b>:</p>", search);
				TopicName topicName = new AbsoluteTopicName(search);

				if( topicName == null ) {
					OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.ResearchManagement, "Procura sobre " + search);
				} else {
					OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.ResearchManagement, "Procura sobre " + topicName.FormattedName);
				}

				Hashtable searchTopics = new Hashtable();
				ArrayList uniqueNamespaces = new ArrayList(GetFederation().Namespaces);

				foreach (string ns in uniqueNamespaces)	{
					ContentBase cb = GetFederation().ContentBaseForNamespace(ns);
					if (cb == null)
						continue;
					searchTopics[cb] = cb.AllTopics(false);
				}

				foreach (ContentBase cb in searchTopics.Keys) {
					string ns = cb.Namespace;
					
					writer.WriteLine("<ul>");
					foreach (AbsoluteTopicName topic in (ArrayList)(searchTopics[cb])) {
						string s = cb.Read(topic);
						string bodyWithTitle = topic.ToString() + s;
						
						if (Regex.IsMatch(bodyWithTitle, search, RegexOptions.IgnoreCase)) {
							writer.WriteLine("<li>");
							writer.WriteLine("<a title='" + topic.Fullname + "'  href='" + GetUrl(topic) + "'>");
							writer.WriteLine(GetDisplay(topic));
							writer.WriteLine("</a>");			
							writer.WriteLine("</li>");
						}
					}
					writer.WriteLine("</ul>");
				}
			} else {
				writer.WriteLine("Pedido Invlido");
			}
		}

		private string GetSpecificTopic( string str )
		{
			int idx = str.IndexOf('.');
			if( idx < 0 ) {
				return str;
			}
			return str.Substring(idx + 1);
		}

		public string GetTopicField( FlexWiki.AbsoluteTopicName topic, string field  )
		{
			Hashtable hash = GetFederation().ContentBaseForNamespace(topic.Namespace).GetFieldsForTopic(topic);
			if( hash == null ) {
				return null;
			}
			return (string) hash[field];
		}

		public string GetDisplay( FlexWiki.AbsoluteTopicName topic )
		{
			string display = GetTopicField(topic, "Display");
			if( display == null ) {
				return topic.FormattedName;
			}
			return display;
		}
		
		public static FlexWiki.Federation FlexFederation {
			get {
				return GetFederation(); 
			}
		}

		private static FlexWiki.Federation GetFederation()
		{
			object obj = HttpContext.Current.Application["---FEDERATION---"];
			if( obj == null ) {
				string path = GetFederationFile();
				FlexWiki.LinkMaker lm = new FlexWiki.LinkMaker(string.Format("{0}wiki/", OrionGlobals.AlnitakUrl));
				FlexWiki.Federation fed = new FlexWiki.Federation(path, FlexWiki.Formatting.OutputFormat.HTML, lm);
				HttpContext.Current.Application["---FEDERATION---"] = fed;
				return fed;
			}

			return (FlexWiki.Federation) obj;
		}

		private static string GetFederationFile()
		{
			string baseDir = Chronos.Utils.Platform.BaseDir;
			baseDir = Path.Combine(baseDir, "wiki");
			return Path.Combine(baseDir, "WikiBases/NamespaceMap.xml");
		}

		private void WriteLocation( HtmlTextWriter writer, FlexWiki.AbsoluteTopicName topic, string display )
		{
			writer.WriteLine("<div id='wiki_nav'>");
			writer.WriteLine("<div id='wiki_search'><input type='text' name='wiki_search' value='' /> <input type='submit' value='Procurar' /></div>");
			writer.WriteLine("<a href='{0}'>Manual</a>", OrionGlobals.getSectionBaseUrl("wiki"));

			string parent = GetTopicField(topic, "Parent");
			if( parent != null ) {
				FlexWiki.AbsoluteTopicName parentTopic = new FlexWiki.AbsoluteTopicName(parent);
				writer.WriteLine(" &raquo; <a href='{0}'>{1}</a>", GetUrl(parentTopic), GetDisplay(parentTopic));	
			}
			
			if( topic.Fullname != "Orionsbelt.Orionsbelt" ) {
				writer.WriteLine(" &raquo; <a href='{0}'>{1}</a>", GetUrl(topic), display);
			}
			writer.WriteLine("</div>");
		}

		public static string GetUrl(FlexWiki.AbsoluteTopicName topic)
		{
			return GetUrl( topic.Namespace, topic.Name );
		}

#endif
		#endregion
		
		#region Static Utilities
		
		/// <summary>Indica o URL base da seco Wiki</summary>
		public static string Section 
		{
			get { return string.Format("{0}wiki/default.aspx", OrionGlobals.InternalAppPath); }
		}
		
		/// <summary>Dado um Url, retorna um OrionTopic</summary>
		public static OrionTopic ParseTopic( string path )
		{
			if( path.ToLower().IndexOf("/wiki/default.aspx/") != -1 ) {
				
				int idx = path.LastIndexOf("/");
				string topic = path.Substring( idx  + 1, path.Length - idx -1 );

				if( topic.IndexOf(".aspx") != -1 ) {
					return null;
				}

				// caso dos tpicos que ainda no existem
				idx = topic.IndexOf(".html");
				if( idx != -1 ) {
					topic = topic.Substring(0, idx);
					return new OrionTopic(topic, false);
				}
				
				return new OrionTopic(topic);
			}
			
			return null;
		}
		
		/// <summary>Dada um tema, indica o URL</summary>
		public static string GetUrl( string topic )
		{
			return GetUrl("Orionsbelt", topic);
		}

		public static string GetUrl( string nameSpace, string topic )
		{
			return OrionGlobals.resolveBase( string.Format("wiki/default.aspx/{0}.{1}", nameSpace, topic));
		}
		
		public static ArrayList GetTopicSpacedLines( string topicName )
		{
#if WIKI
			AbsoluteTopicName topic = new AbsoluteTopicName(topicName);
			ContentBase cb = GetFederation().ContentBaseForNamespace(topic.Namespace);
			
			using (TextReader sr = cb.TextReaderForTopic(topic)) 	{
				ArrayList lines = new ArrayList();
				string line = null;
				while( (line = sr.ReadLine()) != null ) {
					if( line.StartsWith(" ") ) 	{
						lines.Add(line.Trim());
					}
				}
				return lines;
			}
#else
		return new ArrayList();
#endif
		}
		
		public static Hashtable GetProductsFromWiki()
		{
			Hashtable hash = (Hashtable) HttpContext.Current.Cache["ShopItems"];
			if( hash == null ) {
				hash = RawGetProductsFromWiki();
				HttpContext.Current.Cache["ShopItems"] = hash;
			}
			return hash;
		}
		
		public static Hashtable RawGetProductsFromWiki()
		{
#if WIKI
			string topicName = "Orionsbelt.Shop"; 
			AbsoluteTopicName topic = new AbsoluteTopicName(topicName);
			ContentBase cb = GetFederation().ContentBaseForNamespace(topic.Namespace);
			
			Hashtable categories = new Hashtable();
			ArrayList current = null;
			using (TextReader sr = cb.TextReaderForTopic(topic)) 	{
				string line = null;
				while( (line = sr.ReadLine()) != null ) {
					if( line.StartsWith("!!!") ) 	{
						current = new ArrayList();
						string category = line.Replace("!!!","").Trim();
						categories.Add( category, current );
					}
					if( line.StartsWith("||") ) {
						line = line.Replace("||", "|");
						string[] parts = line.Split('|');
						current.Add( new ShopItem(parts[1], parts[2], parts[3] ) );
					}
				}
				return categories;
			}
#else
			return new Hashtable();
#endif
		}
		
		#endregion
		
	};
}
