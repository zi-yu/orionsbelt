// created on 9/7/2005 at 9:14 AM

//#define DEBUG_SABOTAGE

using System;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Interfaces;
using Chronos.Sabotages.Factories;
using Chronos.Messaging;
using Chronos.Utils;
using DesignPatterns;

namespace Chronos.Sabotages {

	[Serializable]
	public abstract class Sabotage : ITask {
	
		#region Instance Fields
		
		protected Planet planet;
		protected Planet source;
		
		#endregion
		
		#region Instance Properties
		
		public Planet Target {
			get { return planet; }
			set { planet = value; }
		}
		
		public Planet Source {
			get { return source; }
			set { source = value; }
		}
		
		#endregion
	
		#region Sabotage Members
		
		/// <summary>Indica quantos Soldados da defesa morreram</summary>
		public int MarinesKilled()
		{
			int attackForce = Spies;
			int defenseForce = planet.Marines;
			int numberOfBarracks = planet.getResourceCount("Building", "Barracks");
			
			double percent = 1 - ((numberOfBarracks-1)*5/10);
			if( percent < 0.50 ) {
				percent = 0.50;
			}
			
			int min = attackForce;
			if( defenseForce < attackForce ) {
				min = defenseForce;
			}
			
			return (int) (min * percent);
		}
		
		/// <summary>Indica se a Sabotagem vai suceder</summary>
		public bool SabotageSucceded()
		{
			int attackForce = Spies;
			int defenseForce = planet.Marines;
			int numberOfBarracks = planet.getResourceCount("Building", "Barracks");
			
			int defense = defenseForce + numberOfBarracks *  500;
			int ratio = defense / attackForce;

#if DEBUG_SABOTAGE
			Log.log("---DEBUG_SABOTAGE-----");
			Log.log("attackForce: {0}", attackForce);
			Log.log("defenseForce: {0}", defenseForce);
			Log.log("numberOfBarracks: {0}", numberOfBarracks);
			Log.log("defense: {0}", defense);
			Log.log("ratio: {0}", ratio);
#endif

			if( ratio < 2 ) {
				return true;
			} else if( ratio < 4 ) {
				return RandomSuccess();
			} else {
				return false;
			}
		}
		
		/// <summary>Indica sucesso com baixa probabilidade</summary>
		private bool RandomSuccess()
		{
			int number = MathUtils.random(0, 100);
			return number < 20;
		}
		
		/// <summary>Realiza a sabotagem</summary>
		public void PrepareSabotage( Planet _source, Planet _target )
		{
			this.planet = _target;
			this.source = _source;
			
			this.source.take( "Intrinsic", "spy", Spies );
			
			planet.Tasks.Register(TaskDescriptor.Sabotage, this, Turns, 1);
		}
		
		/// <summary>Indica se é possível realizar a sabotagem</summary>
		public Result CanSabotage( Planet source, Planet target )
		{
			Result result = new Result();
		
			if( !source.isResourceAvailable("Research", "Espionage") ) {
				result.failed( new ResourceNotAvailable("Research", "Espionage") );
				return result;
			}
		
			if( source.Spies < Spies ) {
				result.failed( new ResourceQuantityNotAvailable("spy") );
				return result;
			}
			
			if( !Coordinate.IsAccessible(source, target.Coordinate) ) {
				result.failed( new TargetNotAccessible(target.Coordinate) );
				return result;
			}
			
			if( source.Tasks.HasTask( TaskDescriptor.Sabotage ) ) {
				result.failed( new FullQueue(1, 1) );
				return result;
			}
			
			SpecificCanSabotage(result, source, target);
			
			if( result.Ok ) {
				result.passed(new OperationSucceded());
			}
			
			return result;
		}
		
		/// <summary>Indica se é possível realizar a sabotagem</summary>
		protected virtual void SpecificCanSabotage( Result result, Planet source, Planet target )
		{
		}
		
		/// <summary>Realiza a sabotagem</summary>
		protected abstract void DoSabotage();
		
		/// <summary>Indica a quantidade de turnos necessária</summary>
		public abstract int Turns {get; }
		
		/// <summary>Indica a quantidade mínima de espioes necessária</summary>
		public abstract int Spies {get; }
		
		/// <summary>Nome desta sabotagem</summary>
		public abstract string Key {get; }
		
		#endregion
		
		#region ITask Implementation
		
		/// <summary>Realiza a Sabotagem</summary>
		public void turn()
		{
			int killed = MarinesKilled();
			bool success = SabotageSucceded();
			
			planet.take("Intrinsic", "marine", killed );
			
			if( Target.InitMade && success ) {
				DoSabotage();
				Messenger.Send(source, "SabotageSucceded", Key, source.Coordinate.ToString(), killed.ToString());
			} else {
				Messenger.Send(source, "SabotageFailed", Key, source.Coordinate.ToString(), killed.ToString());
				Messenger.Send(source, "SabotageAttempt", Key, source.Coordinate.ToString(), Target.Coordinate.ToString(), killed.ToString() );
			}
		}
		
		#endregion
		
		#region Static Members
		
		private static FactoryContainer factories = new FactoryContainer(typeof(SabotageFactory));
		
		public static FactoryContainer Factories {
			get { return factories; }
		}
		
		public static Sabotage GetSabotage( string key )
		{
			return (Sabotage) factories.create( key, null );
		}
		
		#endregion
	
	};

}