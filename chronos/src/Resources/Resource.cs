// created on 2/29/04 at 6:35 a

using System;
using System. Runtime.Serialization;
using System.Collections;
using Chronos.Core;
using Chronos.Utils;
using Chronos.Battle;

namespace Chronos.Resources {
	
	/// <summary>Representaao logica de um recurso</summary>
	[Serializable]
	public class Resource : ISerializable {

		#region Instance Fields
		
		private ResourceFactory factory;

		#endregion

		#region Ctors
		
		/// <summary>Construtor</summary>
		public Resource( ResourceFactory owner )
		{
			factory = owner;
		}

		#endregion
		
		#region Instance Properties
		
		/// <summary>Nome do recurso</summary>
		public string Name
		{
			get { return Factory.Name; }
		}
		
		/// <summary>Atributos deste recurso</summary>
		public Hashtable Attributes
		{
			get { return Factory.Attributes; }
		}
		
		/// <summary>Retorna os modificadores deste recurso</summary>
		public Hashtable Modifiers
		{
			get { return Factory.Modifiers; }
		}
		
		/// <summary>Retorna a factory que sabe criar este recurso</summary>
		public ResourceFactory Factory {
			get { return factory; }
		}
		
		/// <summary>Indica o UnitDescriptor do recurso</summary>
		public UnitDescriptor Unit {
			get { return Factory.Unit; }
		}
		
		/// <summary>Indica se este recurso participa em combates</summary>
		public bool CombatUnit {
			get { return Factory.CombatUnit; }
		}
		
		/// <summary>Indica se o recurso é raro</summary>
		public bool Rare {
			get { return Factory.Category == "Rare"; }
		}
		
		/// <summary>Indica se este recurso pode ser comprado</summary>
		public bool MarketResource {
			get {
				return IsTrue("MarketResource");
			}
		}
		
		/// <summary>Indica o valor da unidade no torneio</summary>
		public int TournamentValue {
			get {
				object obj = Factory.Attributes["TournamentValue"]; 
				if( obj == null ) {
					return 0;
				}
			
				return (int) obj;
			}
		}
		
		/// <summary>Mostra o nome do recurso</summary>
		public override string ToString()
		{
			return Name;
		}
		
		#endregion
		
		#region Instance Utilities
		
		public bool IsTrue( string att )
		{
			object obj = Factory.Attributes[att]; 
			if( obj == null ) {
				return false;
			}
			return obj.ToString() == "1";
		}
		
		#endregion
		
		#region Static Utilities
		
		/// <summary>Indica o nvel de um determinado recurso</summary>
		public static int GetResearchLevel( IResourceManager source, string resource )
		{
			if( source == null ) {
				return 0;
			}
			
			if( source.isResourceAvailable("Research", resource+"III", 1) ) {
				return 3;
			}
			if( source.isResourceAvailable("Research",resource+"II", 1) ) {
				return 2;
			}
			if( source.isResourceAvailable("Research",resource+"I", 1) ) {
				return 1;
			}
			
			return 0;
		}
		
		/// <summary>Indica se determinado recurso intrinseco  teletransportvel</summary>
		public static bool IsTeletransportable( string intrinsic )
		{
			ResourceFactory factory = Universe.getFactory("planet", "Intrinsic", intrinsic);
			
			object att = factory.Attributes["Teletransportable"];
			if( att == null ) {
				return false;
			}
			
			return att.ToString() == "1";
		}
		
		/// <summary>Indica se determinado recurso intrinseco  teletransportvel</summary>
		public static bool IsTeletransportable( string category, string intrinsic )
		{
			ResourceFactory factory = Universe.getFactory("planet", category, intrinsic);
			
			object att = factory.Attributes["Teletransportable"];
			if( att == null ) {
				return false;
			}
			
			return att.ToString() == "1";
		}
		
		/// <summary>Indica se determinado recurso intrinseco  teletransportvel</summary>
		public static bool IsTeletransportable( Resource resource )
		{
			ResourceFactory factory = Universe.getFactory("planet", resource.Factory.Category, resource.Name);
			
			object att = factory.Attributes["Teletransportable"];
			if( att == null ) {
				return false;
			}
			
			return att.ToString() == "1";
		}
		
		/// <summary>Indica se determinado recurso intrinseco  teletransportvel</summary>
		public static int TeletransportationCost( string category, string intrinsic, int quantity )
		{
			ResourceFactory factory = Universe.getFactory("planet", category, intrinsic);
			
			object att = factory.Attributes["TeletransportationCost"];
			if( att == null ) {
				return 0;
			}
			
			try {
				return ((int)att) * quantity;
			} catch {
				return 0;
			}
		}
		
		/// <summary>Indica se determinado recurso intrinseco  teletransportvel</summary>
		public static int FleetTeletransportationCost( Resource ship, int quantity )
		{
			object att = ship.Attributes["TeletransportationCost"];
			if( att == null ) {
				return 0;
			}
			
			try {
				return ((int)att) * quantity;
			} catch {
				return 0;
			}
		}
		
		/// <summary>Indica se determinado recurso é raro</summary>
		public static bool IsRare( string resource )
		{
			ResourceBuilder factories = Universe.getFactories("planet", "Rare");
			foreach( ResourceFactory factory in factories.Values ) {
				if( resource == factory.Name ) {
					return true;
				}
			}
			return false;
		}
		
		#endregion

		#region Serialization

		/// <summary>Classe auxiliar</summary>
		[Serializable]
		private sealed class ResourceSerializationHelper : IObjectReference {
			
			#region Instance Fields
			
			public string appliesTo = null;
			public string category = null;
			public string resource = null;

			#endregion
			
			/// <summary>Retorna a ResourceFactoryAssociada</summary>
			public object GetRealObject( StreamingContext context )
			{
				return Universe.getFactory(appliesTo, category, resource).create();
			}
			
		};

		/// <summary>Serializa este objecto</summary>
		void ISerializable.GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.SetType( typeof(ResourceSerializationHelper) );
			info.AddValue("appliesTo", Factory.AppliesTo );
			info.AddValue("category", Factory.Category );
			info.AddValue("resource", Name );
		}

		#endregion
	};
	
}
