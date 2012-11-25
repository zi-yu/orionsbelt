// created on 6/16/2004 at 10:21 AM

using System;
using System.Collections;
using System.IO;

namespace Chronos.Info.Results {

	/// <summary>Entidade que representa o resultado de uma acção</summary>
	public class Result {
	
		#region Static Fields
		
		private static ResultItem[] dummy = new ResultItem[0];
		
		#endregion
	
		#region Instance Fields
		
		private ArrayList passedList;
		private ArrayList failedList;
		
		#endregion
		
		#region Ctors
		
		/// <summary>Construtor</summary>
		public Result()
		{
			passedList = null;
			failedList = null;
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>Indica se o resultado foi com sucesso ou não</summary>
		public bool Ok {
			get { 
				return failedList == null || failedList.Count == 0;  
			}
		}
		
		/// <summary>Indica o total de Items deste Result</summary>
		public int Total {
			get {
				return FailedCount + PassedCount;
			}
		}
		
		/// <summary>Indica o total de items que falharam</summary>
		public int FailedCount {
			get {
				return failedList == null ? 0 : failedList.Count; 
			}
		}	
		
		/// <summary>Indica o total de items que passaram</summary>
		public int PassedCount {
			get {
				return passedList == null ? 0 : passedList.Count; 
			}
		}
		
		/// <summary>Indica todos os ResultItems que passaram</summary>
		public IEnumerable Passed {
			get {
				if (passedList == null ){
					return dummy;
				} else {
					return passedList;
				}
			}
		}
		
		/// <summary>Indica todos os ResultItems que falharam</summary>
		public IEnumerable Failed {
			get {
				if (failedList == null ){
					return dummy;
				} else {
					return failedList;
				}
			}
		}
		
		#endregion
		
		#region Instance Methods
		
		/// <summary>Adiciona um ResultItem ao contentor de items passados</summary>
		public void passed( ResultItem item )
		{
			if( passedList == null ) {
				passedList = new ArrayList();
			}
			
			passedList.Add(item);
		}
		
		/// <summary>Adiciona um ResultItem ao contentor de items que falharam</summary>
		public void failed( ResultItem item )
		{
			if( failedList == null ) {
				failedList = new ArrayList();
			}
			
			failedList.Add(item);
		}
		
		#endregion
		
		#region UtilityMethods
		
		public string log()
		{
			StringWriter writer = new StringWriter();
			
			int pass = PassedCount;
			int fail = FailedCount;
			
			writer.WriteLine("Total: " + (pass + fail) );
			writer.WriteLine("Passed: " + pass + " Failed: " + fail);
			
			foreach( ResultItem item in Passed ) {
				writer.WriteLine(item.log());
			}
			
			foreach( ResultItem item in Failed ) {
				writer.WriteLine(item.log());
			}
			
			return writer.ToString();
		}
		
		#endregion
	
	};

};