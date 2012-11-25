<%@ Control Language="c#" AutoEventWireup="false" Codebehind="lastposts.ascx.cs" Inherits="yaf.pages.lastposts" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>
<asp:repeater id="repLastPosts" runat="server" visible="true">
<HeaderTemplate>
	<table class="forumContent" cellSpacing="1" cellPadding="0" width="100%" align="center">
		<tr>
			<td class=header2 align=middle colSpan=2><%# GetText("last10") %></td>
		</tr>
</HeaderTemplate>
<FooterTemplate>
	</table>
</FooterTemplate>
<ItemTemplate>
		<tr class="postheader">
			<td width="140"><b><a href="<%# string.Format("userinfo.aspx?id={0}",DataBinder.Eval(Container.DataItem, "UserID")) %>"><%# DataBinder.Eval(Container.DataItem, "UserName") %></a></b>
			</td>
			<td width="80%" class="small" align="left"><b><%# GetText("posted") %></b> <%# FormatDateTime((System.DateTime)((System.Data.DataRowView)Container.DataItem)["Posted"]) %></td>
		</tr>
		<tr class="post">
			<td>&nbsp;</td>
			<td valign="top" class="message">
				<%# FormatBody(Container.DataItem) %>
			</td>
		</tr>
</ItemTemplate>
<AlternatingItemTemplate>
		<tr class="postheader">
			<td width="140"><b><a href="<%# string.Format("userinfo.aspx?id={0}",DataBinder.Eval(Container.DataItem, "UserID")) %>"><%# DataBinder.Eval(Container.DataItem, "UserName") %></a></b>
			</td>
			<td width="80%" class="small" align="left"><b><%# GetText("posted") %></b> <%# FormatDateTime((System.DateTime)((System.Data.DataRowView)Container.DataItem)["Posted"]) %></td>
		</tr>
		<tr class="post_alt">
			<td>&nbsp;</td>
			<td valign="top" class="message">
				<%# FormatBody(Container.DataItem) %>
			</td>
		</tr>
</AlternatingItemTemplate>
</asp:repeater>