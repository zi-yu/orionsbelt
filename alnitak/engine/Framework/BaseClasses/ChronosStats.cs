// created on 9/10/2004 at 9:24 AM

using System;
using Chronos.Core;

namespace Alnitak {

	public class ChronosStats {
	
		#region Instance Fields
	
		private DateTime started;
		private DateTime lastTurn;
		private TimeSpan lastTurnTime;
		private TimeSpan dressUp;
		private int turnCount;
	
		#endregion
		
		#region Ctors
		
		public ChronosStats()
		{
			turnCount = 0;
			DateTime now = DateTime.Now;
			lastTurnTime = dressUp =  now - now;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o momento em que o motor começou a rodar</summary>
		public DateTime StartTime {
			get { return started; }
			set { started = value; }
		}
		
		/// <summary>Indica o momento do último turno</summary>
		public DateTime LastTurn {
			get { return lastTurn; }
			set { lastTurn = value; }
		}
		
		/// <summary>Indica o tempo que demorou o último turno</summary>
		public TimeSpan LastTurnTime {
			get { return lastTurnTime; }
			set { lastTurnTime = value; }
		}
		
		/// <summary>Indica quanto tempo demorou para o jogo estar a postos</summary>
		public TimeSpan DressUp {
			get { return dressUp; }
			set { dressUp = value; }
		}
		
		/// <summary>Indica a quantidade de turnos que já passaram</summary>
		public int TurnCount {
			get { return turnCount; }
			set { turnCount = value; }
		}

		/// <summary>Indica a data do próximo turno</summary>
		public DateTime NextTurn {
			get { return LastTurn.AddMilliseconds(Universe.instance.TurnTime); }
		}

		/// <summary>Indica quantos minutos faltam para o próximo turno</summary>
		public int MinutesToNextTurn {
			get {
				TimeSpan span = NextTurn - DateTime.Now;
				return span.Minutes + 1;
			}
		}
		
		#endregion
	
	};

}
