// created on 3/10/04 at 10:41 a

using System;
using System.Collections;
using Chronos.Battle;
using Chronos.Interfaces;
using Chronos.Resources;
using Chronos.Utils;
using Chronos.Alliances;
using Chronos.Info;

namespace Chronos.Core {

	/// <summary>
	/// classe que representa um dono de um ou mais planetas
	/// </summary>
	[Serializable]
	public class Ruler : ResourceManager, IResourceOwner, IComparable {

		#region Instance Fields
		
		private string name;
		private int foreignId;
		private Planet[] planets;
		private Planet homePlanet;
		private Hashtable _universeFleets;
		private int vacationTime = -1;
		private bool premium;
		private AllianceMember.Role allianceRank;
		private int allianceId = 0;
		
		private ArrayList prizes;
		private static ArrayList NoPrizes = new ArrayList();

		private Hashtable _currentBattles = null;
		
		private Hashtable _currentIBattles = new Hashtable();
		
		//Cenas da batalha
		private int _victories = 0;
		private int _defeats = 0;
		private int _draws = 0;
		
		[NonSerialized]
		private int rank = -1;
		[NonSerialized]
		private int lastRank = -1;
		
		#endregion
		
		#region Static Members
		
		/// <summary>Gera um ID nico</summary>
		private static int generateId() {
			return Universe.instance.generateRulerId();
		}
		
		public static string IdentifierString {
			get { return "ruler"; }
		}

		public static bool IsSameAlliance(Ruler r1, Ruler r2) {
			return r1.allianceId == r2.allianceId;
		}

		#endregion
	
		#region Ctors
	
		/// <summary>Construtor</summary>
		public Ruler( IResourceManager owner, Hashtable allFactories, string _name )
			: base(owner,allFactories,IdentifierString,generateId()) {
			name = _name;
			planets = new Planet[0];
			_universeFleets = new Hashtable();
			prizes = null;
			foreignId = -1;
			homePlanet = null;
			vacationTime = -1;
			premium = false;

			InitBattleContainer();
		}
		
		/// <summary>Construtor</summary>
		public Ruler( Hashtable allFactories, string _name )
			: this(null,allFactories,_name) {
		}
		
		/// <summary>Construtor</summary>
		public Ruler( string _name )
			: this(null, Universe.factories ,_name) {
		}
		
		/// <summary>Construtor</summary>
		public Ruler( int id, string _name )
			: base(null, IdentifierString, id) {
			name = _name;
			planets = new Planet[0];
			_universeFleets = new Hashtable();
			prizes = null;
			foreignId = -1;
			homePlanet = null;
			vacationTime = -1;
			premium = false;

			InitBattleContainer();
		}

		void InitBattleContainer() {
			if( _currentBattles == null ) {
				_currentBattles = new Hashtable();
				_currentBattles.Add( BattleType.BATTLE, new Hashtable() );
				_currentBattles.Add( BattleType.TOURNAMENT, new Hashtable() );
				_currentBattles.Add( BattleType.FRIENDLY, new Hashtable() );
			}
		}
		
		#endregion
		
		#region Resource Properties
		
		/// <summary>Indica a quantidade de Cultura disponvel</summary>
		public int Culture {
			get { return getResourceCount("culture"); }
		}
		
		/// <summary>Indica a quantidade de Pontuao<summary>
		public int Score {
			get { return getResourceCount("score"); }
		}
		
		/// <summary>Indica o nmero mximo de planetas que o Ruler pode ter</summary>
		public int MaxPlanets {
			get { return getResourceCount("Intrinsic","maxPlanets"); }
		}
		
		/// <summary>Indica o nmero mximo de armadas que o Ruler pode ter</summary>
		public int MaxFleets {
			get { return getResourceCount("Intrinsic","maxFleets"); }
		}
		
		/// <summary>Indica o nmero total de frotas que o utilizador tem</summary>
		public int TotalFleets {
			get {
				int count = 0;
				foreach( Planet planet in Planets ) {
					count += planet.Fleets.Count - 2;
				}
				return count + UniverseFleets.Count;
			}
		}
		
		/// <summary>Indica o custo de um scan</summary>
		public int ScanCost {
			get { return getResourceCount("Intrinsic", "scanCost"); }
		}
		
		/// <summary>Indica a percentagem a incidir sobre a produção</summary>
		public override double ProductionFactor {
			get {
				double sum = 0;
				foreach( Planet planet in Planets ) {
					sum += planet.RawProductionFactor;	
				}
				return sum / planets.Length;
			}
		}
		
		public int AllianceId {
			get { return allianceId; }
			set { allianceId = value; }
		}
		
		public AllianceMember.Role AllianceRank {
			get { return allianceRank; }
			set { allianceRank = value; }
		}
		
		#endregion
        		
		#region Instance Properties
		
		/// <summary>Indica o nome deste ruler</summary>
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		/// <summary>Indica o ID deste ruler noutro contexto</summary>
		public int ForeignId {
			get { return foreignId; }
			set { foreignId = value; }
		}
		
		/// <summary>Indica a que aliança o ruler pertence</summary>
		public Alliance Alliance {
			get { return (Alliance) Owner; }
		}
		
		/// <summary>Indica se o Ruler pertence a uma aliança</summary>
		public bool HasAlliance {
			get { return Owner != null; }
		}
		
		/// <summary>Indica o Rank do Ruler</summary>
		public int Rank {
			get { return rank; }
			set {
				lastRank = rank;
				if( lastRank == -1 ) {
					lastRank = value;
				}
				rank = value;
			}
		}

		/// <summary>Indica o Rank anterior do Ruler</summary>
		public int LastRank {
			get { return lastRank; }
		}
		
		/// <summary>Indica o Home Planet</summary>
		public Planet HomePlanet {
			get { return homePlanet; }
		}
		
		/// <summary>Indica os prémios deste jogador</summary>
		public ArrayList Prizes {
			get {
				if( prizes == null ) {
					return NoPrizes;
				}
				return prizes;
			}
		}
		
		/// <summary>Indica se um jogador é premium</summary>
		public bool Premium {
			get { return premium; }
			set { premium = value; }
		}

		#endregion
		
		#region Vacations
		
		/// <summary>Indica se o jogador está de férias</summary>
		public bool InVacation {
			get { return vacationTime >= 0; }
		}
		
		/// <summary>Indica o turno em que o jogador entrou de férias</summary>
		public int VacationTurns {
			get { 
				return Universe.instance.TurnCount - vacationTime; 
			}
		}
		
		/// <summary>Coloca o jogador de férias</summary>
		public void StartVacations()
		{
			if( InVacation ) {
				return;
			}
			vacationTime = Universe.instance.TurnCount;
		}
		
		/// <summary>Coloca o jogador no activo</summary>
		public void EndVacations()
		{
			vacationTime = -1;
		}
		
		#endregion
		
		#region Resource Methods
		
		/// <summary>Retorna a quantidade de um recurso contando também todos os filhos</summary>
		/// <remarks>O recurso tem de existir no pai e nos filhos para ser contabilizado. Ex.: Pontuação</remarks>
		public override int getResourceCount( string resourceType, string resource )
		{
			int count = getSpecificResourceCount(resourceType, resource);
			if( null != planets ) {
				foreach( Planet planet in planets ) {
					if( planet.isFactoryAvailable( resourceType, resource ) ) {
						count += planet.getSpecificResourceCount( resourceType, resource );
					}
				}
			}
			return count;
		}
		
		/// <summary>Retorna a quantidade de recurso, sem delegar para ninguém</summary>
		public override int getSpecificResourceCount( string resourceType, string resource )
		{
			return base.getResourceCount( resourceType, resource );
		}
		
		#endregion
		
		#region Planet Management Methods
		
		/// <summary>Retorna um Planeta dado o seu ID</summary>
		public Planet getPlanet( int id )
		{
			foreach( Planet planet in Planets ) {
				if( planet.Id == id ) {
					return planet;
				}
			}
			return null;
		}
		
		/// <summary>Adiciona um planeta a este ruler</summary>
		public void addPlanet( Planet _planet  )
		{
			int count = planets.Length;
			if( count >= MaxPlanets ) {
				throw new Exception("Cannot add Planet. Ruler already has a maximum of " + MaxPlanets +" planets." );
			}
		
			Planet[] array = new Planet[ count + 1 ];
			planets.CopyTo(array,0);
			array[count] = _planet;
			planets = array;
			_planet.Owner = this;
			if( homePlanet == null ) {
				homePlanet = _planet;
			}
			Array.Sort(planets);
		}
		
		/// <summary>Retorna o índice no Array do primeiro planeta com o nome 'name'</summary>
		public int getIndex( string name )
		{
			for( int i = 0; i < planets.Length; ++i ) {
				if( planets[i].Name == name ) {
					return i;
				}
			}
			return -1;
		}
		
		/// <summary>Retorna o índice no Array de um Planeta</summary>
		public int getIndex( Planet toFind )
		{
			return Array.BinarySearch(planets, toFind);
		}
		
		/// <summary>Remove um planeta</summary>
		public bool removePlanet( Planet planet )
		{
			int idx = getIndex( planet );
			if( idx < 0 ) {
				return false;
			}
			return removePlanet(idx);
		}
		
		/// <summary>Remove um membro da aliança</summary>
		public bool removePlanet( int idx )
		{
			Planet[] array = new Planet[planets.Length - 1];

			Planet toRemove = planets[idx];
			toRemove.Owner = null;
			
			if( array.Length > 0 ) {
				Array.Copy(planets, 0, array, 0, idx);
				Array.Copy(planets, idx+1, array, idx, planets.Length - idx - 1);
			}
			
			planets = array;
			Array.Sort(planets);

			return true;
		}
		
		/// <summary>Retorna um array com os planetas do ruler</summary>
		public Planet[] Planets {
			get {
				return planets;
			}
		}

		/// <summary>
		/// verifica de se pode adicionar um novo planeta
		/// </summary>
		public bool CanAddPlanet {
			get { return planets.Length != MaxPlanets; }
		}
		
		#endregion
		
		#region ITask Implementation

		/// <summary>Efectua a passagem de turno do Ruler</summary>
		public override void turn()
		{
			try {

				if( InVacation ) {
					return;
				}

				// sanity check
				for( int i = 0; i < Planets.Length; ++i ) {
					Planet planet = Planets[i];
					if( planet.Owner == null ) {
						planet.Owner = this;
					}else {
						if( planet.Owner.Id != Id ) {
							removePlanet(planet);
							i = 0;
						}
					}
				}
			
				Universe.Events.trace("[START] Updating Ruler {0}(id={1})", Name, Id);
				base.turn ();
				if( Planets == null ) {
					Universe.Events.trace("[END] No Planets for {0}(id={1})", Name, Id);
					return;
				}
				
				Universe.Events.trace("[START] Updating Ruler {0}(id={1}) {2} Planets", Name, Id, Planets.Length);
				
				foreach( Planet planet in Planets ) {
					planet.turn();
				}

				BattleTurns();
				
				checkPrizes();
				
				Universe.Events.trace("[END] Updated Ruler {0}(id={1})", Name, Id);
				
			} catch( Exception ex ){
				Log.log("EXCEPTION: " + ex);
				
				string msg = string.Format("Ruler.turn() error! Ruler:{0} UserId:{1}", Id, ForeignId);
				
				Universe.Events.turnError( new Exception(msg, ex) );
				Universe.Events.trace("[EXCEPTION] `{0}' Ruler:{1} UserId: ", ex, Id, ForeignId);
			}
		}
		
		#endregion

		#region IComparable Implementation

		/// <summary>Compara 2 dois Rulers de acordo com a sua pontuao</summary>
		public override int CompareTo( object obj )
		{
			Ruler tmp = obj as Ruler;
			if( null == tmp ) {
				return -1;
			}
			
			int count1 = getResourceCount("Intrinsic", "score");
			int count2 = tmp.getResourceCount("Intrinsic", "score");
			
			if ( count1 == count2 ) {
				return Name.CompareTo(tmp.Name);
			}
			
			if( count1 > count2 ) {
				return -1;
			}
			
			return 1;
		}
		
		public object Clone()
		{
			return MemberwiseClone();
		}
		
		public override string ToString()
		{
			return Name;
		}
		
		#endregion

		#region Battle Properties

		/// <summary>
		/// Indica se o utilizador está numa batlha ou não
		/// </summary>
		/// <returns><code>true</code> se estiver numa batalha,
		/// <code>false</code> caso contrário.</returns>
		public bool IsInBattle {
			get{ return false; }
		}
		
		/// <summary>Indica se  a vez de jogar do ruler em qualquer batalha</summary>
		public bool BattleWaiting {
			get {
				IDictionaryEnumerator iter1 = _currentBattles.GetEnumerator();
				while( iter1.MoveNext() ) {
					IDictionaryEnumerator iter2 = ((Hashtable)iter1.Value).GetEnumerator();
					while( iter2.MoveNext() ) {
						SimpleBattleInfo info = (SimpleBattleInfo)iter2.Value;				
						if( info.IsTurn && !info.IsPositionTime && !info.EnemyIsPositionTime ) {
							return true;
						}
						if( info.IsPositionTime ) {
							return true;
						}		
					}
				}
				return false;
			}
		}

		/// <summary>Indica se  a vez de jogar do ruler em alguma batalha de conquista</summary>
		public bool TimeToBattle {
			get {
				IDictionaryEnumerator iter1 = _currentBattles.GetEnumerator();
				while( iter1.MoveNext() ) {
					IDictionaryEnumerator iter2 = ((Hashtable)iter1.Value).GetEnumerator();
					while( iter2.MoveNext() ) {
						SimpleBattleInfo info = (SimpleBattleInfo)iter2.Value;
						if( info.BattleType == BattleType.BATTLE ) {
							if( info.TurnsLeft >= (info.TurnsPerMove - 8) ) {
								continue;
							}
							if( info.IsTurn && !info.IsPositionTime && !info.EnemyIsPositionTime ) {
								return true;
							}
							if( info.IsPositionTime ) {
								return true;
							}
						}
					}
				}
				return false;
			}
		}
		
		/// <summary>
		/// retorna o número de jogadas que o jogador pode fazer
		/// </summary>
		public int NumberOfMoves {
			get {
				return 6;
			}
		}

		/// <summary>
		/// Número de vitórias
		/// </summary>
		public int Victories {
			get{ return _victories; }
			set{ _victories = value; }
		}

		/// <summary>
		/// Número de derrotas
		/// </summary>
		public int Defeats {
			get{ return _defeats; }
			set{ _defeats = value; }
		}

		public int Draws {
			get{ return _draws; }
			set{ _draws = value; }
		}

		/// <summary>Indica o nmero todal de batalhas travadas</summary>
		public int BattlesFought {
			get {
				return Victories + Defeats + Draws;
			}
		}

		private int ValueForRanking {
			get {
				int dif = _victories - _defeats;
				if( dif < 0 )
					return 6;
				if( dif > 80 )
					return 20;
				return 2*((dif/10)+3);
			}
		}

		/// <summary>
		/// Ranking do Jogador
		/// </summary>
		public string Ranking {
			get {
				switch( ValueForRanking ) {
					case 6:
						return "weak";
					case 8:
						return "impotent";
					case 10:
						return "peacefull";
					case 12:
						return "agressive";
					case 14:
						return "daring";
					case 16:
						return "fearless";
					case 18:
						return "heroic";
					case 20:
						return "legend";
					default:
						return "not-available";
				}
			}
		}

		#endregion

		#region Battle Related Methods

		private void BattleTurns() {
			foreach(Hashtable battles in _currentBattles.Values ) {
				foreach(SimpleBattleInfo info in battles.Values ) {
					info.Turn( );
				}
			}
		}

		public void AddBattle( SimpleBattleInfo battleInfo, IBattle iBattle, BattleType type ) {
			Hashtable battles = (Hashtable)_currentBattles[type];

			battles.Add( battleInfo.BattleId, battleInfo );
			_currentIBattles.Add( battleInfo.BattleId, iBattle );
		}

		public void RemoveBattle( int battleId, BattleType type ) {
			Hashtable battles = (Hashtable)_currentBattles[type];
            
			battles.Remove( battleId );
			_currentIBattles.Remove( battleId );
		}

		public SimpleBattleInfo GetBattle( int battleId, BattleType type ) {
			Hashtable battles = (Hashtable)_currentBattles[type];

			if( battles.ContainsKey( battleId ) ) {
				return (SimpleBattleInfo)battles[battleId];
			}

			return null;
		}

		public ICollection GetAllBattles( BattleType type ) {
			Hashtable battles = (Hashtable)_currentBattles[type];
			
			return battles.Values;
		}

		public IBattle GetIBattle( int battleId, BattleType type ) {
			if( _currentIBattles.ContainsKey( battleId ) ) {
				return (IBattle)_currentIBattles[battleId];
			}

			return null;
		}

		public void LooseAllBattles() {

			try {
				if( _currentBattles != null) {
					foreach( Hashtable battles in _currentBattles.Values ) {
						if( battles.Count > 0 ) {
							IEnumerator iter = battles.Keys.GetEnumerator();
							while( iter.MoveNext() ) {
								BattleInfo battleInfo = Universe.instance.GetBattle( (int)iter.Current );
								if( battleInfo != null )  {
									battleInfo.ForceEndBattle( this );
								}else {
									battles.Remove(iter.Current);
								}
								iter = battles.Keys.GetEnumerator();
							}
						}

					}
				}
			} catch {}
		}

		#endregion

		#region Fleets

		/// <summary>
		/// obtém todas as armadas que estão no universo
		/// </summary>
		public Hashtable UniverseFleets {
			get { return _universeFleets; }
		}

		public bool HasFleetsInConquerState {
			get{ return FleetsInConquerState().Count != 0; }

		}

		/// <summary>
		/// adiciona uma nave que esteja no universo a movimentar-se
		/// ou parada em algum planeta
		/// </summary>
		/// <param name="fleet">fleet em questão</param>
		public void addUniverseFleet( Fleet fleet ) {
			if( !_universeFleets.ContainsKey( fleet.Id ) )
				_universeFleets.Add( fleet.Id, fleet );
		}

		/// <summary>
		/// remove uma fleet
		/// </summary>
		/// <param name="id"></param>
		public void removeUniverseFleet( int id ) {
			_universeFleets.Remove( id );
		}
		
		/// <summary>
		/// retorna a fleet baseada no nome
		/// </summary>
		/// <param name="name">nome da fleet</param>
		/// <returns>o objecto que representa a fleet</returns>
		public Fleet getFleet( string name ) {
			foreach( Planet p in planets ) {
				if( p.hasFleet( name ) ) {
					return p.getFleet( name );
				}
			}

			IDictionaryEnumerator iter = _universeFleets.GetEnumerator();
			while( iter.MoveNext() ) {
				Fleet f = (Fleet)iter.Value;
				if( f.Name == name ) {
					return f;
				}
			}
			return null;
		}

		
		/// <summary>
		/// obtém as fleets em estado de conquista
		/// </summary>
		/// <returns>ArrayList com as fleets em estado de conquista</returns>
		public ArrayList FleetsInConquerState() {
			ArrayList fleets = new ArrayList();
			foreach( Fleet fleet in UniverseFleets.Values ) {
				if( fleet.IsMoving ) {
					continue;
				}

                Coordinate coord = fleet.Coordinate;
				Planet p = Universe.instance.getPlanet( coord );

				if( null != p ) {
					if( !fleet.isQuantityAvailable("ColonyShip", 1) ) {
						continue;
					}

					if( p.Owner != null ) {
						if( p.Owner.Id == fleet.Owner.Id || p.HasProtection || Ruler.IsSameAlliance( (Ruler)p.Owner, (Ruler)fleet.Owner) )  {
							continue;
						}
					} else {
						if( p.OldOwner != null ) {
							if(  p.OldOwner.Id == Id ) {
								continue;
							}
						}
					}

					if( p.IsVisible && !p.IsInBattle ) {
						fleets.Add( fleet );
					}
				}
			}
			return fleets;
		}

		/// <summary>
		/// verifica se o ruler possui a fleet
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool hasFleet( string name ) {
			foreach( Planet p in planets ) {
				if( p.hasFleet( name ) ) {
					return true;
				}
			}

			IDictionaryEnumerator iter = _universeFleets.GetEnumerator();
			while( iter.MoveNext() ) {
				Fleet f = (Fleet)iter.Value;
				if( f.Name == name ) {
					return true;
				}
			}
			return false;
		}

		public ArrayList getCoordinateFleets( Coordinate c ) {
			ArrayList fleets = new ArrayList();
			IDictionaryEnumerator iter = _universeFleets.GetEnumerator();
			while( iter.MoveNext() ) {
				Fleet f = (Fleet)iter.Value;
				if( f.Coordinate.CompareTo( c ) == 0 ) {
					fleets.Add(f);
				}
			}
			return fleets;
		}

		#endregion
		
		#region Prizes
		
		/// <summary>Verifica se o ruler tem prémios a atribuir</summary>
		private void checkPrizes()
		{
			if( getUnavailableFactories("Research").Count == 0
			    && getAvailableFactories("Research").Count == 0
			    && current("Research") == null
			    && queueCount("Research") == 0
			   ){
				Universe.instance.addPrize(PrizeCategory.Research, "FirstToKnowAll", this);
			}
		}
		
		/// <summary>Adiciona um prmio</summary>
		internal void addPrize( Winner winner )
		{
			if( prizes == null ) {
				prizes = new ArrayList();
			}
			prizes.Add(winner);
		}
		
		#endregion
		
		#region Teletransportation
		
		/// <summary>Indica todos os planetas que podem teletransportar recursos intrinsecos</summary>
		public Planet[] IntrinsicTeletransportationPlanets {
			get {
				ArrayList list = new ArrayList();
				foreach( Planet planet in Planets ) {
					if( planet.CanTeletransportIntrinsic ) {
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
				foreach( Planet planet in Planets ) {
					if( planet.CanTeletransportFleets ) {
						list.Add(planet);
					}
				}
				return (Planet[]) list.ToArray(typeof(Planet));
			}
		}
		
		#endregion
        
	};
}
