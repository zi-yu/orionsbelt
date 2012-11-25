
using System;
using System.Web.UI;

namespace Alnitak {

	public class RulerPage : PlanetControl {
	
		#region Instance Fields

		protected MessageList messageList;
		protected RulerInfo rulerInfo;

		#endregion

		#region Control Events

		/// <summary>Inicializa o controlo</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			messageList.Manager = getRuler();
			rulerInfo.Ruler = getRuler();
			rulerInfo.Title = info.getContent("ruler_info");
		}

		#endregion
			
	};

}
