<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<orion:PlanetNavigation id="planetNavigation" runat="server" />

<orion:QueueErrorReport id="queueError" EnableViewState="false" runat="server" />

<orion:ResourcesList id="resourcesList" runat="server" />

<orion:QueueNotifier id="queue" runat="server" />

<orion:Resources id="general" runat="server" />
<orion:Resources id="upgrade" runat="server" />

<ul class="help_zone">
	<li><asp:HyperLink runat="server" id="intrinsicHelp" /></li>
	<li><asp:HyperLink runat="server" id="queueHelp" /></li>
	<li><asp:HyperLink runat="server" id="buildingHelp" /></li>
</ul>
