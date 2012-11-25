
using System;
using System.Data;
using Chronos.Core;
using Chronos.Battle;

namespace Chronos.Persistence {
	
	/// <summary>Camada de dados para as Mensagens</summary>
	public abstract class BattlePersistence {
		
		#region Instance Fields
		
		private PersistenceParameters parameters;
		
		#endregion
		
		#region Constructor
		
		public BattlePersistence( PersistenceParameters param ) {
			parameters = param;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Obtm os parmetros de persistncia</summary>
		public PersistenceParameters Parameters {
			get { return parameters; }
		}
		
		#endregion
		
		#region Members
		
		/// <summary>Permite guardar uma batalha</summary>
		public abstract void SaveBattle( BattleInfo battleInfo, int rulerIdToPlay );

		public abstract void SaveBattleTurn( int battleId, int rulerIdToPlay );

		/// <summary>Obtm mensagens</summary>
		public abstract BattleInfo LoadBattle( int battleId );

		public abstract int LoadBattleTurn( int battleId );

		/// <summary>Permite guardar uma batalha</summary>
		public abstract void RemoveBattle( int battleInfo );
		
		#endregion
		
		#region Static Members
		
		private static BattlePersistence persistence;
		
		/// <summary>Construtor esttico</summary>
		static BattlePersistence() {
#if PERSIST_TO_POSTGRE
			persistence = new Chronos.Persistence.PostGreSql.PostGreBattlePersistence(Universe.Parameters);
#elif PERSIST_TO_SQLSERVER
			persistence = new Chronos.Persistence.SqlServer.SqlServerBattlePersistence(Universe.Parameters);		
#elif PERSIST_TO_FILE
			persistence = new BattleFilePersistence(Universe.Parameters);
#else
#	error No foi encontrada menhuma macro de compilacao!
#endif
		}
		
		/// <summary>Retorna o objecto que se encarrega da persistncia</summary>
		public static BattlePersistence Instance {
			get {
				return persistence;
			}
		}
		
		#endregion
		
		
	};
}
