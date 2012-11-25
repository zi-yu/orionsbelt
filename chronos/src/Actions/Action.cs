
using Chronos.Resources;

namespace Chronos.Actions {
	
	/// <summary>Class base das Action's</summary>
	public abstract class Action {
	
		#region Core Action Methods
	
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public abstract bool evaluate( IResourceManager planet );
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		/// <remarks>Pode não fazer sentido chamar este método. Se não houver o seu override
		///	por defeito ele vai chamar o evaluate( IResourceManager planet );. Quando este método
		///	é necessário, o 'repeatNumber' indica o múmero de vezes que accção se vai repetir
		/// </remarks>
		public virtual bool evaluate( IResourceManager planet, int repeatNumber )
		{
			return evaluate(planet);
		}
		
		/// <summary>Efectua a acaoo desta Action</summary>
		public abstract bool action( IResourceManager planet );
		
		/// <summary>Efectua a acaoo desta Action para uma determinada quantidade de recursos</summary>
		/// <remarks>Pode não fazer sentido chamar este método. Se não houver o seu override
		///	por defeito ele vai chamar o action( IResourceManager planet );. Quando este método
		///	é necessário, o 'repeatNumber' indica o múmero de vezes que accção se vai repetir
		/// </remarks>
		public virtual bool action( IResourceManager planet, int repeatNumber )
		{
			return action(planet);
		}
		
		/// <summary>Anula o efeito do action</summary>
		public abstract bool undo( IResourceManager manager );
		
		/// <summary>Anula o efeito do action</summary>
		public virtual bool undo( IResourceManager manager, int repeatNumber )
		{
			return undo(manager);
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna o nome desta Action</summary>
		public abstract string Name {get;}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public abstract string[] getParams( int requestedQuantity );
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public string[] Params {
			get {
				return getParams(1);
			}
		}
		
		#endregion
		
		#region UtilityMethods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public virtual string log ()
		{
			return "[" + Name + "] "; 
		}
		
		#endregion
	
	};
	
}
