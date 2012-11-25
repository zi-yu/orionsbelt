<%@ Control Language="C#" %>
<%@ OutputCache Duration="3600" VaryByParam="none" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="top">
	<orion:OrionSkinImage image="logo" type="jpg" runat="server"/>
</div>

<table width="100%" height="134" cellpadding=0 cellspacing=0 valign=top>
	<tr>
		<td background="<%= BaseControl.ResolveImgUrl ("light","banner-start.jpg") %>">
			<div style="width: 421px"></div>
		</td>
		<td width="100%" background="<%= BaseControl.ResolveImgUrl ("light","banner-middle.png") %>">
		</td>
		<td background="<%= BaseControl.ResolveImgUrl ("light","banner-end.jpg") %>">
			<div style="width: 140px"></div>
		</td>
	</tr>
</table>

