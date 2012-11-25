<%@ Control Language="C#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="galaxy" TagName="ProfileControl" Src="../controls/ProfileControl.ascx" %>

<orion:RoleVisible showTo="guest" runat="server">
	<lang:Label ref="profile_no-login" runat="server" />
</orion:RoleVisible>

<orion:RoleVisible showTo="user" runat="server">
	<galaxy:ProfileControl runat="server"/>
</orion:RoleVisible>