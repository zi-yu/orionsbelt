using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Chronos.Battle;
using Chronos.Exceptions;
using Chronos.Persistence;

using Npgsql;

namespace Chronos.Persistence.PostGreSql {

	public class PostGreBattlePersistence : BattlePersistence {

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
		
		public PostGreBattlePersistence( PersistenceParameters param ) : base(param) {
			ConnString = param.GetParameter("ConnectionStringPG");
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
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosSaveBattle", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "id", battleInfo.BattleId );
			cmd.Parameters.Add( "data", SerializeMessage(battleInfo) );
			cmd.Parameters.Add( "rulerid", rulerIdToPlay );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveBattle @ NpgsqlServerBattlePersistence::SaveBattle - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		public override void SaveBattleTurn( int battleId, int rulerIdToPlay ) {
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosSaveBattleTurn", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "id", battleId );
			cmd.Parameters.Add( "rulerid", rulerIdToPlay );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveBattleTurn @ NpgsqlServerBattlePersistence::SaveBattle - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		public override BattleInfo LoadBattle( int battleId ) {
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosLoadBattle", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "id", battleId );

			try {
				conn.Open();
				NpgsqlDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if( dr[0] is System.DBNull ) {
					return null;
				}

				return Deserialize( (byte[])dr[0] );
			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosLoadBattle @ NpgsqlServerBattlePersistence::LoadBattle - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		public override int LoadBattleTurn( int battleId ) {
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosLoadRulerId", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "id", battleId );

			try {
				conn.Open();
				NpgsqlDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if( dr[0] is System.DBNull ) {
					return 0;
				}
				
				return (int)dr[0];
			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosLoadRulerId @ NpgsqlServerBattlePersistence::LoadBattle - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		public override void RemoveBattle( int battleId ) {
			NpgsqlConnection conn = new NpgsqlConnection(ConnString);
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosRemoveBattle", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( "@id", battleId );

			try {
				conn.Open();
				cmd.ExecuteNonQuery( );
			} catch( NpgsqlException e ) {
				throw new ChronosException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosRemoveBattle @ NpgsqlServerBattlePersistence::RemoveBattçe - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		#endregion
	}
}
