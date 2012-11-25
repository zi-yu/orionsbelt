
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

	/// <summary>Avoid <see cref="DesignPatterns.Factory">Factory</see>'s auto registration.</summary>
	/// <example>
	/// <code>
	/// [ NoAutoRegister() ]
	/// public class LaterFactory : IFactory {
	/// 	public virtual object create() 
	/// 	{
	/// 		return "I will not be auto registred!";
	/// 	}
	/// };
	/// </code>
	/// </example>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class NoAutoRegisterAttribute : FactoryAttribute {
	
		/// <summary>Initializes this attribute.</summary>
		public NoAutoRegisterAttribute()
		{
		}
		
	};

}
