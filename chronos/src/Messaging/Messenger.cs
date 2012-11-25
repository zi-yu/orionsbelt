
using System;
using Chronos.Core;
using DesignPatterns;

namespace Chronos.Messaging {
	
	/// <summary>
	/// Entidade que sabe enviar mensagens entre entidades IMessageHandler
	/// </summary>
	public sealed class Messenger {

		#region Static Fields

		private static FactoryContainer allMessageTypes = new FactoryContainer(typeof(MessageInfo));

		#endregion

		#region Static Properties

		/// <summary>Indica o FactoryContainer com todos os tipos de mensages</summary>
		public static FactoryContainer All {
			get { return allMessageTypes; }
		}

		#endregion
		
		#region Static Properties
		
		private static MessageType[] intelMessages = new MessageType[]{
			MessageType.Battle, MessageType.Scan, MessageType.Radar, MessageType.Sabotage
		};
		
		private static MessageType[] managementMessages = new MessageType[]{
			MessageType.PlanetManagement, MessageType.ResearchManagement, MessageType.FleetManagement
		};
		
		private static MessageType[] otherMessages = new MessageType[]{
			MessageType.Information, MessageType.Error, MessageType.Alliance, MessageType.Alert,
			MessageType.Prize, MessageType.Generic, MessageType.None
		};
	
		/// <summary>Indica as mensagens de Management</summary>
		public static MessageType[] ManagementMessages {
			get {
				return managementMessages;
			}
		}
		
		/// <summary>Indica as mensagens de Management</summary>
		public static MessageType[] IntelMessages {
			get {
				return intelMessages;
			}
		}
		
		/// <summary>Indica as mensagens de Management</summary>
		public static MessageType[] OtherMessages {
			get {
				return otherMessages;
			}
		}
		
		#endregion
	
		#region Static Send Members

		/// <summary>Envia uma mensagem com base num MessageInfo</summary>
		public static void Send( IMessageHandler to,
					MessageInfo info,
					string[] args )
		{
			Message message = new Message(info, args);
			to.acceptMessage(message);
		}
		
		/// <summary>Envia uma mensagem com base no nome de um MessageInfo</summary>
		public static void Send( IMessageHandler to,
					string messageInfo,
					params string[] args )
		{
			MessageInfo info = getMessageInfo(messageInfo);
			Message message = new Message(info, args);
			to.acceptMessage(message);
		}

		#endregion

		#region Utility Methods

		/// <summary>Método auxilar que obtém um MessageInfo dado o seu nome</summary>
		public static MessageInfo getMessageInfo( string name )
		{
			if( !All.ContainsKey(name) ) {
				return null;
			}
			return (MessageInfo) All.create(name);
		}

		#endregion

		#region MessageInfo Mappping Properties

		/// <summary>Indica uma MessageInfo do tipo BuildingCompleted</summary>
		public static MessageInfo BuildingCompleted {
			get { return getMessageInfo("BuildingCompleted"); }
		}

		#endregion
				
	};
}
