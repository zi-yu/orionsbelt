// created on 6/16/2004 at 10:44 AM

namespace Chronos.Info.Results {

	public class FactoryNotAvailable : ResultItem {
	
		#region InstanceFields
		
		private string resource;
		private string category;
		
		#endregion
	
		#region Ctor
	
		/// <summary>Construtor<sumary>
		public FactoryNotAvailable( string cat, string res )
		{
			category = cat;
			resource = res;
		}
	
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica a categoria</summary>
		public string Category {
			get { return category; }
		}
		
		/// <summary>Indica o recurso</summary>
		public string Resource {
			get { return resource; }
		}
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Failed: Factory '" + Resource + "' not available in the '" + Category + "' category";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[] { Resource, Category };
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "FactoryNotAvailable"; }
		}
		
		#endregion
	
	};

}