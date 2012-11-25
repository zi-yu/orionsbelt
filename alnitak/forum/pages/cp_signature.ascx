<%@ Control language="c#" Codebehind="cp_signature.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.cp_signature" %>
<%@ Register TagPrefix="editor" Namespace="yaf.editor" Assembly="Alnitak" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>

<yaf:PageLinks runat="server" id="PageLinks"/>

<table class="forumContent" width=100% cellspacing=1 cellpadding=0>
<tr>
	<td class=header1 colspan=2><%= GetText("title") %></td>
</tr>
<tr>
	<td class=postformheader valign=top><%= GetText("signature") %></td>
	<td class=post id="EditorLine" runat="server">
		<!-- editor goes here -->
	</td>
</tr>
<tr>
	<td class=footer1 colspan=2 align=center>
		<asp:button id=save  runat="server"/>
		<asp:button id=cancel  runat="server"/>
	</td>
</tr>
</table>

<yaf:SmartScroller id="SmartScroller1" runat = "server" />
