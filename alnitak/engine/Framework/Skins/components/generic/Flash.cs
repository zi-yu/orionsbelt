// created on 7/13/2005 at 9:20 AM

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {
	
	public class Flash : Control {
	
		#region Control Fields
		
		private string swfFromWebConfig = null;
		private string swf = null;
		private string quality = "high";
		private string witdh = "120";
		private string height = "240";
		
		#endregion
		
		#region Control Properties
		
		public string SwfFromWebConfig {
			get { return swfFromWebConfig; }
			set { swfFromWebConfig = value; }
		}
		
		public string SwfUrl {
			get { return swf; }
			set { swf = value; }
		}
		
		protected string SwfSource {
			get {
				if( SwfUrl != null ) {
					return SwfUrl;
				}
				
				return OrionGlobals.getConfigurationValue("pub", SwfFromWebConfig); 
			}
		}
		
		public string Width {
			get { return witdh; }
			set { witdh = value;}
		}
		
		public string Height {
			get { return height; }
			set { height = value; }
		}
		
		public string Quality {
			get { return quality; }
			set { quality = value; }
		}
		
		#endregion
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			writer.WriteLine("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0' width='{0}' height='{1}'>",
					Width, Height
				);
			writer.WriteLine("<param name='movie' value='{0}'>", SwfSource);
        	writer.WriteLine("<param name='quality' value='{0}'>", Quality);
			writer.WriteLine("<param name='menu' value='false'>");
        	writer.WriteLine("<embed src='{0}' quality='{1}' pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' width='{2}' height='{3}' menu='false'></embed>",
        			SwfSource, Quality, Width, Height
        		);
    		writer.WriteLine("</object>");
		}
		
	};
	
}

