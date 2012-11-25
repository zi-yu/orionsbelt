// created on 29-12-2004 at 18:44

using System;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using Chronos.Battle;
using Chronos.Core;
using Chronos.Info;

namespace Alnitak {
	
	/// <summary>
	/// Documentation page
	/// </summary>
	public class UserInfo : Page {
	
		#region Instance Fields
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		private int id;
		
		#endregion
		
		#region Control Events
		
		/// <summary>Inicializa o controlo</summary>
		protected override void OnInit( EventArgs args )
		{
			base.OnInit(args);
			id = -1;
		}
		
		/// <summary>Builds up the control</summary>
		protected override void OnLoad( EventArgs e )
		{
			base.OnLoad(e);
			string rawId = Page.Request.QueryString["id"];
			if( OrionGlobals.isInt(rawId) ) {
				id = int.Parse(rawId);
			}
		}
		
		#endregion
		
		#region Control Rendering
		
		/// <summary>Pinta o controlo</summary>
		protected override void Render( HtmlTextWriter writer )
		{
			if( -1 == id ) {
				showError(writer);
				return;
			}
			
			User user = UserUtility.bd.getUser(id);
			if ( null == user ) {
				showError(writer);
				return;
			}
			
			showInfo(writer, user);
		}
		
		/// <summary>Mostra o menu que indica todas as categorias possíveis</summary>
		private void showError( HtmlTextWriter writer )
		{
			writer.WriteLine(info.getContent("invalid_request"));
			ExceptionLog.log("Invalid Request", Page.Request.RawUrl );
		}
		
		/// <summary>Mostra a documentação</summary>
		private void showInfo( HtmlTextWriter writer, User user )
		{
			showUserRole(writer, user);
		
			writer.WriteLine("<div class='planetInfoZoneTitle'>{0} - <b>{1}</b></div>",
					info.getContent("information"), user.Nick
				);

			string avatar = user.Avatar!=string.Empty?user.Avatar:Alnitak.User.DefaultAvatar;

			writer.WriteLine("<table cellpadding='0' cellspacing='0' class='planetFrame' >");
			writer.WriteLine("<tr><td>");
			writer.WriteLine("<table width='100%'>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource' width='50%'>{0}</td>", info.getContent("name"));
			writer.WriteLine("<td class='resourceCell' width='50%'>{0}</td>", OrionGlobals.getLink(user));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("online"));
			writer.WriteLine("<td class='resourceCell'>");
			if(  OrionGlobals.isUserOnline(user.UserId) ) {
				writer.WriteLine("<img src='{0}' />", OrionGlobals.getCommonImagePath("online.gif"));
			} else {
				writer.WriteLine("<img src='{0}' />", OrionGlobals.getCommonImagePath("offline.gif"));
			}
			writer.WriteLine("</td>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("profile_website"));
			writer.WriteLine("<td class='resourceCell'><a href='{0}'>{0}</a></td>", user.Website==string.Empty?"-":user.Website);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("section_contact"));
			writer.WriteLine("<td class='resourceCell'><a href='{0}forum/default.aspx?g=pmessage&u={1}'>{2}</a></td>", OrionGlobals.AppPath, user.UserId, info.getContent("pmsg_send"));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("profile_signature"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", user.Signature==string.Empty?"-":user.Signature);
			writer.WriteLine("</tr>");
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("profile_regist"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.FormatDateTime(user.RegistDate));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("profile_lastLogin"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.FormatDateTime(user.LastLogin));
			writer.WriteLine("</tr>");
			writer.WriteLine("</tr>");
			
			writer.WriteLine("</table>");

			writer.WriteLine("</td>");
			
			writer.WriteLine("<td class='resource' align='center'><fieldset><legend><b>{1}</b></legend><img class='avatar' src='{0}'/></fieldset></td></tr>",avatar,info.getContent("profile_avatarText"));

			writer.WriteLine("</table>");
			
			writer.WriteLine("<div class='planetInfoZoneTitle'>{0} - <b>{1}</b></div>",
					info.getContent("elo_battle_rank"), user.Nick
				);
			
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("score"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", user.EloRanking);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("elo_battle_rank"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", user.EloRankDescription);
			writer.WriteLine("</tr>");
			
			User current = Context.User as User;
			if( current != null ) {
				Ranking[] ranks = GetMatchResult(current, user, BattleResult.NumberOneVictory);
				int ifWinPoints = ranks[0].EloRanking - current.EloRanking; 
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("IfWin"));
				writer.WriteLine("<td class='resourceCell'>{0} (<span class='green'>+{1}</span>)</td>", ranks[0].EloRanking, ifWinPoints);
				writer.WriteLine("</tr>");
				
				ranks = GetMatchResult(current, user, BattleResult.NumberTwoVictory);
				int ifLoosePoints = ranks[0].EloRanking - current.EloRanking; 
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("IfLoose"));
				writer.WriteLine("<td class='resourceCell'>{0} (<span class='red'>{1}</span>)</td>", ranks[0].EloRanking, ifLoosePoints);
				writer.WriteLine("</tr>");
			}
			writer.WriteLine("</table>");
		
			if ( user.RulerId == -1 ) {
				return;
			}
			
			Ruler ruler = Universe.instance.getRuler(user.RulerId);
			
			writer.WriteLine("<div class='planetInfoZoneTitle'>{0} - <b>{1}</b></div>",
					info.getContent("information"), user.Nick
				);
			
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("battle_rank"));
			writer.WriteLine("<td class='resourceCell'><b>{0}</b>º</td>", (ruler.Rank==-1?"~":ruler.Rank.ToString()));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("stats_alliances"));
			string allianceText = null;
			if ( Universe.instance.isDefaultAlliance(ruler) || ruler.AllianceId < 0 ) {
				allianceText = "<i>"+info.getContent("no_alliance")+"</i>";
			} else {
				allianceText = AllianceUtility.Persistance.Get( ruler.AllianceId ).Name;
			}
			writer.WriteLine("<td class='resourceCell'>{0}</td>", allianceText  );
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("planetas"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Planets.Length);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("victories"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Victories);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("defeats"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Defeats);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("score"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.getResourceCount("score"));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
			writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("battle_rank"));
			writer.WriteLine("<td class='resourceCell'>{0}</td>", info.getContent(ruler.Ranking));
			writer.WriteLine("</tr>");

			if( current!= null ) {
				if( user.RulerId != current.RulerId ) {
					writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
					writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("battleAdminUser_friendly"));
					writer.WriteLine("<td class='resourceCell'><a href='ruler/battle/friendlybattle/default.aspx?user={0}'>{1}</a></td>",user.Nick, info.getContent("battle_create"));
					writer.WriteLine("</tr>");
				}
			
				if( current.IsInRole("admin") ) {
					writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
					writer.WriteLine("<td class='resource'>{0}</td>", info.getContent("batalhas"));
					writer.WriteLine("<td class='resourceCell'><a href='admin/battleadmin/default.aspx?id={0}'>{1}</a></td>", ruler.Id, info.getContent("battleAdminUser_view") );
					writer.WriteLine("</tr>");
				}
			}

			writer.WriteLine("</table>");
			
			if( ruler.Prizes == null || ruler.Prizes.Count == 0 ) {
				return;
			}
			
			writer.WriteLine("<div class='planetInfoZoneTitle'><b>{0}</b></div>",
					info.getContent("section_prizes")
				);

			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("Medal"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("prize"));
			writer.WriteLine("<td class='resourceTitle'>{0}</td>", info.getContent("turn_current"));
			writer.WriteLine("</tr>");
									
			foreach( Winner winner in ruler.Prizes ) {
				writer.WriteLine("<tr  onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resourceCell'><img src='{0}prizes/{1}{2}.gif' /></td>", OrionGlobals.getCommonImagePath(), OrionGlobals.getPrizeCategory(winner), winner.Medal.ToString() );
				writer.WriteLine("<td class='resource'>{0}</td>", info.getContent(winner.Prize));
				writer.WriteLine("<td class='resourceCell'>{0}</td>", winner.Turn);
				writer.WriteLine("</tr>");
			}

			writer.WriteLine("</table>");
		}
		
		/// <summary>Mostra informação de roles</summary>
		private void showUserRole( HtmlTextWriter writer, User user )
		{
			if( OrionGlobals.isAdmin(user) ) {
				writer.WriteLine("<div class='nav'>{0}</div>", string.Format(info.getContent("user_admin"), OrionGlobals.getLink(user)) );
				return;
			}
			if( OrionGlobals.isBetaTester(user) ) {
				writer.WriteLine("<div class='nav'>{0}</div>", string.Format(info.getContent("user_betatester"), OrionGlobals.getLink(user)) );
				return;
			}
			if( OrionGlobals.isArtist(user) ) {
				writer.WriteLine("<div class='nav'>{0}</div>", string.Format(info.getContent("user_artist"), OrionGlobals.getLink(user)) );
				return;
			}
		}
		
		private Ranking[] GetMatchResult( User current, User user, BattleResult result )
		{
			Ranking one = new Ranking();
			one.EloRanking = current.EloRanking;
			Ranking two = new Ranking();
			two.EloRanking = user.EloRanking;
			
			Ranking.Update(one, two, result);
			
			return new Ranking[] { one, two };
		}
				
		#endregion
		
	};
}
