<%@ Control language="c#" Codebehind="im_yim.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.im_yim" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="Alnitak" %>

<yaf:PageLinks runat="server" id="PageLinks"/>

<center>
<asp:hyperlink runat="server" id="Msg"><img runat="server" id="Img" border="0"/></asp:hyperlink>
</center>

<yaf:SmartScroller id="SmartScroller1" runat = "server" />
