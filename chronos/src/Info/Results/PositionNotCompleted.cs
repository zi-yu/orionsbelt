namespace Chronos.Info.Results {

	public class PositionNotCompleted : ResultItem {
	
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public PositionNotCompleted() {}
		
		#endregion
		
		#region Instance Properties}
		
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Not all units were positioned";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[0];
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "PositionNotCompleted"; }
		}
		
		#endregion

	};
}