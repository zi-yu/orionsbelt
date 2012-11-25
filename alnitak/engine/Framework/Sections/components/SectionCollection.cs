namespace Alnitak {

	using System;
	using System.Collections;
	
	/// <summary>
	/// Armazena objectos do tipo SectionInfo
	/// </summary>
	public class SectionCollection : UtilityCollection, IComparer{

		ArrayList orderedSections;
						
		#region inherit methods

			/// <summary>
			/// mtodo herdado de ICompare para comparar dois objectos
			/// </summary>
			/// <param name="x">primeiro objecto a comparar</param>
			/// <param name="y">segundo objecto a comparar</param>
			/// <returns>menos que zero se x  menor que y
			/// zero se x  igual a y
			/// mais que zero se x  maior y</returns>
			public System.Int32 Compare ( object x , object y ) 
			{
				return ((SectionInfo)x).sectionOrder - ((SectionInfo)y).sectionOrder;
			}

		#endregion
		
		#region overrided methods
			
			/// <summary>
			/// Adiciona uma nova seco  <code>collection</code> de
			/// seces
			/// </summary>
			/// <param name="key"></param>
			/// <param name="value"></param>
			public void Add(string key, object value) {
				base.Add (key, value);
				orderedSections.Add(value);
			}

		#endregion
		
		#region public methods

			/// <summary>
			/// obtm todas seces ordenadas
			/// </summary>
			/// <returns>um <code>Arraylist</code> com as seces</returns>
			public ArrayList getOrderedSections() {
				orderedSections.Sort(0,orderedSections.Count,this);
				return orderedSections;
			}

			/// <summary>
			/// Construtor
			/// </summary>
			public SectionCollection() {
				orderedSections = new ArrayList();
			}

		#endregion

	}
}
