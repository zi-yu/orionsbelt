using System;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Messaging;
using Chronos.Utils;

namespace Chronos.Battle {
	
	public class BattleInterpreter : InterpreterBase {
		
		#region Private Fields

		private int s_x;
		private int s_y;
		private int d_x;
		private int d_y;
		private int range;
		private RulerBattleInfo info;
		private RulerBattleInfo enemyInfo;
		private Element currentElement;
	
		#endregion

		#region Constructor

		public BattleInterpreter( string info, BattleInfo battleInfo ) : base( info, battleInfo )
			{}

		#endregion

		#region CheckAttack Methods

		private bool CheckPathVert( int stat, int src, int dst ) {
			for( int i = src; i < dst; ++i ) {
				if( ( info.SectorHasElements(i+"_"+stat) || enemyInfo.SectorHasElements( RulerBattleInfo.InvertSector( i+"_"+stat ) ) ) && !currentElement.Unit.CatapultAttack ) {
					return false;
				}
			}
			return true;
		}
			
		private bool CheckPathHoriz( int stat, int src, int dst ) {
			for( int i = src; i < dst; ++i ) {
				if( ( info.SectorHasElements( stat+"_"+i ) || enemyInfo.SectorHasElements( RulerBattleInfo.InvertSector( stat+"_"+i ) ) ) && !currentElement.Unit.CatapultAttack ) {
					return false;
				}
			}
			return true;
		}
		
		private bool CheckN() {
			int v = s_x-range;
			if( this.CheckPathVert(s_y,d_x+1,s_x) ){
				return d_x < s_x && d_x >= v && s_y == d_y;
			}
			return false;
		}

		private bool CheckS() {
			int v = s_x+range;
			if( this.CheckPathVert(s_y,s_x+1,d_x) ){
				return d_x > s_x && d_x <= v && s_y == d_y;
			}
			return false;
		}

		private bool CheckW() {
			int v = s_y-range;
			if( this.CheckPathHoriz(s_x,d_y+1,s_y) ) {
				return d_y < s_y && d_y >= v && s_x == d_x;
			}
			return false;
		}

		private bool CheckE() {
			int v = s_y+range;
			if( this.CheckPathHoriz(s_x,s_y+1,d_y) ) {
				return d_y > s_y && d_y <= v && s_x == d_x;
			}
			return false;
		}


		#endregion

		#region Private

		private void MakeDamage( string src, string dst ) {
			Element target = enemyInfo.SectorGetElement( dst );
			if( null != target) {
				int attack = currentElement.Unit.BaseAttack;
				int targetDefense = target.Unit.BaseDefense;
				
				int damage = CalculateDamage( target, attack, targetDefense )*currentElement.Quantity;
				damage = CalculatePenalty( damage );
				int q = MakeCalculations(damage, currentElement, target, enemyInfo, dst);

				Messenger.Send( info,"BattleAttack",src, RulerBattleInfo.InvertSector(dst) );
				Messenger.Send( info,"BattleDamage",currentElement.Quantity.ToString(),currentElement.Type,damage.ToString(),q.ToString(),target.Type);

				Messenger.Send( enemyInfo,"BattleAttack",RulerBattleInfo.InvertSector( src ), dst );
				Messenger.Send( enemyInfo,"BattleDamage",currentElement.Quantity.ToString(),currentElement.Type,damage.ToString(),q.ToString(),target.Type);

				UnitsDestroyed(q, target);

				Rebound(target, q, dst, damage);
				TripleAttack(target, dst, damage/2);
			}
		}

		private int MakeCalculations(int damage, Element attacker, Element target,RulerBattleInfo enemyInfo, string dst) {
			int q = damage/target.Unit.GetLive( attacker.Resource, BattleInfo.Terrain );
			if( q >= target.Quantity || q < 0) {
				enemyInfo.SectorRemoveElement( dst );
				q = target.Quantity;
			} else {
				target.Quantity -= q;
				target.RemainLive -= damage - (q*target.Unit.HitPoints);
				if( target.RemainLive == 0 ) {
					target.Quantity -=1;
					q += 1;
					if( target.Quantity == 0 ) {
						enemyInfo.SectorRemoveElement( dst );
						q = 1;
					}else {
						target.RemainLive = target.Unit.HitPoints;
					}
				}
			}
			return q;
		}

		private int CalculateRestDamage(int damage, Element target) {
			int live = target.Unit.GetLive( currentElement.Resource, BattleInfo.Terrain );
			return damage - (target.Quantity*live);
		}

		private void UnitsDestroyed(int q, Element target) {
			info.AddUnitsDestroyed( q, target.Unit.UnitType );
			info.AddResourceDestroyed( q, target.Resource );
			Replicator(q);

			if( target.IsBuilding ) {
				info.AddBuildingDestroyed( target.Type );
			}
		}

		private int CalculatePenalty(int damage) {
			int distance = Distance();
			if( distance < 4 ) {
				return damage;
			}
			double percent = (7 - distance)*0.25;
			Messenger.Send( info,"BattlePenalty", (100 - (percent*100)).ToString(), distance.ToString() );
			Messenger.Send( enemyInfo,"BattlePenalty", (100 - (percent*100)).ToString(), distance.ToString() );
			
			return (int) ( ( percent * damage) + 0.5 );
		}

		private int Distance() {
			if( s_y == d_y ) {
				return Math.Abs(s_x-d_x);
			}
			return Math.Abs(s_y-d_y);
		}

		private int CalculateDamage( Element target, int attack, int defense ) {
			int damage = (int) ( currentElement.Unit.MinimumDamage + ( (currentElement.Unit.MaximumDamage-currentElement.Unit.MinimumDamage) / 2 ) ) ;
			int mod = attack/defense;
			
			int bonus = currentElement.Unit.GetAttackBonus( target.Resource, BattleInfo.Terrain );
			
			if( mod >= 1 ) {
				return MathUtils.random( damage,currentElement.Unit.MaximumDamage ) + bonus;
			}

			return MathUtils.random( currentElement.Unit.MinimumDamage, damage ) + bonus;
		}

		private void InitAttackCheck( string src, string dst, int range ) {
			s_x = int.Parse(src[0].ToString());
			s_y = int.Parse(src[2].ToString());
			d_x = int.Parse(dst[0].ToString());
			d_y = int.Parse(dst[2].ToString());

			this.range = range;
		}

		private bool CanAttack( string position ) {
			switch( position ) {
				case "n": return CheckN();
				case "s": return CheckS();
				case "w": return CheckW();
				default: return CheckE();
			}
		}

		private void SwapInfo( string src, string dst) {
			RulerBattleInfo temp = info;
			info = enemyInfo;
			enemyInfo = temp;

			s_x = int.Parse(src[0].ToString());
			s_y = int.Parse(src[2].ToString());
			d_x = int.Parse(dst[0].ToString());
			d_y = int.Parse(dst[2].ToString());
		}

		private bool IsAtRange() {
			return currentElement.Unit.Range >= Distance();
		}

		#endregion

		#region Attacks

		private void Replicator( int q ) {
			if( currentElement.Unit.ReplicatorAttack ) {
				currentElement.Quantity += q;
				Messenger.Send( info,"BattleReplicator",q.ToString(), currentElement.Type );
				Messenger.Send( enemyInfo,"BattleReplicator",q.ToString(), currentElement.Type);
				
			}
		}

		private void Rebound(Element target, int q, string dst, int damage) {
			if( currentElement.Unit.CanDamageBehindUnits && target.Quantity == q ) {
				string nextCoord = RulerBattleInfo.NextSector( dst,currentElement.Position.ToString().ToLower() );
				if( enemyInfo.SectorHasElements( nextCoord ) ) {
					damage = CalculateRestDamage( damage, target );
					target = enemyInfo.SectorGetElement( nextCoord );
						
					q = MakeCalculations( damage , currentElement, target, enemyInfo, nextCoord);
					UnitsDestroyed(q, target);

					Messenger.Send( enemyInfo,"BattleRebound", damage.ToString(), q.ToString(), target.Type );
					Messenger.Send( info,"BattleRebound", damage.ToString(),  q.ToString(), target.Type );
				}
			}
		}

		private void TripleAttack(Element target, string dst, int damage) {
			if( currentElement.Unit.TripleAttack ) {
				string position = currentElement.Position.ToString().ToLower();
				string leftCoord = RulerBattleInfo.LeftSector( dst, position );
				string rightCoord = RulerBattleInfo.RightSector( dst, position );

				TripleAttackDamage(enemyInfo, info, leftCoord, damage, target, "Left","Right");
				TripleAttackDamage(enemyInfo, info, rightCoord, damage, target, "Right","Left");
			}
		}

		private void TripleAttackDamage(RulerBattleInfo rinfo, RulerBattleInfo rinfo2, string coord, int damage, Element target, string side, string side2) {
			if( rinfo.SectorHasElements( coord ) ) {
				target = rinfo.SectorGetElement( coord );

				int q = MakeCalculations( damage , currentElement, target, rinfo, coord);
				UnitsDestroyed(q, target);

				Messenger.Send( rinfo,"BattleTripleAttack" + side, damage.ToString(), q.ToString(), target.Type );
				Messenger.Send( rinfo2,"BattleTripleAttack"  + side2, damage.ToString(),  q.ToString(), target.Type );
			}
		}


		#endregion

		#region Public 

		public override ResultItem CheckMove( ) {
			string[] items = Move.Split( '-' );

			if( items.Length > 2 ) {
				return new InvalidMove();
			}

			Ruler ruler = Universe.instance.getRuler( BattleInfo.CurrentRulerId );
			info = BattleInfo.GetRulerBattleInfo( ruler );
			enemyInfo = BattleInfo.GetEnemyBattleInfo( ruler );
			
			if( !GridCoordValid( items[0] ) ) {
				return new InvalidCoordinate( items[0]);
			}

			if( !info.SectorHasElements( items[0] ) ) {
				return new InvalidShip( items[0] );
			}

			if( !GridCoordValid( items[1] ) ) {
				return new InvalidCoordinate( items[0]);
			}
			
			currentElement = info.SectorGetElement( items[0] );
			InitAttackCheck( items[0],items[1], currentElement.Unit.Range );
			if( !CanAttack( currentElement.Position.ToString().ToLower() ) ) {
				return new InvalidAttack( items[0], items[1]);
			}

			return null;
		}

		public override void Interpretate( ) {
			string[] items = Move.Split( '-' );

			MakeDamage( items[0], RulerBattleInfo.InvertSector(items[1]) );

			Element enemy = enemyInfo.SectorGetElement( RulerBattleInfo.InvertSector(items[1]));

			if( null != enemy && enemy.Unit.CanStrikeBack && enemy.Position == currentElement.Position ) {
				SwapInfo( RulerBattleInfo.InvertSector(items[1]), RulerBattleInfo.InvertSector(items[0]) );

				currentElement = enemy;
				
				if( !currentElement.Unit.CatapultAttack ) {
					if( !CanAttack( currentElement.Position.ToString().ToLower() ) ) {
						return;
					}
				}

				if( IsAtRange() ) {
					MakeDamage( RulerBattleInfo.InvertSector(items[1]), items[0] );
				}
			}
		}

		#endregion

	}
}