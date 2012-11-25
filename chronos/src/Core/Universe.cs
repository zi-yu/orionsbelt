using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Threading;
using Chronos.Actions;
using Chronos.Alliances;
using Chronos.Battle;
using Chronos.Exceptions;
using Chronos.Info;
using Chronos.Info.Results;
using Chronos.Interfaces;
using Chronos.Messaging;
using Chronos.Persistence;
using Chronos.Resources;
using Chronos.Utils;
using Chronos.Tournaments;

namespace Chronos.Core {
	
	/// <summary>
	/// classe que representa o universo
	/// </summary>
	[Serializable]
	public class Universe : ITask, IDeserializationCallback {
		
		#region private fields

		//contem todos os planetas
		private static Hashtable _factories;

		//contem todos as alianas
		private AllianceCollection _alliances;

		//contem todos os rulers
		private Hashtable _rulers = null;

		//contem todos os planetas
		private Hashtable _planets = null;

		//contem todas as fleets em movimento
		private Hashtable _fleets = null;
		
		//indica as keywords dos recursos
		[NonSerialized]
		private static Hashtable keywords = null;
		
		// contentores com os objectos que ganharam prémios
		private Hashtable prizes = null;

		//prxima coordenada onde vai ser inserido o planeta
		private Coordinate _nextCoordinate = null;

		//unica instncia de Universe
		private static Universe universe = new Universe();
		
		//controla a sincronização dentro de turnos
		[NonSerialized]
		private ReaderWriterLock universeAvailable = new ReaderWriterLock();
		
		// chama turn() periodicamente
		[NonSerialized]
		private Timer timer;
		
		// tempo que demora um turno
		private long turnTime;
		
		// contagem de turnos
		private int turnCount;

		[NonSerialized]
		private IPersistence persistence = null;
		
		[NonSerialized]
		private Ruler[] rank;

		[NonSerialized]
		private Hashtable battleHandlers;

		public delegate void BattleHandler( int battleId, Ruler player1, IBattle ibattle1, Ruler player2, IBattle ibattle2, string endType );
		public delegate void RankingBattleHandler( Ruler one, Ruler two, BattleResult result );
		
		public static event RankingBattleHandler RankingBattleEnded;
		
		#endregion

		#region properties


		/// <summary>
		/// todas as factories que existem
		/// </summary>
		public static Hashtable factories {
			get{ return _factories; }
		}

		/// <summary>
		/// todas as alinaas que existem
		/// </summary>
		public Hashtable alliances {
			get{ return _alliances; }
		}

		/// <summary>
		/// todos os rulers que existem
		/// </summary>
		public Hashtable rulers {
			get{ return _rulers; }
		}

		/// <summary>
		/// todos os planetas que existem
		/// </summary>
		public Hashtable planets {
			get{ return _planets; }
		}

		/// <summary>
		/// numero de rulers do universo
		/// </summary>
		public int rulerCount {
			get{ return _rulers.Count; }
		}

		/// <summary>
		/// numero de alianas
		/// </summary>
		public int allianceCount {
			get{ return _alliances.Count; }
		}

		/// <summary>
		/// numero de planetas Habitados
		/// </summary>
		public int planetCount {
			get{
				int count = 0;
				/*foreach( Ruler ruler in _rulers.Values ) {
					count += ruler.Planets.Length;
				}*/
				foreach( Planet p in _planets.Values ) {
					if( p.Owner != null )
					count += 1;
				}
				return count;
			}
		}


		/// <summary>
		/// tipo de persistncia
		/// </summary>
		public IPersistence Persistence {
			get{ return persistence; }
			set{ persistence = value; }
		}
		
		/// <summary>Indica se o Universo está disponível para operações, ou está a decorrer um turno</summary>
		public bool UniverseAvailable {
			get {
				return !universeAvailable.IsWriterLockHeld;
			}
		}

		/// <summary>Obtm o objecto de sincronizao</summary>
		public ReaderWriterLock SyncRoot {
			get { return universeAvailable; }
		}
		
		/// <summary>Indica o tempo que demora um turno</summary>
		public long TurnTime {
			get { return turnTime; }
			set { turnTime = value; }
		}
		
		/// <summary>Indica a quantidade de turnos passados</summary>
		public int TurnCount {
			get { return turnCount; }
			set { turnCount = value; }
		}

		#endregion
		
		#region Events
		
		[NonSerialized]
		private static TurnEvents turnEvents = new TurnEvents();

		public static TurnEvents Events {
			get{
				if( turnEvents == null )
					turnEvents = new TurnEvents();
				return turnEvents;
			}
		}

		/// <summary>Argumentos para os eventos de erro</summary>
		public class TurnErrorEventArgs : EventArgs {

			protected Exception e;
			
			public TurnErrorEventArgs( Exception e ) {
				this.e = e;
			}

			public Exception Error {
				get{ return e; }
			}
		};
		
		/// <summary>Argumentos para os eventos de erro</summary>
		public class TraceEventArgs : EventArgs {
		
			private string message;
			private int turn;
			
			public TraceEventArgs( string message, int turn ) {
				this.message = message;
				this.turn = turn;
			}

			public string Message {
				get{ return message; }
			}
			
			public int Turn {
				get { return turn; }
			}
			
			public override string ToString(){
				return message;
			}
		};
		
		/// <summary>Eventos do Universo</summary>
		public class TurnEvents {

			/// <summary>Invoca um evento</summary>
			private void notify( EventHandler ev, EventArgs args ) {
				if ( ev != null ) {
					ev(this, args);
				}
			}
			
			public event EventHandler TurnStart;
			public event EventHandler TurnEnd;
			public event EventHandler TurnError;
			public event EventHandler UniverseTrace;

			public void startTurn() {
				notify(TurnStart,EventArgs.Empty);
			}

			public void endTurn() {
				notify(TurnEnd,EventArgs.Empty);
			}

			public void turnError( Exception e ) {
				notify(TurnError, new TurnErrorEventArgs( e ) );
			}
			
			public void trace( string message, params object[] args ) {
				if( UniverseTrace == null ) {
					return;
				}
				string m = string.Format(message, args);
				TraceEventArgs eventArgs = new TraceEventArgs(m, Universe.instance.TurnCount);
				UniverseTrace(this, eventArgs);
			}
		}
		
		#endregion
		
		#region Persistence Methods
		
		private static PersistenceParameters param = new PersistenceParameters();
		
		/// <summary>Obtem os parametros de persistencia</summary>
		public static PersistenceParameters Parameters {
			get { return param; }
			set { param = value; }
		}
		
		/// <summary>Guarda o Universo</summary>
		public static void persist( Universe universe ) {
			
			if( null == universe.persistence )
				universe.persistence = UniverseSerializer.Instance;
			
			Universe.Events.trace("[START] Persisting Universe to {0}", universe.persistence.GetType().Name );
			universe.persistence.save( universe, Parameters );
			Universe.Events.trace("[END] Persisting Universe to {0}", universe.persistence.GetType().Name );
		}
		
		/// <summary>Carrega todo Universo</summary>
		public static Universe load()
		{
			IPersistence persistence = UniverseSerializer.Instance;
			return persistence.load(Parameters);
		}
		
		private PersistenceServices persistenceServices = null;
		
		public PersistenceServices PersistenceServices {
			get {
				if( persistenceServices == null ) {
					persistenceServices = new PersistenceServices();
				}
				return persistenceServices;
			}
		}
		
		public Tournament GetTournament( string type )
		{
			return (Tournament) this.PersistenceServices.GetState(type);
		}
		
		public void RegisterTournamentBattle( string type, BattleInfo info )
		{
			Tournament tour = GetTournament(type);
			if( tour != null ) {
				tour.CurrentPhase.BattleEnded(info);
				if( RankingBattleEnded != null ) {
					Ruler one = getRuler( info.RBI1.OwnerId );
					Ruler two = getRuler( info.RBI2.OwnerId );
					RankingBattleEnded(one, two, info.Result(one, two));
				}
			}
		}
		
		public static int GetTournamentPoints( RulerBattleInfo info )
		{
			int points = 0;
			IDictionaryEnumerator it = info.ResourceDestroyed.GetEnumerator();
			while( it.MoveNext() ) {
				Resource resource = (Resource) it.Key;
				int quant = (int) it.Value;
				points += resource.TournamentValue * quant;
			}
			return points;
		}
		
		#endregion
		
		#region Timer Methods
		
		/// <summary>Começa a execução dos turnos</summary>
		public void start()
		{
			TimerCallback callback = new TimerCallback(callTurn);
			if( timer == null ) {
				timer = new Timer(callback, this, 0, TurnTime);
			} else {
				timer.Change(0, TurnTime);
			}
		}
		
		/// <summary>Invoca o turn()</summary>
		public void callTurn( object state )
		{
			turn();
		}
		
		#endregion
		
		#region public methods
		
		/// <summary>Retorna true se for possível adicionar um novo ruler</summary>
		public bool canAddRuler() {
			return CurrentCoordinate.HasMoreCoordinates;
		}

		/// <summary>
		/// mtodo para passar um turno
		/// </summary>
		public void turn() {
			if ( _alliances == null )
				throw new NullReferenceException("alliances not set to an instance of an object @ Universe:turn");
			if ( _fleets == null )
				throw new NullReferenceException("fleet not set to an instance of an object @ Universe:turn");
			try{
			
				++TurnCount;
				
				Events.startTurn();
				Events.trace("[START] Turn Started");

				acquireWriterLock();
			
				WaitHandle[] handles = new WaitHandle[allianceCount];
				int i = 0;
				
				Events.trace("Universe elements: {0} Alliances; {1} Rulers; {2} Planets", alliances.Count, rulers.Count, planets.Count);

				IDictionaryEnumerator enumerator = _alliances.GetEnumerator();
				while( enumerator.MoveNext() ){
					WaitCallback callBack = new WaitCallback(updateAliance);
					handles[i] = callBack.BeginInvoke(enumerator.Value, null, null).AsyncWaitHandle;
					
					++i;
				}

				moveFleets();

				WaitHandle.WaitAll(handles);
				sortRulers();
			
				persist( this );
				
				Events.trace("[END] Turn Ended");
				
			}catch( Exception e ) {
				Log.log("Turn error: " +  e );
				Events.trace("[EXCEPTION] " + e);
				Events.turnError( e );
			}finally{
				Log.log("Writer Lock released");
				universeAvailable.ReleaseWriterLock();
				Events.endTurn();
			}
		}

		/// <summary>Adquire acesso de escrita para o turno</summary>
		private void acquireWriterLock()
		{
			try {
				universeAvailable.AcquireWriterLock(Timeout.Infinite);
				Log.log("Writer Lock");
			} catch( ApplicationException ) {
				Log.log("###### ERROR #### AcquireWriterLock timed out!");
				acquireWriterLock();
			}
		}

		#endregion

		#region Conquer Stuff

		private void PlanetConquered(string name, Planet planet, Fleet fleet, Ruler ruler) {
			planet.Name = name;
			planet.StartImmunity();
			fleet.removeShip("ColonyShip", 1);
			checkRulerConquerPoints(ruler);
			CheckFleetsInConqueredPlanet(ruler, planet);
		}

		/// <summary>
		/// muda o dono de um planeta
		/// </summary>
		/// <param name="planet">planeta</param>
		/// <param name="newOwner">novo dono do planeta</param>
		/// <remarks>O planeta n  removido pq  removido no final da batalha</remarks>
		public bool changePlanetOwner( Planet planet, Ruler newOwner ) {
			try {
				if( newOwner.CanAddPlanet ) {
					Ruler r = (Ruler)planet.Owner;
					if( r != null ) {
						r.removePlanet(planet);
					}
					newOwner.addPlanet( planet );
					planet.OldOwner = null;
					planet.WonABattle = null;
					return true;
				} /*else {//devolver ao respectivo Owner
					Ruler ruler = planet.OldOwner;
					if( null != ruler ) {
						ruler.addPlanet( planet );
						planet.OldOwner = null;
						planet.WonABattle = null;
					}
				}*/
			}catch( Exception e ){
				throw e;
			}
			return false;
		}

		private void CheckFleetsInConqueredPlanet(Ruler ruler, Planet planet) {
			ArrayList fleets = ruler.getCoordinateFleets(planet.Coordinate);
			foreach( Fleet f in fleets ) {
				ruler.removeUniverseFleet( f.Id );
				planet.addFleet( f );
				planet.AddToDefense( f );
			}
		}

		/// <summary>
		/// Conquista um planeta inhabitado
		/// </summary>
		public void conquerPlanet( Coordinate coordinate, string name, Fleet fleet ) {
			if( _planets.ContainsKey(coordinate) ) {

				Ruler ruler = fleet.Owner as Ruler;

				if( null == ruler ) {
					throw new RuntimeException("Dono da Fleet não é um Ruler @ Universe.conquerPlanet!");
				}

				Planet planet = (Planet)_planets[coordinate];
				
				if( planet.InitMade ) {
					if( changePlanetOwner( planet,ruler) ) {
						PlanetConquered(name, planet, fleet, ruler);
					}
				} else {
					if( planet.Owner != null ) {
						throw new RuntimeException("O planeta devia estar desabitado mas está habitado! Owner!=null @ Universe.conquerPlanet ");
					}
					planet.init( ruler, name, _factories );
					PlanetConquered(name, planet, fleet, ruler);
				}

			}else{
				throw new RuntimeException("Tentativa de conquistar um planeta que ainda não está criado! @ Universe.conquerPlanet");
			}
		}

		#endregion

		#region Ruler Stuff
		
		/// <summary>Faz o sort de todos os rulers</summary>
		private void sortRulers()
		{
			Events.trace("[START] Sorting {0} Rulers", rulers.Count);
			rank = new Ruler[rulers.Count];
			int i = -1;
			foreach( Ruler ruler in rulers.Values ) {
				rank[++i] = ruler;
			}
			Array.Sort(rank);
			for( i = 0; i < rank.Length; ++i ) {
				Ruler ruler = rank[i];
				ruler.Rank = i + 1;
			}
			Events.trace("[END] Sorting {0} Rulers", rulers.Count);
		}
		
		/// <summary>
		/// adiciona o Ruler e o seu respectivo planeta ao universo e h
		/// aliana por defeito (no-alliance)
		/// </summary>
		/// <param name="planetName">nome do planeta</param>
		public int addRulerToUniverse( string name, string planetName )
		{
			Ruler ruler = new Ruler(factories, name);
			addRulerToUniverse(ruler,planetName);
			return ruler.Id;
		}
		
		/// <summary>
		/// adiciona o Ruler e o seu respectivo planeta ao universo e h
		/// aliana por defeito (no-alliance)
		/// </summary>
		/// <param name="planetName">nome do planeta</param>
		public Ruler CreateRuler( string name, string planetName )
		{
			int id = addRulerToUniverse(name, planetName);
			return getRuler(id);
		}

		/// <summary>
		/// adiciona o Ruler e o seu respectivo planeta ao universo e h
		/// aliana por defeito (no-alliance)
		/// </summary>
		/// <param name="ruler">objecto que representa o ruler a adicionar</param>
		/// <param name="planetName">nome do planeta</param>
		public void addRulerToUniverse( Ruler ruler, string planetName )
		{
			if( ruler == null ) {
				throw new NullReferenceException("Cannot add ruler to universe because his instance is set to null!");
			}
			
			if( planetName == null || planetName == string.Empty ) {
				throw new NullReferenceException("PlanetName is invalid");
			}

			//System.Console.WriteLine("Coordinate to add:" + _nextCoordinate.galaxy+"."+ _nextCoordinate.sector+"."+_nextCoordinate.planet);
			
			try {
				_rulers.Add( ruler.Id, ruler );

				addPlanet( planetName, ruler );

				addRulerToAlliance("no-alliance",ruler,AllianceMember.Role.Private);
				
			} catch(ArgumentException e) {
				string msg = "[Coordinate Galaxy:" + CurrentCoordinate.Galaxy + " Sector:" + CurrentCoordinate.Sector + " Planet:" + CurrentCoordinate.Planet + "]";
				msg += "[RulerId:" + ruler.Id + "] ";
				throw new RuntimeException(msg + e.Message);
			}
		}

		/// <summary>Retorna um ruler dado o seu ID</summary>
		public Ruler getRuler( int id ) {
			return (Ruler) rulers[id];
		}

		#endregion

		#region Alliance Stuff
		
		/// <summary>
		/// adiciona um Ruler  alianca.
		/// </summary>
		/// <param name="allianceName">nome da aliana</param>
		/// <param name="ruler">objecto que representa o ruler</param>
		public void createAlliance( string allianceName, Ruler ruler ) {
			removeRulerFromAlliance( ruler );
			Alliance alliance = new Alliance( this, allianceName );
			_alliances[allianceName] = alliance;
			addRulerToAlliance( allianceName, ruler, AllianceMember.Role.Admiral );
		}
		
		/// <summary>
		/// Cria uma aliança
		/// </summary>
		/// <param name="allianceName">nome da aliana</param>
		public void createAlliance( string allianceName, bool sharing, int id ) {
			Alliance alliance = new Alliance( this, allianceName, sharing, id );
			_alliances[allianceName] = alliance;
		}
		
		/// <summary>Indica se uma aliança é a aliança por defeito</summary>
		public bool isDefaultAlliance( Alliance alliance )
		{
			Alliance defaultAlliance = (Alliance) alliances[Alliance.defaultAllianceName];
			return object.ReferenceEquals(defaultAlliance, alliance );
		}
		
		/// <summary>Indica se uma aliança é a aliança por defeito</summary>
		public bool isDefaultAlliance( Ruler ruler )
		{
			if ( null == ruler.Owner ) {
				return false;
			}

			return ruler.AllianceId == 0;
		}

		/// <summary>
		/// adiciona um ruler a uma alianca
		/// </summary>
		/// <param name="allianceName">nome da aliana</param>
		/// <param name="ruler">objecto que representa o ruler</param>
		public void addRulerToAlliance( string allianceName, Ruler ruler ) {
			addRulerToAlliance( allianceName, ruler, AllianceMember.Role.Private );
		}
		/// <summary>
		/// adiciona um Ruler  aliana.
		/// </summary>
		/// <param name="allianceName">nome da aliana</param>
		/// <param name="ruler">objecto que representa o ruler</param>
		/// <param name="role">role do ruler</param>
		public void addRulerToAlliance( string allianceName, Ruler ruler, AllianceMember.Role role ) {
			if( allianceName.CompareTo( string.Empty ) == 0 )
				throw new Exception("Cannot add ruler to alliance because there is no name to the alliance");

			if( ruler == null )
				throw new NullReferenceException("Cannot add ruler to universe because his instance is set to null!");
			
			//adicionar o ruler  nova aliana
			Alliance alliance = (Alliance)_alliances[allianceName];
			alliance.addRuler( ruler, role );
		}
		
		/// <summary>
		/// muda a alianca do utilizador
		/// </summary>
		/// <param name="allianceName">nome da nova alianca</param>
		/// <param name="ruler">ruler que vai mudar de aliana</param>
		/// <param name="role">role dele na aliana</param>
		public void changeRulerAlliance( string allianceName, Ruler ruler, AllianceMember.Role role ) {
			removeRulerFromAlliance( ruler );
			addRulerToAlliance(allianceName, ruler, role );
		}

		/// <summary>
		/// muda a alianca do utilizador
		/// </summary>
		/// <param name="allianceName">nome da nova aliana</param>
		/// <param name="ruler">ruler que vai mudar de aliana</param>
		/// <remarks>a role da aliana por defeito ser member</remarks>
		public void changeRulerAlliance( string allianceName, Ruler ruler  ) {
			changeRulerAlliance( allianceName, ruler, AllianceMember.Role.Private );
		}

		/// <summary>
		/// verifica a existncia de uma aliana
		/// </summary>
		/// <param name="name">name</param>
		/// <returns>retorna <code>true</code> se a aliana existir,
		/// <code>false</code> caso contrrio</returns>
		public bool allianceExists( string name ) {
			return _alliances.ContainsKey( name );
		}

		/// <summary>
		/// remove um ruler da aliança
		/// </summary>
		/// <param name="ruler">objecto que representa o ruler a remover</param>
		public void removeRulerFromAlliance( Ruler ruler ) {
			if( ruler.Owner == null ) {
				return;
			}
			//remover o ruler da aliana onde estava
			Alliance currentRulerAlliance = (Alliance)ruler.Owner;
			int idx = currentRulerAlliance.getIndex(ruler);
			currentRulerAlliance.removeRuler(idx);
		}


		/// <summary>
		/// determina se os dois rulers estão na mesma aliança
		/// </summary>
		public bool sameAlliance( object ruler1, object ruler2 ) {
			return Object.ReferenceEquals( ruler1, ruler2 );
		}

		#endregion
		
		#region ID handling
		
		private int allianceId = 0;
		private int planetId = 0;
		private int rulerId = 0;
		private int fleetId = 0;
		private int battleId = 0;
		private int taskId = 0;
		private int rulerBattleInfoId = 0;

		/// <summary>Indica o ID currente para uma aliança</summary>
		public int AllianceId {
			get { return allianceId; }
			set { allianceId = value; }
		}
		
		/// <summary>Indica o ID currente para um planeta</summary>
		public int PlanetId {
			get { return planetId; }
			set { planetId = value; }
		}
		
		/// <summary>Indica o ID currente para um planeta</summary>
		public int RulerId {
			get { return rulerId; }
			set { rulerId = value; }
		}
		
		/// <summary>Indica o ID currente para uma Fleet</summary>
		public int FleetId {
			get { return fleetId; }
			set { fleetId = value; }
		}

		/// <summary>
		/// Indica o ID único para a batalha
		/// </summary>
		public int BattleId {
			get { return battleId; }
			set { battleId = value; }
		}

		/// <summary>
		/// Indica o ID único para a batalha
		/// </summary>
		public int RulerBattleInfoId {
			get { return rulerBattleInfoId; }
			set { rulerBattleInfoId = value; }
		}

		/// <summary>Indica os rulers pelos seu rank</summary>
		public Ruler[] Rank {
			get { return rank; }
		}

		/// <summary>Retorna um ID para uma aliança</summary>
		public int generateAllianceId()
		{
			return ++allianceId;
		}
		
		/// <summary>Retorna um ID para um planeta</summary>
		public int generatePlanetId()
		{
			return ++planetId;
		}
		
		/// <summary>Retorna um ID para um ruler</summary>
		public int generateRulerId()
		{
			return ++rulerId;
		}
		
		/// <summary>Retorna um ID para uma fleet</summary>
		public int generateFleetId()
		{
			return ++fleetId;
		}

		/// <summary>Retorna um ID para uma batalha</summary>
		public int generateBattleId() {
			return ++battleId;
		}
		
		/// <summary>Retorna um ID para uma tarefa</summary>
		public int generateTaskId() {
			return ++taskId;
		}


		/// <summary>Retorna um ID para uma tarefa</summary>
		public int generateRulerBattleInfoId() {
			return ++RulerBattleInfoId;
		}

		#endregion

		#region Planet Stuff

		/// <summary>
		/// adiciona um planet inabitado
		/// </summary>
		public void addPlanet( Coordinate coordinate ) {
			_planets.Add( coordinate, new Planet( coordinate ) );
		}

		/// <summary>
		/// adiciona um planet ao universo e ao ruler ou se ele j existir
		/// muda-lhe o nome e o Ruler
		/// </summary>
		/// <param name="name">nome do planet</param>
		/// <param name="ruler">dono do planeta</param>
		public void addPlanet( string name, Ruler ruler ) {
			Coordinate coordinate = NextCoordinate;
			while( _planets.ContainsKey(coordinate) )
				coordinate = NextCoordinate;

			if( !_planets.ContainsKey(coordinate) ) {
				Planet planet = new Planet( ruler, _factories, name, coordinate );
				_planets.Add( coordinate, planet );
			}
		}

		/// <summary>
		/// obtem o um determinado planeta dependendo da coordenada
		/// </summary>
		/// <param name="coordinate">coordenada do planeta</param>
		/// <returns>o objecto que representa o planeta nessa coordenada, null caso o planeta n exista</returns>
		public Planet getPlanet( Coordinate coordinate ) {
			if( _planets.ContainsKey(coordinate) )
				return(Planet)_planets[coordinate];
			return null;
		}

		#endregion

		#region Fleet Stuff

		private bool checkExplorationResearch( Ruler ruler, string type, int current, int destiny ) {
			if( current != destiny )
				return ruler.isResourceAvailable( "Research", type );
			return true;
		}

		/// <summary>
		/// verifica de o Ruler possui research suficiente para mover a nave para a coordenada
		/// </summary>
		private bool canMove( Fleet fleet, Coordinate coord ){
			Ruler ruler = fleet.Owner as Ruler;
			if( ruler == null ) {
				ruler = (Ruler)fleet.Owner.Owner;
				if( ruler == null )
					throw new RuntimeException("Onde est o ruler @ Universe::IsDestinationReachable");
			}
			
			return Coordinate.IsAccessible(ruler, fleet.Coordinate, coord);
		}


		public bool IsDestinationReachable( Fleet f, Coordinate coord ) {
			if( !Coordinate.isValid(coord) )
				return false;
			
			if( canMove( f, coord ) ) {
				if( coord.Planet == 1 ) {
					Planet p = (Planet)_planets[coord];
					if( null == p || null == p.Owner  )
						return false;
					if( f.Owner is Planet ) {
						return p.Owner == f.Owner.Owner;
					}else {
						return p.Owner == f.Owner;
					}
				}
				return true;
			}

			return false;
		}

		/// <summary>
		/// regista no universo a fleet que vai iniciar o movimento
		/// </summary>
		/// <param name="fleet">objecto que representa a fleet a mover</param>
		public void registerMovingFleet( Fleet fleet ) {
			if( null == fleet )
				throw new RuntimeException("Fleet  null @ Universe::registerMovingFleet");

			if( _fleets.Contains(fleet.Id) )
				return;

			//se o owner um planeta quer dizer que a fleet está associada
			//a um planeta e não pode
			if( fleet.Owner is Planet ) {
				((Planet)fleet.Owner).removeFleet( fleet );
				Ruler ruler = fleet.Owner.Owner as Ruler;
				if( ruler == null ) {
					throw new RuntimeException("_owner do planeta devia ser um Ruler @ Universe::registerMovingFleet");
				}

				fleet.Owner = ruler;
				//adiciona a fleet ao ruler de modo a ele saber quais as fleets não estão num planeta
				ruler.addUniverseFleet( fleet );
			}

			_fleets.Add( fleet.Id, fleet );
		}

		/// <summary>
		/// move as fleets que estão em movimento
		/// </summary>
		private void moveFleets() {
		
			Events.trace("[START] Move Fleets - Fleet count: {0}", _fleets.Count);
			
			if( 0 != _fleets.Count ) {
				ArrayList toRemove = null;
				IDictionaryEnumerator iter =  _fleets.GetEnumerator();
				
				while( iter.MoveNext() ) {
					Fleet fleet = (Fleet)iter.Value;
					fleet.moveFleet();
					if( !fleet.IsMoving ) {
						fleetArrived( fleet );
						if( null == toRemove ) {
							toRemove = new ArrayList();
						}
						toRemove.Add(iter.Key);
					}
				}

				if( toRemove != null && 0 != toRemove.Count ) {
					foreach( object key in toRemove ) {
						_fleets.Remove( key );
					}
				}
			}
			
			Events.trace("[END] Move Fleets");
		}

		/// <summary>
		/// trata da informação quando uma fleet chega ao destino
		/// </summary>
		/// <param name="fleet"></param>
		private void fleetArrived( Fleet fleet ) {
			try {
				//obter o owner da fleet
				if( !(fleet.Owner is Ruler) ) {
					throw new RuntimeException("fleetOwner não é um Ruler @ Universe::fleetArrived");
				}

				Ruler fleetOwner = fleet.Owner as Ruler;
				if( null == fleetOwner ) {
					throw new RuntimeException("fleetOwner  null @ Universe::fleetArrived");
				}
				
				Messenger.Send( fleetOwner,"FleetArrived", fleet.Name, fleetOwner.Name, fleet.Coordinate.ToString() );

				Planet planet = null;
				if( _planets.ContainsKey(fleet.Coordinate) ) {
					planet = (Planet)_planets[fleet.Coordinate];
					if( null == planet.Owner ) {
						return;
					}
				}
				
				//o dono do planeta e do dono da fleet
				if( planet.Owner.Id == fleetOwner.Id ){
					//remover a fleet do universo e do ruler
					fleetOwner.removeUniverseFleet( fleet.Id );
					planet.addFleet( fleet );
				}
				
			} catch( Exception ex ) {
				Events.turnError(ex);
			}
		}

		#endregion

		#region Battle Stuff

		#region Battle Private
		
		private void AddTournament( int battleId, Ruler player1, IBattle iBattle1, Ruler player2, IBattle iBattle2, string endType ) {
			player1.AddBattle( new SimpleBattleInfo( battleId, player1, player2, iBattle1, BattleType.TOURNAMENT, endType, true ),iBattle1, BattleType.TOURNAMENT );
			player2.AddBattle( new SimpleBattleInfo( battleId, player2, player1, iBattle2, BattleType.TOURNAMENT, endType, false ),iBattle2, BattleType.TOURNAMENT );

			Messenger.Send( player1, "TournamentStarted", player2.Name );
			Messenger.Send( player2, "TournamentStarted", player1.Name );
		}

		private void AddBattle( int battleId, Ruler player1, IBattle iBattle1, Ruler player2, IBattle iBattle2, string endType ) {
			player1.AddBattle( new SimpleBattleInfo( battleId, player1, player2, iBattle1, BattleType.BATTLE, endType, true ),iBattle1, BattleType.BATTLE );
			player2.AddBattle( new SimpleBattleInfo( battleId, player2, player1, iBattle2, BattleType.BATTLE, endType, false ),iBattle2, BattleType.BATTLE );

			Messenger.Send( player1, "BattleStarted", player2.Name, iBattle1.Coordinate.ToString() );
			Messenger.Send( player2, "BattleStarted", player1.Name, iBattle2.Coordinate.ToString() );
		}

		private void AddFriendly( int battleId, Ruler player1, IBattle iBattle1, Ruler player2, IBattle iBattle2, string endType ) {
			SimpleBattleInfo s1 = new SimpleBattleInfo( battleId, player1, player2, iBattle1, BattleType.FRIENDLY, endType, true );
			s1.Accepted = true;
			player1.AddBattle( s1,iBattle1, BattleType.FRIENDLY );
			
			SimpleBattleInfo s2 = new SimpleBattleInfo( battleId, player2, player1, iBattle2, BattleType.FRIENDLY, endType, false );
			s2.Accepted = false;
			player2.AddBattle( s2,iBattle2, BattleType.FRIENDLY );

			Messenger.Send( player1, "FriendlyStarted", player2.Name );
			Messenger.Send( player2, "FriendlyStarted", player1.Name );
		}

		#endregion 

		/// <summary>
		/// Cria uma batalha
		/// </summary>
		/// <param name="player1">Jogador que começa a batalha</param>
		/// <param name="iBattle1">Fleet ou planeta do jogador</param>
		/// <param name="player2">Jogador que é desafiado para a batalha</param>
		/// <param name="iBattle2">Fleet ou planeta do jogador</param>
		/// <returns>A battle Info responsável por esta batalha</returns>
		public int CreateBattle( Ruler player1, IBattle iBattle1, Ruler player2, IBattle iBattle2, BattleType type, string endType ) {
			int battleId = Universe.instance.generateBattleId( );

			BattleHandler handler = (BattleHandler)battleHandlers[ type ];
			handler( battleId, player1, iBattle1, player2, iBattle2, endType );

            BattleInfo battleInfo = new BattleInfo(player1,player2,battleId, type, endType );
			BattlePersistence.Instance.SaveBattle( battleInfo, player1.Id );

			iBattle1.IsInBattle = iBattle2.IsInBattle = true;
			
			return battleInfo.BattleId;
		}

		public bool RemoveBattleByAdmin( int battleId ) {
			BattleInfo battleInfo = Universe.instance.GetBattle(battleId);

			bool b = RemoveBattle( battleId );

			if( b ) {
				Ruler r1 = Universe.instance.getRuler(battleInfo.RBI1.OwnerId) ;
				Ruler r2 = Universe.instance.getRuler(battleInfo.RBI2.OwnerId) ;
				
				Messenger.Send( r1, "BattleDeleted", r2.Name );
				Messenger.Send( r2, "BattleDeleted", r1.Name );
			}
			
			return b;
		}

		public void AcceptBattle( int battleId, Ruler ruler ) {
			SimpleBattleInfo battleInfo = ruler.GetBattle(battleId,BattleType.FRIENDLY);
			Messenger.Send(battleInfo.Enemy, "BattleAccepted",ruler.Name);
		}

		public bool RejectBattle( int battleId, Ruler ruler ) {
			SimpleBattleInfo battleInfo = ruler.GetBattle(battleId,BattleType.FRIENDLY);

			bool b = RemoveBattle( battleId );

			if( b ) {
				Messenger.Send(battleInfo.Enemy, "BattleRejected",ruler.Name);
			}
			return b;
		}

		public bool RemoveBattle( int battleId ) {
			try {
				BattleInfo battleInfo = BattlePersistence.Instance.LoadBattle( battleId );
				if( null != battleInfo) {
					battleInfo.DeleteRulersBattles( battleId );
				}
				BattlePersistence.Instance.RemoveBattle( battleId );
				return true;
			}catch( SerializationException ) {
				return false;
			}
		}

		public BattleInfo GetBattle( int battleId ) {
			return BattlePersistence.Instance.LoadBattle( battleId );
		}

		public int GetBattleTurn( int battleId ) {
			return BattlePersistence.Instance.LoadBattleTurn( battleId );
		}

		public void SaveBattle( BattleInfo info, int rulerIdToPlay ) {
			BattlePersistence.Instance.SaveBattle( info,rulerIdToPlay );
		}

		public void SaveBattleTurn( int battleId, int rulerIdToPlay ) {
			BattlePersistence.Instance.SaveBattleTurn( battleId, rulerIdToPlay );
		}

		public IBattle GetIBattle( int ownerId, int battleId, BattleType type ) {
			Ruler owner = Universe.instance.getRuler( ownerId );
			return owner.GetIBattle( BattleId, type );
		}

		
		#endregion

		#region static methods
		
		/// <summary>Construtor estático</summary>
		static Universe()
		{
			loadFactories();
			instance.init();
		}
		
		/// <summary>
		/// obtm a nica instncia de universo
		/// </summary>
		public static Universe instance
		{
			get{
				return universe;
			}
			set {
				universe = value;
			}
		}
		
		/// <summary>Retorna todos os Terrain</summary>
		public static Terrain[] AllTerrains {
			get { return Terrain.All; }
		}
		
		/// <summary>Retorna todos os PlanetInfos</summary>
		public static PlanetInfo[] AllPlanetInfos {
			get { return PlanetInfo.All; }
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="state"></param>
		private void updateAliance( object state ) {
			Alliance alliance = (Alliance)state;
			alliance.turn();
		}

		#endregion
        
		#region private methods and properties

		/// <summary>
		/// retorna a prxima coordenada possivel
		/// </summary>
		private Coordinate NextCoordinate {
			get{
				Coordinate toReturn = _nextCoordinate;
				_nextCoordinate = new Coordinate( _nextCoordinate );
				_nextCoordinate.incrementCoordinate();
				return toReturn;
			}
			set{ _nextCoordinate = value; }
		}
		
		// tem de ser pblica por causa de persistncia
		public Coordinate CurrentCoordinate {
			get {
				return _nextCoordinate;
			}
			set{ _nextCoordinate = value; }
		}

		#endregion

		#region initStuff

		private static void loadFactories() {
			Loader loader = new Loader();
			_factories = loader.load(Platform.ResourceConfigDir);
			keywords = loader.Keywords;
		}

		private void loadAlliances() {
			_alliances = new AllianceCollection();
		}

		/// <summary>
		/// cria todos os planetas
		/// </summary>
		private void createPlanets() {
			Coordinate c = Coordinate.First;
			while( c.HasMoreCoordinates ) {
				for( int i = 2; i < Coordinate.MaximumPlanets+1; ++i ) {
					Coordinate tmp = new Coordinate( c );
					tmp.Planet = i;
					addPlanet( tmp );
				}
				c.incrementCoordinate();
			}
		}

		/// <summary>
		/// construtor
		/// </summary>
		public void init() {
		
			turnCount = 0;
			turnTime = 60000;

			loadAlliances();
			
			//iniciar o contentor de Rulers
			_rulers = Hashtable.Synchronized( new Hashtable() );

			//iniciar o contentor de planetas
			_planets = new Hashtable();

			//criar todos os planetas excepto os homeplanets
			createPlanets();

			//iniciar as coordenadas
			NextCoordinate = Coordinate.First;

			//iniciar o contentor de fleets
			_fleets = new Hashtable();
			
			// prémios
			prizes = new Hashtable();
			
			//adicionar a aliana por defeito
			_alliances.Add(Alliance.defaultAllianceName, new Alliance( this, Alliance.defaultAllianceName ) );

			persistence = UniverseSerializer.Instance;
		}

		/// <summary>Repe objectos no serializveis</summary>
		void IDeserializationCallback.OnDeserialization( object sender )
		{
			universeAvailable = new ReaderWriterLock();

			if( null == battleHandlers ) {
				battleHandlers = new Hashtable();
			}

			battleHandlers.Add( BattleType.BATTLE, new BattleHandler( AddBattle ) );
			battleHandlers.Add( BattleType.TOURNAMENT, new BattleHandler( AddTournament ) );
			battleHandlers.Add( BattleType.FRIENDLY, new BattleHandler( AddFriendly ) );
		}
		
		#endregion

		#region Static Utilities

		/// <summary>Retorna uma factory dado o seu nome, categoria e pertena</summary>
		public static bool ContainsFactories( string appliesTo, string category ) {
			Hashtable all = (Hashtable) factories[appliesTo];
			ResourceBuilder builder = (ResourceBuilder) all[category];
			
			if( null == builder ) {
				return false;
			}
			
			return true;
		}
		
		/// <summary>Retorna uma factory dado o seu nome, categoria e pertena</summary>
		public static ResourceBuilder getFactories( string appliesTo, string category )
		{
			return getFactories( factories, appliesTo, category );
		}
		
		/// <summary>Retorna uma factory dado o seu nome, categoria e pertena</summary>
		public static ResourceBuilder getFactories( Hashtable root, string appliesTo, string category )
		{
			Hashtable all = (Hashtable) root[appliesTo];
			ResourceBuilder builder = (ResourceBuilder) all[category];
			
			if( builder == null ) {
				throw new Exception("NULL! @ Universe::getFactory");
			}
			
			return builder;
		}

		/// <summary>Verifica se exista uma factory dado o seu nome, categoria e pertena</summary>
		public static bool ContainsFactory( string appliesTo, string category, string resource ) {
			ResourceBuilder builder = getFactories(appliesTo, category);
			ResourceFactory fact = (ResourceFactory) builder[resource];

			if( null == fact ) {
				return false;
			}
			
			return true;
		}
		
		/// <summary>Retorna uma factory dado o seu nome, categoria e pertena</summary>
		public static ResourceFactory getFactory( string appliesTo, string category, string resource )
		{
			return getFactory(factories, appliesTo, category, resource);
		}

		/// <summary>Retorna uma factory dado o seu nome, categoria e pertena</summary>
		public static ResourceFactory getFactory( Hashtable root, string appliesTo, string category, string resource )
		{
			ResourceBuilder builder = getFactories(root, appliesTo, category);
			ResourceFactory fact = (ResourceFactory) builder[resource];

			if( fact == null ) {
				throw new Exception(string.Format("NULL! @ Universe::getFactory : {0},{1},{2}",appliesTo,category,resource));
			}
			
			return fact;
		}
		
		/// <summary>Verifica se um determinado ResourceManager passa as restricções</summary>
		public static Result CheckRestrictions( ResourceManager manager )
		{
			Result result = new Result();
			CheckRestrictions(manager, result);
			return result;
		}
		
		/// <summary>Verifica se um determinado ResourceManager passa as restricções</summary>
		public static bool CheckRestrictions( ResourceManager manager, Result result )
		{
			if( Loader.Restrictions == null ) {
				return true;
			}
			
			foreach( Action action in Loader.Restrictions ) {
				if( !action.evaluate(manager) ) {
					result.failed( new ActionFailed(action, 0) );
				}
			}
			
			return result.Ok;
		}

		#endregion
		
		#region Prize Sections
		
		/// <summary>Adiciona um prémio a um ruler</summary>
		public void addPrize( PrizeCategory category, string prize, Ruler ruler )
		{
			if( !prizes.Contains(prize) ) {
				prizes.Add(prize, new PrizeManager(prize));
			}
			PrizeManager manager = (PrizeManager) prizes[prize];
			manager.register(category, TurnCount, ruler);
		}
		
		/// <summary>Adiciona um prémio a um planeta</summary>
		public void addPrize( PrizeCategory category, string prize, Planet planet )
		{
			if( planet.Owner == null ) {
				return;
			}
			addPrize(category, prize, (Ruler) planet.Owner);
		}

		/// <summary>Obtém o vencedor de um prémio</summary>
		public Winner getWinner( string prize )
		{
			PrizeManager prizeManager = (PrizeManager) prizes[prize];
			if ( null == prizeManager ) {
				return null;
			}
			return prizeManager.Gold;
		}
		
		/// <summary>Indica o PrizeManager associado a um prémio</summary>
		public PrizeManager getPrizeManager( string prize )
		{
			if( prizes == null ) {
				return null;
			}
		
			PrizeManager prizeManager = (PrizeManager) prizes[prize];
			if ( null == prizeManager ) {
				return null;
			}
			return prizeManager;
		}
		
		/// <summary>Verifica se o ruler conseguiu algum prémio</summary>
		private void checkRulerConquerPoints( Ruler ruler )
		{
			if( ruler.Planets.Length == 2 ) {
				addPrize(PrizeCategory.Conquer, "FirstToConquer", ruler);
			}
			if( ruler.Planets.Length == 5 ) {
				addPrize(PrizeCategory.Conquer, "FirstTo5", ruler);
			}
			if( ruler.Planets.Length == 10 ) {
				addPrize(PrizeCategory.Conquer, "FirstTo10", ruler);
			}
			if( ruler.Planets.Length == 15 ) {
				addPrize(PrizeCategory.Conquer, "FirstTo15", ruler);
			}
			if( ruler.Planets.Length == 20 ) {
				addPrize(PrizeCategory.Conquer, "FirstTo20", ruler);
			}
		}
		
		#endregion
		
		#region Keywords
		
		/// <summary>Obtém keywords</summary>
		public static ArrayList getKeywords( string appliesTo, string category )
		{
			if( keywords == null ) {
				return null;
			}
			string key = string.Format("{0}-{1}", appliesTo, category);
			object o = keywords[key];
			if( o == null ) {
				return null;
			}
			
			return (ArrayList) o;
		}
		
		#endregion

		#region Constructor

		public Universe() {
			battleHandlers = new Hashtable();
			battleHandlers.Add( BattleType.BATTLE, new BattleHandler( AddBattle ) );
			battleHandlers.Add( BattleType.TOURNAMENT, new BattleHandler( AddTournament ) );
			battleHandlers.Add( BattleType.FRIENDLY, new BattleHandler( AddFriendly ) );
		}

		#endregion

		
	}
}
