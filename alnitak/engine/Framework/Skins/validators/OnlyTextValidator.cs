// created on 5/7/04 at 11:15 a

using Language;
using System;
using System.Web.UI.WebControls;

namespace Alnitak {

	public class OnlyTextValidator : AlnitakValidator {
	
		public OnlyTextValidator()
		{
			ValidationExpression = @"(\w|( )|[.!?])*";
		}
	
	};

}