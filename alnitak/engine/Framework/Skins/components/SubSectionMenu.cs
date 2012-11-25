using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Core;
using Chronos.Resources;

namespace Alnitak {
	/// <summary>
	/// responsvel por povoar todas as imagens relativas s seces principais
	/// </summary>
	public class SubSectionMenu : BaseControl{
		
		#region Static Members
		
		private static Hashtable available = new Hashtable();
		
		public static Hashtable Available {
			get { return available; }
		}
		
		public delegate bool IsAvailable( Planet planet );
		
		static SubSectionMenu()
		{
			available.Add("Scan", new IsAvailable(CanScan) );
			available.Add("ScanOverview", new IsAvailable(CanScan) );
			available.Add("Tele", new IsAvailable(CanTeletransport) );
			available.Add("Fleet", new IsAvailable(CanBuildShips) );
			available.Add("Ships", new IsAvailable(CanBuildShips) );
			available.Add("Buildings", new IsAvailable(AlwaysAvailable) );
			available.Add("Resources", new IsAvailable(AlwaysAvailable) );
			available.Add("Market", new IsAvailable(MarketAvailable) );
			available.Add("Barracks", new IsAvailable(BarracksAvailable) );
		}
		
		#endregion
		
		#region Section Tests
		
		/// <summary>Verifica se um planeta pode fazer scan</summary>
		public static bool CanScan( Planet planet )
		{
			return planet.isResourceAvailable("Building", "CommsSatellite");
		}
		
		/// <summary>Verifica se um planeta pode fazer teletransportes</summary>
		public static bool CanTeletransport( Planet planet )
		{
			return planet.CanTeletransportIntrinsic || planet.CanTeletransportFleets;
		}
		
		/// <summary>Verifica se um planeta pode construir naves</summary>
		public static bool CanBuildShips( Planet planet )
		{
			return planet.getAvailableFactories("Unit").Count != 0;
		}
		
		/// <summary>Retorna sempre true</summary>
		public static bool AlwaysAvailable( Planet planet )
		{
			return true;
		}
		
		/// <summary>Indica se o planeta tem mercado</summary>
		public static bool MarketAvailable( Planet planet )
		{
			return planet.getResourceCount("Building", "Marketplace") > 0;
		}
		
		/// <summary>Indica se o planeta tem mercado</summary>
		public static bool BarracksAvailable( Planet planet )
		{
			return planet.getResourceCount("Building", "Barracks") > 0;
		}
		
		#endregion
		
		#region private members

		private PlaceHolder placeholder;
		private Repeater repeater;
		private SectionInfo currentSectionInfo;
		private object sync = new object();

		#endregion

		#region private methods

		private bool isSectionAvailable( string section ){
			User user = Page.User as User;
			if( user == null || Page.Request.QueryString.Get("id") == null ) {
				return true;
			}

			string url = Page.Request.RawUrl.ToLower();

			Planet planet = Universe.instance.getRuler( user.RulerId ).getPlanet( Int32.Parse( Page.Request.QueryString["id"] ) );
			
			object obj = Available[section];
			if( obj != null ) {
				IsAvailable available = (IsAvailable) obj;
				return available(planet);
			}
			
			if( Page.Request.QueryString.Count != 0 && url.IndexOf("/planet/") != -1 && url.IndexOf("default.aspx") != -1 ) {
			
				string resourceType = OrionGlobals.getConfigurationValue("sectionResourceTypes", section, false);
				if( null == resourceType ) {
					return true;
				}
				if( ! planet.Resources.ContainsKey(resourceType) ) {
					return true;
				}
				ResourceInfo info = planet.getResourceInfo( resourceType );
				return info.AvailableFactories.Count > 0;
			}

			return false;
		}

		private bool isSectionGood( SectionInfo sectionInfo) {
			if( !sectionInfo.isVisible || sectionInfo.sectionParentId == 2 || sectionInfo.sectionParentId == 1 || sectionInfo.sectionParentId == -1 )
				return false;

			if( (sectionInfo.sectionId == 5 && currentSectionInfo.sectionId == 3) )
				return false;

			if( sectionInfo.sectionId == 5 && ( currentSectionInfo.sectionId != 5 && currentSectionInfo.sectionParentId != 5 )  ) {
				return false;
			}
			
			//contemplar o pai contemplar os filhos                                              contemplar os irmos e no ser a seco planetas
			if( sectionInfo.sectionId == currentSectionInfo.sectionParentId || sectionInfo.sectionParentId == currentSectionInfo.sectionId || ( sectionInfo.sectionParentId == currentSectionInfo.sectionParentId && currentSectionInfo.sectionId != 5 ) || currentSectionInfo.sectionId == sectionInfo.sectionId )
				return true;
			
			return false;
		}

		private ArrayList getOrderedSections() {
			lock(sync) {
				ArrayList orderedSections = (ArrayList)Context.Cache["SubSectionsMenu"];
				if( orderedSections == null ){
					orderedSections = ((SectionCollection)SectionUtility.getAllSections()).getOrderedSections();
					Context.Cache["SubSectionsMenu"] = orderedSections;
				}
				return orderedSections;
			}
		}

		/// <summary>
		/// obtem a informao das seces e constroi os objectos que
		/// representam a sua informao no menu (SectionMenuLink)
		/// </summary>
		/// <returns>o contentor com os objectos menuLink que representam a
		/// a informao das seces</returns>
		private ArrayList calculateSubSections() {
			
			ArrayList sectionsCollection = getOrderedSections();
			ArrayList menuSection = new ArrayList();
			foreach(SectionInfo sectionInfo in sectionsCollection ) {
				if( isSectionGood(sectionInfo) && isSectionAvailable(sectionInfo.sectionName)  ) {
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
		private ArrayList getSubSections() {
			ArrayList sections = calculateSubSections();
			return sections;
		}

		#endregion

		#region overrided methods

		/// <summary>
		/// inicializa a skin
		/// </summary>
		/// <param name="skin">o control que representa a skin</param>
		override protected void initializeSkin(Control skin) {

			currentSectionInfo = (SectionInfo)Context.Items["SectionInfo"];

			placeholder = (PlaceHolder)getControl(skin,"placeholder");

			if( currentSectionInfo.isVisible ) {
				repeater = (Repeater)getControl(skin,"menu");
				
				ArrayList subSections = getSubSections();

				if( subSections.Count > 0 ) {
					placeholder.Visible = true;
					repeater.DataSource = subSections;
					repeater.DataBind();
					return;
				}
			}
			placeholder.Visible = false;
		}

		#endregion

		/// <summary>
		/// construtor
		/// </summary>
		public SubSectionMenu(){
			_skinFileName = "subsectionmenu.ascx";
			_skinName = "subsectionmenu";
		}
	}
}

