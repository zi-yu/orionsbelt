// created on 6/16/2004 at 10:40 AM

namespace Chronos.Info.Results {

	public abstract class ResultItem {
	
		#region Instance Abstract Methods
	
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public abstract string log();
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public abstract string[] Params {get;}
		
		/// <summary>Indica o nome deste Item</summary>
		public abstract string Name {get;}
		
		#endregion
		
	};

}