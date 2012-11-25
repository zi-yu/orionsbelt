namespace Alnitak {

	using System;
	using System.Web;
	using Chronos.Utils;
		
	/// <summary>
	/// Classe que intercepta todos os pedidos
	/// </summary>
	public class OrionModule : IHttpModule {

		#region inherit methods

			/// <summary>
			/// Mtodo de inicializao herdado de IHttpModule
			/// </summary>
			public void Init(HttpApplication application) {
				application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
				Log.log("Orion Module Init");
			}

			/// <summary>
			/// Metodo de dispose herdado de IHttpModule
			/// </summary>
			public void Dispose() {}
		#endregion

		#region private

			/// <summary>
			/// obtem a informacao sobre a pagina do pedido
			/// </summary>
			/// <param name="sectionInfo">a informao da seco corrente</param>
			/// <param name="requestPath">o path da request</param>
			/// <returns>o objecto PageInfo com a informao da pgina ou ento
			/// <code>null</code> se a pgina no for encontrada</returns>
			private PageInfo getPageInfo(SectionInfo sectionInfo, MasterSkinInfo masterSkinInfo, string requestPath)
			{
				// obter o ficheiro do pedido
				string requestFile = OrionGlobals.getPageName(requestPath);
		     
				// se for uma seco ou subsecco
				if (requestFile.ToLower() == "/default.aspx")
					return new PageInfo(
						-1,
						sectionInfo.sectionParentId,
						sectionInfo.sectionName,
						sectionInfo.sectionTitle,
						masterSkinInfo.masterSkinName,
						sectionInfo.sectionDescription,
						sectionInfo.sectionContent
					);

				PageInfo pageInfo;

				pageInfo = NamedPageUtility.getNamedPageInfo( requestFile.ToLower() );
				if(pageInfo != null)
					return pageInfo;

				// se tudo falhar kaboom, big kaboom
				return null;
			}

		#endregion

		#region private methods
			
		private void storeSkinNumber(int n) {
			HttpContext.Current.Items["SkinNumber"] = OrionGlobals.GenerateRandInt( 1, n );
		}
		
		private string CheckWiki(string path)
		{
			Log.log("Checking wiki... {0}", path);
			
			OrionTopic topic = Wiki.ParseTopic(path);
			if( topic != null ) {
				Log.log("Wiki Request!");
				
				HttpContext.Current.RewritePath(Wiki.Section);
				HttpContext.Current.Items["WikiTopic"] = topic;
				
				Log.log("Original Path: {0}", path);
				Log.log("New Path: {0}", Wiki.Section);
				Log.log("WikiTopic: {0}", HttpContext.Current.Items["WikiTopic"]);

				return Wiki.Section;
			} else {
				if( path.IndexOf("/wiki/default.aspx/") != -1 ) {
					Log.log("Ups, path and url mixed!");
					Log.log("Path: {0}", path);
					path = path.Replace("wiki/default.aspx/","");
					Log.log("Arranged Path: {0}", path);
					HttpContext.Current.RewritePath(path);
				}
			}

			return path;
		}
		
		#endregion

		#region public methods

			private void Application_BeginRequest(Object source, EventArgs e) {

				//obter o HttpContext
				HttpContext Context = HttpContext.Current;

				// verificar o wiki
				string path = CheckWiki(Context.Request.Path);

				// obter os paths do ficheiro e da base
				string requestPath = OrionGlobals.removePathInfo(path);
				string requestBasePath = OrionGlobals.getSectionPath(requestPath);
				Log.log( "Request to: {0}", requestPath );
				
				try {
					
					//Descobrir qual a seco pedida e adiciona-la ao Items
					Log.log("Checking for {0} section... ", requestPath);
					SectionInfo sectionInfo = SectionUtility.getSectionInfoFromFullPath(requestPath);
					if( sectionInfo == null ) {
						Log.log("\tsectionInfo is null");
						//normalmente acontece quando e named page
						sectionInfo = SectionUtility.getSectionInfoFromBasePath(requestBasePath);
					} else {
						Log.log("\tsectionInfo ok");
					}

					//quando  imagem cai aqui ou outra coisa qq que n seja seco
					if (sectionInfo == null) {
						Log.log("\tsectionInfo is null");
						return;
					}

					//adiciona aos items a Items
					Context.Items[ "SectionInfo" ] = sectionInfo;
					Log.log( "\tSectionInfo: {0}", sectionInfo.sectionName );
				
					//Descobrir qual a skin corrente
					MasterSkinInfo masterSkinInfo = MasterSkinUtility.getMasterSkinInfoFromId( sectionInfo.sectionSkinId );
					if( masterSkinInfo == null ) {
						masterSkinInfo = MasterSkinUtility.getMasterSkinInfoFromId( 1 );
					}

					//adiciona o nmero da skin (Caso a skin tenha vrios esquemas de cor)
					storeSkinNumber( masterSkinInfo.masterSkinCount );

					//adiciona aos items a Items
					Context.Items[ "MasterSkinInfo" ] = masterSkinInfo;

					PageInfo pageInfo = getPageInfo( sectionInfo, masterSkinInfo, requestPath );
					if (pageInfo != null)  {
						Context.Items[ "PageInfo" ] = pageInfo;
						Chronos.Utils.Log.log("Request: " + requestPath);
	
						Log.log("Rewriting Path to '{0}'...", OrionGlobals.UrlBasePage);
						if( requestPath.IndexOf(".aspx") >= 0 ) {
							Log.log("\tDone!");
							Context.RewritePath(OrionGlobals.UrlBasePage);
						} else {
							Log.log("\tNot rewrited! '{0}' isn't aspx", requestPath);
						}
					} else {
						Log.log("PageInfo is null for '{0}'", requestPath);
					}

				} catch ( Exception exception ) {
					try {
						ExceptionLog.log( exception );
					} catch ( Exception exp){
						HttpContext.Current.Cache[ OrionGlobals.SessionId +  "AlnitakException"] = new ExceptionInfo( exp ); ;
					}
				
					//mostrar a pgina de erro global
					HttpContext.Current.Server.Transfer( OrionGlobals.resolveBase( OrionGlobals.getConfigurationValue("pagePath","globalError") ) );
				}
				
			}

		#endregion

	}
}
