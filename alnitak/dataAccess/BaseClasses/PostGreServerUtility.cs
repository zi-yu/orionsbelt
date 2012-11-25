using System;
using System.Data;
using System.Collections;
using Alnitak.Exceptions;
using Npgsql;

namespace Alnitak.PostGre {
	
	/// <summary>
	/// Classe que contem metodos de acesso a BD
	/// </summary>
	public class PostGreServerUtility {

		public static DataSet getAllFromDB( string procedure ) {

			NpgsqlDataAdapter NpgsqlDataAdapter = new NpgsqlDataAdapter( procedure , OrionGlobals.getConnectionString("connectionStringPG") );
            NpgsqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
			NpgsqlDataAdapter.SelectCommand.CommandTimeout = 0;

            DataSet dataSet = new DataSet();
			try{
				NpgsqlDataAdapter.Fill( dataSet );
			} catch( NpgsqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ PostGreServerUtility::getAllFromDB - {1}",procedure,e.Message),e);
			}
			if( dataSet.Tables.Count == 0 )
				throw new AlnitakException( procedure + " no retornou nenhum valor @ PostGreServerUtility::getAllFromDB");

			return dataSet;
		}

		public static DataSet getFromDB( string procedure, PostGreParam[] param ) {
			if( param == null )
				throw new AlnitakException("Tem de passar parmetros ao SP @ PostGreServerUtility::getFromDB");

			NpgsqlDataAdapter NpgsqlDataAdapter = new NpgsqlDataAdapter( procedure , OrionGlobals.getConnectionString("connectionStringPG") );
			if( procedure.ToLower().IndexOf("select") == -1 ) {
				NpgsqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
			}

			NpgsqlDataAdapter.SelectCommand.CommandTimeout = 0;
			
			int i = 0;
			foreach(PostGreParam p in param ) {
				NpgsqlParameter pa = NpgsqlDataAdapter.SelectCommand.Parameters.Add("$"+ i++, p.Type );
				if(p.HasSize) {
					pa.Size = p.Size;
				}
				pa.Value = p.Value;
			}
				
			
            DataSet dataSet = new DataSet();
			try {
				NpgsqlDataAdapter.Fill( dataSet );
			} catch( NpgsqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ PostGreServerUtility::getFromDB - {1}",procedure,e.Message),e);
			}

			if( dataSet.Tables.Count == 0 )
				throw new AlnitakException( procedure + " no retornou nenhum valor.");

			return dataSet;
		}

		public static DataSet getFromDBWithQuery( string query ) {
			
			NpgsqlDataAdapter NpgsqlDataAdapter = new NpgsqlDataAdapter( query , OrionGlobals.getConnectionString("connectionStringPG") );
			
			NpgsqlDataAdapter.SelectCommand.CommandTimeout = 0;
			
			DataSet dataSet = new DataSet();
			try {
				NpgsqlDataAdapter.Fill( dataSet );
			} catch( NpgsqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ PostGreServerUtility::getFromDB - {1}",query,e.Message),e);
			}

			if( dataSet.Tables.Count == 0 )
				throw new AlnitakException( query + " no retornou nenhum valor.");

			return dataSet;
		}

		public static bool checkResults( string procedure, Hashtable param ) {
			NpgsqlConnection conn = new NpgsqlConnection(OrionGlobals.getConnectionString("connectionStringPG"));
			NpgsqlCommand cmd = new NpgsqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			if( param != null ) {
				IDictionaryEnumerator iter = param.GetEnumerator();
				while( iter.MoveNext() )
					cmd.Parameters.Add( (string)iter.Key, iter.Value );
			}

			try {
				conn.Open();
				NpgsqlDataReader dr = cmd.ExecuteReader();
				if( dr.Read()) {
					return  !(dr[0] is DBNull);
				}
				
				return false;
			} catch( NpgsqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ PostGreServerUtility::checkResults - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}

		public static void executeNonQuery( string procedure, Hashtable param ) {
			NpgsqlConnection conn = new NpgsqlConnection(OrionGlobals.getConnectionString("connectionStringPG"));
			NpgsqlCommand cmd = new NpgsqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			if( param != null ) {
				IDictionaryEnumerator iter = param.GetEnumerator();
				while( iter.MoveNext() ) {
					if(iter.Value.GetType() == typeof(string)) {
						cmd.Parameters.Add( (string)iter.Key, (string)iter.Value );
					}else {
						PostGreParam p = (PostGreParam)iter.Value;
						NpgsqlParameter pa;
						if(p.HasSize) {
							pa = cmd.Parameters.Add( (string)iter.Key , p.Type, p.Size  );
						}else {
							pa = cmd.Parameters.Add( (string)iter.Key , p.Type  );
						}
						pa.Value = p.Value;
					}
				}	
			}
						
			try {

				conn.Open();
				cmd.ExecuteNonQuery();

			} catch( NpgsqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ PostGreServerUtility::executeNonQuery - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}

		public static void executeNonQuery2( string procedure, PostGreParam[] param ) {
			NpgsqlConnection conn = new NpgsqlConnection(OrionGlobals.getConnectionString("connectionStringPG"));
			NpgsqlCommand cmd = new NpgsqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			if( param != null ) {
				int i = 0;
				foreach( PostGreParam p in param ) {
					NpgsqlParameter pa = cmd.Parameters.Add( "$"+ i++ , p.Type  );
					if(p.HasSize) {
						pa.Size = p.Size;
					}
					pa.Value = p.Value;
				}
			}
						
			try {

				conn.Open();
				cmd.ExecuteNonQuery();

			} catch( NpgsqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ PostGreServerUtility::executeNonQuery - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}


		public static void executeNonQuery( string procedure, string paramname, object param ) {
			NpgsqlConnection conn = new NpgsqlConnection(OrionGlobals.getConnectionString("connectionStringPG"));
			NpgsqlCommand cmd = new NpgsqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			cmd.Parameters.Add( paramname, param );

			try {

				conn.Open();
				cmd.ExecuteNonQuery();

			} catch( NpgsqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ PostGreServerUtility::executeNonQuery - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}

		public static void executeNonQuery( string procedure ) {
			NpgsqlConnection conn = new NpgsqlConnection(OrionGlobals.getConnectionString("connectionStringPG"));
			NpgsqlCommand cmd = new NpgsqlCommand(procedure, conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;

			try {

				conn.Open();
				cmd.ExecuteNonQuery();

			} catch( NpgsqlException e ) {
				throw new AlnitakException(String.Format("Excepcao a correr o SP '{0}' @ PostGreServerUtility::executeNonQuery - {1}",procedure,e.Message),e);
			} finally {
				conn.Close();
			}
		}
	}
}
