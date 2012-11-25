// created on 10/14/2004 at 2:42 PM

using Chronos.Actions;
using Chronos.Utils;

namespace Chronos.Info.Results {

	public class ActionItem : ResultItem {
	
		#region InstanceFields
		
		private Action source;
		private bool passed;
		private int quantity;
		
		#endregion
	
		#region Ctor
	
		/// <summary>Construtor<sumary>
		public ActionItem( Action a, int q, bool p )
		{
			source = a;
			passed = p;
			quantity = q;
		}
	
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica a Action</summary>
		public Action Source {
			get { return source; }
		}
		
		/// <summary>Indica se este Item passou</summary>
		public bool Passed {
			get {
				return passed;
			}
		}
		
		/// <summary>Indica se este Item falhou</summary>
		public bool Failed {
			get {
				return !Passed;
			}
		}
		
		#endregion
	
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return (Failed ? "Failed: " : "Passed: ") + source.log();
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				Log.log(Source.log());
				return Source.getParams(quantity);
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return Source.Name; }
		}

		#endregion
	};
}
