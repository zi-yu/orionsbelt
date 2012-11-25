using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Alnitak.Exceptions;
using Chronos.Core;
using Chronos.Utils;

namespace Alnitak {

	/// <summary>
	/// Todas as requests da nossa pgina so reencaminhadas para este default
	/// a classe adiciona o conteudo s pginas
	/// </summary>
	public class OrionDefault : Page {

		#region Instance Fields

		private ReaderWriterLock universeLock;

		#endregion

		#region Control Events
		
		/// <summary>
		/// adiciona o conteudo pagina. Esta  a unica pgina em toda a aplicao
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e) {
			SetResponseHeaders();
			universeLock = null;
			Information.InitMessageControls( );
	
			// escreve o path original
			Context.RewritePath( Path.GetFileName( Request.RawUrl ) );

			if( Context.Session["SkinNumber"] == null ) {
				object number = Context.Items["SkinNumber"];
				if( number == null )
					number = 1;
				Context.Session["SkinNumber"] = number;
			}
 
			// obter a section e page info armazenadas no module
			PageInfo pageInfo = (PageInfo)Context.Items["PageInfo"];
			if( pageInfo == null ) {
				throw new AlnitakException("Context.Items possui a referencia para PageInfo a null @ OrionDefault::OnInit");
			}

			MasterSkinInfo masterSkinInfo = (MasterSkinInfo)Context.Items["MasterSkinInfo"];

			// contruir o header
			StringBuilder objBuilder = new StringBuilder();

			//objBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\">\n");
			//objBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");

			objBuilder.Append( "<html>\n<head>" );
            
			// adicionar o titulo
			objBuilder.AppendFormat( "\n<title>Orion's Belt :: {0}</title>", CultureModule.getLanguage().getContent("section_" + pageInfo.pageName.ToLower()) );
           
			// adicionar o style sheet
			string skinCss = OrionGlobals.resolveBase( masterSkinInfo.masterSkinName + "/" + masterSkinInfo.masterSkinStyle );
			objBuilder.AppendFormat( "\n<link href=\"{0}\" rel=\"stylesheet\" />", skinCss );
			objBuilder.AppendFormat("<link href='{0}' type='image/png' rel='icon'/>", OrionGlobals.getCommonImagePath("orion.gif"));

			objBuilder.AppendFormat( "\n<script src=\"{0}\" type=\"text/javascript\" ></script>", OrionGlobals.resolveBase( "skins/commonScripts/common.js" ) );
			objBuilder.AppendFormat( "\n<script src=\"{0}\" type=\"text/javascript\" ></script>", OrionGlobals.resolveBase( masterSkinInfo.masterSkinName + "/" + masterSkinInfo.masterSkinScript ));

			// Adicionar o header da pagina
			objBuilder.Append( "\n</head>\n<body>\n" );

#if DEBUG
			objBuilder.Append("<div style='color: red; background-color: black; text-align:center; padding: 15px;'>DEBUG MODE</div>");
#endif
			
			Controls.Add( new LiteralControl( objBuilder.ToString() ) );

			
			// just in case...
#if MONO_1_1_9_1
			#warning Mono Hack...
			Context.RewritePath( Context.Request.RawUrl );
#endif
			
			// criar um Form Control
			HtmlForm pageContent = new HtmlForm();
			pageContent.ID = "pageContent";
            
			// load da skin da pgina
			Control pageSkin = LoadControl( OrionGlobals.AppPath + masterSkinInfo.masterSkinName + "/page/baseSkin.ascx" );

			// Add the Page Content Page Part
			PlaceHolder pagePart = (PlaceHolder)pageSkin.FindControl( "content" );
			if (pagePart != null) {
				pagePart.Controls.Add( Information.GetErrorControl() );
				pagePart.Controls.Add( Information.GetInformationControl() );
				Control content = getContent(pageInfo);
				pagePart.Controls.Add( content );
			}
           
			// Add the Page Skin to the Form
			pageContent.Controls.Add( pageSkin );

			//adicionar o conteudo a pagina
			Controls.Add( pageContent );
			
			// introduzir o cdigo restante
			Controls.Add( new LiteralControl( "\n</body>\n</html>" ) );
			
			AddInfo();
		}

		protected override void OnError(EventArgs e) {
            base.OnError(e);
            Log.log("Error...");
			releaseLock();
			
			Exception exp = Server.GetLastError();
			if( exp != null ) {
				if( null != HttpContext.Current.Cache[ OrionGlobals.SessionId + "ExceptionNumber"] ) {
					HttpContext.Current.Response.Redirect( OrionGlobals.resolveBase( OrionGlobals.getConfigurationValue("pagePath","globalError") ) );
				}
				HttpContext.Current.Cache[ OrionGlobals.SessionId + "ExceptionNumber"] = 1;
				ExceptionLog.log( exp );
				HttpContext.Current.Response.Redirect( OrionGlobals.resolveBase("orionsbelterror.aspx") );
			}
			HttpContext.Current.Cache[ OrionGlobals.SessionId + "ExceptionNumber"] = 1;
			exp = Server.GetLastError();
			if( exp == null ) {
				Log.log("\tError: {0}", "null" );
				return;
			} else {
				Log.log("\tError: {0}", exp.ToString() );
			}
			ExceptionLog.log( exp );
			HttpContext.Current.Response.Redirect( OrionGlobals.resolveBase("orionsbelterror.aspx") );
		}

		protected override void OnUnload( EventArgs args )
		{
			releaseLock();
		}
		
		#endregion

		#region Utilities

		/// <summary>Retorna o contedo da pgina</summary>
		private Control getContent( PageInfo pageInfo )
		{
			try {
				Universe.instance.SyncRoot.AcquireReaderLock(10);
				Log.log("Reader Lock acquired");
				universeLock = Universe.instance.SyncRoot;

				Type type = Type.GetType( pageInfo.pageContent, true );
				Control objPageContent = (Control)Activator.CreateInstance(type);
				return objPageContent;

			} catch( ApplicationException ) {
				universeLock = null;
				Control control = LoadControl( OrionGlobals.AppPath + "skins/commonControls/UniverseUnavailable.ascx" );
				return control;
			}
		}

		/// <summary>Faz o Release ao lock do Universo</summary>
		private void releaseLock()
		{
			if( null != universeLock ) {
				Log.log("Reader Lock released");
				universeLock.ReleaseReaderLock();
			}
			universeLock = null;
		}
		
		/// <summary>Mostra informao sobre a aplicao</summary>
		private void AddInfo()
		{
#if DEBUG
			Controls.Add(new LiteralControl(string.Format("<p align='center'>{0}<br/>{1}</p>", OrionGlobals.AlnitakInfo, Platform.ChronosInfo)));
#endif
		}
		
		private void SetResponseHeaders()
		{
			Response.AddHeader("Pragma", "no-cache");
			Response.AddHeader("Cache-Control", "no-cache");
		}

		#endregion

		
	};
}
