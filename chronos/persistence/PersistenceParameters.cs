
using System;
using System.Collections;

namespace Chronos.Persistence {
	
	public class PersistenceParameters {
		
		#region Instance Fields
		
		private Hashtable parameters;
		
		#endregion
		
		#region Ctor
		
		public PersistenceParameters()
		{
			parameters = new Hashtable();
		}
		
		#endregion
		
		#region Public Access
		
		/// <summary>Regista um parâmetro</summary>
		public void Register( string key, string val )
		{
			parameters.Add(key, val);
		}
		
		/// <summary>Obtém um parâmetro</summary>
		public string GetParameter( string key )
		{
			return (string) parameters[key];
		}
		
		#endregion
		
	};
}
