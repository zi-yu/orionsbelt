using System;
using Chronos.Core;
using Chronos.Interfaces;

namespace Chronos.Battle{
	
	[Serializable]
	public class SimpleBattleInfo {
		#region ReadOnly Fields

		private static readonly int daySpan = 86400000;
		private static readonly int days = 1;
		private static int maxMisses = 3;

		#endregion

		#region Fields

		private bool _isTurn = false;
		private bool _isPositionTime = true;
		private bool _isPlanet;
		private bool _accepted = true;


		private int _battleId;
		private Ruler _enemy;
		private int _turnsLeft;
		private int _missedTurns = 0;

		private Coordinate _coordinate;

		private int _positionTime;
		private BattleType _type;
		private int _turn = 1;
		private string _endType;
		private Ruler _owner;
		
		[NonSerialized]
		private bool turnLoaded = false;

		#endregion

		#region Properties

		public bool IsTurn {
			get {
				if( !turnLoaded ) {
					int rulerid = Universe.instance.GetBattleTurn(BattleId);
					if( rulerid != 0 ) {
						_isTurn = rulerid == Owner.Id;
					}
					turnLoaded = true;
				}
				return _isTurn;
			}
			set {
				_isTurn = value;	
				SimpleBattleInfo info = Enemy.GetBattle( BattleId, BattleType );
				info._isTurn = !value;	
				if( value ) {
					Universe.instance.SaveBattleTurn( BattleId, Owner.Id);
				}else {
					Universe.instance.SaveBattleTurn( BattleId, info.Owner.Id);
				}
			}
		}

		public bool IsPositionTime {
			get { return _isPositionTime; }
			set { _isPositionTime = value; }
		}

		public bool EnemyIsPositionTime {
			get { return Enemy.GetBattle( BattleId, BattleType ).IsPositionTime; }
		}

		public bool IsPlanet {
			get { return _isPlanet; }
		}

		public bool Accepted {
			get { return _accepted; }
			set { _accepted = value; }
		}

		public int TurnsLeft {
			get {
				if( IsPositionTime ) {
					return _positionTime;
				}
				return _turnsLeft;
			}
			set {
				_turnsLeft = value;
			}
		}

		public int CurrentTurn {
			get { return _turn; }
			set { _turn = value; }
		}

		public int MissedTurns {
			get { return _missedTurns; }
			set { _missedTurns = value; }
		}
		public int BattleId {
			get { return _battleId; }
		}

		public Ruler Enemy {
			get { return _enemy; }
		}

		public Ruler Owner {
			get { return _owner; }
		}

		public Coordinate Coordinate {
			get { return _coordinate; }
		}

		public BattleType BattleType {
			get { return _type; }
		}

		public string Type {
			get { return _endType; }
		}

		#endregion

		#region Private

		private void TimeEnded() {
			if( IsTurn ) {
				SimpleBattleInfo enemyBattleInfo = Enemy.GetBattle( BattleId, BattleType );
				
				IsTurn = false;
				enemyBattleInfo.IsTurn = true;
				Universe.instance.SaveBattleTurn(BattleId, enemyBattleInfo.Owner.Id);
				ResetTurn();

				++MissedTurns;
				if( MissedTurns == SimpleBattleInfo.maxMisses ) {
					BattleInfo info = Universe.instance.GetBattle(BattleId);
					info.ForceEndBattle(Owner);
				}
            }
		}

		private void PassTurn() {
			if( IsTurn ) {
				SimpleBattleInfo enemyBattleInfo = Enemy.GetBattle( BattleId, BattleType );
				--_turnsLeft;
				enemyBattleInfo.TurnsLeft = TurnsLeft;
				if( _turnsLeft <= 0 ) {
					TimeEnded();
				}
			}
		}

		private void ForcePositioning() {
			BattleInfo battleInfo = Universe.instance.GetBattle( BattleId );

			battleInfo.GetRulerBattleInfo(Owner).ForcePositioning();

			if( IsTurn ) {
				Universe.instance.SaveBattle(battleInfo, Owner.Id);
			}else {
				Universe.instance.SaveBattle(battleInfo, Enemy.Id);
			}
		}

		private void PassPosition() {
			if( IsPositionTime ) {
				--_positionTime;
				if( _positionTime <= 0 ) {
					ForcePositioning();
					IsPositionTime = false;
					ResetTurn();
				}
			}
		}

		#endregion

		#region Public

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>O turno  incrementado pelo utilizador currente</remarks>
		public void Turn() {
			if( IsPositionTime || EnemyIsPositionTime ) {
				PassPosition();
			}else {
				PassTurn();
			}
		}

		public void ResetTurn() {
			SimpleBattleInfo info = Enemy.GetBattle( BattleId, BattleType );
			info.TurnsLeft = TurnsLeft = TurnsPerMove;
		}
		
		public int TurnsPerMove {
			get {
				return (int) ( ( days * daySpan ) / Universe.instance.TurnTime );
			}
		}

		public void AddTurn() {
			++_turn;
		}


		#endregion

		#region Constructor

		public SimpleBattleInfo( int battleId, Ruler owner, Ruler enemy, IBattle iBattle, BattleType type, string endType, bool isTurn ) {
			_battleId = battleId;
			_enemy = enemy;
			_owner = owner;
			_isTurn = isTurn;
			_isPlanet = iBattle is Planet;
			_type = type;
			
			_coordinate = iBattle.Coordinate;

			TurnsLeft = TurnsPerMove;

			_positionTime = TurnsPerMove;

			_endType = endType;
		}

		public SimpleBattleInfo( int battleId, Ruler owner, Ruler enemy, IBattle iBattle, BattleType type, string endType)
			: this( battleId, owner, enemy, iBattle, type, endType, false ){}

		#endregion

	}
}
