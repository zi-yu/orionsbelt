using System;
using System.Web.UI;

namespace Alnitak {
	
	public class ScreenShotViewer : Control {
		
		#region Private Fields

		private int max = 10;

		#endregion

		#region Properties

		public int Max {
			get { return max; }
			set { max = value; }
		}

		public int RandomPicture {
			get { return OrionGlobals.GenerateRandInt(1,Max); }
		}

		#endregion

		#region Events

		protected override void Render(HtmlTextWriter writer) {
			int r = RandomPicture;
			Page.RegisterHiddenField("imagesPath",OrionGlobals.getCommonImagePath("screenshots/"));
			writer.WriteLine("<table id='screenShotViewer' >");
			writer.WriteLine("<tr><td>");
			writer.WriteLine("<img id='currentPicture' src='{0}' />",OrionGlobals.getCommonImagePath("screenshots/" + r + ".png") );
			writer.WriteLine("</td></tr>");
			writer.WriteLine("<th>");
			writer.WriteLine("<span onClick='prevPicture()'> << </span>");
			writer.WriteLine("<span id='currentPictureNumber'>{1}</span>/<span id='maxPicture'>{0}</span>",Max,r);
			writer.WriteLine("<span onClick='nextPicture()'> >> </span>");
			writer.WriteLine("</th>");
			writer.WriteLine("</table>");
		}

		#endregion
	}
}
