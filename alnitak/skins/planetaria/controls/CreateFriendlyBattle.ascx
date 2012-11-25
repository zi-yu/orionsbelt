<%@ Control Language="c#" Inherits="Alnitak.Battle.CreateFriendlyBattle" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="battle" Namespace="Alnitak.Battle" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>


<asp:Panel id="chooseRuler" runat="server">
	<battle:ShipSelector id="shipSelector" runat="server" />

	<table class='planetFrame'>
		<tr>
			<td class='resourceTitle'>
				<lang:Label ref="battle_choose" runat="server" />
			</td>
			<td class='resourceTitle'>
				<lang:Label ref="battle_type" runat="server" />
			</td>
			<td class='resourceTitle'>
				<lang:Label ref="battle_create" runat="server" />
			</td>
		</tr>
		<tr>
			<td align='center' class="resource" >
				<asp:DropDownList EnableViewState="true" ID="rulers" Runat="server" />
			</td>
			<td align='center' class="resource" >
				<asp:DropDownList EnableViewState="true" ID="battleTypes" Runat="server" />
			</td>
			<td align='center' class="resource" >
				<asp:ImageButton ID="createBattle" Runat="server" />
			</td>
		</tr>
	</table>
</asp:Panel> 