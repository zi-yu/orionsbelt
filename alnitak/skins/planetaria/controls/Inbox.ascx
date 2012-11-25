<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<table cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td><orion:OrionSkinImage image="topleftcorner" runat="server" ID="Orionskinimage5"/></td>
		<td background="<%=OrionGlobals.getSkinImagePath("borderhorizontaltop.gif")%>" colspan="2"/></td>
		<td><orion:OrionSkinImage image="toprightcorner" runat="server" ID="Orionskinimage6"/></td>
	</tr>
	<tr>
		<td background="<%=OrionGlobals.getSkinImagePath("borderverticalleft.gif")%>" />
		<td width="25" align="left">
			&nbsp;
		</td>
		<td width="100%" align="left">
			<a href="<%= OrionGlobals.AppPath %>forum/default.aspx?g=pmessage" ><orion:OrionSkinImage image="subButtons/send" language="true" runat="server" /></a></br>
			<a href="<%= OrionGlobals.AppPath %>forum/default.aspx?g=cp_inbox" ><orion:OrionSkinImage image="subButtons/received" language="true" runat="server" /></a></br>
			<a href="<%= OrionGlobals.AppPath %>forum/default.aspx?g=cp_inbox&sent=1" ><orion:OrionSkinImage image="subButtons/sent" language="true" runat="server"/></a></br>
		</td>
		<td background="<%=OrionGlobals.getSkinImagePath("borderverticalright.gif")%>"/>
	</tr>
	<tr>
		<td><orion:OrionSkinImage image="bottomleftcorner"  runat="server" ID="Orionskinimage7"/></td>
		<td background="<%=OrionGlobals.getSkinImagePath("borderhorizontalbottom.gif")%>" colspan="2"></td>
		<td><orion:OrionSkinImage image="bottomrightcorner"  runat="server" ID="Orionskinimage8"/></td>
	</tr>
</table>