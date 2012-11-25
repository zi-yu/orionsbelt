// created on 8/12/2005 at 2:03 PM

using Chronos.Core;

namespace Chronos.Info.Results {
	
	public class InvalidOperation : ResultItem {
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Invalid Operation";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[0];
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "InvalidOperation"; }
		}
		
		#endregion
		
	};
	
}
