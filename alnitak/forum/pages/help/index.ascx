<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>
<%@ Control language="c#" Codebehind="index.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.help.index" %>
<yaf:PageLinks runat="server" id="PageLinks" />
<yaf:helpmenu runat="server">

<table class="forumContent" width="100%" cellspacing="0" cellpadding="0">
<tr><td class="post" valign="top">
	<table width="100%" cellspacing="0" cellpadding="0">
	<tr><td nowrap class="header2"><b>Search Help Topics</b></td></tr>
	<tr>
		<td nowrap class="post">
			Enter keywords to search for:
			<asp:textbox runat="server" id="search"/>
			<asp:button runat="server" id="DoSearch" text="Search"/>
		</td>
	</tr>
	</table>
</td></tr>
</table>

</yaf:helpmenu>
<yaf:SmartScroller id="SmartScroller1" runat = "server" />
