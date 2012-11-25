// created on 9/2/2005 at 10:44 AM

#define DEBUG_CHAMP_GROUPS
//#define DEBUG_ELO_RANKING
#define DEBUG_FIRST_PLAYOFFS

using Chronos.Battle;
using Chronos.Core;
using Chronos.Tournaments;
using Chronos.Utils;
using NUnit.Framework;

namespace Chronos.Tests {

	[TestFixture]
	public class TournamentTester {
		
		#region Instance Fields
		
		private Ruler ruler;
		private Tournament tournament;
		const int nRulers = 122;
		
		#endregion
		
		#region Set Up Methods
		
		[SetUp]
		public void prepare()
		{
			ruler = Universe.instance.CreateRuler("buu", "bii");
			Fleet fleet = new Fleet("TournamentFleet", Coordinate.First, null, false);
			tournament = new Tournament("totalannihilation", fleet);
		}
		
		#endregion
		
		#region Subscription Tests
		
		[Test]
		public void TestInitialState()
		{
			Assert.IsTrue( tournament.AllowRegistering );
			Assert.AreEqual( tournament.State, TournamentState.Subscriptions );
		}
		
		[Test]
		public void TestRegister()
		{
			tournament.Register(ruler);
			
			Assert.AreEqual(1, tournament.Registered.Count );
			
			Assert.IsFalse( tournament.Register(ruler), "no duplicate rulers allowed" );
			Assert.AreEqual(1, tournament.Registered.Count );
		}

		#endregion
		
		#region Championship Tests
		
		[Test]
		public void TestChampionshipInitialState()
		{
			ToChampionship();
			
			Assert.IsFalse( tournament.AllowRegistering );
			Assert.AreEqual( tournament.State, TournamentState.Championship );
		}
		
		[Test]
		public void TestChampionshipRulersPerGroupGenerator()
		{
			ToChampionship();
			
			Championship champ = (Championship) tournament.CurrentPhase;

			// <n rulers> <grupos gerados>
			CheckRulersPerGroup( champ, 4, 1 );
			CheckRulersPerGroup( champ, 6, 1 );
			CheckRulersPerGroup( champ, 8, 1 );
			CheckRulersPerGroup( champ, 10, 1 );
			CheckRulersPerGroup( champ, 11, 2 );
			CheckRulersPerGroup( champ, 15, 2 );
			CheckRulersPerGroup( champ, 20, 2 );
			CheckRulersPerGroup( champ, 21, 3 );
			CheckRulersPerGroup( champ, 27, 3 );
			CheckRulersPerGroup( champ, 30, 3 );
			CheckRulersPerGroup( champ, 55, 6);
			CheckRulersPerGroup( champ, 300, 30);
		}
		
		[Test]
		public void TestNumberOfGroups()
		{
			ToChampionship();			
			Championship champ = (Championship) tournament.CurrentPhase;

			Assert.AreEqual( champ.Groups.Length, Championship.GetNumberOfGroups(nRulers) );
			
			int total = 0;
			foreach( Group group in champ.Groups ) {
				total += group.Registered.Count;
			}
			Assert.AreEqual(nRulers, total, "Number of Rulers");
			
			foreach( Group group in champ.Groups ) {
				foreach( Classification c in group.Registered ) {
					Ruler ruler = c.Player;
					int matches = 0;
					foreach( Match match in group.Matches.Values ) {
						if( match.Participates(ruler) ) {
							++matches;
						}
					}
					Assert.AreEqual( group.Registered.Count -1, matches, "Ruler should have more/lesse matches" );
				}
			}
			
#if DEBUG_CHAMP_GROUPS
			Log.log("--- DEBUG_CHAMP_GROUPS ---");
			foreach( Group group in champ.Groups ) {
				Log.log("--- Group {0} ---", group.Id );
				
				Log.log("--- {0} Players", group.Registered.Count);
				foreach( Classification c in group.Registered ) {
					Log.log("- {0}", c.Player.Name);
				}
				
				/*Log.log("--- {0} Matches", group.Matches.Count);
				foreach( Match match in group.Matches.Values ) {
					Log.log(match);
				}*/
			}
#endif
		}
		
		[Test]
		public void TestNextPow()
		{
			Assert.AreEqual( 16, Championship.NextPow(15) );
			Assert.AreEqual( 16, Championship.NextPow(16) );
			Assert.AreEqual( 32, Championship.NextPow(17) );
		}

		#endregion
		
		#region Playoffs Tests
		
		[Test]
		public void TestPlayoffsInitialState()
		{
			ToChampionship();

#if DEBUG_FIRST_PLAYOFFS
			Championship champ = (Championship) tournament.CurrentPhase;
			Log.log("----- Participants -------");
			foreach( Ruler ruler in champ.GetWinners() ) {
				Log.log(ruler);
			}
#endif

			int players = ToPlayoffs();
			
			Assert.IsFalse( tournament.AllowRegistering );
			Assert.AreEqual( tournament.State, TournamentState.Playoffs );
			Assert.AreEqual( Championship.NextPow(players), tournament.CurrentPhase.Participants );
			
#if DEBUG_FIRST_PLAYOFFS
			Playoffs play = (Playoffs) tournament.CurrentPhase;
			Log.log("----- Participants -------");
			foreach( Ruler ruler in play.Registered ) {
				Log.log(ruler);
			}
			Log.log("----- Playoffs -------");
			foreach( Match match in play.Matches ) {
				Log.log("{0} vs {1}", match.NumberOne, match.NumberTwo );
			}
#endif
		}

		#endregion
		
		#region Rankings
		
		[Test]
		public void TestEloRankings()
		{
			CheckRanking( 1000, 1000, BattleResult.Draw );
			CheckRanking( 1000, 1000, BattleResult.NumberOneVictory );
			CheckRanking( 1613, 1609, BattleResult.NumberOneVictory );
			CheckRanking( 1200, 1000, BattleResult.NumberOneVictory );
			CheckRanking( 1500, 2000, BattleResult.NumberOneVictory );
			CheckRanking( 1500, 2000, BattleResult.NumberTwoVictory );
			CheckRanking( 1500, 2000, BattleResult.Draw );
			CheckRanking( 1500, 6000, BattleResult.NumberOneVictory );
			CheckRanking( 2600, 2399, BattleResult.NumberOneVictory );
			CheckRanking( 2600, 1000, BattleResult.NumberTwoVictory );
			CheckRanking( 2600, 2600, BattleResult.NumberOneVictory );
		}
		
		private void CheckRanking( int score1, int score2, BattleResult result )
		{
#if DEBUG_ELO_RANKING
			Alnitak.Ranking one = new Alnitak.Ranking();
			one.EloRanking = score1;
			
			Alnitak.Ranking two = new Alnitak.Ranking();
			two.EloRanking = score2;
			
			Alnitak.Ranking.Update(one, two, result);
#endif
			
		}
		
		#endregion
		
		#region Utilities
		
		private void ToChampionship()
		{
			for( int i = 0; i < nRulers; ++i ) {
				Ruler ruler = Universe.instance.CreateRuler("Ruler " + (i+1), "asdasdasd");
				ruler.ForeignId = 1000 - i;
				tournament.Register( ruler );
			}
			tournament.EndSubscriptions();
		}
		
		private int ToPlayoffs()
		{
			Championship champ = (Championship)tournament.CurrentPhase;
			int basePlayers = champ.Groups.Length * 3;
			foreach( Group group in champ.Groups ) {
				foreach( Classification c in group.Registered ) {
					c.Points = c.Player.ForeignId;
				}
				group.Registered.Sort();
			}
			tournament.EndChampionship();
			return basePlayers;
		}
		
		private void CheckRulersPerGroup( Championship tournament, int nRulers, int nGroups )
		{
			Assert.AreEqual( nGroups, Championship.GetNumberOfGroups(nRulers), "We should have " + nGroups + " for " + nRulers + " rulers" );
		}
		
		#endregion
	
	};
}