<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="turn" TagName="TurnControl" Src="../controls/TurnControl.ascx" %>
<div id="leftMenu">
	<dl id="topItem">
		<dt>Hot!</dt>
		<dd><orion:HotNews runat="server" /></dd>
		<dd class="menuBottom"/>
	</dl>
	<turn:TurnControl runat="server"/>
	<orion:RoleVisible DontshowTo="guest" runat="server" >
		<dl>
			<dt><lang:Label ref="user" runat="server" /></dt>
			<orion:RoleVisible showTo="user" runat="server" >
				<dd><a href="<%= OrionGlobals.AppPath %>logout.aspx" >  <lang:Label ref="section_logout" runat="server" /></a></dd>
				<dd><a href="<%= OrionGlobals.AppPath %>profile.aspx" >  <lang:Label ref="section_profile"  runat="server" /></a></dd>
			</orion:RoleVisible>
			<dd class="menuBottom"/>
		</dl>
	</orion:RoleVisible>
	
	<orion:SubSectionmenu runat="server" />
	<dl id="pub">
		<dt><lang:Label ref="pub" runat="server" /></dt>
		<dd class="pubItem"><orion:Flash SwfFromWebConfig="GSMBit" runat="server"/></dd>
		<dd class="menuBottom"/>
	</dl>
</div>
