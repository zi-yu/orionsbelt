// created on 5/8/04 at 10:31 a

using Language;
using System;
using System.Web.UI.WebControls;

namespace Alnitak {

	public class UrlValidator : AlnitakValidator {
	
		public UrlValidator()
		{
			ValidationExpression = @"(http://)(\w|[.#-/?=])*";
		}
	
	};

}