// created on 2/23/04 at 1:43 a

using System;
using System.Collections;
using System.Runtime.Serialization;
using Chronos.Actions;
using Chronos.Exceptions;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Battle;

namespace Chronos.Resources {
	
	/// <summary>
	/// Fabrica de Resource's
	/// </summary>
	[Serializable]
	public class ResourceFactory : ISerializable {
	
		#region Instance Fields
		
		private ArrayList dependencies;
		private ArrayList oncomplete;
		private ArrayList onturn;
		private ArrayList onavailable;
		private ArrayList onremove;
		private ArrayList cost;
		private ArrayList onCancelDuringBuild;
		private ArrayList keywords;
		
		private Action[] dependenciesArray;
		private Action[] onturnArray;
		private Action[] oncompleteArray;
		private Action[] onavailableArray;
		private Action[] onremoveArray;
		private Action[] costArray;
		private Action[] onCancelDuringBuildArray;
		private string[] keywordArray;
		
		private UnitDescriptor unit;
		private bool alwaysCreate;
		private Duration duration;
		private string name;
		private string appliesTo;
		private string category;
		private string categoryDesc;
		private Resource resource;
		private bool allowUndoDuringBuild;
		
		// <"type" , <"AttName", "value"> >
		private Hashtable attributes;

		#endregion

		#region Ctor
		
		/// <summary>Construtor</summary>
		public ResourceFactory( string resourceName, string _appliesTo, string _category, bool _alwaysCreate, string desc ){
			cost = null;
			dependencies = null;
			oncomplete = null;
			onavailable = null;
			dependenciesArray = null;
			oncompleteArray = null;
			onavailableArray = null;
			costArray = null;
			attributes = null;
			keywords = null;
			keywordArray = null;
			resource = null;
			unit = null;
			allowUndoDuringBuild = false;
			alwaysCreate = _alwaysCreate;
			duration = new Duration(5);
			name = resourceName;
			appliesTo = _appliesTo;
			category = _category;
			categoryDesc = desc;
		}

		#endregion

		#region Factory Methods
		
		/// <summary>Retorna um recurso produzido por esta factory</summary>
		/// <remarks></remarks>
		public Resource create()
		{
			if( AlwaysCreate ) {
				return new Resource( this );
			} else {
				if( resource != null ) {
					return resource;
				} else {
					resource = new Resource( this );
					return resource;
				}
			}
		}

		#endregion

		#region Register Methods
		
		/// <summary>Regista uma dependencia</summary>
		public void registerDependency( Action action )
		{
			if( dependencies == null )
				dependencies = new ArrayList();
			((ArrayList)dependencies).Add(action);
		}

		/// <summary>Regista um custo</summary>
		public void registerCost( Action action )
		{
			if( cost == null )
				cost = new ArrayList();
			((ArrayList)cost).Add(action);
		}
		
		/// <summary>Regista um atributo nesta factory</summary>
		/// <remarks>
		///  Existe uma hash 'tipo, hash' em que o tipo e' "attributes" ou "mod";
		///  a hash interna tem pares 'type' e o valor associado, que pode ser uma string('value')
		///  (normal), pode ser um ArrayList de 'value' (caso o group venha a true);
		///  Por outro lado, se a string value for "hashtable", e' adicionada uma nova
		///  hash em vez da string correspondente. Por conjunaoo dos parametros, ate se
		///  pode ter um arraylist de hashs.
		/// </remarks>
		public void registerAttribute( string type, string attName, string value, bool group, bool applyOnce )
		{
			if( attributes == null ) {
				attributes = new Hashtable();
			}
		
			if( applyOnce ) {
				type += "-once";
			}
			
			Hashtable attBag = (Hashtable) attributes[type];
			if( attBag == null ) {
				attributes.Add(type, attBag = new Hashtable());
			}
			
			object toAdd = value;
			if( value.ToLower() == "hashtable" ) {
				toAdd = new Hashtable();
			}
			
			if( attBag.ContainsKey(attName) ) {
				if( ! group ) {
					throw new LoaderException("Attribute '" + attName + "' already exists - Use group=\"true\" to group attributes with the same type");
				} else {
					ArrayList groupedHash = attBag[attName] as ArrayList;
					if( groupedHash == null )
						throw new LoaderException("Not all '"+attName+"'s' have group=\"true\"");
					groupedHash.Add(toAdd);
				}
			} else {
				if( !group ) {
					int number = int.Parse(value);
					attBag.Add(attName,number);
				} else {
					ArrayList list = new ArrayList();
					list.Add(toAdd);
					attBag.Add( attName, list );
				}
			}
		}
		
		/// <summary>Regista uma Action a ser chamada aquando da passagem do turno</summary>
		public void registerOnTurn( Action action )
		{
			if( onturn == null )
				onturn = new ArrayList();
			((ArrayList)onturn).Add(action);
		}
		
		/// <summary>Regista uma Action a ser chamada quando um objecto e criado</summary>
		public void registerOnComplete( Action action )
		{
			if( oncomplete == null )
				oncomplete = new ArrayList();
			((ArrayList)oncomplete).Add(action);
		}
		
		/// <summary>Regista uma Action a ser chamada quando um objecto e criado</summary>
		public void registerOnAvailable( Action action )
		{
			if( onavailable == null )
				onavailable = new ArrayList();
			((ArrayList)onavailable).Add(action);
		}
		
		/// <summary>Regista uma Action a ser chamada quando um objecto é removido</summary>
		public void registerOnRemove( Action action )
		{
			if( onremove == null )
				onremove = new ArrayList();
			onremove.Add(action);
		}

		/// <summary>Regista uma Action a ser chamada quando um objecto  cancelado quando j est a ser construdo</summary>
		public void registerOnCancelDuringBuild( Action action )
		{
			if( onCancelDuringBuild == null )
				onCancelDuringBuild = new ArrayList();
			onCancelDuringBuild.Add(action);
		}
		
		/// <summary>Regista uma keyword</summary>
		public void registerKeyword( string key )
		{
			if( keywords == null )
				keywords = new ArrayList();
			keywords.Add(key);
		}
		
		/// <summary>Obtém um atributo</summary>
		public int GetAtt( string name )
		{
			object obj = Attributes[name];
			if( obj == null ) {
				throw new Exception("Resource " + Name + " don't have attribute " + name);
			}
			return (int) obj;
		}

		#endregion

		#region Dependencies
		
		/// <summary>Verifica se as dependencias desta Factory</summary>
		public bool checkDependencies( Planet planet )
		{
			foreach( Action action in Dependencies ) {
				if( action.evaluate(planet) == false ) {
					return false;
				}
			}
			return true;
		}

		#endregion

		#region Instance Properties

		/// <summary>Indica se  para fazer undo se o recurso for cancelado quando estiver a ser construdo</summary>
		public bool AllowUndoDuringBuild {
			get { return allowUndoDuringBuild; }
			set { allowUndoDuringBuild = value; }
		}
			
		/// <summary>Retorna true caso esta Factory crie uma nova instancia a cada create</summary>
		public bool AlwaysCreate {
			get { return alwaysCreate; }
		}
		
		/// <summary>Retorna o nome do recurso que esta factory cria</summary>
		public string Name {
			get { return name; }
		}
		
		/// <summary>Indica o UnitDescriptor do recurso</summary>
		public UnitDescriptor Unit {
			get { return unit; }
			set { unit = value; }
		}
		
		/// <summary>Indica se este recurso participa em combates</summary>
		public bool CombatUnit {
			get { return Unit != null; }
		}
		
		/// <summary>Retorna um ArrayList com as dependencias(Action's)</summary>
		public Action[] Dependencies {
			get{ return dependenciesArray; }
		}
		
		/// <summary>Retorna um ArrayList com os custos(Action's)</summary>
		public Action[] CostActions {
			get{ return costArray; }
		}
		
		/// <summary>Retorna um ArrayList com as actions a exectuar quando um recurso e criado</summary>
		public Action[] OnCompleteActions {
			get{ return oncompleteArray; }
		}
		
		/// <summary>Retorna um ArrayList com as actions a exectuar aquando da passagem do turno</summary>
		public Action[] OnTurnActions {
			get{ return onturnArray; }
		}
		
		/// <summary>Retorna um ArrayList com as actions a exectuar quando um recurso está disponível</summary>
		public Action[] OnAvailableActions {
			get{ return onavailableArray; }
		}
		
		/// <summary>Retorna um ArrayList com as actions a exectuar quando um recurso está disponível</summary>
		public Action[] OnRemoveActions {
			get{ return onremoveArray; }
		}

		/// <summary>Retorna um ArrayList com as actions a exectuar quando um recurso está disponível</summary>
		public Action[] OnCancelDuringBuild {
			get{ return onCancelDuringBuildArray; }
		}
		
		/// <summary>Indica as palavras chave deste recurso</summary>
		public string[] Keywords {
			get{ return keywordArray; }
		}

		/// <summary>Retorna uma hash com todos os tipos de atributos</summary>
		public Hashtable AllAttributes {
			get { return attributes; }
		}
		
		/// <summary>Retorna os atributos dos recursos que esta factory cria</summary>
		public Hashtable Attributes {
			get {
				if( attributes != null  ) {
					return (Hashtable) attributes["attribute"];
				}
				return null;
			}
		}
		
		/// <summary>Retorna os modificadores dos recursos que esta factory cria</summary>
		public Hashtable Modifiers {
			get {
				if( attributes != null  ) {
					Hashtable hash = (Hashtable) attributes["mod"];
					return hash;
				}
				return null;
			}
		}

		/// <summary>Retorna o numero de turnos que demora a construir o Recurso</summary>
		public Duration Duration {
			get { return duration;  }
			set { duration = value; }
		}

		/// <summary>Indica a que entidade se aplica esta factory</summary>
		public string AppliesTo {
			get { return appliesTo; }
		}

		/// <summary>Indica a categoria desta factory</summary>
		public string Category {
			get { return category; }
		}
		
		/// <summary>Indica o tipo da factory</summary>
		public string CategoryDescription {
			get { return categoryDesc; }
		}
		
		/// <summary>Indica se o recurso é raro</summary>
		public bool RareResource {
			get { return Category == "Rare"; }
		}
		
		#endregion

		#region Type Events
		
		/// <summary>Método a chamar quando todos os recursos tiverem sido carregados</summary>
		public void onLoaded()
		{		
			if( dependencies != null ) {
				dependenciesArray = (Action[]) ((ArrayList)dependencies).ToArray( typeof(Action) );
			}
			if( onturn != null ) {
				onturnArray = (Action[]) ((ArrayList)onturn).ToArray( typeof(Action) );
			}
			if( oncomplete != null ) {
				oncompleteArray = (Action[]) ((ArrayList)oncomplete).ToArray( typeof(Action) );
			}
			if( onavailable != null ) {
				onavailableArray = (Action[]) ((ArrayList)onavailable).ToArray( typeof(Action) );
			}
			if( onremove != null ) {
				onremoveArray = (Action[]) ((ArrayList)onremove).ToArray( typeof(Action) );
			}
			if( onCancelDuringBuild != null ) {
				onCancelDuringBuildArray = (Action[]) ((ArrayList)onCancelDuringBuild).ToArray( typeof(Action) );
			}
			if( keywords != null ) {
				keywordArray = (string[]) keywords.ToArray( typeof(string) );
			}
			if( cost != null ) {
				costArray = (Action[]) ((ArrayList)cost).ToArray( typeof(Action) );
			}

			// deixar o GC tratar disto
			
			dependencies = null;
			oncomplete = null;
			onturn = null;
			onavailable = null;
			onremove = null;
			onCancelDuringBuild = null;
			keywords = null;
			cost = null;
		}

		#endregion

		#region Serialization

		/// <summary>Classe auxiliar</summary>
		[Serializable]
		private sealed class ResourceFactorySerializationHelper : IObjectReference {
			
			#region Instance Fields
			
			public string appliesTo = null;
			public string category = null;
			public string resource = null;

			#endregion
			
			/// <summary>Retorna a ResourceFactoryAssociada
			public object GetRealObject( StreamingContext context )
			{
				return Universe.getFactory(appliesTo, category, resource);
			}
			
		};

		/// <summary>Serializa este objecto</summary>
		void ISerializable.GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.SetType( typeof(ResourceFactorySerializationHelper) );
			info.AddValue("appliesTo", AppliesTo );
			info.AddValue("category", Category );
			info.AddValue("resource", Name );
		}

		#endregion

	};
}
