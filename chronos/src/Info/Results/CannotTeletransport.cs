// created on 6/16/2004 at 10:44 AM

namespace Chronos.Info.Results {
	
	public class CannotTeletransport : ResultItem {
		
		#region InstanceFields
		
		private string resource;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor<sumary>
		public CannotTeletransport( string res )
		{
			resource = res;
		}
		
		#endregion
		
		#region Instance Properties}
		
		/// <summary>Indica o recurso</summary>
		public string Resource {
			get { return resource; }
		}
		
		#endregion
		
		#region Inherited Methods
		
		/// <summary>Retorna uma mensagem que identifica o Item</summary>
		public override string log()
		{
			return "'" + Resource + "' it's not teletransportable";
		}
		
		/// <summary>Indica um array com os campos importantes deste item</summary>
		public override string[] Params {
			get {
				return new string[] { Resource, "Intrinsic" };
			}
		}
		
		/// <summary>Indica o nome deste Item</summary>
		public override string Name {
			get { return "CannotTeletransport"; }
		}
		
		#endregion
		
	};
	
}
