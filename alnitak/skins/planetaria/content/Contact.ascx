<%@ Control Language="C#" Inherits="Alnitak.ContactPage" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<asp:PlaceHolder Visible="false" id="messageSent" runat="server">
	<h2><lang:Label ref="MessageSent" runat="server" /></h2>
</asp:PlaceHolder>

<table width="90%">
	<tr>
		<td><lang:Label ref="MailFrom" runat="server" />:</td>
		<td>
			<asp:Label Visible="false" id="from" runat="server" />
			<asp:TextBox Visible="false" id="fromBlank" runat="server" />
		</td>
	</tr>
	<tr>
		<td><lang:Label ref="MailTo" runat="server" />:</td>
		<td><asp:PlaceHolder id="to" runat="server" /></td>
	</tr>
	<tr>
		<td><lang:Label ref="Message" runat="server" /></td>
		<td><asp:TextBox id="message" TextMode="MultiLine" runat="server" /></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:Button id="send" OnClick="SendMessage" runat="server" /></td>
	</tr>
</table>
