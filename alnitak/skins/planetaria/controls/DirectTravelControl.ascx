<%@ Control Language="c#" Inherits="Alnitak.DirectTravelControl" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<div align="center" style="padding-bottom:5px;">
	<asp:Label id="prevGalaxy" Runat="server" /><asp:DropDownList ID="galaxy" EnableViewState="True" CssClass="textbox" Runat="server" />&nbsp;:
	<asp:Label id="prevSystem" Runat="server" /><asp:DropDownList ID="system" EnableViewState="True" CssClass="textbox" Runat="server" />&nbsp;:
	<asp:Label id="prevSector" Runat="server" /><asp:DropDownList ID="sector" EnableViewState="True" CssClass="textbox" Runat="server" />&nbsp;:
	<asp:DropDownList ID="planet" EnableViewState="True" CssClass="textbox" Runat="server" />
</div>