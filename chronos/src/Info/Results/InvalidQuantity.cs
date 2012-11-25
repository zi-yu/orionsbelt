// created on 8/1/2005 at 10:51 AM

namespace Chronos.Info.Results {

	public class InvalidQuantity : ResultItem {
	
		#region Ctor
	
		/// <summary>Construtor<sumary>
		public InvalidQuantity()
		{
		}
	
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Invalid Quantity";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[0];
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "InvalidQuantity"; }
		}
		
		#endregion
	
	};

}