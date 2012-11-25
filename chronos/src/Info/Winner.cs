// created on 19-01-2005 at 18:09

using System;
using Chronos.Core;

namespace Chronos.Info {

	/// <summary>Indica um vencedor de um prémio</summary>
	[Serializable]
	public class Winner {
	
		#region Instance Fields
	
		private int turn;
		private Ruler ruler;
		private string prize;
		private Medal medal;
		private PrizeCategory category;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Constrói o vencedor</summary>
		internal Winner(PrizeCategory category, int turn, Ruler ruler, string prize, Medal medal )
		{
			this.turn = turn;
			this.ruler = ruler;
			this.prize = prize;
			this.medal = medal;
			this.category = category;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o turno em que ocorreu o prémio</summary>
		public int Turn {
			get { return turn; }
		}
		
		///< summary>Indica o Ruler que ganhou o prémio</summary>
		public Ruler Ruler {
			get { return ruler; }
		}
		
		/// <summary>Indica qual foi o prémio</summary>
		public string Prize {
			get { return prize; }
		}
		
		/// <summary>Indica a medalha do prémio</summary>
		public Medal Medal {
			get { return medal; }
		}
		
		/// <summary>Indica a categoria do pr�mio</summary>
		public PrizeCategory Category {
			get { return category; }
		}
		
		#endregion
	};
}
