<%@ Control Language="c#" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<script language="javascript">
	function goRefresh()
	{
		window.location.reload(true);
	}
</script>

<lang:Label ref="onTurn" runat="server" />
<p />
<a href="javascript:goRefresh();"><lang:Label ref="tryAgain" runat="server" /></a>

