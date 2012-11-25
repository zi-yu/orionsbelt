using System;
using System.Web;
using System.Web.UI;

using Alnitak.Exceptions;

namespace Alnitak {

	/// <summary>
	/// representa uma imagem
	/// </summary>
	public abstract class OrionImage : UserControl, INamingContainer  {

		#region protected fields

		protected string _image;
		protected string _type = "gif";
		protected bool _random = false;
		protected string _url;
		protected MasterSkinInfo masterSkinInfo = null;
		protected string css;
		protected bool _language = false;

		#endregion

		#region constructors

		public OrionImage() {
			masterSkinInfo = (MasterSkinInfo)Context.Items["MasterSkinInfo"];
			css = null;
		}

		#endregion

		#region properties

		public string Image {
			get { return _image; }
			set { _image = value; }
		}

		public string Type {
			get { return _type; }
			set { _type = value; }
		}

		public bool Random {
			get { return _random; }
			set { _random = value; }
		}
		
		public string Css {
			get { return css; }
			set { css = value; }
		}

		public bool Language {
			get { return _language; }
			set { _language = value; }
		}

		#endregion

		#region events

		protected override void OnInit(EventArgs e) {
			EnableViewState = false;
			base.OnInit (e);
		}

		protected override void Render(HtmlTextWriter writer) {
			if(_image.Length == 0 )
				throw new AlnitakException("Atributo imagem é necessario @ OrionImage::Render ");

			writer.Write("<img src=\"{0}", getImage(_image));
			if( _random ) {
				writer.Write( (int)HttpContext.Current.Session["SkinNumber"] );
			}
			if( _language ) {
				User user = HttpContext.Current.User as User;
				if( user != null ) {
					writer.Write( "_" + user.Lang );	
				} else {
					string lang = CultureModule.RequestLanguage;
					if( lang.Length > 2 )
						lang = lang.Substring( 0, 2 );
					writer.Write( "_" + lang );		
				}
			}

			writer.Write(".{0}\"", _type);
			if( Css != null ) {
				writer.Write("class=\"{0}\"", css);
			}
			writer.Write(" />");
		}

		#endregion

		#region abstract

		public abstract string getImage( string image );

		#endregion
	}
}
