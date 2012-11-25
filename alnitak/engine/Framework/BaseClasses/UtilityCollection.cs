namespace Alnitak {

	using System.Collections;
	
	/// <summary>
	/// 
	/// </summary>
	public class UtilityCollection : Hashtable {
									
		#region overrided methods

			/// <summary>
			/// redefini��o para n�o haver repeti��es
			/// </summary>
			public override object this[object key] {
				set {
					if(this.ContainsKey(key))
						return;
					base[key] = value;
				}
			}
			
		#endregion
	}
}

