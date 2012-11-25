using Chronos.Persistence;

using Chronos.Persistence.SqlServer;

namespace Chronos.Tests {

	public class SqlServerGlobals {
		
		#region Properties
		
		public static string ConnectionString {
			get {
				return "server=.;uid=orionsbelt;pwd=spoon;database=orionsbeltTests";
			}
		}

		#endregion
		
		#region Utilities

		public static void InitConnectionString() {
			SqlServerMessagesPersistence messagesPersistence = MessagesPersistence.Instance as SqlServerMessagesPersistence;
			if( null != messagesPersistence )
				messagesPersistence.ConnString = ConnectionString;

			SqlServerBattlePersistence battlePersistence = BattlePersistence.Instance as SqlServerBattlePersistence;
			if( null != messagesPersistence )
				battlePersistence.ConnString = ConnectionString;
		}
		
		#endregion

	};

}
