// created on 13-01-2004 at 12:39

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

namespace DesignPatterns.Exceptions {
	
	/// <summary>Cannot create an instance of a value type.</summary>
	/// <remarks>
	/// This exception is thown when the Factory trys to create 
	/// a value type.
	/// </remarks>
	public class ValueTypeException : FactoryException {
		
		/// <summary>Creates the ValueTypeException.</summary>
		/// <param name="type">Value type class.</param>
		public ValueTypeException(Type type) 
			: base("Cannot create an instance of a value type ("+type.Name+")")
		{
		}
		
	};
	
}
