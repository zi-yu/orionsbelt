<%@ Control language="c#" Codebehind="search.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.search" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>

	<yaf:PageLinks runat="server" id="PageLinks"/>
	<script language="javascript">
function doSearch() {
   if (window.event.keyCode == 13) _ctl0._ctl1__ctl0_btnSearch.focus();
}
</script>
	<table class="forumContent" cellSpacing="1" cellPadding="0" width="100%">
		<tr>
			<td class="header1" colspan="2"><%= GetText("title") %></td>
		</tr>
		<tr>
			<td class="postheader" colspan="2" align="center">
				<asp:dropdownlist id="listForum" runat="server"/>
				<asp:dropdownlist id="listResInPage" runat="server"/>
				<asp:dropdownlist id="listSearchWhere" runat="server"/>
				<asp:dropdownlist id="listSearchWath" runat="server"/>
			</td>
		</tr>
		<tr>
			<td class="postheader" colspan="2" align="center">
				<asp:textbox id="txtSearchString" runat="server" Width="350px" onkeypress="doSearch();"/>
				<asp:button id="btnSearch"  runat="server"/>
			</td>
		</tr>
	</table><br/>
	
	<table class="command" cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td class="navlinks"><yaf:pager runat="server" id="Pager"/></td>
		</tr>
	</table>	
	
	<table class="forumContent" cellSpacing="1" cellPadding="0" width="100%">	
		<asp:repeater id="SearchRes" runat="server">
			<HeaderTemplate>
				<tr>
					<td class="header1" colspan="2"><%= GetText("RESULTS") %></td>
				</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr class="header2"><td colspan="2"><b><%= GetText("topic") %></b> <a href="<%# yaf.Forum.GetLink(yaf.Pages.posts,"t={0}",DataBinder.Eval(Container.DataItem, "TopicID")) %>"><%# DataBinder.Eval(Container.DataItem, "Topic") %></a></td></tr>
				<tr class="postheader">
					<td width="140px" id="NameCell" valign="top">
						<a "select a.ForumID, a.TopicID, a.Topic, b.User_ID, b.user_nick, c.MessageID, c.Posted, c.Message, c.Flags from yaf_topic a left join yaf_message c on a.TopicID = c.TopicID left join orionsbelt_users b on c.UserID = b.User_ID join yaf_vaccess x on x.ForumID=a.ForumID where x.ReadAccess<>0 and x.UserID=27 and ( (c.Message like '%teste%' or a.Topic like '%teste%' ) )  order by c.Posted desc"="<%# DataBinder.Eval(Container.DataItem, "MessageID") %>"/>
						<b><a href="<%# string.Format("userinfo.aspx?id={0}",DataBinder.Eval(Container.DataItem, "User_ID")) %>"><%# Server.HtmlEncode(Convert.ToString(DataBinder.Eval(Container.DataItem, "user_nick"))) %></a></b>						
					</td>
					<td width="80%" class="postheader">
						<b><%# GetText("POSTED") %></b> 
						<%# FormatDateTime((System.DateTime)DataBinder.Eval(Container.DataItem, "Posted")) %>
					</td>
				</tr>
				<tr class="post">
					<td width="140px">&nbsp;</td>
					<td width="80%">
						<%# FormatMessage(Container.DataItem) %>
					</td>
				</tr>
			</ItemTemplate>	
			<AlternatingItemTemplate>
				<tr class="header2"><td colspan="2"><b><%= GetText("topic") %></b> <a href="<%# yaf.Forum.GetLink(yaf.Pages.posts,"t={0}",DataBinder.Eval(Container.DataItem, "TopicID")) %>"><%# DataBinder.Eval(Container.DataItem, "Topic") %></a></td></tr>
				<tr class="postheader">
					<td width="140px" id="NameCell" valign="top">
						<a name="<%# DataBinder.Eval(Container.DataItem, "MessageID") %>"/>
						<b><a href="<%# string.Format("userinfo.aspx?id={0}",DataBinder.Eval(Container.DataItem, "User_ID")) %>"><%# Server.HtmlEncode(Convert.ToString(DataBinder.Eval(Container.DataItem, "user_nick"))) %></a></b>						
					</td>
					<td width="80%" class="postheader">
						<b><%# GetText("POSTED") %></b> 
						<%# FormatDateTime((System.DateTime)DataBinder.Eval(Container.DataItem, "Posted")) %>
					</td>
				</tr>
				<tr class="post_alt">
					<td width="140px">&nbsp;</td>
					<td width="80%">
						<%# FormatMessage(Container.DataItem) %>
					</td>
				</tr>
			</AlternatingItemTemplate>			
			<FooterTemplate>			
				<tr>
					<td class="footer1" colspan="2">&nbsp;</td>
				</tr>
			</FooterTemplate>
		</asp:repeater>
		<asp:placeholder id="NoResults" runat="Server" visible="false">
			<tr>
				<td class="header2" colspan="2"><%= GetText("RESULTS") %></td>
			</tr>			
			<tr>
				<td class="postheader" colspan="2" align="center"><br/><%= GetText("NO_SEARCH_RESULTS") %><br/></br></td>
			</tr>
			<tr>
				<td class="footer1" colspan="2">&nbsp;</td>
			</tr>			
		</asp:placeholder>				
	</table>
	<table class="command" width="100%" cellspacing="0" cellpadding="0">
		<tr>
			<td class="navlinks"><yaf:pager runat="server" linkedpager="Pager"/></td>
		</tr>
	</table>
	
<yaf:SmartScroller id="SmartScroller1" runat = "server" />
