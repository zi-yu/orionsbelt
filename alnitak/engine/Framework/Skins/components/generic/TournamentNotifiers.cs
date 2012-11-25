// created on 12/13/2005 at 5:00 PM

using System.Collections;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using Chronos.Tournaments;
using System;

namespace Alnitak {
	
	public class TournamentNotifier : Control {
	
		private string type = "--asd--asd-asd-as-da-sd-as-das";
		
		public string Type {
			get { return type; }
			set { type = value; }
		}
		
		/// <summary>Escreve este controlo em html</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			Tournament tour = (Tournament) Universe.instance.PersistenceServices.GetState(Type);
			if( tour == null || tour.State != TournamentState.Subscriptions ) {
				return;
			}
			
			writer.WriteLine("<img src='{0}' />", OrionGlobals.getCommonImagePath("yes.gif"));
		}
		
	};
	
}

