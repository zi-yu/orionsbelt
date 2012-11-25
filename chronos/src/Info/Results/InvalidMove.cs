// created on 6/16/2004 at 10:48 AM

using Chronos.Actions;

namespace Chronos.Info.Results {

	public class InvalidMove : ResultItem {
	
		#region InstanceFields
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public InvalidMove() {
		}
		
		#endregion
		
		#region Instance Properties}
		
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Invalid Move Detected!";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[0];
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "InvalidMove"; }
		}
		
		#endregion

	};
}