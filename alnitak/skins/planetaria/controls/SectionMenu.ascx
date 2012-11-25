<%@ Control Language="c#" Inherits="Alnitak.SectionMenu" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<table cellpadding="0" cellspacing="0" border="0" align="right">  
	<tr>
	<asp:Repeater ID="menu" Runat="Server">
		<ItemTemplate>
			<td class="section_menu_button" >
				<a class="section_menu_link" href='<%#((SectionMenuLink)Container.DataItem).menuLink %>'><%# CultureModule.getContent( "section_" + ((SectionMenuLink)Container.DataItem).menuFileName.ToLower() ) %></a>
			</td>
			<td>
				<orion:OrionSkinImage image="sectionButton" runat="server"/>
			</td>
		</ItemTemplate>
	</asp:Repeater>
	</tr>
</table>


