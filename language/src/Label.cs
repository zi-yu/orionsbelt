// created on 3/20/04 at 12:15 a

using System;
using System.Web;
using System.Globalization;
using Language.Exceptions;

namespace Language {

	/// <summary>Label que procura o seu conteúdo no LanguageManager</summary>
	public class Label : System.Web.UI.LiteralControl {
	
		private string reference;
		private string target;
		private ILanguageInfo language = null;
		
		/// <summary>Construtor</summary>
		public Label(){
			//EnableViewState=false;
			target = null;
			reference = null;
		}
		
		/// <summary>Modifica/Obtém o target onde esta Label vai buscar o conteúdo</summary>
		public string Target {
			get { return target; }
			set { target = value; }
		}
		
		/// <summary>Modifica/Obtém a referência onde esta Label vai buscar o conteúdo</summary>
		public string Ref {
			get { return reference; }
			set { reference = value;}
		}


		
		/// <summary>Retorna a chave para usar no Items</summary>
		public string CultureString {
			get { return "language.culture"; }
		}
		
		/// <summary>Regista um delegate no evento de Load e PreRender</summary>
		protected override void OnInit( EventArgs e )
		{
			getManager();
			base.OnInit(e);
			PreRender += new EventHandler(this.fetchCaption);
		}
		
		/// <summary>Vai o LanguageInfo deste pedido</summary>
		/// <remarks>Se por ventura a label já tiver caption, não é feito nada.</remarks>
		private void fetchCaption( object src, EventArgs e )
		{
			if( Text != null && Text.Length != 0 ) {
				Page.Trace.Write("Language","@ fetchCaption - Default Text is " + Text);
				return;
			}
			
			if( reference == null ) {
				throw new LanguageException("No reference set");
			}
			
			Text = language.getContent( target, reference );
			//Page.Trace.Write("Language","@ fetchCaption - Caption: "+Text+" Reference: " +Ref);
		}
		
		/// <summary>Retorna o ResourceInfo adquado</summary>
		/// <remarks>
		///	Para saber a cultura, ele vai ao CurrentUICulture.
		/// </remarks>
		private ILanguageInfo getManager()
		{
			if( language != null ) {
				return language;
			}
			
			string locale = CultureInfo.CurrentUICulture.Name;
			
			LanguageManager man = (LanguageManager) Context.Application["LanguageManager"];
			if( man == null ) {
				throw new LanguageException("LanguageManager not found at 'Application'");
			}
			
			return language = man.getLanguageInfo( locale );
		}
	};

}
