<%@ Control Language="C#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<p><lang:Label ref="about_intro" runat="server" /></p>

<h2><lang:Label ref="administrators" runat="server" /></h2>

<img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/Admin.gif") %>" />
<a href="http://orionsbelt.zi-yu.com/userinfo.aspx?id=2" class="ruler">Nuno Silva</a><br/>
<lang:Label ref="about_pyro" runat="server" />
<p/>
<img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/Admin.gif") %>" />
<a href="http://orionsbelt.zi-yu.com/userinfo.aspx?id=1" class="ruler">Pedro Santos</a><br/>
<lang:Label ref="about_pre" runat="server" />

<h2><lang:Label ref="about_artists" runat="server" /></h2>

<p><lang:Label ref="about_artists_intro" runat="server" /></p>

<img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/Artist.gif") %>" />
<b>Ricardo Vieira</b><br/>
<lang:Label ref="about_ricard" runat="server" />
<p/>
<img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/Artist.gif") %>" />
<b>Lu&iacute;s Floureiro</b><br/>
<lang:Label ref="about_zeroshift" runat="server" />
<p/>
<img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/Artist.gif") %>" />
<b>Pedro Amaral Couto</b><br/>
<lang:Label ref="about_pedroac" runat="server" />
<p/>
<img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/Artist.gif") %>" />
<b>NunoLAC</b><br/>
<lang:Label ref="about_nunolac" runat="server" />
<p/>

<h2><lang:Label ref="about_betas" runat="server" /></h2>

<p><lang:Label ref="about_betas_intro" runat="server" /></p>

<ul>
	<li><img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/BetaTester.gif") %>" /> 
		<a href="http://orionsbelt.zi-yu.com/userinfo.aspx?id=13" class="ruler">Joaquim Leiteiro</a>
	</li>
	<li><img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/BetaTester.gif") %>" /> 
		<a href="http://orionsbelt.zi-yu.com/userinfo.aspx?id=4" class="ruler">Jorge Lopes</a>
	</li>
	<li><img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/BetaTester.gif") %>" /> 
		<a href="http://orionsbelt.zi-yu.com/userinfo.aspx?id=19" class="ruler">Tiago Sousa</a>
	</li>
	<li><img src="<%= Alnitak.OrionGlobals.getCommonImagePath("prizes/BetaTester.gif") %>" /> 
		<a href="http://orionsbelt.zi-yu.com/userinfo.aspx?id=121" class="ruler">S&eacute;rgio Santos</a>
	</li>
</ul>