using System;
using System.Web.UI;
using System.Web;
using System.IO;

namespace Alnitak {

	/// <summary>
	/// classe base da lgica associada a cada pgina
	/// </summary>
	/// <remarks> nome do ficheiro  o nome sem extenso</remarks>
	public class BasePageModule : Control {
		
		#region protected fields
		
		protected string _fileName = null;
		protected string _sectionContent = string.Empty;
		protected MasterSkinInfo masterSkinInfo;
		
		#endregion

		#region overrided methods
		
		protected override void OnInit( EventArgs e ) {

			Context.Trace.Write("BasePageModule","OnInit");
			
			Control skin;
	        
			// Determinar a skin pedida
			SectionInfo sectionInfo = (SectionInfo)HttpContext.Current.Items[ "SectionInfo" ];
			PageInfo pageInfo = (PageInfo)HttpContext.Current.Items["PageInfo"];
			masterSkinInfo = (MasterSkinInfo)HttpContext.Current.Items["MasterSkinInfo"];
			
			string skinFileName;
			
			if( _sectionContent != string.Empty && sectionInfo.sectionContent != _sectionContent ) {
				_fileName = "InvalidAccess.ascx";
			} else {
				_fileName = _fileName == null?pageInfo.pageName:_fileName;
			}

			skinFileName = String.Format( "{0}{1}/content/{2}.ascx", OrionGlobals.AppPath, masterSkinInfo.masterSkinName , _fileName );

			if( !File.Exists( HttpContext.Current.Server.MapPath( skinFileName ) ) ) {
				masterSkinInfo = MasterSkinUtility.getDefaultMasterSkinInfo();
				skinFileName = String.Format( "{0}{1}/content/{2}.ascx", OrionGlobals.AppPath, masterSkinInfo.masterSkinName , _fileName );
			}

			Context.Trace.Write("BasePageModule","Loading Module: " + skinFileName);
			try {
				skin = Page.LoadControl(skinFileName);
			} catch( Exception ex ) {
				throw new Exception("Error Loading " + skinFileName + " - " + ex.Message, ex);
			}
			Controls.Add(skin);
		}

		#endregion

		public BasePageModule(){}
	}
}
