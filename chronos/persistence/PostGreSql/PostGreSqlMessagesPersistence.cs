using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Chronos.Messaging;
using Chronos.Exceptions;
using Npgsql;

namespace Chronos.Persistence.PostGreSql {
	
	public class PostGreSqlMessagesPersistence : MessagesPersistence {

		#region Instance Fields
		
		private string _connString;

		IFormatter formatter = new BinaryFormatter();
		
		#endregion
		
		#region Properties

		public string ConnString {
			get{ return _connString; }
			set{ _connString = value; }
		}

		#endregion
		
		#region Ctor
		
		public PostGreSqlMessagesPersistence( PersistenceParameters param ) : base(param)
		{
			ConnString = param.GetParameter("ConnectionStringPG");
		}
		
		#endregion
		
		#region Private

		private byte[] SerializeMessage( Message message ) {
			MemoryStream stream = new MemoryStream();

			formatter.Serialize(stream, message);
			byte[] data = stream.ToArray();
			//size = data.Length;
			return data;
		}

		private Message Deserialize(byte[] message) {
			Stream stream = new MemoryStream(message);
			if( null == stream ) {
				return null;
			}
			return (Message) formatter.Deserialize(stream);
		}

		private string CreateTypesForNpgsql( MessageType[] types ) {
			string NpgsqlTypes = string.Format("AND ( message_type='{0}'",types[0].ToString());
			for( int i = 1 ; i < types.Length; ++i ) {
				NpgsqlTypes += string.Format(" OR message_type='{0}' ",types[i].ToString());
			}
			NpgsqlTypes += ")";
			return NpgsqlTypes;
		}

		#endregion
		
		#region Members
		
		/// <summary>Permite Guardar uma mensagem</summary>
		public override void SaveMessage( int id, string identifier, Message message ) {
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosSaveMessage", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "id", id );
			cmd.Parameters.Add( "identifier", identifier );
			cmd.Parameters.Add( "type", message.Info.Category.ToString() );
			cmd.Parameters.Add( "data", SerializeMessage(message) );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveMessage @ NpgsqlServerMessagesPersistence::SaveMessage - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Marca todas as mensagens como lidas</summary>
		public override void MarkAllRead( int id, string identifier ) {
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosMarkAllAsRead", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", id );
			cmd.Parameters.Add( "@identifier", identifier );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosMarkAllAsRead @ NpgsqlServerMessagesPersistence::MarkAllRead - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Indica quantas mensagens não lidas existem</summary>
		public override int UnreadCount( int id, string identifier )
		{
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosCountMessages", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", id );
			cmd.Parameters.Add( "@identifier", identifier );

			try {
				conn.Open();
				NpgsqlDataReader dr = cmd.ExecuteReader();
				if( dr.Read() )
					return (int)((Int64)dr[0]);
				
				return 0;
			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosCountMessages @ NpgsqlServerMessagesPersistence::MarkAllRead - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Obtém mensagens</summary>
		public override Message[] GetMessages( int id, string identifier, MessageType[] types, int count ) {
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand(
				string.Format(
					@"SELECT message_data FROM OrionsBelt_Messages WHERE message_id = {0} AND message_identifier = '{1}' {2} ORDER BY message_uniqueId DESC LIMIT {3};",
					id.ToString(),
					identifier,
					CreateTypesForNpgsql(types),
					count.ToString()
				),
				conn);

			cmd.CommandTimeout = 0;

			try {
				conn.Open();
				NpgsqlDataReader dr = cmd.ExecuteReader();
				if( !dr.HasRows )
					return new Message[0];
				
				ArrayList list = new ArrayList();
				
				while( dr.Read() ) {
					list.Add( Deserialize( (byte[])dr[0] ) );				
				}

				return (Message[]) list.ToArray(typeof(Message));

			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosLoadMessage @ NpgsqlServerMessagesPersistence::GetMessages - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}
		
		#endregion
		
	};
}
