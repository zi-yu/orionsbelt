using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Alnitak {

	/// <summary>
	/// responsvel por povoar todas as imagens relativas s seces principais
	/// </summary>
	public class SectionMenu : UserControl {
		
		#region Protected Members

		protected Repeater menu;
		protected bool firstButton = true;
		protected int numberOfSections = 0;

		private static object lockObj = new object();

		#endregion

		#region Private Methods

		private bool checkRole( string role ) {
			return HttpContext.Current.User.IsInRole(role);
		}

		/// <summary>
		/// método que pode ser herdado
		/// </summary>
		protected virtual void repeaterEvent(Object s, RepeaterItemEventArgs e) {
			if( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem ) {
				Image buttonBegin = (Image)e.Item.FindControl("buttonBegin");
				if( buttonBegin != null ) {
					if(firstButton) {
						buttonBegin.Visible=false;
						firstButton = false;
					} else {
						buttonBegin.ImageUrl = OrionGlobals.getSkinImagePath("buttonBegin.gif") ;
					}
				}
			}
		}

		/// <summary>
		/// delegate que vai construir cada um dos items do menu principal
		/// </summary>
		private void repeater_ItemDataBound(Object s, RepeaterItemEventArgs e) {
			repeaterEvent(s,e);
		}

		/// <summary>
		/// obtém o nivel do item do menu
		/// </summary>
		/// <returns></returns>
		private int getMenuLevel(){
			return checkRole("ruler")?2:1;
		}

		/// <summary>
		/// obtm a informao das seces e constroi os objectos que
		/// representam a sua informao no menu (SectionMenuLink)
		/// </summary>
		/// <returns>o contentor com os objectos menuLink que representam a
		/// a informao das seces</returns>
		private ArrayList calculateMotherSections() {
			ArrayList sectionsCollection = ((SectionCollection)SectionUtility.getAllSections()).getOrderedSections();
			ArrayList menuSection = new ArrayList();
			int menuLevel = getMenuLevel();
			
			foreach(SectionInfo sectionInfo in sectionsCollection ) {
				if( !sectionInfo.isVisible )
					continue;
				if( sectionInfo.sectionName.CompareTo("Admin") == 0 )
					if( !checkRole("admin") )
						continue;
				if( sectionInfo.sectionParentId <= menuLevel ) {
					SectionMenuLink link = new SectionMenuLink(sectionInfo.sectionName,sectionInfo.sectionPath);
					menuSection.Add(link);
				}
			}

			return menuSection;
		}

		/// <summary>
		/// obtem uma ArrayList de MenuLink com a informacao de todas
		/// as seccoes
		/// </summary>
		/// <returns>o informacao das seccoes</returns>
		protected ArrayList getMotherSections() {
			ArrayList sections = calculateMotherSections();
			numberOfSections = sections.Count;
			return sections;
		}

		#endregion

		#region overrided methods

		/// <summary>
		/// evento que ocorre antes do render. Prepara as informaes para
		/// o menu ser preenchido
		/// </summary>
		/// <param name="e"></param>
		override protected void OnPreRender(EventArgs e) {
			menu.DataSource = getMotherSections();
			menu.DataBind();  
		}

		protected override void OnLoad(EventArgs e) {
			menu.EnableViewState = true;

			menu.ItemDataBound += new RepeaterItemEventHandler(repeater_ItemDataBound);
			base.OnLoad (e);
		}

		#endregion
	}
}
