
using System;
using System.Data;
using System.Web;
using System.Collections;
using Alnitak.News;
using Alnitak.Mail;
using Chronos.Core;
using Chronos.Info;

namespace Alnitak {
	
	public abstract class ScanUtility {
		
		#region Static Region
		
		/// <summary>Indica o driver que sabe persistir notcias</summary>
		public static ScanUtility Persistence {
			get {
				string value = OrionGlobals.getConfigurationValue("utilities",getKey());
				
				return (ScanUtility)Activator.CreateInstance( Type.GetType( value , true) );
			}
		}
		
		public static string getKey()
		{
			return OrionGlobals.resolveDataAccessName("scanUtility");
		}
		
		#endregion
		
		#region Abstract Methods
		
		/// <summary>Regista um scan</summary>
		public abstract void Register( Scan scan );
		
		/// <summary>Obtm todos os scans de um planeta</summary>
		public abstract Scan[] GetScans( Planet scanner );
		
		/// <summary>Obtm todos os scans de um jogador em todos os planetas</summary>
		public abstract Scan[] GetScans( Ruler ruler );

		/// <summary>Obtm um scan dado o seu id</summary>
		public abstract Scan GetScan( int id );
		
		#endregion
		
		#region Utilities
		
		/// <summary>Cria um array de Scans com base num DataSet</summary>
		/// <remarks>
		/// 	Tem em conta um dataset com os campos na seguinte ordem:
		///		Id,	0
		/// 	SourcePlanetId,	1
		/// 	TargetPlanetId,	2
		/// 	ScanLevel,	3
		/// 	Intercepted,	4
		/// 	Succeded,	5
		/// 	Turn 		6
		///		Culture 	7
		///		HasCommsSatellite 	8
		///		HasGate 	9
		///		HasStarGate 	10
		///		HasStarPort 	11
		///		InBattle 	12
		///		FleetCount 13
		///		TargetPlanetOwner 14
		///		TotalShips 15
		/// </remarks>
		virtual public Scan[] DataSetToScans( DataSet ds )
		{
			ArrayList list = new ArrayList();
			
			foreach( DataRow row in ds.Tables[0].Rows ) {
				Scan scan = new Scan();
				scan.Id = (int) row[0];
				scan.SourcePlanetId = (int) row[1];
				scan.Target = Coordinate.translateCoordinate(row[2].ToString());
				scan.ScanLevel = (int) row[3];
				scan.Intercepted = GetBool(row[4]);
				scan.Success = GetBool(row[5]);
				scan.Turn = (int)row[6];
				scan.Culture = (int) row[7];
				scan.HasCommsSatellite = GetBool(row[8]);
				scan.HasGate = GetBool(row[9]);
				scan.HasStarGate = GetBool(row[10]);
				scan.HasStarPort = GetBool(row[11]);
				scan.InBattle = GetBool(row[12]);
				scan.NumberOfFleets = (int) row[13];
				scan.TargetPlanetOwner = (int) row[14];
				scan.TotalShips = (int) row[15];
				list.Add(scan);
			}
			
			return (Scan[]) list.ToArray(typeof(Scan));
		}

		/// <summary>Retorna um bool dado um campo</summary>
		public bool GetBool(object obj)
		{
			if( obj == null ) {
				return false;
			}
			if( obj.GetType() == typeof(bool) ) {
				return (bool) obj;
			}
			if( obj.GetType() == typeof(int) ) {
				int i = (int) obj;
				if( i == 0 ) {
					return false;
				}
				return true;
			}
			return true;
		}
		
		#endregion
		
	};
	
}
