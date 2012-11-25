<%@ Control Language="C#" Inherits="Alnitak.ExceptionLogPage" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Import Namespace="Alnitak" %>
<script language="C#" runat="server">
	int id=0;
	string eliminarExp = CultureModule.getLanguage().getContent("exceptionLog_clear");
</script>	

<asp:PlaceHolder ID="content" Runat="server" EnableViewState="False">
	<div align="center">
		<asp:Button id="removeAllException" CssClass="button" runat="server" />
	</div>
	<asp:Repeater ID="exceptions" Runat="server" EnableViewState="False">
		<ItemTemplate>
			<div class="stats-box">
				<b><lang:Label ref="orionsbelterror_date" runat="server" /></b>: <%# ((ExceptionInfo)Container.DataItem).Date %>
				<img src='<%= OrionGlobals.getCommonImagePath("remove.gif") %>' onClick="removeException(<%# ((ExceptionInfo)Container.DataItem).Id %>)" alt='<%= eliminarExp %>'/>
				<br />
				<img src='<%= OrionGlobals.getCommonImagePath("plus.gif") %>' onClick="show('<%= "exception_" + (++id).ToString() %>',this);" /><b><lang:Label ref="orionsbelterror_name" runat="server" /></b>: <%# ((ExceptionInfo)Container.DataItem).Name %><br />
				<table id='<%= "exception_" + (id).ToString() %>' style="display:none;">
					<tr>
						<td>
							<b><%# ((ExceptionInfo)Container.DataItem).Message %>'</b><br />
						</td>
					</tr>
					<tr>
						<td>
							<%# ((ExceptionInfo)Container.DataItem).StackTrace %>'<br />
						</td>
					</tr>
				</table>
			</div>
		</ItemTemplate>
	</asp:Repeater>
</asp:PlaceHolder>

<asp:PlaceHolder ID="noContent" Runat="server">
	<b><lang:Label ref="orionsbelterror_noContent" runat="server" /></b>
</asp:PlaceHolder>


