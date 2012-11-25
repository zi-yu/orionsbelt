using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.IO;
	
using Alnitak.Exceptions;

namespace Alnitak {

	/// <summary>
	/// Base de todos os controls
	/// </summary>
	public abstract class BaseControl : Control ,INamingContainer {

		#region protected members

		protected string _skinFileName;
		protected string _skinName;
		protected MasterSkinInfo masterSkinInfo;

		#endregion

		#region properties

		public string skinFileName {
			get{ return _skinFileName; }
		}

		public string skinName {
			get{ return _skinName; }
		}

		#endregion

		#region overrided methods

		/// <summary>
		/// mtodo de WebControl chamado para criar os filhos do
		/// control, neste caso, carrega o conteudo do control
		/// </summary>
		protected override void OnInit(EventArgs e) {
			
			// carregar a skin
			Control skin = loadSkin();
			Controls.Add(skin);

			// Inicializar a  skin
			initializeSkin(skin);

			base.OnInit (e);
		}

		#endregion

		#region private methods
	
		private Control loadSkin(){
			Control skin = (Control)Context.Cache[masterSkinInfo.masterSkinName+ "/"+ _skinName];
            if (skin == null) {
				string skinPath = OrionGlobals.AppPath + masterSkinInfo.masterSkinName + "/controls/" + _skinFileName;
				if( !File.Exists( HttpContext.Current.Server.MapPath( skinPath ) ) ) {
					MasterSkinInfo m = MasterSkinUtility.getDefaultMasterSkinInfo();
					skinPath = OrionGlobals.AppPath + m.masterSkinName + "/controls/" + _skinFileName;
				}
				skin = Page.LoadControl( skinPath );
				Context.Cache[ masterSkinInfo.masterSkinName + "/"+ _skinName] = skin;
			}
			return skin;
		}
	
		#endregion

		#region public methods

		/// <summary>
		/// verifica se a skin tem um determinado controlo
		/// </summary>
		/// <param name="skin">a skin corrente</param>
		/// <param name="controlName">nome do control a procurar</param>
		public bool hasControl(Control skin, string controlName) {
			return skin.FindControl(controlName) != null;
		}

		/// <summary>
		/// mtodo para obter o control que existe na skin que estamos
		/// a carregar
		/// </summary>
		/// <param name="skin">a skin corrente</param>
		/// <param name="controlName">nome do control a procurar</param>
		/// <returns>o control que est associado ao nome</returns>
		public Control getControl(Control skin, string controlName) {
			Control ctrl = skin.FindControl(controlName);
			if (ctrl == null)
				throw new AlnitakException( "Nao foi possivel encontrar o controlo "+ controlName +" na skin "+ skin.ToString() );
			return ctrl;
		}
		
		#endregion

		#region abstract methods

		/// <summary>
		/// mtodo abstracto para a classe derivada inicializar a skin
		/// </summary>
		/// <param name="skin"></param>
		protected abstract void initializeSkin(Control skin);

		#endregion

		public BaseControl() {
			masterSkinInfo = (MasterSkinInfo)Context.Items["MasterSkinInfo"];
		}
		
		public string ResolveImgUrl( string img )
		{
			return ResolveImgUrl(masterSkinInfo.masterSkinName, img);
		}
		
		public static string ResolveImgUrl( string skin, string img )
		{
			return OrionGlobals.getSkinImagePath(img);
		}
	}
}
