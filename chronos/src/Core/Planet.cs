//#define DEBUG_DestroyRandomBuildings

using System;
using System.Collections;
using Chronos.Exceptions;
using Chronos.Info;
using Chronos.Info.Results;
using Chronos.Interfaces;
using Chronos.Messaging;
using Chronos.Resources;
using Chronos.Utils;

namespace Chronos.Core {

	/// <summary>Class que representa um Planeta</summary>
	[Serializable]
	public class Planet : ResourceManager, IBattle {

		#region Instance Fields

		private string _name;
		private PlanetInfo info;

		private Hashtable _fleets;
		private Coordinate _coordinate;
		private bool _isVisible;
		private int builingsDemolished;
		private int turnsSinceBattle = 0;

		private bool initMade = false;

		// indica se o planeta est em alguma batalha
		private bool _isInBattle = false;

		private Ruler oldOwner = null;
		private TaskManager tasks = null;
		private Ruler _wonABattle = null;

		#endregion

		#region properties

		/// <summary>
		/// propriedade que representa o nome do planeta
		/// </summary>
		public string Name {
			get { return _name; }
			set {
				if( _fleets != null && _fleets.Count > 0 ) {
					Fleet tmp = (Fleet)_fleets[_name];
					_fleets.Remove(_name);
					tmp.Name = value;
					_fleets.Add( value, tmp );
				}
				_name = value;
			}
		}
		
		/// <summary>
		/// propriedade que representa a corrdenada do planeta
		/// </summary>
		public Coordinate Coordinate {
			get { return _coordinate; }
			set {  }
		}
		
		/// <summary>Obtém o TaskManager associado</summary>
		public TaskManager Tasks {
			get {
				if( tasks == null ) {
					tasks = new TaskManager();
				}
				return tasks;
			}
		}

		/// <summary>Indica se este planeta é visível a outros</summary>
		public bool IsVisible {
			get { return _isVisible; }
			set { _isVisible = value; }
		}
		
		/// <summary>Retorna o PlanetInfo deste planeta</summary>
		public PlanetInfo Info {
			get { return info; }
			set { info = value; }
		}

		/// <summary>Indica se o planeta já foi inicializado</summary>
		public bool InitMade {
			get{ return initMade;}
		}

		/// <summary>Indica o dono antigo</summary>
		public Ruler OldOwner {
			get{ return oldOwner; }
			set{ oldOwner = value; }
		}

		public Ruler WonABattle {
			get{ return _wonABattle; }
			set{ _wonABattle = value; }
		}
		
		/// <summary>Indica se o planeta  um home planet</summary>
		public bool IsHomePlanet {
			get {
				return Coordinate.Planet == 1;
			}
		}

		public bool HasDefenses {
			get {
				return isResourceAvailable("Building", "Turret") || isResourceAvailable("Building", "IonCannon");
			}
		}
		
		public int BuilingsDemolished {
			get { return builingsDemolished; }
			set { builingsDemolished = value; }
		}
		
		public int Immunity {
			get { return turnsSinceBattle; }
			set { turnsSinceBattle = value; }
		}

		public bool HasImmunity {
			get { return turnsSinceBattle > 0; }
		
		}
	
		#endregion
		
		#region Resource Properties
		
		/// <summary>Indica a quantidade de Energy disponvel</summary>
		public int Energy {
			get { return getResourceCount("energy"); }
		}
		
		/// <summary>Indica a quantidade de Gold disponvel</summary>
		public int Gold {
			get { return getResourceCount("gold"); }
		}
		
		/// <summary>Indica a quantidade de MP disponvel</summary>
		public int MP {
			get { return getResourceCount("mp"); }
		}
		
		/// <summary>Indica a quantidade de Labor disponvel</summary>
		public int Labor {
			get { return getResourceCount("labor"); }
		}
		
		/// <summary>Indica a quantidade de MArines disponvel</summary>
		public int Marines {
			get { return getResourceCount("marine"); }
		}
		
		/// <summary>Indica a quantidade de Spies disponvel</summary>
		public int Spies {
			get { return getResourceCount("spy"); }
		}
		
		/// <summary>Indica a quantidade de MArines disponvel</summary>
		public int Population {
			get { return Labor + Marines + Spies; }
		}
		
		/// <summary>Indica a quantidade de Cultura disponvel</summary>
		public int Culture {
			get { return getResourceCount("culture"); }
		}
		
		/// <summary>Indica a quantidade de Habitaes disponvel</summary>
		public int Housing {
			get { return getResourceCount("housing"); }
		}
		
		/// <summary>Indica a quantidade de Comida disponvel</summary>
		public int Food {
			get { return getResourceCount("food"); }
		}
		
		/// <summary>Indica a quantidade de Poluição disponvel</summary>
		public int Polution {
			get { return getResourceCount("polution"); }
		}
		
		/// <summary>Indica a quantidade de Pontuao<summary>
		public int Score {
			get { return getResourceCount("score"); }
		}
		
		/// <summary>Indica a quantidade de Espao em Terra<summary>
		public int GroundSpace {
			get { return getResourceCount("groundSpace"); }
		}
		
		/// <summary>Indica a quantidade de Espao em rbita<summary>
		public int OrbitSpace {
			get { return getResourceCount("orbitSpace"); }
		}
		
		/// <summary>Indica a quantidade de Espao em Terra<summary>
		public int WaterSpace {
			get { return getResourceCount("waterSpace"); }
		}
		
		/// <summary>Indica a quantidade mxima de Espao em Terra</summary>
		public int TotalGroundSpace {
			get {
				if( !IsHomePlanet ) {
					return Info.GroundSpace;
				}
				
				return GetDefaultValue("groundSpace") + 7;
			}
		}
		
		/// <summary>Indica a quantidade mxima de Espao em gua</summary>
		public int TotalWaterSpace {
			get {
				if( !IsHomePlanet ) {
					return Info.WaterSpace;
				}
				
				return GetDefaultValue("waterSpace");
			}
		}
		
		/// <summary>Indica a quantidade mxima de Espao em rbita</summary>
		public int TotalOrbitSpace {
			get {
				if( !IsHomePlanet ) {
					return Info.OrbitSpace;
				}
				
				return GetDefaultValue("orbitSpace");
			}
		}
		
		public double LaborInfluence {
			get { return Labor / 1000; }
		}
		
		public double PolutionInfluence {
			get { return Polution / 500; }
		}
		
		public double CultureInfluence {
			get {
				if( Culture > 0 ) {
					 return Culture / 10000;
				} else {
					return Culture / 500;
				}
			}
		}
		
		public double HousingInfluence {
			get { 
				if( Population > Housing ) {
					return 20;
				} 
				return 0;
			}
		}
		
		/// <summary>Indica a percentagem a incidir sobre a produção</summary>
		public override double ProductionFactor {
			get {
				if( IsInBattle ) {
					return 5.00;
				}
			
				return RawProductionFactor;
			}
		}

		/// <summary>Indica a percentagem a incidir sobre a produção</summary>
		public double RawProductionFactor {
			get {
				double percent = 0;

				percent -= LaborInfluence;
				percent += PolutionInfluence;
				percent -= CultureInfluence;
				percent += HousingInfluence;				

				if( percent > 50 ) {
					percent = 50;
				}
				if( percent < -35 ) {
					percent = -35;
				}

				percent += FarAwayFromHomeFactor;

				return 1 + (percent/100);
			}
		}
		
		/// <summary>Indica a penalização por o planeta estar longe do HomePlanet</summary>
		public double FarAwayFromHomeFactor {
			get {
				Ruler ruler = (Ruler) Owner;
				Coordinate hp = ruler.HomePlanet.Coordinate;
				
				return GenerateFarAwayFromHomeFactor( hp, this.Coordinate );
			}
		}
		
		#endregion
		
		#region Fleet

		/// <summary>
		/// permite obter o objecto que representa a fleet que tem o nome 'name'
		/// e que faz parte deste planeta.
		/// </summary>
		/// <param name="name">nome da fleet</param>
		/// <returns>objecto que representa a fleet</returns>
		public Fleet getFleet( string name ) {
			if( name == null || name == string.Empty )
				throw new RuntimeException("Invalid Fleet Name" );
			
			Fleet fleet = (Fleet)_fleets[name];

			if( fleet == null)
				throw new RuntimeException("Fleet '"+name+"' not found in Planet " + _name );
			
			return fleet;
		}

		/// <summary>
		/// retorna a fleet de defesa do planet
		/// </summary>
		/// <returns>a fleet por defeito do planet</returns>
		public Fleet getDefenseFleet() {
			return getFleet( "defenseFleet" );
		}

		/// <summary>
		/// retorna a fleet do planet
		/// </summary>
		/// <returns>a fleet por defeito do planet</returns>
		public Fleet getPlanetFleet() {
			return getFleet( Name );
		}

		/// <summary>
		/// adiciona uma fleet nova a este planeta
		/// </summary>
		/// <param name="name">nome da fleet</param>
		public bool addFleet( string name ) {
			if( ((Ruler)Owner).hasFleet( name ) ) {
				return false;
			}
			
			_fleets.Add( name, new Fleet( name, _coordinate, this ) );
			return true;
		}

		/// <summary>
		/// adiciona uma fleet existente a este planeta
		/// </summary>
		/// <param name="fleet">fleet a adicionar</param>
		public bool addFleet( Fleet fleet ) {
			if( null == fleet ) {
				throw new RuntimeException("O Object fleet que se est a tentar adicionar ao planeta "+ Name +" est a null");
			}

			_fleets.Add( fleet.Name, fleet );
			fleet.Owner = this;
			fleet.Coordinate = Coordinate;
			return true;
		}

		public void removeFleet( Fleet fleet) {
			if( fleet == null ) {
				throw new RuntimeException(" Objecto fleet é null @ Planet.removeFleet");
			}

			_fleets.Remove( fleet.Name );
		}

		public void FleetsToOrbit() {
			IDictionaryEnumerator iter = Fleets.GetEnumerator();
			while( iter.MoveNext() ) {
				Fleet fleet = (Fleet)iter.Value;
				if( fleet.IsMoveable && !fleet.IsDefenseFleet ) {
					removeFleet( fleet );
					if(fleet.HasShips) {
						Ruler ruler = Owner.Owner as Ruler;
						if( ruler == null ) {
							//para evitar eventuais erros
							if( Owner is Ruler ) {
								ruler = Owner as Ruler;
							}else {
								//Se cair aqui é pq houve um erro grave e então remove a fleet e continua;
								continue;
							}
						}
						ruler.addUniverseFleet(fleet);
						fleet.Owner = ruler;
					}
					iter = Fleets.GetEnumerator();
				}
			}
		}

		/// <summary>
		/// retorna todas as Fleets
		/// </summary>
		public Hashtable Fleets {
			get { return _fleets; }
		}

		/// <summary>
		/// verifica se o planeta possui esta fleet
		/// </summary>
		/// <param name="name">nome da fleet</param>
		/// <returns><code>true</code> se este planeta tiver a fleet,<code>false</code> caso contrário</returns>
		public bool hasFleet( string name ) {
			return _fleets.ContainsKey( name );
		}
		
		/// <summary>Indica o nmero total de naves, independentemente do seu tipo</summary>
		public int TotalShips {
			get {
				if( Fleets == null ) {
					return 0;
				}
				int count = 0;
				foreach( Fleet fleet in Fleets.Values ) {
					count += fleet.TotalUnits;
				}
				return count;
			}
		}
		
		#endregion
		
		#region Static Methods
		
		/// <summary>Gera um ID único</summary>
		private static int generateId() {
			return Universe.instance.generatePlanetId();
		}
		
		public static string IdentifierString {
			get { return "planet"; }
		}
		
		public static double GenerateFarAwayFromHomeFactor( Coordinate reference, Coordinate another )
		{	
			if( another.Galaxy != reference.Galaxy ) {
				return 75;
			}

			if( another.System > reference.System + 3 ) {
				return 38;
			}
			
			if( another.System < reference.System - 3 ) {
				return 38;
			}
			
			if( reference.Equals(another) ) {
				return -5;
			}
			
			int systemFactor = Math.Abs(another.System - reference.System);
			if( systemFactor != 0 ) {
				return systemFactor * 3;
			}
			
			int sectorFactor = Math.Abs(another.Sector - reference.Sector);

			switch(sectorFactor) {
				case 0: return -4;
				case 1: return -3;
				case 2: return -2;
				case 3: return -1;
				default: return 0;
			}
		}
		
		#endregion
		
		#region Ctors

		/// <summary>Construtor</summary>
		/// <remarks>
		///  O Planet vai-se construir com a Hashtable que contém todos os recursos.
		///  Assim que povoar o seu contentor de All, vai ver quais é que estão disponíveis
		///  e coloca-as na lista Available (ou seja  todas as Factories sem dependências).
		///  Este construtor S  USADO para construir planetas por defeito
		/// </remarks>
		public Planet( Ruler ruler, Hashtable allFactories, string planetName , Coordinate coordinate )
			: base(ruler,allFactories, IdentifierString, generateId()) {
			if( null == ruler )
				throw new RuntimeException("Ruler é null @ Planet::Planet");

			info = PlanetInfo.DefaultPlanetInfo;
			_coordinate = coordinate;

			setUpPlanet(true);

			if( planetName == null || planetName.Length == 0 ) {
				Name =  Id + "_Planet";
			} else {
				Name = planetName;
			}
			
			_fleets = new Hashtable();
			_fleets.Add( planetName, new Fleet( planetName, _coordinate, this, false ) );

			Fleet defense = new Fleet( "defenseFleet", _coordinate, this, false );
			defense.IsDefenseFleet = true;
			_fleets.Add( "defenseFleet", defense );

			registerEvents();
			
			addToRuler(ruler);

			IsVisible = false;
			initMade = true;
		}
		
		/// <summary>
		/// Constroi um planeta fantasma.  uma planeta que existe,
		/// mas que ainda não é de ninguém.
		/// </summary>
		/// <param name="coordinate">Coordenada do planeta</param>
		public Planet( Coordinate coordinate ) {
			_coordinate = coordinate;
			info = PlanetInfo.Random;
			IsVisible = true;
		}

		/// <summary>
		/// inicializa um planeta sem dono
		/// </summary>
		public void init( Ruler ruler, string planetName, Hashtable allFactories  ) {
			if( null == ruler )
				throw new RuntimeException("Ruler é null @ Planet::init");
			
			base.init( ruler, allFactories,IdentifierString, generateId() );
			setUpPlanet(false);

			if( planetName == null || planetName.Length == 0 ) {
				Name =  Id + "_Planet";
			} else {
				Name = planetName;
			}
			
			_fleets = new Hashtable();
			_fleets.Add( planetName, new Fleet( planetName, _coordinate, this, false ) );
			
			Fleet defense = new Fleet( "defenseFleet", _coordinate, this, false );
			defense.IsDefenseFleet = true;
			_fleets.Add( "defenseFleet", defense );

			registerEvents();
			
			addToRuler(ruler);

			initMade = true;
		}
		
		public new void FullReset() 
		{
			base.FullReset();

			Name = string.Empty; 
			
			_fleets = null;
			initMade = false;
		}
		
		#endregion
		
		#region Registered Events
		
		/// <summary>Regista os eventos que este objecto quer apanhar</summary>
		public void registerEvents() {
			ResourceInfo.ResourceNotification notify = new ResourceInfo.ResourceNotification(shipBuildComplete);

			getResourceInfo("Unit").OnComplete += notify;
		}
		
		/// <summary>Chamado quando naves são criadas</summary>
		public void shipBuildComplete( Resource res, int quantity ) {
			Fleet fleet = getDefenseFleet();
			if( fleet.TotalUnitTypes >= Fleet.MaxUnitTypes /*&& !fleet.ShipsResources.ContainsValue(res)*/ )  {
				fleet = (Fleet)_fleets[Name];
			}
			fleet.addShip(res.Name,quantity);
		}
		
		#endregion
		
		#region Utility methods
		
		/// <summary>Regista o planeta no ruler</summary>
		private void addToRuler( Ruler ruler ) {
			int idx = ruler.getIndex(this);
			if( idx < 0 ) {
				ruler.addPlanet(this);
			}
		}

		public void StartImmunity() {
			Immunity = 100;			
		}
		
		public void DestroyRandomBuildings( int count )
		{
			ResourceInfo buildings = getResourceInfo("Building");
			Hashtable resources = buildings.Resources;
			
			for( int i = 0; i < count; ++i ) {
				int rnd = MathUtils.random(0, resources.Count);
				Resource lucky = null;
				int j = 0;
				foreach( Resource resource in resources.Keys ) {
					lucky = resource;
					if( rnd == j++ ) {
						break;
					}
				}
#if DEBUG_DestroyRandomBuildings
				Log.log("(Count:{1}) Removing 1 {0}", lucky.Name, count);
#endif
				if( take("Building", lucky.Name, 1) ) {
					Messenger.Send( this, "BuildingDestroyed", Name, lucky.Name);
				} 
			}
		}
		
		#endregion

		#region PlanetInfo
		
		/// <summary>Inicializa as características do planeta</summary>
		private void setUpPlanet( bool homePlanet )
		{
			if ( homePlanet ) {
				setUpHomePlanet();
			} else {
				setUpNormalPlanet();
			}
			registerModifier("food", 100);
			registerModifier("labor", 50);
			registerModifier("polution", -2);
			setUpRareResources();
		}
		
		/// <summary>Indica a quantidade inicial de determinado recurso</summary>
		public int GetDefaultValue( string intrinsic )
		{
			ResourceInfo info = getResourceInfo("Intrinsic");
			ResourceFactory factory = (ResourceFactory)info.AvailableFactories[intrinsic];
			if( factory == null ) {
				throw new Exception("Resource '" + intrinsic + "' not found");
			}
			Hashtable atts = factory.Attributes;
			if( atts == null ) {
				return 0;
			}
			
			object obj = atts["DefaultValue"];
			if ( null != obj ) {
				return int.Parse(obj.ToString());
			}
			
			return 0;
		}
		
		/// <summary>Inicializa o planeta com base no seu PlanetInfo</summary>
		private void setUpHomePlanet()
		{
			ResourceInfo info = getResourceInfo("Intrinsic");

			foreach( ResourceFactory factory in info.AvailableFactories.Values ) {
				Hashtable atts = factory.Attributes;
				if( atts == null ) {
					registerModifierRatio(factory.Name, 0);
					continue;
				}
				
				object obj = atts["DefaultRatio"];
				if ( null == obj ) {
					registerModifierRatio(factory.Name, 0, false);
				} else {
					registerModifierRatio(factory.Name, int.Parse(obj.ToString()), false );
				}
				
				obj = atts["DefaultValue"];
				if ( null != obj ) {
					addResource("Intrinsic", factory.Name, int.Parse(obj.ToString()));
				}
			}
			registerModifier("labor", 10);
			registerModifier("culture", 10);
		}
		
		/// <summary>Inicializa o planeta com base no seu PlanetInfo</summary>
		private void setUpNormalPlanet()
		{
			registerModifierRatio("food", info.FoodRatio );
			registerModifierRatio("labor", info.FoodRatio );
			registerModifierRatio("mp", info.MPRatio);
			registerModifierRatio("gold", info.GoldRatio);
			registerModifierRatio("energy", info.EnergyRatio);
			registerModifierRatio("polution", 100);
			registerModifierRatio("culture", 100);
			
			addResource("Intrinsic", "groundSpace", info.GroundSpace);
			addResource("Intrinsic", "waterSpace", info.WaterSpace);
			addResource("Intrinsic", "orbitSpace", info.OrbitSpace);
			
			registerModifier("labor", 10);
			registerModifier("culture", 10);
		}
		
		/// <summary>Inicializa os recursos raros</summary>
		private void setUpRareResources()
		{
			ResourceBuilder rare = Universe.getFactories("planet", "Rare");
			ArrayList list = new ArrayList();
			
			foreach( ResourceFactory factory in rare.Values ) {
				int terrain = factory.GetAtt("SpecificTerrain");
				if( terrain == -1 ) {
					list.Add(factory);
					continue;
				}
				if( terrain == Info.Terrain.Id ) {
					registerModifierRatio(factory.Name, factory.GetAtt("DefaultRatio"));
					registerRareModifier(factory.Name, MathUtils.random(17, 25) );
				}
			}
			
			ResourceFactory other = (ResourceFactory) list[ MathUtils.random(0, list.Count) ];
			registerModifierRatio( other.Name, other.GetAtt("DefaultRatio") );
			registerRareModifier(other.Name, MathUtils.random(17, 25) );
		}

		/// <summmary>Inicializa o Planeta</summary>
		public void init() {
			
		}

		#endregion
		
		#region IComparable

		/// <summary>Compara 2 Planetas, segundo o Id</summary>
		override public int CompareTo( object obj ) {
			Planet planet = (Planet) obj;
			return planet.Id.CompareTo(Id);
		}
		
		public override string ToString()
		{
			if( Name == null || Name == string.Empty ) {
				return "Home Planet";
			}
			return string.Format("{0} @ {1}", Name, Coordinate);
		}
		
		#endregion

		#region Battle

		/// <summary>Indica se o planeta está protegido</summary>
		public bool HasProtection {
			get {
				if( _fleets == null ) {
					return false;
				}
				return getDefenseFleet().HasShips || HasDefenses;
			}
		}

		public bool HasShips {
			get{
				foreach( Fleet f in Fleets.Values ) {
					if( f.HasShips ) {
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>
		/// verifica se este planeta está numa batalha
		/// </summary>
		public bool IsInBattle {
			get{ return _isInBattle; }
			set{ _isInBattle = value; }
		}

		public ArrayList GetBattleElements() {
			return getDefenseFleet().GetBattleElements();
		}

		public void SetBattleElements(ArrayList elements) {
			Fleet f = getDefenseFleet();
			f.SetBattleElements( elements);
		}

		#endregion

		#region IMessageHandler Implementation

		// Delega para o Ruler as mensagens que receber

		/// <summary>Aceita uma mensagem e armazena-a</summary>
		public override void acceptMessage( Message message ) {
			if( null == Owner )  {
				return;
			}

			IMessageHandler handler = (IMessageHandler) Owner;
			handler.acceptMessage(message);
		}

		#endregion

		#region ITask Implementation

		/// <summary>Efectua a passagem de turno do planeta</summary>
		public override void turn()
		{
			try {
				BuilingsDemolished = 0;
				base.turn();
				checkPolution();
				checkLabor();
				checkTasks();
				checkConsistency();
				if( turnsSinceBattle > 0 ) {
					--turnsSinceBattle;
				}
			} catch( Exception ex ){
				Log.log("EXCEPTION: " + ex);
				Ruler ruler = (Ruler) Owner;
				
				string msg = string.Format("Planet.turn() error! PlanetId:{2} Ruler:{0} UserId:{1}", ruler.Id, ruler.ForeignId, Id);
				
				Universe.Events.turnError( new Exception(msg, ex) );
				Universe.Events.trace("[EXCEPTION] `{0}' Ruler:{1} UserId: ", ex, ruler.Id, ruler.ForeignId);
			}
		}
		
		/// <summary>Indica se  para aplicao um factor de atenuao</summary>
		public override bool Attenuation  {
			get { return false; }
		}
		
		/// <summary>Corre Tarefas Registadas</summary>
		private void checkTasks()
		{
			if( tasks == null ) {
				return;
			}
			
			tasks.turn();
		}
		
		private void checkConsistency()
		{
			try {
				if( Housing == 0 ) {
					addResource("Intrinsic", "housing", 1000 );
				}
				int mod = (int) Modifiers["mp"];
				if( mod == 0 && MP < 1000 ) {
					registerModifier("mp", 50);
				}
				mod = (int) Modifiers["gold"];
				if( mod == 0 && Gold < 1000 ) {
					registerModifier("gold", 50);
				}
				
				mod = (int) Modifiers["labor"];
				if( mod == 0 && Labor < 1000 ) {
					registerModifier("labor", 50);
				}
			} catch ( Exception ex ) {
				Universe.Events.turnError(ex);
			}
		}
		
		/// <summary>Verifica se está tudo em ordem com a poluição</summary>
		private void checkPolution()
		{
			if( Polution < 0 ) {
				addResource("Intrinsic", "polution", -Polution);
			}
		}
		
		/// <summary>Verifica se está tudo em ordem com a população</summary>
		private void checkLabor()
		{
			int food = getResourceCount("Intrinsic", "food");
			int foodMod = (int) Modifiers["food"];
			
			if( food < 0 && foodMod < 0 ) {
				int val = MathUtils.round(-foodMod*3/2);
				take("Intrinsic", "labor", val );
				take("Intrinsic", "culture", 30 );
				Messenger.Send((IMessageHandler)Owner, "LaborDying", Name, "food");
			}
			
			if( food < 0 ) {
				addResource("Intrinsic", "food", -food);
			}
			
			if( Labor <= 0 ) {
				addResource("Intrinsic", "labor", -Labor);
				Modifiers["food"] = 100;
			}
		}
		
		#endregion

		#region Scan
		
		/// <summary>Indica o custo de um scan</summary>
		public int ScanCost {
			get { return getResourceCount("Intrinsic", "scanCost"); }
		}
		
		/// <summary>Indica se  possivel adicionar um Scan</summary>
		public Result canScan( Coordinate destiny )
		{
			Result result = PlanetScanner.CanPerformScan(this, destiny);
			if( Energy < ScanCost ) {
				result.failed( new ResourceQuantityNotAvailable("energy") );
			}
			if( result.Ok ) {
				result.passed( new OperationSucceded() );
			}
			return result;
		}
		
		/// <summary>Realiza um novo scan</summary>
		public Scan performScan( Coordinate destiny )
		{
			Scan scan = PlanetScanner.PerformScan(this, destiny);
			take("energy", ScanCost);
			return scan;
		}
		
		#endregion
		
		#region Teletransportation
		
		/// <summary>Indica todos os planetas que podem teletransportar recursos intrinsecos</summary>
		public Planet[] IntrinsicTeletransportationPlanets {
			get {
				ArrayList list = new ArrayList();
				foreach( Planet planet in ((Ruler)Owner).Planets ) {
					if( planet.Id != Id && planet.CanTeletransportIntrinsic ) {
						list.Add(planet);
					}
				}
				return (Planet[]) list.ToArray(typeof(Planet));
			}
		}
		
		/// <summary>Indica todos os planetas que podem teletransportar recursos intrinsecos</summary>
		public Planet[] FleetTeletransportationPlanets {
			get {
				ArrayList list = new ArrayList();
				foreach( Planet planet in ((Ruler)Owner).Planets ) {
					if( planet.Id != Id && planet.CanTeletransportFleets ) {
						list.Add(planet);
					}
				}
				return (Planet[]) list.ToArray(typeof(Planet));
			}
		}
		
		/// <summary>Indica se este planeta pode teletransportar recursos intrinsecos</summary>
		public bool CanTeletransportIntrinsic {
			get { return isResourceAvailable("Building", "Gate"); }
		}
		
		/// <summary>Indica se este planeta pode teletransportar armadas</summary>
		public bool CanTeletransportFleets {
			get { return isResourceAvailable("Building", "StarGate"); }
		}
		
		/// <summary>Indica se pode  possvel teletransportar determinados recursos entre este e outro planeta</summary>
		public Result CanMoveResource( Planet destiny, string category, string resource, int quantity )
		{
			if( category == null ) {
				if( Resource.IsRare(resource) ) {
					category = "Rare";
				} else {
					category = "Intrinsic";
				}
			}
			
			Result result = new Result();
			
			if( destiny.IsInBattle || this.IsInBattle ) {
				result.failed( new PlanetInBattle() );
				return result;
			}
			
			if( quantity <= 0 ) {
				result.failed( new InvalidQuantity() );
				return result;
			}
			
			if( !CanTeletransportIntrinsic || !destiny.CanTeletransportIntrinsic ) {
				result.failed( new ResourceNotAvailable("Building", "Gate") );
				return result;
			}
			
			if( resource == "labor" || resource == "marine" || resource == "spy" ) {
				int destinyHousing = destiny.Housing - destiny.Population;
				if( quantity >= destinyHousing ) {
					result.failed( new ResourceNotAvailable("Intrinsic", "housing") );
					return result;
				}
			}
			
			if( !Resource.IsTeletransportable(category, resource) ) {
				result.failed( new CannotTeletransport(resource) );
				return result;
			}
			
			int unitCost = Resource.TeletransportationCost( category, resource, quantity);
			if( unitCost > Energy ) {
				result.failed( new ResourceNotAvailable("Intrinsic", "energy") );
				return result;
			}
			
			if( quantity > getResourceCount(category, resource)) {
				result.failed( new ResourceNotAvailable("Intrinsic", resource) );
				return result;
			}
			
			if( !Universe.CheckRestrictions(this, result) ) {
				return result;
			}
			
			result.passed( new OperationSucceded() );
			
			return result;
		}
		
		/// <summary>Indica se pode  possvel teletransportar determinados recursos entre este e outro planeta</summary>
		public Result CanMoveIntrinsic( Planet destiny, string resource, int quantity )
		{
			return CanMoveResource(destiny, "Intrinsic", resource, quantity);
		}
		
		/// <summary>Teletransporta determinados recursos para outro planeta</summary>
		public void MoveResource( Planet destiny, string category, string resource, int quantity )
		{
			if( category == null ) {
				if( Resource.IsRare(resource) ) {
					category = "Rare";
				} else {
					category = "Intrinsic";
				}
			}
			
			take(category, resource, quantity);
			take("energy", Resource.TeletransportationCost(category, resource, quantity));
			destiny.addResource(category, resource, quantity);
		}
		
		/// <summary>Teletransporta determinados recursos para outro planeta</summary>
		public void MoveIntrinsic( Planet destiny, string resource, int quantity )
		{
			MoveResource(destiny, "Intrinsic", resource, quantity );
		}
		
		/// <summary>Indica se pode  possvel teletransportar determinados recursos entre este e outro planeta</summary>
		public Result CanMoveFleet( Planet destiny, string fleet )
		{
			Result result = new Result();

			if( !CanTeletransportFleets || !destiny.CanTeletransportFleets ) {
				result.failed( new ResourceNotAvailable("Building", "StarGate") );
				return result;
			}
			
			if( destiny.IsInBattle || this.IsInBattle ) {
				result.failed( new PlanetInBattle() );
				return result;
			}
			
			Fleet ships = getFleet(fleet);
			if( FleetMoveCost(ships) > Energy ) {
				result.failed( new ResourceNotAvailable("Intrinsic", "energy") );
				return result;
			}
			
			if( !Universe.CheckRestrictions(this, result) ) {
				return result;
			}
			
			result.passed( new OperationSucceded() );

			return result;
		}
		
		/// <summary>Teletransporta determinados recursos para outro planeta</summary>
		public void MoveFleet( Planet destiny, string fleet )
		{
			Fleet toMove = getFleet(fleet);
			
			take("energy", FleetMoveCost(toMove));
			removeFleet( toMove );
			destiny.addFleet( toMove );
		}
		
		/// <summary>Indica o preco de teletransporte de uma fleet</summary>
		public int FleetMoveCost( Fleet fleet )
		{
			int fleetCost = 0;
			
			if( !fleet.HasShips ) {
				return 0;
			}
			
			IDictionaryEnumerator it = fleet.Ships.GetEnumerator();
			while( it.MoveNext() ) {
				Resource resource = Universe.getFactory("planet", "Unit", it.Key.ToString()).create();
				int quantity = (int) it.Value;
				fleetCost += Resource.FleetTeletransportationCost(resource, quantity);
			}
			
			return fleetCost;
		}

		public void AddToDefense(Fleet f) {
			Fleet defense = getDefenseFleet();
			
			IDictionaryEnumerator iter = f.Ships.GetEnumerator();
			while( iter.MoveNext() ) {
				if( defense.swapShips( f, iter.Key.ToString(), (int)iter.Value) ) {
					iter = f.Ships.GetEnumerator();
				}else {
					return;
				}
			}
		}

		#endregion
	};
}
