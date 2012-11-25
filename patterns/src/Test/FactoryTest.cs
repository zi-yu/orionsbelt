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
using DesignPatterns.Attributes;
using DesignPatterns.Exceptions;

namespace DesignPatterns.Test {
	
	class MyClass1 {
		/*public int a;
		public int b;		*/
	};
	class MyClass3 {};
	
	class MyClass2 {
		public MyClass2() {}
		public override string ToString()
		{
			return "hoba hoba";
		}
	};
	
	public class MyFactory : FactoryBase {
		public MyFactory(Type type)  : base(type) {}
	};
	
	public class MyClass1Factory : MyFactory {
		public MyClass1Factory() : base(typeof(MyClass1)) {}
	};
	
	[FactoryKey("NamedFactory")]
	public class MyClass2Factory : MyFactory {
		public MyClass2Factory() : base(typeof(MyClass2)) {}
	};
	
	[FactoryKey("Retarded Factory")]
	[NoAutoRegister()]
	public class MyClass3Factory : MyFactory {
		public MyClass3Factory() : base(typeof(MyClass3)) {}
	};

	public class FactoryTest {
	
		public static void Main( string[] args )
		{	
			try {
				Console.WriteLine();
				Assembly asm = Assembly.GetAssembly(typeof(FactoryContainer));
				Console.WriteLine(asm.FullName);
				
				Console.WriteLine();
				FactoryContainer factory = new FactoryContainer( typeof(MyFactory) );
				
				Console.WriteLine("Factories Registred:");
				foreach( string key in factory.Keys ) {
					Console.WriteLine("- <{0} , {1}>", key, factory.getFactory(key));
				}
				
				Console.WriteLine();
				Console.WriteLine("Objects:");
				foreach( string key in factory.Keys ) {
					Console.WriteLine("- <{0} , {1}>", key, factory.create(key) );
				}
				
				Console.WriteLine();
				Console.WriteLine("Register another factory");
				
				factory.registerFactory( typeof(MyClass3Factory) );
				
				foreach( string key in factory.Keys ) {
					Console.WriteLine("- <{0} , {1}>", key+"asd", factory.create(key+"asd") );
				}
				
				Console.WriteLine();
			} catch( FactoryException e ) {
				Console.WriteLine(e);
			}
		}
		
	};

}
