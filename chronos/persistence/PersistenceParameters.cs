
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
		
		/// <summary>Regista um par�metro</summary>
		public void Register( string key, string val )
		{
			parameters.Add(key, val);
		}
		
		/// <summary>Obt�m um par�metro</summary>
		public string GetParameter( string key )
		{
			return (string) parameters[key];
		}
		
		#endregion
		
	};
}
