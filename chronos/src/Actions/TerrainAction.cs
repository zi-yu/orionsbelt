// created on 8/10/2005 at 12:53 PM

using Chronos.Resources;
using Chronos.Core;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'terrain'</summary>
	public class TerrainAction : Action {
	
		#region Instance Fields
	
		protected Terrain terrain;
		
		#endregion
		
		#region Ctor
	
		/// <summary>Construtor</summary>
		public TerrainAction( Terrain _terrain )
		{
			terrain = _terrain;
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager manager )
		{
			Planet planet = (Planet) manager;
			return planet.Info.Terrain.Id == terrain.Id;
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager planet )
		{
			return true;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			return true;
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna "resource-ref"</summary>
		public override string Name {
			get{ return "terrainAction"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams(int requestedQuantity) 
		{
			return new string[] { terrain.Description };	
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			return base.log() + "Terrain: " + terrain.Description; 
		}
		
		#endregion
		
	};
	
}
