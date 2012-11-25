using System;
using System.Collections;
using System.Data;

namespace Alnitak {
	
	public abstract class AllianceUtility {
		
		#region Static Region
		
		/// <summary>Indica o driver que sabe persistir notcias</summary>
		public static AllianceUtility Persistance {
			get {
				string value = OrionGlobals.getConfigurationValue("utilities",getKey());
				
				return (AllianceUtility)Activator.CreateInstance( Type.GetType( value , true) );
			}
		}
		
		public static string getKey()
		{
			return OrionGlobals.resolveDataAccessName("allianceUtility");
		}
		
		#endregion
		
		#region Abstract Methods
		
		public abstract ArrayList GetAll();
		
		public abstract AllianceInfo Get( int id );
		
		public abstract void Save( AllianceInfo alliance );
		
		public abstract int Register( AllianceInfo alliance );
		
		#endregion
		
		#region Utilities
		
		protected ArrayList FromDataSet( DataSet ds )
		{
			ArrayList list = new ArrayList();
			
			foreach( DataRow row in ds.Tables[0].Rows ) {
				AllianceInfo info = new AllianceInfo();
				
				info.Id = (int) row["alliance_id"];
				info.Ranking = (int) row["alliance_rank"];
				info.RankingBattles = (int) row["alliance_rankBattles"];
				
				info.Name = (string) row["alliance_name"];
				info.Motto = (string) row["alliance_motto"];
				info.Tag = (string) row["alliance_tag"];
				
				//info.RegistDate = (DateTime) row["alliance_regist"];
				
				UserUtility.bd.setAllianceMembers( info );
				
				list.Add(info);
			}
			
			return list;
		}
		
		public static void SortByRanking( ArrayList alliances )
		{
			alliances.Sort( new AllianceRankingSorter() );
		}
		
		public static void SortByRound( ArrayList alliances )
		{
			alliances.Sort( new AllianceScoreSorter() );
		}
		
		#endregion
		
	};
	
	
	public class AllianceRankingSorter : IComparer {
	
		#region PlanetComparer Implementation
		
		int IComparer.Compare( object x, object y )
		{
			AllianceInfo a1 = x as AllianceInfo;
			AllianceInfo a2 = y as AllianceInfo;
			
			if( a1 == null || a2 == null ) {
				return 0;
			}
			
			int byRank = -a1.Ranking.CompareTo(a2.Ranking);
			if( byRank != 0 ) {
				return byRank;
			}
			
			return -a1.AverageRanking.CompareTo(a2.AverageRanking);
		}
		
		#endregion
	};
	
	public class AllianceScoreSorter : IComparer {
	
		#region PlanetComparer Implementation
		
		int IComparer.Compare( object x, object y )
		{
			AllianceInfo a1 = x as AllianceInfo;
			AllianceInfo a2 = y as AllianceInfo;
			
			if( a1 == null || a2 == null ) {
				return 0;
			}
			
			return -a1.Score.CompareTo(a2.Score);
		}
		
		#endregion
	};
	
}
