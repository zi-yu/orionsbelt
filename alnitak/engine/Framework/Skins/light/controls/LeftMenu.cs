// created on 3/29/04 at 8:10 a

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Alnitak.Exceptions;

namespace Alnitak {

	public class LeftMenu : UserControl, INamingContainer {
	
		protected Repeater commandMenu;
		protected RoleVisible commandPanel;
	
		protected override void OnLoad( EventArgs e )
		{
			base.OnInit(e);
			if( commandPanel == null ) {
				throw new AlnitakException("Nao foi encontrado o controlo 'commandPannel'");
			}
			commandMenu = (Repeater) commandPanel.FindControl("commandMenu");
			if( commandMenu == null ) {
				throw new AlnitakException("Nao foi encontrado o controlo 'commandMenu' no controlo 'commandPannel'");
			}
		}
		
		protected override void OnPreRender(EventArgs e) 
		{
			base.OnPreRender(e);
			commandMenu.DataSource = SectionUtility.getAllSections();
    		commandMenu.DataBind();
		}
		
		protected string getSectionName( RepeaterItem item )
		{
			return "section_" + ((SectionInfo)((DictionaryEntry)item.DataItem).Value).sectionName.ToLower();
		}
		
		protected string getSectionLink( RepeaterItem item )
		{
			SectionInfo info = (SectionInfo)((DictionaryEntry)item.DataItem).Value;
			return info.sectionPath;
		}
		
		protected string getPageName( RepeaterItem item )
		{
			return ((PageInfo)((DictionaryEntry)item.DataItem).Value).pageName;
		}
		
		protected string getPageLink( RepeaterItem item )
		{
			PageInfo info = (PageInfo)((DictionaryEntry)item.DataItem).Value;
			return info.pageName;
		}

	};

}