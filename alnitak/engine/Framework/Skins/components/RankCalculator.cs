// created on 12/27/2005 at 7:48 PM

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chronos.Core;
using Chronos.Messaging;
using Chronos.Battle;

namespace Alnitak {

	public class RankCalculator : UserControl {
	
		#region Instance Fields
		
		protected Language.ILanguageInfo info = CultureModule.getLanguage();
		protected TextBox rank1;
		protected TextBox rank2;
		protected Label rank1Label;
		protected Label rank2Label;
		protected Label userRank;
		protected DropDownList resultDDL;
		
		#endregion	
		
		#region Events
		
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			if( Page.IsPostBack ) {
				return;
			}
			if( Page.User is User) {
				User user = (User)Page.User;
				rank1.Text = user.EloRanking.ToString();
				userRank.Text = OrionGlobals.getLink(user) + ": " + user.EloRanking.ToString();
			}
			
			resultDDL.Items.Add( "NumberOneVictory" );
			resultDDL.Items.Add( "NumberTwoVictory" );
			resultDDL.Items.Add( "Draw" );
		}
		
		protected void Calculate( object sender, EventArgs args )
		{
			try{
				BattleResult result = GetResult();
				
				Ranking one = new Ranking();
				one.EloRanking = int.Parse( rank1.Text );
				
				Ranking two = new Ranking();
				two.EloRanking = int.Parse( rank2.Text );
				
				Ranking.Update(one, two, result);
				
				rank1Label.Text = one.EloRanking.ToString();
				rank2Label.Text = two.EloRanking.ToString();
				
			} catch( Exception ex)  {
				userRank.Text ="Error <!-- " + ex.ToString() + "-->";
			}
		}
		
		#endregion
		
		#region Utilities
		
		private BattleResult GetResult()
		{
			string str = resultDDL.SelectedValue;
			switch(str) {
				case "NumberOneVictory": return BattleResult.NumberOneVictory;
				case "NumberTwoVictory": return BattleResult.NumberTwoVictory;
				case "Draw": return BattleResult.Draw;
			} 
			return BattleResult.Draw;
		}
		
		#endregion	
	
	};
}
