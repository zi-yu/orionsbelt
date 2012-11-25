<%@ Control Language="C#" Inherits="Alnitak.ScanReportControl" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="planetInfoZoneTitle">
	<b><lang:Label ref="generalInformation" runat="server" /></b>
</div>

<table class="planetFrame">
	<tr>
		<td valign="top">
			<table width="100%">
				<tr>
					<td class="row1"><b><lang:Label ref="name" runat="server" /></b></td>
					<td class="row2"><b><asp:Label CssClass="leftLabel" id="name" runat="server" /></b></td>
				</tr>
				<tr>
					<td class="row1"><b><lang:Label ref="section_ruler" runat="server" /></b></td>
					<td class="row2"><b><asp:Label CssClass="leftLabel" id="ruler" runat="server" /></b></td>
				</tr>
				<tr>
					<td class="row1"><b><lang:Label ref="PlanetInBattle" runat="server" /></b></td>
					<td class="row2"><asp:Image id="inBattle" runat="server" /></td>
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
					<td class="row2"><b><asp:Label id="terrain" runat="server" /></b></td>
				</tr>
				<tr>
					<td class="row1"><lang:Label ref="culture" runat="server" /></td>
					<td class="row2"><asp:Label id="cultureValue" runat="server" /></td>
				</tr>
				<tr>
					<td class="row1"><lang:Label ref="travelTime" runat="server" /></td>
					<td class="row2"><asp:Label id="travelTime" runat="server" /></td>
				</tr>
			</table>
		</td>
		<td valign="top" width="100">
			<asp:Image id="img" Width="100" Height="100" CssClass="planetInfoZone" runat="server" />
		</td>
	</tr>
</table>


<asp:PlaceHolder id="level2" Visible="false" runat="server"><div class="planetInfoZoneTitle">
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
</table></asp:PlaceHolder>

<asp:PlaceHolder id="level3" Visible="false" runat="server">

<div class="planetInfoZoneTitle">
	<b><lang:Label ref="section_buildings" runat="server" /></b>
</div>
<table class="planetFrame">
	<tr>
		<td class="row1"><lang:Label ref="StarPort" runat="server" /></td>
		<td class="row2"><asp:Image id="hasStarPort" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="CommsSatellite" runat="server" /></td>
		<td class="row2"><asp:Image id="hasCommsSatellite" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="Gate" runat="server" /></td>
		<td class="row2"><asp:Image id="hasGate" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="StarGate" runat="server" /></td>
		<td class="row2"><asp:Image id="hasStarGate" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="Hospital" runat="server" /></td>
		<td class="row2"><asp:Image id="hasHospital" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="LandReclamation" runat="server" /></td>
		<td class="row2"><asp:Image id="hasLandReclamation" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="MineralExtractor" runat="server" /></td>
		<td class="row2"><asp:Image id="hasMineralExtractor" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="Spa" runat="server" /></td>
		<td class="row2"><asp:Image id="hasSpa" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="StockMarkets" runat="server" /></td>
		<td class="row2"><asp:Image id="hasStockMarkets" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="WaterReclamation" runat="server" /></td>
		<td class="row2"><asp:Image id="hasWaterReclamation" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="Turret" runat="server" /></td>
		<td class="row2"><asp:Image id="hasTurret" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="IonCannon" runat="server" /></td>
		<td class="row2"><asp:Image id="hasTurret" runat="server" /></td>
	</tr>
</table>


<div class="planetInfoZoneTitle">
	<b><lang:Label ref="fleet_planetShips" runat="server" /></b>
</div>
<table class="planetFrame">
	<tr>
		<td class="row1"><lang:Label ref="scan_fleet_number" runat="server" /></td>
		<td class="row2"><asp:Label id="fleetNumber" runat="server" /></td>
	</tr>
	<tr>
		<td class="row1"><lang:Label ref="scan_ship_number" runat="server" /></td>
		<td class="row2"><asp:Label id="shipsNumber" runat="server" /></td>
	</tr>
</table>

</asp:PlaceHolder>

<ul class="help_zone">
	<li><asp:HyperLink runat="server" id="scanWiki" /></li>
</ul>
