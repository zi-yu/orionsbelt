<%@ Control Language="c#" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<dl id="turn">
	<dt><lang:Label ref="turn_current" runat="server" /></dt>
	<dd class="firstItem"><span><lang:Label ref="turn_current" runat="server"/>: <b><orion:CurrentTurn runat="server" /></b></span></dd>
	<dd class="lastItem"><span><lang:Label ref="turn_next" runat="server" /> <b><orion:NextTurn runat="server" /></b>m</span></dd>
</dl>
		
		