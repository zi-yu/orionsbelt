<%@ Control Language="c#" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<%@ Register TagPrefix="battle" Namespace="Alnitak.Battle" Assembly="Alnitak" %>

<orion:CheckVacations Value="true" runat="server">
	<div class="red"><lang:Label ref="vacation_restriction" runat="server" /></div>
</orion:CheckVacations>

<orion:CheckVacations Value="false" runat="server">
<battle:CancelBattle runat="server" />

<orion:ReadyForBattle runat="server" />
<orion:CurrentBattles TitleRef="battleAdminUser_battle" runat="server" />

<orion:CurrentBattles TitleRef="battleAdminUser_friendly" BattleType="friendly" runat="server" />

<orion:CurrentBattles TitleRef="battleAdminUser_tournament" BattleType="tournament" runat="server" />
</orion:CheckVacations>