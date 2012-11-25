<%@ Control language="c#" Codebehind="topics.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.topics" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>
<%@ Register TagPrefix="yaf" TagName="ForumList" Src="../controls/ForumList.ascx" %>

<yaf:PageLinks runat="server" id="PageLinks"/>

<asp:placeholder runat="server" id="SubForums" visible="false">
<table class="forumContent" cellspacing="1" cellpadding="0" width="100%">
	<tr class="header1"><td colspan="5"><%=GetSubForumTitle()%></td></tr>
	<tr class="header2">
		<td width=1%>&nbsp;</td>
		<td align=left><%# GetText("FORUM") %></td>
		<td align=center width=7%><%# GetText("topics") %></td>
		<td align=center width=7%><%# GetText("posts") %></td>
		<td align=center width=25%><%# GetText("lastpost") %></td>
	</tr>
	<yaf:forumlist runat="server" id="ForumList"/>
</table>
</asp:placeholder>

<table class=command cellspacing=0 cellpadding=0 width="100%">
	<tr>
		<td class="navlinks" align="left"><yaf:pager runat="server" id="Pager"/></td>
		<td align="right">
			<asp:linkbutton id=moderate1 runat=server cssclass="imagelink"/>
			<asp:linkbutton id=NewTopic1 runat="server" cssclass="imagelink"/>
		</td>
	</tr>
</table>

<table class="forumContent" cellSpacing=1 cellPadding=0 width="100%">
	<tr>
		<td class=header1 colspan=6><asp:label id=PageTitle runat="server"></asp:label></td>
	</tr>
  <tr>
    <td class=header2 width="1%">&nbsp;</td>
    <td class=header2 align=left><%# GetText("topics") %></td>
    <td class=header2 align=left width="20%"><%# GetText("topic_starter") %></td>
    <td class=header2 align=middle width="7%"><%# GetText("replies") %></td>
    <td class=header2 align=middle width="7%"><%# GetText("views") %></td>
    <td class=header2 align=middle width="25%"><%# GetText("lastpost") %></td>
   </tr>
<asp:repeater id=Announcements runat="server">
	<ItemTemplate>
		<yaf:TopicLine runat="server" DataRow=<%# Container.DataItem %>/>
	</ItemTemplate>
</asp:repeater>
<asp:repeater id=TopicList runat="server">
	<ItemTemplate>
		<yaf:TopicLine runat="server" DataRow=<%# Container.DataItem %>/>
	</ItemTemplate>
	<AlternatingItemTemplate>
		<yaf:TopicLine runat="server" IsAlt="True" DataRow=<%# Container.DataItem %>/>
	</AlternatingItemTemplate>
</asp:repeater>

<yaf:ForumUsers runat="server"/>

  <tr>
    <td align=middle colSpan=6 class=footer1>
      <table cellSpacing=0 cellPadding=0 width="100%">
        <tr>
				<td width="1%">
					<nobr>
					<%# GetText("showtopics") %>
					<asp:DropDownList id=ShowList runat="server" AutoPostBack="True"/>
					</nobr>
				</td>
				<td align="right">
				<asp:linkbutton id="WatchForum" runat="server"/><span id="WatchForumID" runat="server" visible="false"/></span>
				|
				<asp:linkbutton runat="server" id="MarkRead"/>
				<span id="RSSLinkSpacer" runat="server">|</span>
				<asp:hyperlink id="RssFeed" runat="server" />
				</td></tr></table>

</td></tr></table>

<table class=command width="100%" cellspacing=0 cellpadding=0>
	<tr>
		<td align="left" class="navlinks"><yaf:pager runat="server" linkedpager="Pager"/></td>
		<td align="right">
			<asp:linkbutton id=moderate2 runat=server cssclass="imagelink"/>
			<asp:linkbutton id=NewTopic2 runat="server" cssclass="imagelink"/>
		</td>
	</tr>
</table>

<table width=100% cellspacing=0 cellpadding=0>
<tr id="ForumJumpLine" runat="Server">
	<td align=right colspan=2>
		<%# GetText("Forum_Jump") %> <yaf:forumjump runat="server" />
	</td>
</tr>
<tr>
	<td valign=top><yaf:IconLegend runat="server"/></td>
	<td align=right>
		<table cellspacing=1 cellpadding=1>
			<tr>
				<td align="right" valign="top" class="smallfont">
					<yaf:PageAccess runat="server"/>
				</td>
			</tr>
		</table>
	</td>
</tr>
</table>

<yaf:SmartScroller id="SmartScroller1" runat = "server" />
