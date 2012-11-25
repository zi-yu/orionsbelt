// created on 10/24/2005 at 10:06 AM

using System;
using System.Collections;
using Chronos.Core;

namespace Chronos.Tournaments {

	[Serializable]
	public class Classification : IComparable {
	
		#region Instance Fields
		
		private Ruler player;
		private int draws;
		private int wins;
		private int defeats;
		private int points;
		
		#endregion
		
		#region Instance Properties
		
		public Ruler Player {
			get { return player; }
		}
		
		public int Points {
			get{ return points; }
			set { points = value; }
		}
		
		public int Draws {
			get { return draws; }
			set { draws = value; }
		}
		
		public int Wins {
			get { return wins; }
			set { wins = value; }
		}
		
		public int Defeats {
			get { return defeats; }
			set { defeats = value; }
		}
		
		public int Games {
			get { return Defeats + Draws + Wins; }
		}
		
		#endregion
		
		#region Ctors
		
		public Classification( Ruler _player )
		{
			player = _player;
			defeats = wins = draws = 0; 
		}
		
		public Classification( Ruler _player, int points )
		{
			player = _player;
			defeats = wins = draws = 0;
			Points = points; 
		}
		
		#endregion
		
		#region Public Methods
		
	
		#endregion
		
		#region Utilities
		
	
		#endregion
		
		#region IComparable Implementation
		
		public int CompareTo( object another )
		{
			Classification c = another as Classification;
			if( c == null ) {
				return 0;
			}
			
			return -Points.CompareTo(c.Points);
		}
		
		#endregion
	
	};

}