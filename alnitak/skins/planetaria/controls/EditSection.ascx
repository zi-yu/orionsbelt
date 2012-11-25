<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<asp:DataGrid id="dgrdSections" CellPadding="5" GridLines="None" BorderStyle="None" AutoGenerateColumns="False" runat="server" HeaderStyle-CssClass="headerItemStyle" ItemStyle-CssClass="itemStyle" AlternatingItemStyle-CssClass="alternatingItemStyle">
	<Columns>
		<asp:TemplateColumn>
			<HeaderTemplate>Home</HeaderTemplate>
			<ItemTemplate>
								
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<HeaderTemplate>&nbsp; Edit &nbsp;</HeaderTemplate>
			<ItemTemplate>
				<center>
					<asp:HyperLink ImageUrl="../Images/Edit.gif" Text="Edit" NavigateUrl='<%# String.Format("EditSection.aspx?id={0}", DataBinder.Eval( Container, "DataItem.section_id" ) )%>' Runat="Server" ID="Hyperlink1" NAME="Hyperlink1"/></center>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<HeaderTemplate>&nbsp; Delete &nbsp;</HeaderTemplate>
			<ItemTemplate>
				<center>
					<asp:ImageButton id="btnDelete" CommandName="Delete" ImageUrl="../Images/Delete.gif" AlternateText="Delete" Runat="Server" /></center>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<HeaderTemplate>&nbsp; Sort Up &nbsp;</HeaderTemplate>
			<ItemTemplate>
				<center>
					<asp:ImageButton CommandName="Up" ImageUrl="../Images/Up.gif" AlternateText="Up" Runat="Server" ID="Imagebutton1" NAME="Imagebutton1" /></center>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<HeaderTemplate>&nbsp; Sort Down &nbsp;</HeaderTemplate>
			<ItemTemplate>
				<center>
					<asp:ImageButton CommandName="Down" ImageUrl="../Images/Dn.gif" AlternateText="Down" Runat="Server" ID="Imagebutton2" NAME="Imagebutton2" /></center>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="section_name" HeaderText="Section Name"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Section Path">
			<ItemTemplate>
				<span class="content">
					<asp:HyperLink Text='<%# DataBinder.Eval( Container, "DataItem.section_path" )%>' NavigateUrl='<%# DataBinder.Eval( Container.DataItem, "section_path" ) %>' Runat="Server" ID="Hyperlink2" NAME="Hyperlink2"/>
				</span>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid>