namespace Alnitak {
	
	using System;
	using System.Web;
	using System.Configuration;
	using System.Collections.Specialized;
	using System.Globalization;
	using System.Reflection;
	using System.IO;
	using System.Security.Cryptography;

	using Alnitak.Exceptions;
	using Chronos.Core;
	using Chronos.Info;
	using Chronos.Utils;
	using Chronos.Battle;
	using Chronos.Persistence;

	using System.Text.RegularExpressions;

	/// <summary>
	/// Summary description for AlnitakGlobals.
	/// </summary>
	public class OrionGlobals {

		public static string getConfigurationValue(string name,string key){
			return getConfigurationValue(name, key, true);
		}

		public static string getConfigurationValue(string name,string key, bool throwIfNotFound){
			NameValueCollection nvc = (NameValueCollection)
					ConfigurationSettings.GetConfig("OrionGroup/" + name);
					
			if( throwIfNotFound && nvc == null ) {
				throw new AlnitakException(string.Format("Seccao {0} nao existe!", name));
			}
					
			string value = nvc[key];

			if( throwIfNotFound && (value == null || value.Length == 0) ) {
				throw new AlnitakException( string.Format("Seccao {0} do ficheiro Web.Config nao possui a chave {1}",name,key) );
			}
			return value;
		}
		
		/// <summary>Retorna uma chave para o Web.config que ter um nome de uma classe</summary>
		public static string resolveDataAccessName( string name )
		{
			#if PERSIST_TO_MYSQL
				return name +"-mySql";
			#elif PERSIST_TO_SQLSERVER
				return name + "-sqlServer";
			#elif PERSIST_TO_POSTGRE
				return name + "-postgre";
			#else
				#error No DataAccess compilation Macro Found!
			#endif
		}
		
		/// <summary>Obtm parmetros de persistncia do Chronos</summary>
		public static PersistenceParameters Persistence {
			get {
				PersistenceParameters param = new PersistenceParameters();
				param.Register( "FilePersistence", Path.Combine(Platform.BaseDir, "backups/Universe.bin") );
				param.Register( "ConnectionString", getConnectionString("connectionString") );
				param.Register( "ConnectionStringPG", getConnectionString("connectionStringPG") );
				param.Register( "Path", Path.Combine(Platform.BaseDir,"backups/") );
				return param;
			}
		}
		
		#region Images

		/// <summary>
		/// responsvel por gerar o path das imagens
		/// </summary>
		/// <returns>o path das imagens</returns>
		public static string getSkinImagePath( string image ){
			MasterSkinInfo masterSkinInfo = (MasterSkinInfo)HttpContext.Current.Items["MasterSkinInfo"];
			return ImagePath + masterSkinInfo.masterSkinName + "/images/" + image;
		}

        /// <summary>
		/// responsvel por gerar o path das imagens comuns
		/// </summary>
		/// <returns>o path das imagens</returns>
		public static string getCommonImagePath( string image ){
			return ImagePath + "skins/commonImages/" + image;
		}
		
		/// <summary>
		/// Retorna o path das common images
		/// </summary>
		/// <returns>o path das imagens</returns>
		public static string getCommonImagePath(){
			return ImagePath + "skins/commonImages/";
		}

		public static string CurrentSkinName {
			get {
				MasterSkinInfo skin = (MasterSkinInfo)HttpContext.Current.Items["MasterSkinInfo"];
				return skin.masterSkinName;
			}
		}

		public static string ForumSkinName {
			get {
				return CurrentSkinName + "Forum";
			}
		}

		#endregion
		
		/// <summary>Indica uma imagem com base num bool</summary>
		public static string YesNoImage( bool b )
		{
			string str = "no.gif";
			if(b) {
				str = "yes.gif";
			}
			return OrionGlobals.getCommonImagePath(str);
		}
		
		/// <summary>
		/// descobre qual a cultura em que a pgina est a correr.Por
		/// exemplo em Portugal ir retornar "pt", nos EUA retornar
		/// "en" etc...
		/// </summary>
		/// <returns>a cultura</returns>
		public static string getCulture(){
			HttpContext context = HttpContext.Current;

			User user = context.User as User;


			if(	user != null ) {
				return user.Lang.Substring(0,2).ToLower();
			}else{
				CultureInfo cultureInfo = CultureInfo.CurrentCulture;
				string name = cultureInfo.Name;
				//return name;
				return name.Substring(0,2).ToLower();
			}
       	}

		/// <summary>
		/// retorna o Url relativo  base da aplicao
		/// </summary>
		/// <param name="relativePath">o path do ficheiro que se est a carregar</param>
		/// <returns>o path relativo  base</returns>
		public static string resolveBase(string relativePath) {
			return AppPath + relativePath;
		}
		
		/// <summary>
		/// retorna o url da pgina principal de cada seco (default.aspx)
		/// ou ento da pgina do pedido (caso n seja seco)
		/// </summary>
		public static string UrlBasePage {
			get {
				NameValueCollection nvc = (NameValueCollection)
					ConfigurationSettings.GetConfig("OrionGroup/pagePath");
				Log.log("Getting UrlBasePage: {0}/{1}", InternalAppPath, nvc[ "basePage" ]);
				return InternalAppPath + nvc[ "basePage" ];
			}
		}

		/// <summary>
		/// mtodo que limpa todo o url do pedido excepto o nome do ficheiro
		/// </summary>
		/// <param name="requestedPath">o path do pedido</param>
		/// <returns>o nome do ficheiro</returns>
		public static string getPageName(string requestedPath) {
			/*// remover a query string
			if ( requestPath.IndexOf( '?' ) != -1 )
				requestPath = requestPath.Substring( 0, requestPath.IndexOf( '?' ) );
			*/

			// Remove tudo menos o nome da pgina
			return requestedPath.Remove( 0, requestedPath.LastIndexOf( "/" ) );
		}

		public static string getQueryString() {
			string url = HttpContext.Current.Request.RawUrl;

			// remover a query string
			if ( url.IndexOf( '?' ) != -1 )
				url = url.Substring( url.IndexOf( '?' ) );
			else
				url = "";

			return url;
		}


		/// <summary>
		/// obtm a <code>string</code> de conexo  base de dados
		/// </summary>
		/// <returns>string de conexo</returns>
		public static string getConnectionString( string key ) {
			NameValueCollection nvc = (NameValueCollection)
				ConfigurationSettings.GetConfig("OrionGroup/database");
			return nvc[key];
		}
		
		/// <summary>Obtm a connection string</summary>
		public static string getConnectionString()
		{
			#if PERSIST_TO_MYSQL
			return getConnectionString("connectiostring-mysql");
			#elif PERSIST_TO_SQLSERVER
			return getConnectionString("connectionString");
			#elif PERSIST_TO_POSTGRE
			return getConnectionString("connectionStringPG");
			#else
			#error No DataAccess compilation Macro Found!
			#endif
		}
		
		/// <summary>
		/// remove informao auxiliar do path pedido (aquela que vem a seguir
		/// ao nome do ficheiro por exemplo)
		/// </summary>
		/// <param name="requestedPath">path da request</param>
		/// <returns></returns>
		public static string removePathInfo(string requestedPath) {
			string pathInfo = HttpContext.Current.Request.PathInfo;
			if ( pathInfo.Length == 0 || pathInfo == requestedPath ) {
				return requestedPath;
			}
			
			return requestedPath.Replace(pathInfo, "");
		}

		/// <summary>
		/// Path base do pedido
		/// </summary>
		/// <param name="requestedPath">path da request</param>
		public static string getSectionPath(string requestedPath){
			int slashIndex = requestedPath.LastIndexOf("/");
            if (slashIndex < requestedPath.Length)
                requestedPath = requestedPath.Remove( slashIndex + 1, requestedPath.Length - (slashIndex + 1) );
            
			return requestedPath;
		}

		public static string calculatePath( string page ) {
			string currentPath = getSectionPath( HttpContext.Current.Request.RawUrl );
			return currentPath  + page;
		}

		public static void forceLogin(){
			HttpContext context = HttpContext.Current;
			string url = context.Request.RawUrl.Replace(InternalAppPath, AppPath);
			context.Response.Redirect( String.Format( "{0}?ReturnUrl={1}","login.aspx", context.Server.UrlEncode( url ) ) );
		}
		
		/// <summary>Obtm o OrionRequestManager da sesso corrente</summary>
		public static OrionRequestManager RequestManager {
			get {
				return (OrionRequestManager) HttpContext.Current.Session["OrionRequestManager"];
			}
		}
		
		/// <summary>Regista um pedido no RequestManager</summary>
		public static void RegisterRequest( Chronos.Messaging.MessageType type, string caption )
		{
			RequestManager.Register(type, HttpContext.Current.Request.RawUrl, caption);
		}
		
		/// <summary>
		/// retorna o path da aplicao
		/// </summary>
		public static string AppPath {
			get {
				if (HttpContext.Current.Request.ApplicationPath == "/" || HttpContext.Current.Request.Headers["X-Forwarded-Host"] != null )
					return "/";
				return HttpContext.Current.Request.ApplicationPath + "/";
			}
		}
		
		public static string InternalAppPath {
			get {
				if (HttpContext.Current.Request.ApplicationPath == "/" )
					return "/";
				return HttpContext.Current.Request.ApplicationPath + "/";
			}
		}

		/// <summary>
		/// retorna o path da aplicao
		/// </summary>
		public static string ImagePath {
			get {
				User user = HttpContext.Current.User as User;
				if( null != user ) {
					if( user.HasImagesDir )
						return "file:///" + user.ImagesDir;
				}
				/*if (HttpContext.Current.Request.ApplicationPath == "/")
					return "/";*/
/*#if DEBUG && !NANT

				string path = HttpContext.Current.Request.ApplicationPath;
				if( path[path.Length-1].CompareTo( '/' ) == 0) {
					return path;
				}
				return path + "/";

#else*/
				return getConfigurationValue("alnitak","imagesSrc");
//#endif
			}
		}
		
		/// <summary>Indica se está definido no Web.config o tempo de turno</summary>
		public static bool IsTurnTimeSpecified {
			get {
				return getConfigurationValue("chronos","turnTime") != null;
			}
		}
		
		/// <summary>Retorna a o tempo de turno</summary>
		public static long TurnTime {
			get {
				object obj = getConfigurationValue("chronos","turnTime");
				return long.Parse(obj.ToString());
			}
		}
		
		/// <summary>Indica se vai ser feito o trace à passagem de turno</summary>
		public static bool TraceTurn {
			get {
				object obj = getConfigurationValue("alnitak","traceTurn");
				if( obj == null ) {
					return false;
				}
				return obj.ToString() == "true";
			}
		}
		
		/// <summary>Indica o tempo de Beacon</summary>
		public static long BeaconTime {
			get {
				object obj = getConfigurationValue("alnitak", "beaconTime");
				return long.Parse(obj.ToString());
			}
		}
		
		/// <summary>Indica o URL da aplicao</summary>
		public static string AlnitakUrl {
			get {
//#if DEBUG
				return AppPath;
/*#else
				return OrionGlobals.getConfigurationValue("alnitak", "url");
#endif*/
			}
		}
		
		private static Regex FormatNumber = new Regex(@"(?<=\d)(?=(\d\d\d)+(?!\d))", RegexOptions.Compiled);

		/// <summary>Indica se uma string é um inteiro</summary>
		public static bool isInt( string str )
		{
			return Chronos.Utils.MathUtils.isInt(str);
		}
		
		/// <summary>Formata um número</summary>
		public static string format( int number )
		{
			return FormatNumber.Replace( number.ToString(), " " );
		}

		/// <summary>
		/// obtém o id da sessão ou, caso ainda não exista, uma string vazia
		/// </summary>
		public static string SessionId {
			get{
				if( null != HttpContext.Current.Session )
					return HttpContext.Current.Session.SessionID;
				return string.Empty;
			}
		}

		/// <summary>
		/// obtém o script que permite esconder/mostrar um elemento em javascript
		/// </summary>
		/// <returns></returns>
		public static void registerShowHideScript( System.Web.UI.Page page ) {
			string script =	@"
					<script language='javascript'>
						var imagePath = '"+ OrionGlobals.getCommonImagePath("") +@"';
						function show(id,img) {
							var obj = document.getElementById(id);
							if( 'none' != obj.style.display ) {
								obj.style.display = 'none';
								img.src = imagePath + 'plus.gif';
							} else {
								obj.style.display = 'inline';
								img.src = imagePath + 'minus.gif';
							}
						}
					</script>
				";
			page.RegisterClientScriptBlock("showhide", script );
		}
		
		/// <summary>
		/// dado a chave da secção, obtém o url desta
		/// </summary>
		/// <param name="key">chave que representa a secção</param>
		/// <returns>url da secção</returns>
		public static string getSectionUrl( string key ) {
			StringDictionary urls = (StringDictionary)HttpContext.Current.Cache["SectionUrl"];
			
			if( null != urls ) {
				if( urls.ContainsKey(key) )
					return urls[key].ToString();
				
				throw new AlnitakException("Secção " + key + " é inválida @ OrionGlobals::getSectionUrl");
			}
			
			throw new AlnitakException("urls sao inválidos @ OrionGlobals::getSectionUrl");
		}

		/// <summary>
		/// obtm o url da pgina principal de uma seco
		/// </summary>
		/// <param name="key">nome da seco</param>
		/// <returns>string com a pgina de defeito da seco</returns>
		public static string getSectionBaseUrl( string key ) {
			return getSectionUrl(key) + "default.aspx";
		}
		
		/// <summary>Elimina os vestigios do utilizador corrente online</summary>
		public static void clearOnlineUserInformation()
		{
			string name = HttpContext.Current.User.Identity.Name;
			if( HttpContext.Current.Cache[name] != null ) {
				HttpContext.Current.Cache.Remove(name);
				if( HttpContext.Current.User is User ) {
					HttpContext.Current.Cache.Remove("Ruler-" + ((User)HttpContext.Current.User).UserId );
				}
			}
		}
		
		/// <summary>Indica se um utilizador com determinado ID está online</summary>
		public static bool isUserOnline( int id )
		{
			return HttpContext.Current.Cache["Ruler-" + id] != null;
		}

		public static int GenerateRandInt( int minValue, int maxValue ) {
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] b = new byte[1];
			rng.GetBytes(b);
			Random random = new Random(Convert.ToInt32(b[0]));
			return random.Next( minValue , maxValue+1 );
		}
		
		#region Prizes
		
		public static string[] Conquer = new string[] {
			"FirstToConquer", "FirstTo5", "FirstTo10", "FirstTo15", "FirstTo20"
		};
		
		public static string[] Building = new string[] {
			"StarPortPrize", "CommsSatellitePrize"
		};
		
		public static string[] Research = new string[] {
			"FirstToKnowAll"
		};
			
		
		public static string getLink( AllianceInfo info )
		{
			return string.Format("<a href='{0}allianceinfo.aspx?id={1}'>{2}</a>",
							AppPath, info.Id, info.Name
				);
		}
			
		/// <summary>Retorna um link completo para um ruler</summary>
		public static string getLink( User user )
		{
			if( user.RulerId == -1 ) {
				StringWriter writer = new StringWriter();
				string css = getUserCss(user.UserId);
				writer.Write("<a href='{2}userinfo.aspx?id={1}' class='{3}'>{0}</a>", user.Nick, user.UserId, AppPath, css);
				getUserText(writer, user.UserId, null);
				return writer.ToString();
			}
			return getLink( Universe.instance.getRuler(user.RulerId) );
		}

		/// <summary>Retorna um link completo para um ruler</summary>
		public static string getCompleteLink( Ruler ruler )
		{
			return getLink(ruler, AlnitakUrl);			
		}

		/// <summary>Retorna um link completo para um ruler</summary>
		public static string getLink( Ruler ruler )
		{
			return getLink(ruler, AppPath);
		}
			
		/// <summary>Retorna um link completo para um ruler</summary>
		public static string getLink( Ruler ruler, string baseUrl )
		{
			StringWriter writer = new StringWriter();
			writer.Write("<a href='{2}userinfo.aspx?id={1}' class='{3}'>{0}</a>", ruler.Name, ruler.ForeignId, baseUrl, getUserCss(ruler.ForeignId));
			
#if DEBUG
			writer.Write(" <span class='red'>(RId:{0})</span> ", ruler.Id);
#endif
			
			if( ruler.InVacation ) {
				writer.Write("<img src={0} />", getCommonImagePath("prizes/vacation.gif"));
			}

			getUserText(writer, ruler.ForeignId, ruler);
			
			if( ruler.Prizes.Count == 0 ) {
				return writer.ToString();
			}
	
			Medal conquer = getBest(ruler, PrizeCategory.Conquer);
			Medal building = getBest(ruler, PrizeCategory.Building);
			Medal research = getBest(ruler, PrizeCategory.Research);
			string basePath = ImagePath + "skins/commonImages/prizes/"; 

			if( conquer != Medal.None ) {
				writer.Write("<img class='prize' src='{0}Conquer{1}.gif' />", basePath, conquer.ToString() );
			}
			if( building != Medal.None ) {
				writer.Write("<img class='prize' src='{0}Building{1}.gif' />", basePath, building.ToString() );
			}
			if( research != Medal.None ) {
				writer.Write("<img class='prize' src='{0}Research{1}.gif' />", basePath, research.ToString() );
			}
			
			return writer.ToString();
		}
		
		private static string getUserCss( int userId )
		{
			if( isAdmin(userId) ) {
				return "admin";
			}
			return "ruler";
		}
		
		/// <summary>Indica o texto só relativo a um user</summary>
		private static void getUserText( StringWriter writer, int userId, Ruler ruler )
		{
			string basePath = ImagePath + "skins/commonImages/prizes/";
			if( ruler != null && ruler.AllianceId > 0 ) {
				writer.Write("<img class='prize' src='{0}{1}.gif' />", basePath, ruler.AllianceRank);
			}
			if( isAdmin(userId) ) {
				writer.Write("<img class='prize' src='{0}Admin.gif' />", basePath);
				return;
			}
			
			if( isBetaTester(userId) ) {
				writer.Write("<img class='prize' src='{0}BetaTester.gif' />", basePath);
				return;
			}
			
			if( isArtist(userId) ) {
				writer.Write("<img class='prize' src='{0}Artist.gif' />", basePath);
			}
		}
		
		/// <summary>Indica a maior medalha de um ruler em determinada categoria</summary>
		public static Medal getBest( Ruler ruler, PrizeCategory category )
		{
			Medal best = Medal.None;
		
			foreach( Winner winner in ruler.Prizes ) {
				if( winner.Category != category ) {
					continue;
				}
				Medal medal = winner.Medal;
				if( medal < best ) {
					best = medal;
				}
			}
			
			return best;
		}
		
		/// <summary>Indica a categoria de um prémio</summary>
		public static string getPrizeCategory( Winner prize )
		{
			return prize.Category.ToString();
		}
						
		#endregion
		
		#region Roles
		
		protected static int[] admins;
		protected static int[] betaTesters;
		protected static int[] artists;
		
		/// <summary>Obtm as Roles alojadas no Web.config</summary>
		public static void getRoles()
		{
			admins = UserUtility.bd.getUsers(4);
			betaTesters = UserUtility.bd.getUsers(5);
			artists = UserUtility.bd.getUsers(6);
		}
		
		/// <summary>Indica se determinado user é admin</summary>
		public static bool isAdmin( User user )
		{
			return user.IsInRole("admin");
		}
		
		/// <summary>Indica se determinado indice é admin</summary>
		public static bool isAdmin( int idx )
		{
			return hasIndex(ref admins, idx);
		}
		
		/// <summary>Indica se determinado indice é admin</summary>
		public static bool isBetaTester( int idx )
		{
			return hasIndex(ref betaTesters, idx);
		}
		
		/// <summary>Indica se determinado indice é admin</summary>
		public static bool isBetaTester( User user )
		{
			return user.IsInRole("betaTester");
		}
		
		/// <summary>Indica se determinado indice é admin</summary>
		public static bool isArtist( int idx )
		{
			return hasIndex(ref artists, idx);
		}
		
		/// <summary>Indica se determinado indice é admin</summary>
		public static bool isArtist( User user )
		{
			return user.IsInRole("artist");
		}
		
		/// <summary>Indica se determinado indice está num array</summary>
		public static bool hasIndex( ref int[] array, int idx )
		{
			if( array == null ) {
				return false;
			}
			for( int i = 0; i < array.Length; ++i ) {
				if( array[i] == idx ) {
					return true;
				}
			}
			return false;
		}
		
		#endregion
		
		#region Information
		
		public static Assembly AlnitakAssembly {
			get { return typeof(OrionGlobals).Assembly; }
		}
		
		public static string AlnitakInfo {
			get {
				return alnitakInfo;
			}
		}
		
		private static string alnitakInfo = string.Format("Alnitak {1} Version: {0}",
											AlnitakAssembly.GetName().Version,
											Chronos.Utils.Platform.GetInfo(AlnitakAssembly)
									);
		
		#endregion
		
		#region Date Handling
		
		/// <summary>Formata uma data</summary>
		public static string FormatDate( DateTime date )
		{
			return string.Format("{0}-{1}-{2}", date.Day, date.Month, date.Year);
		}
		
		/// <summary>Formata uma hora</summary>
		public static string FormatTime( DateTime time )
		{
			return string.Format("{0}:{1}", time.Hour - 1, time.Minute);
		}
		
		/// <summary>Formata uma data também com hora</summary>
		public static string FormatDateTime( DateTime time )
		{
			return string.Format("{0} {1}", FormatDate(time), FormatTime(time));
		}
		
		#endregion

		#region Forum

		public static string ResolveForumImagePath(string themefile) {
			MasterSkinInfo masterSkinInfo = (MasterSkinInfo)HttpContext.Current.Items["MasterSkinInfo"];
			
			string appPath = HttpContext.Current.Request.ApplicationPath + "/"+ masterSkinInfo.masterSkinName + "/images/";
			

			//return appPath +"forum/" + themefile;
			return System.Web.HttpContext.Current.Server.MapPath(appPath + "forum/" + themefile );
		}

		#endregion
		
		#region Ranking
		
		public static void RankingBattleEndend( Ruler one, Ruler two, BattleResult result )
		{
			try {
				User uone = UserUtility.bd.getUser(one.ForeignId);
				User utwo = UserUtility.bd.getUser(two.ForeignId);
				
				Ranking.Update( uone, utwo, result );
				
				if( result == BattleResult.NumberOneVictory ) {
					++uone.Wins;
					++utwo.Losses;
				} else {
					++utwo.Wins;
					++uone.Losses;
				}
				
				UserUtility.bd.saveUser(uone, string.Empty);
				UserUtility.bd.saveUser(utwo, string.Empty);
				
				if( uone.AllianceId > 0 && utwo.AllianceId > 0 && uone.AllianceId != utwo.AllianceId ) {
					AllianceInfo aone = AllianceUtility.Persistance.Get(uone.AllianceId);
					AllianceInfo atwo = AllianceUtility.Persistance.Get(utwo.AllianceId);
					
					Ranking.Update(aone, atwo, result);
					
					++aone.RankingBattles;
					++atwo.RankingBattles;
					
					AllianceUtility.Persistance.Save(aone);
					AllianceUtility.Persistance.Save(atwo);	
				}
			} catch( Exception ex ) {
				ExceptionLog.log(ex);
			}
		}
		
		#endregion

	};
}
