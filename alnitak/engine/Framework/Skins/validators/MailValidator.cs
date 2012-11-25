// created on 5/7/04 at 11:15 a

using System;
using System.Web.UI.WebControls;

namespace Alnitak {

	public class MailValidator : RegularExpressionValidator {
	
		public MailValidator()
		{
			ValidationExpression = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
		}
	
	};

}