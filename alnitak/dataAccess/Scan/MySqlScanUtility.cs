using System;
using System.IO;
using System.Data;
using Alnitak.News;
using Chronos.Core;
using Chronos.Info;

namespace Alnitak {
	
	public class MySqlScanUtility : ScanUtility {
		
		#region NewsUtility Implementation
		
		/// <summary>Regista um scan</summary>
		public override void Register( Scan scan )
		{
			string script = string.Format("INSERT INTO Scans VALUES('', {0}, '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14} )",
									scan.SourcePlanetId, scan.Target.ToString(), scan.ScanLevel,
									scan.Intercepted?1:0, scan.Success?1:0,
									scan.Turn,
									scan.Culture, scan.HasCommsSatellite, scan.HasGate,
									scan.HasStarGate, scan.HasStarPort, scan.InBattle, scan.NumberOfFleets,
									scan.TargetPlanetOwner,
									scan.TotalShips
								);

			Chronos.Utils.Log.log(script);
			MySqlUtility.executeNonQuery(script);
		}
		
		/// <summary>Obtm todos os scans de um jogador em todos os planetas</summary>
		public override Scan[] GetScans( Ruler ruler )
		{
			StringWriter writer = new StringWriter();
			
			writer.Write("SELECT * FROM Scans WHERE SourcePlanetId={0}", ruler.Planets[0].Id);
			for( int i = 1; i < ruler.Planets.Length; ++i ) {
				writer.Write(" or SourcePlanetId={0} ", ruler.Planets[i].Id);
			}
			writer.Write(" ORDER BY Id DESC LIMIT 0, 60");
			
			DataSet ds = MySqlUtility.getQuery(writer.ToString());
			return DataSetToScans(ds);
		}
		
		/// <summary>Obtm todos os scans de um planeta</summary>
		public override Scan[] GetScans( Planet scanner )
		{
			string query = string.Format("SELECT * FROM Scans WHERE SourcePlanetId={0} ORDER BY Id DESC LIMIT 0, 40", scanner.Id);
			DataSet ds = MySqlUtility.getQuery(query);
			return DataSetToScans(ds);
		}
		
		/// <summary>Obtm um scan dado o seu id</summary>
		public override Scan GetScan( int id )
		{
			string query = string.Format("SELECT * FROM Scans WHERE Id={0}", id);
			DataSet ds = MySqlUtility.getQuery(query);
			if( ds.Tables[0].Rows.Count == 0 ) {
				return null;
			}
			return DataSetToScans(ds)[0];
		}
		
		#endregion
		
	};
	
}
