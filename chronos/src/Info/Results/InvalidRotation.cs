// created on 6/16/2004 at 10:48 AM

using Chronos.Actions;

namespace Chronos.Info.Results {

	public class InvalidRotation : ResultItem {
	
		#region InstanceFields
		
		private string _pos;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public InvalidRotation( string pos ) {
			_pos = pos;
		}
		
		#endregion
		
		#region Instance Properties}
		
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Invalid Rotation:" + _pos;
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[] { _pos };
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "InvalidRotation"; }
		}
		
		#endregion

	};
}