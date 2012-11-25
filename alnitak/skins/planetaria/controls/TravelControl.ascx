<%@ Control Language="C#" Inherits="Alnitak.TravelControl" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" TagName="TravelPanel" Src="TravelPanel.ascx" %>

<table width="100%" >
	<tr>
		<td width="100%" >
			<asp:PlaceHolder id="travelControlBase" runat="server" ></asp:PlaceHolder>
		</td>
	</tr>
	<tr>
		<td valign="top">
			<orion:TravelPanel id="travelPanel" runat="server"/>
		</td>
	</tr>
</table>

