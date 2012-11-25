<%@ Control language="c#" Codebehind="nntpservers.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.admin.nntpservers" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>

<yaf:PageLinks runat="server" id="PageLinks"/>

<yaf:adminmenu runat="server">

<table class="forumContent" width="100%" cellspacing=1 cellpadding=0>
<tr>
	<td class=header1 colspan=6>NNTP Servers</td>
</tr>

<asp:repeater id=RankList runat="server">
	<HeaderTemplate>
		<tr>
			<td class=header2>Name</td>
			<td class=header2>Address</td>
			<td class=header2>User Name</td>
			<td class=header2>&nbsp;</td>
		</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td class=post>
				<%# DataBinder.Eval(Container.DataItem, "Name") %>
			</td>
			<td class=post>
				<%# DataBinder.Eval(Container.DataItem, "Address") %>
			</td>
			<td class=post>
				<%# DataBinder.Eval(Container.DataItem, "UserName") %>
			</td>
			<td class=post>
				<asp:linkbutton runat="server" commandname="edit" commandargument='<%# DataBinder.Eval(Container.DataItem, "NntpServerID") %>'>Edit</asp:linkbutton>
				|
				<asp:linkbutton runat="server" onload="Delete_Load" commandname="delete" commandargument='<%# DataBinder.Eval(Container.DataItem, "NntpServerID") %>'>Delete</asp:linkbutton>
			</td>
		</tr>
	</ItemTemplate>
</asp:repeater>

<tr>
	<td class=footer1 colspan=5><asp:linkbutton id=NewServer runat="server" text="New Server"/></td>
</tr>
</table>

</yaf:adminmenu>

<yaf:SmartScroller id="SmartScroller1" runat = "server" />
