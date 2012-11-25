
//#define DEBUG_FILE_PERSISTENCE

using System.IO;
using Chronos.Core;
using Chronos.Utils;
using System.Threading;

namespace Chronos.Persistence {

	/// <summary>
	/// Summary description for XmlPersistence.
	/// </summary>
	public class FilePersistence : UniverseSerializer {

		#region UniverseSerializer Implementation
		
		public override void save( Universe universe, PersistenceParameters parameters )
		{
			string path = GetPath(parameters);
			string file = GetFile(parameters);
			
			string tempFile = Path.Combine(path, "tmp.bin");
			FileStream stream = null;
			
			using( stream = new FileStream(tempFile, FileMode.Create) ) {
				Log.log("Saving universe...");
				Formatter.Serialize(stream, universe);
				StreamSize = stream.Length;
				
				System.IO.File.Copy(tempFile, file, true);
				System.IO.File.Copy(tempFile, Path.Combine(GetPath(parameters), Universe.instance.TurnCount+".bin"), true);
			}
			
			System.IO.File.Delete(tempFile);
			
			Log.log("Tmp File: {0}", tempFile);
			Log.log("Universe File: {0}", file);
			Log.log("Backup File: {0}", Universe.instance.TurnCount+".bin");
		
			int toDelete = Universe.instance.TurnCount - 10;
			string pathToDelete = Path.Combine(GetPath(parameters), toDelete+".bin");
			
			if( System.IO.File.Exists(pathToDelete) ) {
				System.IO.File.Delete(pathToDelete);
			}
			Log.log("...Done!");
		}

		/// <summary>Armazena uma Stream com persistncia</summary>
		protected override void saveData( byte[] data, PersistenceParameters parameters )
		{
			FileStream fileStream = new FileStream(GetFile(parameters), FileMode.Create);

			fileStream.Write( data, 0, data.Length );
			fileStream.Close();

			fileStream = new FileStream(Path.Combine(GetPath(parameters), Universe.instance.TurnCount+".bin"), FileMode.Create);
			fileStream.Write( data, 0, data.Length );
			fileStream.Close();
			
			int toDelete = Universe.instance.TurnCount - 10;
			string pathToDelete = Path.Combine(GetPath(parameters), toDelete+".bin");

#if DEBUG_FILE_PERSISTENCE
			Log.log("---- FILE PERSISTENCE DEBUG INFO ----------------");
			Log.log("To delete : {0}", pathToDelete);
			Log.log("Exists    : {0}", System.IO.File.Exists(pathToDelete));
			Log.log("-------------------------------------------------");
#endif
			
			if( System.IO.File.Exists(pathToDelete) ) {
				System.IO.File.Delete(pathToDelete);
			}
		}

		/// <summary>Carrega uma Stream com um Universo</summary>
		protected override Stream loadData(PersistenceParameters parameters){
			try {
				return getData(parameters);
			} catch {
				Thread.Sleep(1500);
				try {
					return getData(parameters);
				} catch {
					return null;
				}
			}
		}
		
		/// <summary>Carrega uma Stream com um Universo</summary>
		private Stream getData(PersistenceParameters parameters)
		{
			return new FileStream(GetFile(parameters), FileMode.Open);
		}

		#endregion

		#region Instance Members

		private string file;
		private string path = string.Empty;

		/// <summary>Ctor</summary>
		public FilePersistence( string _file )
		{
			file = _file;
		}

		/// <summary>Indica o Ficheiro a usar</summary>
		public string File {
			get { return file; }
			set { file = value; }
		}
		
		/// <summary>Obtm o ficheiro</summary>
		public string GetFile( PersistenceParameters parameters )
		{
			if( parameters == null ) {
				return File;
			}
			string file = parameters.GetParameter("FilePersistence");
			if( file == null || file == string.Empty ) {
				return File;
			}
			return file;
		}

		public string GetPath( PersistenceParameters p) {
			if( p == null ) {
				return path;			
			}
			string tmp = p.GetParameter( "Path" );
			if( tmp != null ) {
				return tmp;
			}
			return path;
		}
	
		
		#endregion
			
	};
}
