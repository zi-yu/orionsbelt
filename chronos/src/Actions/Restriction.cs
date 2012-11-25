// created on 11/14/2004 at 8:04 AM

using System.Collections;
using Chronos.Resources;

namespace Chronos.Actions {

	/// <summary>Responsavel pela tag 'restriction'</summary>
	public class Restriction : Action {
	
		#region Instance Fields
	
		protected string resourceCategorySource;
		protected string resourceSource;
		protected string type;
		protected string resourceCategoryCmp;
		protected string resourceCmp;
		protected int quantity;
		
		private delegate bool RestrictionAction( int source, int other );
		
		#endregion
		
		#region Static Members
		
		private static Hashtable restrictions = new Hashtable();
		
		static Restriction()
		{
			restrictions.Add("less-than", new RestrictionAction(lessThan) );
			restrictions.Add("bigger-than", new RestrictionAction(biggerThan) );
			restrictions.Add("equal-to", new RestrictionAction(equalTo) );
		}
		
		public static Hashtable Restrictions {
			get { return restrictions; }
		}
		
		#endregion
		
		#region Ctor
	
		/// <summary>Construtor</summary>
		public Restriction(
			string _resourceCategorySource,
			string _resourceSource,
			string _type,
			string _resourceCategoryCmp,
			string _resourceCmp  )
		{
			resourceCategorySource = _resourceCategorySource;
			resourceSource = _resourceSource;
			type = _type;
			resourceCategoryCmp = _resourceCategoryCmp;
			resourceCmp = _resourceCmp;
		}
		
		/// <summary>Construtor</summary>
		public Restriction(
			string _resourceCategorySource,
			string _resourceSource,
			string _type,
			int _quantity  )
		{
			resourceCategorySource = _resourceCategorySource;
			resourceSource = _resourceSource;
			type = _type;
			quantity = _quantity;
			resourceCategoryCmp = null;
			resourceCmp = null;
		}
		
		#endregion
		
		#region Restriction Methods
		
		/// <summary>Indica se uma quantidade é menor que outra</summary>
		private static bool lessThan( int source, int other )
		{
			return source < other;
		}
		
		/// <summary>Indica se uma quantidade é maior que outra</summary>
		private static bool biggerThan( int source, int other )
		{
			return source > other;
		}
		
		/// <summary>Indica se uma quantidade é menor que outra</summary>
		private static bool equalTo( int source, int other )
		{
			return source == other;
		}
		
		#endregion
		
		#region Core Action Methods
		
		/// <summary>Retorna true se esta Action puder ser efectuada</summary>
		public override bool evaluate( IResourceManager manager )
		{
			int sourceCount = manager.getResourceCount(resourceCategorySource, resourceSource);
			int otherCount = GetOtherValue(manager);
			
			RestrictionAction action = (RestrictionAction) restrictions[type];
			
			return action(sourceCount, otherCount);
		}
		
		/// <summary>Efectua a acao correspondente a esta Action</summary>
		public override bool action( IResourceManager manager )
		{
			return evaluate(manager);
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
			get{ return "restriction"; }
		}
		
		/// <summary>Indica um array com os campos importantes desta Action</summary>
		public override string[] getParams( int requestedQuantity )
		{
			string val = resourceCmp;
			if( resourceCategoryCmp == null || resourceCmp == null ) {
				val = quantity.ToString();
			}
			
			return new string[] {
					resourceSource, type, val
				};
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna uma string que idêntifica esta action</summary>
		public override string log ()
		{
			string val = resourceCmp;
			if( resourceCategoryCmp == null || resourceCmp == null ) {
				val = quantity.ToString();
			}
			
			return base.log() + "Restriction: " + resourceSource + " " + type + " " + val;
		}
		
		/// <summary>Indica o argumento 2 da restric��o</summary>
		private int GetOtherValue(IResourceManager manager)
		{
			if( resourceCategoryCmp == null || resourceCmp == null ) {
				return quantity;
			}
			return manager.getResourceCount(resourceCategoryCmp, resourceCmp);
		}
		
		#endregion
		
	};
	
}
