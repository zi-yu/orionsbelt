using System;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

using Alnitak.News;
using Chronos.Core;
using Chronos.Info;
using System.IO;


using Alnitak.MsSqlServer;
using System.Collections;


namespace Alnitak {
	
	public class SqlServerScanUtility : ScanUtility {

		#region Fields

		private BinaryFormatter formatter = new BinaryFormatter();

		#endregion
		
		#region Private

		/// <summary>
		/// converte um array de bytes para um objecto do tipo Scan
		/// </summary>
		/// <param name="o">array de Bytes</param>
		/// <returns>Objectio do tipo Scan</returns>
		private Scan ConvertBinaryToScan( object o ) {
			MemoryStream stream = new MemoryStream( (byte[])o );
			return (Scan)formatter.Deserialize(stream);
		}

		#endregion

		#region Overrides 

		/// <summary>
		/// converte um conjunto de rows em binrio para um array de objectos do tipo scan;
		/// </summary>
		/// <param name="ds">DataSet com as Rows</param>
		/// <returns>Array com todos os Scans</returns>
		public override Scan[] DataSetToScans(DataSet ds) {
			Scan[] list = new Scan[ds.Tables[0].Rows.Count];
			
			int i = -1;
			foreach( DataRow row in ds.Tables[0].Rows ) {
				Scan scan = ConvertBinaryToScan( row["scans_data"] );
				scan.Id = (int)row["scans_id"];
				list[++i] = scan;
			}
			
			return list;
		}

		#endregion
		
		#region ScanUtility Implementation
		
		/// <summary>Regista um scan</summary>
		public override void Register( Scan scan ) {
			MemoryStream stream = new MemoryStream();

			formatter.Serialize(stream, scan);
			byte[] data = stream.ToArray();

			Hashtable parameters = new Hashtable();
			parameters.Add("@sourcePlanetId",scan.SourcePlanetId);
			parameters.Add("@data",data);

			SqlServerUtility.executeNonQuery("OrionsBelt_InsertScans",parameters);
		}
		
		/// <summary>Obtm todos os scans de um planeta</summary>
		public override Scan[] GetScans( Planet scanner ) {
			Hashtable parameters = new Hashtable();
			parameters.Add("@targetPlanetOwner", scanner.Id );

			DataSet ds = SqlServerUtility.getFromDB("OrionsBelt_GetScansByPlanetId", parameters );
		
			return DataSetToScans(ds);
		}
		
		/// <summary>Obtém todos os scans de um jogador em todos os planetas</summary>
		public override Scan[] GetScans( Ruler ruler ) {
			StringWriter writer = new StringWriter();
			
			writer.Write("scans_sourcePlanetId={0}", ruler.Planets[0].Id);
			for( int i = 1; i < ruler.Planets.Length; ++i ) {
				writer.Write(" or scans_sourcePlanetId={0} ", ruler.Planets[i].Id);
			}

			Hashtable parameters = new Hashtable();
			parameters.Add("@count", 40 );
			parameters.Add("@types", writer.ToString( ) );

			DataSet ds = SqlServerUtility.getFromDB("OrionsBelt_GetScans", parameters );
		
			return DataSetToScans(ds);
		}
		
		/// <summary>Obtm um scan dado o seu id</summary>
		public override Scan GetScan( int id ) {
			Hashtable parameters = new Hashtable();
			parameters.Add("@id", id );

			DataSet ds = SqlServerUtility.getFromDB("OrionsBelt_GetScansById", parameters );
		
			return DataSetToScans(ds)[0];
		}
		
		#endregion
		
	};
	
}
