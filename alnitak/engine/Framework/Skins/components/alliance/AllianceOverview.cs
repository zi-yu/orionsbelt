// created on 3/20/2006 at 1:25 PM

using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Core;
using Chronos.Utils;
using Chronos.Alliances;

namespace Alnitak {
	
	public class AllianceOverview : PlanetControl {
	
		private AllianceInfo alliance = null;
		
		public AllianceInfo Current {
			get {
				if( alliance == null ) {
					string fromQueryString = Page.Request.QueryString["id"];
					int id = 0;
					if( fromQueryString != null ) {
						id = int.Parse(fromQueryString);
					} else {
						User user = (User) Page.User;
						id = Math.Abs(user.AllianceId);
					}
					alliance = AllianceUtility.Persistance.Get( id );
				}
				return alliance;
			}
			set { alliance = null; }
		}
		
		public bool SomeoneChecking {
			get {
				User user = Page.User as User;
				if( user == null ) {
					return true;
				} 
				return !Current.HasMember(user);
			}
		}
		
		protected override void OnInit( EventArgs args )
		{
			base.OnInit(args);
			CheckJoin();
			CheckAproove();
			CheckReject();
			CheckUpdate();
			WriteAprooveJs();
		}

		protected override void OnPreRender(EventArgs e) {
			if(  Current == null ) {
				Information.AddError( info.getContent("alliance_doesntExists") );
			}

			base.OnPreRender (e);
		}

		
		protected override void Render( HtmlTextWriter writer )
		{
			if( Current != null ) {
				RenderOverview(writer);
				RenderMembers(writer);
				if( SomeoneChecking ) {
					RenderWannaJoin(writer);
				} else {
					RenderWannabe(writer);
				}
			}
		}
		
		protected void RenderOverview( HtmlTextWriter writer )
		{
			writer.WriteLine("<h2>{0}</h2>", Current.Name);
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>{0}</td>", CultureModule.getContent("alliance_tag"));
			writer.WriteLine("<td>{0}</td>", Current.Tag);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>{0}</td>", CultureModule.getContent("section_topranks"));
			writer.WriteLine("<td>{0} (Avg: {1}) - {2}</td>", Current.Ranking, Current.AverageRanking, string.Format(CultureModule.getContent("battlesFought"), Current.RankingBattles));
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>{0}</td>", CultureModule.getContent("alliance_motto"));
			writer.WriteLine("<td>{0}</td>", Current.Motto);
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>{0}</td>", CultureModule.getContent("profile_regist"));
			writer.WriteLine("<td>{0}</td>", Current.RegistDate);
			writer.WriteLine("</tr>");
			writer.WriteLine("</table>");
		}
		
		protected void RenderMembers( HtmlTextWriter writer )
		{
			User watching = Page.User as User;
			bool member = false;
			bool canAdmin = false;
			if( watching != null ) {
				member = watching.AllianceId == Current.Id;
				canAdmin = member && watching.AllianceRank >= AllianceMember.Role.Admiral;
				writer.WriteLine("<!-- member:{0} watching.AllianceId:{1} Current.Id:{2}-->", member, watching.AllianceId, Current.Id );
				writer.WriteLine("<!-- canAdmin:{0} rank:{1} -->", canAdmin, watching.AllianceRank);
			}
				
			writer.WriteLine("<h2>{0} {1}</h2>", Current.Members.Count, CultureModule.getContent("alliance_members"));
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("section_ruler"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("alliance_role"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("section_topranks"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("score"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("section_toprulers"));
			if( canAdmin ) {
				writer.WriteLine("<th class='resourceTitle'></th>");
			}
			writer.WriteLine("</tr>");
		
			foreach( User user in Current.Members ) {
				
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.getLink(user));
				writer.WriteLine("<td class='resourceCell'>{0}</td>", user.AllianceRank);
				writer.WriteLine("<td class='resourceCell'>{0}</td>", user.EloRanking);
				if( user.RulerId > 0 ) {
					Ruler ruler = Universe.instance.getRuler(user.RulerId);
					writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Score);
					writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Rank);
				} else {
					writer.WriteLine("<td class='resourceCell'>-</td>");
					writer.WriteLine("<td class='resourceCell'>-</td>");
				}
				if( canAdmin ) {
					writer.WriteLine("<td class='resourceCell'>{0}</td>", GetSelect(user));
				}
				writer.WriteLine("</tr>");
			}
		
			writer.WriteLine("</table>");
		}
		
		protected void RenderWannabe( HtmlTextWriter writer )
		{
			User watching = Page.User as User;
			if( watching.AllianceId != Current.Id ) {
				return;
			}
		
			writer.WriteLine("<h2>{0} {1}</h2>", Current.Wannabe.Count, CultureModule.getContent("alliance_wannabes"));
			if( Current.Wannabe.Count == 0 ) {
				writer.WriteLine("<p>{0}</p>", CultureModule.getContent("noneAvailable"));
				return;
			}
			
			writer.WriteLine("<table class='planetFrame'>");
			writer.WriteLine("<tr class='resourceTitle'>");
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("section_ruler"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("section_topranks"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("score"));
			writer.WriteLine("<th class='resourceTitle'>{0}</th>", CultureModule.getContent("section_toprulers"));
			if( watching.AllianceRank >= AllianceMember.Role.ViceAdmiral ) {
				writer.WriteLine("<th class='resourceTitle'></th>");
				writer.WriteLine("<th class='resourceTitle'></th>");
			}
			writer.WriteLine("</tr>");
		
			foreach( User user in Current.Wannabe ) {
				
				writer.WriteLine("<tr onmouseover='overResource(this);' onmouseout='outResource(this);'>");
				writer.WriteLine("<td class='resourceCell'>{0}</td>", OrionGlobals.getLink(user));
				writer.WriteLine("<td class='resourceCell'>{0}</td>", user.EloRanking);
				if( user.RulerId > 0 ) {
					Ruler ruler = Universe.instance.getRuler(user.RulerId);
					writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Score);
					writer.WriteLine("<td class='resourceCell'>{0}</td>", ruler.Rank);
				} else {
					writer.WriteLine("<td class='resourceCell'>-</td>");
					writer.WriteLine("<td class='resourceCell'>-</td>");
				}
				if( watching.AllianceRank >= AllianceMember.Role.ViceAdmiral ) {
					writer.WriteLine("<td class='resourceCell'><a href='javascript:aproove({0})'><img src='{1}' /></a></td>", user.UserId, OrionGlobals.getCommonImagePath("ok.gif"));
					writer.WriteLine("<td class='resourceCell'><a href='javascript:reject({0})'><img src='{1}' /></a></td>", user.UserId, OrionGlobals.getCommonImagePath("remove.gif"));
				}
				writer.WriteLine("</tr>");
			}
		
			writer.WriteLine("</table>");
		}
		
		protected void RenderWannaJoin( HtmlTextWriter writer )
		{
			if(!( Page.User is User) ) {
				return;
			}
		
			writer.WriteLine("<h2>{0}</h2>", CultureModule.getContent("alliance_join"));
			if( Current.HasWannabe( Page.User as User ) ) {
				writer.WriteLine("<p>{0}</p>", CultureModule.getContent("alliance_join_waiting"));
			} else {
				writer.WriteLine("<p><a href='{0}&join=1'>{1}</a></p>", Page.Request.RawUrl,
							CultureModule.getContent("alliance_join_try")
					);
			}
		}
		
		private string GetSelect( User user )
		{
			StringWriter writer = new StringWriter();
			
			writer.Write("<select name='updateUser{0}' id='updateUser{0}'>", user.UserId );
			writer.Write("<option >Remove</option>", user.UserId );
			writer.Write("<option {1}>Private</option>", user.UserId, user.AllianceRank == AllianceMember.Role.Private? "selected" : "" );
			writer.Write("<option {1}>Corporal</option>", user.UserId, user.AllianceRank == AllianceMember.Role.Corporal? "selected" : "" );
			writer.Write("<option {1}>ViceAdmiral</option>", user.UserId, user.AllianceRank == AllianceMember.Role.ViceAdmiral? "selected" : "" );
			writer.Write("<option {1}>Admiral</option>", user.UserId, user.AllianceRank == AllianceMember.Role.Admiral? "selected" : "" );
			writer.Write("</select> ");
			writer.Write("<input value='Update' type='button' onclick='update({0})' />", user.UserId);
			
			return writer.ToString();
		}
		
		private void CheckJoin()
		{
			if( Page.Request.QueryString["join"] == null ) {
				return;
			}
			User user = Page.User as User;
			if( !Current.HasMember(user) && !Current.HasWannabe(user) ) {
				Current.Wannabe.Add(user);
				user.AllianceId = -Current.Id;
				UserUtility.bd.saveUser(user, "");
			}
		}
		
		private void CheckAproove()
		{
			string aproove = Page.Request.Form["aproove"];
			if( aproove == null || aproove == "" ) {
				return;
			}
		
			User user = (User) Page.User;
			if( user.AllianceId != Current.Id ) {
				return;
			}
			if( (int) user.AllianceRank < (int)AllianceMember.Role.ViceAdmiral ) {
				return;
			}
			
			User wannabe = UserUtility.bd.getUser( int.Parse(aproove) );
			
			if( Current.HasWannabe(wannabe) ) {
				Current.RemoveWannabe(wannabe.UserId);
				Current.Members.Add(wannabe);
				wannabe.AllianceId = Current.Id;
				wannabe.AllianceRank = AllianceMember.Role.Private;
				UserUtility.bd.saveUser(wannabe,"");
			}
		}
		
		private void CheckUpdate()
		{
			if( !Page.IsPostBack ) {
				return;
			}
			
			string id = Page.Request.Form["toUpdate"];
			string val = Page.Request.Form["newValue"];
			
			if( id == null || val == null || id.Length == 0 || val.Length == 0 ) {
				return;
			}
			
			User user = (User) Page.User;
			if( user.AllianceId != Current.Id ) {
				return;
			}
			if( user.AllianceRank < AllianceMember.Role.Admiral ) {
				return;
			}
			
			User wannabe = UserUtility.bd.getUser( int.Parse(id) );
			
			if( val == "Remove" ) {
				Current.RemoveMember(wannabe.UserId);
				wannabe.AllianceId = 0;
				UserUtility.bd.saveUser(wannabe,"");
				if( wannabe.UserId == user.UserId ) {
					user.AllianceId = 0;
				}
				return;
			}
			
			AllianceMember.Role role = (AllianceMember.Role)Enum.Parse( typeof(AllianceMember.Role), val);
			wannabe.AllianceRank = role;
			UserUtility.bd.saveUser(wannabe,"");
			Current = null;
			if( wannabe.UserId == user.UserId ) {
				user.AllianceRank = role;
			}
		}
		
		private void CheckReject()
		{
			string reject = Page.Request.Form["reject"];
			if( reject == null || reject == "" ) {
				return;
			}
		
			User user = (User) Page.User;
			if( user.AllianceId != Current.Id ) {
				return;
			}
			if( (int) user.AllianceRank < (int)AllianceMember.Role.ViceAdmiral ) {
				return;
			}
			
			User wannabe = UserUtility.bd.getUser( int.Parse(reject) );
			
			if( Current.HasWannabe(wannabe) ) {
				Current.RemoveWannabe(wannabe.UserId);
				wannabe.AllianceId = 0;
				UserUtility.bd.saveUser(wannabe,"");
			}
		}
		
		private void WriteAprooveJs()
		{
			string script = @"<script type='text/javascript'>
	var theform = document.pageContent;
	
	function aproove( id )
	{
		theform.aproove.value = id;
	    theform.submit();
	}
	
	function reject( id )
	{
		theform.reject.value = id;
	    theform.submit();
	}
	
	function update( id )
	{
		var s = document.getElementById('updateUser'+id);
		theform.toUpdate.value = id;
		theform.newValue.value = s.value;
		theform.submit();
	}
	
</script>";
			
			Page.RegisterClientScriptBlock("aproove,reject", script);
			Page.RegisterHiddenField("aproove", "");
			Page.RegisterHiddenField("reject", "");
			Page.RegisterHiddenField("toUpdate", "");
			Page.RegisterHiddenField("newValue", "");
		}
		
	};

}

