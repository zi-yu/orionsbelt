// created on 5/17/04 at 9:52 a

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {

	public class CurrentTurn : Control {
	
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			writer.Write(Universe.instance.TurnCount);
		}
	
	};
	
}

