<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="planetInfoZoneTitle">
	<b><lang:Label ref="generalInformation" runat="server" /></b>
</div>

<table class="planetFrame">
	<tr>
		<td valign="top" width="100%">
			<div class="planetInfoZoneBody2">
			
				<table width="90%">
					<tr>
						<td class="row1"><b><lang:Label ref="name" runat="server" /></b></td>
						<td class="row2"><b><asp:Label CssClass="leftLabel" id="name" runat="server" /></b></td>
					</tr>
					<tr>
						<td class="row1"><lang:Label ref="coordinate" runat="server" /></td>
						<td class="row2"><asp:Label id="coordinate" runat="server" /></td>
					</tr>
					<tr>
						<td class="row1"><lang:Label ref="diameter" runat="server" /></td>
						<td class="row2"><asp:Label id="diameter" runat="server" /></td>
					</tr>
					<tr>
						<td class="row1"><lang:Label ref="mass" runat="server" /></td>
						<td class="row2"><asp:Label id="mass" runat="server" /></td>
					</tr>
					<tr>
						<td class="row1"><lang:Label ref="temperature" runat="server" /></td>
						<td class="row2"><asp:Label id="temperature" runat="server" /></td>
					</tr>
					<tr>
						<td class="row1"><lang:Label ref="escape" runat="server" /></td>
						<td class="row2"><asp:Label id="escape" runat="server" /></td>
					</tr>
					<tr>
						<td class="row1"><lang:Label ref="terrain" runat="server" /></td>
						<td class="row2"><asp:Label id="terrain" runat="server" /></td>
					</tr>
				</table>
			</div>
		</td>
		<td valign="top">
			<asp:Image id="img" Width="100" Height="100" CssClass="planetInfoZone" runat="server" />
		</td>
	</tr>

</table>
	
<div class="planetInfoZoneTitle">
	<b><lang:Label ref="planetRicheness" runat="server" /></b>
</div>


<table class="planetFrame">
	<tr>
		<td class="row1"><lang:Label ref="mineral" runat="server" /></td>
		<td class="row2"><asp:Panel CssClass="graph" id="mineral" runat="server">&nbsp;</asp:Panel></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="food" runat="server" /></td>
		<td class="row2"><asp:Panel CssClass="graph" id="food" runat="server">&nbsp;</asp:Panel></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="gold" runat="server" /></td>
		<td class="row2"><asp:Panel CssClass="graph" id="gold" runat="server">&nbsp;</asp:Panel></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="energy" runat="server" /></td>
		<td class="row2"><asp:Panel CssClass="graph" id="energy" runat="server">&nbsp;</asp:Panel></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="groundSpace" runat="server" /></td>
		<td class="row2"><asp:Panel CssClass="graph" id="groundSpace" runat="server">&nbsp;</asp:Panel></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="waterSpace" runat="server" /></td>
		<td class="row2"><asp:Panel CssClass="graph" id="waterSpace" runat="server">&nbsp;</asp:Panel></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="orbitSpace" runat="server" /></td>
		<td class="row2"><asp:Panel CssClass="graph" id="orbitSpace" runat="server">&nbsp;</asp:Panel></td>
	</tr>
</table>

<orion:ResourcesList id="resourcesList" runat="server" />
