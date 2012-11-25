<%@ Control Language="c#" Inherits="Alnitak.RegistControl" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="loginPanel" runat="server">
	<lang:Label ref="mail" runat="server"/>:<br /> 
	<asp:TextBox id="userMail" Width="100%" runat="server" /> <br /> 
	
	<lang:Label id="nickLabel" ref="nick" runat="server"/>:<br /> 
	<asp:TextBox id="nick" Width="100%" runat="server" /> <br />
	
	<lang:Label ref="password" runat="server"/>:<br /> 
	<asp:TextBox id="password" TextMode="password" Width="100%" runat="server" /> <br />
	<lang:Label ref="retype-password" runat="server"/>:<br /> 
	<asp:TextBox id="password2" TextMode="password" Width="100%" runat="server" /> <br />  
	 
	<div align="center"> 
		<asp:Button id="loginButton" OnClick="onLoginClick" runat="server" />
	</div>
	
	<asp:Label id="message" runat="server" />
	
	<div class="error">
</div>
<% /* MAIL */ %>

		<asp:RequiredFieldValidator
			id="userValidator"
			ControlToValidate="userMail"
			Display="static"
			InitialValue=""
			runat="server">
		</asp:RequiredFieldValidator>
		
		<orion:MailValidator
			id="mailValidator"
			ControlToValidate="userMail"
			Display="static"
			InitialValue=""
			runat="server">
		</orion:MailValidator>

<% /* NICK */ %>

		<asp:RequiredFieldValidator
			id="nickValidator"
			ControlToValidate="nick"
			Display="static"
			InitialValue=""
			runat="server">
		</asp:RequiredFieldValidator>
		
		<orion:OnlyTextValidator
			id="nickTextValidator"
			ControlToValidate="nick"
			Display="static"
			InitialValue=""
			runat="server">
		</orion:OnlyTextValidator>
		
		<orion:CharCountValidator
			id="nickRangeValidator"
			ControlToValidate="nick"
			Display="static"
			InitialValue=""
			Min="0"
			Max="30"
			runat="server">
		</orion:CharCountValidator>

<% /* PASSWORD */ %>

		<asp:RequiredFieldValidator
			id="passValidator"
			ControlToValidate="password"
			Display="static"
			InitialValue=""
			runat="server">
		</asp:RequiredFieldValidator>
		
		<orion:CharCountValidator
			id="passRangeValidator"
			ControlToValidate="password"
			Display="static"
			InitialValue=""
			Min="3"
			Max="10"
			runat="server">
		</orion:CharCountValidator>
		
		<asp:CompareValidator
			id="passValidator2"
			ControlToValidate="password"
			ControlToCompare="password2"
			Display="static"
			InitialValue=""
			runat="server">
		</asp:CompareValidator>
		
		<asp:Label id="errorMessage" runat="server" />

	</div>
