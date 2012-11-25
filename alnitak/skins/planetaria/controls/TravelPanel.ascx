<!--Inherits="Alnitak.TravelPanel"-->
<%@ Control Language="c#" Inherits="Alnitak.TravelPanel" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<table align='center' class='frame' width='480'>
	<tr>
		<td class='resourceTitle'>
			<lang:Label ref="travel_controlPanel" runat="server" />
		</td>
	</tr>
	<tr>
		<td class='resource' align='center' valign='top'>
			<asp:Button ID="prev" Text="<" CssClass="button" Width="20px" Runat="server" />
			<asp:Button ID="up" Text="^" CssClass="button" Width="20px" Runat="server" />
			<asp:Button ID="next" Text=">" CssClass="button" Width="20px" Runat="server" />
		</td>
	</tr>
</table>