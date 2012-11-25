<%@ Control Language="C#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="light" TagName="RegistControl" Src="../controls/RegistControl.ascx" %>

<orion:RoleVisible showTo="guest" runat="server">
	<light:RegistControl runat="server" />
</orion:RoleVisible>

<orion:RoleVisible showTo="user" runat="server">
	<lang:Label ref="register_has-regist" runat="server" />
</orion:RoleVisible>