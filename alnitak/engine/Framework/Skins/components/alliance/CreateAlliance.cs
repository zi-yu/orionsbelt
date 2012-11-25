// created on 3/20/2006 at 11:53 AM

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Core;
using Chronos.Alliances;

namespace Alnitak {
	
	public class CreateAlliance : PlanetControl {
	
		protected TextBox name = new TextBox();
		protected TextBox tag = new TextBox();
		protected TextBox motto = new TextBox();
		protected Button button = new Button();
		
		protected override void OnInit( EventArgs args )
		{
			base.OnInit(args);
			
			name.MaxLength = 50;
			tag.MaxLength = 5;
			motto.MaxLength = 250;
			motto.TextMode = TextBoxMode.MultiLine;
			button.Text = CultureModule.getContent("alliance_new");
			button.Click += new EventHandler(CreateAllianceClick);
			
			Controls.Add(name);
			Controls.Add(tag);
			Controls.Add(motto);
			Controls.Add(button);
		}
		
		protected override void Render( HtmlTextWriter writer )
		{
			writer.WriteLine("<p>{0}</p>", CultureModule.getContent("alliance_create") );
			
			writer.WriteLine("<table>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>{0}</td>", CultureModule.getContent("name"));
			writer.WriteLine("<td>");
			name.RenderControl(writer);
			writer.WriteLine("</td>");
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>{0}</td>", CultureModule.getContent("alliance_tag"));
			writer.WriteLine("<td>");
			tag.RenderControl(writer);
			writer.WriteLine("</td>");
			writer.WriteLine("</tr>");
			writer.WriteLine("<tr>");
			writer.WriteLine("<td>{0}</td>", CultureModule.getContent("alliance_motto"));
			writer.WriteLine("<td>");
			motto.RenderControl(writer);
			writer.WriteLine("</td>");
			writer.WriteLine("</tr>");
			writer.WriteLine("<td></td>");
			writer.WriteLine("<td>");
			button.RenderControl(writer);
			writer.WriteLine("</td>");
			
			writer.WriteLine("</table>");
		}
		
		protected void CreateAllianceClick( object src, EventArgs args )
		{
			AllianceInfo info = new AllianceInfo();
			info.Name = name.Text;
			info.Tag = tag.Text;
			info.Motto = motto.Text;
			info.Ranking = 1000;
			info.RankingBattles = 0;
			
			int id = AllianceUtility.Persistance.Register(info);
			
			User user = (User) Page.User;
			user.AllianceId = id;
			user.AllianceRank = AllianceMember.Role.Admiral;
			
			UserUtility.bd.saveUser(user, "");
			
			if( user.RulerId > 0 ) {
				Ruler ruler = Universe.instance.getRuler(user.RulerId);
				ruler.AllianceId = id;
				ruler.AllianceRank = AllianceMember.Role.Admiral;
			}
			
			Page.Response.Redirect(Page.Request.RawUrl, false);
		}
		
	};

}
