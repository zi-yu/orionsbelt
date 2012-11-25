<%@ Control Language="c#" Inherits="Alnitak.RankCalculator" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<asp:Label id="userRank" runat="server" />
<p/>
<b>Rank 1</b>: <asp:TextBox id="rank1" runat="server" /> <asp:Label id="rank1Label" runat="server" />
<p/>
<b>Rank 2</b>: <asp:TextBox id="rank2" runat="server" /> <asp:Label id="rank2Label" runat="server" />
<p />
<asp:DropDownList id="resultDDL" runat="server" />
<p/>
<asp:Button OnClick="Calculate" Text="Compute" runat="server" />
