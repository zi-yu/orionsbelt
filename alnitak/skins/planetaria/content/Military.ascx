<%@ Control Language="C#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak"  %>
<%@ Register TagPrefix="fleet" TagName="MoveFleet" Src="../controls/MoveFleet.ascx" %>

<fleet:MoveFleet runat="server" />

<orion:ConquerPlanet runat="server" />

<orion:ShowAllFleets runat="server" />

<orion:ShowRulersFleets runat="server"  />
