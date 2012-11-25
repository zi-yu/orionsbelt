// created on 6/16/2004 at 10:40 AM

namespace Chronos.Info.Results {
	
	public class PlanetInBattle : ResultItem {
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public PlanetInBattle()
		{
		}
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Planet in battle";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[0];
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "PlanetInBattle"; }
		}
		
		#endregion
		
	};
	
}
