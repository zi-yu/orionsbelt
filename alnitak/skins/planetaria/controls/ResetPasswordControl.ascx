<%@ Control Language="c#" EnableViewState="True" Inherits="Alnitak.ResetPassword" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<asp:Panel ID="doReset" CssClass="loginPanel" Runat="server">
	
	<div class="box">
		<lang:Label ref="reset_text" runat="server" />
		<p/>
		<lang:Label ref="mail" runat="server" />&nbsp;<asp:TextBox ID="mail" CssClass="textbox" Runat="server" />
		<div class="error">
			<orion:MailValidator
				id="mailValidator"
				ControlToValidate="mail"
				Display="Dynamic"
				InitialValue=""
				runat="server">
			</orion:MailValidator>
			<asp:Label ID="message" CssClass="red" Runat="server" />
		</div>
		<p/>
		<div align="center">
			<asp:Button id="resetButton" CssClass="button" OnClick="onResetPassword" runat="server" />
		</div>
	</div>
	
</asp:Panel>

<asp:Panel ID="resetMade" Runat="server">
	
	<div class="box" align="center">
		<p/>
		<lang:Label ref="reset_done" runat="server" />
		<p/>
	</div>
	
</asp:Panel>
