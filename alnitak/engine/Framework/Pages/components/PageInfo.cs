namespace Alnitak {
	using System;

	/// <summary>
	/// representa a informao de uma pgina
	/// </summary>
	public class PageInfo {

		#region private members

			private int _pageId;
			private int _pageParentID;
			private string _pageName;
			private string _pageTitle;
			private string _pageSkin;
			private string _pageDescription;
			private string _pageContent;

		#endregion

		#region properties
		
			/// <summary>
			/// retorna o Id da pgina
			/// </summary>
			public int pageId{
				get{return _pageId;}
			}

			/// <summary>
			/// retorna o Id do pai da pgina
			/// </summary>
			public int pageParentID {
				get{return _pageParentID;}
			}

			/// <summary>
			/// retorna o nome da pgina
			/// </summary>
			public string pageTitle{
				get{return _pageTitle;}
			}

			/// <summary>
			/// retorna o nome ttulo da pgina
			/// </summary>
			public string pageName{
				get{return _pageName;}
			}
			/// <summary>
			/// retorna a skin da pgina
			/// </summary>
			public string pageSkin{
				get{return _pageSkin;}
				set{_pageSkin=value;}
			}
			/// <summary>
			/// retorna uma pequena descrio da pgina
			/// </summary>
			public string pageDescription{
				get{return _pageDescription;}
			}

			/// <summary>
			/// retorna o path dentro do assembly para a classe que trata 
			/// do codebehind da pgina
			/// </summary>
			public string pageContent{
				get{return _pageContent;}
			}

		#endregion
		
		public PageInfo(
			int pageId,
			int parentID,
			string pageName,
			string pageTitle,
			string pageSkin,
			string pageDescription,
			string pageContent
		) 
		{
			
			_pageId = pageId;
			_pageParentID = parentID;
			_pageName = pageName;
			_pageTitle = pageTitle;
			_pageSkin = pageSkin;
			_pageDescription = pageDescription;
			_pageContent = pageContent;

		}

	}
}
