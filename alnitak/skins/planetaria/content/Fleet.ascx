<%@ Control Language="C#" Inherits="Alnitak.Fleet" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak"  %>
<%@ Register TagPrefix="orionTag" TagName="ChangeShips" Src="../controls/ChangeShips.ascx" %>
<%@ Register TagPrefix="orionTag" TagName="CreateFleet" Src="../controls/CreateFleet.ascx" %>

<orion:PlanetNavigation id="planetNavigation" runat="server" />
<orion:ResourcesList id="resourcesHelp" runat="Server" />
<orion:QueueErrorReport id="queueError" EnableViewState="false" runat="server" />
<orion:QueueNotifier id="queue" runat="server" />
<orion:Resources id="resources" runat="Server" />
<orionTag:CreateFleet runat="server" />
<orionTag:ChangeShips id="changeShips" runat="Server" />
<orion:ShowPlanetFleet runat="server" />

