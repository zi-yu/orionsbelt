// created on 8/1/2005 at 11:38 AM

using System;
using System.IO;
using System.Collections;
using Language;
using Alnitak;
using Chronos.Core;
using Chronos.Resources;
using Chronos.Actions;
using Chronos.Utils;
using Chronos.Battle;
using Chronos.Queue;

namespace RulesToWiki {
	
	/// <summary>
	/// Documentation Generator
	/// </summary>
	public class Docer {
	
		#region Ctor
		
		public Docer( string localeDir, string _baseDir,  string _targetDir )
		{
			info = new LanguageInfo(localeDir);
			rulesDir = Path.Combine(_baseDir, "resources/");
			generalDir = Path.Combine(_baseDir, "general/");
			targetDir = _targetDir;
			
			categories = new Hashtable();
			categories.Add("Building", "Edifícios");
			categories.Add("Unit", "Unidades");
			categories.Add("Research", "Pesquisas");
			categories.Add("Intrinsic", "Intrínsecos");
			categories.Add("Rare", "Raro");
			
			indexes = new Hashtable();
			indexes.Add("Building", new CategoryIndex(GetBuildingCategoryIndex));
			indexes.Add("Unit", new CategoryIndex(GetUnitCategoryIndex));
			indexes.Add("Research", new CategoryIndex(GetResearchCategoryIndex));
		}
	
		#endregion
	
		#region Instance Fields
		
		protected Language.ILanguageInfo info = null;
		protected string rulesDir = null;
		protected string generalDir = null;
		protected string targetDir = null;
		protected Hashtable all = null;
		protected Hashtable categories = null;
		protected Hashtable indexes = null;
		
#if !DEBUG
		private static string BaseImgUrl = "http://orionsbelt.sourceforge.net/skins/commonImages/";
#else
		private static string BaseImgUrl = "/skins/commonImages/";
#endif
		
		private delegate bool ResourceAction( ResourceFactory source, ResourceFactory factory );
		private static string[] Levels = new string[] { "ground", "air" };
		private static string[] MovementTypes = new string[] { "all", "normal", "diagonal", "front" };
		private static string[] UnitTypes = new string[] { "light", "medium", "heavy", "animal", "building", "special" };
		private static string[] BuildingTypes = new string[] { "general", "upgrade" };
		private static string[] ResearchTypes = new string[] { "tech", "fleet", "exploration", "planet" };
		
		private delegate string CategoryIndex( string category );
		
		#endregion
		
		#region Rendering
		
		/// <summary>Cria a documentação</summary>
		public void Generate()
		{
			Exception ex = Terrain.LoadTerrainInfo(generalDir);
			if( ex != null ) {
				throw ex;
			}
			Loader loader = new Loader();
			all = loader.load(rulesDir);
			WriteDocs();
		}
		
		/// <summary>Mostra a documentação</summary>
		private void WriteDocs()
		{
			IDictionaryEnumerator it = categories.GetEnumerator();
			while( it.MoveNext() ) {
				ResourceBuilder toShow = getHash(it.Key.ToString());
				
				DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(targetDir, ToSimpleCharaters(it.Key.ToString())));

        		if (dirInfo.Exists == false) {
					dirInfo.Create();
				}
				
				writeEntries( dirInfo, toShow );
				
				if( it.Key.ToString() == "Intrinsic" ) {
					Hashtable root = (Hashtable) all["planet"];
					if( root != null && root.ContainsKey("Intrinsic") ) {
						 writeEntries( dirInfo, (ResourceBuilder) root["Intrinsic"]);
					}
				}
				
				WriteCategoryIndex(dirInfo, it.Key.ToString());
				Console.WriteLine();
			}
			DirectoryInfo units = new DirectoryInfo(Path.Combine(targetDir, "Unit"));
			WriteUnitsTerrainHandicap(units);
			WriteUnitsCategoryHandicap(units);
			WriteUnitsLevelHandicap(units);
			WriteUnitsByMovement(units);
			Console.WriteLine();
		}
		
		/// <summary>Cria um tópico com o índice</summary>
		private void WriteCategoryIndex( DirectoryInfo dir, string category  )
		{
			Console.Write("Generating category '{0}' index... ", category);
			string content = null;
			CategoryIndex index = (CategoryIndex) indexes[category];
			if( index != null ) {
				content = index(category);
			} else {
				content = GetCategoryIndex( category );
			}
			
			StreamWriter writer = new StreamWriter(Path.Combine(dir.FullName, category+".wiki"), false, System.Text.Encoding.UTF8);
			writer.WriteLine(content);
			writer.Close();
			
			writer = new StreamWriter(Path.Combine(dir.FullName, "HomePage.wiki"), false, System.Text.Encoding.UTF8);
			writer.WriteLine(content);
			writer.Close();
			
			writer = new StreamWriter(Path.Combine(dir.FullName, "_ContentBaseDefinition.wiki"), false, System.Text.Encoding.UTF8);
			writer.WriteLine("Description: Orion's Belt Wiki");
			writer.WriteLine("Namespace: {0}", category);
			writer.WriteLine("Title: Orion's Belt");
			writer.WriteLine("Description: Orion's Belt Wiki");
			writer.WriteLine("HomePage: HomePage");
			writer.WriteLine("Contact:");
			writer.Close();
			
			Console.WriteLine("Ok");
			
			if( category == "Building" ) {
				WriteCategoryByKeywords(dir, category);
			}
		}
		
		private string[] GetKeywords( string category )
		{
			ArrayList list = new ArrayList();
			foreach( ResourceFactory factory in getHash(category).Values ) {
				foreach( string k in factory.Keywords ) {
					if( !list.Contains(k) ) {
						list.Add(k);
					}
				}
			}
			return (string[]) list.ToArray(typeof(string));
		}
		
		private void WriteCategoryByKeywords( DirectoryInfo dir, string category )
		{
			string[] keywords = GetKeywords(category);
			
			foreach( string keyword in keywords ) {
				string file = Path.Combine(dir.FullName, "Filter_"+keyword+".wiki");
				Console.Write("Generating {0}/Filter_{1}.wiki... ", category, keyword);
				using( StreamWriter writer = new StreamWriter(file, false, System.Text.Encoding.UTF8) ) {
					writer.WriteLine(":Summary: Filtragem por {0}", info.getContent(keyword));
					writer.WriteLine(":Display: {0}", info.getContent(keyword));
					writer.WriteLine(":Parent: {0}.{0}", category);
					writer.WriteLine("{0}.[{0}] relacionados com *{1}*.", category, info.getContent(keyword));
					
					writer.WriteLine("||{{!^}}*Recurso*||{{!^}}*{0}*||", info.getContent("description"));
					
					foreach( ResourceFactory factory in getHash(category).Values ) {
						bool toShow = false;
						foreach( string k in factory.Keywords ) {
							if( k == keyword ) {
								toShow = true;
								break;
							}
						}
						if( !toShow ) {
							continue;
						} 
						string desc = getDesc(factory.Name);
						if( desc == null ) {
							desc = info.getContent("noneAvailable");;
						}
						writer.WriteLine("||{0}||{1}||", GetLink(factory), desc.Replace("\n", "").Replace("\t", " ") );	
					}
					
					Console.WriteLine("Ok");
				}
			}
		}
		
		private string GetResearchCategoryIndex( string category )
		{
			string caption = info.getContent(category);
			StringWriter writer = new StringWriter();
			
			writer.WriteLine(":Display:{0}", caption);
			writer.WriteLine(":Summary: Lista de {0}", caption);
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(info.getContent("ResearchListIntro"));
			writer.WriteLine();

			foreach( string type in ResearchTypes ) {
				GetResearchCategoryIndexAux(writer, category, type);
			}
			
			return writer.ToString();
		}
		
		private void GetResearchCategoryIndexAux( StringWriter writer, string category, string type )
		{
			writer.WriteLine("!!!Pesquisas Categoria *{0}*", info.getContent(type));
			writer.WriteLine("||{{!^}}*Recurso*||{{!^}}*{0}*||", info.getContent("description"));
			
			foreach( ResourceFactory factory in getHash(category).Values ) {
				if( factory.CategoryDescription != type ) {
					continue;
				}
				string desc = getDesc(factory.Name);
				if( desc == null ) {
					desc = info.getContent("noneAvailable");;
				}
				writer.WriteLine("||{0}||{1}||", GetLink(factory), desc.Replace("\n", "").Replace("\t", " ") );	
			}
		}	
		
		private string GetBuildingCategoryIndex( string category )
		{
			string caption = info.getContent(category);;
			StringWriter writer = new StringWriter();
			
			writer.WriteLine(":Display:{0}", caption);
			writer.WriteLine(":Summary: Lista de {0}", caption);
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(info.getContent("BuildingListIntro"));
			writer.WriteLine();
			
			string[] keys = GetKeywords(category);
			writer.WriteLine("!!!Filtros");
			writer.Write("!!!Building.[Filter_{0}]", keys[0]);
			for( int i = 1; i < keys.Length; ++i ) {
				writer.Write("\t* Building.[Filter_{0}]", keys[i]);
			}
			writer.WriteLine();

			foreach( string type in BuildingTypes ) {
				GetBuildingCategoryIndexAux(writer, category, type);
			}
			
			return writer.ToString();
		}
		
		private void GetBuildingCategoryIndexAux( StringWriter writer, string category, string type )
		{
			writer.WriteLine("!!!{0}", string.Format( info.getContent("BuildingCategory"), info.getContent(type)) );
			writer.WriteLine("||{{!^}}*Recurso*||{{!^}}*{0}*||", info.getContent("description"));
			
			foreach( ResourceFactory factory in getHash(category).Values ) {
				if( factory.CategoryDescription != type ) {
					continue;
				}
				string desc = getDesc(factory.Name);
				if( desc == null ) {
					desc = info.getContent("noneAvailable");;
				}
				writer.WriteLine("||{0}||{1}||", GetLink(factory), desc.Replace("\n", "").Replace("\t", " ") );	
			}
		}		
		
		private string GetUnitCategoryIndex( string category )
		{
			string caption = info.getContent(category);;
			StringWriter writer = new StringWriter();
			
			writer.WriteLine(":Display:{0}s", caption);
			writer.WriteLine(":Summary: Lista de {0}", caption);
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(info.getContent("UnitListIntro"));
			writer.WriteLine("\t* Unit.[UnidadesPorTerreno]");
			writer.WriteLine("\t* Unit.[UnidadesPorCategoria]");
			writer.WriteLine("\t* Unit.[UnidadesPorPosicao]");
			writer.WriteLine("\t* Unit.[UnidadesPorTipoDeMovimento]");
			writer.WriteLine();
			
			foreach( string type in UnitTypes ) {
				if( type == "building" ) {
					GetUnitCategoryIndexAux(writer, "Building", type);
				} else {
					GetUnitCategoryIndexAux(writer, category, type);
				}
			}
			
			return writer.ToString();
		}
		
		private void GetUnitCategoryIndexAux( StringWriter writer, string category, string unitType )
		{
			writer.WriteLine("!!!Unidades da categoria *{0}*", unitType);
			writer.WriteLine("||{!^}*Imagem*||{!^}*Recurso*||{!^}*Ataque*||{!^}*Defesa*||{!^}*Vida*||{!^}*Dano*||{!^}*Movimento*||{!^}*Custo Movimento*||{!^}*Alcance*||{!^}*Contra-Ataca*||{!^}*Catapulta*||{!^}*Ricochete*||{!^}*Triple*||{!^}*Replicator*||");
			
			foreach( ResourceFactory factory in getHash(category).Values ) {
				if( !factory.CombatUnit ) {
					continue;
				}
				if( factory.Unit.UnitType != unitType ) {
					continue;
				}
				writer.Write("|| http://orionsbelt.sourceforge.net/skins/commonImages/{0}.gif", factory.Name.ToLower());
				writer.WriteLine("||{0}||{{^}}{1}||{{^}}{2}||{{^}}{3}||{{^}}{4}-{5}||{{^}}{6}||{{^}}{7}||{{^}}{8}||{{^}}{9}||{{^}}{10}||{{^}}{11}||{{^}}{12}||{{^}}{13}||", 
						GetLink(factory), 
						factory.Unit.BaseAttack,
						factory.Unit.BaseDefense,
						factory.Unit.HitPoints,
						factory.Unit.MinimumDamage,
						factory.Unit.MaximumDamage,
						factory.Unit.MovementTypeDescription,
						factory.Unit.MovementCost,
						factory.Unit.Range,
						GetYesNoImage(factory.Unit.CanStrikeBack),
						GetYesNoImage(factory.Unit.CatapultAttack),
						GetYesNoImage(factory.Unit.CanDamageBehindUnits),
						GetYesNoImage(factory.Unit.TripleAttack),
						GetYesNoImage(factory.Unit.ReplicatorAttack)
					);	
			}
		}
		
		private string GetYesNoImage( bool b )
		{
			if( b) {
				return "http://orionsbelt.sourceforge.net/skins/commonImages/yes.gif";
			}
			return "http://orionsbelt.sourceforge.net/skins/commonImages/no.gif";
		}
		
		private string GetCategoryIndex( string category )
		{
			string caption = info.getContent(category);
			StringWriter writer = new StringWriter();
			
			writer.WriteLine(":Display:{0}", caption);
			writer.WriteLine(":Summary: Lista de {0}", caption);
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(info.getContent(category+"ListIntro"));
			writer.WriteLine();
			writer.WriteLine("||{{!^}}*Recurso*||{{!^}}*{0}*||", info.getContent("description"));
			
			foreach( ResourceFactory factory in getHash(category).Values ) {
				string desc = getDesc(factory.Name);
				if( desc == null ) {
					desc = info.getContent("noneAvailable");;
				}
				writer.WriteLine("||{0}||{1}||", GetLink(factory), desc.Replace("\n", "").Replace("\t", " ") );	
			}
			
			return writer.ToString();
		}
		
		/// <summary>Mostra todos os recursos</summary>
		private void writeEntries( DirectoryInfo dir, ResourceBuilder toShow )
		{
			foreach( ResourceFactory factory in toShow.Values ) {
			
				string file = factory.Name;
				file = ToSimpleCharaters(file.Replace(" ", "")) + ".wiki";
				
				StreamWriter writer = new StreamWriter(Path.Combine(dir.FullName, file), false, System.Text.Encoding.UTF8);
				
				AppendDescription(factory, writer, dir);
				
				Console.Write("Generating {0}/{1}... ", dir.Name, file);
				writer.WriteLine(":Display:{0}", info.getContent(factory.Name) );
				writer.WriteLine(":Parent:{0}.{0}", factory.Category );
				if( factory.CombatUnit ) {
					writer.WriteLine(":PreviewImage: {1}units/{0}_preview.gif", factory.Name, BaseImgUrl );
				}
				writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
				
				string desc = getDesc(factory.Name);
				if( desc != null ) {
					writer.WriteLine(":Summary: {0}", desc);
					writer.WriteLine("!!!{0}", info.getContent("Summary"));
					writer.WriteLine("\t* {0}", desc);
				}
				
				writeUnitDescriptor(writer, factory.Unit);
				writeResourcesProvided( writer, factory );	
				writeResourcesThatNeedThis( writer, factory );
				writeActions(writer, "dependencies", factory.Dependencies);
				writeModifiers(writer, "modifiers", factory.Modifiers);
				writeAtts(writer, "attributes", factory.Attributes);
				writeActions(writer, "cost", factory.CostActions);
				writeDuration(writer, factory );
				writeActions(writer, "onturn", factory.OnTurnActions);
				writeActions(writer, "oncomplete", factory.OnCompleteActions);
				writeActions(writer, "onremove", factory.OnRemoveActions);
				writeActions(writer, "onCancelDuringBuild", factory.OnCancelDuringBuild);
				writeActions(writer, "onavailable", factory.OnAvailableActions);
				
				writer.Close();
				Console.WriteLine("Ok");
			}
		}
		
		private void writeUnitDescriptor( StreamWriter writer, UnitDescriptor unit )
		{
			if( unit == null ) {
				return;
			}

			writer.WriteLine("!!! {0}", info.getContent("CombatCaracteristics"));
			writer.WriteLine("||{{!^}}*{0}*||{{!^}}*{0}*||", info.getContent("Attribute"), info.getContent("Value"));
			writer.WriteLine("||Categoria||{{^}}{0}||", unit.UnitType);
			writer.WriteLine("||Ataque Base||{{^}} {0} ||", unit.BaseAttack);
			writer.WriteLine("||Defesa Base||{{^}} {0} ||", unit.BaseDefense);
			writer.WriteLine("||Alcance||{{^}} {0}||", unit.Range);
			writer.WriteLine("||Dano||{{^}} {0}-{1} ||", unit.MinimumDamage, unit.MaximumDamage);
			writer.WriteLine("||Vida||{{^}} {0}||", unit.HitPoints);
			writer.WriteLine("||Tipo de Movimento||{{^}} {0}||", unit.MovementTypeDescription);
			writer.WriteLine("||Custo de Movimento||{{^}} {0}||", unit.MovementCost);
			writer.WriteLine("||Nivel||{{^}} {0}||", unit.Level);
			writer.WriteLine("||Consegue Retaliar||{{^}} {0}||", unit.CanStrikeBack?info.getContent("sim"):info.getContent("nao"));
			writer.WriteLine("*Modificadores de Ataque*");
			WriteUnitTargets(writer, unit.AttackTargets);
			writer.WriteLine("*Modificadores de Defesa*");
			WriteUnitTargets(writer, unit.DefenseTargets);
		}
		
		private void WriteUnitTargets( StreamWriter writer, Hashtable targets )
		{
			if( targets == null || targets.Count == 0 ) {
				writer.WriteLine("\t\t* ''Sem modificadores''");
				return;
			}
			
			IDictionaryEnumerator it = targets.GetEnumerator();
			while( it.MoveNext() ) {
				IDictionaryEnumerator target = ((Hashtable) it.Value).GetEnumerator();
				while( target.MoveNext() ) {
					int val = (int) target.Value;
					string plus = string.Empty;
					if( val > 0 ) {
						plus = "+";
					}
					writer.WriteLine("\t\t* *{0}* - *{1}*: {3}{2}", it.Key, target.Key, val, plus);
				}
			}
		}
		
		private void AppendDescription( ResourceFactory factory, StreamWriter writer, DirectoryInfo dir)
		{
			string name = string.Format("{0}Desc", info.getContent(factory.Name).Replace(" ","") );
			string fileName = Path.Combine(dir.FullName, name+".wiki");
			if( File.Exists(fileName) ) {
				writer.WriteLine("{{{{{0}}}}}", fileName);
			}
		}
		
		private string getDesc( string fact )
		{
			try {
				return info.getContent(fact + "_description");
			} catch {
				return null;
			}
		}
		
		/// <summary>Escreve atributos numa hash</summary>
		private void writeDuration( StreamWriter writer, ResourceFactory factory )
		{
			writer.WriteLine("!!! {0}", info.getContent("Duration"));
			writer.Write("\t* {0} turno(s)", factory.Duration.Value);
			if( factory.Duration.Quantity > 1 ) {
				writer.WriteLine(" por cada {0} unidades", factory.Duration.Quantity);
			} else {
				writer.WriteLine();
			}
			if( factory.Duration.Dependecy != null ) {
				writer.WriteLine("\t* Influenciado por {0}.[{1}]", GetCategory(factory.Duration.Dependecy), factory.Duration.Dependecy);
			}
		}

		/// <summary>Escreve modificadores duma hash</summary>
		private void writeModifiers( StreamWriter writer, string title, Hashtable hash )
		{
			if( hash == null || hash.Count == 0 ) {
				return;
			}
			
			writer.WriteLine("!!!Modificadores");
			IDictionaryEnumerator it = hash.GetEnumerator();
			while( it.MoveNext() ) {
				int v = (int) it.Value;
				string val = (v<0?v.ToString():"+"+v.ToString());
				string caption = it.Key.ToString();
				writer.WriteLine("\t* Intrinsic.[{0}]: {1} Por Turno", caption, val);
			}
		}
		
		/// <summary>Escreve atributos duma hash</summary>
		private void writeAtts( StreamWriter writer, string title, Hashtable hash )
		{
			if( hash == null || hash.Count == 0 ) {
				return;
			}
		
			writer.WriteLine("!!!Atributos");

			IDictionaryEnumerator it = hash.GetEnumerator();
			while( it.MoveNext() ) {
				string caption = Translate(it.Key.ToString());
				writer.WriteLine("\t* {0}: {1}", caption, it.Value);
			}
		}
		
		private string Translate( string str )
		{
			try {
				return info.getContent(str);
			} catch {
				return string.Format("''{0}''",str);
			}
		}
		
		/// <summary>Escreve as dependências</summary>
		private void writeActions( StreamWriter writer, string title, Action[] actions )
		{
			if( actions == null ) {
				return;
			}
			
			writer.Write("!!!");
			writer.WriteLine(info.getContent(title));

			foreach( Action action in actions ) {
				writer.WriteLine("\t* {0}",
						string.Format(info.getContent(action.Name), getParams(action.Params))
					);
			}
		}

		/// <summary>Retorna a Hash a mostrar</summary>
		private ResourceBuilder getHash(string cat)
		{
			Hashtable root = (Hashtable) all["planet"];
			if( root != null && root.ContainsKey(cat) ) {
				return (ResourceBuilder) root[cat];
			}
			
			root = (Hashtable) all["ruler"];
			if( root != null && root.ContainsKey(cat) ) {
				return (ResourceBuilder) root[cat];
			}
			
			return null;
		}
		
		/// <summary>Retorna parâmetros internacionalizados</summary>
		private string[] getParams( string[] original )
		{
			string[] result = new string[original.Length];
			for( int i = 0; i < original.Length; ++i ) {
				if( !OrionGlobals.isInt(original[i]) ) {
					result[i] = info.getContent(original[i]);
					string category = GetCategory(original[i]);
					if( category != null ) {
						result[i] = string.Format("{0}.[{1}]", category, original[i]); 
					}
				} else {
					result[i] = original[i];
				}
			}
			return result;
		}
		
		private string GetCategory( string resource )
		{
			IDictionaryEnumerator entities = all.GetEnumerator();
			while( entities.MoveNext() ) {
				Hashtable builder = (Hashtable) entities.Value;
				IDictionaryEnumerator fact = builder.GetEnumerator();
				while( fact.MoveNext() ) {
					ResourceBuilder factories = (ResourceBuilder) fact.Value;
					if( factories.ContainsKey(resource) ) {
						ResourceFactory factory = (ResourceFactory) factories[resource];
						if( factory.Name == resource ) {
							return fact.Key.ToString();
						}
					}
				}
			}
			
			if( categories.ContainsKey(resource) ) {
				return resource;
			}
			
			return null;
		}
		
		private void writeResourcesThatNeedThis( StreamWriter writer, ResourceFactory factory )
		{
			try {
				ResourceFactory[] factories = FetchResources(factory, new ResourceAction(this.ResourcesNeeded));
				if( factories.Length == 0 ) {
					return;
				}
				writer.WriteLine("!!! {0}", info.getContent("RelatedTo"));
				foreach( ResourceFactory related in factories ) {
					writer.WriteLine("\t* {0}", GetLink(related));
				}
			} catch( Exception ex ) {
				throw new Exception("Error writing factory " + factory.Name, ex);
			}
		}
		
		private void writeResourcesProvided( StreamWriter writer, ResourceFactory factory )
		{
			try {
				ResourceFactory[] factories = FetchResources(factory, new ResourceAction(this.ResourcesProvided));
				if( factories.Length == 0 ) {
					return;
				}
				writer.WriteLine("!!! {0}", info.getContent("WhoDependsOfThisResource"));
				foreach( ResourceFactory related in factories ) {
					writer.WriteLine("\t* {0}", GetLink(related));
				}
			} catch( Exception ex ) {
				throw new Exception("Error writing factory " + factory.Name, ex);
			}
		}
		
		private ResourceFactory[] FetchResources( ResourceFactory source, ResourceAction action )
		{
			ArrayList list = new ArrayList();
		
			IDictionaryEnumerator entities = all.GetEnumerator();
			while( entities.MoveNext() ) {
				Hashtable builder = (Hashtable) entities.Value;
				IDictionaryEnumerator fact = builder.GetEnumerator();
				while( fact.MoveNext() ) {
					ResourceBuilder factories = (ResourceBuilder) fact.Value;
					foreach( ResourceFactory factory in factories.Values ) {
						if( action(source, factory) ) {
							list.Add(factory);
						}
					}
				}
			}
			
			return (ResourceFactory[]) list.ToArray( typeof(ResourceFactory) );
		}
		
		private string GetLink( ResourceFactory resource )
		{
			try {
				string category = resource.Category; 
				category = ToSimpleCharaters(category);
				
				return string.Format("{0}.[{1}]", category, resource.Name);
			} catch( Exception ex ) {
				Console.WriteLine("*$ Error Generating link for '{0}'", resource.Name);
				return "[ERROR] " + resource.Name;
			}
		}
		
		private string ToSimpleCharaters( string str )
		{
			str = str.Replace("ã", "a");
			str = str.Replace("Ã", "A");
			str = str.Replace("á", "a");
			str = str.Replace("Á", "A");
			str = str.Replace("à", "a");
			str = str.Replace("À", "A");
			
			str = str.Replace("ç", "c");
			
			str = str.Replace("é", "e");
			str = str.Replace("É", "E");
			
			str = str.Replace("í", "i");
			str = str.Replace("Í", "I");
			
			str = str.Replace("ó", "o");
			str = str.Replace("Ó", "O");
			str = str.Replace("õ", "o");
			str = str.Replace("Õ", "O");
			
			str = str.Replace("ú", "u");
			str = str.Replace("Ú", "u");
			
			return str;
		}
		
		#endregion
		
		#region ResourceActions
		
		private bool ResourcesProvided( ResourceFactory source, ResourceFactory factory )
		{
			try {
				if( factory.Dependencies == null ) {
					return false;
				}
				
				foreach( Action action in factory.Dependencies ) {
					if( action is ResourceRef ) {
						ResourceRef rref = (ResourceRef) action;
						if( rref.Key == source.Category && rref.Value == source.Name ) {
							return true;
						}
					}
				}
				
				return false;
			} catch( Exception ex ) {
				throw new Exception("Error Operating " + source.Name + " over " + factory.Name);
			}
		}
		
		private bool ResourcesNeeded( ResourceFactory source, ResourceFactory factory )
		{
			try {
				
				if( CheckOnTurnActions(source, factory ) ) {
					return true;
				}
				
				if( CheckOnCompleteActions(source, factory ) ) {
					return true;
				}
				
				if( source.Category == "Rare" ) {
					bool check = CheckCostActions(source, factory);
					if( check ) {
						return true;
					}
				}
				
				return CheckModifiers(source, factory);
				
			} catch( Exception ex ) {
				throw new Exception("[ResourcesNeeded] Error Operating " + source.Name + " over " + factory.Name, ex);
			}
		}
		
		private bool CheckModifiers( ResourceFactory source, ResourceFactory factory )
		{
			try {
				if( factory.Modifiers == null ) {
					return false;
				}
		
				foreach( string mod in factory.Modifiers.Keys ) {
					if( mod == source.Name ) {
						return true;
					}
				}
		
				return false;
			} catch( Exception ex ) {
				Console.WriteLine("*$ ERROR: {0}", ex.ToString());
				return false;
			}
		} 
		
		private bool CheckOnTurnActions( ResourceFactory source, ResourceFactory factory )
		{
			if( factory.OnTurnActions == null ) {
				return false;
			}
			
			foreach( Action action in factory.OnTurnActions ) {
				if( action is TransformRareResource ) {
					TransformRareResource rref = (TransformRareResource) action;
					if( rref.Rare == source.Name || rref.Intrinsic == source.Name ) {
						return true;
					}
				}
			}
			
			return false;
		}
		
		private bool CheckOnCompleteActions( ResourceFactory source, ResourceFactory factory )
		{
			if( factory.OnCompleteActions == null ) {
				return false;
			}
			
			foreach( Action action in factory.OnCompleteActions ) {
				if( action is Add ) {
					Add rref = (Add) action;
					if( rref.Key == source.Category && rref.Value == source.Name ) {
						return true;
					}
				}
			}
			
			return false;
		}
		
		private bool CheckCostActions( ResourceFactory source, ResourceFactory factory )
		{
			if( factory.CostActions == null ) {
				return false;
			}
			
			foreach( Action action in factory.CostActions ) {
				if( action is ResourceNeeded ) {
					ResourceNeeded rref = (ResourceNeeded) action;

					
					if( rref.Key == source.Name ) {
						return true;
					}
				}
			}
			
			return false;
		}
		
		#endregion
		
		#region Unit Rendering
		
		private void WriteUnitsTerrainHandicap( DirectoryInfo dir )
		{
			Console.Write("Generating 'Unit/UnidadesPorTerreno.wiki'... ");
			ResourceBuilder ships = getHash("Unit");
		
			StreamWriter writer = new StreamWriter(Path.Combine(dir.FullName, "UnidadesPorTerreno.wiki"), false, System.Text.Encoding.UTF8);
			writer.WriteLine(":Summary: {0}", info.getContent("UnidadesPorTerreno"));
			writer.WriteLine(":Display: {0}", info.getContent("UnidadesPorTerreno"));
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(":Parent: Unit.Unit");
			writer.WriteLine(info.getContent("UnidadesPorTerrenoIntro"));
			
			writer.WriteLine("!!! Unidades Por Tipo de Terreno");
			writer.WriteLine("||{!^}*Tipo de Terreno*||{!^}*Ataque*||{!^}*Defesa*||");
			foreach( Terrain terrain in Terrain.All ) {
				writer.Write("||{0}||@@[", info.getContent(terrain.Description));
				if( !WriteFactories(writer, ships, terrain.Description, "terrain", true) ) {
					writer.Write("\" \"");
				}
				writer.Write("]@@||");
				writer.Write("@@[");
				if( !WriteFactories(writer, ships, terrain.Description, "terrain", false) ) {
					writer.Write("\" \"");
				}
				writer.WriteLine("]@@||");
			}
			
			writer.Close();
			Console.WriteLine("Ok");
		}
		
		private void WriteUnitsCategoryHandicap( DirectoryInfo dir )
		{
			Console.Write("Generating 'Unit/UnidadesPorCategoria.wiki'... ");
			ResourceBuilder ships = getHash("Unit");
		
			StreamWriter writer = new StreamWriter(Path.Combine(dir.FullName, "UnidadesPorCategoria.wiki"), false, System.Text.Encoding.UTF8);
			writer.WriteLine(":Summary: {0}", info.getContent("UnidadesPorCategoria"));
			writer.WriteLine(":Display: {0}", info.getContent("UnidadesPorCategoria"));
			writer.WriteLine(":Parent: Unit.Unit");
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(info.getContent("UnidadesPorCategoriaIntro"));
			
			writer.WriteLine("!!! Unidades Por Categoria");
			writer.WriteLine("||{!^}*Categoria*||{!^}*Ataque*||{!^}*Defesa*||");
			foreach( string cat in UnitTypes ) {
				string category = cat.ToLower();
				writer.Write("||{0}||@@[", cat);
				if( !WriteFactories(writer, ships, category, "unit", true) ) {
					writer.Write("\" \"");
				}
				writer.Write("]@@||");
				writer.Write("@@[");
				if( !WriteFactories(writer, ships, category, "unit", false) ) {
					writer.Write("\" \"");
				}
				writer.WriteLine("]@@||");
			}
			
			writer.Close();
			Console.WriteLine("Ok");
		}
		
		private void WriteUnitsLevelHandicap( DirectoryInfo dir )
		{
			Console.Write("Generating 'Unit/UnidadesPorPosicao.wiki'... ");
			ResourceBuilder ships = getHash("Unit");
		
			StreamWriter writer = new StreamWriter(Path.Combine(dir.FullName, "UnidadesPorPosicao.wiki"), false, System.Text.Encoding.UTF8);
			writer.WriteLine(":Display: {0}", info.getContent("UnidadesPorPosicao"));
			writer.WriteLine(":Summary: {0}", info.getContent("UnidadesPorPosicao"));
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(":Parent: Unit.Unit");
			writer.WriteLine(info.getContent("UnidadesPorPosicaoIntro"));
			
			writer.WriteLine("!!! {0}", info.getContent("UnidadesPorPosicao"));
			writer.WriteLine("||{!^}*Categoria*||{!^}*Ataque*||{!^}*Defesa*||");
			foreach( string level in Levels ) {
				writer.Write("||{0}||@@[", level);
				if( !WriteFactories(writer, ships, level, "level", true) ) {
					writer.Write("\" \"");
				}
				writer.Write("]@@||");
				writer.Write("@@[");
				if( !WriteFactories(writer, ships, level, "level", false) ) {
					writer.Write("\" \"");
				}
				writer.WriteLine("]@@||");
			}
			
			writer.Close();
			Console.WriteLine("Ok");
		}
		
		private bool WriteFactories( StreamWriter writer, ResourceBuilder ships, string target, string root, bool attack )
		{
			bool first = true;
			
			foreach( ResourceFactory factory in ships.Values ) {
				Hashtable hash = factory.Unit.DefenseTargets;
				if( attack ) {
					hash = factory.Unit.AttackTargets;
				}
				int handicap = factory.Unit.AddUp(hash, root, target);
				if( handicap == 0 ) {
					continue;
				}
				if( !first ) {
					writer.Write(",");
				} 
				first = false;
				writer.Write("\"\t* {0} ",GetLink(factory));
				if( handicap > 0 ) {
					writer.Write("+");
				}
				writer.Write("{0}\", Newline", handicap);
			}
			
			return !first;
		}
		
		private void WriteUnitsByMovement( DirectoryInfo dir )
		{
			Console.Write("Generating 'Unit/UnidadesPorTipoDeMovimento.wiki'... ");
			ResourceBuilder ships = getHash("Unit");
		
			StreamWriter writer = new StreamWriter(Path.Combine(dir.FullName, "UnidadesPorTipoDeMovimento.wiki"), false, System.Text.Encoding.UTF8);
			writer.WriteLine(":Summary: {0}", info.getContent("UnidadesPorTipoMovimento"));
			writer.WriteLine(":Display: {0}", info.getContent("UnidadesPorTipoMovimento"));
			writer.WriteLine(":CreatedBy: Orion's Belt Documentation Generator");
			writer.WriteLine(":Parent: Unit.Unit");
			writer.WriteLine(info.getContent("UnidadesPorTipoMovimentoIntro"));
			
			writer.WriteLine("!!! Unidades Por Tipo de Movimento");
			writer.WriteLine("||{!^}*Tipo de Movimento*||{!^}*Unidade*||");
			foreach( string mov in MovementTypes ) {
				writer.Write("||{0}||@@[", mov);
				if( !WriteUnitByMovement(writer, ships, mov) ) {
					writer.Write("\" \"");
				}
				writer.WriteLine("]@@||");
			}
			
			writer.Close();
			Console.WriteLine("Ok");
		}
		
		private bool WriteUnitByMovement( StreamWriter writer, ResourceBuilder ships, string mov )
		{
			bool first = true;
			
			foreach( ResourceFactory factory in ships.Values ) {
				if( factory.Unit.MovementTypeDescription != mov ) {
					continue;
				}
				if( !first ) {
					writer.Write(",");
				} 
				first = false;
				writer.Write("\"\t* {0}\", Newline ",GetLink(factory));
			}
			
			return !first;
		}
		
		#endregion

	};
}
