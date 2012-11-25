<%@ Control Language="c#" Inherits="Alnitak.SectionMenu" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<div align="center">
	<asp:Repeater ID="menu" Runat="Server">
		<ItemTemplate>
			<a class="section_menu_link" href='<%#((SectionMenuLink)Container.DataItem).menuLink %>'>
				<asp:HyperLink ImageUrl='<%# OrionGlobals.getSkinImagePath(((SectionMenuLink)Container.DataItem).menuFileName.ToLower() + "_" + OrionGlobals.getCulture() + ".gif") %>' NavigateUrl='<%#((SectionMenuLink)Container.DataItem).menuLink %>'  EnableViewState="False" Runat="Server" ID="lnkSection"/>
			</a>
		</ItemTemplate>
	</asp:Repeater>
</div>