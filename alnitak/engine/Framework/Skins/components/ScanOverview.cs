// created on 8/28/2005 at 11:02 AM

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.IO;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Info;

namespace Alnitak {

	/// <summary>
	/// classe que representa a pgina de scans
	/// </summary>
	public class ScanOverview : ManageScan {
		
		#region ManageScan Implementation
		
		protected override Scan[] GetScans()
		{
			return ScanUtility.Persistence.GetScans(getRuler());
		}
		
		protected override void RegisterRequest()
		{
			OrionGlobals.RegisterRequest(Chronos.Messaging.MessageType.Radar, string.Format("{0}",info.getContent("section_scanoverview")));
		}
		
		protected override void InitControlOnLoad()
		{
		}
		
		protected override void InitControlOnPreRender()
		{
		}
		
		protected override bool ShowNavigation {
			get { return false; }
		}
		
		protected override bool ShowSourcePlanet {
			get { return true; }
		}
		
		#endregion
			
	};

}
