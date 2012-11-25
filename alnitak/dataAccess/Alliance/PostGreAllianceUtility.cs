// created on 3/20/2006 at 10:19 AM

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Data;
using Alnitak.PostGre;
using Chronos.Utils;
using NpgsqlTypes;
	
namespace Alnitak {

	public class PostGreAllianceUtility : AllianceUtility {
	
		#region Utilities
	
		
		#endregion Utilities
		
		#region Base Implementation
		
		public override ArrayList GetAll(){
			DataSet alliances = PostGreServerUtility.getAllFromDB( "OrionsBelt_AllianceGetAlliance" );
			return FromDataSet(alliances);
		}
		
		public override AllianceInfo Get( int id ) {
			PostGre.PostGreParam [] param = new PostGreParam[1];
			param[0] = new PostGreParam( id ,NpgsqlDbType.Integer );

			DataSet alliances = PostGreServerUtility.getFromDB("OrionsBelt_AllianceGetAllianceById", param );

			ArrayList allianceArray = FromDataSet(alliances);

			if( allianceArray.Count > 0 ) {
				return (AllianceInfo)allianceArray[0];

			}

			return null;
		}
		
		public override void Save( AllianceInfo info )
		{
			PostGre.PostGreParam [] param = new PostGreParam[6];
			param[0] = new PostGreParam( info.Name ,NpgsqlDbType.Varchar,150 );
			param[1] = new PostGreParam( info.Tag ,NpgsqlDbType.Varchar,150 );
			param[2] = new PostGreParam( info.Motto ,NpgsqlDbType.Varchar,150 );
			param[3] = new PostGreParam( info.Ranking ,NpgsqlDbType.Integer );
			param[4] = new PostGreParam( info.RankingBattles ,NpgsqlDbType.Integer );
			param[5] = new PostGreParam( info.Id ,NpgsqlDbType.Integer );
		
			PostGreServerUtility.executeNonQuery2("orionsbelt_alliancesavealliance", param );
		}
		
		public override int Register( AllianceInfo info ) {
			PostGre.PostGreParam [] param = new PostGreParam[5];
			param[0] = new PostGreParam( info.Name ,NpgsqlDbType.Varchar,150 );
			param[1] = new PostGreParam( info.Tag ,NpgsqlDbType.Varchar,150 );
			param[2] = new PostGreParam( info.Motto ,NpgsqlDbType.Varchar,150 );
			param[3] = new PostGreParam( info.Ranking ,NpgsqlDbType.Integer );
			param[4] = new PostGreParam( info.RankingBattles ,NpgsqlDbType.Integer );
			
			DataSet ds = PostGreServerUtility.getFromDB("OrionsBelt_AllianceRegisterAlliance", param );
			return int.Parse(ds.Tables[0].Rows[0][0].ToString());
		}

		#endregion Base Implementation
		
	};

}
