<%@ Control Language="C#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="left" TagName="LeftMenu" Src="../controls/LeftMenu.ascx" %>
<%@ Register TagPrefix="left" TagName="RightMenu" Src="../controls/RightMenu.ascx" %>
<%@ Register TagPrefix="menu" TagName="SectionMenu" Src="../controls/SectionMenu.ascx" %>
<%@ Register TagPrefix="orion" TagName="UsersOnline" Src="../controls/UsersOnline.ascx" %>

<table width="100%" border="0" cellspacing="0" cellpadding="0" >
	<tr>
		<td colspan="5" class="title" valign="top">
			<orion:RoleVisible showTo="guest" runat="server" >
				<a href="login.aspx"><orion:OrionSkinImage image="login" type="gif" Css="floatRight" Language="true" runat="server" /></a>
				<a href="regist.aspx"><orion:OrionSkinImage image="register" type="gif" Css="floatRight" Language="true" runat="server" /></a>
			</orion:RoleVisible>
			<orion:RoleVisible showTo="user-no-ruler" runat="server" >
				<a href="addruler.aspx"><orion:OrionSkinImage image="play" type="gif" Css="floatRight" Language="true" runat="server" /></a>
			</orion:RoleVisible>
			<orion:OrionSkinImage image="logo" type="gif" runat="server" />
			<menu:SectionMenu runat="server" />
		</td>
	</tr>
	<tr valign="top">
		<td class="left_menu" width="140px">
			<left:LeftMenu runat="server" />
		</td>
		<td class="border"><orion:OrionSkinImage image="border" type="gif" runat="server"/></td>
		<td width="100%" id="allContent" >
			<table id="content">
				<tr>
					<td id="contentContainertd">
						<asp:PlaceHolder id="Content" Runat="Server" />	
					</td>
				</tr>
			</table>
			<orion:UsersOnline runat="server" />
		</td>
		<td class="border"><orion:OrionSkinImage image="border" type="gif" runat="server" /></td>
		<td class="right_menu" width="129">
			<left:RightMenu runat="server" />
		</td>
	</tr>
	<tr>
		<td colspan="5" align="center">
			<br/>
			<p>
				<a href="http://www.gsmbit.com" target="_blank" class="logo"><img src="http://www.gsmbit.com/banners/orionsbelt_88x31_03.jpg" border="0" alt="GSMBit" /></a>
				<a href="http://sourceforge.net"><img src="http://sourceforge.net/sflogo.php?group_id=99352&amp;amp;type=1" width="88" height="31" border="0" alt="SourceForge.net Logo" /></a>
			</p>
			Copyright  <a href="about.aspx">Orion's Belt Team</a> 2005
		</td>
	</tr>
</table>
