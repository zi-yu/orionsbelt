<%@ Control Language="c#" Inherits="Alnitak.LoginControl" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="loginPanel">	
	<lang:Label ref="mail" runat="server"/>:<br /> 
	<asp:TextBox id="userMail" Width="100%" runat="server" /> <br /> 
	
	<lang:Label ref="password" runat="server"/>:<br /> 
	<asp:TextBox id="password" TextMode="password" Width="100%" runat="server" /> <br /> 
	 
	<asp:CheckBox id="autoLogin" runat="server" /> <lang:Label ref="auto-login" runat="server"/><br />
	<div align="center"> 
		<asp:Button id="loginButton" OnClick="onLoginClick" runat="server" />
	</div>
	<div class="error">

		<asp:RequiredFieldValidator
			id="userValidator"
			ControlToValidate="userMail"
			Display="static"
			InitialValue=""
			runat="server">
		</asp:RequiredFieldValidator>

		<asp:RequiredFieldValidator
			id="passValidator"
			ControlToValidate="password"
			Display="static"
			InitialValue=""
			runat="server">
		</asp:RequiredFieldValidator>
		
		<asp:RegularExpressionValidator
			id="mailValidator"
			ControlToValidate="userMail"
			Display="static"
			InitialValue=""
			ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
			runat="server">
		</asp:RegularExpressionValidator>
		
		<asp:Label id="message" runat="server" />

	</div>
</div>