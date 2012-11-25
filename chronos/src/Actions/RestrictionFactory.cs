// created on 11/14/2004 at 8:26 AM

using Chronos.Core;
using Chronos.Utils;
using Chronos.Exceptions;
using System.Collections;
using DesignPatterns.Attributes;

namespace Chronos.Actions {

	/// <summary>Factory da acção 'restriction'</summary>
	[FactoryKey("restriction")]
	public class RestrictionFactory : ActionFactory {
	
		#region Static Members
	
		private static char[] Separator = new char[] { '.' };
	
		#endregion
	
		/// <summary>
		///  Cria Restriction's
		/// </summary>
		protected override Action createAction( Hashtable args )
		{
			object type = args["type"];
			if( null == type ) {
				throw new LoaderException("Can't find 'type' ammong 'restriction' arguments");
			}
			
			if( !Restriction.Restrictions.Contains(type) ) {
				throw new LoaderException("Don't know how to handle restriction type '"+type+"'");
			}
			
			string[] res = separate(args, "resource");
			string arg2 = args["value"].ToString();
			Restriction restriction = null;
			
			if( MathUtils.isInt(arg2) ) {
				restriction = new Restriction(res[0], res[1], type.ToString(), int.Parse(arg2));
			} else {
				string[] other = separate(args, "value");
				restriction = new Restriction( res[0], res[1], type.ToString(), other[0], other[1] );
			}
			
			return restriction;
		}
		
		/// <summary>Separa a categoria do recurso</summary>
		private string[] separate( Hashtable args, string att )
		{
			if( ! args.Contains(att) ) {
				throw new LoaderException("Can't find '"+att+"' ammong 'restriction' arguments");
			}
		
			string rawResource = args[att].ToString();
			
			string[] rawResourceData = rawResource.Split( Separator );
			if( rawResourceData.Length != 2 ) {
				throw new LoaderException("Can't read attribute '"+att+"'. Syntax: 'Category.Resource', but I got '" + rawResource + "'");
			}
			return rawResourceData;
		}
	
	}
}
