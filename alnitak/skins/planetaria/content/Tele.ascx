<%@ Control Language="C#" Inherits="Alnitak.Teletransportation" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak"  %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<orion:PlanetNavigation id="planetNavigation" runat="server" />

<orion:QueueErrorReport id="errorReport" runat="server" Visible="false" />

<asp:PlaceHolder id="intrinsicTeletransport" runat="server">

	<div class="planetInfoZoneTitle"><b><lang:Label ref="tele_intrinsicTeletransportation" runat="server" /></b></div>
	<table class="planetFrame">
		<tr>
			<td class="resource"><lang:Label ref="recursos" runat="server" /></td>
			<td><asp:DropDownList id="intrinsicResources" runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<lang:Label ref="quantidade_title" runat="server" />: <b><span id="resourceAvailable">-</span></b></td>
		</tr>
		<tr>
			<td class="resource"><lang:Label ref="send" runat="server" /></td>
			<td><asp:DropDownList id="toSend" runat="server" /></td>
		</tr>
		<tr>
			<td class="resource"><lang:Label ref="energy" runat="server" />&nbsp;<lang:Label ref="tele_available" runat="server" /></td>
			<td><asp:Label id="energyQuantity" runat="server" /></td>
		</tr>
		<tr>
			<td class="resource"><lang:Label ref="cost" runat="server" /> (<lang:Label ref="energy" runat="server" />)</td>
			<td><div id="resourceCost">-</div></td>
		</tr>
		<tr>
			<td class="resource"><lang:Label ref="tele_destiny" runat="server" /></td>
			<td><asp:DropDownList id="intrinsicDestiny" runat="server" /></td>
		</tr>
		<tr>
			<td class="resource"></td>
			<td><asp:Button id="moveIntrinsic" runat="server" /></td>
		</tr>
	</table>

</asp:PlaceHolder>

<asp:PlaceHolder id="fleetTeletransport" runat="server">

	<div class="planetInfoZoneTitle"><b><lang:Label ref="tele_fleetTeletransportation" runat="server" /></b></div>
	<table class="planetFrame">
		<tr>
			<td class="resource"><lang:Label ref="fleet_planetsShips" runat="server" /></td>
			<td><asp:DropDownList id="planetFleets" runat="server" /></td>
		</tr>
		<tr>
			<td class="resource"><lang:Label ref="energy" runat="server" />&nbsp;<lang:Label ref="tele_available" runat="server" /></td>
			<td><asp:Label id="energyQuantity2" runat="server" /></td>
		</tr>
		<tr>
			<td class="resource"><lang:Label ref="cost" runat="server" /> (<lang:Label ref="energy" runat="server" />)</td>
			<td><div id="fleetCost">-</div></td>
		</tr>
		<tr>
			<td class="resource"><lang:Label ref="tele_destiny" runat="server" /></td>
			<td><asp:DropDownList id="fleetDestiny" runat="server" /></td>
		</tr>
		<tr>
			<td class="resource"></td>
			<td><asp:Button id="moveFleet" runat="server" /></td>
		</tr>
	</table>
	
</asp:PlaceHolder>

<asp:PlaceHolder id="noFleets" Visible="false" runat="server">
	<div class="planetInfoZoneTitle"><b><lang:Label ref="tele_fleetTeletransportation" runat="server" /></b></div>
	<table class="planetFrame">
		<tr>
			<td><lang:Label ref="noFleets" runat="server" /></td>
		</tr>
	</table>
</asp:PlaceHolder>

<ul class="help_zone">
	<li><asp:HyperLink runat="server" id="teletransportationWiki" /></li>
</ul>
