<%@ Control Language="c#" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<table width="100%" CellPadding="0" Cellspacing="0" id="usersOnline" >
	<th colspan="2">
		<lang:Label ref="information" runat="server" />
	</th>
	<tr>
		<td>
			<div>
				<span style="float: right;">
					<lang:Label ref="irc" runat="server" /><p/>
					<b>Admins</b><br/>
					<a href="http://nunos.zi-yu.com">Nuno Silva</a><br/>
					<a href="http://psantos.net">Pedro Santos</a>
				</span>
				<b><lang:Label ref="stats_registered" runat="server" />:</b> <orion:TotalUsers runat="server" /><br/>
				<b><lang:Label ref="stats_rulers" runat="server" />:</b> <orion:TotalRulers runat="server" /><br/>
				<b><lang:Label ref="OnlineUsersCount" runat="server" />:</b> <orion:OnlineUserCount runat="server" /></br>
				<b><lang:Label ref="LatestRuler" runat="server" />:</b> <orion:LatestRuler runat="server" /><br />
				<b><lang:Label ref="LatestUsers" runat="server" />:</b> <orion:LatestUsers runat="server" />
			</div>
		</td>
	</tr>
	<th colspan="2">
		<b><lang:Label ref="QuoteOfTheDay" runat="server" /></b>
	</th>
	<tr>
		<td><orion:QuoteOfTheDay runat="server" /></td>
	</tr>
</table>
