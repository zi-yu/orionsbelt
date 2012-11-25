// created on 3/29/04 at 9:49 a

using System;
using System.Data;
using System.Web;
using ByteFX.Data;
using ByteFX.Data.MySqlClient;
using Chronos.Utils;

namespace Alnitak {

	internal class MySqlExceptionLogUtility : ExceptionLogUtility {

		#region Private - Podes retirar se quiseres,  so para facilitar e para a msg de erro ser melhor

		private DataRow _exception = null;

		private object getField( string columnName ) {
			try {
				return _exception[columnName];
			} catch {
				throw new Exception( string.Format( "Campo {0} no na tabela ExceptionLog !", columnName ) );
			}
		}

		#endregion

		#region Ctor and Instance Fields

		private MySqlConnection conn;
	
		/// <summary>Construtor</summary>
		public MySqlExceptionLogUtility()
		{
			conn = new MySqlConnection( OrionGlobals.getConnectionString("connectiostring-mysql") );
		}

		#endregion

		#region ExceptionLogUtility Implementation
		
		/// <summary>
		/// Salva a informao da excepo
		/// </summary>
		/// <param name="exceptionInfo">objecto que encapsula a informao da excepo</param>
		public override void save( ExceptionInfo exceptionInfo )
		{
			try {

				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = "INSERT INTO Exceptions VALUES('',@Name, @Message, @Stacktrace, @Date)";
				cmd.Parameters.Add("@Name", exceptionInfo.Name );
				cmd.Parameters.Add("@Message", exceptionInfo.Message);
				cmd.Parameters.Add("@Stacktrace", exceptionInfo.StackTrace);
				cmd.Parameters.Add("@Date", exceptionInfo.Date);
				
				MySqlDataReader reader = cmd.ExecuteReader();
				reader.Read();

			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.Message);
				Log.log(e.Message);
			} finally {
				conn.Close();
			}

		}

		/// <summary>
		/// carrega todas as excepes ocorridas para um array de excepes
		/// </summary>
		/// <returns></returns>
		public override ExceptionInfo[] load()
		{
			DataSet dataset = MySqlUtility.getAll("Exceptions", "ORDER BY Id DESC LIMIT 0, 30");

			int i = 0, count = dataset.Tables[0].Rows.Count;

			ExceptionInfo[] exceptions = new ExceptionInfo[count];
			
			foreach( DataRow row in dataset.Tables[0].Rows ) {
				_exception = row;
				exceptions[i++] = new ExceptionInfo(
					(int)getField("id"),
					(string)getField("Name"),
					(DateTime)getField("Date"),
					(string)getField("Message"),
					(string)getField("StackTrace")
				);
			}

			return exceptions;
		}

		/// <summary>
		/// remove a excepo com o identificador id
		/// </summary>
		/// <param name="id">identifucador da excepo</param>
		public override void remove( int id )
		{
			try {

				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = "DELETE FROM Exceptions WHERE Id = `"+id+"`)";
				
				MySqlDataReader reader = cmd.ExecuteReader();
				reader.Read();

			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.Message);
				Log.log(e.Message);
			} finally {
				conn.Close();
			}

		}

		/// <summary>
		/// remove todas as excepes
		/// </summary>
		public override void removeAll()
		{
			MySqlUtility.executeNonQuery("DELETE FROM `Exceptions`");
		}

		#endregion
	};

};
