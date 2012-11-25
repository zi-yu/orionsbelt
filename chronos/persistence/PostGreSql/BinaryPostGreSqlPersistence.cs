using System;
using System.Data;
using System.IO;
using Chronos.Exceptions;
using Npgsql;

namespace Chronos.Persistence.PostGreSql {

	public class BinaryPostGreSqlPersistence : FilePersistence {
	
		#region Instance Fields
		
		private string connString;
		
		#endregion
	
		#region Ctors

		/// <summary>Ctor</summary>
		public BinaryPostGreSqlPersistence( ) : base("Universe.bin") {
		
		}

		/// <summary>Ctor</summary>
		public BinaryPostGreSqlPersistence( string conn ) : base("Universe.bin"){
			connString = conn;
		}
		
		#endregion

		#region UniverseSerializer Implementation
	
		/// <summary>Armazena uma Stream com persistncia</summary>
		protected override void saveData( byte[] data, PersistenceParameters parameters ) {
			NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString(parameters));
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosSaveUniverse", conn);
			cmd.CommandTimeout = 0;
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add( "data", data );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
				
				PersistToFile( data,parameters );
				
			} catch( NpgsqlException e ) {
				throw new RuntimeException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveUniverse @ BinarySqlPersistence::saveData - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		/// <summary>Carrega uma Stream com um Universo</summary>
		protected override Stream loadData(PersistenceParameters parameters) {
			NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString(parameters));
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_ChronosLoadUniverse", conn);
			cmd.CommandTimeout = 0;
			cmd.CommandType=CommandType.StoredProcedure;
			
			try {
				conn.Open();
				NpgsqlDataReader dr = cmd.ExecuteReader();
				if( dr.Read() ) {
					return new MemoryStream( (byte[])dr[0] );
				}else{
					if( System.IO.File.Exists( Path.Combine(GetPath(parameters),"universe.bin") )){
						return GetFromFile(parameters);
					}
				}
			} catch( NpgsqlException e ) {
				Chronos.Core.Universe.Events.turnError( new RuntimeException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveUniverse @ BinarySqlPersistence::saveData - {0} ; {1}",e.Message,connString ) ) );
				return GetFromFile(parameters); 
			} finally {
				conn.Close();
			}

			return null;
		}

		#endregion

		#region Private

		private void PersistToFile( byte[] data, PersistenceParameters p ){
			base.saveData(data, p);
		}

		private FileStream GetFromFile( PersistenceParameters parameters){
			return (FileStream) base.loadData(parameters);
		}

		private string GetConnectionString(PersistenceParameters p) {
			if( p == null ) {
				return connString;			
			}
			connString = p.GetParameter( "ConnectionStringPG" ).ToString( );
			return connString;
		}

	}

	#endregion
};
