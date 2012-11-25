<%@ Control Language="c#" Inherits="Alnitak.AdminResetPassword" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="PlanetInfoZoneTitle">
	Reset PassWord
</div>

<div align="center">
	<asp:PlaceHolder ID="reset_done" Runat="server">
		Mail:<asp:TextBox CssClass="textbox" ID="mail" Runat="server"></asp:TextBox><br/><br/>
		<asp:Button id="reset" CssClass="button" runat="server" /><br/>
	</asp:PlaceHolder>
	<asp:Label ID="message" Runat="server" />
</div>
