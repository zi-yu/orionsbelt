// created on 10/24/2004 at 12:10 PM

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Resources;
using Chronos.Queue;
using Chronos.Utils;
using Chronos.Info.Results;

namespace Alnitak {

	public class QueueErrorReport : Control {
	
		#region Instance Fields
		
		private Result result;
		
		#endregion
		
		#region Static Members
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		protected string title;
	
		#endregion
		
		#region Ctor
		
		/// <summary>Ctor</summary>
		public QueueErrorReport()
		{
			result = null;
			Title = info.getContent("ResultReport");
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o result set a mostrar</summary>
		public Result ResultSet {
			get { return result; }
			set { result = value; }
		}
		
		/// <summary>Indica o ttulo da caixa de tempo</summary>
		public string Title {
			get { return title; }
			set { title = value; }
		}
		
		#endregion
	
		#region Control Render
		
		protected override void OnPreRender( EventArgs args )
		{
			if( ResultSet == null ) {
				return;
			}

			WriteHeader();
			Information.ShowResult(ResultSet, Title);
		}
		
		/// <summary>Escreve texto antes dos Results</summary>
		protected virtual void WriteHeader()
		{
			return;
		}
		
		#endregion

	};

}
