<%@ Control Language="C#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="left" TagName="LeftMenu" Src="../controls/LeftMenu.ascx" %>
<%@ Register TagPrefix="left" TagName="RightMenu" Src="../controls/RightMenu.ascx" %>
<%@ Register TagPrefix="menu" TagName="SectionMenu" Src="../controls/SectionMenu.ascx" %>
<%@ Register TagPrefix="orion" TagName="UsersOnline" Src="../controls/UsersOnline.ascx" %>

<table border="0" cellspacing="0" cellpadding="0" >
	<tr>
		<td colspan="5">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr >
					<td rowspan="2" width="333" height="153" valign="top" >
						<orion:OrionSkinImage image="planets" runat="server"/>
					</td>
					<td background="<%= OrionGlobals.getSkinImagePath("titleFill.gif") %>" class="nameImage"  >
						<orion:OrionSkinImage image="name" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="section_menu" ><menu:SectionMenu runat="server" /></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr valign="top">
		<td  >
			<left:LeftMenu runat="server" />
		</td>
		<td>&nbsp;</td>
		<td width="100%" >
			<table cellpadding="0" cellspacing="0" width="100%" class="content">
				<tr>
					<td class="page_title">
						<orion:PageTitle runat="server" />
					</td>
				</tr>
				<tr>
					<td height="5px"></td>
				</tr>
				<tr>
					<td class="content">
						<asp:PlaceHolder id="Content" Runat="Server" />	
					</td>
				</tr>
			</table>
			<p/>
			<orion:UsersOnline runat="server" />
			<p/>
		</td>
		<td>&nbsp;</td>
		<td class="right_menu" >
			<left:RightMenu runat="server" />
		</td>
	</tr>
	<tr>
		<td colspan="5">
			<div align="right">
				<orion:OrionSkinImage image="credits" runat="server" />
			</div>
		</td>
	</tr>
</table>