using System;
using System.Web;
using System.Security.Principal;
using System.Web.Caching;
using Alnitak;
using Alnitak.Exceptions;
using Chronos.Core;
using Chronos.Utils;

namespace Alnitak {

	public class OrionApplication : HttpApplication {
	
		#region Static Fields
	
		public static readonly string[] defaultRoles = new string[] {"guest"};
		public static GenericPrincipal defaultPrincipal;
	
		#endregion
		
		#region Chronos Events
		
		DateTime turnTimeStart;
		
		/// <summary>notifica o inicio da passagem de turno</summary>
		private void turnStart( object src, EventArgs args )
		{
			turnTimeStart = DateTime.Now;
			Chronos.Utils.Log.log("Chronos Turn Start!");
		}
		
		/// <summary>notifica o inicio da passagem de turno</summary>
		private void turnEnd( object src, EventArgs args )
		{
			DateTime now = DateTime.Now;
			TimeSpan span = now - turnTimeStart;
			
			Application.Lock();
			
			ChronosStats stats = (ChronosStats) Application["ChronosStats"];
			stats.LastTurnTime = span;
			stats.LastTurn = now;
			++stats.TurnCount;
			
			Application.UnLock();
			
			Chronos.Utils.Log.log("Chronos Turn End! " + span);
		}
		
		/// <summary>Realiza o trace ao Universo</summary>
		private void Events_TraceUniverse( object src, EventArgs args )
		{
			Universe.TraceEventArgs eventArgs = (Universe.TraceEventArgs) args;
			Chronos.Utils.Log.log("{0}# {1}", eventArgs.Turn, eventArgs.Message);
		}

		/// <summary>
		/// Apanha os erros ocorridos na passagem de turno
		/// </summary>
		private void Events_TurnError(object sender, EventArgs e) {
			ExceptionLog.log( ((Universe.TurnErrorEventArgs)e).Error );
		}

		#endregion
	
		#region Application Events
	
		/// <summary>Marca o incio da aplicacao</summary>
		protected void Application_Start(object src, EventArgs e)
		{
			Log.log("---");
			Log.log(OrionGlobals.AlnitakInfo);
			Log.log(Chronos.Utils.Platform.ChronosInfo);
			Log.log("---");
			Log.log("Starting Alnitak...");

			GenericIdentity identity = new GenericIdentity("guest");
			defaultPrincipal = new GenericPrincipal(identity, defaultRoles);

			DateTime now = DateTime.Now;
			DateTime dressUp = now;

			Universe.Events.TurnStart += new EventHandler(turnStart);
			Universe.Events.TurnEnd += new EventHandler(turnEnd);
			Universe.Events.TurnError += new EventHandler(Events_TurnError);

			if( OrionGlobals.TraceTurn ) {
				Universe.Events.UniverseTrace += new EventHandler(Events_TraceUniverse);
			}

			try {
				Log.log("Loading Universe...");
				Universe.Parameters = OrionGlobals.Persistence;
				Universe.instance = Universe.load();

				if( null == Universe.instance ) {
					Universe.instance = new Universe();
					Universe.instance.init();
				}
				Log.log("Done!");

				dressUp = DateTime.Now;
			} catch(Exception ex) {
				Log.log(ex);
				ExceptionLog.log( ex );
			} finally {
				try{
					ChronosStats stats = new ChronosStats();
					stats.StartTime = now;
					stats.DressUp = dressUp - now;
					Application["ChronosStats"] = stats;
					Application["AlnitakOnlineUsers"] = new UserWatcher();
					Application["AlnitakOnlineUsersCount"] = 0;
					
					OrionGlobals.getRoles();

					if( OrionGlobals.IsTurnTimeSpecified ) {
						Universe.instance.TurnTime = OrionGlobals.TurnTime;
					}

					Universe.instance.start();
					Universe.RankingBattleEnded += new Universe.RankingBattleHandler( OrionGlobals.RankingBattleEndend );

					Log.log("... Done!");
				} catch( Exception ex ) {
					throw new AlnitakException( ex.Message );
				}
			}
		}
		
		/// <summary>Marca o início de uma sessão</summary>
		protected void Session_Start(object src, EventArgs e)
		{
			Session["OrionRequestManager"] = new OrionRequestManager();
			Log.log("Session Started!");
		}
		
		/// <summary>Responde ao pedido de autenticação</summary>
		protected void Application_AuthenticateRequest( object src, EventArgs e )
		{
			Log.log("Authenticate Request.... ");
			string request =  HttpContext.Current.Request.FilePath;
			bool isAspx = request.IndexOf(".aspx") != -1;

			if( Request.IsAuthenticated ) {
				if( Context.User is User ) {
					Log.log("Context is user!");
					return;
				}
				string name = Context.User.Identity.Name;

				object o = HttpContext.Current.Cache[name];
				if( o == null ) {
					User user = new User( Context.User.Identity, UserUtility.bd.getRoles(name) );
					HttpContext.Current.Cache.Add(name, user, null, DateTime.Now.AddMinutes(9), Cache.NoSlidingExpiration, CacheItemPriority.Default, new CacheItemRemovedCallback(this.UserOffline));
					HttpContext.Current.Cache.Add("Ruler-"+user.UserId, user, null, DateTime.Now.AddMinutes(9), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
					Context.User = user;
					RegisterOnlineUser(user);
					UserUtility.bd.updateLastLogin(name);
				} else {
					User user = (User) o;
					Context.User = user;
					string userId = "Ruler-"+user.UserId;
					if(HttpContext.Current.Cache[name] == null || HttpContext.Current.Cache[userId] == null) {
						HttpContext.Current.Cache.Add(name, user, null, DateTime.Now.AddMinutes(9), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
						HttpContext.Current.Cache.Add(userId, user, null, DateTime.Now.AddMinutes(9), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
					}
				}
				setUserSkin( (User)Context.User );
			} else {
				Context.User = defaultPrincipal;
			}
			
			if( CheckWiki(Context.User) ) {
				Log.log("... Done!");
				return;
			}
			
			if( !canView() && isAspx ) {
				OrionGlobals.forceLogin();
			}
			
			Log.log("... Done!");
		}
		
		/// <summary>Limpa o que for preciso</summary>
		protected void Application_End( object src, EventArgs e )
		{
			Log.log("Shuting down application... ");
			Universe.persist(Universe.instance);
			Log.log("Done");
		}

		protected void Application_Error( object src, EventArgs e ) {
			Log.log("Application Error... ");

			HttpContext context = HttpContext.Current;
			
			Exception exception = Server.GetLastError();
			
			Log.log(" Error: " + exception.Message);
			
			try {
				if( null == exception )
					throw new AlnitakException("A excepção recebida em Application_Error é null");

				if( exception is HttpUnhandledException ) {
					ExceptionInfo info =  (ExceptionInfo)context.Cache[ OrionGlobals.SessionId + "AlnitakException"];
					endRequest( info, null );
				}
				
				ExceptionLog.log( exception );
				Log.log("... Done!");
	
				Log.log("Going transfer...");
				context.Server.Transfer( OrionGlobals.resolveBase( OrionGlobals.getConfigurationValue("pagePath","globalError") ) );
			} catch( System.Threading.ThreadAbortException ) {
				Log.log("$$$$$$$ Thread Abort");
			} catch( Exception ex ) {
				Log.log("... Exception: " + ex.Message );
				ExceptionInfo info =  (ExceptionInfo)context.Cache[ OrionGlobals.SessionId + "AlnitakException"];
				endRequest( info, new ExceptionInfo( ex ) );
			}
		}

		private void endRequest( ExceptionInfo info, ExceptionInfo lastExp ) {
			HttpContext.Current.Cache.Remove( OrionGlobals.SessionId + "ExceptionNumber");
			HttpContext.Current.Response.Write( string.Format( "FATAL APPLICATION ERROR <!--<br/>Name:{0}<br/>Error:{0}<br/>Stack Trace:{1} -->",info.Message,info.StackTrace.Replace("<br/>","\n" ) ) );
			if( null != lastExp )
				HttpContext.Current.Response.Write( string.Format( "<p/>Excepção 2:<p/><!--<br/>Name:{0}<br/>Error:{0}<br/>Stack Trace:{1} -->",lastExp.Message,lastExp.StackTrace.Replace("<br/>","\n" ) ) );
			HttpContext.Current.Response.End();
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>
		/// verifica se o utilizador pode ver ou n a página corrente
		/// </summary>
		/// <returns><code>true</code> se o utilizador puder ver a página, <code>false</code> caso contrário</returns>
		private bool canView(){
			Log.log("Checking 'canView'...");
			SectionInfo sectionInfo = (SectionInfo)Context.Items["SectionInfo"];
			if( sectionInfo == null ) {
				Log.log("[false] sectionInfo is null");
				return false;
			}

			string[] sectionRoles = sectionInfo.sectionRoles;
			
			if ( null == sectionRoles ) {
				return true;
			}
			
			bool toReturn = false;
			
			for( int i = 0 ; i < sectionRoles.Length ; ++i ) {
				if( Context.User.IsInRole( sectionRoles[i] ) )
					toReturn = true;
			}
			
			return toReturn;
		}

		/// <summary>Altera a skin currente para a skin do utilizador</summary>
		private void setUserSkin( User user ) {
			MasterSkinInfo masterSkinInfo = MasterSkinUtility.getMasterSkinInfoFromId( user.Skin );
			Context.Items[ "MasterSkinInfo" ] = masterSkinInfo;
		}
		
		/// <summary>Regista a entrada de um utilizador online</summary>
		private void RegisterOnlineUser( User user )
		{
			UserWatcher users = (UserWatcher) Application["AlnitakOnlineUsers"];
			users.Register(user);
			UserOnline();

			Log.log("User " + user.Nick + "("+user.UserId+") has entered the game");
		}
		
		/// <summary>Verifica se o pedido  do Wiki e se for trata-o</summary>
		private bool CheckWiki( IPrincipal user )
		{
			Log.log("Checking Wiki...");
			SectionInfo sectionInfo = (SectionInfo)Context.Items["SectionInfo"];
			if( sectionInfo == null ) {
				Log.log("   sectionInfo == null");
				return true;
			}
			
			if( sectionInfo.sectionName != "Wiki" ) {
				Log.log("   sectionInfo.sectionName != Wiki");
				return false;
			}
			
#if WIKI
			Log.log("User can edit wiki? " + user.IsInRole("admin") );
			if( false && user.IsInRole("admin") ) {
				string newPath = HttpContext.Current.Request.RawUrl;
				Log.log("Going to rewrite path to {0}", newPath);
				Context.RewritePath(newPath);
				return true;
			}
#endif
			return true;
		}
		
		#endregion
		
		#region User Counter
		
		/// <summary>Marca o início de uma sessão</summary>
		protected void UserOnline()
		{
			try {
				Application.Lock();
				int curr = (int) Application["AlnitakOnlineUsersCount"];
				Application["AlnitakOnlineUsersCount"] = ++curr;
				
				Log.log("Sessions: " + curr);
			} finally {
				Application.UnLock();
			}
		}
		
		/// <summary>Marca o incio de uma sesso</summary>
		protected void UserOffline( string key, object val, CacheItemRemovedReason reason)
		{
			try {
				Application.Lock();
				int curr = (int) Application["AlnitakOnlineUsersCount"];
				Application["AlnitakOnlineUsersCount"] = --curr;
				Log.log("Sessions: " + curr);
			} finally {
				Application.UnLock();
			}
		}
		
		#endregion

	};

}
