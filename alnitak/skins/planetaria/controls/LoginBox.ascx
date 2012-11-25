<%@ Control Language="C#" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>


<table cellpadding="0" cellspacing="0" width="180">
	<tr>
		<td><orion:OrionSkinImage image="topleftcorner" runat="server"  ID="Orionskinimage1"/></td>
		<td background="<%=OrionGlobals.getSkinImagePath("borderhorizontaltop.gif")%>" colspan="2"/>
		</td>
		<td><orion:OrionSkinImage image="toprightcorner" runat="server"  ID="Orionskinimage2"/></td>
	</tr>
	<tr>
		<td background="<%=OrionGlobals.getSkinImagePath("borderverticalleft.gif")%>" />
		<td width="25" align="left">
			&nbsp;
		</td>
		<td width="145" align="left">
			<asp:Panel id="login" visible="true" runat="server" >
				<lang:Label ref="mail" runat="server" />:<br/>
				<asp:TextBox id="userMail" value="" Width="100%" CssClass="textbox" runat="server" EnableViewState="False" /><br/>
				<lang:Label ref="password" runat="server" />:<br/>
				<asp:TextBox id="password" value="" TextMode="password" CssClass="textbox" Width="100%" runat="server"  /><br/>
				<asp:CheckBox id="autoLogin" runat="server" /><lang:Label ref="login_auto-login" runat="server" ID="Label3"/><br/><br/>
				<div >
					<asp:ImageButton id="loginButton" CssClass="imageButton" runat="server" CausesValidation="False" />
					<asp:ImageButton id="registerButton" CssClass="imageButton" runat="server" CausesValidation="False" />	
				</div>
				<asp:Label id="message" CssClass="error" runat="server" />
			</asp:Panel>
			<asp:Panel id="logout" visible="False" runat="server">
				<div >
					<asp:ImageButton id="logoutButton" CssClass="imageButton" runat="server" CausesValidation="False" /><br/>
					<asp:ImageButton id="profileButton" CssClass="imageButton" runat="server" CausesValidation="False" /><br/>
					<asp:Panel ID="becomeRulerPanel" Visible="False" Runat="server"></asp:Panel>
				</div>
			</asp:Panel>
		</td>
		<td background="<%=OrionGlobals.getSkinImagePath("borderverticalright.gif")%>"/>
	</tr>
	<tr>
		<td><orion:OrionSkinImage image="bottomleftcorner"  runat="server" ID="Orionskinimage3"/></td>
		<td background="<%=OrionGlobals.getSkinImagePath("borderhorizontalbottom.gif")%>" colspan="2"></td>
		<td><orion:OrionSkinImage image="bottomrightcorner"  runat="server" ID="Orionskinimage4"/></td>
	</tr>
</table>