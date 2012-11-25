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
using System.Collections;
using DesignPatterns.Attributes;
using DesignPatterns.Exceptions;

namespace DesignPatterns {

	/// <summary>Factory class.</summary>
	/// <remarks>This class gathers a set of IFactory's with an associated key (string).
	///  It will search for every <see cref="DesignPatterns.Factory.FactoryType">FactoryType</see>
	///  derived class in the <see cref="DesignPatterns.Factory.FactoryType">FactoryType</see>'s
	///  Assembly and will register it in the factory. The key will be the name of the
	///  factory, bu you can choose a key using the 
	///  <see cref="DesignPatterns.Attributes.FactoryKeyAttribute">Factorykey</see> attribute.<p/>
	/// You can also use the attribute <see cref="DesignPatterns.Attributes.NoAutoRegisterAttribute">NoAutoRegister</see>.
	/// This way the Factory will not auto register that factory, and you can add it later on
	/// using the <see cref="DesignPatterns.Factory.registerFactory">registerFactory</see> methods.
	/// </remarks>
	/// <example>
	/// <code>
	/// class MyClass1 {};
	/// class MyClass3 {};
	/// 
	///class MyClass2 {
	///	public MyClass2() {}
	///	public override string ToString()
	///	{
	///		return "hoba hoba";
	///	}
	///};
	///
	///public class MyFactory : FactoryBase {
	///	public MyFactory(Type type)  : base(type) {}
	///};
	///
	///public class MyClass1Factory : MyFactory {
	///	public MyClass1Factory() : base(typeof(MyClass1)) {}
	///};
	///
	///[FactoryKey("NamedFactory")]
	///public class MyClass2Factory : MyFactory {
	///	public MyClass2Factory() : base(typeof(MyClass2)) {}
	///};
	///
///[FactoryKey("Retarded Factory")]
	///[NoAutoRegister()]
///	public class MyClass3Factory : MyFactory {
	///	public MyClass3Factory() : base(typeof(MyClass3)) {}
	///};
	/// 
///public class FactoryTest {
	///
	///	public static void Main( string[] args )
	///	{	
	///		Console.WriteLine();
	///		<b>Factory factory = new Factory( typeof(MyFactory) );</b>
	///		
	///		Console.WriteLine("Factories Registred:");
	///
	///		foreach( string key in factory.Keys ) {
	///			Console.WriteLine("- {0} , {1}", key, factory.getFactory(key));
	///		}
	///		
	///		Console.WriteLine();
	///		Console.WriteLine("Objects:");
	///		
	///		foreach( string key in factory.Keys ) {
	///			Console.WriteLine("- {0} , {1}", key, factory.create(key) );
	///		}
	///		
	///		Console.WriteLine();
	///		Console.WriteLine("Register another factory");
	///		
	///		factory.registerFactory( typeof(MyClass3Factory) );
	///		
	///		Console.WriteLine();
	///	}
	///	
	///};
	/// </code>
	/// </example>
	public class FactoryContainer : Hashtable {
	
		#region Private Members
		
		private Type _type;
		private Assembly _sourceAssembly;
		
		/// <summary>Populates this factory with all the factories in the source assembly</summary>
		private void populate( Assembly source )
		{
			Type[] types = source.GetTypes();
			
			foreach( Type type in types ) {
				if( type.IsSubclassOf(FactoryType) ) {
					addEntry( type, false );
				}
			}
		}
		
		/// <summary>Populates this factory with all the factories in the source assembly</summary>
		private void populate()
		{
			populate(_sourceAssembly);
		}
		
		/// <sumary>Registers a factory.</sumary>
		private void addEntry( Type type, bool forceRegister )
		{
			object[] atts = type.GetCustomAttributes( typeof(FactoryAttribute), false );
			string key = type.Name;

			foreach( object att in atts ) {
				
				if( att is FactoryKeyAttribute ) {
					key = getFactoryKey( (FactoryKeyAttribute) att );
					if( key == null ) {
						throw new InvalidFactoryKeyException(type);
					}
				}
				
				if( att is NoAutoRegisterAttribute ) {
					if( ! forceRegister ) {
						OnNotRegistered(type);
						return;
					}
				}
				
				if( att is SignalAttribute ) {
					OnSignal(type);
				}
			}
			
			OnRegistered(type);
			Add( key, createInstance(type) );
		}
		
		/// <summary>Given a FActoryKey, returns associated key.</summary>
		private string getFactoryKey( FactoryKeyAttribute att )
		{
			return att.Key;
		}
		
		/// <summary>Returns true if the type param implements IFactory,</summary>
		private bool implementsIFactory(Type type)
		{
			return type.GetInterface("IFactory") != null;
		}
		
		#endregion
		
		/// <summary>Create's an object.</summary>
		/// <param name="type">Type of the object to be created.</param>
		/// <returns>The new object.</returns>
		/// <remarks>This method will only create instances of classes that 
		/// have default ctor. Override if you wish to use another mechanism.</remarks>
		/// <exception cref="DesignPatterns.Exceptions.ValueTypeException">
		/// If 'type' is a value type.
		/// </exception>
		/// <exception cref="DesignPatterns.Exceptions.CannotCreateException">
		/// If 'type' has no default ctor.
		/// </exception>
		public static object createInstance( Type type ) 
		{
			if( type.IsValueType ) {
				throw new ValueTypeException(type);
			}
			
			ConstructorInfo ctor = type.GetConstructor( Type.EmptyTypes  );
			
			if( ctor == null ) {
				throw new CannotCreateException(type);
			}

			return ctor.Invoke(null);
		}
		
		/// <summary>Creates an empty FactoryContainer.</summary>
		public FactoryContainer() 
		{
		}
	
		/// <summary>Creates and populates the factory.</summary>
		/// <remarks>Will gather all the type derived classes in the type's Assembly.</remarks>
		/// <param name="type">Type of the base factory class.</param>
		/// <exception cref="DesignPatterns.Exceptions.IFactoryNotImplementedException">
		/// If type does not implement IFactory
		/// </exception>
		public FactoryContainer( Type type ) : this( type, Assembly.GetAssembly(type) ) 
		{
		}
		
		/// <summary>Creates and populates the factory.</summary>
		/// <remarks>Will gather all the type derived classes in the source Assembly.</remarks>
		/// <param name="type">Type of the base factory class.</param>
		/// <param name="source">Assembly to search.</param>
		/// <exception cref="DesignPatterns.Exceptions.IFactoryNotImplementedException">
		/// If type does not implement IFactory
		/// </exception>
		public FactoryContainer( Type type, Assembly source )
		{
			_type = type;
			_sourceAssembly = source;
			
			Load(type, source);
		}
		
		public FactoryContainer( Type type, Assembly[] sources )
		{
			if( ! implementsIFactory(type) )
				throw new IFactoryNotImplementedException(type);
			
			_type = type;
			
			foreach( Assembly asm in sources ) {
				_sourceAssembly = asm;
				populate();
			}
		}
		
		/// <summary>Loads the factories in the assembly of the type</summary>
		public void Load( Type type )
		{
			Load( type, Assembly.GetAssembly(type) );
		}
		
		/// <summary>Loads the factories in the assembly</summary>
		public void Load( Type type, Assembly source )
		{
			_type = type;
			_sourceAssembly = source;
			
			if( ! implementsIFactory(type) )
				throw new IFactoryNotImplementedException(type);
			
			populate(source);
		}
		
		/// <summary>Gets the Factory base type.</summary>
		/// <value>Base type from witch this Factory has been populated.</value>
		public Type FactoryType
		{
			get { return _type; }
		}
		
		/// <summary>Obtain a specific factory.</summary>
		/// <param name="key">Factory key.</param>
		/// <returns>The IFactory associated, null if key not found.</returns>
		public IFactory getFactory( string key )
		{
			if( ContainsKey(key) )
				return (IFactory) this[key];
			return null;
		}
		
		/// <summary>Creates a new object.</summary>
		/// <param name="key">Factory key.</param>
		/// <returns>The new object.</returns>
		/// <exception cref="DesignPatterns.Exceptions.FactoryKeyNotFoundException">
		/// If the key is not found.
		/// </exception>
		public object create(string key, object args )
		{
			if( ! ContainsKey(key) )
				throw new FactoryKeyNotFoundException(key);
				
			IFactory creator = (IFactory) this[key];
				
			return creator.create(args);
		}

		/// <sumary>Creates a bew object with args.</sumary>
		public object create( string key )
		{
			return create(key,null);
		}
		
		/// <summary>Registers a new factory.</summary>
		/// <param name="key">Factory key.</param>
		/// <param name="factory">Associated Factory.</param>
		/// <returns>True if the factory was registred.</returns>
		public bool registerFactory( string key, IFactory factory )
		{
			Add( key, factory );
			return true;
		}
		
		/// <summary>Registers a new factory.</summary>
		/// <param name="type">Type of the factory class to add.</param>
		/// <returns>True if the factory was registred.</returns>
		/// <remarks>The key will be the type.Name, unless the class has the 
		/// <see cref="DesignPatterns.Attributes.FactoryKeyAttribute">FactoryKey</see> attribute.</remarks>
		/// <exception cref="DesignPatterns.Exceptions.InvalidFactoryKeyException">
		/// If FactoryKey exists but is invalid.
		/// </exception>
		public bool registerFactory( Type type )
		{
			if( implementsIFactory(type) ) {
				addEntry( type, true );
				return true;
			}
			return false;
		}
		
		#region Events
		
		public delegate void Registered( Type type );
		public delegate void NotRegistered( Type type );
		public delegate void Signal( Type type );
		
		public event Registered FactoryRegistered;
		public event NotRegistered FactoryNotRegistered;
		public event Signal FactorySignal;
		
		protected void OnRegistered( Type type )
		{
			if( FactoryRegistered != null ) {
				FactoryRegistered(type);
			}
		}
		
		protected void OnNotRegistered( Type type )
		{
			if( FactoryNotRegistered != null ) {
				FactoryNotRegistered(type);
			}
		}
		
		protected void OnSignal( Type type )
		{
			if( FactorySignal != null ) {
				FactorySignal(type);
			}
		}
		
		#endregion
	
	};
	
}
