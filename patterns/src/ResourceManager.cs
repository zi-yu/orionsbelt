// created on 1/24/04 at 10:14 a

//----------------------------------------------------------------------------
// This file is part of the Resource Manager Design Pattern library.
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
using System.Collections;

namespace DesignPatterns {
	
	/// <summary>Manages resources.</summary>
	public abstract class ResourceManager : Hashtable {
		
		/// <summary>Creates a ResourceManager</summary>
		protected ResourceManager()
		{
		}

		/// <summary>
		/// Gets the specified resource.
		/// </sumary>
		/// <remarks>
		/// If the resource does not exist, then its created.
		/// </remarks>
		/// <returns>The resource.</returns>
		/// <param name="key">Resource key.</param>
		public object GetResource( string key )
		{
			if( ContainsKey(key) ) 
				return this[key];
			
			object newObject = CreateResource(key);
				
			if( newObject == null )
				return null;
				
			Add(key, newObject);
			return newObject;
		}
		
		/// <summary>Creates a resource.</summary>
		/// <param name="key">Resource key.</param>
		/// <returns>The new Resource.</returns>
		public abstract object CreateResource( string key );
		
	
	};
	
}
