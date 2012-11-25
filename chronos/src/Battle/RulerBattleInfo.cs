using System;
using System.Collections;
using Chronos.Core;
using Chronos.Interfaces;
using Chronos.Messaging;
using Chronos.Resources;
using DesignPatterns;

namespace Chronos.Battle {
	
	[Serializable]
	public class RulerBattleInfo : MessageManager,ICloneable {

		#region Fields

		private static FactoryContainer _battleUtilFactory = new FactoryContainer( typeof ( BattleUtilFactory ) );

		protected int _ownerId;
		private int _battleId;
		private int _id;
		private BattleType _type;

		//contentores

		private ArrayList _initialContainer;
		private Hashtable _battleField = new Hashtable();
		private Hashtable _unitsDestroyed = new Hashtable();
		private Hashtable _resourcesDestroyed = new Hashtable();
		private string _battleType;
		private ArrayList _buildingsDestroyed = null;
		
		#endregion

		#region Properties

		public int OwnerId {
			get { return _ownerId; }
			set { _ownerId = value; }
		}

		public int BattleId {
			get { return _battleId; }
			set { _battleId = value; }
		}

		public BattleType BattleType {
			get { return _type; }
			set { _type = value; }
		}

		public ArrayList InitialContainer {
			get { return _initialContainer; }
			set { _initialContainer = value; }
		}

		public bool InitialContainerHasUnits {
			get {
				if( _initialContainer == null ) {
					return false;
				}
				return _initialContainer.Count != 0;
			}
		}

		public Hashtable BattleField {
			get { return _battleField; }
			set { _battleField = value; }
		}

		public Hashtable UnitsDestroyed {
			get { return _unitsDestroyed; }
			set { _unitsDestroyed = value; }
		}

		public Hashtable ResourceDestroyed {
			get { return _resourcesDestroyed; }
			set { _resourcesDestroyed = value; }
		}

		public bool HasUnits {
			get { return BattleField.Count != 0; }
		}

		public bool Won {
			get {
				return ((BattleUtilBase)_battleUtilFactory.create(_battleType)).HasWon(this);
			}
		}

		public int PartialScore {
			get {
				int light = (int)UnitsDestroyed["light"];
				int medium = (int)UnitsDestroyed["medium"];
				int heavy = (int)UnitsDestroyed["heavy"];
				int animal = (int)UnitsDestroyed["animal"];
				int building = (int)UnitsDestroyed["building"];

				return (light + (medium + animal)*2 + heavy*4 + building*1000) /3;
			}
		}

		public IBattle IBattle {
			get{
				Ruler ruler = Universe.instance.getRuler( OwnerId );
				return ruler.GetIBattle( BattleId, BattleType );
			}

		}

		#endregion

		#region Copy

		private ArrayList CopyInitialContainer() {
			if( InitialContainer == null ) {
				return null;
			}

			if( InitialContainer.Count == 0 ) {
				return new ArrayList();
			}

			ArrayList copy = new ArrayList();
			foreach( Element e in InitialContainer ) {
				copy.Add( e.Clone( ) );
			}
			return copy;
		}

		private Hashtable CopyBattleField() {
			IDictionaryEnumerator iter = BattleField.GetEnumerator( );
			Hashtable copy = new Hashtable();
			while( iter.MoveNext( ) ) {
				copy.Add( iter.Key, ((Element)iter.Value).Clone( ) );
			}
			return copy;
		}

		#endregion Copy

		#region Populate

		private void PopulateInitialContainer() {
			IBattle ibattle =  Universe.instance.GetIBattle( OwnerId, BattleId, BattleType );
			_initialContainer = ibattle.GetBattleElements( );
		}

		private void PopulateShips() {
			PopulateInitialContainer();
			((BattleUtilBase)_battleUtilFactory.create(_battleType)).Init(this);
		}

		#endregion Populate

		#region Moves

		private bool MoveToTop( Element e , Element d ) {
			if( e.Type == d.Type ) {
				d.Quantity += e.Quantity;
				return true;
			}
			return false;
		}

		private bool MoveAllSrc( Element e, string dst ) {
			_initialContainer.Remove( e );
			Element d = (Element)BattleField[dst];

			if( null == d ) {
				BattleField[dst] = e;
			}else {
				return MoveToTop( e, d);
			}
			return true;
		}

		private bool MoveAll( string src, string dst ) {
			Element s = (Element)BattleField[src];
			Element d = (Element)BattleField[dst];
			BattleField.Remove( src );
			if( null == d ) {
				BattleField[dst] = s;
			}else {
				return MoveToTop( s, d);
			}
			return true;
		}

		#endregion Moves

		#region Private

		private bool HasColonyShip( RulerBattleInfo info ) {
			if( info.BattleField != null ) {
				foreach(Element e in info.BattleField.Values ) {
					if( e.Type.ToLower() == "colonyship" ) {
						return true;
					}
				}
			}

			if( info.InitialContainerHasUnits ) {
				foreach(Element e in info.InitialContainer ) {
					if( e.Type.ToLower() == "colonyship" ) {
						return true;
					}
				}	
			}
			return false;
		}

		#endregion

		#region Public

		public bool HasUnit(string unit) {
			foreach( Element e in BattleField.Values ) {
				if( e.Type.CompareTo(unit) == 0 ) {
					return true;
				}
			}
			return false;
		}

		public void ForcePositioning() {
			if( _initialContainer.Count != 0 ) {
				for( int i = BattleInfo.ROWCOUNT; i >= 1 ; --i ) {
					for( int j = BattleInfo.COLUMNCOUNT; j >= 1 ; --j ) {
						string coord = string.Format("{0}_{1}",i,j);
						if( !BattleField.ContainsKey( coord ) ) {
							Element elem = _initialContainer[0] as Element;
							_initialContainer.RemoveAt( 0 );
							SectorSrcMove( elem, coord, elem.Quantity);
							if( _initialContainer.Count == 0 )
								return;
						}
					}
				}
			}
		}

		public bool SectorSrcMove( Element e, string dst, int quant ) {
			if( e.Quantity == quant ) {
				return MoveAllSrc( e, dst );
			} else {
				Element d = (Element) e.Clone( );
				d.Quantity = quant;
				BattleField[dst] = d;
				e.Quantity -= quant;
			}
			return true;
		}

		public bool SectorHasElements( string sector ) {
			return BattleField.ContainsKey( sector );
		}

		public Element SectorGetElement( string sector ) {
			return BattleField[sector] as Element;
		}

		public void SectorRemoveElement( string sector ) {
			BattleField.Remove( sector );
		}

		public bool SectorMove( string src, string dst, int quant ) {
			Element e = BattleField[src] as Element;
			if( e.Quantity == quant ) {
				return MoveAll(src, dst);
			} else {
				e.Quantity -= quant;
				if( BattleField.ContainsKey(dst)) {
					e = (Element) BattleField[dst];
					e.Quantity += quant;
				}else {
					e = (Element) e.Clone( );
					e.Quantity = quant;
					BattleField[dst] = e;
				}
			}
			return true;
		}

		public void RemoveUnit(string unit) {
			IDictionaryEnumerator iter = BattleField.GetEnumerator();

			while( iter.MoveNext() ) {
				Element e = iter.Value as Element;
				if( e.Type.CompareTo(unit) == 0 ) {
					BattleField.Remove(iter.Key);
					iter = BattleField.GetEnumerator();
				}
			}
		}

		public void AddUnitsDestroyed( int q, string type ) {
			UnitsDestroyed[type] = ((int)UnitsDestroyed[type]) + q;
		}

		public void AddResourceDestroyed(int q, Resource resource) {
			if( _resourcesDestroyed.ContainsKey(resource) ) {
				int d = (int)_resourcesDestroyed[resource];
				_resourcesDestroyed[resource] = d + q;
			}else {
				_resourcesDestroyed[resource] = q;
			}
		}

		public void AddBuildingDestroyed( string buildingName ) {
			if( _buildingsDestroyed == null ) {
				_buildingsDestroyed = new ArrayList();
			}
			_buildingsDestroyed.Add( buildingName );
		}

		public void ClearUnits( RulerBattleInfo enemy ) {
			foreach( Element e in BattleField.Values ) {
				enemy.AddUnitsDestroyed(e.Quantity,e.Unit.UnitType);
				enemy.AddResourceDestroyed(e.Quantity,e.Resource);
			}

			foreach( Element e in InitialContainer ) {
				enemy.AddUnitsDestroyed(e.Quantity,e.Unit.UnitType);
				enemy.AddResourceDestroyed(e.Quantity,e.Resource);
			}

			BattleField.Clear();
			InitialContainer.Clear();
		}

		#endregion

		#region Static

		public static string InvertSector( string sector ) {
			string[] newSector = sector.Split( '_' );
			int x = int.Parse( newSector[0] );
			int y = int.Parse( newSector[1] );
			x = 8 - x + 1;
			y = 8 - y + 1;
			return x + "_" + y;
		}

		public static string NextSector( string sector, string position ) {
			string[] newSector = sector.Split( '_' );
			int x = int.Parse( newSector[0] );
			int y = int.Parse( newSector[1] );
			switch( position ) {
				case "n":
					return (x+1).ToString() + "_" + newSector[1];
				case "s":
					return (x-1).ToString() + "_" + newSector[1];
				case "w":
					return newSector[0] + "_" + (y+1).ToString();
				default:
					return newSector[0] + "_" + (y-1).ToString();
			}
		}

		public static string LeftSector( string sector, string position ) {
			string[] newSector = sector.Split( '_' );
			int x = int.Parse( newSector[0] );
			int y = int.Parse( newSector[1] );
			switch( position ) {
				case "n":
					return newSector[0] + "_" + (y+1).ToString();
				case "s":
					return newSector[0] + "_" + (y-1).ToString();
				case "w":
					return (x-1).ToString() + "_" + newSector[1];
				default:
					return (x+1).ToString() + "_" + newSector[1];
			}
		}

		public static string RightSector( string sector, string position ) {
			string[] newSector = sector.Split( '_' );
			int x = int.Parse( newSector[0] );
			int y = int.Parse( newSector[1] );
			switch( position ) {
				case "n":
					return newSector[0] + "_" + (y-1).ToString();
				case "s":
					return newSector[0] + "_" + (y+1).ToString();
				case "w":
					return (x+1).ToString() + "_" + newSector[1];
				default:
					return (x-1).ToString() + "_" + newSector[1];
			}
		}

		public static string InvertPosition( Element.PositionType position ) {
			switch( position ) {
				case Element.PositionType.N:
					return Element.PositionType.S.ToString().ToLower();
				case Element.PositionType.S:
					return Element.PositionType.N.ToString().ToLower();
				case Element.PositionType.W:
					return Element.PositionType.E.ToString().ToLower();
				default:
					return Element.PositionType.W.ToString().ToLower();
			}
		}

		#endregion

		#region Constructors

		public RulerBattleInfo( int ownerId, int battleId, BattleType type, string battleType ) {
			_ownerId = ownerId;
			_battleId = battleId;
			_type = type;
			_battleType = battleType;

			PopulateShips();

			_id = Universe.instance.generateRulerBattleInfoId( );

			UnitsDestroyed["special"] = 0;
			UnitsDestroyed["light"] = 0;
			UnitsDestroyed["medium"] = 0;
			UnitsDestroyed["heavy"] = 0;
			UnitsDestroyed["animal"] = 0;
			UnitsDestroyed["building"] = 0;
		}

		public RulerBattleInfo() {
			_id = Universe.instance.generateRulerBattleInfoId( );
		}

		#endregion

		#region ICloneable Members

		public object Clone() {
			RulerBattleInfo r = new RulerBattleInfo();
			r.OwnerId = OwnerId;
			r.BattleId = BattleId;
			r.BattleType = BattleType;
			r._battleType = _battleType;
			r._id = _id;
		
			r.InitialContainer = CopyInitialContainer();
			r._battleField = CopyBattleField();
			r.UnitsDestroyed = UnitsDestroyed.Clone() as Hashtable;

			r.ResourceDestroyed = ResourceDestroyed.Clone() as Hashtable;
			return r;
		}

		#endregion

		#region MessageManager Implementation
		
		/// <summary>Indica um identificador deste Handler</summary>
		public override int HandlerId {
			get { return _id; }
		}
		
		/// <summary>Indica uma string que identifica o tipo deste handler (ruler, planet, ...)</summary>
		public override string HandlerIdentifier {
			get { return "battle"; }
		}

		#endregion

		#region IEndBAttle

		protected void EndBattle() {
			IBattle iBattle = IBattle;
			if( HasUnits ) {
				ArrayList elements = new ArrayList();
				foreach(Element e in BattleField.Values ) {
					if( !e.IsBuilding ) {
						elements.Add( e );
					}
				}				
				iBattle.SetBattleElements( elements );
			}
			if( _buildingsDestroyed != null && iBattle is Planet ) {
				Planet p = iBattle as Planet;
				foreach( string building in _buildingsDestroyed ) {
					p.take( building, 1);
				}
			}
		}

		public virtual void BattleEnd( RulerBattleInfo enemyBattleInfo ) {
			EndBattle();
			Ruler ruler = Universe.instance.getRuler( OwnerId );
			
			IBattle iBattle = IBattle;
			if( Won ) {
				RulerWins(ruler, iBattle);
			} else {
				RulerLoses(ruler, iBattle, enemyBattleInfo );
			}
			iBattle.IsInBattle = false;
		}

		private void RulerLoses(Ruler ruler, IBattle iBattle, RulerBattleInfo enemyBattleInfo ) {
			++ruler.Defeats;
			Ruler enemy = Universe.instance.getRuler(enemyBattleInfo.OwnerId);
			if( iBattle is Planet ) {
				Planet p = iBattle as Planet;
				if( HasColonyShip( enemyBattleInfo ) && enemy.CanAddPlanet ) {
					p.FleetsToOrbit();
					p.OldOwner = ruler;
					p.Owner = null;
					p.WonABattle = enemy;
					ruler.removePlanet(p);
				}else {
					p.StartImmunity();
				}

				int toDestroy = BattleInfo.GetLostScore(enemyBattleInfo,ruler,enemy) / 500;
				p.DestroyRandomBuildings( toDestroy );
				return;
			}
			if( iBattle is Fleet ) {
				Fleet f = (Fleet)iBattle;
				ruler.removeUniverseFleet(f.Id);
			}
		}

		private static void RulerWins(Ruler ruler, IBattle iBattle) {
			++ruler.Victories;
			if( iBattle is Fleet ) {
				Planet p = Universe.instance.getPlanet( iBattle.Coordinate );
				Fleet f = (Fleet)iBattle;
				p.WonABattle = (Ruler) f.Owner;
				return;
			}
			if( iBattle is  Planet ) {
				Planet p = (Planet)iBattle;
				p.WonABattle = null;
				p.OldOwner = null;
				p.Owner = ruler;
				p.StartImmunity();
			}
		}

		public virtual void TournamentEnd() {}

		public virtual void FriendlyEnd() {}

		#endregion
	}
}
