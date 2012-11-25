<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<asp:PlaceHolder EnableViewState="False" ID="placeholder" Runat="server">
	<dl>
		<dt><lang:Label ref="leftmenu_subMenu" runat="server" /></dt>
		<asp:Repeater EnableViewState="False" id="menu" Runat="Server">
			<ItemTemplate>
				<dd><a href='<%#((SectionMenuLink)Container.DataItem).menuLink + OrionGlobals.getQueryString() %>'><%# CultureModule.getContent( "section_" + ((SectionMenuLink)Container.DataItem).menuFileName.ToLower() ) %></a></dd>
			</ItemTemplate>
			<FooterTemplate>
				<dd class="lastItem"></dd>
			</FooterTemplate>
		</asp:Repeater>
	</dl>
</asp:PlaceHolder>
