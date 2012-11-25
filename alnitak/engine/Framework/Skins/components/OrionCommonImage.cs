namespace Alnitak {

	using System;
	using System.Web;
	using System.Web.UI;

	/// <summary>
	/// representa uma imagem
	/// </summary>
	public class OrionCommonImage : OrionImage  {
		
		override public string getImage( string image ) {
			return OrionGlobals.getCommonImagePath( image );
		}
	}
}
