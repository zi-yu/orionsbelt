
using Chronos.Core;
using Chronos.Persistence;

namespace Chronos {

	/// <summary>
	/// Summary description for IPersistence.
	/// </summary>
	public interface IPersistence {
	
		/// <summary>Salva um Universo</summary>
		void save( Universe universe, PersistenceParameters parameters );

		/// <summary>Carrrega um Universo</summary>
		Universe load( PersistenceParameters parameters );
	
	};
}
