using System;
using System.Collections;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Interfaces;
using Chronos.Messaging;
using DesignPatterns;

namespace Chronos.Battle {
	[Serializable]
	public class BattleInfo : IEndBattle {
		#region Static Fields

		public static int ROWCOUNT = 8;
		public static int COLUMNCOUNT = 8;

		#endregion

		#region Fields

		private BattleType _type;

		private RulerBattleInfo ruler1;
		private RulerBattleInfo ruler2;

		private EndBattleBase _endBattle;

		private int _currentRulerId;

		private int _battleId;

		private string _terrainType = null;

		private static FactoryContainer endBattleFactory = new FactoryContainer( typeof ( EndBattleFactory ) );

		#endregion

		#region Private Static 
		
		private static int CalculateWinnerScore( int destroyedScore, int opponentRank ) {
			return ((destroyedScore*2)/opponentRank);
		}

		private static int CalculateLoserScore( int lostScore, int opponentRank ) {
			return CalculateWinnerScore(lostScore, opponentRank);
		}
		
		#endregion

		#region Public Static 


		public static int GetWonScore( RulerBattleInfo rulerInfo, RulerBattleInfo enemyInfo, Ruler enemy ) {
			int score = 500 + CalculateWinnerScore(rulerInfo.PartialScore,enemy.Rank);
			if( score < 0 ) {
				score = 0;
			}
			return score>5000?5000:score;
		}

		public static int GetLostScore( RulerBattleInfo enemyInfo, Ruler ruler, Ruler enemy ) {
			int score = - 100 - CalculateLoserScore(enemyInfo.PartialScore,enemy.Rank);
			score = score<-5000?-5000:score;
			if( ruler.Score + score < 0 ) {
				return -ruler.Score;
			}
			return score;
		}


		/// <summary>Indica quantos pontos se ganha numa batalha</summary>
		[Obsolete]
		public static int getWinnerScore( Ruler looser ) {
			return ( looser.NumberOfMoves - 2 )*100;
		}

		/// <summary>Indica quantos pontos se ganha numa batalha</summary>
		[Obsolete]
		public static int getLooserScore( Ruler looser ) {
			return -( looser.NumberOfMoves - 2 )*20;
		}

		#endregion

		#region Constructor

		public BattleInfo( Ruler player1, Ruler player2, int battleId, BattleType type, string battleType ) {
			_battleId = battleId;
			_currentRulerId = player1.Id;

			RBI1 = new RulerBattleInfo( player1.Id, battleId, type, battleType );
			RBI2 = new RulerBattleInfo( player2.Id, battleId, type, battleType );

			_type = type;

			SetFieldType( );

			_endBattle = endBattleFactory.create(battleType,this) as EndBattleBase;
		}

		#endregion

		#region Properties

		public int CurrentRulerId {
			get { return _currentRulerId; }
		}

		public int BattleId {
			get { return _battleId; }
		}

		public BattleType BattleType {
			get { return _type; }
		}

		public string Terrain {
			get { return _terrainType; }
		}

		public RulerBattleInfo RBI1 {
			get { return ruler1; }
			set { ruler1 = value; }
		}

		public RulerBattleInfo RBI2 {
			get { return ruler2; }
			set { ruler2 = value; }
		}

		public EndBattleBase EndBattleBase {
			get { return _endBattle; }
		}

		#endregion

		#region Static Properties

		public static ICollection EndBattleTypes {
			get { return endBattleFactory.Keys; }
		}

		#endregion

		#region Private

		private string GetFieldFromIBattle( int rulerId ) {
			IBattle ibattle = Universe.instance.GetIBattle( rulerId, BattleId, BattleType );
			if ( ibattle is Planet ) {
				Planet p = ibattle as Planet;
				return p.Info.Terrain.Description;
			}
			return null;
		}

		private void SetFieldType( ) {
			string type = GetFieldFromIBattle( RBI1.OwnerId );
			if ( type != null ) {
				_terrainType = type;
				return;
			}

			type = GetFieldFromIBattle( RBI2.OwnerId );
			if ( type != null ) {
				_terrainType = type;
				return;
			}

			_terrainType = Core.Terrain.Random.Description;
		}

		private bool CheckMoves( string moves ) {
			string[] s = moves.Split( ';' );
			foreach ( string move in s ) {
				if ( move == string.Empty ) {
					continue;
				}

				string[] m = move.Split( ':' );
				if ( m[ 0 ] != "move" ) {
					return false;
				}
			}
			return true;
		}

		#endregion

		#region Public

		/// <summary>
		/// Faz os movimentos passados como parmetro
		/// </summary>
		/// <param name="moves">movimentos</param>
		/// /// <param name="currentRuler">Ruler corrente</param>
		public Result MakePositioning( string moves, Ruler currentRuler ) {
			Result result = null;
			if ( CheckMoves( moves ) ) {
				_currentRulerId = currentRuler.Id;
				Interpreter interpreter = new Interpreter( currentRuler, this );
				result = interpreter.Interpretate( moves );
			} else {
				result = new Result( );
				result.failed( new InvalidMove() );
			}

			if( result.Ok ) {
				RulerBattleInfo rbi = GetRulerBattleInfo(currentRuler);
				if( rbi.InitialContainer.Count == 0 ) {
					currentRuler.GetBattle(BattleId,BattleType).IsPositionTime = false;	
				}

				Universe.instance.SaveBattle( this, Universe.instance.GetBattleTurn(rbi.BattleId) );
			}

			return result;
		}

		public Result MakeTurn( string moves, Ruler currentRuler ) {
			_currentRulerId = currentRuler.Id;
			Interpreter interpreter = new Interpreter( currentRuler, this );
			Result result = interpreter.Interpretate( moves );
			if( result.Ok ) {
				SimpleBattleInfo sbInfo = currentRuler.GetBattle(BattleId,BattleType);
				sbInfo.IsTurn = false;
				sbInfo.ResetTurn();
				
				Universe.instance.SaveBattle( this, GetEnemyBattleInfo(currentRuler).OwnerId );
			}
			return result;
		}

		/// <summary>
		/// Obtm o RulerBattleInfo de determinado utilizador
		/// </summary>
		/// <param name="ruler">objecto que representa o ruler</param>
		/// <returns>RulerBattleIndo correspondente</returns>
		public RulerBattleInfo GetRulerBattleInfo( Ruler ruler ) {
			if ( ruler.Id == RBI1.OwnerId ) {
				return RBI1;
			}

			return RBI2;
		}

		/// <summary>
		/// Obtm o RulerBattleInfo de determinado utilizador
		/// </summary>
		/// <param name="ruler">objecto que representa o ruler</param>
		/// <returns>RulerBattleIndo correspondente</returns>
		public RulerBattleInfo GetEnemyBattleInfo( Ruler ruler ) {
			if ( ruler.Id == RBI1.OwnerId ) {
				return RBI2;
			}

			return RBI1;
		}

		/// <summary>
		/// Obtm o RulerBattleInfo de determinado utilizador
		/// </summary>
		/// <param name="ruler">objecto que representa o ruler</param>
		/// <returns>RulerBattleIndo correspondente</returns>
		public Ruler GetEnemyRuler( Ruler ruler ) {
			if ( ruler.Id == RBI1.OwnerId ) {
				return Universe.instance.getRuler( RBI2.OwnerId );
			}

			return Universe.instance.getRuler( RBI1.OwnerId );
		}

		public void DeleteRulersBattles( int battleId ) {
			RemoveRulersBattles(battleId);
		}

		public void RemoveRulersBattles( int battleId ) {
            Ruler r1 = (Ruler) Universe.instance.rulers[ RBI1.OwnerId ];
			Ruler r2 = (Ruler) Universe.instance.rulers[ RBI2.OwnerId ];
			
			IBattle iBattle1 = r1.GetIBattle( BattleId, BattleType );
			iBattle1.IsInBattle = false;
			r1.RemoveBattle( battleId, BattleType );

			IBattle iBattle2 = r2.GetIBattle( BattleId, BattleType );
			iBattle2.IsInBattle = false;
			r2.RemoveBattle( battleId, BattleType );
		}

		#endregion

		#region IEndBattle Members

        public BattleResult Result( Ruler one, Ruler two ) {
			if( !RBI1.Won && !RBI2.Won ) {
				return BattleResult.Draw;
			}
			RulerBattleInfo r = GetRulerBattleInfo(one);
			if( r.Won ) {
				return BattleResult.NumberOneVictory;
			}else {
				return BattleResult.NumberTwoVictory;
			}
		}
		
		private void SendBattleMessages() {
			Ruler ruler1 = Universe.instance.getRuler(RBI1.OwnerId);
			Ruler ruler2 = Universe.instance.getRuler(RBI2.OwnerId);
			IBattle iBattle1 = ruler1.GetIBattle( BattleId, BattleType );
			
			if( !RBI1.Won && !RBI2.Won ) {
				ruler1.Draws++;
				ruler2.Draws++;
				Messenger.Send(ruler1,"BattleEnded",ruler2.Name,iBattle1.Coordinate.ToString(),"Draw","0");
				Messenger.Send(ruler2,"BattleEnded",ruler1.Name,iBattle1.Coordinate.ToString(),"Draw","0");
			}else{
				if( RBI1.Won ) {
					int wonScore = GetWonScore(RBI1,RBI2,ruler2);
					int lostScore = GetLostScore(RBI1,ruler2,ruler1);
					Messenger.Send(ruler1,"BattleEnded",ruler2.Name,iBattle1.Coordinate.ToString(),"Won",wonScore.ToString());
					Messenger.Send(ruler2,"BattleEnded",ruler1.Name,iBattle1.Coordinate.ToString(),"Lost",lostScore.ToString());
					ruler1.addResource("Intrinsic", "score", wonScore);
					ruler2.addResource("Intrinsic", "score", lostScore);
				} else {
					int wonScore = GetWonScore(RBI2,RBI1,ruler1);
					int lostScore = GetLostScore(RBI2,ruler1,ruler2);
					Messenger.Send(ruler1,"BattleEnded",ruler2.Name,iBattle1.Coordinate.ToString(),"Lost",lostScore.ToString());
					Messenger.Send(ruler2,"BattleEnded",ruler1.Name,iBattle1.Coordinate.ToString(),"Won",wonScore.ToString());
					ruler2.addResource("Intrinsic", "score", wonScore);
					ruler1.addResource("Intrinsic", "score", lostScore);
				}
			}
		}
		private void SendMessages( string messageType, bool points ) {
			Ruler ruler1 = Universe.instance.getRuler(RBI1.OwnerId);
			Ruler ruler2 = Universe.instance.getRuler(RBI2.OwnerId);
			
			if( !RBI1.Won && !RBI2.Won ) {
				Messenger.Send(ruler1,messageType,ruler2.Name,"Draw");
				Messenger.Send(ruler2,messageType,ruler1.Name,"Draw");
			}else{
				if( RBI1.Won ) {
					Messenger.Send(ruler1,messageType,ruler2.Name,"Won");
					Messenger.Send(ruler2,messageType,ruler1.Name,"Lost");
					if( points ) {
						ruler1.addResource("Intrinsic", "score", 500);
					}
				}else {
					Messenger.Send(ruler1,messageType,ruler2.Name,"Lost");
					Messenger.Send(ruler2,messageType,ruler1.Name,"Won");
					if( points ) {
						ruler2.addResource("Intrinsic", "score", 500);
					}
				}
			}
		}

		public void BattleEnd() {
			SendBattleMessages();

			RBI1.BattleEnd( RBI2 );
			RBI2.BattleEnd( RBI1 );
		}

		public void TournamentEnd() {
			SendMessages("TournamentEnded",true);
		}

		public void FriendlyEnd() {
			SendMessages("FriendlyEnded",false);
		}

		public void ForceEndBattle(Ruler ruler) {
			RulerBattleInfo enemy = GetEnemyBattleInfo(ruler);
			GetRulerBattleInfo(ruler).ClearUnits(enemy);
			enemy.ForcePositioning();
			EndBattleBase.EndBattle();
		}

		#endregion
	}
}
