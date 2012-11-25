using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


using Chronos.Messaging;
using Chronos.Exceptions;


namespace Chronos.Persistence.SqlServer {
	
	public class SqlServerMessagesPersistence : MessagesPersistence {

		#region Instance Fields
		
		private string _ConnString;

		IFormatter formatter = new BinaryFormatter();
		
		#endregion
		
		#region Properties

		public string ConnString {
			get{ return _ConnString; }
			set{ _ConnString = value; }
		}

		#endregion
		
		#region Ctor
		
		public SqlServerMessagesPersistence( PersistenceParameters param ) : base(param)
		{
			ConnString = param.GetParameter("ConnectionString");
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

		private string CreateTypesForSQL( MessageType[] types ) {
			string sqlTypes = string.Format("AND ( message_type='{0}'",types[0].ToString());
			for( int i = 1 ; i < types.Length; ++i ) {
				sqlTypes += string.Format(" OR message_type='{0}' ",types[i].ToString());
			}
			sqlTypes += ")";
			return sqlTypes;
		}

		#endregion
		
		#region Members
		
		/// <summary>Permite Guardar uma mensagem</summary>
		public override void SaveMessage( int id, string identifier, Message message ) {
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosSaveMessage", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", id.ToString() );
			cmd.Parameters.Add( "@identifier", identifier );
			cmd.Parameters.Add( "@type", message.Info.Category.ToString() );
			cmd.Parameters.Add( "@data", SerializeMessage(message) );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveMessage @ SqlServerMessagesPersistence::SaveMessage - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Marca todas as mensagens como lidas</summary>
		public override void MarkAllRead( int id, string identifier ) {
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosMarkAllAsRead", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", id );
			cmd.Parameters.Add( "@identifier", identifier );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosMarkAllAsRead @ SqlServerMessagesPersistence::MarkAllRead - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Indica quantas mensagens não lidas existem</summary>
		public override int UnreadCount( int id, string identifier )
		{
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosCountMessages", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", id );
			cmd.Parameters.Add( "@identifier", identifier );

			try {
				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				if( dr.Read() )
					return (int)dr["readCount"];
				
				return 0;
			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosCountMessages @ SqlServerMessagesPersistence::MarkAllRead - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Obtém mensagens</summary>
		public override Message[] GetMessages( int id, string identifier, MessageType[] types, int count ) {
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosLoadMessage", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", id );
			cmd.Parameters.Add( "@identifier", string.Format("'{0}'",identifier) );
			cmd.Parameters.Add( "@count", count.ToString() );
			cmd.Parameters.Add( "@types", CreateTypesForSQL(types) );

			try {
				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				if( !dr.HasRows )
					return new Message[0];
				
				ArrayList list = new ArrayList();
				
				while( dr.Read() ) {
					list.Add( Deserialize( (byte[])dr["message_data"] ) );				
				}

				return (Message[]) list.ToArray(typeof(Message));

			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosLoadMessage @ SqlServerMessagesPersistence::GetMessages - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}
		
		#endregion
		
	};
}
