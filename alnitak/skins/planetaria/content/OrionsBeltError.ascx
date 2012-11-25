<%@ Control Language="C#" Inherits="Alnitak.OrionsBeltError" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<table width="100%">
	<tr>
		<td class="alertBar"></td>
	</tr>
	<tr>
		<td >
			<lang:Label ref="orionsbeltError_text" runat="server" ID="Label2"/>
			<asp:PlaceHolder ID="adminContent" Runat="server">
				<b>Error: </b><br/><asp:Label ID="exceptionName" Runat="server" />
				<p/>
				<b>Mensagem: </b><br/><asp:Label ID="exceptionMessage" Runat="server" />
				<p/>
				<b>Stack Trace: </b><asp:Label ID="exceptionTrace" Runat="server" />
			</asp:PlaceHolder>
			<p/>
		</td>
	</tr>
	<tr>
		<td class="alertBar"></td>
	</tr>
</table>

