<%@ Control Language="c#" Inherits="Alnitak.CreateUsers" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div>
	user: <asp:TextBox CssClass="textbox" ID="user" Runat="server"/>
	quant: <asp:TextBox CssClass="textbox" ID="quant" Runat="server"/>
</div>
<div align="center">
	<asp:Button CssClass="button" ID="create" Runat="server" />
</div>