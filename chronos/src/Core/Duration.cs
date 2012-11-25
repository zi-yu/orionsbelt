// created on 6/8/2004 at 9:07 AM

namespace Chronos.Core {

	/// <summary>Representa a duração de construcção de um recurso</summary>
	public class Duration {

		#region Instance Fields

		public int duration;
		public int quantity;
		public string dependency;
		public string dependecyFunction;

		#endregion

		#region Instance Ctors

		/// <summary>Construtor público</summary>
		public Duration( int duration ) : this(duration, -1, null, null)
		{
		}
	
		/// <summary>Construtor público</summary>
		public Duration( int duration, int quantity ) : this(duration, quantity, null, null)
		{
		}
		
		/// <summary>Construtor público</summary>
		public Duration( int duration, string dependency, string func ) : this(duration, -1, dependency, func)
		{
		}
		
		/// <summary>Construtor público</summary>
		public Duration( int _duration, int _quantity, string _dependency, string _dependencyFuncion )
		{
			duration = _duration;
			quantity = _quantity;
			dependency = _dependency;
			dependecyFunction = _dependencyFuncion;
		}
		
		#endregion
		
		#region Information Properties
		
		/// <summary>Indica se há uma quantidade associada a esta duração</summary>
		public bool HasQuantity {
			get { return quantity != -1; }
		}
		
		/// <summary>Indica se há uma dependência associada a esta duração</summary>
		public bool HasDependency {
			get { return dependency != null; }
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica a duração</summary>
		public int Value {
			get { return duration; }
		}
		
		/// <summary>Indica a quantidade</summary>
		public int Quantity {
			get { return quantity; }
		}
		
		/// <summary>Indica a dependência</summary>
		public string Dependecy {
			get { return dependency; }
		}	
		
		/// <summary>Indica a duração</summary>
		public string DependencyFunction {
			get { return dependecyFunction; }
		}
		
		#endregion
			
	};
};