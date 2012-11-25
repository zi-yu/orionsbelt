<%@ Control Language="c#" EnableViewState="True" Inherits="Alnitak.ProfileControl" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<div class="loginPanel">

	<div><lang:Label id="userUpdated" ref="profile_profile-updated" runat="server"/></div>

	<div class="error">

<%-- Nick --%>

		<asp:RequiredFieldValidator 
			id="nickValidator"
			ControlToValidate="nick"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</asp:RequiredFieldValidator>
		
		<orion:OnlyTextValidator
			id="nickTextValidator"
			ControlToValidate="nick"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:OnlyTextValidator>
		
		<orion:CharCountValidator
			id="nickRangeValidator"
			ControlToValidate="nick"
			Display="Dynamic"
			InitialValue=""
			Min="0"
			Max="30"
			runat="server">
		</orion:CharCountValidator>
		
<%-- Password --%>
		
		<orion:CharCountValidator
			id="passRangeValidator"
			ControlToValidate="password"
			Display="static"
			InitialValue=""
			Min="3"
			Max="10"
			runat="server">
		</orion:CharCountValidator>
		
		<%--asp:CompareValidator
			id="passValidator2"
			ControlToValidate="password"
			ControlToCompare="password2"
			Display="static"
			runat="server">
		</asp:CompareValidator--%>
		
<%-- WEBSITE --%>

		<orion:UrlValidator
			id="avatarUrlValidator"
			ControlToValidate="avatar"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:UrlValidator>
		
		<orion:CharCountValidator
			id="avatarRangeValidator"
			ControlToValidate="avatar"
			Display="Dynamic"
			InitialValue=""
			Min="0"
			Max="250"
			runat="server">
		</orion:CharCountValidator>

<%-- WEBSITE --%>

		<orion:UrlValidator
			id="urlTextValidator"
			ControlToValidate="website"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:UrlValidator>
		
		<orion:CharCountValidator
			id="urlRangeValidator"
			ControlToValidate="website"
			Display="Dynamic"
			InitialValue=""
			Min="0"
			Max="60"
			runat="server">
		</orion:CharCountValidator>
		
		<orion:DirectoryValidator
			id="imagesDirValidator"
			ControlToValidate="imagesDir"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:DirectoryValidator>
		
<%-- CONTACTOS --%>

		<orion:MailValidator
			id="msnValidator"
			ControlToValidate="msn"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:MailValidator>
		
		<asp:CompareValidator
			id="icqValidator"
			Operator="DataTypeCheck"
			Type="Integer"
			ControlToValidate="icq"
			Display="Dynamic"
			runat="server">
		</asp:CompareValidator>
		
		<orion:OnlyTextValidator
			id="aimValidator"
			ControlToValidate="aim"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:OnlyTextValidator>
		
		<orion:MailValidator
			id="jabberValidator"
			ControlToValidate="jabber"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:MailValidator>
		
		<orion:MailValidator
			id="yahooValidator"
			ControlToValidate="yahoo"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:MailValidator>
		
<%-- OUTROS --%>

		<orion:OnlyTextValidator
			id="signatureValidator"
			ControlToValidate="signature"
			Display="Dynamic"
			InitialValue=""
			runat="server">
		</orion:OnlyTextValidator>
		
		<orion:CharCountValidator
			id="signatureRangeValidator"
			ControlToValidate="signature"
			Display="Dynamic"
			InitialValue=""
			Min="0"
			Max="255"
			runat="server">
		</orion:CharCountValidator>

	</div>

<h2>
	<lang:Label ref="profile_personal-info" runat="server" />
</h2>

<div class="box">
	<lang:Label ref="mail" runat="server"/> (login): <b><asp:Label id="userMail" runat="server"/></b> <br /> <br /> 
	
	<lang:Label ref="nick" runat="server"/>:<br /> 
	<asp:TextBox id="nick" Width="100%" CssClass="textbox" runat="server" /> <br />
	
	<lang:Label ref="profile_website" runat="server"/>:<br /> 
	<asp:TextBox id="website" Width="100%" CssClass="textbox" runat="server" /> <br /> 
	
	<lang:Label ref="password" runat="server" />:<br /> 
	<asp:TextBox id="password" TextMode="password" Width="100%" runat="server" /> <br />
	<lang:Label ref="retype-password" runat="server" />:<br /> 
	<asp:TextBox id="password2" TextMode="password" Width="100%" runat="server" /> <br />  
	
	<lang:Label ref="profile_avatar" runat="server" ID="Label1"/>:<br /> 
	<asp:TextBox id="avatar" Width="100%" CssClass="textbox" runat="server" /> <br />
	<div align="center">
		<fieldset>
			<legend>
				<b><lang:Label ref="profile_avatarText" runat="server" /></b>
			</legend>
			<asp:Image ID="avatarImg" CssClass="avatar" Runat="server" />
		</fieldset>
	</div>
</div>

<h2><lang:Label ref="profile_player_preferences" runat="server" /></h2>
<div class="box">

	<lang:Label ref="profile_vacation" runat="server"/>:
	<asp:CheckBox EnableViewState="True" id="vacation" CssClass="dropdownlist" runat="server" /><br/>
	<lang:Label ref="profile_vacation_warning" runat="server"/><p />

</div>

<h2><lang:Label ref="profile_preferences" runat="server" /></h2>
<div class="box">

	<lang:Label ref="profile_language" runat="server"/>:
	<asp:DropDownList EnableViewState="True" id="lang" CssClass="dropdownlist" runat="server" /><p />
	
	<lang:Label ref="profile_skin" runat="server"/>:
	<asp:DropDownList EnableViewState="True" id="skin" CssClass="dropdownlist" runat="server" /><p />
	
	<asp:Label id="imagesDirText" runat="server" /><br />
	<asp:TextBox id="imagesDir" Width="100%" CssClass="textbox" Runat="server" />

</div>

<h2><lang:Label ref="profile_contacts" runat="server" /></h2>
<div class="box">

	<lang:Label ref="profile_msn" runat="server"/>:<br /> 
	<asp:TextBox id="msn" Width="100%" CssClass="textbox" runat="server" /> <br /> 
	
	<lang:Label ref="profile_icq" runat="server"/>:<br /> 
	<asp:TextBox id="icq" Width="100%" CssClass="textbox" runat="server" /> <br />
	
	<lang:Label ref="profile_aim" runat="server"/>:<br /> 
	<asp:TextBox id="aim" Width="100%" CssClass="textbox" runat="server" /> <br />
	
	<lang:Label ref="profile_jabber" runat="server"/>:<br /> 
	<asp:TextBox id="jabber" Width="100%" CssClass="textbox" runat="server" /> <br />
	
	<lang:Label ref="profile_yahoo" runat="server"/>:<br /> 
	<asp:TextBox id="yahoo" Width="100%" CssClass="textbox" runat="server" /> <br />
</div>

<h2><lang:Label ref="profile_others" runat="server" /></h2>
<div class="box">
	<lang:Label ref="profile_signature" runat="server" /> <br />
	<asp:TextBox id="signature" Width="100%" MaxLength="255" TextMode="MultiLine" CssClass="textbox" runat="server" /> <br />
</div>

	<p />

	<div align="center"> 
		<asp:Button id="updateButton" CssClass="button" OnClick="onUpdateClick" runat="server" />
	</div>

</div>
