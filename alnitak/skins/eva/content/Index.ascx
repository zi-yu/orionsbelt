<%@ Control Language="C#" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<style>
	#content td{
		padding:0px;
	}
</style>

<div align="center" >
	<orion:Flash SwfFromWebConfig="Intro" Width="100%" Height="347px" runat="server" />
</div>

<table id="news" >
	<th>
		<lang:Label ref="section_index" runat="server" />
	</th>
	<th>
		<lang:Label ref="screenshots" runat="server" />
	</th>
	<tr>
		<td>
			<div id="moveNews">
				<orion:ShowNews Format="Rss" runat="server" />
			</div>
		</td>
		<td id="fixedNews">
			<orion:ScreenShotViewer Max="5" runat="server"/>
			<div class="newsTitle"><b><lang:Label ref="top_rulers" runat="server"/></b></div>
			<div id="topRulers">
				<orion:TopRulers ShowOnline="false" ShowPlanets="false" ShowBattles="false" ShowRank="false" ShowScore="false" ShowOnlyTopPlayers="true" runat="server"/>
			</div>
		</td>
	</tr>
</table>
