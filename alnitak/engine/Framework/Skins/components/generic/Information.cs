using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Chronos.Info.Results;

namespace Alnitak {

	public class Information : Control {

		ArrayList errors = new ArrayList();
		private Result result = null;
		private string title = null;
		private bool _isInformation = false;

		#region Public 
		
		public string Title {
			set { title = value; }
			get {
				if( title != null ) {
					return title;
				}
				return CultureModule.getLanguage().getContent( "Message" );
			}
		}
		
		public Result ResultSet {
			get { return result; }
			set { result = value; }
		}

		public bool IsInformation {
			get { return _isInformation; }
			set { _isInformation = value; }
		}

		public void InsertMessage( string message ) {
			errors.Add( message );
		}

		#endregion

		#region Events

		protected override void Render(HtmlTextWriter writer) {
			if( errors.Count == 0 && ResultSet == null ) {
				this.Visible = false;
				return;
			}	
			writer.WriteLine( "<table id='informationTable'>" );
			writer.WriteLine( "<th colspan='2'><img src='{0}'/>&nbsp;{1}</th>",OrionGlobals.getSkinImagePath( "msg_left.gif" ), Title );
			if( IsInformation ) {
				writer.WriteLine( "<tr><td><img src='{0}'/></td>", OrionGlobals.getCommonImagePath( "information.gif" ) );
			} else {
				writer.WriteLine( "<tr><td><img src='{0}'/></td>", OrionGlobals.getCommonImagePath( "error.gif" ) );
			}
			writer.WriteLine( "<td class='messageContent'>" );

			writer.WriteLine( "<ul class='information'>" );
			if( errors != null && errors.Count > 0 ) {
				WriteErrors(writer);
			}
			if( ResultSet != null ) {
				WriteResult(writer);
			}
			writer.WriteLine( "</ul>" );

			writer.WriteLine( "</td></tr>" );
			writer.WriteLine( "</table><p/>" );

			base.Render (writer);
			errors.Clear();
		}
		
		private void WriteResult( HtmlTextWriter writer )
		{
			foreach( ResultItem item in ResultSet.Failed ) {
				string content = string.Format( CultureModule.getContent(item.Name), getParams(item.Params));
				writer.WriteLine("<li class='failed'>{0}</li>", content);
			}
			
			foreach( ResultItem item in ResultSet.Passed ) {
				string content = string.Format( CultureModule.getContent(item.Name), getParams(item.Params));
				writer.WriteLine("<li class='succeeded'>{0}</li>", content);
			}
		}
		
		private void WriteErrors( HtmlTextWriter writer )
		{
			foreach( object error in errors ) {
				writer.WriteLine( "<li>{0}</li>", error);
			}
		}
		
		/// <summary>Retorna parâmetros internacionalizados</summary>
		private string[] getParams( string[] original )
		{
			string[] result = new string[original.Length];
			for( int i = 0; i < original.Length; ++i ) {
				if( !OrionGlobals.isInt(original[i]) ) {
					result[i] = CultureModule.getContent(original[i], false);
				} else {
					result[i] = original[i];
				}
			}
			return result;
		}

		#endregion

		#region Static 

		public static void InitMessageControls() {
			HttpContext.Current.Items["ErrorControl"] = new Information();
			HttpContext.Current.Items["InformationControl"] = new Information();
		}

		public static void AddInformation( string message ) {
			Information info = (Information) HttpContext.Current.Items["InformationControl"];
			info.IsInformation = true;
			info.InsertMessage( message );
		}

		public static void AddError( string message ) {
			Information info = (Information) HttpContext.Current.Items["ErrorControl"];
			info.InsertMessage( message );
		}

		public static Information GetInformationControl() {
			Information info = GetInformation("InformationControl");
			info.IsInformation = true;
			return info;
		}
		
		public static void ShowResult( Result result, string title ) {
			Information inf = GetInformationControl();
			inf.ResultSet = result;
			inf.Title = title;
			inf.IsInformation = result.Ok;
		}

		public static Information GetErrorControl() {
			return GetInformation("ErrorControl");
		}

		private static Information GetInformation( string name ) {
			Information info = (Information) HttpContext.Current.Items[name];
		/*	if( null == info) {
				info = new Information();
			}*/
			return info;
		}

		#endregion
	}
}
