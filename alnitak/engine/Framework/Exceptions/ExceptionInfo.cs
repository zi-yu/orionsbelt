namespace Alnitak {

	using System;
	using System.Data;

	using Alnitak.Exceptions;

	/// <summary>
	/// Summary description for ExceptionInfo.
	/// </summary>
	public class ExceptionInfo {

		private int _id;
		private string _name;
		private string _message;
		private string _stackTrace;
		private DateTime _date;

		#region properties

		public int Id {
			get{ return _id; }
			set{ _id = value; }
		}

		public string Name {
			get{ return _name; }
			set{ _name = value; }
		}

		public string StackTrace {
			get{ return _stackTrace; }
			set{ _stackTrace = value; }
		}

		public string Message {
			get{ return _message; }
			set{ _message = value; }
		}

		public DateTime Date {
			get{ return _date; }
			set{ _date = value; }
		}

		#endregion

		#region construtores
		
		/// <summary>
		/// Construtor
		/// </summary>
		/// <param name="exception">Excepcao gerada</param>
		public ExceptionInfo( Exception exception ) {
			//Nome da Excepcao
			_name = exception.ToString().Substring(0, exception.ToString().IndexOf(":"));
			
			// Mensagem
			_message = exception.ToString( );

			// Build error message
			_stackTrace = exception.StackTrace.Replace(" at ","<br/> at ");

			_date = DateTime.Now;
		}
		
		/// <summary>
		/// Construtor
		/// </summary>
		/// <param name="exception">Excepcao gerada</param>
		public ExceptionInfo( string name, string message ) {
			//Nome da Excepcao
			_name = name;
			
			// Mensagem
			_message = message;

			// Build error message
			_stackTrace = "";

			_date = DateTime.Now;
		}

		/// <summary>
		/// Constroi um ExceptionInfo com base na informao da base de dados
		/// </summary>
		/// <param name="exception">row da base de dados</param>
		public ExceptionInfo( int id, string name, DateTime date, string message, string stackTrace ) {
			_id = id;
			_name = name;
			_date = date;
			_message = message;
			_stackTrace = stackTrace;
		}
		
		#endregion

	}
}
