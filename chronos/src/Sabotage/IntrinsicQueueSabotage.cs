// created on 9/7/2005 at 10:12 AM

using System;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Interfaces;
using Chronos.Messaging;
using DesignPatterns;

namespace Chronos.Sabotages {

	[Serializable]
	public class IntrinsicQueueSabotage : Sabotage {
	
		#region Sabotage Implementation
	
		/// <summary>Realiza a sabotagem</summary>
		protected override void DoSabotage()
		{
			planet.cancel("Intrinsic");
			Messenger.Send(planet, "QueueSabotage", "Barracks", source.Coordinate.ToString(), MarinesKilled().ToString() );
		}
		
		/// <summary>Indica a quantidade de turnos necessária</summary>
		public override int Turns {
			get { return 20; } 
		}
		
		/// <summary>Indica a quantidade mínima de espioes necessária</summary>
		public override int Spies {
			get { return 1000; } 
		}
		
		/// <summary>Nome desta sabotagem</summary>
		public override string Key {
			get { return "IntrinsicQueue"; } 
		}
		
		#endregion
	
	};

}