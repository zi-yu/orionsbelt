// created on 8/1/2005 at 9:52 AM

using System.Collections;
using System;
using System.Web;
using System.Web.UI;
using Language;
using Chronos.Core;
using Chronos.Info.Results;

namespace Alnitak {

	/// <summary>Mostra todas as restricções</summ<ry>
	public class CheckRestrictions : QueueErrorReport {
	
		#region Control Events
		
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			try {
			
				User user = (User) Context.User;
				Ruler ruler = Universe.instance.getRuler(user.RulerId);
				Result result = Universe.CheckRestrictions(ruler);
				
				Visible = !result.Ok;
				ResultSet = result; 
				
				if( Visible ) {
					Title = info.getContent("restrictions_title");
				}
			
			} catch( Exception ex ) {
				ExceptionLog.log(ex);
			}
		}
		
		#endregion
		
		#region QueueErrorReport Implementation
		
		/// <summary>Escreve texto antes dos Results</summary>
		protected override void WriteHeader()
		{
			Information.AddInformation( info.getContent("restrictions_header") + string.Format(" <a href='{0}'>{1}</a>", Wiki.GetUrl("Restricoes"), info.getContent("restrictions_link")) );
		}
		
		#endregion
	};

}