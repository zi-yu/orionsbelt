<%@ Control Language="c#" Inherits="Alnitak.CreateFleet" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="planetInfoZoneTitle" width="100%">
	<b><lang:Label ref="fleet_create" runat="server" /></b>
</div>
<table width="100%" class="planetFrame">
	<tr>
		<td class="resourceTitle" align="center">
			<lang:Label ref="fleet_name" runat="server" />
		</td>
	</tr>
	<tr>
		<td class="resource" align="center">
			<asp:TextBox id="fleetName" CssClass="textbox" Text="" EnableViewState="false" Runat="server" /><br/>
		</td>
	</tr>
	<tr>
		<td class="resource" align="center">
			<asp:Button id="createFleet" CssClass="button" Runat="server" /><br/>
			<orion:OnlyTextValidator id="nameValidator" Display="Dynamic" runat="server" ControlToValidate="fleetName"/>
		</td>
	</tr>
</table>
<p/>