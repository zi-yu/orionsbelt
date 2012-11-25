// created on 9/11/2005 at 12:32 PM

// created on 2/29/04 at 9:48 a

using Chronos.Resources;
using Chronos.Utils;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'reserve-resource'</summary>
	public class ReserveResource : ResourceNeeded, IUndoAfterBuild {
	
		#region Ctors
	
		/// <summary>Construtor</summary>
		public ReserveResource( string intrinsic, string quantity )
			: base( intrinsic, quantity )
		{
		}
		
		#endregion

	};
	
}

