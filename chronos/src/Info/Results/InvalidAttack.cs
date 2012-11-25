// created on 6/16/2004 at 10:48 AM

using Chronos.Actions;

namespace Chronos.Info.Results {

	public class InvalidAttack : ResultItem {
	
		#region InstanceFields

		private string _src;
		private string _dst;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public InvalidAttack( string src, string dst ) {
			_src = src;
			_dst = dst;
		}
		
		#endregion
		
		#region Instance Properties}
		
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return string.Format("Invalid Attack from coord {0} to coordinate {1}",_src,_dst);
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[] { _src, _dst };
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "InvalidAttack"; }
		}
		
		#endregion

	};
}