namespace Alnitak {

	using System;
	using System.Web;
	using System.Collections;
	using Alnitak.Mail;

	/// <summary>
	/// ExceptionLogUtility 
	/// </summary>
	public class ExceptionLog {

		private static ExceptionLogUtility getInstance() {
			string value = OrionGlobals.getConfigurationValue("utilities", ExceptionLogUtility.getExceptionLogKey() );
			return (ExceptionLogUtility)Activator.CreateInstance( Type.GetType( value , true) );
		}
		
		/// <summary>
		/// faz log da excepcao
		/// </summary>
		/// <param name="exceptionInfo">objecto que tem a informao da excepcao</param>
		public static ExceptionInfo log( Exception exception ) {
			return log(exception, true);
		}

		/// <summary>
		/// faz log da excepcao
		/// </summary>
		/// <param name="exceptionInfo">objecto que tem a informao da excepcao</param>
		public static ExceptionInfo log( Exception exception, bool sendMail ) {
			//carregar o ExceptionLogBase consuante a plataforma
			ExceptionInfo exceptionInfo;			
			
			try{
				Chronos.Utils.Log.log(exception);
				
				exceptionInfo = new ExceptionInfo( exception );
				
				if( exception.GetType() == typeof(System.Web.HttpException) ) {
					return exceptionInfo;
				}

				getInstance().save( exceptionInfo );
				
				if( sendMail ) {
					Mailer.SendToAdmin("[Orionsbelt] Exception!", string.Format("{0}\n\nPath: {1}",exception.ToString(), getPath()));
				}

			} catch( Exception e ) {
				exceptionInfo = new ExceptionInfo( e );
			}
			if( null != HttpContext.Current )
				HttpContext.Current.Cache[ OrionGlobals.SessionId +  "AlnitakException"] = exceptionInfo;

			return exceptionInfo;
		}
		
		/// <summary>
		/// faz log da excepcao
		/// </summary>
		/// <param name="exceptionInfo">objecto que tem a informao da excepcao</param>
		public static ExceptionInfo log( string name, string message ) {
			ExceptionInfo exceptionInfo;			
			
			try{
				exceptionInfo = new ExceptionInfo( name, message );
				getInstance().save( exceptionInfo );
			} catch( Exception e ) {
				exceptionInfo = new ExceptionInfo( e );
			}
			
			return exceptionInfo;
		}
 
		/// <summary>
		/// Careega todas as excepcoes ocorridas at ao momento
		/// </summary>
		/// <param name="exceptionInfo">objecto que tem a informao da excepcao</param>
		/// <returns>ArrayList com todos os ExceptionInfo's</returns>
		public static ExceptionInfo[] load() {
			return  getInstance().load();
		}

		/// <summary>
		/// remove a excepo com identificador id
		/// </summary>
		/// <param name="id">identificador da excepo</param>
		public static void remove( int id ) {
			 getInstance().remove( id );
		}

		public static void removeAll( ) {
			getInstance().removeAll();
		}
		
		/// <summary>Indica o path do pedido corrente</summary>
		public static string getPath()
		{
			if( HttpContext.Current == null ) {
				return "?";
			}
			return string.Format("http://{0}{1}", 
					HttpContext.Current.Request.Url.Host, 
					HttpContext.Current.Request.RawUrl
				);
		}

	}
}
