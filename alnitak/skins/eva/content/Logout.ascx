<%@ Control Language="C#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="title">
	<lang:Label ref="section_logout" runat="server" />
</div>

<orion:RoleVisible showTo="guest" runat="server">
	<lang:Label ref="login_not-login" runat="server" />
	<p/>
	* <a href="login.aspx"><lang:Label ref="section_login" runat="server" /></a>
</orion:RoleVisible>