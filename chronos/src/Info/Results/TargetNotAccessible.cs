// created on 6/16/2004 at 10:44 AM
using Chronos.Core;

namespace Chronos.Info.Results {
	
	public class TargetNotAccessible : ResultItem {
		
		#region InstanceFields
		
		private Coordinate coordinate;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public TargetNotAccessible( Coordinate coord )
		{
			coordinate = coord;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica a coordenada alvo</summary>
		public Coordinate Target {
			get { return coordinate; }
		}
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "Target not accessible";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[] { Target.ToString() };
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "TargetNotAccessible"; }
		}
		
		#endregion
		
	};
	
}
