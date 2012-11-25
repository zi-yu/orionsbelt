using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Chronos.Battle;
using Chronos.Exceptions;
using Chronos.Persistence;

namespace Chronos.Persistence.SqlServer {
	public class SqlServerBattlePersistence : BattlePersistence {

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
		
		public SqlServerBattlePersistence( PersistenceParameters param ) : base(param) {
			ConnString = param.GetParameter("ConnectionString");
		}
		
		#endregion
		
		#region Private

		private byte[] SerializeMessage( BattleInfo message ) {
			MemoryStream stream = new MemoryStream();

			formatter.Serialize(stream, message);
			byte[] data = stream.ToArray();
			//size = data.Length;
			return data;
		}

		private BattleInfo Deserialize(byte[] message) {
			Stream stream = new MemoryStream(message);
			if( null == stream ) {
				return null;
			}
			return (BattleInfo) formatter.Deserialize(stream);
		}

		#endregion
		
		#region Members
		
		public override void SaveBattle( BattleInfo battleInfo, int rulerIdToPlay ) {
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosSaveBattle", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", battleInfo.BattleId );
			cmd.Parameters.Add( "@data", SerializeMessage(battleInfo) );
			cmd.Parameters.Add( "@rulerid", rulerIdToPlay );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveBattle @ SqlServerBattlePersistence::SaveBattle - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		public override void SaveBattleTurn( int battleId, int rulerIdToPlay ) {
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosSaveBattleTurn", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", battleId );
			cmd.Parameters.Add( "@rulerid", rulerIdToPlay );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveBattleTurn @ SqlServerBattlePersistence::SaveBattle - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		public override BattleInfo LoadBattle( int battleId ) {
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosLoadBattle", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", battleId );

			try {
				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				if( !dr.HasRows )
					return null;
				
				dr.Read();

				return Deserialize( (byte[])dr["battle_data"] );
			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosLoadBattle @ SqlServerBattlePersistence::LoadBattle - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		public override int LoadBattleTurn( int battleId ) {
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosLoadBattle", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", battleId );

			try {
				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				if( !dr.HasRows )
					return 0;
				
				dr.Read();

				return (int)dr["battle_rulerid"];
			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosLoadBattle @ SqlServerBattlePersistence::LoadBattle - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		public override void RemoveBattle( int battleId ) {
			SqlConnection conn = new SqlConnection(ConnString);
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosRemoveBattle", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", battleId );

			try {
				conn.Open();
				cmd.ExecuteNonQuery( );
			} catch( SqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosRemoveBattle @ SqlServerBattlePersistence::RemoveBattçe - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		#endregion
	}
}
