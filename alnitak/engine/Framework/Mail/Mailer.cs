// created on 18-01-2005 at 10:12

//#define DEBUG_MAIL

using System;
using System.Web.Mail;
using Alnitak;
using Chronos.Utils;

namespace Alnitak.Mail {

	public abstract class Mailer {
	
		#region Generic Static Members
		
		static Mailer()
		{
			SmtpMail.SmtpServer = "foxie.clustercube.com:25";
		}
		
		#endregion

		#region Static Send Methods
		
		/// <summary>Envia um mail</summary>
		public static bool Send( MailMessage message )
		{
			try {
				if( message.From == null || message.From == "" ) {
					message.From = Mailer.From;
				}
				
#if DEBUG_MAIL
				Log.log("----- SEND MAIL DEBUG ----------");
				Log.log("To: {0}", message.To);
				Log.log("From: {0}", message.From);
				Log.log("Bcc: {0}", message.Bcc);
				Log.log("Title: {0}", message.Subject);
				Log.log("Message: {0}", message.Body);
				Log.log("-------------------------------");
#endif
			
				Log.log("Sending mail message '{0}'...", message.Subject );
				SmtpMail.Send(message);
				Log.log("... Done!");
				return true;
					
			} catch(System.Exception e) {
				ExceptionLog.log(e, false);
				return false;
			}
		}
		
		/// <summary>Envia um mail</summary>
		public static bool Send( string to, string subject, string body )
		{
			MailMessage message = new MailMessage();
			message.To = to;
			message.Subject = subject;
			message.Body = body;
			
			return Send(message);
		}
		
		/// <summary>Envia um mail</summary>
		public static bool Send( string from, string to, string subject, string body )
		{
			MailMessage message = new MailMessage();
			
			message.From = from;
			message.To = to;
			message.Subject = subject;
			message.Body = body;
			
			return Send(message);
		}
		
		/// <summary>Envia um mail aos administradores</summary>
		public static bool SendToAdmin( string subject, string body )
		{
			MailMessage message = new MailMessage();
			
			string admins = OrionGlobals.getConfigurationValue("alnitak", "adminMail");
			
			message.To = admins;
			message.Subject = subject;
			message.Body = body;
			
			return Send(message);
		}
		
		/// <summary>Envia um mail para a mailing list de notcias</summary>
		public static bool SendToNewsML( string subject, string body )
		{
			MailMessage message = new MailMessage();
			
			string admins = OrionGlobals.getConfigurationValue("alnitak", "adminMail");
			string ml = OrionGlobals.getConfigurationValue("alnitak", "newsMailingListMail");
			
			message.Bcc = admins;
			message.To = ml;
			message.Subject = subject;
			message.Body = body;
			
			return Send(message);
		}

		#endregion
		
		#region Static Utilities
		
		/// <summary>Indica o campo completo From do jogo</summary>
		public static string From  {
			get {
				string mail = OrionGlobals.getConfigurationValue("alnitak", "orionMail");
				string name = OrionGlobals.getConfigurationValue("alnitak", "orionSender");
				
				return string.Format("{0} <{1}>", name, mail);
			}
		}
		
		/// <summary>Indica um mail dado um user</summary>
		public static string GetFormattedMail( User user )
		{
			return string.Format("{0} <{1}>", user.Nick, user.Mail);
		}
		
		#endregion
	
	};
}
