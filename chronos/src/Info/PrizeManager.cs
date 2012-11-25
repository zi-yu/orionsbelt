// created on 20-01-2005 at 14:20

using System;
using System.Collections;
using Chronos.Core;
using Chronos.Messaging;
using Chronos.Utils;

namespace Chronos.Info {

	/// <summary>Indica os vencedores de um prémio</summary>
	[Serializable]
	public class PrizeManager {
	
		#region Instance Fields
	
		private Hashtable winners = new Hashtable();
		private string prize;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Constrói o vencedor</summary>
		public PrizeManager(string prize)
		{
			this.prize = prize;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica quem ganhou o prémio de ouro</summary>
		public Winner Gold {
			get { return (Winner) winners[Medal.Gold]; }
		}
		
		/// <summary>Indica quem ganhou o prémio de prata</summary>
		public Winner Silver {
			get { return (Winner) winners[Medal.Silver]; }
		}
		
		/// <summary>Indica quem ganhou o prémio de bronze</summary>
		public Winner Bronze {
			get { return (Winner) winners[Medal.Bronze]; }
		}
		
		/// <summary>Indica o turno em que ocorreu o último prémio</summary>
		public Winner Last {
			get { return (Winner) winners[Medal.Other]; }
		}
		
		#endregion
		
		#region Instance Methods
		
		/// <summary>Regista um novo utilizador</summary>
		public bool register( PrizeCategory category, int turn, Ruler ruler )
		{
			Winner winner = null;
		
			if( !winners.Contains(Medal.Gold) ) {
				return register(category, turn, ruler, prize, Medal.Gold);
			}
			if( !winners.Contains(Medal.Silver) ) {
				winner = (Winner) winners[Medal.Gold];
				return register(category, turn, ruler, prize, Medal.Silver);
			}
			if( !winners.Contains(Medal.Bronze) ) {
				winner = (Winner) winners[Medal.Silver];
				return register(category, turn, ruler, prize, Medal.Bronze);
			}
			
			return false;
		}
		
		/// <summary>Regista um prémio</summary>
		private bool register(PrizeCategory category, int turn, Ruler ruler, string prize, Medal medal )
		{	
			if( alreadyWinner(ruler, prize) ) {
				return false;
			}

			int points = turn;
			if( medal != Medal.Gold ) {
				points = Gold.Turn;
			}
	
			Winner winner = new Winner(category, turn, ruler, prize, medal);
			ruler.addPrize(winner);
			ruler.addResource("Intrinsic", "score", points);
			Messenger.Send(ruler, "RulerPrize", prize, points.ToString(), medal.ToString());
			winners[medal] = winner;
			
			return true;
		}
		
		 /// <summary>Indica se determinado ruler já ganhou algum prémio</summary>
		 public static bool alreadyWinner( Ruler ruler, string prize )
		 {
		 	foreach( Winner winner in ruler.Prizes ) {
		 		if( winner.Prize == prize ) {
		 			return true;
		 		}
		 	}
		 	return false;
		 }
		
		#endregion
	};
}
