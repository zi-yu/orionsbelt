// created on 6/16/2004 at 10:47 AM

using Chronos.Actions;

namespace Chronos.Info.Results {

	public class ActionPassed : ActionItem {
	
		#region Ctor
	
		/// <summary>Construtor<sumary>
		public ActionPassed( Action a, int quantity ) : base(a, quantity, true)
		{
		}
	
		#endregion
	
	};
}