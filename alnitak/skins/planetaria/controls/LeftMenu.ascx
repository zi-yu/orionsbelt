<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="turn" TagName="TurnControl" Src="../controls/TurnControl.ascx" %>

<div id="menu">
	<dl>
		<dt>Hot!</dt>
		<dd class="firstItem"><div align="center"><orion:HotNews runat="server" /></div></dd>
		<dd class="lastItem"></dd>
	</dl>
	<turn:TurnControl runat="server" />
	
	<dl>
		<dt><lang:Label ref="user" runat="server" /></dt>

		<orion:RoleVisible showTo="guest" runat="server" >
			<dd class="firstItem"><a href="<%= OrionGlobals.AppPath %>login.aspx" class="menuItem"><lang:Label ref="section_login" runat="server" /></a></dd>
			<dd><a href="<%= OrionGlobals.AppPath %>regist.aspx" class="menuItem"><lang:Label ref="section_regist" runat="server" /></a></dd>
		</orion:RoleVisible>

		<orion:RoleVisible showTo="user" runat="server">
			<dd class="firstItem"><a href="<%= OrionGlobals.AppPath %>logout.aspx" ><lang:Label ref="section_logout" runat="server" /></a></dd>
			<dd><a href="<%= OrionGlobals.AppPath %>profile.aspx" ><lang:Label ref="section_profile"  runat="server" /></a></dd>
		</orion:RoleVisible>
		
		<orion:RoleVisible showTo="user-no-ruler" runat="server" >
			<dd><a href="<%= OrionGlobals.AppPath %>addruler.aspx" class="menuItem"> <lang:Label ref="play" runat="server" /></a></dd>
		</orion:RoleVisible>
		
		<dd class="lastItem"></dd>
	</dl>
		
	<orion:SubSectionmenu runat="server" />
	<dl id="pub">
		<dt><lang:Label ref="pub" runat="server" /></dt>
		<dd class="pubItem"><orion:Flash SwfFromWebConfig="GSMBit" runat="server" /></dd>
	</dl>
</div>
