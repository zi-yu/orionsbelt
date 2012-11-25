<%@ Control Language="C#" Inherits="Alnitak.BattleNotifier" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<dl id="topItemR">
	<dt><lang:Label ref="batalhas" runat="server" /></dt>
	<dd class="battleNotifier">
		<a href="<%= OrionGlobals.AppPath %>ruler/battle/default.aspx" ><lang:Label ref="BattlesWaiting" runat="server" /></a>
	</dd>	
	<dd class="menuBottom"/>
</dl>


