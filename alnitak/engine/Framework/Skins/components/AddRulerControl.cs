using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Language;
using Chronos.Core;
using Alnitak.Mail;

namespace Alnitak {

	public class AddRulerControl : UserControl, INamingContainer {
	
	protected System.Web.UI.WebControls.Label insertPlanetName;
	protected System.Web.UI.WebControls.TextBox planet;
	protected Alnitak.OnlyTextValidator onlyTextValidator;
	protected System.Web.UI.WebControls.RequiredFieldValidator requiredFieldValidator;
	protected Alnitak.CharCountValidator charCountValidator;
	
	protected System.Web.UI.WebControls.Button registerRuler;
		
		/// <summary>Inicializa o controlo</summary>
		protected override void OnInit( EventArgs args )
		{
			base.OnInit(args);
			buildControlTree();
		}
		
		/// <summary>Constrói todo o conteúdo do controlo</summary>
		private void buildControlTree()
		{
			ILanguageInfo info = CultureModule.getLanguage();
			
			User user = (User) Page.User;
			if( user.IsInRole("ruler") ) {
				Information.AddError( info.getContent("addruler_already-ruler") );
				hide();
				return;
			}
			
			int count = int.Parse(OrionGlobals.getConfigurationValue( "alnitak","userCount" ));
			
			if( Universe.instance.canAddRuler() && Universe.instance.rulers.Count < count ) {
				string status = info.getContent("addruler_status-goodnews");
				string m = string.Format(info.getContent("addruler_intro"), status);
				Information.AddInformation( m );
			} else {
				string status = info.getContent("addruler_status-badnews");
				string m = string.Format(info.getContent("addruler_intro"), status);
				Information.AddInformation( m );
				hide();
				return;
			}

			EventHandler click = new EventHandler(this.onClick);
			
			insertPlanetName.Text = info.getContent("addruler_home-planet");
			planet.TextChanged += click; 
			
			registerRuler.Text = info.getContent("register");
			registerRuler.Click += click; 

			onlyTextValidator.ErrorMessage = info.getContent("addruler_planet-only-text") + "<p/>";
			requiredFieldValidator.ErrorMessage = info.getContent("addruler_planet-required") + "<p/>";
			charCountValidator.ErrorMessage = info.getContent("addruler_planet-tolong");
			
		}
		
		/// <summary>regista um user</summary>
		private void onClick( object sender, EventArgs args )
		{
			int count = int.Parse(OrionGlobals.getConfigurationValue( "alnitak","userCount" ));
			if( Universe.instance.rulers.Count < count ) {
				User user = (User) Page.User;
				Page.Validate();
				if( Page.IsValid ) {
					int id = Universe.instance.addRulerToUniverse(user.Nick,planet.Text);
					user.RulerId = id;
					Ruler ruler = Universe.instance.getRuler(id);
					ruler.ForeignId = user.UserId;
					ruler.AllianceId = user.AllianceId;
					ruler.AllianceRank = user.AllianceRank;
					UserUtility.bd.saveUser(user,"");
					Page.Application["LatestRuler"] = user;
					Chronos.Utils.Log.log("Registado o "+user.Mail +" com o planeta " + planet.Text);
					Page.Response.Redirect( OrionGlobals.calculatePath("ruler/default.aspx") );
				}
			}
		}

		private void hide() 
		{
			planet.Visible=false;
			registerRuler.Visible=false;
		}
	};

}