namespace Chronos.Info.Results {

	public class InvalidCoordinate : ResultItem {
	
		#region InstanceFields
		
		private string coord;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public InvalidCoordinate( string coord ) {
			this.coord = coord;
		}
		
		#endregion
		
		#region Instance Properties}
		
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Invalid Coordinate:" + coord;
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[] { coord };
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "InvalidCoordinate"; }
		}
		
		#endregion

	};
}