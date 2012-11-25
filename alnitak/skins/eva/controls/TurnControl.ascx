<%@ Control Language="c#" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<dl id="topItem">
	<dt><lang:Label ref="turn_current" runat="server"/></dt>
	<dd><lang:Label ref="turn_current" runat="server" />: <b><orion:CurrentTurn runat="server" /></b></dd>
	<dd><lang:Label ref="turn_next" runat="server" /> <b><orion:NextTurn runat="server" /></b>m</dd>
	<dd class="menuBottom">&nbsp;</dd>
</dl>
		