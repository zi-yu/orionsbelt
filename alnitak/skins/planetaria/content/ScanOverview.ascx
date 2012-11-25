<%@ Control Language="C#" Inherits="Alnitak.ScanOverview" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="galaxy" TagName="Travel" Src="../controls/Travel.ascx" %>

<orion:QueueErrorReport id="errorReport" runat="server" Visible="false" />

<div class="planetInfoZoneTitle"><b><lang:Label ref="scan_reports" runat="server" /></b></div>
<table class="planetFrame">
	<asp:PlaceHolder id="reports" runat="server" />
</table>

<ul class="help_zone">
	<li><asp:HyperLink runat="server" id="scanWiki" /></li>
</ul>
