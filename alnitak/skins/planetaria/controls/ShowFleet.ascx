<%@ Control Language="c#" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<asp:PlaceHolder ID="hasShips" Runat="server">
	<script language="C#" runat="server">
		static int id = 0;
	</script>
	<div class="planetInfoZoneTitle" width="100%" style="margin-bottom:2px;">
		<b><asp:Label id="title" runat="server" /></b>
	</div>
	<div>
		<asp:Repeater EnableViewState="False" ID="fleets" Runat="server">
			<ItemTemplate>
				<table class="frame" width="400" cellpadding="0" cellspacing="0">
					<tr>
						<td class="smallPadding" width="360">
							<img src='<%= OrionGlobals.getCommonImagePath("plus.gif") %>' onClick="show('<%= "fleet_" + (++id).ToString() %>',this);" />
							<b><asp:Label ID="fleetName" CssClass="fleetName" Runat="server" /></b>
						</td>
						<td align="left" width="40">
							<asp:Label ID="removeFleet" Runat="server" />
						</td>
					</tr>
					<asp:PlaceHolder EnableViewState="False" ID="content" Runat="server">
						<tr>
							<td colspan="2" >
								<table border="0" id='<%= "fleet_" + (id).ToString() %>' cellpadding="0" cellspacing="0" style="display:none;">
									<asp:PlaceHolder ID="fleetInformationPanel" Runat="server">
										<tr>
											<td class="borderTop" colspan="3" style="padding: 5px 0px 5px 0px;">
												<asp:Label ID="fleetState" Runat="server" /><br/>
												<asp:Label ID="fleetInformation" Runat="server" />
											</td>
										</tr>
									</asp:PlaceHolder>
									<tr>
										<td align="center" class="borderTop">
											&nbsp;
										</td>
										<td align="center" class="borderTop">
											<lang:Label ref="fleet_quant" runat="server"/>
										</td>
										<td align="center" class="borderTop">
											<lang:Label ref="fleet_type" runat="server"/>
										</td>
									</tr>
									<asp:Repeater EnableViewState="False" ID="fleet" Runat="server">
										<ItemTemplate>
											<tr height="40">
												<td width="80" align="center" class="borderTop">
													<orion:OrionCommonImage id="shipImage" runat="server" />
												</td>
												<td width="120" align="center" class="borderTop">
													<asp:Label id="shipQuantity" Runat="server" />
												</td>
												<td width="200" align="center" class="borderTop smallPadding">
													<asp:Label id="shipName" Runat="server" />
												</td>
											</tr>
										</ItemTemplate>
									</asp:Repeater>
								</table>
							</td>
						</tr>
					</asp:PlaceHolder>
					<asp:PlaceHolder EnableViewState="False" ID="noContent" Runat="server">
						<tr >
							<td colspan="2">
								<table id='<%= "fleet_" + (id).ToString() %>' cellpadding="0" cellspacing="0"  width="400" style="display:none;">
									<tr>
										<td width="400" class="borderTop smallPadding">
											<lang:Label ref="military_noShipsInThisFleet" runat="server" ID="Label2"/>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</asp:PlaceHolder>
				</table>
				<br/>
			</ItemTemplate>
		</asp:Repeater>
	</div>
</asp:PlaceHolder>
