<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="light" TagName="LoginControl" Src="../controls/LoginControl.ascx" %>
<%@ Register TagPrefix="notifier" TagName="BattleNotifier" Src="../controls/BattleNotifier.ascx" %>

<div id="rightMenu">
	<orion:RoleVisible showTo="ruler" runat="server">
		<notifier:BattleNotifier runat="server" />
	</orion:RoleVisible>
	<dl>
		<dt>Orion's Belt</dt>
		<dd><a href="<%= OrionGlobals.AppPath %>index.aspx"><lang:Label ref="section_index" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>prizes.aspx"><lang:Label ref="section_prizes" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>docs/default.aspx"><lang:Label id="docsLink" ref="section_docs" runat="server"/></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>wiki/default.aspx">Manual</a></dd>
		<dd class="menuBottom"/>
	</dl>
	<dl>
		<dt><lang:Label ref="section_tops" runat="server" /></dt>
		<dd><a href="<%= OrionGlobals.AppPath %>topalliances.aspx"><lang:Label ref="section_topalliances" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>toprulers.aspx"><lang:Label ref="section_toprulers" runat="server" /></a></dd>
		<orion:RoleVisible ShowTo="admin" runat="server">
			<dd><a href="<%= OrionGlobals.AppPath %>topplanets.aspx?option=TopPlanets"><lang:Label ref="section_topplanets" runat="server" /></a></dd>
			<dd><a href="<%= OrionGlobals.AppPath %>topplanets.aspx?option=TopRulers">Ruler Stats</a></dd>
		</orion:RoleVisible>
		<dd><a href="<%= OrionGlobals.AppPath %>topranks.aspx"><lang:Label ref="section_topranks" runat="server" /></a></dd>		
		<dd class="menuBottom"/>
	</dl>

	<dl>
		<dt><lang:Label ref="section_tournament" runat="server" /></dt>
		<dd><a class="menuItem" href="<%= OrionGlobals.AppPath %>tournament/default.aspx?t=regicide"><lang:Label ref="regicide" runat="server" /></a><orion:TournamentNotifier Type="regicide" runat="server" /></dd>
		<dd><a class="menuItem" href="<%= OrionGlobals.AppPath %>tournament/default.aspx?t=totalannihilation"><lang:Label ref="totalannihilation" runat="server" /></a><orion:TournamentNotifier Type="totalannihilation" runat="server" /></dd>
		<dd class="menuBottom"/>
	</dl>

	<dl>
		<dt><lang:Label ref="leftmenu_namedpages-header" runat="server" /></dt>
		<dd><a href="<%= OrionGlobals.AppPath %>about.aspx"><lang:Label ref="section_about" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>media.aspx"><lang:Label ref="section_media" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>shop/default.aspx"><lang:Label ref="section_shop" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>wiki/default.aspx/Orionsbelt.PerguntasFrequentes"><lang:Label id="faqLink" ref="section_faq" runat="server"/></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>contact.aspx"><lang:Label id="contactLink" ref="section_contact" runat="server"/></a></dd>
		<dd><a href="http://sourceforge.net/tracker/?group_id=99352">Bugs</a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>supports.aspx"><lang:Label ref="section_supports" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>stats.aspx"><lang:Label id="statsLink" ref="section_stats" runat="server"/></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>artwork.aspx"><lang:Label ref="section_artwork" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>forum/default.aspx"><lang:Label ref="section_forum" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>forum/default.aspx?g=members"><lang:Label ref="stats_registered" runat="server" /></a></dd>
		<dd><a href="<%= OrionGlobals.AppPath %>wiki/default.aspx/Orionsbelt.UserTags">Tags</a></dd>
		<dd class="menuBottom"/>
	</dl>

	<dl>
		<dt><lang:Label ref="section_shop" runat="server"/></dt>
		<dd><orion:ShopItemOfTheDay runat="server" /></dd>
		<dd class="menuBottom"/>
	</dl>
</div>

<dl id="historyItem">
	<dt><lang:Label ref="history" runat="server"/></dt>
	<dd><orion:RequestHistory runat="server"/></dd>
	<dd class="menuBottom"/>
</dl>
