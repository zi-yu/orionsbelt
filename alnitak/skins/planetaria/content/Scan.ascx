<%@ Control Language="C#" Inherits="Alnitak.ManageScan" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="galaxy" TagName="Travel" Src="../controls/Travel.ascx" %>

<orion:PlanetNavigation id="planetNavigation" runat="server" />
<galaxy:Travel id="travelControl" runat="server" />

<orion:QueueErrorReport id="errorReport" runat="server" Visible="false" />

<div class="planetInfoZoneTitle"><b><lang:Label ref="scan_performScan" runat="server" /></b></div>
<table class="planetFrame">
	<tr>
		<td>
			<asp:PlaceHolder id="performScanPanel" Visible="false" runat="server">
				<lang:Label ref="fleet_help" runat="server" />
				<p/>
				<lang:Label ref="scan_planetEnergy" runat="server" />: <b><asp:Label id="planetEnergy" runat="server" /></b><br/>
				<lang:Label ref="scan_scanCost" runat="server" />: <b><asp:Label id="scanCost" runat="server" /></b><br/>
				<p/>
				<orion:OrionCommonImage Image="move" Type="gif" runat="server" />&nbsp;<asp:LinkButton id="performScan" Visible="true" runat="server" /><br/>
				<orion:OrionCommonImage Image="move" Type="gif" runat="server" />&nbsp;<asp:LinkButton id="performSystemScan" Visible="true" runat="server" />
			</asp:PlaceHolder>
			<asp:Label id="pleaseChooseCoord"  Visible="false" runat="server" />
		</td>
	</tr>
</table>

<div class="planetInfoZoneTitle"><b><lang:Label ref="scan_reports" runat="server" /></b></div>
<table class="planetFrame">
	<asp:PlaceHolder id="reports" runat="server" />
</table>

<ul class="help_zone">
	<li><asp:HyperLink runat="server" id="scanWiki" /></li>
</ul>
