// created on 12/25/2005 at 8:34 PM

using System;
using System.Collections;
using Chronos.Battle;

namespace Alnitak {

	[Serializable]
	public class Ranking {
	
		#region Private Fields
		
		private int rank = -1;
		
		#endregion
		
		#region Properties
		
		public  int EloRanking { 
			get { return rank; } 
			set { rank = value; } 
		}
		
		public double EloRankingModifier {
			get {
				int rank = EloRanking;
				return Math.Abs( (rank / 100) - 30);
			}
		}
		
		public RankDescription EloRankDescription {
			get {
				if( EloRanking >= (int) RankDescription.Grandmaster ) {
					return RankDescription.Grandmaster;
				}
				
				if( EloRanking >= (int) RankDescription.Master ) {
					return RankDescription.Master;
				}
				
				if( EloRanking >= (int) RankDescription.Elite ) {
					return RankDescription.Elite;
				}
			
				if( EloRanking >= (int) RankDescription.Guru ) {
					return RankDescription.Guru;
				}
				
				if( EloRanking >= (int) RankDescription.Expert ) {
					return RankDescription.Expert;
				}
				
				if( EloRanking >= (int) RankDescription.Veteran ) {
					return RankDescription.Veteran;
				}
				
				if( EloRanking >= (int) RankDescription.Advanced ) {
					return RankDescription.Advanced;
				}
				
				if( EloRanking >= (int) RankDescription.Intermediate ) {
					return RankDescription.Intermediate;
				}
				
				if( EloRanking >= (int) RankDescription.Novice ) {
					return RankDescription.Novice;
				}
				
				if( EloRanking >= (int) RankDescription.Rookie ) {
					return RankDescription.Rookie;
				}
				
				if( EloRanking >= (int) RankDescription.ChickenChaser ) {
					return RankDescription.ChickenChaser;
				}
				
				return RankDescription.ChickenChaser;
			}
		}
		
		#endregion
		
		#region Static Methods
		
		public static void Update( AllianceInfo one, AllianceInfo two, BattleResult result )
		{
			Ranking r1 = new Ranking();
			r1.EloRanking = one.Ranking;
			
			Ranking r2 = new Ranking();
			r2.EloRanking = two.Ranking;
			
			Update( r1, r2, result );
			
			one.Ranking = r1.EloRanking;
			two.Ranking = r2.EloRanking;
		}
		
		public static void Update( Ranking one, Ranking two, BattleResult result )
		{
			double expectedOne = 0.5;
			double expectedTwo = 0.5;
			
			if( result != BattleResult.Draw ) {
				expectedOne = (result == BattleResult.NumberOneVictory) ? 1 : 0;
				expectedTwo = (result == BattleResult.NumberTwoVictory) ? 1 : 0;
			}
			
			double realOne = RealScore(one, two);
			double realTwo = RealScore(two, one);
			
			double oneDistance = (one.EloRanking - two.EloRanking) / 200 * expectedOne;
			double twoDistance = (two.EloRanking - one.EloRanking) / 200 * expectedTwo;
			
			one.EloRanking = FinalScore(one, realOne, expectedOne, oneDistance);
			two.EloRanking = FinalScore(two, realTwo, expectedTwo, twoDistance);
		}
		
		public static double RealScore( Ranking asker, Ranking other )
		{
			int exp = (int)Math.Round((other.EloRanking - asker.EloRanking) / 400.0);
			double factor = 1 + System.Math.Pow(10, exp);
			
			factor = 1.0 / factor;		
			return factor;
		}
		
		public static int FinalScore( Ranking rank, double real, double expected, double distance )
		{
			if( distance < 0 ) {
				distance *= -1;
				distance += 1;
			} else {
				distance = 1;
			}
		
			double dif = expected - real;
			//Chronos.Utils.Log.log("*{3}* Dif: {0}={1} - {2}", dif, expected, real, rank.EloRanking );
			double withMod = rank.EloRankingModifier * distance * dif;
			//Chronos.Utils.Log.log("*{3}* withMod: {0}={1} - {2}", withMod, rank.EloRankingModifier, dif, rank.EloRanking );
			
			return (int) Math.Round(rank.EloRanking + withMod);
		} 
		
		#endregion
		
	};

}