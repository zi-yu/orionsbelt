using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;
using Alnitak;

namespace Alnitak {
	
	public class TotalRulers : Control {
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			writer.Write(Universe.instance.rulerCount);
		}
		
	};
	
}

