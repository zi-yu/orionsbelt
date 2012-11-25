using System;
using System.Collections;
using System.Runtime.Serialization;
using Chronos.Utils;

namespace Chronos.Resources {
	
	/// <summary>
	///  Contentor de ResourceFactory's
	/// </summary>
	[Serializable]
	public class ResourceBuilder : SortedList {
	
		private string appliesTo;
		
		/// <summary>
		///  Construtor
		/// </summary>
		public ResourceBuilder( string appliesTo ) 
			: base()
		{
			//comparer = CaseInsensitiveComparer.Default;
			this.appliesTo = appliesTo;
		}
		
		/// <summary>Retorna um resource com base no seu nome</summary>
		public Resource create( string name )
		{
			if( ContainsKey(name) ) {
				ResourceFactory factory = (ResourceFactory) this[name];
				return factory.create();
			}
			return null;
		}
		
		/// <summary>Indica a que é que este resource builder se aplica ('planet' ou 'ruler' por exemplo)</summary>
		public string AppliesTo 
		{
			get { return appliesTo; }
		}
		
		/// <summary>Realiza o clone deste resource builder (só referências).</summary>
		public override object Clone()
		{
			ResourceBuilder same = new ResourceBuilder(AppliesTo);
			
			IDictionaryEnumerator it = GetEnumerator();
			while( it.MoveNext() ) {
				same.Add( it.Key, it.Value );
			}
			
			return same;
		}

	};
	
}
