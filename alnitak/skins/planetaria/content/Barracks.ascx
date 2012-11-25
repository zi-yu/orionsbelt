<%@ Control Language="C#" Inherits="Alnitak.Barracks" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="travel" TagName="Travel" Src="../controls/Travel.ascx" %>

<orion:PlanetNavigation id="planetNavigation" runat="server" />
<orion:QueueErrorReport id="queueError" EnableViewState="false" runat="server" />
<orion:ResourcesList id="resourcesHelp" runat="Server" />
<orion:Resources id="resources" runat="Server" />
<orion:QueueNotifier id="queue" runat="server" />

<travel:Travel id="travelControl" runat="server" />
<orion:SabotageQueue id="sabotageQueue" runat="server" />
<orion:SabotageList id="sabotageList" runat="server" />

<ul class="help_zone">
	<li><asp:HyperLink runat="server" id="intrinsicHelp" /></li>
	<li><asp:HyperLink runat="server" id="queueHelp" /></li>
	<li><asp:HyperLink runat="server" id="sabotageHelp" /></li>
</ul>
