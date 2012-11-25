<%@ Control language="c#" Codebehind="editcategory.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.admin.editcategory" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>

<yaf:PageLinks runat="server" id="PageLinks"/>

<yaf:adminmenu runat="server">

<table class="forumContent" cellspacing=1 cellpadding=0 width="100%">
	<tr>
		<td class=header1 colspan=2>Edit Category: <asp:label id=CategoryNameTitle runat=server></asp:label></td>
	</tr>
	<tr>
		<td class=post>Name:</td>
		<td class=post>
<asp:TextBox id=Name runat="server" cssclass=edit></asp:TextBox></td>
	</tr>
	<tr>
		<td class=post>Sort Order:</td>
		<td class=post>
<asp:TextBox id=SortOrder runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<td class=postfooter colspan=2 align=middle>
<asp:Button id=Save runat="server" Text="Save"></asp:Button>&nbsp;
<asp:Button id=Cancel runat="server" Text="Cancel"></asp:Button></td>
	</tr>
</table>

</yaf:adminmenu>

<yaf:SmartScroller id="SmartScroller1" runat = "server" />
