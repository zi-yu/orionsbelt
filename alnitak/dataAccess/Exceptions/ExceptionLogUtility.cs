namespace Alnitak {

	using System;
	using System.Collections;

	/// <summary>
	/// interface que implementa o mtodo para salvar o log 
	/// </summary>
	public abstract class ExceptionLogUtility {
		
		public static string getExceptionLogKey(){
			return OrionGlobals.resolveDataAccessName("exceptionLog");
		}

		public abstract void save( ExceptionInfo execptionInfo );
		public abstract ExceptionInfo[] load( );
		public abstract void remove( int id );
		public abstract void removeAll();
	}
}
