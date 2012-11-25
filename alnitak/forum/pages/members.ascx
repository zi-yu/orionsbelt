<%@ Control language="c#" Codebehind="members.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.members" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>

<yaf:PageLinks runat="server" id="PageLinks"/>

<table class="forumContent" width="100%" cellspacing="1" cellpadding="0">
<tr runat="server" id="LetterRow"/>
</table>

<table class=command><tr><td class="navlinks"><yaf:pager runat="server" id="Pager"/></td></tr></table>

<table class="forumContent" width="100%" cellspacing=1 cellpadding=0>
	<tr>
		<td class=header1 colspan=4><%= GetText("title") %></td>
	</tr>
	<tr>
		<td class=header2><img runat="server" id="SortUserName" align="absmiddle"/> <asp:linkbutton runat=server id="UserName"/></td>
		<td class=header2><img runat="server" id="SortRank" align="absmiddle"/> <asp:linkbutton runat=server id="Rank"/></td>
		<td class=header2><img runat="server" id="SortJoined" align="absmiddle"/> <asp:linkbutton runat=server id="Joined"/></td>
		<td class=header2 align=center><img runat="server" id="SortPosts" align="absmiddle"/> <asp:linkbutton runat=server id="Posts"/></td>
	</tr>
	
	<asp:repeater id=MemberList runat=server>
		<ItemTemplate>
			<tr>
				<td class=post><a href='<%# string.Format("userinfo.aspx?id={0}",DataBinder.Eval(Container.DataItem,"User_ID") ) %>'><%# Server.HtmlEncode(Convert.ToString(DataBinder.Eval(Container.DataItem,"user_nick"))) %></a></td>
				<td class=post><%# DataBinder.Eval(Container.DataItem,"RankName") %></td>
				<td class=post><%# FormatDateLong((System.DateTime)((System.Data.DataRowView)Container.DataItem)["user_registdate"]) %></td>
				<td class=post align=center><%# String.Format("{0:N0}",((System.Data.DataRowView)Container.DataItem)["user_NumPosts"]) %></td>
			</tr>
		</ItemTemplate>
	</asp:repeater>
</table>

<table class=command><tr><td class=navlinks><yaf:pager runat="server" linkedpager="Pager"/></td></tr></table>

<yaf:SmartScroller id="SmartScroller1" runat = "server" />
