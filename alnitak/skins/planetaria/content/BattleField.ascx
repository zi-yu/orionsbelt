<%@ Control Language="c#" Inherits="Alnitak.Battle.BattleField" EnableViewState="false" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>

<orion:QueueErrorReport id="queueError" EnableViewState="false" runat="server" />
<div align="center">
	<table id="battleField">
		<tr>
			<td>
				<asp:Label id="vs" runat="server" />
				<asp:Panel ID="field" runat="server"></asp:Panel>	
			</td>
			<td>
				<div align="center">
					<asp:Panel ID="menu" Runat="server"></asp:Panel>
					<asp:Button ID="turn" Runat="server" /><asp:Button ID="setPosition" Runat="server" /><br/>
                    <div style="height: 50px;">&nbsp;</div>
					<asp:Button ID="giveUp"  Runat="server" />
				</div>			
			</td>
		</tr>		
	</table>
</div>
<orion:MessageList id="messageList" runat="server" />


