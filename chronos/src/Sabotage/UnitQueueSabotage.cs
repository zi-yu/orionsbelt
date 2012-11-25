// created on 9/7/2005 at 10:12 AM

using System;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Interfaces;
using Chronos.Messaging;
using DesignPatterns;

namespace Chronos.Sabotages {

	[Serializable]
	public class UnitQueueSabotage : Sabotage {
	
		#region Sabotage Implementation
		
		/// <summary>Realiza a sabotagem</summary>
		protected override void DoSabotage()
		{
			planet.cancel("Unit");
			Messenger.Send(planet, "QueueSabotage", "Unit", source.Coordinate.ToString(), MarinesKilled().ToString() );
		}
		
		/// <summary>Indica a quantidade de turnos necessária</summary>
		public override int Turns {
			get { return 25; } 
		}
		
		/// <summary>Indica a quantidade mínima de espioes necessária</summary>
		public override int Spies {
			get { return 2500; } 
		}
		
		/// <summary>Nome desta sabotagem</summary>
		public override string Key {
			get { return "UnitQueue"; } 
		}
		
		#endregion
	
	};

}