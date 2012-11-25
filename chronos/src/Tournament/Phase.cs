// created on 9/2/2005 at 9:57 AM

using System;
using System.Collections;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Battle;
using Chronos.Resources;

namespace Chronos.Tournaments {

	[Serializable]
	public abstract class Phase {
	
		#region Instance Fields
		
		private ArrayList registered;
		private Tournament owner;
		
		#endregion
		
		#region Instance Properties
		
		public ArrayList Registered {
			get { return registered; }
			set { registered = value; }
		}
		
		public Tournament Owner {
			get { return owner; }
		}
	
		#endregion
		
		#region Ctors
		
		public Phase( ArrayList _registered, Tournament _owner )
		{
			owner = _owner;
			registered = Randomize(_registered);
		}
		
		public Phase( Tournament _owner )
		{
			owner = _owner;
		}
		
		#endregion
		
		#region Public Methods
		
		public void GenerateBattle( Match match )
		{
			Fleet f1 = GetFleet();
			f1.Owner = match.NumberOne;
			
			Fleet f2 = GetFleet();
			f2.Owner = match.NumberTwo;
		
			match.BattleId = Universe.instance.CreateBattle(
								match.NumberOne,
								f1,
								match.NumberTwo,
								f2,
								BattleType.TOURNAMENT,
								Owner.TournamentType
						);
		}
		
		#endregion
		
		#region Abstract Members
		
		public abstract void ForceEnd();
		
		public abstract ArrayList GetWinners();
		
		public abstract void BattleEnded( BattleInfo battle );
		
		public abstract int Participants{get;}
		
		#endregion
		
		#region Utilities
		
		public int GetPoints( RulerBattleInfo info )
		{
			return Universe.GetTournamentPoints(info);
		}
		
		public ArrayList Randomize( ArrayList ordered )
		{
			ArrayList list = new ArrayList();
			
			while( ordered.Count != 0 ) {
				int idx = MathUtils.random(0, ordered.Count);
				object obj = ordered[idx];
				ordered.RemoveAt(idx);
				list.Add(obj); 
			}
			
			return list;
		}
		
		public Fleet GetFleet()
		{
			return Owner.BaseFleet.DeepClone();
		}
		
		#endregion
	
	};

}