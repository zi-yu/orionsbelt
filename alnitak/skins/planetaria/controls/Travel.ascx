<%@ Control Language="c#" Inherits="Alnitak.Travel" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="directTravel" TagName="DirectTravel" Src="DirectTravelControl.ascx" %>
<%@ Register TagPrefix="advancedTravel" TagName="AdvancedTravel" Src="TravelControl.ascx" %>


<div class="tabsPlaceholder" align="center" style="padding-bottom:10px">
	<div class="tabs" align="left">
		<span id="tab1" class="unselectedTab" onClick="tabClick(this,'content1');">
			<lang:Label ref="travel_directTravel" runat=server ID="Label1"/>
		</span>
		<span id="tab2" class="unselectedTab" onClick="tabClick(this,'content2');">
			<lang:Label ref="travel_advancedTravel" runat=server ID="Label2"/>
		</span>
	</div>
	<div class="tabContents">
		<div id="content1" class="unselectedTabContents">
			<directTravel:DirectTravel id="directTravel" runat="server" />
		</div>
		<div id="content2" class="unselectedTabContents">
			<advancedTravel:AdvancedTravel id="advancedTravel" runat="server" />
		</div>
	</div>
</div>

<script language="javascript">
		var theform = document.pageContent;
		var tab = document.getElementById( theform.oldTabCtrl.value );
		tab.className = "selectedTab";
				
		var cont = document.getElementById( theform.oldTabContent.value );
		cont.className = "selectedTabContents";
</script>