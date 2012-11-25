// created on 8/23/2005 at 10:19 AM

using System;
using System.Collections;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Battle {

	[Serializable]
	public class UnitDescriptor {
		
		#region Instance Fields
		
		private int movementCost;
		private string movementTypeDesc;
		private string unitType;
		private string level;
		private int baseDefense;
		private int baseAttack;
		private int range;
		private int maximumDamage;
		private int minimumDamage;
		private int hitPoints;
		private bool canStrikeBack;
		private bool canDamageBehindUnits;
		private bool catapultAttack;
		private bool replicatorAttack;
		private bool tripleAttack;
		private Hashtable attackTargets;
		private Hashtable defenseTargets;
		
		#endregion
		
		#region Instance Properties
		
		public int MovementCost {
			get { return movementCost; }
			set { movementCost = value; }
		}
		
		public int BaseDefense {
			get { return baseDefense; }
			set { baseDefense = value; }
		}
		
		public int BaseAttack {
			get { return baseAttack; }
			set { baseAttack = value; }
		}
		
		public int MaximumDamage {
			get { return maximumDamage; }
			set { maximumDamage = value; }
		}
		
		public int MinimumDamage {
			get { return minimumDamage; }
			set { minimumDamage = value; }
		}
		
		public int HitPoints {
			get { return hitPoints; }
			set { hitPoints = value; }
		}
		
		public int Range {
			get { return range; }
			set { range = value; }
		}
		
		public bool CanStrikeBack {
			get { return canStrikeBack; }
			set { canStrikeBack = value; }
		}
		
		public bool CatapultAttack {
			get { return catapultAttack; }
			set { catapultAttack = value; }
		}
		
		public bool ReplicatorAttack {
			get { return replicatorAttack; }
			set { replicatorAttack = value; }
		}
		
		public bool TripleAttack {
			get { return tripleAttack; }
			set { tripleAttack = value; }
		}
		
		public bool CanDamageBehindUnits {
			get { return canDamageBehindUnits; }
			set { canDamageBehindUnits = value; }
		}
		
		public string UnitType {
			get { return unitType; }
			set { unitType = value; }
		}
		
		public string MovementTypeDescription {
			get { return movementTypeDesc; }
			set { movementTypeDesc = value; }
		}
		
		public string Level {
			get { return level; }
			set { level = value; }
		}
		
		public Hashtable AttackTargets {
			get { return attackTargets; }
			set { attackTargets = value; }
		}
		
		public Hashtable DefenseTargets {
			get { return defenseTargets; }
			set { defenseTargets = value; }
		}
		
		#endregion
		
		#region Public Methods

		public int GetAttackBonus( Resource target, string terrain ) {
			int attack = 0;
			
			attack += AddUp( AttackTargets, "terrain", terrain );
			attack += AddUp( AttackTargets, "unit", target.Unit.UnitType );
			attack += AddUp( AttackTargets, "level", target.Unit.Level );
			attack += AddUp( AttackTargets, "unit", target.Name );
			
			return attack;
		}
		
		public int GetAttack( Resource target, string terrain )
		{
			int attack = BaseAttack;
			
			attack += AddUp( AttackTargets, "terrain", terrain );
			attack += AddUp( AttackTargets, "unit", target.Unit.UnitType );
			attack += AddUp( AttackTargets, "unit", target.Name );
			attack += AddUp( AttackTargets, "level", target.Unit.Level );
			
			return attack;
		}
		
		public int GetLive( Resource attacker, string terrain )
		{
			int hitpoints = HitPoints;
			
			hitpoints += AddUp( DefenseTargets, "terrain", terrain );
			hitpoints += AddUp( DefenseTargets, "unit", attacker.Unit.UnitType );
			hitpoints += AddUp( AttackTargets, "unit", attacker.Name );
			hitpoints += AddUp( DefenseTargets, "level", attacker.Unit.Level );
			
			return hitpoints;
		}
		
		public int AddUp( Hashtable hash, string root, string toSearch )
		{
			if( hash == null ) {
				return 0;
			}
			
			Hashtable target = (Hashtable) hash[root];
			if( target == null ) {
				return 0;
			}
			
			object val = target[toSearch];
			if( val == null ) {
				return 0;
			}
			
			return (int) val;
		}
		
		public Hashtable GetAttackTargets( string category )
		{
			return GetHash(AttackTargets, category);
		}
		
		public Hashtable GetDefenseTargets( string category )
		{
			return GetHash(DefenseTargets, category);
		}
		
		public Hashtable GetHash( Hashtable targets, string category )
		{
			if( targets == null ) {
				return null;
			}
			
			return (Hashtable) targets[category];
		}
		
		#endregion
		
	};

}