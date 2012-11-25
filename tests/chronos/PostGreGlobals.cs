using Chronos.Persistence;

using Chronos.Persistence.PostGreSql;

namespace Chronos.Tests {

	public class PostGreGlobals {
		
		#region Properties
		
		public static string ConnectionString {
			get {
				return "server=localhost;User ID=sa;password=spoon;Database=orionsbelttest";
			}
		}

		#endregion
		
		#region Utilities

		public static void InitConnectionString() {
			PostGreSqlMessagesPersistence messagesPersistence = MessagesPersistence.Instance as PostGreSqlMessagesPersistence;
			if( null != messagesPersistence )
				messagesPersistence.ConnString = ConnectionString;

			PostGreBattlePersistence battlePersistence = BattlePersistence.Instance as PostGreBattlePersistence;
			if( null != messagesPersistence )
				battlePersistence.ConnString = ConnectionString;
		}
		
		#endregion

	};

}
