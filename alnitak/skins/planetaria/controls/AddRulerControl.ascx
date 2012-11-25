<%@ Control Language="c#" Inherits="Alnitak.AddRulerControl" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<br/><br/>

<asp:Label id="message" runat="server" /><br/>

<asp:Label id="insertPlanetName" runat="server" />
<asp:TextBox id="planet" CssClass="textbox" runat="server" /><br/>

<br/>

<div class="error">
	
	<orion:OnlyTextValidator id="onlyTextValidator" ControlToValidate="planet" Display="Dynamic" runat="server"/>
	<asp:RequiredFieldValidator id="requiredFieldValidator" ControlToValidate = "planet" Display="Dynamic" runat="server"/>
	<orion:CharCountValidator id="charCountValidator" ControlToValidate = "planet" Max = 20 Display="Dynamic" runat="server"/>
	
	<div align="center">
		<asp:Button id="registerRuler" CssClass="button" runat="server"/>
	</div>
</div>