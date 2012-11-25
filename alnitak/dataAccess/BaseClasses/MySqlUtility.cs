// created on 3/29/04 at 9:49 a

using System;
using System.Data;
using ByteFX.Data;
using ByteFX.Data.MySqlClient;

namespace Alnitak {

	internal class MySqlUtility {

		/// <summary>Retorna todos os tuplos de uma tabela</summary>
		internal static DataSet getAll( string table )
		{
			string connectionString = OrionGlobals.getConnectionString("connectiostring-mysql");

			string query = "select * from " + table;
			DataSet dsSections = null;
		
			try {
				MySqlDataAdapter sections = new MySqlDataAdapter( query, connectionString );
            
				dsSections = new DataSet();

				sections.Fill( dsSections );
			} catch( Exception e ) {
				Chronos.Utils.Log.log("Connection String: {0}", connectionString);
				Chronos.Utils.Log.log("Error: " + e.Message);
				throw;
			}
			
			return dsSections;
		}
		
		/// <summary>Retorna todos os tuplos de uma tabela</summary>
		internal static DataSet getAll( string table, string query )
		{
			string connectionString = OrionGlobals.getConnectionString("connectiostring-mysql");

			string queryString = "select * from " + table +" " + query;
			DataSet dsSections = null;
		
			try {
				MySqlDataAdapter sections = new MySqlDataAdapter( queryString, connectionString );
            
				dsSections = new DataSet();

				sections.Fill( dsSections );
			} catch( Exception e ) {
				Chronos.Utils.Log.log("Connection String: {0}", connectionString);
				Chronos.Utils.Log.log("Error: " + e.Message);
				throw;
			}
			
			return dsSections;
		}
		
		/// <summary>Retorna todos os tuplos de uma tabela</summary>
		internal static DataSet getQuery( string query )
		{
			string connectionString = OrionGlobals.getConnectionString("connectiostring-mysql");

			string queryString = query;
			DataSet dsSections = null;
		
			try {
				MySqlDataAdapter sections = new MySqlDataAdapter( queryString, connectionString );
            
				dsSections = new DataSet();

				sections.Fill( dsSections );
			} catch( Exception e ) {
				Chronos.Utils.Log.log("Connection String: {0}", connectionString);
				Chronos.Utils.Log.log("Error: " + e.Message);
				throw;
			}
			
			return dsSections;
		}
		
		/// <summary>l um valor</summary>
		internal static int executeNonQuery( string query )
		{
			string connectionString = OrionGlobals.getConnectionString("connectiostring-mysql");
			MySqlConnection conn = new MySqlConnection(connectionString);
			
			try {
				conn.Open();
				
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;
				cmd.CommandType = CommandType.Text;
				
				object obj = cmd.ExecuteScalar();
				if( obj == null ) {
					return 0;
				}
				return int.Parse(obj.ToString());
				
			} catch( Exception e ) {
				Chronos.Utils.Log.log("Connection String: {0}", connectionString);
				Chronos.Utils.Log.log("Error: " + e.Message);
				throw;
			} finally {
				conn.Close();
			}
		}
		
	};

};
