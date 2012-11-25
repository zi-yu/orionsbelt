// created on 6/16/2004 at 10:48 AM

using Chronos.Actions;

namespace Chronos.Info.Results {

	public class MinimumRest : ResultItem {
		private string _quant;
		private string _type;
		private string _min;

		#region InstanceFields
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public MinimumRest( string quant, string type, string min ) {
			_quant = quant;
			_type = type;
			_min = min;
		}
		
		#endregion
		
		#region Instance Properties}
		
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log() {
			return string.Format("You must leave at least 20% of the ships in a group. You left {0} {1} when you should have left at least {2}!",_quant,_type,_min);
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[3]{_quant,_type,_min};
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "MinimumRest"; }
		}
		
		#endregion

	};
}