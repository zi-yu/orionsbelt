// created on 3/20/2006 at 10:19 AM

using System;
using System.Collections;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using ByteFX.Data;
using ByteFX.Data.MySqlClient;
using Chronos.Utils;

namespace Alnitak {

	public class MySqlAllianceUtility : AllianceUtility {
	
		#region Utilities
	
		private MySqlConnection conn;
	
		/// <summary>Construtor</summary>
		public MySqlAllianceUtility()
		{
			conn = new MySqlConnection( OrionGlobals.getConnectionString("connectiostring-mysql") );
		}
		
		#endregion Utilities
		
		#region Base Implementation
		
		public override ArrayList GetAll()
		{
			DataSet alliances = MySqlUtility.getAll("Alliance");
			return FromDataSet(alliances);
		}
		
		public override AllianceInfo Get( int id )
		{
			DataSet alliances = MySqlUtility.getQuery("select * from Alliance where alliance_id=" + id);
			return (AllianceInfo) FromDataSet(alliances)[0];
		}
		
		public override void Save( AllianceInfo info )
		{
			StringWriter writer = new StringWriter();
			
			writer.Write("UPDATE Alliances SET ");
			writer.Write("alliance_name='{0}', ", info.Name);
			writer.Write("alliance_tag='{0}', ", info.Tag);
			writer.Write("alliance_motto='{0}', ", info.Motto);
			writer.Write("alliance_rank='{0}', ", info.Ranking);
			writer.Write("alliance_rankBattles='{0}' ", info.RankingBattles);
			writer.Write("WHERE alliance_id={0}", info.Id);
			
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = writer.ToString();
				
				cmd.ExecuteNonQuery();
				
			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.ToString());
				Console.WriteLine("Update alliance: " + e.ToString());
			} finally {
				conn.Close();
			}
		}
		
		public override int Register( AllianceInfo info )
		{
			StringWriter writer = new StringWriter();
			
			writer.Write("insert into Alliance(alliance_id, alliance_name, alliance_tag, alliance_motto, alliance_rank, alliance_rankBattles) values(NULL, ");
			writer.Write("'{0}', ", info.Name);
			writer.Write("'{0}', ", info.Tag);
			writer.Write("'{0}', ", info.Motto);
			writer.Write("'{0}', ", info.Ranking);
			writer.Write("'{0}'); select LAST_INSERT_ID()", info.RankingBattles);
			
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = writer.ToString();
				
				object obj = cmd.ExecuteScalar();
				
				Log.log(cmd.CommandText);
				Log.log("Result: {0}", obj);
				
				if( obj != null ) {
					return int.Parse(obj.ToString());
				} else {
					return 0;
				}
				
			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.ToString());
				Console.WriteLine("create alliance: " + e.ToString());
			} finally {
				conn.Close();
			}
			
			return 0;
		}

		#endregion Base Implementation
		
	};

}
