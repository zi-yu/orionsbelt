namespace Alnitak {

	using System;
	using System.Collections;

	using System.Data;
	using System.Data.SqlClient;

	using Alnitak.MsSqlServer;
	using Alnitak.Exceptions;

	/// <summary>
	/// interface que implementa o método para salvar o log 
	/// </summary>
	public class SqlServerExceptionLogUtility : ExceptionLogUtility {
		
		#region Private

		private DataRow _exception = null;

		private object getField( string columnName ) {
			try {
				return _exception[columnName];
			} catch {
				throw new AlnitakException( string.Format( "Campo {0} no existe não procedimento ou na tabela ExceptionLog !", columnName ) );
			}
		}

		#endregion

		#region Public
		
		/// <summary>
		/// Salva a informação da excepção
		/// </summary>
		/// <param name="exceptionInfo">objecto que encapsula a informação da excepção</param>
		public override void save( ExceptionInfo exceptionInfo ) {
			Hashtable parameters = new Hashtable();
			parameters.Add( "@name" , exceptionInfo.Name );
			parameters.Add( "@message" , exceptionInfo.Message );
			parameters.Add( "@stackTrace" , exceptionInfo.StackTrace );
			parameters.Add( "@date" , exceptionInfo.Date );

			SqlServerUtility.executeNonQuery( "OrionsBelt_ExceptionLogSave", parameters );
		}

		/// <summary>
		/// carrega todas as excepções ocorridas para um array de excepções
		/// </summary>
		/// <returns></returns>
		public override ExceptionInfo[] load() {
			DataSet dataset = SqlServerUtility.getAllFromDB("OrionsBelt_ExceptionLogLoad");
			
			int i = 0, count = dataset.Tables[0].Rows.Count;

			ExceptionInfo[] exceptions = new ExceptionInfo[count];
			
			foreach( DataRow row in dataset.Tables[0].Rows ) {
				_exception = row;
                exceptions[i++] = new ExceptionInfo(
					(int)getField("exceptionLog_id"),
					(string)getField("exceptionLog_name"),
					(DateTime)getField("exceptionLog_date"),
					(string)getField("exceptionLog_message"),
					(string)getField("exceptionLog_stackTrace")
				);
			}

			return exceptions;
		}

		/// <summary>
		/// remove a excepção com o identificador id
		/// </summary>
		/// <param name="id">identifucador da excepção</param>
		public override void remove( int id ) {
			SqlServerUtility.executeNonQuery("OrionsBelt_ExceptionLogRemoveException","@id",id);
		}
		
		/// <summary>
		/// remove todas as excepções
		/// </summary>
		public override void removeAll() {
			SqlServerUtility.executeNonQuery("OrionsBelt_ExceptionLogRemoveAllExceptions");
		}

		#endregion

	}
}
