// created on 16-01-2005 at 18:15

using System;
using System.Collections;
using Chronos.Utils;
using Chronos.Core;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Messaging;

namespace Chronos.Info {
	
	public sealed class PlanetScanner {
		
		#region Scan Methods
		
		/// <summary>Indica se  possivel realizar um scan a uma coordenada</summary>
		public static Result CanPerformScan( Planet asker, Coordinate target )
		{
			Result result = new Result();
			
			if( !asker.isResourceAvailable("Building", "CommsSatellite", 1) ) {
				result.failed( new ResourceNotAvailable("Building","CommsSatellite") );
				return result;
			}
			
			if( Resource.GetResearchLevel(asker.Owner, "ScanLevel") == 0 ) {
				result.failed( new ResourceNotAvailable("Research", "ScanLevelI") );
				return result;
			}
			
			if( !Coordinate.IsAccessible(asker, target) ) {
				result.failed( new TargetNotAccessible(target) );
				return result;
			}
			
			Universe.CheckRestrictions(asker, result);
			
			return result;
		}
		
		/// <summary>Realiza um scan a uma coordenada<summary>
		public static Scan PerformScan( Planet asker, Coordinate target )
		{
			Scan scan = new Scan();
			scan.Turn = Universe.instance.TurnCount;
			
			scan.Target = target;
			scan.SourcePlanetId = asker.Id;
			scan.TargetPlanetOwner = -1;
			
			Planet targetPlanet = (Planet) Universe.instance.planets[target];
			if( targetPlanet.Owner == null ) {
				return GetScanToUnhabitedPlanet(scan, target);
			}
			
			scan.ScanLevel = Resource.GetResearchLevel((Ruler)asker.Owner, "ScanLevel");
			scan.TargetPlanetOwner = targetPlanet.Owner.Id;
			
			Ruler other = (Ruler) targetPlanet.Owner;
			int scanNotification = 0;
			int scanShield = 0;
			
			if( targetPlanet.isResourceAvailable("Building", "CommsSatellite") ) {
				scanNotification = Resource.GetResearchLevel(other, "ScanNotification");
				scanShield = Resource.GetResearchLevel(other, "ScanShieldLevel");
			}
			
			bool bonus = (MathUtils.Random % 100) < 60;
			
			scan.Intercepted = scanNotification > scan.ScanLevel;
			if(scanNotification == scan.ScanLevel) {
				scan.Intercepted = bonus;
			}
			
			scan.Success = scanShield < scan.ScanLevel;
			if(scanShield == scan.ScanLevel) {
				scan.Success = bonus;
			}
			
			FillScanInformation(targetPlanet, scan);
			CheckNotifications(asker, targetPlanet, scan);
			//CheckScore(asker.Owner, targetPlanet.Owner, scan);
			
			return scan;
		}
		
		/// <summary>Retorna um scan para um planeta desabitado</summary>
		public static Scan GetScanToUnhabitedPlanet(Scan scan, Coordinate target)
		{
			scan.ScanLevel = 2;
			scan.Target = target;
			scan.Intercepted = false;
			scan.Success = true;
			scan.InBattle = false;
			
			return scan;
		}
		
		/// <summary>Preenche o scan</summary>
		public static void FillScanInformation( Planet planet, Scan scan )
		{
			scan.Culture = planet.Culture;

			scan.HasCommsSatellite = planet.isResourceAvailable("Building", "CommsSatellite");
			scan.HasStarPort = planet.isResourceAvailable("Building", "StarPort");
			scan.HasGate = planet.isResourceAvailable("Building", "Gate");
			scan.HasStarGate = planet.isResourceAvailable("Building", "StarGate");
			scan.HasHospital = planet.isResourceAvailable("Building", "Hospital");
			scan.HasLandReclamation = planet.isResourceAvailable("Building", "LandReclamation");
			scan.HasMineralExtractor = planet.isResourceAvailable("Building", "MineralExtractor");
			scan.HasStockMarkets = planet.isResourceAvailable("Building", "StockMarkets");
			scan.HasSpa = planet.isResourceAvailable("Building", "Spa");
			scan.HasWaterReclamation = planet.isResourceAvailable("Building", "WaterReclamation");
			scan.HasIonCannon = planet.isResourceAvailable("Building", "IonCannon");
			scan.HasTurret = planet.isResourceAvailable("Building", "Turret");

			scan.NumberOfFleets = planet.Fleets.Count;
			scan.TotalShips = planet.TotalShips;
			scan.InBattle = planet.IsInBattle;
			scan.TotalBarracks = planet.getResourceCount("Building", "Barracks");
			scan.TotalSoldiers = planet.Spies + planet.Marines;
			
			ArrayList list = new ArrayList();
			foreach( ResourceFactory factory in planet.getAvailableFactories("Rare").Values ) {
				if( planet.isResourceAvailable(factory.Category, factory.Name, 1) ) {
					list.Add(factory.Name);
				}
			}
			scan.RareResources = (string[]) list.ToArray(typeof(string));
		}
		
		/// <summary>Verifica que mensagens tem para enviar</summary>
		public static void CheckNotifications( Planet source, Planet other, Scan scan )
		{
			if( scan.Intercepted ) {
				Messenger.Send(other, "ScanDetected", source.Coordinate.ToString(), other.Name, scan.Success.ToString());
			}
		}
		
		/// <summary>Adiciona score conforme o scan</summary>
		public static void CheckScore( IResourceManager source, IResourceManager other, Scan scan)
		{
			if( scan.Success ){
				source.addResource("score", 15);
			} else {
				other.addResource("score", 15);
			}
			
			if( scan.Intercepted ) {
				other.addResource("score", 15);
			} else {
				source.addResource("score", 15);
			}
		}
		
		#endregion
	
	};
	
}
