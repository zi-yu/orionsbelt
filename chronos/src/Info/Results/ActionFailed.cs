// created on 6/16/2004 at 10:48 AM

using Chronos.Actions;

namespace Chronos.Info.Results {

	public class ActionFailed : ActionItem {
	
		#region Ctor
	
		/// <summary>Construtor<sumary>
		public ActionFailed( Action a, int quantity ) : base(a, quantity, false)
		{
		}
	
		#endregion

	};
}