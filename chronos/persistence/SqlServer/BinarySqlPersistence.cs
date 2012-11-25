using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Chronos.Exceptions;

namespace Chronos.Persistence.SqlServer {

	/// <summary>
	/// Summary description for XmlPersistence.
	/// </summary>
	public class BinarySqlPersistence : FilePersistence {
	
		#region Instance Fields
		
		private string connString;
		
		#endregion
	
		#region Ctors

		/// <summary>Ctor</summary>
		public BinarySqlPersistence( ) : base("Universe.bin") {
		
		}

		/// <summary>Ctor</summary>
		public BinarySqlPersistence( string conn ) : base("Universe.bin"){
			connString = conn;
		}
		
		#endregion

		#region UniverseSerializer Implementation
	
		/// <summary>Armazena uma Stream com persistncia</summary>
		protected override void saveData( byte[] data, PersistenceParameters parameters )
		{
			SqlConnection conn = new SqlConnection(GetConnectionString(parameters));
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosSaveUniverse", conn);
			cmd.CommandTimeout = 0;
			cmd.CommandType=CommandType.StoredProcedure;

			cmd.Parameters.Add( "@data", data );

			try {
				conn.Open();
				cmd.ExecuteNonQuery();
				
				PersistToFile( data,parameters );
				
			} catch( SqlException e ) {
				throw new RuntimeException( String.Format("Excepcao a correr o SP OrionsBelt_ChronosSaveUniverse @ BinarySqlPersistence::saveData - {0}",e.Message) );
			} finally {
				conn.Close();
			}
		}

		/// <summary>Carrega uma Stream com um Universo</summary>
		protected override Stream loadData(PersistenceParameters parameters)
		{
			SqlConnection conn = new SqlConnection(GetConnectionString(parameters));
			SqlCommand cmd = new SqlCommand("OrionsBelt_ChronosLoadUniverse", conn);
			cmd.CommandTimeout = 0;
			cmd.CommandType=CommandType.StoredProcedure;
			
			try {
				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				if( dr.Read() ) {
					return new MemoryStream( (byte[])dr["data"] );
				}else{
					if( System.IO.File.Exists( Path.Combine(GetPath(parameters),"universe.bin") )){
						return GetFromFile(parameters);
					}
				}
			} catch( SqlException e ) {
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
			connString = p.GetParameter( "ConnectionString" ).ToString( );
			return connString;
		}

		#endregion
	};
}
