// created on 9/6/2005 at 6:07 PM

using Chronos.Resources;
using Chronos.Core;

namespace Chronos.Actions {
	
	/// <summary>Responsavel pela tag 'transform'</summary>
	public class Transform : Action {
	
		#region Instance Fields
	
		protected string _input;
		protected string _output;
		protected int _factor;
		
		#endregion
		
		#region Ctor
	
		/// <summary>Construtor</summary>
		public Transform( string input, string output, int factor )
		{
			_input = input;
			_output = output;
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
			
			int ammount = planet.getResourceCount("Intrinsic", _input);
			if( ammount >= _factor ) {
				ammount = _factor;
			} 
		
			if( ammount <= 0 ) {
				return true;
			}
			
			planet.take("Intrinsic", _input, ammount);
			return planet.addResource("Intrinsic", _output, ammount) != null;
		}
		
		/// <summary>Anula o efeito do action</summary>
		public override bool undo( IResourceManager manager )
		{
			Planet planet = (Planet) manager;
			
			int ammount = planet.getResourceCount("Intrinsic", _input);
			if( ammount >= _factor ) {
				ammount = _factor;
			}
			
			if( ammount <= 0 ) {
				return true;
			}
			
			planet.addResource("Intrinsic", _input, ammount);
			return planet.take("Intrinsic", _output, ammount);
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Retorna "transformRareResource"</summary>
		public override string Name {
			get{ return "transform"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams(int requestedQuantity) 
		{
			return new string[] { _input, _output, _factor.ToString() };	
		}
		
		public string Input {
			get { return _input; }
		}
		
		public string Output {
			get { return _output; }
		}
		
		public int Factor {
			get { return _factor; }
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			return base.log() + "Convert '"+_input+"' in "+_factor+" of '"+_output+"'"; 
		}
		
		#endregion
		
	};
	
}
