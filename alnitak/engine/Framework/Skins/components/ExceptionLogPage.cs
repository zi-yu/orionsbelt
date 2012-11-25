namespace Alnitak {

	using System;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.Caching;
	using Language;

	public class ExceptionLogPage : UserControl {

		#region fields

		protected ILanguageInfo info = CultureModule.getLanguage();

		protected Repeater exceptions;
		protected PlaceHolder content;
		protected PlaceHolder noContent;
		protected DateTime time;
		protected Button removeAllException;

		#endregion

		#region events

		protected override void OnLoad(EventArgs e) {
			removeAllException.Click +=new EventHandler(removeAllException_Click);
			base.OnLoad (e);
		}

		
		protected override void OnPreRender(EventArgs e) {

			string id = (string) Page.Request.Form["exception"];
			if( id != null && OrionGlobals.isInt( id ) )
				remove( int.Parse(id) );
			
			ExceptionInfo[] allExceptions = ExceptionLog.load();
			if( allExceptions.Length != 0 ) {
				content.Visible = true;
				noContent.Visible = false;
				removeAllException.Text =  info.getContent("orionsbelterror_removeAll");
				exceptions.DataSource = allExceptions;
				exceptions.DataBind();
			} else {
				content.Visible = false;
				noContent.Visible = true;
			}

			//regista o código javascript
			registerScripts();
		}

		#endregion

		#region private

		/// <summary>
		/// cria e regista o código javascript desta página
		/// </summary>
		private void registerScripts() {
			string script = @"
				<script language='javascript'>					
					var theform = document.pageContent;

					function removeException( id ) {
						var resp = confirm('" + CultureModule.getLanguage().getContent("exceptionLog_removeExp") + @"');
						if( resp == false ) {
							return false;
						}
						
						theform.exception.value = id;
						theform.submit();
					}
				</script>";

			Page.RegisterClientScriptBlock("imagePath", script );
			
			OrionGlobals.registerShowHideScript( Page );

			Page.RegisterHiddenField("exception","");
		}

		/// <summary>
		/// remove a excepção com o identificador id
		/// </summary>
		/// <param name="id">identificador da excepção</param>
		private void remove( int id ) {
			ExceptionLog.remove( id );
		}

		/// <summary>
		/// Botão para remover todas as excepções
		/// </summary>
		private void removeAllException_Click(object sender, EventArgs e) {
			ExceptionLog.removeAll();
		}

		#endregion
	}
}
