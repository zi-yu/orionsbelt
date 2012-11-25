// created on 3/21/2006 at 12:44 PM

namespace Chronos.Info.Results {
	
	public class CanNotDemolish : ResultItem {
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public CanNotDemolish()
		{
		}
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Can not Demolish";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[0];
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "CanNotDemolish"; }
		}
		
		#endregion
		
	};
	
}
