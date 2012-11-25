<%@ Control Language="c#" Inherits="Alnitak.MoveFleet" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="travel" TagName="Travel" Src="../controls/Travel.ascx" %>


<travel:Travel id="travel" runat="server" />

<orion:ItemsTable id="itemsTable" runat="server" />







