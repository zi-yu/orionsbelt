// created on 10/23/2004 at 9:46 AM

using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Resources;
using Chronos.Queue;
using Chronos.Actions;
using Chronos.Info.Results;
using Chronos.Messaging;
using Chronos.Utils;
using Language;

namespace Alnitak {

	public class MessageList : Control {

		#region Instance Fields

		private MessageManager manager;
		private bool showImages;
		private string title;
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		private int numberMsg = 10;

		#endregion

		#region Ctor
	
		/// <summary>Ctor</summary>
		public MessageList()
		{
			Manager = null;
			showImages = true;
			title = info.getContent("mensagens");
		}
	
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o MessageManager a utilizar</summary>
		public MessageManager Manager {
			get { return manager; }
			set { manager = value; }
		}

		/// <summary>Indica se é para mostrar as imagens relativas ao tipo de recurso</summary>
		public bool ShowImages {
			get { return showImages; }
			set { showImages = value; }
		}

		/// <summary>Indica o título a utilizar</summary>
		public string Title {
			get { return title; }
			set { title = value; }
		}

		public int NumberMsg {
			get { return numberMsg; }
			set { numberMsg = value; }
		}

		#endregion
		
		#region Control Title Rendering
		
		/// <summary>Escreve o título da tabela</summary>
		private void writeTitle( ref StringBuilder writer )
		{
			if( ShowImages ) {
				writer.Append("<td class='resourceTitle'>#</td>");
			}

			writer.Append("<td class='resourceTitle'><b>");
			writer.Append( info.getContent("mensagens") );
			writer.Append("</b></td>");
			
			writer.Append("<td class='resourceTitle'>");
			writer.Append( info.getContent("turn_current") );
			writer.Append("</td>");
		}

		#endregion
		
		#region Control Items Rendering
		
		/// <summary>Pinta o Controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			try {
				int unread = manager.UnreadCount;
				writer.WriteLine("<p>");
				writer.WriteLine(string.Format(info.getContent("unread_messages"), unread));
				writer.WriteLine("</p>");
			
				writeMessages(writer, Messenger.IntelMessages, "intel", NumberMsg);
				writeMessages(writer, Messenger.OtherMessages, "other", NumberMsg);
				writeMessages(writer, Messenger.ManagementMessages, "management", NumberMsg);
				manager.markAllRead();
			} catch( Exception ex ) {
				writer.WriteLine("<h2 class='red'>Message Server Down</h2>");
				writer.WriteLine("<!-- {0} -->", ex.ToString());
			}
		}
		
		
		/// <summary>Escreve os recursos</summary>
		private void writeMessages( HtmlTextWriter writer, MessageType[] types, string subtitle, int quant )
		{
			StringBuilder allMessages = new StringBuilder();

			allMessages.Append(string.Format("<div class='planetInfoZoneTitle'>{0}</b> - {1}</div>",
										   Title, info.getContent(subtitle))
			);
			
			allMessages.Append("<table class='planetFrame' width='100%'>");
			
			Message[] messages = manager.GetMessages(types, quant);
			if( messages.Length == 0 ) {
				allMessages.Append("<tr><td>");
				allMessages.Append(info.getContent("noneAvailable"));
				allMessages.Append("</td></tr></table>");
				return;
			}
			
			allMessages.Append("<tr class='resourceTitle'>");
			writeTitle( ref allMessages);
			allMessages.Append("</tr>");

			for( int i = 0; i < messages.Length; ++i) {
				allMessages.Append("<tr>");
				writeLine( ref allMessages, messages[i], false);
				allMessages.Append("</tr>");
			}
			
			allMessages.Append("</table>");

			writer.WriteLine( allMessages );
		}
		
		/// <summary>Escreve um item</summary>
		private void writeLine( ref StringBuilder allMessages, Message message, bool bold )
		{
			if( ShowImages ) {
				allMessages.Append("<td class='resourceCell'>");
				if ( message.Info != null ) {
					allMessages.Append(string.Format("<img src='{0}' />",
						OrionGlobals.getCommonImagePath("messages/" + message.Info.Category + ".gif"))
					);
				}
				allMessages.Append("</td>");
			}
			
			string line = string.Empty;

			try {
		
				allMessages.Append("<td class='resource'>");
				line = info.getContent(message.Info.Name);
			
				if( bold ) allMessages.Append("<b>");
				allMessages.Append( message.Info.localize(message, line, new MessageDecorator(info) ) );
				if( bold ) allMessages.Append("</b>");
				
				allMessages.Append("</td>");
			
				allMessages.Append("<td class='resourceCell'>");
				allMessages.Append(message.Turn.ToString());
				allMessages.Append("</td>");

			} catch(Exception e) {
				Log.log("-------------- MESSAGE ERROR ------------");
				Log.log(e);
				ExceptionLog.log( e );
				allMessages.Append("<span class='error'>[Error translating]</span> " + message.ToString());
			}
		}
		
	
		#endregion
		
	};
	
}
