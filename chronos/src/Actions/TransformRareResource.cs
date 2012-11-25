// created on 8/10/2005 at 3:58 PM

using Chronos.Resources;
using Chronos.Core;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'transformRareResource'</summary>
	public class TransformRareResource : Action {
	
		#region Instance Fields
	
		protected string _rare;
		protected string _intrinsic;
		protected int _factor;
		
		#endregion
		
		#region Ctor
	
		/// <summary>Construtor</summary>
		public TransformRareResource( string rare, string intrinsic, int factor )
		{
			_rare = rare;
			_intrinsic = intrinsic;
			_factor = factor;
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager manager )
		{
			return true;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager manager, int repeatNumber )
		{
			for( int i = 0; i < repeatNumber; ++i ) {
				action(manager);
			}
			return true;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager manager )
		{
			Planet planet = (Planet) manager;
			if( planet.getResourceCount("Rare", _rare) < 1 ) {
				return false;
			}
			planet.take("Rare", _rare, 1);
			return planet.addResource("Intrinsic", _intrinsic, _factor) != null;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			Planet planet = (Planet) manager;
			planet.addResource("Rare", _rare, 1);
			return planet.take("Intrinsic", _intrinsic, _factor);
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna "transformRareResource"</summary>
		public override string Name {
			get{ return "transformRareResource"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams(int requestedQuantity) 
		{
			return new string[] { _rare, _intrinsic, _factor.ToString() };	
		}
		
		public string Rare {
			get { return _rare; }
		}
		
		public string Intrinsic {
			get { return _intrinsic; }
		}
		
		public int Factor {
			get { return _factor; }
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			return base.log() + "Convert 1 '"+_rare+"' in "+_factor+" of '"+_intrinsic+"'"; 
		}
		
		#endregion
		
	};
	
}
