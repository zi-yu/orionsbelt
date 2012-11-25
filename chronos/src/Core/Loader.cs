// created on 2/23/04 at 8:39

//#define DEBUG_LOADER
//#define DEBUG_LOADER_BATTLE

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections;
using Chronos.Exceptions;
using Chronos.Actions;
using Chronos.Utils;
using Chronos.Resources;
using Chronos.Battle;
using DesignPatterns;

namespace Chronos.Core {

	/// <summary>Faz o load dos ficheiros XML de configuraao dos Resource's.</summary>
	public class Loader {
	
		#region Static Members
	
		private static FactoryContainer actions = null;
		private static ArrayList restrictions = null;
		
		static Loader() 
		{
			actions = new FactoryContainer();
			actions.FactorySignal += new FactoryContainer.Signal(RegisterRestriction);
			actions.Load(typeof(ActionFactory));
#if DEBUG_LOADER
			Log.log("----- {0} Actions --------", actions.Count );
			Log.log(actions);
#endif
		}
		
		private static void RegisterRestriction( Type type )
		{
			if( restrictions == null ) {
				restrictions = new ArrayList();
			}
			
			IFactory factory = (IFactory) FactoryContainer.createInstance(type);
			restrictions.Add( factory.create(null) );
#if DEBUG_LOADER
			Log.log("Restriction Detected: {0}", factory.GetType().Name );
#endif
		}
		
		public static ArrayList Restrictions {
			get { return restrictions; }
		}
		
		#endregion
	
		#region Instance Fields
	
		private string[] files;
		private string currentFile;
		private string currentResource;
		private string currentTag;
		private Hashtable rootTags;
		private Hashtable allBuilders;
		private Hashtable keywords;
		
		#endregion
		
		#region Delegates
		
		private delegate void TagHandler( XmlNode node, ResourceFactory src );
		public delegate void RegisterAction( Action action );
		
		#endregion
		
		#region Base TagHandler's
		
		/// <summary>Cria um Action, dada a ActionFactory key.</summary>
		private object getAction( string name, XmlNode son )
		{
			if( ! actions.ContainsKey(son.Name) ) {
				throwLoaderException("["+name+"] Action '"+son.Name+"' not registered");
			}
					
			return actions.create( son.Name, son );
		}
		
		/// <summary>Encarregue de tratar da tag 'dependencies'</summary>
		private void treatDependencies( XmlNode node, ResourceFactory src )
		{
			treatActions(node, src, new RegisterAction(src.registerDependency));
		}
		
		/// <summary>Encarregue de tratar a tag 'keywords'</summary>
		private void treatKeywords( XmlNode node, ResourceFactory src )
		{
			try {
				
				foreach( XmlNode son in node.ChildNodes ) {
					
					if( son is XmlComment )
						continue;
					
					if( son.Name.ToLower() == "keyword" ) {
						XmlAttribute att = son.Attributes["key"];
						if( att == null ) {
							throwLoaderException("Tag 'keyword' with no 'key' attribute");
						}
						string key = string.Format("{0}-{1}", src.AppliesTo, src.Category);
						object o = keywords[key];
						if( o == null ) {
							keywords.Add(key, new ArrayList());
						}
						ArrayList list = (ArrayList) keywords[key];
						if( !list.Contains(att.Value) ) {
							list.Add(att.Value);
						}

						src.registerKeyword(att.Value);
						
						continue;
					}
					
					throw new LoaderException("Don't know how to handle '"+son.Name+"'");
					
				}
			} catch(LoaderException e) {
				throwLoaderException(e.Message);
			}
		}
		
		/// <summary>Encarregue de tratar a tag 'cost'</summary>
		private void treatCost( XmlNode node, ResourceFactory src )
		{
			try {
				XmlAttribute allowCancel = node.Attributes["allowUndoDuringBuild"];
				src.AllowUndoDuringBuild = checkTrue(allowCancel);
				
				foreach( XmlNode son in node.ChildNodes ) {
					
					if( son is XmlComment )
						continue;
						
					if( actions.ContainsKey(son.Name) ){
						src.registerCost( (Action) actions.create(son.Name,son) );
						continue;
					}
					
					if( son.Name.ToLower() == "duration" ) {
						int quantity=-1, duration;
						string dependency = null, func = null;
						
						XmlAttribute att = son.Attributes["value"];
						if( att == null ) {
							throwLoaderException("Tag 'duration' with no 'value' attribute");
						}
						duration = int.Parse(att.Value);
						
						att = son.Attributes["quantity"];
						if( att != null ) {
							quantity = int.Parse(att.Value);
						}
						
						att = son.Attributes["dependency"];
						if( att != null ) {
							dependency = att.Value;
						}
						
						att = son.Attributes["function"];
						if( att != null ) {
							func = att.Value;
						}
#if DEBUG
						src.Duration = new Duration(duration/duration, 100, dependency, func);
#else
						src.Duration = new Duration(duration, quantity, dependency, func);
#endif
						continue;
					}
					
					throw new LoaderException("Don't know how to handle '"+son.Name+"'");
					
				}
			} catch(LoaderException e) {
				throwLoaderException(e.Message);
			} catch( FormatException e ) {
				throwLoaderException("Integer expected? " + e.Message);
			}
		}
		
		/// <summary>Engarregue de tratar a tag 'onturn'</summary>
		private void treatOnTurn( XmlNode node, ResourceFactory src )
		{
			treatActions(node, src, new RegisterAction(src.registerOnTurn));
		}
		
		/// <summary>Engarregue de tratar a tag 'oncomplete'</summary>
		private void treatOnComplete( XmlNode node, ResourceFactory src )
		{
			treatActions(node, src, new RegisterAction(src.registerOnComplete));
		}
		
		/// <summary>Engarregue de tratar a tag 'onavailable'</summary>
		private void treatOnAvailable( XmlNode node, ResourceFactory src )
		{
			treatActions(node, src, new RegisterAction(src.registerOnAvailable));
		}
		
		/// <summary>Engarregue de tratar a tag 'onavailable'</summary>
		private void treatOnRemove( XmlNode node, ResourceFactory src )
		{
			treatActions(node, src, new RegisterAction(src.registerOnRemove));
		}

		/// <summary>Engarregue de tratar a tag 'onavailable'</summary>
		private void treatOnCancelDuringBuild( XmlNode node, ResourceFactory src )
		{
			treatActions(node, src, new RegisterAction(src.registerOnCancelDuringBuild));
		}
	
		/// <summary>Regista actions num RegistarAction delegate</summary>
		private void treatActions( XmlNode node, ResourceFactory src, RegisterAction reg )
		{
			try {
				foreach( XmlNode son in node.ChildNodes ) {
					
					if( son is XmlComment )
						continue;
									
					object action = getAction(node.Name,son);
					reg( (Action) action );
				}
			} catch(LoaderException e) {
				throwLoaderException(e.Message);
			}
		}
		
		/// <summary>Retorna true se o valor do atributo for true</summary>
		public static bool checkTrue( XmlAttribute att )
		{
			if( att != null ) {
				if( att.Value.ToLower() == "true" ) {
					return true;
				}
			}
			
			return false;
		}
		
		/// <summary>Retorna true se o valor do atributo for true</summary>
		public static bool checkTrue( object att )
		{
			if( att != null ) {
				if( att.ToString().ToLower() == "true" ) {
					return true;
				}
			}
			
			return false;
		}
		
		/// <summary>Trata a tag 'attributes'</summary>
		private void treatAttributes( XmlNode node, ResourceFactory src )
		{
			
			try {
				foreach( XmlNode son in node.ChildNodes ) {
					
					if( son is XmlComment )
						continue;
					
					string type = son.Name;
					string attType = son.Attributes["type"].Value;
					string attValue = son.Attributes["value"].Value;
					bool group = false;
					bool applyOnce = false;
					
					XmlAttribute groupAtt = son.Attributes["group"];
					if( checkTrue(groupAtt) ) {
						group = true;
					}
					
					XmlAttribute applyAttribute = son.Attributes["apply-once"];
					if( checkTrue(applyAttribute) ) {
						applyOnce = true;
					}
										
					src.registerAttribute( type, attType, attValue, group, applyOnce );
				}
			} catch( LoaderException e ) {
				throwLoaderException(e.Message);
			} catch( FormatException e ) {
				throwLoaderException("Integer expected? " + e.Message);
			}
		}
		
		/// <summary>Trata da tag battle</summary>
		private void treatBattle( XmlNode node, ResourceFactory src )
		{
			try {
				UnitDescriptor unit = new UnitDescriptor();
				
				foreach( XmlNode son in node.ChildNodes ) {
					
					if( son is XmlComment )
						continue;
						
					switch(son.Name) {
						case "attack": 
							treatAttack(son, unit);
							break;
						case "defense": 
							treatDefense(son, unit);
							break;
						case "movement": 
							treatMovement(son, unit);
							break;
						default:
							throw new LoaderException("Don't know how to handle battle/"+son.Name);
					}
				}
				
				unit.UnitType = XmlUtils.getString(node, "unitType");
				src.Unit = unit;
				
#if DEBUG_LOADER_BATTLE
				Log.log("--- UNIT DESCRIPTOR DEBUG ----");
				Log.log("Resource: {0}", src.Name);
				Log.log("CombatUnit: {0}", src.CombatUnit);
				Log.log("BaseAttack: {0}", src.Unit.BaseAttack);
				Log.log("AttackTargets: {0}", src.Unit.AttackTargets.Count);
				Log.log("Range: {0}", src.Unit.Range);
				Log.log("BaseDefense: {0}", src.Unit.BaseDefense);
				Log.log("DefenseTargets: {0}", src.Unit.DefenseTargets.Count);
				Log.log("MovementType: {0}", src.Unit.MovementTypeDescription);
				Log.log("MovementCost: {0}", src.Unit.MovementCost);
				Log.log("Minimum damage: {0}", src.Unit.MinimumDamage);
				Log.log("Maximum Damage: {0}", src.Unit.MaximumDamage);
#endif
				
			} catch( LoaderException e ) {
				throwLoaderException(e.Message);
			} catch( FormatException e ) {
				throwLoaderException("Integer expected? " + e.Message);
			}
		}
		
		/// <summary>Trata da tag attack</summary>
		private void treatAttack( XmlNode node, UnitDescriptor unit )
		{
			unit.BaseAttack = XmlUtils.getInt(node, "base");
			unit.Range = XmlUtils.getInt(node, "range");
			unit.MaximumDamage = XmlUtils.getInt(node, "maximumDamage");
			unit.MinimumDamage = XmlUtils.getInt(node, "minimumDamage");
			unit.CanDamageBehindUnits = XmlUtils.getBool(node, "canDamageBehindUnits");
			unit.CatapultAttack = XmlUtils.getBool(node, "catapultAttack");
			unit.TripleAttack = XmlUtils.getBool(node, "tripleAttack");
			unit.ReplicatorAttack = XmlUtils.getBool(node, "replicatorAttack");
			unit.AttackTargets = treatTargets(node);
		}
		
		/// <summary>Trata da tag defense</summary>
		private void treatDefense( XmlNode node, UnitDescriptor unit )
		{
			unit.BaseDefense = XmlUtils.getInt(node, "base");
			unit.HitPoints = XmlUtils.getInt(node, "hitPoints");
			unit.CanStrikeBack = XmlUtils.getBool(node, "canStrikeBack");
			unit.DefenseTargets = treatTargets(node);
		}
		
		/// <summary>Trata da tag movement</summary>
		private void treatMovement( XmlNode node, UnitDescriptor unit )
		{
			unit.MovementCost = XmlUtils.getInt(node, "cost");
			unit.MovementTypeDescription = XmlUtils.getString(node, "type");
			unit.Level = XmlUtils.getString(node, "level");
		}
		
		/// <summary>Trata as tags target</summary>
		private Hashtable treatTargets( XmlNode node )
		{
			Hashtable root = new Hashtable();
			
			foreach( XmlNode son in node.ChildNodes ) {
				if( son is XmlComment ) {
					continue;
				}
				
				string type = XmlUtils.getString(son, "type");
				string key = XmlUtils.getString(son, "key");
				int val = XmlUtils.getInt(son, "value");
				
				if( !root.Contains(type) ) {
					root.Add( type, new Hashtable() );
				}
				
				Hashtable target = (Hashtable) root[type];
				target.Add(key, val);
			}
			
			return root;
		}
		
		#endregion
		
		#region Ctors
		
		/// <summary>Construtor</summary>
		public Loader()
		{
			files = null;
			allBuilders = null;
			
			rootTags = new Hashtable();
			rootTags.Add( "dependencies", new TagHandler(this.treatDependencies) );
			rootTags.Add( "cost", new TagHandler(this.treatCost) );
			rootTags.Add( "onturn", new TagHandler(this.treatOnTurn) );
			rootTags.Add( "oncomplete", new TagHandler(this.treatOnComplete) );
			rootTags.Add( "attributes", new TagHandler(this.treatAttributes) );
			rootTags.Add( "onavailable", new TagHandler(this.treatOnAvailable) );
			rootTags.Add( "onremove", new TagHandler(this.treatOnRemove) );
			rootTags.Add( "onCancelDuringBuild", new TagHandler(this.treatOnCancelDuringBuild) );
			rootTags.Add( "battle", new TagHandler(this.treatBattle) );
			rootTags.Add( "keywords", new TagHandler(this.treatKeywords) );
			
			keywords = new Hashtable();
			
		}
		
		#endregion
		
		#region Exception Utilities
		
		/// <summary>Lanaa uma excepaoo com informaoes do Load</summary>
		private void throwLoaderException( string message )
		{
			if( message.IndexOf(" in file ") != -1 ) {
				throw new LoaderException(message);
			}
		
			StringWriter str = new StringWriter();
			str.WriteLine("[{2}] Error loading '{1}' in file '{0}' !", currentFile, currentResource, currentTag);
			str.WriteLine();
			str.WriteLine(message);
		
			throw new LoaderException(str.ToString());
		}
		
		#endregion
		
		#region XML Related Methods
	
		/// <summary>Faz o load de um ficheiro XML para um XmlDocument</summary>
		private XmlDocument loadXmlDoc( string file )
		{
			currentFile = file;
			currentResource = "?";
			currentTag = "XML Parser";
		    try {

		     	return XmlUtils.loadXmlDoc(file);
       
		    } catch(XmlException e) {
		    	throwLoaderException(e.Message);
		    	return null;	// para o compilador nao se queixar
			}
		}
		
		/// <summary>Faz o load de todas as Factories um ficheiro XML</summary>
		private void loadFactories( XmlDocument doc )
		{
			XmlElement root = doc.DocumentElement;
			
			// itera sobre todas as tag's <resource>
			foreach( XmlNode son in root.ChildNodes ) {
				
				if( son is XmlComment )
					continue;
				
				string resourceType = son.Attributes["type"].Value;
				string key = son.Attributes["value"].Value;
				currentResource = key;
				currentTag = son.Name;
				Hashtable builders;
				
				if( key.IndexOf(' ') != -1 ) {
					throwLoaderException("No spaces allowed: '"+key+"'");
				}
				
				string appliesTo = null;
				XmlAttribute appliesToAtt = son.Attributes["appliesTo"];
				if( appliesToAtt != null ) {
					appliesTo = appliesToAtt.Value;
				} else {
					appliesTo = "planet";
				}
				
				string category = null;
				XmlAttribute categoryAtt = son.Attributes["category"];
				if( categoryAtt != null ) {
					category = categoryAtt.Value;
				} else {
					category = "general";
				}
				
				object obj = allBuilders[appliesTo];
				if( obj == null ) {
					builders = new Hashtable();
					allBuilders.Add(appliesTo, builders);
				} else {
					builders = (Hashtable) obj;
				}
				
				ResourceBuilder current = null;
				if( builders.ContainsKey(resourceType) ) {
					current = (ResourceBuilder) builders[resourceType];
				} else {
					builders.Add( resourceType, current = new ResourceBuilder(appliesTo) );
				}
				
				if( current.ContainsKey(key) ) {
					throwLoaderException("'"+key+"' factory already exists");
				}
				
				bool alwaysCreate = false;
				XmlAttribute alwaysAtt = son.Attributes["always-create"];
				if( alwaysAtt != null && checkTrue(alwaysAtt) ) {
					alwaysCreate = true;
				}

				ResourceFactory factory = new ResourceFactory(key, appliesTo, resourceType, alwaysCreate, category);

				// itera sobre <dependencies>, <cost>, etc...
				foreach( XmlNode neto in son.ChildNodes ) {
					currentTag = neto.Name;
					if( rootTags.ContainsKey(neto.Name) ) {
						TagHandler handler = (TagHandler) rootTags[neto.Name];
						handler(neto,factory);
					} else {
						throwLoaderException("Don't know how to handle '" + neto.Name +"'");
					}
				}

				factory.onLoaded();
				current.Add(key,factory);
			}
			
		}
		
		#endregion
		
		#region Load Related Methods
	
		/// <summary>Carrega os ficheiros de configuraaoo e retorna uma hash com as factories.</summary>
		public Hashtable load( string confPath )
		{
			if( ! Directory.Exists( confPath ) ) {
				throw new LoaderException("Configuration directory not found: '" + confPath + "'");
			}
			
			string dtdPath = confPath + Path.DirectorySeparatorChar + "conf.dtd";
			if( ! File.Exists(dtdPath) ) {
				throw new LoaderException("DTD file("+dtdPath+") not found");
			}
			
			files = Directory.GetFiles(confPath, "*.xml");
			if( files.Length == 0 ) {
				throw new LoaderException("No XML files found on '" + confPath + "'");
			}
			
			allBuilders = new Hashtable();
				
			foreach( string file in files ) {
				loadFactories( loadXmlDoc(file) );
			}
			
			return allBuilders;
		}
	
		/// <summary>Devolve os ficheiros usados no Loader.</summary>
		public string[] Files {
			get { return files; }
		}
		
		/// <summary>Devolve uma Hashtable com todos os builders</summary>
		public Hashtable Builders {
			get { return allBuilders; }
		}
		
		/// <summary>Indica todas as Keywords</summary>
		public Hashtable Keywords {
			get { return keywords; }
		}
		
		#endregion
		
		#region Data Output Related Methods
		
		/// <summary>Retorna uma string que ilustra o conteudo do Loader</summary>
		public void printOn( StreamWriter str )
		{
			/*if( allBuilders == null ) {
				str.WriteLine("Empty Loader!");
				return;
			}
			
			IDictionaryEnumerator it = builders.GetEnumerator();
			while( it.MoveNext() ) {
				ResourceBuilder builder = (ResourceBuilder) it.Value;
				IDictionaryEnumerator factIt = builder.GetEnumerator();
				str.WriteLine("|-- {0} -- applies-to: '{1}' -----------|",it.Key,builder.AppliesTo);
				str.WriteLine();
				while( factIt.MoveNext() ) {
					ResourceFactory factory = (ResourceFactory) factIt.Value;
					str.WriteLine("\t- {0} [always-create='{1}', duration='{2}']",factIt.Key, factory.AlwaysCreate, factory.Duration);
					
					// <dependencies>
					writeActions(str,factory.Dependencies,"dependencies");
					
					// <cost>
					writeActions(str, factory.CostActions, "cost");
					
					// <attributes>
					writeAtts(str,factory.Attributes, "attribute");
					writeAtts(str,factory.Modifiers, "mod");
					writeAtts(str,factory.OneTimeModifiers, "mod-once");
					
					// <oncomplete>
					writeActions(str,factory.OnCompleteActions,"oncomplete");
					
					// <onavailable>
					writeActions(str,factory.OnAvailableActions,"onavailable");
					
					str.WriteLine();
				}
				str.WriteLine();
			}*/
		}
		
		/// <summary>Escreve uma lista de attributos</summary>
		private void writeAtts( StreamWriter str, Hashtable container, string title )
		{
			/*if( container != null ) {
				str.WriteLine("\t\t<{1} count='{0}'>", container.Count, title);
				IDictionaryEnumerator en = container.GetEnumerator();
				while( en.MoveNext() ) {
					if( en.Value is IList ) {
						IList list = (IList) en.Value;
						for( int i = 0; i < list.Count; ++i ) {
							str.WriteLine("\t\t\t{0}({1}) = {2}",en.Key,i,list[i]);
						}
					} else {
						str.WriteLine("\t\t\t{0} = {1}",en.Key,en.Value);
					}
				}
				str.WriteLine("\t\t</{0}>",title);
			}*/
		}
		
		/// <summary>Escreve uma lista de Actions</summary>
		private void writeActions(StreamWriter str, IEnumerable container, string title)
		{
			/*if( container != null ) {
				str.WriteLine("\t\t<{0}>", title);
				foreach( Action action in container ) {
					str.WriteLine("\t\t\t{0}: {1} -> {2}", action.Name, action.Key, action.Value);
				}
				str.WriteLine("\t\t</{0}>",title);
			}*/
		}
		                                      
		#endregion
		
	};

}
