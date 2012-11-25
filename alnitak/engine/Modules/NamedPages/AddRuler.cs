using System.Web;


namespace Alnitak {
	
	/// <summary>
	/// Frequently asked Questions
	/// </summary>
	public class AddRuler : BasePageModule {
		public AddRuler() {
			User user = HttpContext.Current.User as User;
			if( null == user )
				_fileName = "InvalidAccess";
			else
				if( user.IsInRole("ruler") )
					_fileName = "InvalidAccess";
		}
	};
}
