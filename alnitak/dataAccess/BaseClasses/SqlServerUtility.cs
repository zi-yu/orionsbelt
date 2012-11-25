namespace Alnitak.MsSqlServer {
	
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Collections;

	using Alnitak.Exceptions;
	

	/// <summary>
	/// Classe que contem metodos de acesso a BD
	/// </summary>
	public class SqlServerUtility {

		public static DataSet getAllFromDB( string procedure ) {
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter( procedure , OrionGlobals.getConnectionString("connectionString") );
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
			sqlDataAdapter.SelectCommand.CommandTimeout = 0;

            DataSet dataSet = new DataSet();
			try{
				sqlDataAdapter.Fill( dataSet );
			} catch( SqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ SqlServerUtility::getAllFromDB - {1}",procedure,e.Message),e);
			}
			if( dataSet.Tables.Count == 0 )
				throw new AlnitakException( procedure + " não retornou nenhum valor @ SqlServerUtility::getAllFromDB");

			return dataSet;
		}

		public static DataSet getFromDB( string procedure, Hashtable param ) {
			if( param.Count == 0 )
				throw new AlnitakException("Tem de passar parâmetros ao SP @ SqlServerUtility::getFromDB");

			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter( procedure , OrionGlobals.getConnectionString("connectionString") );
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
			sqlDataAdapter.SelectCommand.CommandTimeout = 0;
			
			IDictionaryEnumerator iter = param.GetEnumerator();

			while( iter.MoveNext() )
				sqlDataAdapter.SelectCommand.Parameters.Add( (string)iter.Key, iter.Value );
			
            DataSet dataSet = new DataSet();
			try {
				sqlDataAdapter.Fill( dataSet );
			} catch( SqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ SqlServerUtility::getFromDB - {1}",procedure,e.Message),e);
			}


			if( dataSet.Tables.Count == 0 )
				throw new AlnitakException( procedure + " não retornou nenhum valor.");

			return dataSet;
		}

		public static bool checkResults( string procedure, Hashtable param ) {
			SqlConnection conn = new SqlConnection(OrionGlobals.getConnectionString("connectionString"));
			SqlCommand cmd = new SqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;
			
			if( param != null ) {
				IDictionaryEnumerator iter = param.GetEnumerator();
				while( iter.MoveNext() )
					cmd.Parameters.Add( (string)iter.Key, iter.Value );
			}

			try {

				conn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				return dr.HasRows;

			} catch( SqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ SqlServerUtility::checkResults - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}

		public static void executeNonQuery( string procedure, Hashtable param ) {
			SqlConnection conn = new SqlConnection(OrionGlobals.getConnectionString("connectionString"));
			SqlCommand cmd = new SqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			if( param != null ) {
				IDictionaryEnumerator iter = param.GetEnumerator();
				while( iter.MoveNext() )
					cmd.Parameters.Add( (string)iter.Key, iter.Value );
			}
						
			try {

				conn.Open();
				cmd.ExecuteNonQuery();

			} catch( SqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ SqlServerUtility::executeNonQuery - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}

		public static void executeNonQuery( string procedure, string paramname, object param ) {
			SqlConnection conn = new SqlConnection(OrionGlobals.getConnectionString("connectionString"));
			SqlCommand cmd = new SqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( paramname, param );

			try {

				conn.Open();
				cmd.ExecuteNonQuery();

			} catch( SqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ SqlServerUtility::executeNonQuery - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}

		public static void executeNonQuery( string procedure ) {
			SqlConnection conn = new SqlConnection(OrionGlobals.getConnectionString("connectionString"));
			SqlCommand cmd = new SqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			try {

				conn.Open();
				cmd.ExecuteNonQuery();

			} catch( SqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ SqlServerUtility::executeNonQuery - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}
	}
}
