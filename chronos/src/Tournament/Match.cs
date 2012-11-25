// created on 9/2/2005 at 9:57 AM

using System;
using Chronos.Core;
using Chronos.Battle;

namespace Chronos.Tournaments {

	[Serializable]
	public class Match {
	
		#region Instance Fields
		
		private Ruler a;
		private Ruler b;
		private int aPoints;
		private int bPoints;
		private int battleId;
		private BattleResult result;
		
		#endregion
		
		#region Instance Properties
		
		public Ruler NumberOne {
			get { return a; }
		}
		
		public int NumberOnePoints {
			set { aPoints = value; }
			get { return aPoints; }
		}
		
		public int NumberTwoPoints {
			set { bPoints = value; }
			get { return bPoints; }
		}
		
		public Ruler NumberTwo {
			get { return b; }
		}
		
		public Ruler Winner {
			get { 
				if( Result == BattleResult.NumberOneVictory ) {
					return NumberOne;
				}
				if( Result == BattleResult.NumberTwoVictory ) {
					return NumberTwo;
				}
				
				return null;
			}
		}
		
		public int BattleId {
			get { return battleId; }
			set { battleId = value; }
		}
		
		public BattleResult Result {
			get { return result; }
			set { result = value; }
		}
		
		#endregion
		
		#region Ctors
		
		public Match( Ruler _a, Ruler _b ) 
		{
			a = _a;
			b = _b;
			aPoints = 0;
			bPoints = 0;
			result = BattleResult.None;
		}
		
		#endregion
		
		#region Public Methods
		
		public override string ToString()
		{
			return string.Format("{0} vs {1}", NumberOne, NumberTwo);
		}
		
		public bool Participates( Ruler ruler )
		{
			return ruler.Id == NumberOne.Id || ruler.Id == NumberTwo.Id;
		}
		
		#endregion
		
		#region Utilities
		
		#endregion
	
	};

}