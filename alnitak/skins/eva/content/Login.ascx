<%@ Control Language="C#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="light" TagName="LoginControl" Src="../controls/LoginControl.ascx" %>

<orion:RoleVisible showTo="guest" runat="server">
	<light:LoginControl runat="server" />
</orion:RoleVisible>

<orion:RoleVisible showTo="user" runat="server">
	<lang:Label ref="login_has-login" runat="server" />
</orion:RoleVisible>
