// created on 16-01-2005 at 18:15

using System;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Info {

	[Serializable]
	public class Scan {
	
		#region Private Fields
		
		private int scanLevel;
		private bool intercepted;
		private bool success;
		private int owner;
		private int turn;
		private Coordinate coordinate;
		private int id;
		private int sourcePlanetId;
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Identifica o scan</summary>
		public int Id {
			get { return id; }
			set { id = value; }
		}
		
		/// <summary>Indica o Id do planeta que realizou o scan</summary>
		public int SourcePlanetId {
			get { return sourcePlanetId; }
			set { sourcePlanetId = value; }
		}
		
		/// <summary>Indica o alvo</summary>
		public Planet TargetPlanet {
			get { return Universe.instance.getPlanet(Target); }
		}
		
		/// <summary>Indica a coordenada alvo</summary>
		public Coordinate Target {
			get { return coordinate; }
			set { coordinate = value; }
		}
		
		/// <summary>Indica o nvel de Scan quando foi feito este scan</summary>
		public int ScanLevel {
			get { return scanLevel; }
			set { scanLevel = value; }
		}
		
		/// <summary>Indica se o scan foi interceptado</summary>
		public bool Intercepted {
			get { return intercepted; }
			set { intercepted = value; }
		}
		
		/// <summary>Indica se o scan foi bem sucedido</summary>
		public bool Success {
			get { return success; }
			set { success = value; }
		}
		
		/// <summary>Indica o owner do planeta a scannar</summary>
		public int TargetPlanetOwner {
			get { return owner; }
			set { owner = value; }
		}
		
		/// <summary>Indica o turno em que o scan foi relaizado</summary>
		public int Turn {
			get { return turn; }
			set { turn = value; }
		}
		
		#endregion
		
		#region General Attribute Fields
		
		private int culture;
		private int numberOfFleets;
		private int totalShips;
		private int totalSoldiers;
		private int totalBarracks;
		private bool hasCommsSatellite;
		private bool hasStarPort;
		private bool hasGate;
		private bool hasStarGate;
		private bool inBattle;

		private bool hasHospital;
		private bool hasLandReclamation;
		private bool hasMineralExtractor;
		private bool hasSpa;
		private bool hasStockMarkets;
		private bool hasWaterReclamation;
		private bool hasTurret;
		private bool hasIonCannon;
		
		private string[] rareResources;
		
		#endregion
		
		#region General Attributes
		
		public string[] RareResources {
			get { return rareResources; }
			set { rareResources = value; }
		}
		
		public int Culture {
			get { return culture; }
			set { culture = value; }
		}
		
		public int NumberOfFleets {
			get { return numberOfFleets; }
			set { numberOfFleets = value; }
		}
		
		public int TotalShips {
			get { return totalShips; }
			set { totalShips = value; }
		}
		
		public int TotalSoldiers {
			get { return totalSoldiers; }
			set { totalSoldiers = value; }
		}
		
		public int TotalBarracks {
			get { return totalBarracks; }
			set { totalBarracks = value; }
		}
		
		public bool HasStarPort {
			get { return hasStarPort; }
			set { hasStarPort = value; }
		}
		
		public bool HasCommsSatellite {
			get { return hasCommsSatellite; }
			set { hasCommsSatellite = value; }
		}
		
		public bool HasGate {
			get { return hasGate; }
			set { hasGate = value; }
		}
		
		public bool HasStarGate {
			get { return hasStarGate; }
			set { hasStarGate = value; }
		}
		
		/// <summary>Indica se o planeta est em batalha</summary>
		public bool InBattle {
			get { return inBattle; }
			set { inBattle = value; }
		}

		public bool HasHospital {
			get { return hasHospital; }
			set { hasHospital = value; }
		}

		public bool HasLandReclamation {
			get { return hasLandReclamation; }
			set { hasLandReclamation = value; }
		}

		public bool HasMineralExtractor {
			get { return hasMineralExtractor; }
			set { hasMineralExtractor = value; }
		}

		public bool HasSpa {
			get { return hasSpa; }
			set { hasSpa = value; }
		}

		public bool HasStockMarkets {
			get { return hasStockMarkets; }
			set { hasStockMarkets = value; }
		}
	
		public bool HasWaterReclamation {
			get { return hasWaterReclamation; }
			set { hasWaterReclamation = value; }
		}
		
		public bool HasTurret {
			get { return hasTurret; }
			set { hasTurret = value; }
		}
		
		public bool HasIonCannon {
			get { return hasIonCannon; }
			set { hasIonCannon = value; }
		}
		
		#endregion
		
	};
	
}
