namespace Alnitak {
	
	using System;
	
	/// <summary>
	/// representa os dados de uma seco
	/// </summary>
	public class SectionMenuLink {

		#region private members

			private string _menuLink;
			private string _menuFileName;

		#endregion
		
		#region properties

			/// <summary>
			/// devolve o link link da seco
			/// </summary>
			public string menuLink {
				get{ return _menuLink; }
			}

			/// <summary>
			/// devolve o ficheiro de imagem da seco
			/// </summary>
			public string menuFileName {
				get{ return _menuFileName; }
			}

		#endregion

		#region public methods

			/// <summary>
			/// construtor
			/// </summary>
			/// <param name="menuFileName">ficheiro de imagem</param>
			/// <param name="menuLink">link da seco</param>
			public SectionMenuLink(string menuFileName, string menuLink ) {
				_menuFileName = menuFileName;
				_menuLink = menuLink;
			}

		#endregion
	}
}
