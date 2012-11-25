<%@ Control Language="C#" Inherits="Alnitak.ChangeShips" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>


<asp:PlaceHolder id="content" runat="server">
<div class="planetInfoZoneTitle" width="100%">
	<b><lang:Label ref="fleet_changeFleet" runat="server" /></b>
</div>
<table class="planetFrame" width="100%">
	<tr>
		<td class="resourceTitle" align="center">
			<lang:Label ref="fleet_quant" runat="server" />:
		</td>
		<td class="resourceTitle" align="center">
			<lang:Label ref="fleet_origin" runat="server" />
		</td>
		<td class="resourceTitle" align="center">
			<lang:Label ref="fleet_destiny" runat="server" />
		</td>
		<td class="resourceTitle" align="center">
			<lang:Label ref="fleet_shipType" runat="server" />
		</td>
	</tr>
	<tr>
		<td class="resource" align="center">
			<asp:TextBox ID="quant" CssClass="textbox" Text="" Runat="server" />
		</td>
		<td class="resource" align="center">
			<asp:DropDownList ID="originFleet" CssClass="textbox" Runat="server" />
		</td>
		<td class="resource" align="center">
			<asp:DropDownList ID="destinyFleet" CssClass="textbox" Runat="server" />
		</td>
		<td class="resource" align="center">
			<asp:DropDownList ID="availableShips" CssClass="textbox" Runat="server" />
		</td>
	</tr>
	<tr>
		<td class="resource heightPadding" align="center" colspan="4">
			<p/>
			<asp:Button ID="moveShips" CssClass="button" Runat="server" />&nbsp;&nbsp;
			<asp:Button ID="moveAllShips" CssClass="button" CausesValidation="False" Runat="server" /><br/>
			
			<div align="center">
				<asp:RegularExpressionValidator id="quantityValidator" Display="Dynamic" ControlToValidate="quant" ValidationExpression="^\d+$" Runat="server"></asp:RegularExpressionValidator>
			</div>
		</td>
	</tr>
</table>
</asp:PlaceHolder>

