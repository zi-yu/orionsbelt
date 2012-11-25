<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<table width="100%">
	<tr>
		<th class="planetInfoZone"><lang:Label ref="recurso" runat="server" /></th>
		<th class="planetInfoZone"><lang:Label ref="quantidade" runat="server" /></th>
		<th class="planetInfoZone"><lang:Label ref="turnIncome" runat="server" /></th>
		<th class="planetInfoZone"><lang:Label ref="ratio" runat="server" /></th>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="food" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="foodQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="foodPerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="foodRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="energy" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="energyQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="energyPerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="energyRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="mp" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="mpQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="mpPerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="mpRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="gold" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="goldQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="goldPerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="goldRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="labor" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="laborQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="laborPerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="laborRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="housing" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="housingQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="housingPerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="housingRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="culture" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="cultureQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="culturePerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="cultureRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="groundSpace" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="groundSpaceQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="groundSpacePerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="groundSpaceRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="waterSpace" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="waterSpaceQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="waterSpacePerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="waterSpaceRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="orbitSpace" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="orbitSpaceQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="orbitSpacePerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="orbitSpaceRatio" runat="server" /></td>
	</tr>
	<tr>
		<td class="planetInfoZone"><lang:Label ref="score" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="scoreQuantity" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="scorePerTurn" runat="server" /></td>
		<td class="planetInfoZone"><asp:Label id="scoreRatio" runat="server" /></td>
	</tr>
</table>