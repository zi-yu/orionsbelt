//----------------------------------------------------------------------------
// This file is part of the Factory Design Pattern library.
// Author: 	Pedro Santos
// Contact:	pre@users.sourceforge.net | www.psantos.net
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//----------------------------------------------------------------------------

using System;

namespace DesignPatterns.Attributes {

	/// <summary>Associates a custom key name to a class.</summary>
	/// <example>
	/// <code>
	/// [ FactoryKey( "Hellos" ) ]
	/// public class HelloFactory : IFactory {
	/// 	public virtual object create() 
	/// 	{
	/// 		return "Hello";
	/// 	}
	/// };
	/// </code>
	/// </example>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class FactoryKeyAttribute : FactoryAttribute {
	
		private string _key;
	
		/// <summary>Creates the FactoryKey attribute.</summary>
		/// <param name="key">Key name</param>
		public FactoryKeyAttribute(string key)
		{
			_key = key;
		}
		
		/// <summary>Gets the key.</summary>
		/// <value>Gets the key.</value>
		public string Key 
		{
			get { return _key; }
		}
		
		/// <summary>Gets the key.</summary>
		/// <returns>The key name.</returns>
		public string getKey()
		{
			return Key;
		}
		
	};

}
