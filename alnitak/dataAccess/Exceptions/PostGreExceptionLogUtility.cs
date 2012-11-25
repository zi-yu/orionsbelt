using System;
using System.Collections;
using System.Data;
using Alnitak.Exceptions;
using Alnitak.PostGre;
using NpgsqlTypes;

namespace Alnitak {
	/// <summary>
	/// interface que implementa o mtodo para salvar o log 
	/// </summary>
	public class PostGreExceptionLogUtility : ExceptionLogUtility {
		
		#region Private

		private DataRow _exception = null;

		private object getField( int id ) {
			try {
				return _exception[id];
			} catch {
				throw new AlnitakException( string.Format( "Campo {0} no existe no procedimento ou na tabela ExceptionLog !", id ) );
			}
		}

		#endregion

		#region Public
		
		/// <summary>
		/// Salva a informao da excepo
		/// </summary>
		/// <param name="exceptionInfo">objecto que encapsula a informao da excepo</param>
		public override void save( ExceptionInfo exceptionInfo ) {
			PostGreParam[] parameters = new PostGreParam[4];
			parameters[0] = new PostGreParam( exceptionInfo.Name,NpgsqlDbType.Varchar, 100 );
			parameters[1] = new PostGreParam( exceptionInfo.Message,NpgsqlDbType.Varchar, 3000 );
			parameters[2] = new PostGreParam( exceptionInfo.StackTrace,NpgsqlDbType.Varchar, 3000 );
			parameters[3] = new PostGreParam( exceptionInfo.Date,NpgsqlDbType.Timestamp );

			PostGreServerUtility.executeNonQuery2( "OrionsBelt_ExceptionLogSave", parameters );
		}

		/// <summary>
		/// carrega todas as excepes ocorridas para um array de excepes
		/// </summary>
		/// <returns></returns>
		public override ExceptionInfo[] load() {
			//DataSet dataset = PostGreServerUtility.getAllFromDB("OrionsBelt_ExceptionLogLoad");
			string query = "SELECT * FROM OrionsBelt_ExceptionLog ORDER BY exceptionLog_date DESC;";
			DataSet dataset = PostGreServerUtility.getFromDBWithQuery(query);

			int i = 0, count = dataset.Tables[0].Rows.Count;

			ExceptionInfo[] exceptions = new ExceptionInfo[count];
			
			foreach( DataRow row in dataset.Tables[0].Rows ) {
				_exception = row;
                exceptions[i++] = new ExceptionInfo(
					(int)getField(0),
					(string)getField(1),
					(DateTime)getField(4),
					(string)getField(2),
					(string)getField(3)
				);
			}

			return exceptions;
		}

		/// <summary>
		/// remove a excepo com o identificador id
		/// </summary>
		/// <param name="id">identifucador da excepo</param>
		public override void remove( int id ) {
			PostGreServerUtility.executeNonQuery("OrionsBelt_ExceptionLogRemoveException","@id",id);
		}
		
		/// <summary>
		/// remove todas as excepes
		/// </summary>
		public override void removeAll() {
			PostGreServerUtility.executeNonQuery("OrionsBelt_ExceptionLogRemoveAllExceptions");
		}

		#endregion

	}
}
