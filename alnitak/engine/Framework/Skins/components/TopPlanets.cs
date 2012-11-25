// created on 2/26/2006 at 11:07 AM

using System;
using System.IO;
using System.Web.UI;
using Chronos.Core;
using Chronos.Messaging;
using Chronos.Utils;

namespace Alnitak {

	public class TopPlanets : Control {	
	
		#region Instance Fields
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		
		#endregion

		#region Control Events

		/// <summary>Prepara o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			OrionGlobals.RegisterRequest(MessageType.Generic, info.getContent("section_topplanets"));
		}
		
		#endregion

		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{	
			if( !Page.User.IsInRole("admin") ) {
				writer.WriteLine("there is no spoon!");
				return;
			}
			
			string option = Page.Request.QueryString["option"];
			if( option == null || ( option != "TopPlanets" && option != "TopRulers" ) ) {
				option = "TopRulers";
			}
			
			Write( writer, string.Format("{0}.aspx.raw", option) );
		}
		
		private void Write( HtmlTextWriter writer, string file )
		{
			file = Path.Combine( Platform.BaseDir, file );
			if( !File.Exists(file) ) {
				writer.WriteLine(CultureModule.getContent("noneAvailable"));
				return;
			}
			
			using( StreamReader reader = new StreamReader(file) ) {
				writer.WriteLine(reader.ReadToEnd());
			}
		}

		#endregion
	
	};
}
