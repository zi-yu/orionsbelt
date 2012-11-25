// created on 1/25/2006 at 9:15 AM

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using System;

namespace Alnitak {
	
	public class CheckVacations : Control {
	
		private bool val;
		
		public bool Value {
			get { return val; }
			set { val = value; }
		}
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			Ruler ruler = Universe.instance.getRuler( ((User)Page.User).RulerId );
			if( Value == ruler.InVacation ) {
				base.Render(writer);
			}
		}
		
	};
	
}

