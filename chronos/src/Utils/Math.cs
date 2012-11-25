// created on 5/25/04 at 8:54 a

using System;
using System.Text.RegularExpressions;

namespace Chronos.Utils {

	public abstract class MathUtils {
	
		#region Private Fields
		
		private static Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));
		private static Regex IsInt = new Regex(@"^-?\d+$", RegexOptions.Compiled);
		
		#endregion
		
		#region Properties
		
		/// <summary>Retorna um número aleatório entre 0 e int.MaxValue</summary>
		public static int Random {
			get {
				return rdm.Next();
			}
		}
		
		#endregion
	
		#region Utility Methods
	
		/// <summary>Retorna um número aleatório entre dois limites</summary>
		public static int random( int start, int end )
		{
			return rdm.Next(start, end);
		}
		
		/// <summary>Retorna um número entre 0 e um limite</summary>
		public static int random( int lim )
		{
			return rdm.Next(lim);
		}
		
		/// <summary>Retorna um inteiro arredondado</summary>
		public static int round( float num )
		{
			return (int) Math.Round(num);
		}
		
		/// <summary>Retorna um inteiro arredondado</summary>
		public static int round( double num )
		{
			return (int) Math.Round(num);
		}
		
		/// <summary>Indica se uma string  um inteiro</summary>
		public static bool isInt( string str )
		{
			if( str == null ) {
				return false;
			}
			return IsInt.Match(str).Success;
		}
	
		#endregion
	
	};

}
