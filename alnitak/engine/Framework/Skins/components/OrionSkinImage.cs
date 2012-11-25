namespace Alnitak {

	/// <summary>
	/// representa uma imagem
	/// </summary>
	public class OrionSkinImage : OrionImage  {

		override public string getImage( string image ) {
			return OrionGlobals.getSkinImagePath( image );
		}

	}
}
