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
using System.Reflection;

namespace DesignPatterns {

	/// <summary>
	/// Simple base <see cref="DesignPatterns.Factory">Factory</see> class. Inherit from 
	/// FactoryBase if you wish simple object factories. </summary>
	 /// <example><code>
	/// public MyFactory : FactoryBase {
	/// 	public MyFactory() : base( typeof(MyClass) ) {}
	/// }
	/// </code>Calling MyFactory create method, will create an instance of MyClass.</example>
	/// <remarks>FactoryBase will only create instances of classes that have default ctor.</remarks>
	/// <seealso cref="DesignPatterns.Factory">Factory class</seealso>
	public abstract class FactoryBase : IFactory {
	
		private Type _type;
	
		/// <summary>
		/// Creates a new FactoryBase.
		/// </summary>
		/// <param name="type">This factory will create instances of type.</param> 
		protected FactoryBase( Type type )
		{
			_type = type;
		}
	
		/// <summary>Creates an instance of this Factory type.</summary>
		public virtual object create( object args )
		{
			if( _type != null ) {
				return createInstance(_type);
			}
			return null;
		}
		
		/// <summary>Creates an instance of a type.</summary>
		/// <param name="type">Type of the class to create the instance.</param>
		/// <returns>A new object.</returns>
		/// <remarks>This method will only create instances of classes that 
		/// have default ctor. Override if you wish to use another mechanism.</remarks>
		/// <exception cref="DesignPatterns.Exceptions.ValueTypeException">
		/// If 'type' is a value type.
		/// </exception>
		/// <exception cref="DesignPatterns.Exceptions.CannotCreateException">
		/// If 'type' has no default ctor.
		/// </exception>
		public virtual object createInstance( Type type )
		{
			return FactoryContainer.createInstance(type);
		}
		
	};

}
