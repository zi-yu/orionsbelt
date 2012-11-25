<%@ Control Language="C#" Inherits="Alnitak.Battle.BattleAdminUser" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<table class="planetFrame">
	<tr>
		<td class="resourceTitle" >
			<lang:Label ref="battleAdminUser_chooseRuler" runat="server" />
		</td>
	</tr>
	<tr>
		<td align="center">
			<asp:TextBox Id="userId" runat="server" /> <asp:Button id="set" Text="Set" runat="server" /><br/>
			<asp:RegularExpressionValidator ValidationExpression="^\d+$" CssClass="red" ErrorMessage="INT!!!" Runat="server" ControlToValidate="userId" />
		</td>
	</tr>
</table>

<asp:Label ID="user" Runat="server" />

<orion:ItemsTable id="battle" runat="server" />
<orion:ItemsTable id="tournament" runat="server" />
<orion:ItemsTable id="friendly" runat="server" />
