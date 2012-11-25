<%@ Control Language="C#" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<orion:QueueErrorReport id="queueError" runat="server" />

<orion:QueueNotifier id="queue" runat="server" />

<orion:Resources id="tech" runat="server" />
<orion:Resources id="fleet" runat="server" />
<orion:Resources id="exploration" runat="server" />
<orion:Resources id="planet" runat="server" />

<ul class="help_zone">
	<li><asp:HyperLink runat="server" id="availableResearch" /></li>
	<li><asp:HyperLink runat="server" id="researchHelp" /></li>
	<li><asp:HyperLink runat="server" id="aboutThisPage" /></li>
</ul>
