// created on 8/31/2005 at 8:49 AM

using System;
using System.Collections;
using Chronos.Battle;
using Chronos.Core;

namespace Chronos.Persistence {
	
	public class BattleFilePersistence : BattlePersistence {
		
		#region Ctor
		
		public BattleFilePersistence( PersistenceParameters param ) : base(param)
		{
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Obtm o contentor de mensagens</sumary>
		public Hashtable Container {
			get {
				object state = Universe.instance.PersistenceServices.GetState("--BATTLE--");
				if( state == null ) {
					state = new Hashtable();
					Universe.instance.PersistenceServices.Register("--BATTLE--", state);
				}
				return (Hashtable) state;
			}
		}
		
		#endregion
		
		#region Members
		
		/// <summary>Permite guardar uma batalha</summary>
		public override void SaveBattle( BattleInfo battleInfo, int rulerIdToPlay )
		{
			throw new NotImplementedException("Falta salvar o id");
			Container[battleInfo.BattleId] = battleInfo;
		}

		public override void SaveBattleTurn( int battleId, int rulerIdToPlay ) {
			throw new NotImplementedException("Falta salvar o id");
		}
		
		/// <summary>Obtm mensagens</summary>
		public override BattleInfo LoadBattle( int battleId )
		{
			return (BattleInfo) Container[battleId];
		}

		public override int LoadBattleTurn( int battleId ) {
			throw new NotImplementedException("falta carregar o id do jogador que vai jogar");
		}

		/// <summary>Permite guardar uma batalha</summary>
		public override void RemoveBattle( int battleInfo )
		{
			Container.Remove(battleInfo);
		}
	
		#endregion
		
		#region Utilities
		
		/// <summary>Cria um ID</summary>
		private string MakeKey( int id, string identifier )
		{
			return string.Format("{0}-{1}", identifier, id);
		}
		
		#endregion
		
	};
}
