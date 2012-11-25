// created on 5/7/04 at 11:15 a

using System;
using System.Web.UI.WebControls;

namespace Alnitak {

	public class CharCountValidator : RegularExpressionValidator {
	
		private int min;
		private int max;  
	
		public CharCountValidator()
		{
			min = 0;
			max = 3;
		}
		
		protected override void OnLoad( EventArgs e )
		{
			base.OnLoad(e);
			ValidationExpression = @"(\d|\D){" + min + "," + max + "}";
		}
		
		public int Min {
			get { return min; }
			set { min = value; }
		}
		
		public int Max {
			get { return max; }
			set { max = value; }
		}
	
	};

}