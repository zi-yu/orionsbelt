// created on 10-01-2005 at 20:42

using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Chronos.Core;
using Chronos.Queue;
using Chronos.Resources;
using System;

using Alnitak.Exceptions;

namespace Alnitak {

	public class PlanetNavigation : Control {
	
		#region Instance Fields
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		private Ruler ruler;
		private Planet current;
		private string section;
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o Ruler a usar</summary>
		public Ruler Player {
			get { return ruler; }
			set { ruler = value; }
		}
		
		/// <summary>Indica o planeta corrente</summary>
		public Planet Current {
			get { return current; }
			set { current = value; }
		}
		
		/// <summary>Indica a secção a linkar</summary>
		public string Section {
			get { return section; }
			set { section = value; }
		}
		
		#endregion
	
		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			Planet previous = null;
			Planet next = null;
		
			int idx = Player.getIndex(Current);
			previous = getPrevious(idx);
			next = getNext(idx);
			
			writer.WriteLine("<table width='100%' class='nav'>");
			writer.WriteLine("<tr>");
			
			writer.WriteLine("<td align='left' width='33%'>");
			if( next != null ) {
				writer.WriteLine("<a href='{0}?id={1}' title='{2} @ {3}'>&lt;&lt;</a>", OrionGlobals.getSectionBaseUrl(Section), next.Id, next.Name, next.Coordinate.ToString() );
			}
			writer.WriteLine("</td>");
			
			writer.WriteLine("<td align='center' width='33%'>");
			writer.WriteLine("<b>{0}</b> @ {1}", Current.Name, Current.Coordinate.ToString());
			writer.WriteLine("</td>");
			
			writer.WriteLine("<td align='right' width='33%'>");
			if( previous != null ) {
				writer.WriteLine("<a href='{0}?id={1}' title='{2} @ {3}'>&gt;&gt;</a>", OrionGlobals.getSectionBaseUrl(Section), previous.Id, previous.Name, previous.Coordinate.ToString() );
			}
			writer.WriteLine("</td>");
			
			writer.WriteLine("</tr>");
			writer.WriteLine("</table>");
			
			if( Current.IsInBattle ) {
				writer.WriteLine("<div class='errorTitle'>{0}</div>", info.getContent("PlanetInBattle"));
			}
		}
		
		/// <summary>Indica o planeta anterior</summary>
		private Planet getPrevious( int idx )
		{
			Planet toReturn = null;
		
			while( idx > 0 ) {
				toReturn = Player.Planets[idx - 1];
				SubSectionMenu.IsAvailable available = (SubSectionMenu.IsAvailable) SubSectionMenu.Available[Section];
				if( available == null ) {
					throw new Exception("Don't know how to handke '" + Section + "'");
				}
				if( !available(toReturn) ) {
					toReturn = null;
					--idx;
					continue;
				}
				return toReturn;
			}
			
			return toReturn;
		}
		
		/// <summary>Indica o planeta próximo</summary>
		private Planet getNext( int idx )
		{
			Planet toReturn = null;

			while( idx < Player.Planets.Length -1 ) {
				toReturn =  Player.Planets[idx + 1];
				SubSectionMenu.IsAvailable available = (SubSectionMenu.IsAvailable) SubSectionMenu.Available[Section];
				if( available == null ) {
					throw new Exception("Don't know how to handke '" + Section + "'");
				}
				if( !available(toReturn) ) {
					toReturn = null;
					++idx;
					continue;
				}
				return toReturn;
			}
			
			return toReturn;
		}
		
		#endregion
	};
}
