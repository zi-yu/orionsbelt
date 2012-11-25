using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Alnitak.PostGre;
using Chronos.Core;
using Chronos.Info;
using NpgsqlTypes;

namespace Alnitak {
	
	public class PostGreScanUtility : ScanUtility {

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
			Scan[] list = null;
			
			int i = -1;
			foreach( DataRow row in ds.Tables[0].Rows ) {
				if(row[0] is System.DBNull ) {
					continue;
				} 
				if( list == null ) {
					list = new Scan[ds.Tables[0].Rows.Count];
				}
				Scan scan = ConvertBinaryToScan( row[1] );
				scan.Id = (int)row[0];
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

			PostGre.PostGreParam [] parameters = new PostGreParam[2];
			parameters[0] = new PostGreParam(scan.SourcePlanetId,NpgsqlDbType.Integer);
			parameters[1] = new PostGreParam(data,NpgsqlDbType.Bytea);

			PostGreServerUtility.executeNonQuery2("OrionsBelt_InsertScans",parameters);
		}
		
		/// <summary>Obtm todos os scans de um planeta</summary>
		public override Scan[] GetScans( Planet scanner ) {
			//DataSet ds = PostGreServerUtility.getFromDB("OrionsBelt_GetScansByPlanetId", parameters );

			string query = string.Format("SELECT scans_id,scans_data FROM OrionsBelt_Scans WHERE scans_sourcePlanetId={0} ORDER BY scans_id DESC LIMIT 40;",scanner.Id.ToString());
			DataSet ds = PostGreServerUtility.getFromDBWithQuery(query );

			return DataSetToScans(ds);
		}
		
		/// <summary>Obtém todos os scans de um jogador em todos os planetas</summary>
		public override Scan[] GetScans( Ruler ruler ) {
			StringWriter writer = new StringWriter();
			
			writer.Write("scans_sourcePlanetId={0}", ruler.Planets[0].Id);
			for( int i = 1; i < ruler.Planets.Length; ++i ) {
				writer.Write(" or scans_sourcePlanetId={0} ", ruler.Planets[i].Id);
			}

			string query = string.Format("SELECT scans_id,scans_data FROM OrionsBelt_Scans WHERE {0} LIMIT {1}", writer.ToString( ),40);
			DataSet ds = PostGreServerUtility.getFromDBWithQuery(query );
			
			return DataSetToScans(ds);
		}
		
		/// <summary>Obtm um scan dado o seu id</summary>
		public override Scan GetScan( int id ) {
			string query = string.Format("SELECT scans_id,scans_data FROM OrionsBelt_Scans WHERE scans_id = {0} LIMIT 10;",id.ToString());
			DataSet ds = PostGreServerUtility.getFromDBWithQuery(query );

			return DataSetToScans(ds)[0];
		}
		
		#endregion
		
	};
	
}
