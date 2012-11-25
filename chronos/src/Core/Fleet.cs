using System;
using System.Collections;
using Chronos.Battle;
using Chronos.Exceptions;
using Chronos.Interfaces;
using Chronos.Resources;

namespace Chronos.Core {
	/// <summary>
	/// classe que representa uma fleet
	/// </summary>
	[Serializable]
	public class Fleet : IIdentifiable, IBattle, ICloneable {

		#region private fields
		
		// identificador da Fleet
		private int _id;
		
		//nome da fleet
		private string _name;

		// ruler owner da fleet
		private IResourceManager _owner = null;
	
		//conteudo da fleet
		private Hashtable _ships = new Hashtable();

		//conteudo da fleet
		//private Hashtable _shipsResources = null;

		//tempo que falta para chegar ao destino
		private int _hoursToArrive = 0;

		//localizao da fleet no universo
		private Coordinate _currentLocation;
		
		//localizao final da fleet depois de se mover
		private Coordinate _destinationCoord = null;

		//diz se a fleet pode-se mover (visto que cada planeta tem uma fleet
		//intrinseca que faz parte dele e que n se pode mexer)
		private bool _isMoveable;

		// indica se a fleet está numa batalha ou não
		private bool _isInBattle = false;

		// indica se a fleet é de defesa
		private bool _isDefense = false;
		
		/*//cenas para a paneleirice
		//step usado para quando est em movimento
		private int _step;
		private int _auxcount = 0;*/

		#endregion

		#region private methods

		private Resource GetShipResource( string ship ) {
			return Universe.getFactory("planet", "Unit", ship).create( );
		}

		private void CheckLivingPlanet() {
			if( Owner is Ruler ) {
				Planet p = Universe.instance.getPlanet(Coordinate);
				if( p != null && p.WonABattle != null && p.WonABattle.Id == Owner.Id && p.OldOwner != null && p.Owner == null ) {
					p.Owner = p.OldOwner;
					((Ruler)p.Owner).addPlanet(p);
					p.WonABattle = null;
					p.OldOwner = null;
					
					CheckFleetsInOrbit( p );
				}
			}
		}

		private void CheckFleetsInOrbit( Planet p ) {
			Ruler ruler = ((Ruler)p.Owner);

			IDictionaryEnumerator iter = ruler.UniverseFleets.GetEnumerator();
			while( iter.MoveNext() ) {
				Fleet fleet = iter.Value as Fleet;
				if( fleet != null ) {
					if( fleet.Coordinate.CompareTo( this.Coordinate ) == 0  ) {
						ruler.removeUniverseFleet( fleet.Id );
						p.addFleet( fleet );
						iter = ruler.UniverseFleets.GetEnumerator();
					}
				}
			}
		}

		#endregion

		#region public methods

		/// <summary>
		/// responsavel por mover esta fleet
		/// </summary>
		public void moveFleet(){
			if( _hoursToArrive != 0 ) {
				--_hoursToArrive;
				if( _hoursToArrive == 0 ) {
					_currentLocation = _destinationCoord;
				}
			}
		}
		
		/// <summary>Indica a distncia entre duas coordenadas</summary>
		public static int TravelTime( Coordinate source, Coordinate destination ) {
			int time;
#if DEBUG
			time = 1;
#else
			//no maximo dos maximos o hoursToArrive com 20 sectores, 20 sistemas e
			//3 galaxias o que equivale a de 2400 turnos (sem upgrades nas naves)
			int gCount = Math.Abs( destination.Galaxy - source.Galaxy );
			int syCount = Math.Abs( destination.System - source.System );
			int sCount = Math.Abs( destination.Sector - source.Sector );
			int pCount = Math.Abs( destination.Planet - source.Planet );
			
			time = gCount*800 + syCount*40 + sCount*2 + (pCount==0?1:pCount);
#endif
			
			return time;
		}
		
		/// <summary>
		/// inicia o deslocamento de uma fleet
		/// </summary>
		/// <param name="destination">coordenada de destino da fleet</param>
		public bool startMoving( Coordinate destination ) {
			//TO DO: Mudar isto para results
			if( _ships == null || _ships.Count == 0 ) {
				throw new NoShipsToMoveException();
			}
			if( ! _isMoveable ) {
				throw new FleetNotMoveableException();
			}
			if( IsMoving ) {
				throw new AlreadyInMoveException();
			}

			if( Universe.instance.IsDestinationReachable( this, destination ) ) {

				_hoursToArrive = TravelTime(_currentLocation, destination);
				_destinationCoord = destination;

				//regista a fleet no universo
				Universe.instance.registerMovingFleet( this );

				CheckLivingPlanet();

				return true;
			}
			return false;
		}

		/// <summary>
		/// adiciona uma determinada quantidade de um tipo de naves
		/// </summary>
		/// <param name="ship"></param>
		/// <param name="quant"></param>
		//ainda n sei se vai ser assim
		public void addShip( string ship, int quant /*, Resource res*/ ) {
			if( _ships.ContainsKey(ship) ) {
				_ships[ship] = (int)_ships[ship] + quant;
			}else{
				_ships[ship] = quant;
				//if( _shipsResources == null )
				//	_shipsResources = new Hashtable();
			}
			//_shipsResources[ship] = res;
		}

		/// <summary>
		/// remove todas as naves
		/// </summary>
		public void removeAllShips( ) {
			Ships.Clear();
			//ShipsResources.Clear();
		}

		/// <summary>
		/// remove uma ship desta fleet
		/// </summary>
		/// <param name="ship">Nome da nave</param>
		/// <param name="quant">Quantidade</param>
		public void removeShip( string ship, int quant ) {
			Ships[ship] = (int)Ships[ship] - quant;
			
			if( (int)Ships[ship] == 0 ) {
				Ships.Remove( ship );
				//ShipsResources.Remove( ship );
			}
		}

		/// <summary>
		/// remove uma ship desta fleet
		/// </summary>
		/// <param name="ship">Nome da nave</param>

		public void removeShip( string ship ) {
			_ships.Remove( ship );
			//_shipsResources.Remove( ship );
		}

		/// <summary>
		/// troca as naves da fleet passada para a corrente
		/// </summary>
		/// <param name="sourceFleet">Fleet de origem</param>
		/// <param name="ship">names a mover</param>
		/// <param name="quant">quantidade de naves a mover</param>
		public bool swapShips( Fleet sourceFleet, string ship, int quant ) {
			//Resource res = (Resource)Universe.getFactory("planet", "Unit", ship).create();
			if( sourceFleet.HasShip( ship ) && sourceFleet.HasQuantity(ship,quant) ) {
				if( HasShip(ship) || ( TotalUnitTypes < MaxUnitTypes ) || ( !IsMoveable && !IsDefenseFleet) ) {
					sourceFleet.removeShip( ship, quant );
					addShip( ship, quant );
					return true;
				}
			}
			return false;
		}

		public bool HasShip( string ship ) {
			if( _ships.ContainsKey(ship) )
				return true;

			return false;
		}

		public bool HasQuantity(string ship, int quant ) {
			if( Ships.ContainsKey( ship ) ) {
				int q = (int)Ships[ship];
				if(q >= quant )
					return true;
			}
			return false;
		}

		/// <summary>
		/// verifica se a fleet possui uma dada quantidade de naves
		/// </summary>
		/// <param name="ship">nome da nave</param>
		/// <param name="quantity">quantidade</param>
		/// <returns><code>true</code> se a quantidade estiver disponivel, <code>false</code> caso contrário</returns>
		public bool isQuantityAvailable( string ship, int quantity ) {
			if( _ships.ContainsKey(ship) ) {
				int quant = (int)_ships[ship];
				if( quant >= quantity )
					return true;
			}
			return false;
		}

		#endregion
		
		#region Properties
		
		/// <summary>
		/// propriedade para obter(ou fazer set) o owner desta fleet
		/// </summary>
		public IResourceManager Owner {
			get{ return _owner; }
			set{ _owner = value; }
		}

		/// <summary>
		/// verifica se a fleet se est a mover
		/// </summary>
		public bool IsMoving {
			get{ return _hoursToArrive != 0; }
		}
		
		/// <summary>
		/// horas que faltam para chegar. 0 quer dizer que j chegou.
		/// </summary>
		public int HoursToArrive {
			get{ return _hoursToArrive; }
			set{ _hoursToArrive = value; }
		}

		/// <summary>
		/// position of the fleet
		/// </summary>
		public Coordinate Coordinate {
			get{ return _currentLocation; }
			set{ _currentLocation = value; }
		}

		/// <summary>
		/// position of the fleet
		/// </summary>
		public Coordinate DestinyCoordinate {
			get{ return _destinationCoord; }
			set{ _destinationCoord = value; }
		}

		/// <summary>
		/// retorna uma Hashtable com todas as fleets
		/// </summary>
		public Hashtable Ships {
			get {
				if( _ships == null ) {
					_ships = new Hashtable();
				}
				return _ships;
			}
		}
		
		/// <summary>
		/// retorna uma Hashtable com todas as fleets
		/// </summary>
		public ArrayList ShipsResources {
			get {
				ArrayList resources = new ArrayList();
				foreach( string name in Ships.Keys ) {
					resources.Add( GetShipResource( name ) );
				}
				return resources;
			}
		}

		/// <summary>
		/// afecta ou retorna o nome da fleet
		/// </summary>
		public string Name {
			get{ return _name; }
			set{ _name = value; }
		}

		/// <summary>
		/// diz se a fleet se pode mexer ou no
		/// </summary>
		public bool IsMoveable {
			get{ return _isMoveable; }
		}
		
		/// <summary>Retorna o ID desta entidade</summary>
		public int Id {
			get { return _id; }
		}

		/// <summary>
		/// <code>true</code> se a fleet tem naves, <code>false</code>
		/// caso contrário.
		/// </summary>
		public bool HasShips {
			get { return _ships != null && _ships.Count != 0; }
		}

		/// <summary>
		/// verifica ou diz se uma fleet está num sitio bom para entrar numa
		/// batalha
		/// </summary>
		public bool GoodForBattle {
			get{
				Planet p = Universe.instance.getPlanet( Coordinate );
				if( p.Owner == null || Owner is Planet || IsMoving || p.Owner.Id == this.Owner.Id || IsInBattle || !HasShips ) {
					return false;
				}

				return true;
			}
		}
		
		/// <summary>Indica o nmero total de naves, independentemente do seu tipo</summary>
		public int TotalUnits {
			get {
				if( !HasShips ) {
					return 0;
				}
				int count = 0;
				foreach( int i in Ships.Values ) {
					count += i;
				}
				return count;
			}
		}

		public int TotalUnitTypes {
			get { return Ships.Values.Count; }
		}

		public static int MaxUnitTypes {
			get{ return 8; }
		}

		public int UnitTypeCount {
			get { return _ships.Keys.Count; }
		}

		public bool IsDefenseFleet {
			get{ return _isDefense; }
			set{ _isDefense = value; }
		}

		public bool CanBeRemoved {
			get {
				return ((IsMoveable && !IsMoving && !IsDefenseFleet && Owner is Planet) || ( !HasShips && Owner is Ruler )) && !IsInBattle;
			}
		}

		public bool CanBeMoved {
			get{ return IsMoveable && !IsMoving && !IsInBattle && HasShips;}
		}

		#endregion
		
		#region constructors
		
		/// <summary>Gera um ID único</summary>
		private static int generateId()
		{
			return Universe.instance.generateFleetId();
		}
		
		/// <summary>
		/// construtor de fleet
		/// </summary>
		/// <param name="name">nome que se vai dar  fleet</param>
		/// <param name="position">posio inicial da fleet</param>
		/// <param name="owner"></param>
		/// <param name="isMoveable">se a fleet se pode mover ou no</param>
		public Fleet( string name, Coordinate position, IResourceManager owner, bool isMoveable ) {
			_name = name;
			_currentLocation = position;
			_isMoveable = isMoveable;
			_owner = owner;
			_id = generateId();
			
		}

		/// <summary>
		/// Fleet com
		/// </summary>
		/// <param name="name">nome que se vai dar  fleet</param>
		/// <param name="position">posio inicial da fleet</param>
		public Fleet( string name, Coordinate position, IResourceManager owner )
			: this( name, position, owner, true ){}

		#endregion

		#region Battle

		/// <summary>
		/// verifica se a fleet se est a mover
		/// </summary>
		public bool IsInBattle {
			get{ return _isInBattle; }
			set{ _isInBattle = value; }
		}

		public ArrayList GetBattleElements() {
			ArrayList battleShips = new ArrayList();
			if( 0 != Ships.Count ) {
				IDictionaryEnumerator iter = Ships.GetEnumerator();
				while( iter.MoveNext() ) {
					Element element = new Element();

					string key = (string)iter.Key;
				
					element.Quantity = (int)Ships[key];
					element.Type = key;
					
					battleShips.Add( element );
				}
				Ships.Clear();
				//ShipsResources.Clear();
			}

			return battleShips;
		}

		public void SetBattleElements(ArrayList elements) {
			foreach( Element e in elements ) {
				if( Ships.ContainsKey(e.Type) ) {
					int v = (int)Ships[e.Type];
					Ships[e.Type] = v + e.Quantity; 
				}else {
					Ships[e.Type] = e.Quantity; 
				}
				//ShipsResources[e.Type] = e.Resource;
			}
		}

		#endregion
		
		#region ICloneable Implementation

		public object Clone() {
			Fleet f = new Fleet(this.Name,this.Coordinate,this.Owner);

			f._ships = Ships.Clone() as Hashtable;
//			f._shipsResources = ShipsResources.Clone() as Hashtable;

			return f;
		}
		
		public Fleet DeepClone()
		{
			return (Fleet) Clone();
		}
		
		#endregion
	}
}

