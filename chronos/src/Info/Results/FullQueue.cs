// created on 6/16/2004 at 10:40 AM

namespace Chronos.Info.Results {

	public class FullQueue : ResultItem {
	
		#region InstanceFields
		
		private int capacity;
		private int current;
		
		#endregion
	
		#region Ctor
	
		/// <summary>Construtor<sumary>
		public FullQueue( int cap, int curr )
		{
			capacity = cap;
			current = curr;
		}
	
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica a capacidade máxima</summary>
		public int Capacity {
			get { return capacity; }
		}
		
		/// <summary>Indica a capacidade máxima</summary>
		public int Current {
			get { return current; }
		}
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Failed: Full Queue(max is " + Capacity + " and we already got " + Current + ")";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[] { Capacity.ToString(), Current.ToString() };
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "FullQueue"; }
		}
		
		#endregion
	
	};

}