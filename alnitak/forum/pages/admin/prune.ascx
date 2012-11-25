<%@ Control language="c#" Codebehind="prune.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.admin.prune" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>

<yaf:PageLinks runat="server" id="PageLinks"/>

<yaf:adminmenu runat="server">

<table class="forumContent" cellspacing=1 cellpadding=0 width=100%>
<tr>
	<td class=header1 colspan=2>Prune Topics</td>
</tr>
<tr>
	<td class=postheader width=50%><b>Select forum to prune:</b></td>
	<td class=post width=50%><asp:dropdownlist id=forumlist runat=server></asp:dropdownlist>
</tr>
<tr>
	<td class=postheader><b>Enter minimum age in days:</b><br>Topics with the last post older than this will be deleted.</td>
	<td class=post><asp:textbox id=days runat=server></asp:textbox>
</tr>
<tr>
	<td class=footer1 colspan=2 align=center>
		<asp:button id=commit runat=server text=Prune></asp:button>
	</td>
</tr>
</table>

</yaf:adminmenu>

<yaf:SmartScroller id="SmartScroller1" runat = "server" />
